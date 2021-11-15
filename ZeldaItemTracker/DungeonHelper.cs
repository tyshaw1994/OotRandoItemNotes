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
            var usedDungeons = new List<string>();
            int index = 0;

            var defaultRewards = GetDefaultDungeons();
            foreach (var entry in splitString)
            {
                // Switch this to entry length 4 if we ever use variable meds
                if (entry == "" || entry.Length != 2)
                {
                    rewards.Add(new Reward());
                    continue;
                }

                var dungeonShortName = entry.Substring(0, 2);
                if (!Dungeons.Names.TryGetValue(dungeonShortName, out var dungeonName))
                    continue;

                // Commenting this out because we aren't using variable meds
                //var rewardShortName = entry.Substring(2, 2);
                //if (!DungeonConstants.Rewards.TryGetValue(rewardShortName, out var rewardName))
                //    continue;

                defaultRewards[index].DungeonName = dungeonName;

                //usedRewards.Add(rewardShortName);
                usedDungeons.Add(dungeonShortName);

                index++;
            }

            if (splitString.Length == 6)
            {
                var unusedDungeons = Dungeons.Names.Where(dungeon => !usedDungeons.Any(ud => dungeon.Key == ud));

                foreach (var unusedDungeon in unusedDungeons)
                {
                    defaultRewards[index].DungeonName = unusedDungeon.Value;

                    index++;
                }
            }

            return defaultRewards;
        }

        public static List<Reward> GetDefaultDungeons()
        {
            return new List<Reward>
            {
                new Reward { RewardName = Dungeons.Rewards["lm"]},
                new Reward { RewardName = Dungeons.Rewards["gm"]},
                new Reward { RewardName = Dungeons.Rewards["rm"]},
                new Reward { RewardName = Dungeons.Rewards["bm"]},
                new Reward { RewardName = Dungeons.Rewards["pm"]},
                new Reward { RewardName = Dungeons.Rewards["om"]},
                new Reward { RewardName = Dungeons.Rewards["ke"]},
                new Reward { RewardName = Dungeons.Rewards["gr"]},
                new Reward { RewardName = Dungeons.Rewards["zs"]}
            };
        }
    }
}
