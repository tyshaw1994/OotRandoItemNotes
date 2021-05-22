using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Threading;

namespace ZeldaItemTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        DateTime start;

        public MainWindow()
        {
            InitializeComponent();

            Woth1.TextChanged += Location_TextChanged;
            Woth2.TextChanged += Location_TextChanged;
            Woth3.TextChanged += Location_TextChanged;
            Woth4.TextChanged += Location_TextChanged;
            Woth5.TextChanged += Location_TextChanged;
            Woth1Items.TextChanged += TextBox_TextChanged;
            Woth2Items.TextChanged += TextBox_TextChanged;
            Woth3Items.TextChanged += TextBox_TextChanged;
            Woth4Items.TextChanged += TextBox_TextChanged;
            Woth5Items.TextChanged += TextBox_TextChanged;

            Barren1.TextChanged += Location_TextChanged;
            Barren2.TextChanged += Location_TextChanged;
            Barren3.TextChanged += Location_TextChanged;

            Skulls30.TextChanged += TextBox_TextChanged;
            Skulls40.TextChanged += TextBox_TextChanged;
            Skulls50.TextChanged += TextBox_TextChanged;
            OOTSong.TextChanged += TextBox_TextChanged;
            Frogs2.TextChanged += TextBox_TextChanged;
            SkullMask.TextChanged += TextBox_TextChanged;

            Sometimes1.TextChanged += TextBox_TextChanged;
            Sometimes2.TextChanged += TextBox_TextChanged;
            Sometimes3.TextChanged += TextBox_TextChanged;
            Sometimes4.TextChanged += TextBox_TextChanged;
            Sometimes5.TextChanged += TextBox_TextChanged;
            Sometimes6.TextChanged += TextBox_TextChanged;

            timer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0), DispatcherPriority.Background, Timer_Tick, Dispatcher.CurrentDispatcher)
            {
                IsEnabled = false
            };
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Timer.Text = (DateTime.Now - start).ToString(@"hh\:mm\:ss");
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;

            // Figure out which, if any, images are activated
            if (textBox.Name.Contains("WOTH", StringComparison.OrdinalIgnoreCase) && textBox.Name.Contains("ITEM", StringComparison.OrdinalIgnoreCase))
            {
                var imageName = ItemImageNames.ItemImageMaps.FirstOrDefault(x => x.Key.Equals(textBox.Text)).Value;
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

                var imageName = ItemImageNames.ItemImageMaps.FirstOrDefault(x => x.Key.Equals(itemText)).Value;
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
                var imageName = ItemImageNames.ItemImageMaps.FirstOrDefault(x => x.Key.Equals(textBox.Text)).Value;
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
                var currentSlot = image.Name.Substring(image.Name.Length-1, 1);
                
                var relevantItemSlots = Main.Children.OfType<Image>().Where(x => x.Name.StartsWith($"WOTH{wothNumber}", StringComparison.OrdinalIgnoreCase));
                var firstSlot = relevantItemSlots.Single(x => x.Name.Equals($"WOTH{wothNumber}ITEM1", StringComparison.OrdinalIgnoreCase));
                var secondSlot = relevantItemSlots.Single(x => x.Name.Equals($"WOTH{wothNumber}ITEM2", StringComparison.OrdinalIgnoreCase));
                var thirdSlot = relevantItemSlots.Single(x => x.Name.Equals($"WOTH{wothNumber}ITEM3", StringComparison.OrdinalIgnoreCase));

                switch (currentSlot)
                {
                    case "1":
                        // Check if 2 and 3 are active
                        if(secondSlot.Source != null)
                        {
                            firstSlot.Source = secondSlot.Source;

                            if(thirdSlot.Source != null)
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
                        if(thirdSlot.Source != null)
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
    }
}
