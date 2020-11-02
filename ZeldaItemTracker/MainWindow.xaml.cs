using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ZeldaItemTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal List<LocationNotes> _locationNotes;

        public MainWindow()
        {
            InitializeComponent();

            _locationNotes = NotesManager.BaseLocationNotes;

            foreach (var locationNote in _locationNotes)
            {
                if (locationNote != _locationNotes.Last())
                {
                    if (locationNote.LocationName.EndsWith(' '))
                    {
                        WothItems.Text += $"{locationNote.LocationName}items: {locationNote.Items}\n\n\n";
                    }
                    else
                    {
                        WothItems.Text += $"{locationNote.LocationName} items: {locationNote.Items}\n\n\n";
                    }
                }
                else
                {
                    if (locationNote.LocationName.EndsWith(' '))
                    {
                        WothItems.Text += $"{locationNote.LocationName}items: {locationNote.Items}";
                    }
                    else
                    {
                        WothItems.Text += $"{locationNote.LocationName} items: {locationNote.Items}";
                    }
                }
            }

            Woth1.TextChanged += Woth1_TextChanged;
            Woth2.TextChanged += Woth2_TextChanged;
            Woth3.TextChanged += Woth3_TextChanged;
            Woth4.TextChanged += Woth4_TextChanged;

            Biggoron.TextChanged += Biggoron_TextChanged;
            Skulls30.TextChanged += Skulls30_TextChanged;
            Skulls40.TextChanged += Skulls40_TextChanged;
            Skulls50.TextChanged += Skulls50_TextChanged;
            OOTSong.TextChanged += OOTSong_TextChanged;
            Frogs2.TextChanged += Frogs2_TextChanged;

            WothItems.TextChanged += WothItems_TextChanged;
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

        private void WothItems_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(WothItems.Text))
            {
                return;
            }

            var wothLocations = WothItems.Text.Split("\n\n\n");

            for (int i = 0; i < _locationNotes.Count; i++)
            {
                _locationNotes[i] = new LocationNotes(wothLocations[i]);
            }
        }

        private void Card_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Woth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ReprintNotes();
            }
        }

        private void Woth1_TextChanged(object sender, TextChangedEventArgs e)
        {
            var fullName = NotesManager.GetFullName(Woth1.Text);

            _locationNotes[0].LocationName = fullName;

            if (fullName != Woth1.Text)
            {
                ReprintNotes();
            }

            Woth1.Text = fullName;
        }

        private void Woth2_TextChanged(object sender, TextChangedEventArgs e)
        {
            var fullName = NotesManager.GetFullName(Woth2.Text);

            _locationNotes[1].LocationName = fullName;

            if (fullName != Woth2.Text)
            {
                ReprintNotes();
            }

            Woth2.Text = fullName;
        }

        private void Woth3_TextChanged(object sender, TextChangedEventArgs e)
        {
            var fullName = NotesManager.GetFullName(Woth3.Text);

            _locationNotes[2].LocationName = fullName;

            if (fullName != Woth3.Text)
            {
                ReprintNotes();
            }

            Woth3.Text = fullName;
        }

        private void Woth4_TextChanged(object sender, TextChangedEventArgs e)
        {
            var fullName = NotesManager.GetFullName(Woth4.Text);

            _locationNotes[3].LocationName = fullName;

            if (fullName != Woth4.Text)
            {
                ReprintNotes();
            }

            Woth4.Text = fullName;
        }

        private void Frogs2_TextChanged(object sender, TextChangedEventArgs e)
        {
            Frogs2.Text = NotesManager.GetFullName(Frogs2.Text);
        }

        private void OOTSong_TextChanged(object sender, TextChangedEventArgs e)
        {
            OOTSong.Text = NotesManager.GetFullName(OOTSong.Text);
        }

        private void Skulls50_TextChanged(object sender, TextChangedEventArgs e)
        {
            Skulls50.Text = NotesManager.GetFullName(Skulls50.Text);
        }

        private void Skulls40_TextChanged(object sender, TextChangedEventArgs e)
        {
            Skulls40.Text = NotesManager.GetFullName(Skulls40.Text);
        }

        private void Skulls30_TextChanged(object sender, TextChangedEventArgs e)
        {
            Skulls30.Text = NotesManager.GetFullName(Skulls30.Text);
        }

        private void Biggoron_TextChanged(object sender, TextChangedEventArgs e)
        {
            Biggoron.Text = NotesManager.GetFullName(Biggoron.Text);
        }

        private void ReprintNotes()
        {
            WothItems.TextChanged -= WothItems_TextChanged;

            WothItems.Text = "";

            foreach (var locationNote in _locationNotes)
            {
                if (locationNote != _locationNotes.Last())
                {
                    if (locationNote.LocationName.EndsWith(' '))
                    {
                        WothItems.Text += $"{locationNote.LocationName}items: {locationNote.Items}\n\n\n";
                    }
                    else
                    {
                        WothItems.Text += $"{locationNote.LocationName} items: {locationNote.Items}\n\n\n";
                    }
                }
                else
                {
                    if (locationNote.LocationName.EndsWith(' '))
                    {
                        WothItems.Text += $"{locationNote.LocationName}items: {locationNote.Items}";
                    }
                    else
                    {
                        WothItems.Text += $"{locationNote.LocationName} items: {locationNote.Items}";
                    }
                }
            }

            WothItems.TextChanged += WothItems_TextChanged;
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

                var stoneLabels = Main.Children.OfType<Image>().Where(x => x.Name.StartsWith("stone") && x.Name.Contains("label")).ToList();
                var stones = Main.Children.OfType<Image>().Where(x => x.Name.StartsWith("stone") && !x.Name.Contains("label")).ToList();

                index = 0;
                foreach (var uselessReward in dungeonRewards.TakeLast(3))
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
        }
    }
}
