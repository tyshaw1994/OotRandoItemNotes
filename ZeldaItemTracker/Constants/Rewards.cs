using System.Collections.Generic;

namespace ZeldaItemTracker.Constants
{
    public static class Rewards
    {
        public static Dictionary<string, string> RewardNameMaps => new Dictionary<string, string>
        {
            { "lm", "Light Medallion" },
            { "fom", "Forest Medallion" },
            { "fim", "Fire Medallion" },
            { "wm", "Water Medallion" },
            { "spm", "Spirit Medallion" },
            { "shm", "Shadow Medallion" },
            { "ke", "Kokiri Emerald" },
            { "gr", "Goron Ruby" },
            { "zs", "Zora Sapphire" }
        };

        public static Dictionary<string, string> RewardImageMaps => new Dictionary<string, string>
        {
            { "lm", "lightmedallion" },
            { "fom", "forestmedallion" },
            { "fim", "firemedallion" },
            { "wm", "watermedallion" },
            { "spm", "spiritmedallion" },
            { "shm", "shadowmedallion" },
            { "ke", "emerald" },
            { "gr", "ruby" },
            { "zs", "sapphire" }
        };
    }
}
