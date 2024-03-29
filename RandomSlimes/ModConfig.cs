using StardewModdingAPI;

namespace RandomSlimes
{
    public class ModConfig
    {
        public bool InWoods { get; set; } = false;
        public bool InSlimeArea { get; set; } = true;
        public bool InQuarryArea { get; set; } = true;
        public bool InSkullCaverns { get; set; } = true;
        public bool InRegularMines { get; set; } = true;

        public bool Anywhere { get; set; } = false;


    }
}