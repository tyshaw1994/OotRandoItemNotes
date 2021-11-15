# Note that this tracker was built for personal use but was made open source just to share, and I will likely not implement feedback or fix bugs in any timely fashion

# Intro
![Tracker](https://imgur.com/MgYINn8.png)

This is a tracker for Ocarina of Time Randomizer. The goal was to be able to keep track of items and hints on one tracker, while also making it look presentable on stream.

Typically when taking notes during a seed you will want to write out abbreviations so that you aren't spending too much time dealing with notetaking. This makes your notes messy
and as such most people don't usually show their notes on stream. I wanted to be able to have all the information that I have available to a stream viewer, as well as control over my own tracker,
so I created this.

# Running
I am currently not providing pre-built releases or help with running the tracker. It was built using .NET Core 3.1 so you should probably just need the sdk to be able to clone the repo, build it from source, and run it yourself.

# Credits
- Images were exported from Emotracker package "OoT Rando Item Trackers" by Raikaru and atz
  - https://emotracker.net/
- Boss images were taken taken from ArthurOudini's Discord server

# Features
- Item and Song tracking similar to Emotracker (Left click on an item to highlight it, right click on it to remove the highlight. Left click for Iron Boots, right click for Hover Boots)
- Text-based dungeon entry (found at the bottom of the tracker, see tables below for shortcodes for dungeons)
- Location and Item "autofill" based on shorthand abbreviations (e.g. typing "kak" into a Barren box will replace "kak" with "Kakariko Village"; typing "hs" into an item box will replace "hs" with an image of the Hookshot)
- A timer

# Usage
## Item Tracking
- Left click on an item to highlight it
- Right click on an item to un-highlight it
- Progressive items (Bombs, Hookshot, Strength, etc.) can be left clicked multiple times to show the current state
- Compound items (Boots, Elemental Arrows) have the left item highlighted/un-highlighted via left click, and the right item via right click

## Song Tracking
- Left click on a song to highlight it
- Right click on a song to mark it as checked

## Dungeon Reward Tracking
- Enter a comma-separated list of dungeon shortcodes and press Enter to have the dungeon rewards at the top of the tracker labled
  - For example, entering fr,fo,fi,wa,sp,sh will look like this: 
  - ![DungeonRewards](https://i.imgur.com/ZgwLtUU.png)
- Left click on a dungeon reward to highlight it
- Right click on a dungeon reward to un-highlight it

## Hints
- Location-based hint boxes (WOTH, Goal, Barren) support shortcodes for in-game locations (e.g. gtg to Gerudo Training Ground)
- Item-based hint boxes (30 Skulls, OOT Song, etc.) support shortcodes for item images (e.g. hs to an image of Hookshot, ks to an image of Kokiri Sword)
- Sometimes hint boxes don't replace any locations or checks, but they will replace item shortcodes with item images
- Goal hint boxes will display dungeon rewards or boss images above them if you enter a dungeon reward or boss shortcode
  - To do this, I usually type the location first and then the reward. For example if Kakariko leads to the Forest Medallion, I will type "kakfom" which will fill in the box like so:
  - ![GoalExample](https://i.imgur.com/VyInFl2.png)
- Double click on any image auto-added to the tracker to remove it

## Timer
- Start and stop timer buttons, currently no pause functionality because I've never needed it in a race but I might add it at some point. Pressing start a second time will reset the timer

# Shortcodes
## Bosses (only used for Goal hint images)
| Boss     | Shortcode   |
| ----------- | ----------- |
| Gohma   | gma |
| King Dodongo | kd |
| Barinade | bari |
| Phantom Ganon | pg |
| Volvagia | vag |
| Morpha | mor |
| Bongo Bongo | bongo |
| Twinrova | rova |

## Dungeons (can also be used as Locations where applicable)
| Dungeon     | Shortcode   |
| ----------- | ----------- |
| Deku Tree   | dt          |
| Dodongo's Cavern | dc |
| Jabu Jabu's Belly | jb |
| Forest Temple | fo |
| Fire Temple | fi |
| Water Temple | wa |
| Shadow Temple | sh |
| Spirit Temple | sp |

## Dungeon Rewards
| Reward     | Shortcode   |
| ----------- | ----------- |
| Light Medallion   | lm |
| Forest Medallion | fom |
| Fire Medallion | fim |
| Water Medallion | wm |
| Shadow Medallion | shm |
| Spirit Medallion | spm |
| Kokiri Emerald | ke |
| Goron Ruby | gr |
| Zora Sapphire | zs |

## Locations
| Location | Shortcode |
| ----------- | ----------- |
| Kokiri Forest | kf |
| Lost Woods | lw |
| Sacred Forest Meadow | sfm |
| Zora's River | zr |
| Zora's Domain | zd |
| Zora's Fountain | zf |
| Hyrule Field | hf |
| Lake Hylia | lh |
| Gerudo Valley | gv |
| Gerudo Fortress | gf |
| Haunted Wasteland | hw |
| Desert Colossus | colo |
| Market | mkt |
| Temple of Time | tot |
| Hyrule Castle | hc |
| Outside Ganon's Castle | ogc |
| Lon Lon Ranch | llr |
| Kakariko Village | kak |
| Graveyard | gy |
| Death Mountain Trail | dmt |
| Death Mountain Crater | dmc |
| Goron City | gc |
| Ganon's Castle | ganon |

## Items
| Item | Shortcode |
| ----------- | ----------- |
| Kokiri Sword | ks |
| Bomb Bag | bb |
| Hookshot | hs |
| Longshot | ls |
| Goron Bracelet | str1 |
| Silver Gauntlets | str2 |
| Gold Gauntlets | str3 |
| Bow | bow |
| Fire Arrows | fa |
| Light Arrows | la |
| Hammer | ham |
| Iron Boots | ib |
| Hover Boots | hb |
| Mirror Shield | ms |
| Din's Fire | df |
| Magic Meter | mag |
| Slingshot | sling |
| Boomerang | rang |
| Bottle | btl |
| Ruto's Letter | rl |
| Silver Scale | ss |
| Gold Scale | gs |
| Lens of Truth | lot |
| Small Key | sk |
| Boss Key | bk |
