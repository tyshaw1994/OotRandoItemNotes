using System;
using System.Linq;

namespace ZeldaItemTracker
{
    public class LocationNotes
    {
        public LocationNotes()
        {
        }

        public LocationNotes(string lineText)
        {
            var split = lineText.Split(' ');

            for(int i = 0; i < split.Length; i++)
            {
                if(split[i].Contains("items"))
                {
                    break;
                }

                LocationName += $"{split[i]} ";
            }

            var itemStartIndex = split.ToList().IndexOf("items:") + 1;

            for(int i = itemStartIndex; i < split.Length; i++)
            {
                Items += $"{split[i]} ";
            }
        }

        public string LocationName { get; set; }

        public string Items { get; set; }
    }
}
