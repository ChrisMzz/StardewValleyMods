using System;
using Microsoft.Xna.Framework;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley.Monsters;
using StardewValley;

namespace SlimeMinerals
{
    /// <summary>The mod entry point.</summary>
    internal sealed class Mod : StardewModdingAPI.Mod
    {
        private static readonly Random rng = new Random();

        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            
            helper.Events.World.NpcListChanged += this.OnSlimeKill;
            
        }



        private void OnSlimeKill(object sender, NpcListChangedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet or if the location change isn't in the current location
            if (!Context.IsWorldReady || !e.IsCurrentLocation || !Context.IsMainPlayer)
                return;

            foreach (GreenSlime slime in e.Removed.OfType<GreenSlime>())
            {
                Color color = slime.color; // RGBA (0-255)
                IEnumerable<Item> loot = new List<Item>();


                switch (color)
                { // placeholder IDs
                    case Color rgb when (63 < rgb.R && rgb.R < 83) && (82 < rgb.G && rgb.G < 91) && (95 < rgb.B && rgb.B < 97)):
                        loot.Append<Item>(new StardewValley.Object(168, 1)); //  OPAL
                        break;

                    case Color rgb when (15 < rgb.R && rgb.R < 18) && (12 < rgb.G && rgb.G < 15) && (36 < rgb.B && rgb.B < 41):
                        loot.Append<Item>(new StardewValley.Object(168, 1)); // fire opal
                        break;

                    case Color rgb when (27 < rgb.R && rgb.R < 27) && (32 < rgb.G && rgb.G < 37) && (29 < rgb.B && rgb.B < 35)):
                        loot.Append<Item>(new StardewValley.Object(168, 1)); // bixiye
                        break;

                    case Color rgb when (83 < rgb.R && rgb.R < 84) && (22 < rgb.G && rgb.G < 40) && (22 < rgb.B && rgb.B < 40)):
                        loot.Append<Item>(new StardewValley.Object(168, 1)); // baryte
                        break;

                    case Color rgb when (80 < rgb.R && rgb.R < 95) && (56 < rgb.G && rgb.G < 91) && (65 < rgb.B && rgb.B < 75)):
                        loot.Append<Item>(new StardewValley.Object(168, 1)); // dolomite
                        break;

                    case Color rgb when (90 < rgb.R && rgb.R < 92) && (77 < rgb.G && rgb.G < 81) && (35 < rgb.B && rgb.B < 55)):
                        loot.Append<Item>(new StardewValley.Object(168, 1)); //  calcite
                        break;

                    case Color rgb when (29 < rgb.R && rgb.R < 38) && (71 < rgb.G && rgb.G < 72) && (83 < rgb.B && rgb.B < 85)):
                        loot.Append<Item>(new StardewValley.Object(168, 1)); //aerite
                        break;


                    case Color rgb when (58 < rgb.R && rgb.R < 69) && (61 < rgb.G && rgb.G < 69) && (69 < rgb.B && rgb.B < 73)):
                        loot.Append<Item>(new StardewValley.Object(168, 1)); //esperite
                        break; 2258



                    case Color rgb when (52 < rgb.R && rgb.R < 64) && (50 < rgb.G && rgb.G < 62) && (80 < rgb.B && rgb.B < 86)):
                        loot.Append<Item>(new StardewValley.Object(168, 1)); //fluopatite
                        break;


                    case Color rgb when (56 < rgb.R && rgb.R < 68) && (79 < rgb.G && rgb.G < 88) && (71 < rgb.B && rgb.B < 73)):
                        loot.Append<Item>(new StardewValley.Object(168, 1)); //geminite
                        break;


                    case Color rgb when (42 < rgb.R && rgb.R < 45) && (7 < rgb.G && rgb.G < 15) && (4 < rgb.B && rgb.B < 12)):
                        loot.Append<Item>(new StardewValley.Object(168, 1)); //helvite
                        break;


                    case Color rgb when (55 < rgb.R && rgb.R < 58) && (79 < rgb.G && rgb.G < 92) && (27 < rgb.B && rgb.B < 42)):
                        loot.Append<Item>(new StardewValley.Object(168, 1)); //jambonite
                        break;


                    case Color rgb when (71 < rgb.R && rgb.R < 72) && (71 < rgb.G && rgb.G < 71) && (5 < rgb.B && rgb.B < 29)):
                        loot.Append<Item>(new StardewValley.Object(168, 1)); //jzgoite
                        break;


                    case Color rgb when (77 < rgb.R && rgb.R < 88) && (91 < rgb.G && rgb.G < 92) && (80 < rgb.B && rgb.B < 88)):
                        loot.Append<Item>(new StardewValley.Object(168, 1)); //limestone
                        break;


                    case Color rgb when (70 < rgb.R && rgb.R < 70) && (70 < rgb.G && rgb.G < 77) && (70 < rgb.B && rgb.B < 71)):
                        loot.Append<Item>(new StardewValley.Object(168, 1)); //soapstone
                        break;


                    case Color rgb when (20 < rgb.R && rgb.R < 37) && (7 < rgb.G && rgb.G < 18) && (0 < rgb.B && rgb.B < 7)):
                        loot.Append<Item>(new StardewValley.Object(168, 1)); //mudstone
                        break;


                    case Color rgb when ((10 < rgb.R && rgb.R < 13) && (2 < rgb.G && rgb.G < 5) && (31 < rgb.B && rgb.B < 34)):
                        loot.Append<Item>(new StardewValley.Object(168, 1)); //obsidian
                        break;


                    case Color rgb when (42 < rgb.R && rgb.R < 52) && (75 < rgb.G && rgb.G < 89) && (63 < rgb.B && rgb.B < 75)):
                        loot.Append<Item>(new StardewValley.Object(168, 1)); //slate
                        break;



                    case Color rgb when (19 < rgb.R && rgb.R < 28) && (14 < rgb.G && rgb.G < 24) && (64 < rgb.B && rgb.B < 65)):
                        loot.Append<Item>(new StardewValley.Object(168, 1)); //fairy stone
                        break;


                    case Color rgb when (77 < rgb.R && rgb.R < 89) && (5 < rgb.G && rgb.G < 8) && (76 < rgb.B && rgb.B < 85)):
                        loot.Append<Item>(new StardewValley.Object(168, 1)); //sandshard 
                        break;


                    case Color rgb when (44 < rgb.R && rgb.R < 49) && (75 < rgb.G && rgb.G < 82) && (54 < rgb.B && rgb.B < 60)):
                        loot.Append<Item>(new StardewValley.Object(168, 1)); //alamite
                        break;

                    default:
                        break;
                }

                Point center = slime.GetBoundingBox().Center;
                Vector2 dropPos = new Vector2(center.X, center.Y);
                foreach (Item mineral in loot)
                {
                    if ( rng.Next(100) > 8 ) { continue; }
                    Game1.createItemDebris(mineral, dropPos, rng.Next(4), slime.currentLocation);
                }

            }


        }




    }
}
