using System.Collections.Generic;

namespace ZeldaItemTracker
{
    public static class Songs
    {
        public static Dictionary<string, string> SongNameMaps => new Dictionary<string, string>
        {
            { "zl", "Zelda's Lullaby" },
            { "ep", "Epona's Song" },
            { "sar", "Saria's Song" },
            { "suns", "Sun's Song" },
            { "sot", "Song of Time" },
            { "sos", "Song of Storms" },
            { "min", "Minuet" },
            { "bol", "Bolero" },
            { "ser", "Serenade" },
            { "req", "Requiem" },
            { "noct", "Nocturne" },
            { "pre", "Prelude" }
        };
    }
}
