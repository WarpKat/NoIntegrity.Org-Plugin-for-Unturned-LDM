using NoIntegrity.Functions;
using Rocket.API;
using Rocket.Core.Utils;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace NoIntegrity.Commands
{
    public class CMD_BuildInfo : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "buildinfo";

        public string Help => "This command shows the NoIntegrity.Org build info.";

        public string Syntax => "";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string> { "buildinfo" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            string version = Assembly.GetEntryAssembly().GetName().Version.ToString();


            TaskDispatcher.RunAsync(() =>
            {
                TaskDispatcher.QueueOnMainThread(() =>
                {
                    UnturnedChat.Say(player, version);
                });
            });
        }
    }
}
