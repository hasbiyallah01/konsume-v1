using System.Text.RegularExpressions;
using konsume_v1.Core.Application.Interfaces.Repositories;
using konsume_v1.Core.Application.Interfaces.Services;
using konsume_v1.Core.Domain.Entities;
using konsume_v1.Infrastructure.Context;
using konsume_v1.Models;
using OpenAI_API;
using OpenAI_API.Chat;
using Microsoft.EntityFrameworkCore;

namespace konsume_v1.Core.Application.Services
{
    public class MealRecommendationService : IMealRecommendationService
    {
        private readonly KonsumeContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IProfileRepository _profileRepository;

        public MealRecommendationService(KonsumeContext dbContext, IProfileRepository profileRepository, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _profileRepository = profileRepository;
            _configuration = configuration;
        }

        public async Task SaveDailyRecommendationsAsync(int profileId, string dateSeed, string meals)
        {
            var existingRecommendation = await _dbContext.MealRecommendations.FirstOrDefaultAsync(m => m.ProfileId == profileId && m.DateSeed == dateSeed);

            if (existingRecommendation != null)
            {
                existingRecommendation.Meals = meals;
                _dbContext.MealRecommendations.Update(existingRecommendation);
            }
            else
            {
                var newRecommendation = new MealRecommendation
                {
                    ProfileId = profileId,
                    DateSeed = dateSeed,
                    Meals = meals
                };
                await _dbContext.MealRecommendations.AddAsync(newRecommendation);
            }

            await _dbContext.SaveChangesAsync();
        }


        public async Task<BaseResponse<MealDetails>> GenerateMealDetailsAsync(string mealName, int profileId)
        {
            var profile = await _profileRepository.GetAsync(profileId);
            if(profile == null)
            {
                return new BaseResponse<MealDetails>
                {
                     IsSuccessful = false,
                     Message = "Profile doesnt exist",
                     Value = null
                };
            }
            int age = DateTime.Now.Year - profile.DateOfBirth.Year;
            if (profile.DateOfBirth > DateTime.Now.AddYears(-age)) age--;
            var prompt = $@"
            Act as a health-focused meal assistant.

            For the meal: **{mealName}**

            User Profile:
            - Goal: {profile.UserGoals}
            - Allergies: {profile.Allergies}
            - Nationality: {profile.Nationality}
            - Diet Type: {profile.DietType}
            - Gender: {profile.Gender}
            - Age: {age}

            Return the following information in this exact format (Markdown):

            ---
            **Meal Name:**  
            [Meal Name]
            
            **General Health Score:**  
            [Score]/100 — based on average population health standards.

            **Personalized Health Score:**  
            [Score]/100 — based on this user's age, gender, allergies, goals, and diet type.


            **Description:**  
            [A short paragraph describing the meal’s origin, main ingredients, and general characteristics.]

            **Tags:**  
            - Meal  
            - [Food Type like Solid Food / Liquid / Drink]  
            - [Flavor like Spicy / Sweet / Savory]  
            - [Nutritional tags like Moderate Carbs / Oil Rich]  
            - [Cultural region like West Africa]

            **Nutritional Information (per 1 cup serving):**  
            - Calories: [xxx kcal]  
            - Carbs: [xxg]  
            - Protein: [xxg]  
            - Fat: [xxg]  
            - Fiber: [xxg]  
            - Sodium: [xxx mg]  
            - …

            **Recipe Steps:**  
            - Step 1  
            - Step 2  
            - …

            **Description/Usage:**  
            [A sentence or two on how the meal is typically served or enjoyed.]

            **Recommended Alternatives:**  
            - [Alternative 1]  
            - [Alternative 2]  
            - [Alternative 3]
            ---
            Only return the information in this exact structure. No explanations or commentary.";

            string apiKey = _configuration["OpenAI:APIKey"];
            try
            {
                var openai = new OpenAIAPI(apiKey);
                var chatRequest = new ChatRequest
                {
                    Model = "gpt-4",
                    Messages = new[]
                    {
                        new ChatMessage(ChatMessageRole.System, "FoodieAI is a food assistant that returns structured meal responses."),
                        new ChatMessage(ChatMessageRole.User, prompt)
                    }
                };

                var result = await openai.Chat.CreateChatCompletionAsync(chatRequest);
                var markdown = result.Choices.FirstOrDefault()?.Message.Content;
                if (string.IsNullOrWhiteSpace(markdown))
                    throw new Exception("No response from AI.");
                return new BaseResponse<MealDetails>
                {
                    IsSuccessful = true,
                    Message = "Meal Retrieved Successfully",
                    Value = ParseMealMarkdown(markdown)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<MealDetails>
                {
                    IsSuccessful = false,
                    Message = $"Error: {ex.Message}",
                    Value = null
                };
            }
        }

        public MealDetails ParseMealMarkdown(string markdown)
        {
            var lines = markdown.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var meal = new MealDetails();
            var nutrition = new NutritionalInformation();

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Trim();

                if (line.StartsWith("**Meal Name:**"))
                    meal.MealName = lines[++i].Trim();

                else if (line.StartsWith("**General Health Score:**"))
                    meal.GeneralHealthScore = ExtractScore(lines[++i]);

                else if (line.StartsWith("**Personalized Health Score:**"))
                    meal.PersonalizedHealthScore = ExtractScore(lines[++i]);

                else if (line.StartsWith("**Description:**"))
                    meal.Description = lines[++i].Trim();

                else if (line.StartsWith("**Tags:**"))
                {
                    var tags = new List<string>();
                    while (++i < lines.Length && lines[i].Trim().StartsWith("-"))
                        tags.Add(lines[i].Trim().TrimStart('-').Trim());
                    i--;
                    meal.Tags = tags;
                }
                else if (line.StartsWith("**Nutritional Information"))
                {
                    while (++i < lines.Length && lines[i].Trim().StartsWith("-"))
                    {
                        var parts = lines[i].Split(":", 2);
                        var key = parts[0].Trim().TrimStart('-');
                        var value = parts[1].Trim();

                        switch (key)
                        {
                            case " Calories": nutrition.Calories = value; break;
                            case " Carbs": nutrition.Carbs = value; break;
                            case " Protein": nutrition.Protein = value; break;
                            case " Fat": nutrition.Fat = value; break;
                            case " Fiber": nutrition.Fiber = value; break;
                            case " Sodium": nutrition.Sodium = value; break;
                            default:
                                Console.WriteLine($"Unknown nutrition key: '{key}'");
                                break;
                        }
                    }
                    i--;
                    meal.Nutrition = nutrition;
                }
                else if (line.StartsWith("**Recipe Steps:**"))
                {
                    var steps = new List<string>();
                    while (++i < lines.Length && lines[i].Trim().StartsWith("-"))
                        steps.Add(lines[i].Trim().TrimStart('-').Trim());
                    i--;
                    meal.RecipeSteps = steps;
                }
                else if (line.StartsWith("**Description/Usage:**"))
                    meal.Usage = lines[++i].Trim();

                else if(line.StartsWith("**Recommended Alternatives:**"))
                {
                    i++;
                    while (i < lines.Length && lines[i].StartsWith("-"))
                    {
                        meal.Alternatives.Add(lines[i].TrimStart('-').Trim());
                        i++;
                    }
                    continue;
                }
            }

            return meal;
        }

        private int ExtractScore(string scoreLine)
        {
            var match = Regex.Match(scoreLine, @"(\d+)");
            return match.Success ? int.Parse(match.Groups[1].Value) : 0;
        }


    }

    public class MealDetails
    {
        public string MealName { get; set; }
        public int GeneralHealthScore { get; set; }
        public int PersonalizedHealthScore { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; } = new();
        public NutritionalInformation Nutrition { get; set; }
        public List<string> RecipeSteps { get; set; } = new();
        public string Usage { get; set; }
        public List<string> Alternatives { get; set; } = new();
    }

    public class NutritionalInformation
    {
        public string Calories { get; set; }
        public string Carbs { get; set; }
        public string Protein { get; set; }
        public string Fat { get; set; }
        public string Fiber { get; set; }
        public string Sodium { get; set; }
    }
}