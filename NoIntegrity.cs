using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Events;
using NoIntegrity.Functions;
using SDG.Unturned;

/*
 *  NoIntegrity.Org Rocket Plugin for Creative Unturned Servers
 *  Written by `WarpKat <warpkat@nointegrity.org>
 *  Use of this plugin is at your own risk.
 *  Compliments to all of the Unturned Rocket developers that came before me.
 *  It's because of you that I have forced myself to learn C#.
 *  
 *  (I don't very much like C#.)
 *  
 *  Developed on Microsoft Visual Studio 2022
*/

namespace NoIntegrity
{
    public class NoIntegrity : RocketPlugin<NoIntegrityConfiguration>
    {
        public static NoIntegrity Instance { get; private set; }
        
        protected override void Load()
        {
            Instance = this;

            NoIntegrityConfiguration config = NoIntegrity.Instance.Configuration.Instance;

            bool isMaxSkillsAuto = config.maxSkillsAuto;

            DBHandler.dbCheckClothingSchema();
            DBHandler.dbCheckInventorySchema();
            DBHandler.dbCheckBlacklistSchema();

            // Register Maximized Skills Events
            if(isMaxSkillsAuto == true){
                U.Events.OnPlayerConnected += Skills.OnPlayerConnected_MaximizeSkills;
                UnturnedPlayerEvents.OnPlayerRevive += Skills.OnPlayerRevive_MaximizeSkills;
            }

            // Register blacklist checks.
            UnturnedPlayerEvents.OnPlayerWear += ItemBlacklist.OnPlayerWear;
            ItemManager.onTakeItemRequested += ItemBlacklist.OnTakeItemRequested;
            UnturnedPlayerEvents.OnPlayerInventoryAdded += ItemBlacklist.OnPlayerInventoryAdded;

            Logger.Log("NoIntegrity.Org Rocket Plugin for Creative Unturned Servers");
            Logger.Log("-- Developed by `WarpKat <warpkat@nointegrity.org>");
            Logger.Log("-- Website:  https://www.nointegrity.org/");
            Logger.Log("-- Discord:  https://discordapp.com/invite/AZMwbmj");
            Logger.Log("-- Use of this plugin is at your own risk.");
            Logger.Log($"-- {Name} {Assembly.GetName().Version} has been loaded.");
        }

        protected override void Unload()
        {
            NoIntegrityConfiguration config = NoIntegrity.Instance.Configuration.Instance;

            bool isMaxSkillsAuto = config.maxSkillsAuto;

            // De-Register Maximized Skills Events
            if (isMaxSkillsAuto == true)
            {
                U.Events.OnPlayerConnected -= Skills.OnPlayerConnected_MaximizeSkills;
            }

            // De-Register blacklist checks.
            UnturnedPlayerEvents.OnPlayerWear -= ItemBlacklist.OnPlayerWear;
            ItemManager.onTakeItemRequested -= ItemBlacklist.OnTakeItemRequested;
            UnturnedPlayerEvents.OnPlayerInventoryAdded -= ItemBlacklist.OnPlayerInventoryAdded;

            Logger.Log(Configuration.Instance.LoadMessage);
            Logger.Log($"-- {Name} {Assembly.GetName().Version} has been unloaded.");
        }
    }
}
