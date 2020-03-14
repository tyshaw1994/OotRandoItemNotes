using System.Collections.Generic;

namespace ZeldaItemTracker
{
    /// <summary>
    /// Static class for mapping location abbreviations to their full names.
    /// </summary>
    public static class LocationNames
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
            { "dc", "Desert Colossus" },
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
            { "dong", "Dodongo's Cavern" },
            { "jabu", "Jabu Jabu's Belly" },
            { "fot", "Forest Temple" },
            { "fit", "Fire Temple" },
            { "wt", "Water Temple" },
            { "shdw", "Shadow Temple" },
            { "spt", "Spirit Temple" },
            { "ic", "Ice Cavern" },
            { "botw", "Bottom of the Well" },
            { "gtg", "Gerudo Training Grounds" },
            { "ganon", "Ganon's Castle" }
        };
    }
}
