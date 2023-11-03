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
                {
                    case Color rgb when (rgb.R > 0):
                        loot.Append<Item>(new StardewValley.Object(168, 1)); // currently adds trash
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
