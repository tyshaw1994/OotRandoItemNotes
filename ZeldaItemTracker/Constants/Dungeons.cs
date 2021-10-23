using System.Collections.Generic;

namespace ZeldaItemTracker
{
    public static class Dungeons
    {
        public static Dictionary<string, string> Names = new Dictionary<string, string>
        {
            { "dt", "deku" },
            { "dc", "dodongo" },
            { "jb", "jabu" },
            { "fo", "forest" },
            { "fi", "fire" },
            { "wa", "water" },
            { "sh", "shadow" },
            { "sp", "spirit" },
            { "fr", "free" }
        };

        public static Dictionary<string, string> Rewards = new Dictionary<string, string>
        {
            { "ke", "emerald" },
            { "gr", "ruby" },
            { "zs", "sapphire" },
            { "gm", "forestmedallion" },
            { "rm", "firemedallion" },
            { "bm", "watermedallion" },
            { "pm", "shadowmedallion" },
            { "om", "spiritmedallion" },
            { "lm", "lightmedallion" }
        };
    }
}
