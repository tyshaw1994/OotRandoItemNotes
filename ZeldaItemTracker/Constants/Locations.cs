using System.Collections.Generic;

namespace ZeldaItemTracker
{
    /// <summary>
    /// Static class for mapping location abbreviations to their full names.
    /// </summary>
    public static class Locations
    {
        /// <summary>
        /// Maps location abbreviations to their full names.
        /// </summary>
        public static Dictionary<string, string> LocationNameMaps => new Dictionary<string, string>
        {
            { "kf", "Kokiri Forest" },
            { "lw", "Lost Woods" },
            { "sfm", "Sacred Forest Meadow" },
            { "zr", "Zora's River" },
            { "zd", "Zora's Domain" },
            { "zf", "Zora's Fountain" },
            { "hf", "Hyrule Field" },
            { "lh", "Lake Hylia" },
            { "gv", "Gerudo Valley" },
            { "gf", "Gerudo Fortress" },
            { "hw", "Haunted Wasteland" },
            { "colo", "Desert Colossus" },
            { "mkt", "Market" },
            { "tot", "Temple of Time" },
            { "hc", "Hyrule Castle"},
            { "ogc", "Outside Ganon's Castle" },
            { "llr", "Lon Lon Ranch" },
            { "kak", "Kakariko Village" },
            { "gy", "Graveyard" },
            { "dmt", "Death Mountain Trail" },
            { "dmc", "Death Mountain Crater" },
            { "gc", "Goron City" },
            { "dt", "Deku Tree" },
            { "dc", "Dodongo's Cavern" },
            { "jb", "Jabu Jabu's Belly" },
            { "fo", "Forest Temple" },
            { "fi", "Fire Temple" },
            { "wa", "Water Temple" },
            { "sh", "Shadow Temple" },
            { "sp", "Spirit Temple" },
            { "ic", "Ice Cavern" },
            { "botw", "Bottom of the Well" },
            { "gtg", "Gerudo Training Grounds" },
            { "ganon", "Ganon's Castle" }
        };
    }
}
