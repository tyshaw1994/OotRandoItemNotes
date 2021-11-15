using System.Collections.Generic;

namespace ZeldaItemTracker.Constants
{
    public class Bosses
    {
        public static Dictionary<string, string> BossImageMaps => new Dictionary<string, string>
        {
            { "gma", "gohma" },
            { "kd", "kingdong" },
            { "bari", "barinade" },
            { "pg", "phantomganon" },
            { "vag", "volvagia" },
            { "mor", "morpha" },
            { "bongo", "bongo" },
            { "rova", "twinrova" }
        };
    }
}
