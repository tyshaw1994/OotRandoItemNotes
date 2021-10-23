using FluentAssertions;
using Xunit;

namespace ZeldaItemTracker.Tests
{
    public class NotesManagerTests
    {
        [Fact]
        public void BaseLocationNotes_ProvidesCorrectList()
        {
            var locationNotes = NotesManager.BaseLocationNotes;

            for (int i = 1; i <= 5; i++)
            {
                locationNotes[i - 1].LocationName.Should().Be($"WOTH {i}");
                locationNotes[i - 1].Items.Should().BeEmpty();
            }
        }

        [Fact]
        public void GetFullName_Location_AbbreviationInNameMap_ReturnsFullName()
        {
            foreach (var location in Locations.LocationNameMaps)
            {
                var result = NotesManager.GetFullName(location.Key);

                result.Should().Be(location.Value);
            }
        }

        [Fact]
        public void GetFullName_Item_AbbreviationInNameMap_ReturnsFullName()
        {
            foreach (var item in Items.ItemNameMaps)
            {
                var result = NotesManager.GetFullName(item.Key);

                result.Should().Be(item.Value);
            }
        }

        [Fact]
        public void GetFullName_Song_AbbreviationInNameMap_ReturnsFullName()
        {
            foreach (var song in Songs.SongNameMaps)
            {
                var result = NotesManager.GetFullName(song.Key);

                result.Should().Be(song.Value);
            }
        }
    }
}
