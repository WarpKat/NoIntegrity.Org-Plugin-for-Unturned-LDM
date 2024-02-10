using NoIntegrity.Functions;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;

namespace NoIntegrity.Commands
{
    public class CMD_BLDel : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "bldel";

        public string Help => "Deletes an item from the blacklist.";

        public string Syntax => "/bldel <item id>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>();

        public void Execute(IRocketPlayer caller, string[] command)
        {
            NoIntegrityConfiguration config = NoIntegrity.Instance.Configuration.Instance;

            UnturnedPlayer player = (UnturnedPlayer)caller;

            if (command.Length != 1)
            {
                UnturnedChat.Say(caller, Syntax);
                return;
            }
            
            int hisItemID = Int32.Parse(command[0]);
            int tryBLDel = DBHandler.dbDelBLItem(hisItemID);
            if (tryBLDel == 0)
            {
                UnturnedChat.Say(caller, $"Item {hisItemID} does not exist in the database.");
                return;
            }
                else
            {
                UnturnedChat.Say(caller, $"Item {hisItemID} deleted from the blacklist with immediate effect.");
                return;
            }
        }
    }
}
