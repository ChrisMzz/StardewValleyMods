using GenericModConfigMenu;
using StardewModdingAPI;

namespace LegendaryFishBait.Compatibility
{
    public class ModInitializer
    {
        private readonly IManifest modManifest;
        private readonly IModHelper helper;

        public ModInitializer(IManifest modManifest, IModHelper helper)
        {
            this.modManifest = modManifest;
            this.helper = helper;
        }

        public void Initialize(IGenericModConfigMenuApi api, ModConfig config)
        {
            api.Register(
                mod: modManifest,
                reset: () =>
                {
                    config = new ModConfig();
                    ModEntry.Config = config;
                },
                save: () => helper.WriteConfig(config)
            );

            api.AddBoolOption(
                mod: modManifest,
                getValue: () => config.Enabled,
                setValue: value => config.Enabled = value,
                name: () => "Enable",
                tooltip: () => "Check this to enable the mod."
            );

            api.AddBoolOption(
                mod: modManifest,
                getValue: () => config.PreventWaste,
                setValue: value => config.PreventWaste = value,
                name: () => "Prevent Waste",
                tooltip: () => "Check this to prevent any non Legendary fish from biting when using Legandary bait."
            );

            api.AddBoolOption(
                mod: modManifest,
                getValue: () => config.EnableRegularFishHints,
                setValue: value => config.EnableRegularFishHints = value,
                name: () => "Enable Regular Fish Hints (beta)",
                tooltip: () => "Check this to notify player when the specific bait can't catch the targeted fish because of season, location, or some other factors."
            );

            
        }
    }
}
