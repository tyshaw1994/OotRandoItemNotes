using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace ZeldaItemTracker
{
    /// <summary>
    /// Settings class for setting the hints for different settings
    /// </summary>
    public class HintDistributionSettings
    {
        public List<HintDistributionSettings> AllHints()
        {
            return new List<HintDistributionSettings>
            {
                new HintDistributionSettings
                {
                    Name = "league",
                    KeyBind = Key.L,
                    NumberOfWoths = 5,
                    NumberOfGoals = 0,
                    NumberOfOpportunity = 0,
                    NumberOfBarren = 3,
                    NumberOfSometimes = 6,
                    Skulls = true,
                    SkullMask = true,
                    OOTSong = true,
                    Biggoron = false,
                    Frogs2 = true
                },
                new HintDistributionSettings
                {
                    Name = "s5",
                    KeyBind = Key.S,
                    NumberOfWoths = 0,
                    NumberOfGoals = 5,
                    NumberOfOpportunity = 0,
                    NumberOfBarren = 3,
                    NumberOfSometimes = 5,
                    Skulls = true,
                    SkullMask = true,
                    OOTSong = true,
                    Biggoron = true,
                    Frogs2 = true
                },
                new HintDistributionSettings
                {
                    Name = "ddr",
                    KeyBind = Key.D,
                    NumberOfWoths = 2,
                    NumberOfGoals = 0,
                    NumberOfOpportunity = 3,
                    NumberOfBarren = 3,
                    NumberOfSometimes = 5,
                    Skulls = true,
                    SkullMask = true,
                    OOTSong = true,
                    Biggoron = true,
                    Frogs2 = true
                }
            };
        }

        public HintDistributionSettings() { }

        public HintDistributionSettings GetSettingsByName(string name)
        {
            return AllHints().SingleOrDefault(x => x.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// The name of the settings the hint distribution is associated to (DDR, League).
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The key to be pressed, along with Shift + CTRL, to switch to this specific hint distribution.
        /// </summary>
        public Key KeyBind { get; set; }

        /// <summary>
        /// The number of Way of the Hero hints available.
        /// </summary>
        public int NumberOfWoths { get; set; }

        /// <summary>
        /// The number of Goal hints available.
        /// </summary>
        public int NumberOfGoals { get; set; }

        /// <summary>
        /// The number of Opportunity/Path of the Hero hints available.
        /// </summary>
        public int NumberOfOpportunity { get; set; }

        /// <summary>
        /// The number of Barren hints available
        /// </summary>
        public int NumberOfBarren { get; set; }

        /// <summary>
        /// The number of Sometimes hints available
        /// </summary>
        public int NumberOfSometimes { get; set; }

        /// <summary>
        /// Indicates whether Skull (30, 40, 50) always hints are enabled.
        /// </summary>
        public bool Skulls { get; set; }

        /// <summary>
        /// Indicates whether the Skull Mask always hint is enabled.
        /// </summary>
        public bool SkullMask { get; set; }

        /// <summary>
        /// Indicates whether the OOT Song always hint is enabled.
        /// </summary>
        public bool OOTSong { get; set; }

        /// <summary>
        /// Indicates whether the Biggoron always hint is enabled.
        /// </summary>
        public bool Biggoron { get; set; }

        /// <summary>
        /// Indicates whether the Frogs 2 always hint is enabled.
        /// </summary>
        public bool Frogs2 { get; set; }
    }
}
