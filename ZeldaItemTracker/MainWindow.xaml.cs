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
        private List<LocationNotes> _locationNotes;

        public MainWindow()
        {
            InitializeComponent();

            _locationNotes = new List<LocationNotes>
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
                },
                new LocationNotes
                {
                    LocationName = "WOTH 5",
                    Items = ""
                }
            };

            foreach (var locationNote in _locationNotes)
            {
                if(locationNote != _locationNotes.Last())
                {
                    if(locationNote.LocationName.EndsWith(' '))
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
            Woth5.TextChanged += Woth5_TextChanged;
            WothItems.TextChanged += WothItems_TextChanged;
        }

        private void WothItems_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(string.IsNullOrEmpty(WothItems.Text))
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
            _locationNotes[0].LocationName = Woth1.Text;
        }

        private void Woth2_TextChanged(object sender, TextChangedEventArgs e)
        {
            _locationNotes[1].LocationName = Woth2.Text;
        }

        private void Woth3_TextChanged(object sender, TextChangedEventArgs e)
        {
            _locationNotes[2].LocationName = Woth3.Text;
        }

        private void Woth4_TextChanged(object sender, TextChangedEventArgs e)
        {
            _locationNotes[3].LocationName = Woth4.Text;
        }

        private void Woth5_TextChanged(object sender, TextChangedEventArgs e)
        {
            _locationNotes[4].LocationName = Woth5.Text;
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
