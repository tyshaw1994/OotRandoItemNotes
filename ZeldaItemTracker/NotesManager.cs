using System.Collections.Generic;

namespace ZeldaItemTracker
{
    /// <summary>
    /// Manager class for notes.
    /// </summary>
    public static class NotesManager
    {
        /// <summary>
        /// Provides a static list of LocationNotes to use at startup.
        /// </summary>
        public static List<LocationNotes> BaseLocationNotes => 
            new List<LocationNotes>
            {
                new LocationNotes
                {
                    LocationName = "WOTH 1",
                    Items = ""
                },
                new LocationNotes
                {
                    LocationName = "WOTH 2",
                    Items = ""
                },
                new LocationNotes
                {
                    LocationName = "WOTH 3",
                    Items = ""
                },
                new LocationNotes
                {
                    LocationName = "WOTH 4",
                    Items = ""
                }
            };

        /// <summary>
        /// Converts a given abbreviation of a location, item, or song into a fully qualified name for display purposes.
        /// </summary>
        public static string GetFullName(string abbreviation)
        {
            if (LocationNames.LocationNameMaps.TryGetValue(abbreviation, out var locationName))
            {
                return locationName;
            }

            if (ItemNames.ItemNameMaps.TryGetValue(abbreviation, out var itemName))
            {
                return itemName;
            }

            if (SongNames.SongNameMaps.TryGetValue(abbreviation, out var songName))
            {
                return songName;
            }

            return abbreviation;
        }
    }
}
