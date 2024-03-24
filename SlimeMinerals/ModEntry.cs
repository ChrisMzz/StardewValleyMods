using System;
using Microsoft.Xna.Framework;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley.Monsters;
using StardewValley;
using GenericModConfigMenu;
using SlimeMinerals.Compatibility;

namespace SlimeMinerals
{
    /// <summary>The mod entry point.</summary>
    internal sealed class ModEntry : StardewModdingAPI.Mod
    {
        private static readonly Random rng = new Random();

        public static ModConfig Config { get; set; }

        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {

            helper.Events.World.NpcListChanged += this.OnSlimeKill;
            helper.Events.GameLoop.GameLaunched += OnGameLaunched;
            Config = helper.ReadConfig<ModConfig>();

        }

        private void OnGameLaunched(object sender, GameLaunchedEventArgs e)
        {
            // Initialize mod(s)
            ModInitializer modInitializer = new ModInitializer(ModManifest, Helper);
            // Get Generic Mod Config Menu API (if it's installed) thanks to ConvenientInventory as a template for this
            var api = Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (api != null)
            {
                modInitializer.Initialize(api, Config);
            }
        }



        private void OnSlimeKill(object sender, NpcListChangedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet or if the location change isn't in the current location
            if (!Context.IsWorldReady || !e.IsCurrentLocation || !Context.IsMainPlayer)
                return;

            
            //Game1.addHUDMessage(new HUDMessage(Game1.player.currentLocation.Name, 3));
            // ignore if player is in a location toggled off from Generic Config
            if (!Config.Anywhere) // works fine
            {
                if (Game1.player.currentLocation.Name != "Slime Hutch" && Game1.player.currentLocation.Name != "Woods") { return; }
                if (Game1.player.currentLocation.Name == "Slime Hutch" && !Config.InSlimeHutch) { return; }
                if (Game1.player.currentLocation.Name == "Woods" && !Config.InWoods) { return; }
            }

            foreach (GreenSlime slime in e.Removed.OfType<GreenSlime>())
            {
                Netcode.NetColor color = slime.color; // RGBA (0-255)
                List<Item> loot = new List<Item>();

                //Game1.addHUDMessage(new HUDMessage(color.ToString(), 3));
                // the line of code above runs fine

                switch (color)
                { 
                    // tiger slimes seem to have the color (255,255,255,255)
                    case Netcode.NetColor rgb when (0 < rgb.R && rgb.R < 1) && (0 < rgb.G && rgb.G < 1) && (0 < rgb.B && rgb.B < 1):
                        loot.Add(new StardewValley.Object("168", 1)); // trash TEST -- never runs, change 1s to 255s to make always it run
                        break;

                    case Netcode.NetColor rgb when (63 < rgb.R && rgb.R < 83) && (82 < rgb.G && rgb.G < 91) && (80 < rgb.B && rgb.B < 97):
                        loot.Add(new StardewValley.Object("564", 1)); //  opal
                        break;

                    case Netcode.NetColor rgb when (15 < rgb.R && rgb.R < 28) && (12 < rgb.G && rgb.G < 25) && (36 < rgb.B && rgb.B < 46):
                        loot.Add(new StardewValley.Object("565", 1)); // fire opal
                        break;

                    case Netcode.NetColor rgb when (27 < rgb.R && rgb.R < 37) && (32 < rgb.G && rgb.G < 50) && (29 < rgb.B && rgb.B < 45):
                        loot.Add(new StardewValley.Object("539", 1)); // bixite
                        break;

                    case Netcode.NetColor rgb when (73 < rgb.R && rgb.R < 84) && (22 < rgb.G && rgb.G < 40) && (22 < rgb.B && rgb.B < 40):
                        loot.Add(new StardewValley.Object("540", 1)); // baryte
                        break;

                    case Netcode.NetColor rgb when (80 < rgb.R && rgb.R < 95) && (56 < rgb.G && rgb.G < 91) && (65 < rgb.B && rgb.B < 75):
                        loot.Add(new StardewValley.Object("543", 1)); // dolomite
                        break;

                    case Netcode.NetColor rgb when (90 < rgb.R && rgb.R < 102) && (77 < rgb.G && rgb.G < 91) && (25 < rgb.B && rgb.B < 55):
                        loot.Add(new StardewValley.Object("542", 1)); //  calcite
                        break;

                    case Netcode.NetColor rgb when (29 < rgb.R && rgb.R < 48) && (61 < rgb.G && rgb.G < 72) && (73 < rgb.B && rgb.B < 85):
                        loot.Add(new StardewValley.Object("541", 1)); //aerinite
                        break;


                    case Netcode.NetColor rgb when (58 < rgb.R && rgb.R < 69) && (61 < rgb.G && rgb.G < 69) && (69 < rgb.B && rgb.B < 83):
                        loot.Add(new StardewValley.Object("544", 1)); //esperite
                        break;



                    case Netcode.NetColor rgb when (52 < rgb.R && rgb.R < 64) && (50 < rgb.G && rgb.G < 62) && (70 < rgb.B && rgb.B < 86):
                        loot.Add(new StardewValley.Object("545", 1)); //fluoropatite
                        break;


                    case Netcode.NetColor rgb when (56 < rgb.R && rgb.R < 68) && (79 < rgb.G && rgb.G < 88) && (41 < rgb.B && rgb.B < 73):
                        loot.Add(new StardewValley.Object("546", 1)); //geminite
                        break;


                    case Netcode.NetColor rgb when (22 < rgb.R && rgb.R < 45) && (7 < rgb.G && rgb.G < 35) && (4 < rgb.B && rgb.B < 22):
                        loot.Add(new StardewValley.Object("547", 1)); //helvite
                        break;


                    case Netcode.NetColor rgb when (55 < rgb.R && rgb.R < 68) && (79 < rgb.G && rgb.G < 92) && (27 < rgb.B && rgb.B < 42):
                        loot.Add(new StardewValley.Object("548", 1)); //jamborite
                        break;


                    case Netcode.NetColor rgb when (71 < rgb.R && rgb.R < 82) && (71 < rgb.G && rgb.G < 95) && (5 < rgb.B && rgb.B < 29):
                        loot.Add(new StardewValley.Object("549", 1)); //jagoite
                        break;


                    case Netcode.NetColor rgb when (77 < rgb.R && rgb.R < 88) && (81 < rgb.G && rgb.G < 92) && (80 < rgb.B && rgb.B < 88):
                        loot.Add(new StardewValley.Object("571", 1)); //limestone
                        break;


                    case Netcode.NetColor rgb when (70 < rgb.R && rgb.R < 90) && (70 < rgb.G && rgb.G < 77) && (60 < rgb.B && rgb.B < 71):
                        loot.Add(new StardewValley.Object("572", 1)); //soapstone
                        break;


                    case Netcode.NetColor rgb when (20 < rgb.R && rgb.R < 37) && (7 < rgb.G && rgb.G < 18) && (0 < rgb.B && rgb.B < 7):
                        loot.Add(new StardewValley.Object("572", 1)); //mudstone
                        break;


                    case Netcode.NetColor rgb when (10 < rgb.R && rgb.R < 63) && (2 < rgb.G && rgb.G < 15) && (21 < rgb.B && rgb.B < 34):
                        loot.Add(new StardewValley.Object("575", 1)); //obsidian
                        break;


                    case Netcode.NetColor rgb when (42 < rgb.R && rgb.R < 52) && (75 < rgb.G && rgb.G < 89) && (63 < rgb.B && rgb.B < 75):
                        loot.Add(new StardewValley.Object("576", 1)); //slate
                        break;



                    case Netcode.NetColor rgb when (19 < rgb.R && rgb.R < 28) && (14 < rgb.G && rgb.G < 24) && (45 < rgb.B && rgb.B < 65):
                        loot.Add(new StardewValley.Object("577", 1)); //fairy stone
                        break;


                    case Netcode.NetColor rgb when (77 < rgb.R && rgb.R < 89) && (5 < rgb.G && rgb.G < 38) && (76 < rgb.B && rgb.B < 85):
                        loot.Add(new StardewValley.Object("578", 1)); //star shard 
                        break;


                    case Netcode.NetColor rgb when (34 < rgb.R && rgb.R < 49) && (75 < rgb.G && rgb.G < 82) && (34 < rgb.B && rgb.B < 60):
                        loot.Add(new StardewValley.Object("538", 1)); //alamite
                        break;

                    default:
                        break;
                }

                Point center = slime.GetBoundingBox().Center;
                Vector2 dropPos = new Vector2(center.X, center.Y);
                loot.ForEach(mineral => { if (rng.Next(100) <= 8) { Game1.createItemDebris(mineral, dropPos, rng.Next(4), slime.currentLocation); }});

            }


        }




    }
}