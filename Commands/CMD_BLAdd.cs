using NoIntegrity.Functions;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;

namespace NoIntegrity.Commands
{
    public class CMD_BLAdd : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "bladd";

        public string Help => "Adds an item to the blacklist.";

        public string Syntax => "/bladd <item id>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>();

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;

            if (command.Length != 1)
            {
                UnturnedChat.Say(caller, Syntax);
                return;
            }
            
            int hisItemID = Int32.Parse(command[0]);
            int tryBLAdd = DBHandler.dbAddBLItem(player, hisItemID);
            if (tryBLAdd > 0)
            {
                UnturnedChat.Say(caller, $"Item {hisItemID} added to the blacklist with immediate effect.");
            }
                else
            {
                UnturnedChat.Say(caller, "Unable to add item to blacklist - it already exists.");
            }
        }
    }
}
