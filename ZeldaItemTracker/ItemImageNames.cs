using System.Collections.Generic;

namespace ZeldaItemTracker
{
    public static class ItemImageNames
    {
        public static Dictionary<string, string> ItemImageMaps => new Dictionary<string, string>
        {
            { "bb", "bomb" },
            { "hs", "hookshot" },
            { "ls", "hookshot2" },
            { "str1", "lift" },
            { "str2", "lift2" },
            { "str3", "lift3" },
            { "bow", "bow" },
            { "fa", "arrowsleft" },
            { "la", "arrowsright" },
            { "ham", "hammer" },
            { "ib", "bootsleft" },
            { "hb", "bootsright" },
            { "ms", "mirror" },
            { "df", "din" },
            { "mag", "magic1" },
            { "sling", "sling" },
            { "rang", "boomerang" },
            { "btl", "bottle" },
            { "rl", "bottle2" },
            { "ss", "scale" },
            { "gs", "scale2" },
            { "lot", "lens" },
            { "fw", "farore" },
            { "zt", "bluetunic" },
            { "rt", "redtunic" },
            { "zl", "zelda" },
            { "ep", "epona" },
            { "sar", "saria" },
            { "suns", "sunsong" },
            { "sot", "songoftime" },
            { "sos", "songofstorms" },
            { "min", "green_note" },
            { "bol", "red_note" },
            { "ser", "blue_note" },
            { "req", "orange_note" },
            { "noct", "purple_note" },
            { "pre", "yellow_note" },
            { "x", "sold" }

        };
    }
}
