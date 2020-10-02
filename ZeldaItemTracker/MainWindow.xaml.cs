using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

            OOTItem.TextChanged += OOTItem_TextChanged;
            Biggoron.TextChanged += Biggoron_TextChanged;
            Skulls30.TextChanged += Skulls30_TextChanged;
            Skulls40.TextChanged += Skulls40_TextChanged;
            Skulls50.TextChanged += Skulls50_TextChanged;
            OOTSong.TextChanged += OOTSong_TextChanged;
            Frogs2.TextChanged += Frogs2_TextChanged;

            WothItems.TextChanged += WothItems_TextChanged;
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

        private void OOTItem_TextChanged(object sender, TextChangedEventArgs e)
        {
            OOTItem.Text = NotesManager.GetFullName(OOTItem.Text);
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

       
    }
}
