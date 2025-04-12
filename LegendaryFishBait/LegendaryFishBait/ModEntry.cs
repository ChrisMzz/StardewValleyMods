using System;
using Microsoft.Xna.Framework;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using GenericModConfigMenu;
using LegendaryFishBait.Compatibility;
using StardewValley.Tools;
using StardewValley.GameData.Locations;
using xTile.Dimensions;
using StardewValley.Extensions;
using StardewValley.GameData;
using StardewValley.Internal;

namespace LegendaryFishBait
{
    /// <summary>The mod entry point.</summary>
    internal sealed class ModEntry : Mod
    {
        private static readonly Random rng = new Random();

        private float timeElapsed;

        private string baitType;

        public static ModConfig Config { get; set; }

        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            //helper.Events.GameLoop.DayStarted += this.RandomizeGeneratedSlimes;
            helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;
            helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
            Config = helper.ReadConfig<ModConfig>();
            this.timeElapsed = 60f;
            this.baitType = "";
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

        /*
        Dictionary<string, LocationData> dictionary = DataLoader.Locations(Game1.content);
        Season seasonForLocation = Game1.GetSeasonForLocation(location);
        if (location == null || !location.TryGetFishAreaForTile(bobberTile, out var id, out var _))
        {
            id = null;
        }
        string text = null;
        if (player?.CurrentTool is FishingRod fishingRod)
        {
            Object bait = fishingRod.GetBait();
            if (bait.preservedParentSheetIndex.Value == null) {
                return false;
            }
            text = "(O)" + bait.preservedParentSheetIndex.Value;
        }
        

         
         */



        private void OnUpdateTicked(object sender, UpdateTickedEventArgs e)
        {
            if (!Context.IsWorldReady)
                return;
            Farmer player = Game1.player;
            Dictionary<string, LocationData> dictionary = DataLoader.Locations(Game1.content);
            LocationData locationData = player.currentLocation.GetData();
            Dictionary<string, string> allFishData = DataLoader.Fish(Game1.content);
            Season seasonForLocation = Game1.GetSeasonForLocation(player.currentLocation);
            bool conditions = false;
            Microsoft.Xna.Framework.Point tilePoint = player.TilePoint;
            string id = null;
            IEnumerable<SpawnFishData> enumerable = dictionary["Default"].Fish;
            if (locationData != null && locationData.Fish?.Count > 0)
                enumerable = enumerable.Concat(locationData.Fish);
            if (!Config.Enabled)
            {
                // foreach (SpawnFishData spawn in enumerable) { spawn.Chance = 1f; }
                return;
            }
                 // && GameStateQuery.CheckConditions(spawn.Condition, player.currentLocation, null, null, null, null, null) && !(spawn.Season.HasValue && spawn.Season != seasonForLocation) && !(spawn.FishAreaId != null && id != spawn.FishAreaId) && !(spawn.PlayerPosition.HasValue && !spawn.PlayerPosition.GetValueOrDefault().Contains(tilePoint.X, tilePoint.Y)) && !(spawn.BobberPosition.HasValue && !spawn.BobberPosition.GetValueOrDefault().Contains((int)fishingRod.bobber.X, (int)fishingRod.bobber.Y)) && CheckGenericFishRequirements(Item(spawn.Id), allFishData, location, player, spawn, 1, false, false, true, false)
            if (player?.CurrentTool is FishingRod fishingRod)
            {
                StardewValley.Object bait = fishingRod.GetBait();
                conditions = true;
                if (bait == null)
                    foreach (SpawnFishData spawn in enumerable) { spawn.Chance = 1f; }
                else if (bait.preservedParentSheetIndex.Value != null && bait.preservedParentSheetIndex.Value == "159" || bait.preservedParentSheetIndex.Value == "163" || bait.preservedParentSheetIndex.Value == "682" || bait.preservedParentSheetIndex.Value == "775" || bait.preservedParentSheetIndex.Value == "160")
                {
                    if (this.baitType != bait.preservedParentSheetIndex.Value)
                        this.timeElapsed = 60f;
                    this.baitType = bait.preservedParentSheetIndex.Value;
                    conditions = false;
                    if (fishingRod.isFishing)
                    {
                        player.currentLocation.TryGetFishAreaForTile(fishingRod.bobber.Value, out var newId, out var _);
                        id = newId;
                    }
                    foreach (SpawnFishData spawn in enumerable)
                    { // can't generalise checking rain because of source code construction
                        // line below checks if the bait can help find the fish
                        //if (spawn.Id == ("(O)" + bait.preservedParentSheetIndex.Value)) { Game1.addHUDMessage(new HUDMessage("true")); }
                        if (spawn.IsBossFish && GameStateQuery.CheckConditions(spawn.Condition, player.currentLocation, null, null, null, null, null) && !(spawn.Season.HasValue && spawn.Season != seasonForLocation) && !(spawn.FishAreaId != null && id != spawn.FishAreaId) && !(spawn.PlayerPosition.HasValue && !spawn.PlayerPosition.GetValueOrDefault().Contains(tilePoint.X, tilePoint.Y)) && !(spawn.BobberPosition.HasValue && !spawn.BobberPosition.GetValueOrDefault().Contains((int)fishingRod.bobber.X, (int)fishingRod.bobber.Y)))
                        {
                            // spawn.Id == bait.preservedParentSheetIndex.Value

                            //Game1.addHUDMessage(new HUDMessage("a legendary is here"));
                            
                            if (spawn.Id == ("(O)" + bait.preservedParentSheetIndex.Value) && (spawn.Id != "(O)163" || player.currentLocation.IsRainingHere()))
                            {
                                spawn.CatchLimit = -1;
                                spawn.Chance = 1f;
                                conditions = true;
                            } else { spawn.CatchLimit = 1; }
                        }
                        else if (Config.PreventWaste)
                        {
                            spawn.Chance = 0f;
                        }
                        //Game1.addHUDMessage(new HUDMessage(conditions.ToString()+" "+ player.UsingTool.ToString()+" "+ timeElapsed.ToString()));
                    }
                }
                else
                {
                    if (!Config.EnableRegularFishHints)
                        return;
                    conditions = false;
                    foreach (SpawnFishData spawn in enumerable)
                    {
                        spawn.Chance = 1f;
                        if (spawn.IsBossFish) 
                            spawn.CatchLimit = 1;
                        if (bait.preservedParentSheetIndex.Value != null)
                        {
                            if (this.baitType != bait.preservedParentSheetIndex.Value)
                                this.timeElapsed = 60f;
                            this.baitType = bait.preservedParentSheetIndex.Value;
                            if (spawn.Id == ("(O)" + bait.preservedParentSheetIndex.Value))
                                conditions = true;
                        } else { conditions = true; }
                        
                    }
                }
                if (fishingRod.isFishing && !conditions && this.timeElapsed >= 60f)
                {
                    
                    Game1.addHUDMessage(new HUDMessage("It seems the waters do not contain what you seek...", 2));
                    this.timeElapsed = 0f;
                }

                //Game1.addHUDMessage(new HUDMessage(this.timeElapsed.ToString()));
                this.timeElapsed += 0.05f;
            }
        }
        // still get an error somewhere



    }
}