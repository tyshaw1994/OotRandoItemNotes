using System.Collections.Generic;

namespace ZeldaItemTracker
{
    public static class Items
    {
        public static Dictionary<string, string> ItemNameMaps => new Dictionary<string, string>
        {
            { "bb", "Bomb Bag" },
            { "hs", "Hookshot" },
            { "ls", "Longshot" },
            { "str1", "Goron Bracelet" },
            { "str2", "Silver Gauntlets" },
            { "str3", "Gold Gauntlets" },
            { "bow", "Bow" },
            { "fa", "Fire Arrows" },
            { "la", "Light Arrows" },
            { "ham", "Hammer" },
            { "ib", "Iron Boots" },
            { "hb", "Hover Boots" },
            { "ms", "Mirror Shield" },
            { "df", "Din's Fire" },
            { "mag", "Magic Meter" },
            { "sling", "Slingshot" },
            { "rang", "Boomerang" },
            { "btl", "Bottle" },
            { "rl", "Ruto's Letter" },
            { "ss", "Silver Scale" },
            { "gs", "Gold Scale" },
            { "lot", "Lens of Truth" },
            { "fw", "Farore's Wind" },
            { "sk", "smallkey" },
            { "bk", "bigkey" }
        };

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
            { "x", "sold" },
            { "sk", "smallkey" },
            { "bk", "bigkey" }

        };
    }
}
