using konsume_v1.Core.Application.Interfaces.Repositories;
using konsume_v1.Core.Application.Interfaces.Services;
using konsume_v1.Core.Domain.Entities;

namespace konsume_v1.Core.Application.Services
{
    public class StreakService : IStreakService
    {
        private readonly IStreakRepository _streakRepository;
        private readonly IProfileRepository _profileRepository;

        public StreakService(IStreakRepository streakRepository, IProfileRepository profileRepository)
        {
            _streakRepository = streakRepository;
            _profileRepository = profileRepository;
        }


        public async Task<int> GetStreakCountByProfileIdAsync(int profileId)
        {
            var streak = await _streakRepository.GetStreakByProfileIdAsync(profileId);

            return streak?.StreakCount ?? 0;
        }
        

        public async Task<Streak> UpdateReadingStreakAsync(int profileId)
        {
            var profile = await _profileRepository.GetAsync(profileId);
            if (profile == null)
            {
                return new Streak
                {
                    ProfileId = profileId,
                    IsSuccessful = false,
                    Message = "Profile not found.",
                    StreakCount = 0
                };
            }

            var streak = await _streakRepository.GetStreakByProfileIdAsync(profileId);
            var currentTimestamp = DateTime.Now;

            if (streak == null)
            {
                streak = new Streak
                {
                    ProfileId = profileId,
                    StreakCount = 1,
                    DateCreated = currentTimestamp,
                    DateModified = currentTimestamp,
                    IsSuccessful = true,
                    Message = "New streak created.",
                    IsDeleted = false
                };

                await _streakRepository.CreateStreakAsync(streak);
                return streak;
            }

            var lastReadDate = streak.DateModified;

            if (lastReadDate == null)
            {
                streak.StreakCount = 1;
            }
            else
            {
                var currentDate = currentTimestamp.Date;
                var lastReadDateOnly = lastReadDate.Value.Date;

                if (currentDate > lastReadDateOnly)
                {
                    streak.StreakCount += 1;
                }
                else
                {
                    streak.Message = "Reading streak already updated for today.";
                    streak.IsSuccessful = true;
                    return streak;
                }
            }

            streak.DateModified = currentTimestamp;
            streak.IsSuccessful = true;
            streak.Message = "Reading streak updated successfully.";
            streak.IsDeleted = false;
            streak.ProfileId = profileId;

            await _streakRepository.UpdateStreakAsync(streak);

            return streak;
        }

    }
}