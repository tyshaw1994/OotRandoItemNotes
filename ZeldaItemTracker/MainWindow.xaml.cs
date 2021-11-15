using MaterialDesignThemes.Wpf;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using ZeldaItemTracker.Constants;

namespace ZeldaItemTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        DateTime start;
        HintDistributionSettings hints;
        const string League = "league";
        const string DDR = "ddr";
        const string S5 = "s5";

        public MainWindow()
        {
            InitializeComponent();

            // Determine Hint Settings - default is S5
            hints = new HintDistributionSettings().GetSettingsByName(S5);
            SetHints();

            timer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0), DispatcherPriority.Background, Timer_Tick, Dispatcher.CurrentDispatcher)
            {
                IsEnabled = false
            };
        }

        private void SetHints()
        {
            var textBoxes = Main.Children.OfType<TextBox>();

            // SET WOTH
            for (int i = 1; i <= hints.NumberOfWoths; i++)
            {
                var wothBox = textBoxes.SingleOrDefault(textBox => textBox.Name.Equals($"WOTH{i}", StringComparison.OrdinalIgnoreCase));
                var wothItemBox = textBoxes.SingleOrDefault(textBox => textBox.Name.Equals($"WOTH{i}ITEMS", StringComparison.OrdinalIgnoreCase));

                HintAssist.SetHint(wothBox, $"WOTH {i}");
                wothBox.TextChanged += Location_TextChanged;
                wothItemBox.TextChanged += TextBox_TextChanged;
            }

            // SET GOAL
            for (int i = 1; i <= hints.NumberOfGoals; i++)
            {
                var goalBox = textBoxes.SingleOrDefault(textBox => textBox.Name.Equals($"WOTH{i}", StringComparison.OrdinalIgnoreCase));
                var goalItemBox = textBoxes.SingleOrDefault(textBox => textBox.Name.Equals($"WOTH{i}ITEMS", StringComparison.OrdinalIgnoreCase));

                HintAssist.SetHint(goalBox, $"Goal {i}");
                goalBox.TextChanged += Goal_TextChanged;
                goalItemBox.TextChanged += TextBox_TextChanged;
            }

            // SET OPPORTUNITY
            for (int i = 1; i <= hints.NumberOfOpportunity; i++)
            {
                var wothBox = textBoxes.SingleOrDefault(textBox => textBox.Name.Equals($"WOTH{i + hints.NumberOfWoths}", StringComparison.OrdinalIgnoreCase));
                HintAssist.SetHint(wothBox, $"Opportunity {i}");
                wothBox.TextChanged += Location_TextChanged;
            }

            // SET BARREN
            for (int i = 1; i <= hints.NumberOfBarren; i++)
            {
                var barrenBox = textBoxes.SingleOrDefault(textBox => textBox.Name.Equals($"BARREN{i}", StringComparison.OrdinalIgnoreCase));
                HintAssist.SetHint(barrenBox, $"Barren {i}");
                barrenBox.TextChanged += Location_TextChanged;
            }

            // SET SOMETIMES
            for (int i = 1; i <= hints.NumberOfSometimes; i++)
            {
                var sometimesBox = textBoxes.SingleOrDefault(textBox => textBox.Name.Equals($"SOMETIMES{i}", StringComparison.OrdinalIgnoreCase));
                HintAssist.SetHint(sometimesBox, $"Sometimes {i}");
                sometimesBox.TextChanged += TextBox_TextChanged;
            }

            // I'll have to figure out a better way to do this later, but for now the only difference between League and DDR is Biggoron (DDR) and 6 Sometimes (League)
            if (hints.Skulls)
            {
                Skulls30.TextChanged += TextBox_TextChanged;
                Skulls40.TextChanged += TextBox_TextChanged;
                Skulls50.TextChanged += TextBox_TextChanged;
            }

            if (hints.SkullMask)
            {
                SkullMask.TextChanged += TextBox_TextChanged;
            }

            if (hints.Frogs2)
            {
                Frogs2.TextChanged += TextBox_TextChanged;
            }

            if (hints.OOTSong)
            {
                OOTSong.TextChanged += TextBox_TextChanged;
            }

            if (hints.Biggoron)
            {
                HintAssist.SetHint(Sometimes6, "Biggoron");
                Sometimes6.TextChanged += TextBox_TextChanged;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Timer.Text = (DateTime.Now - start).ToString(@"hh\:mm\:ss");
        }

        private void HintItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount != 2)
                return;

            var image = sender as Image;
            image.Source = null;

            // Shift images if needed (only for WOTH for now)
            if (image.Name.StartsWith("WOTH", StringComparison.OrdinalIgnoreCase))
            {
                var wothNumber = image.Name.Substring(4, 1);
                var currentSlot = image.Name.Substring(image.Name.Length - 1, 1);

                var relevantItemSlots = Main.Children.OfType<Image>().Where(x => x.Name.StartsWith($"WOTH{wothNumber}", StringComparison.OrdinalIgnoreCase));
                var firstSlot = relevantItemSlots.Single(x => x.Name.Equals($"WOTH{wothNumber}ITEM1", StringComparison.OrdinalIgnoreCase));
                var secondSlot = relevantItemSlots.Single(x => x.Name.Equals($"WOTH{wothNumber}ITEM2", StringComparison.OrdinalIgnoreCase));
                var thirdSlot = relevantItemSlots.Single(x => x.Name.Equals($"WOTH{wothNumber}ITEM3", StringComparison.OrdinalIgnoreCase));

                switch (currentSlot)
                {
                    case "1":
                        // Check if 2 and 3 are active
                        if (secondSlot.Source != null)
                        {
                            firstSlot.Source = secondSlot.Source;

                            if (thirdSlot.Source != null)
                            {
                                secondSlot.Source = thirdSlot.Source;
                                thirdSlot.Source = null;
                            }
                            else
                            {
                                secondSlot.Source = null;
                            }
                        }

                        break;

                    case "2":
                        // Check if 3 is active
                        if (thirdSlot.Source != null)
                        {
                            secondSlot.Source = thirdSlot.Source;
                            thirdSlot.Source = null;
                        }

                        break;

                    case "3":
                        // Do nothing
                        break;

                    default:
                        // Shouldn't happen
                        break;
                }
            }
            else
            {
                image.Source = null;
            }
        }

        private void ResetDungeons_Click(object sender, RoutedEventArgs e)
        {
            var defaultLayout = DungeonHelper.GetDefaultDungeons();
            var medallionLabels = Main.Children.OfType<Image>().Where(x => x.Name.StartsWith("medallion") && x.Name.Contains("label")).ToList();
            var medallions = Main.Children.OfType<Image>().Where(x => x.Name.StartsWith("medallion") && !x.Name.Contains("label")).ToList();


            int index = 0;
            foreach (var dungeonReward in defaultLayout.Take(6))
            {
                var medallionLabel = medallionLabels[index];
                var medallion = medallions[index];

                if (string.IsNullOrEmpty(dungeonReward.DungeonName))
                {
                    medallionLabel.Source = new BitmapImage(new System.Uri(@$"/images/unknown-small.png", System.UriKind.Relative));
                }
                else
                {
                    medallionLabel.Source = new BitmapImage(new System.Uri(@$"/images/{dungeonReward.DungeonName}.png", System.UriKind.Relative));
                }

                if (string.IsNullOrEmpty(dungeonReward.RewardName))
                {
                    continue;
                }
                else
                {
                    medallion.Source = new BitmapImage(new System.Uri(@$"/images/{dungeonReward.RewardName}-disabled.png", System.UriKind.Relative));
                }

                index++;
            }

            var stones = Main.Children.OfType<Image>().Where(x => x.Name.StartsWith("stone") && !x.Name.Contains("label")).ToList();

            index = 0;
            foreach (var uselessReward in defaultLayout.TakeLast(3))
            {
                var stone = stones[index];

                if (string.IsNullOrEmpty(uselessReward.RewardName))
                {
                    continue;
                }
                else
                {
                    stone.Source = new BitmapImage(new System.Uri(@$"/images/{uselessReward.RewardName}-disabled.png", System.UriKind.Relative));
                }

                index++;
            }
        }

        private void Card_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Location_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            textBox.Text = NotesManager.GetFullName(textBox.Text);
        }

        private void Goal_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Check to see if there's a reward keyed in
            var textBox = sender as TextBox;

            // Loop over the entire string until we find a match or get to the end.
            for (int i = 1; i <= textBox.Text.Length; i++)
            {
                string fileName = "";
                if (Rewards.RewardImageMaps.TryGetValue(textBox.Text.Substring(0, i), out fileName) || Bosses.BossImageMaps.TryGetValue(textBox.Text.Substring(0, i), out fileName))
                {
                    var index = textBox.Name.Last().ToString();

                    var relevantItemSlot = Main.Children.OfType<Image>().FirstOrDefault(x => x.Name.Equals($"Goal{index}", StringComparison.OrdinalIgnoreCase));
                    if (relevantItemSlot != null)
                    {
                        relevantItemSlot.Source = new BitmapImage(new Uri($@"/images/{fileName}.png", UriKind.Relative));
                    }

                    textBox.Text = textBox.Text.Substring(i);
                }
            }

            Location_TextChanged(sender, e);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;

            // Figure out which, if any, images are activated
            if (textBox.Name.Contains("WOTH", StringComparison.OrdinalIgnoreCase) && textBox.Name.Contains("ITEM", StringComparison.OrdinalIgnoreCase))
            {
                var imageName = Items.ItemImageMaps.FirstOrDefault(x => x.Key.Equals(textBox.Text)).Value;
                if (imageName == null)
                    return;

                // 3 Item slots per WOTH
                var wothNumber = textBox.Name.Substring(4, 1);
                var relevantItemSlots = Main.Children.OfType<Image>().Where(x => x.Name.StartsWith($"WOTH{wothNumber}", StringComparison.OrdinalIgnoreCase));
                var itemSlotToUse = relevantItemSlots.FirstOrDefault(x => x.Source == null);

                if (itemSlotToUse != null)
                {
                    itemSlotToUse.Source = new BitmapImage(new Uri($@"/images/{imageName}.png", UriKind.Relative));
                }

                textBox.Text = "";
            }
            else if (textBox.Name.Contains("SOMETIMES", StringComparison.OrdinalIgnoreCase))
            {
                var itemText = textBox.Text.Split(" ").Last();

                var imageName = Items.ItemImageMaps.FirstOrDefault(x => x.Key.Equals(itemText)).Value;
                if (imageName == null)
                    return;

                var relevantItemSlot = Main.Children.OfType<Image>().FirstOrDefault(x => x.Name.StartsWith(textBox.Name, StringComparison.OrdinalIgnoreCase));
                if (relevantItemSlot != null)
                {
                    relevantItemSlot.Source = new BitmapImage(new Uri($@"/images/{imageName}.png", UriKind.Relative));
                }

                textBox.Text = textBox.Text.Substring(0, textBox.Text.Length - itemText.Length);
            }
            else
            {
                var imageName = Items.ItemImageMaps.FirstOrDefault(x => x.Key.Equals(textBox.Text)).Value;
                if (imageName == null)
                    return;

                var relevantItemSlot = Main.Children.OfType<Image>().FirstOrDefault(x => x.Name.StartsWith(textBox.Name, StringComparison.OrdinalIgnoreCase));
                if (relevantItemSlot != null)
                {
                    relevantItemSlot.Source = new BitmapImage(new Uri($@"/images/{imageName}.png", UriKind.Relative));
                }

                textBox.Text = "";
            }
        }

        private void Item_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var clickedImage = sender as System.Windows.Controls.Image;

            // Get all grouped items by name
            var imageFileNames = Directory.GetFiles("images/").Where(file => file.Contains(clickedImage.Name) && !file.Contains("disabled"))
                .OrderBy(x => x)
                .ToList();

            if (imageFileNames.Count > 1)
            {
                var currentSource = clickedImage.Source.ToString();
                var currentFile = currentSource.Substring(currentSource.LastIndexOf('/') + 1, (currentSource.IndexOf(".png") - currentSource.LastIndexOf('/')) - 1);
                if (currentFile.Contains("disabled"))
                {
                    clickedImage.Source = new BitmapImage(new System.Uri(@$"/images/{clickedImage.Name}.png", System.UriKind.Relative));
                    return;
                }

                if (int.TryParse(currentFile.Substring(currentFile.Length - 1, 1), out var currentIndex))
                {
                    if (currentIndex + 1 > imageFileNames.Count)
                        return;

                    clickedImage.Source = new BitmapImage(new System.Uri(@$"/images/{clickedImage.Name}{currentIndex + 1}.png", System.UriKind.Relative));
                }
                else
                {
                    clickedImage.Source = new BitmapImage(new System.Uri(@$"/{imageFileNames[1]}", System.UriKind.Relative));
                }
            }
            else
            {
                clickedImage.Source = new BitmapImage(new System.Uri(@$"/images/{clickedImage.Name}.png", System.UriKind.Relative));
            }
        }

        private void Item_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var clickedImage = sender as System.Windows.Controls.Image;

            // Get all grouped items by name
            var imageFileNames = Directory.GetFiles("images/").Where(file => file.Contains(clickedImage.Name) && !file.Contains("disabled"))
                .OrderBy(x => x)
                .ToList();

            if (imageFileNames.Count > 1)
            {
                var currentSource = clickedImage.Source.ToString();
                var currentFile = currentSource.Substring(currentSource.LastIndexOf('/') + 1, (currentSource.IndexOf(".png") - currentSource.LastIndexOf('/')) - 1);
                if (currentFile.Contains("disabled"))
                {
                    return;
                }

                if (int.TryParse(currentFile.Substring(currentFile.Length - 1, 1), out var currentIndex))
                {
                    if (currentIndex - 1 <= 1)
                    {
                        clickedImage.Source = new BitmapImage(new System.Uri(@$"/images/{clickedImage.Name}.png", System.UriKind.Relative));
                        return;
                    }

                    clickedImage.Source = new BitmapImage(new System.Uri(@$"/images/{clickedImage.Name}{currentIndex - 1}.png", System.UriKind.Relative));
                }
                else
                {
                    clickedImage.Source = new BitmapImage(new System.Uri(@$"/images/{clickedImage.Name}-disabled.png", System.UriKind.Relative));
                }
            }
            else
            {
                clickedImage.Source = new BitmapImage(new System.Uri(@$"/images/{clickedImage.Name}-disabled.png", System.UriKind.Relative));
            }
        }

        private void CompoundItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var clickedImage = sender as System.Windows.Controls.Image;
            var currentSource = clickedImage.Source.ToString();
            var currentFile = currentSource.Substring(currentSource.LastIndexOf('/') + 1, (currentSource.IndexOf(".png") - currentSource.LastIndexOf('/')) - 1);

            if (currentFile.Contains("disabled"))
            {
                var activeFileName = currentFile.Substring(0, currentFile.IndexOf("-disabled"));
                clickedImage.Source = new BitmapImage(new System.Uri(@$"/images/{activeFileName}left.png", System.UriKind.Relative));
            }
            else if (currentFile.Contains("right"))
            {
                var activeFileName = currentFile.Substring(0, currentFile.IndexOf("right"));
                clickedImage.Source = new BitmapImage(new System.Uri(@$"/images/{activeFileName}.png", System.UriKind.Relative));
            }
            else if (currentFile.Contains("left"))
            {
                var activeFileName = currentFile.Substring(0, currentFile.IndexOf("left"));
                clickedImage.Source = new BitmapImage(new System.Uri(@$"/images/{activeFileName}-disabled.png", System.UriKind.Relative));
            }
            else
            {
                clickedImage.Source = new BitmapImage(new System.Uri(@$"/images/{currentFile}right.png", System.UriKind.Relative));
            }
        }

        private void CompoundItem_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var clickedImage = sender as System.Windows.Controls.Image;
            var currentSource = clickedImage.Source.ToString();
            var currentFile = currentSource.Substring(currentSource.LastIndexOf('/') + 1, (currentSource.IndexOf(".png") - currentSource.LastIndexOf('/')) - 1);

            if (currentFile.Contains("disabled"))
            {
                var activeFileName = currentFile.Substring(0, currentFile.IndexOf("-disabled"));
                clickedImage.Source = new BitmapImage(new System.Uri(@$"/images/{activeFileName}right.png", System.UriKind.Relative));
            }
            else if (currentFile.Contains("left"))
            {
                var activeFileName = currentFile.Substring(0, currentFile.IndexOf("left"));
                clickedImage.Source = new BitmapImage(new System.Uri(@$"/images/{activeFileName}.png", System.UriKind.Relative));
            }
            else if (currentFile.Contains("right"))
            {
                var activeFileName = currentFile.Substring(0, currentFile.IndexOf("right"));
                clickedImage.Source = new BitmapImage(new System.Uri(@$"/images/{activeFileName}-disabled.png", System.UriKind.Relative));
            }
            else
            {
                clickedImage.Source = new BitmapImage(new System.Uri(@$"/images/{currentFile}left.png", System.UriKind.Relative));
            }
        }

        private void Song_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var clickedImage = sender as System.Windows.Controls.Image;
            var currentSource = clickedImage.Source.ToString();
            var currentFile = currentSource.Substring(currentSource.LastIndexOf('/') + 1, (currentSource.IndexOf(".png") - currentSource.LastIndexOf('/')) - 1);

            if (currentFile.Contains("disabled"))
            {
                if (currentFile.Contains("check"))
                {
                    var activeFileName = currentFile.Substring(0, currentFile.IndexOf("-disabled"));
                    clickedImage.Source = new BitmapImage(new System.Uri(@$"/images/{activeFileName}-check.png", System.UriKind.Relative));
                }
                else
                {
                    var activeFileName = currentFile.Substring(0, currentFile.IndexOf("-disabled"));
                    clickedImage.Source = new BitmapImage(new System.Uri(@$"/images/{activeFileName}.png", System.UriKind.Relative));
                }
            }
            else
            {
                if (currentFile.Contains("check"))
                {
                    var activeFileName = currentFile.Substring(0, currentFile.IndexOf("-check"));
                    clickedImage.Source = new BitmapImage(new System.Uri(@$"/images/{activeFileName}-disabledcheck.png", System.UriKind.Relative));
                }
                else
                {
                    clickedImage.Source = new BitmapImage(new System.Uri(@$"/images/{currentFile}-disabled.png", System.UriKind.Relative));
                }
            }
        }

        private void Song_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var clickedImage = sender as System.Windows.Controls.Image;
            var currentSource = clickedImage.Source.ToString();
            var currentFile = currentSource.Substring(currentSource.LastIndexOf('/') + 1, (currentSource.IndexOf(".png") - currentSource.LastIndexOf('/')) - 1);

            if (currentFile.Contains("disabled"))
            {
                if (currentFile.Contains("check"))
                {
                    var activeFileName = currentFile.Substring(0, currentFile.IndexOf("-disabled"));
                    clickedImage.Source = new BitmapImage(new System.Uri(@$"/images/{activeFileName}-disabled.png", System.UriKind.Relative));
                }
                else
                {
                    var activeFileName = currentFile.Substring(0, currentFile.IndexOf("-disabled"));
                    clickedImage.Source = new BitmapImage(new System.Uri(@$"/images/{activeFileName}-disabledcheck.png", System.UriKind.Relative));
                }
            }
            else
            {
                if (currentFile.Contains("check"))
                {
                    var activeFileName = currentFile.Substring(0, currentFile.IndexOf("-check"));
                    clickedImage.Source = new BitmapImage(new System.Uri(@$"/images/{activeFileName}.png", System.UriKind.Relative));
                }
                else
                {
                    clickedImage.Source = new BitmapImage(new System.Uri(@$"/images/{currentFile}-check.png", System.UriKind.Relative));
                }
            }
        }

        private void Medallion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var clickedImage = sender as System.Windows.Controls.Image;
            var currentSource = clickedImage.Source.ToString();
            var currentFile = currentSource.Substring(currentSource.LastIndexOf('/') + 1, (currentSource.IndexOf(".png") - currentSource.LastIndexOf('/')) - 1);

            if (!currentFile.Contains("disabled"))
                return;

            var activeFileName = currentFile.Substring(0, currentFile.IndexOf("-disabled"));

            clickedImage.Source = new BitmapImage(new System.Uri(@$"/images/{activeFileName}.png", System.UriKind.Relative));
        }

        private void Medallion_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var clickedImage = sender as System.Windows.Controls.Image;
            var currentSource = clickedImage.Source.ToString();
            var currentFile = currentSource.Substring(currentSource.LastIndexOf('/') + 1, (currentSource.IndexOf(".png") - currentSource.LastIndexOf('/')) - 1);

            if (currentFile.Contains("disabled"))
                return;

            clickedImage.Source = new BitmapImage(new System.Uri(@$"/images/{currentFile}-disabled.png", System.UriKind.Relative));
        }

        private void Dungeons_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
                return;

            try
            {
                var dungeonRewards = DungeonHelper.GetDungeonsFromString(Dungeons.Text);
                var medallionLabels = Main.Children.OfType<Image>().Where(x => x.Name.StartsWith("medallion") && x.Name.Contains("label")).ToList();
                var medallions = Main.Children.OfType<Image>().Where(x => x.Name.StartsWith("medallion") && !x.Name.Contains("label")).ToList();

                if (dungeonRewards.Count <= 6)
                {
                    int index = 0;
                    foreach (var dungeonReward in dungeonRewards)
                    {
                        var medallionLabel = medallionLabels[index];
                        var medallion = medallions[index];

                        if (string.IsNullOrEmpty(dungeonReward.DungeonName))
                        {
                            medallionLabel.Source = new BitmapImage(new System.Uri(@$"/images/unknown-small.png", System.UriKind.Relative));
                        }
                        else
                        {
                            medallionLabel.Source = new BitmapImage(new System.Uri(@$"/images/{dungeonReward.DungeonName}.png", System.UriKind.Relative));
                        }

                        if (string.IsNullOrEmpty(dungeonReward.RewardName))
                        {
                            continue;
                        }
                        else
                        {
                            medallion.Source = new BitmapImage(new System.Uri(@$"/images/{dungeonReward.RewardName}-disabled.png", System.UriKind.Relative));
                        }

                        index++;
                    }
                }
                else
                {
                    int index = 0;
                    foreach (var dungeonReward in dungeonRewards.Take(6))
                    {
                        var medallionLabel = medallionLabels[index];
                        var medallion = medallions[index];

                        if (string.IsNullOrEmpty(dungeonReward.DungeonName))
                        {
                            medallionLabel.Source = new BitmapImage(new System.Uri(@$"/images/unknown-small.png", System.UriKind.Relative));
                        }
                        else
                        {
                            medallionLabel.Source = new BitmapImage(new System.Uri(@$"/images/{dungeonReward.DungeonName}.png", System.UriKind.Relative));
                        }

                        index++;
                    }

                    var stoneLabels = Main.Children.OfType<Image>().Where(x => x.Name.StartsWith("stone") && x.Name.Contains("label")).ToList();
                    var stones = Main.Children.OfType<Image>().Where(x => x.Name.StartsWith("stone") && !x.Name.Contains("label")).ToList();
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
            }
            else
            {
                timer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0), DispatcherPriority.Background, Timer_Tick, Dispatcher.CurrentDispatcher)
                {
                    IsEnabled = true
                };

                start = DateTime.Now;
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                start = DateTime.Now;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.LeftShift) && e.Key != Key.LeftCtrl && e.Key != Key.LeftShift)
            {
                var allHints = new HintDistributionSettings().AllHints();

                var relevantHints = allHints.SingleOrDefault(hints => hints.KeyBind == e.Key);
                if (relevantHints != null)
                {
                    hints = relevantHints;
                    SetHints();
                }
            }
        }
    }
}
