using NoIntegrity.Functions;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;

namespace NoIntegrity.Commands
{
    public class CMD_SlotSave : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "slotsave";

        public string Help => "This command will save your current loadout.  You can retrieve it by issuing /slotload <slot number> in chat.";

        public string Syntax => "<slot number>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string> { "slotsave" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            NoIntegrityConfiguration config = NoIntegrity.Instance.Configuration.Instance;

            UnturnedPlayer player = (UnturnedPlayer)caller;

            if (command.Length != 1)
            {
                UnturnedChat.Say(caller, $"You need to specify a slot number from 1-{config.slotsMaxSlots}.");
                return;
            }

            int thisSlotNum = Int32.Parse(command[0]);

            //int checkSlots = DBHandler.dbGetNumSlots(player);
            
            if (command.Length != 1)
            {
                UnturnedChat.Say(caller, Syntax);
                return;
            }

            if(thisSlotNum == 0)
            {
                UnturnedChat.Say(caller, $"You may not save slot 0.");
                return;
            }

            if( thisSlotNum > config.slotsMaxSlots)
            {
                UnturnedChat.Say(caller, $"You may not save a slot greater than {config.slotsMaxSlots} on this server.");
                return;
            }

            int isSaved = DBHandler.dbSaveInventory(player, thisSlotNum);

            if (isSaved == 1) {
                UnturnedChat.Say(caller, $"Saved slot {thisSlotNum}.");
            } else
            {
                UnturnedChat.Say(caller, $"Unable to save slot {thisSlotNum}.");
            }

            return;
        }
    }
}
