using System.Collections.Generic;
using System.Linq;

namespace ZeldaItemTracker
{
    public static class DungeonHelper
    {
        public static List<Reward> GetDungeonsFromString(string dungeonString)
        {
            var splitString = dungeonString.Split(',');
            var rewards = new List<Reward>();
            var usedRewards = new List<string>();
            foreach (var entry in splitString)
            {
                if(entry == "" || entry.Length != 4)
                {
                    rewards.Add(new Reward());
                    continue;
                }

                var dungeonShortName = entry.Substring(0, 2);
                if (!DungeonConstants.Dungeons.TryGetValue(dungeonShortName, out var dungeonName))
                    continue;

                var rewardShortName = entry.Substring(2, 2);
                if (!DungeonConstants.Rewards.TryGetValue(rewardShortName, out var rewardName))
                    continue;

                rewards.Add(new Reward
                {
                    DungeonName = dungeonName,
                    RewardName = rewardName
                });

                usedRewards.Add(rewardShortName);
            }

            if (splitString.Length == 6)
            {
                var unusedRewards = DungeonConstants.Rewards.Where(reward => !usedRewards.Any(ur => reward.Key == ur));

                foreach (var unusedReward in unusedRewards)
                {
                    rewards.Add(new Reward
                    {
                        RewardName = unusedReward.Value
                    });
                }
            }

            return rewards;
        }

        public static List<Reward> GetDefaultDungeons()
        {
            return new List<Reward>
            {
                new Reward { RewardName = DungeonConstants.Rewards["gm"]},
                new Reward { RewardName = DungeonConstants.Rewards["rm"]},
                new Reward { RewardName = DungeonConstants.Rewards["bm"]},
                new Reward { RewardName = DungeonConstants.Rewards["pm"]},
                new Reward { RewardName = DungeonConstants.Rewards["om"]},
                new Reward { RewardName = DungeonConstants.Rewards["lm"]},
                new Reward { RewardName = DungeonConstants.Rewards["ke"]},
                new Reward { RewardName = DungeonConstants.Rewards["gr"]},
                new Reward { RewardName = DungeonConstants.Rewards["zs"]}
            };
        }
    }
}
