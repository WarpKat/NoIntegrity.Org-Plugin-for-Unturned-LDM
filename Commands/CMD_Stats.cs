using NoIntegrity.Functions;
using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using System.Collections.Generic;
using System;
using Rocket.Core.Utils;

namespace NoIntegrity.Commands
{
    public class CMD_Stats : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "stats";

        public string Help => "This command shows your server stats.";

        public string Syntax => "";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string> { "stats" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;

            string hisSteamID = Convert.ToString(player.CSteamID);

            decimal hisKDR = 0;
            decimal hisHSPct = 0;
            decimal hisDeaths = 0;

            TaskDispatcher.RunAsync(() =>
            {
                string[] hisStatistics = DBHandler.dbFetchStats(hisSteamID);

                TaskDispatcher.QueueOnMainThread(() =>
                {
                    decimal hisKills = Convert.ToDecimal(hisStatistics[0]);
                    decimal hisHeadshots = Convert.ToDecimal(hisStatistics[1]);
                    decimal hisDeaths = Convert.ToDecimal(hisStatistics[2]);

                    // If we don't do these checks, a divide by 0 exception will be thrown and nothing will be displayed.
                    if(hisKills > 0 && hisDeaths > 0)
                    {
                        decimal hisKDR = Math.Round(hisKills / hisDeaths, 2);
                    }

                    // If we don't do these checks, a divide by 0 exception will be thrown and nothing will be displayed.
                    if (hisHeadshots > 0 && hisKills > 0)
                    {
                        decimal hisHSPct = Math.Round(hisHeadshots / hisKills * 100, 2);
                    }

                    UnturnedChat.Say(player, $"Kills/Headshots/Deaths:  {hisKills}/{hisHeadshots}/{hisDeaths}");
                    //UnturnedChat.Say(player, $"Your Total Headshots:  {hisHeadshots}");
                    //UnturnedChat.Say(player, $"Your Total Deaths:  {hisDeaths}");
                    UnturnedChat.Say(player, $"KDR:  {hisKDR}/HSP:  {hisHSPct}%");
                });
            });
        }
    }
}
 