using NoIntegrity.Functions;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using Rocket.Core.Utils;
using SDG.Unturned;
using System;
using System.Collections.Generic;

namespace NoIntegrity.Commands
{
    public class CMD_SlotLoad : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "slotload";

        public string Help => "This command will load your selected saved loadout.";

        public string Syntax => "<slot number>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string> { "slotload" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            NoIntegrityConfiguration config = NoIntegrity.Instance.Configuration.Instance;
            
            UnturnedPlayer player = (UnturnedPlayer)caller;

            if(config.isCreative == false)
            {
                UnturnedChat.Say(caller, $"This command is not available.  Server is not configured for Creative mode.");
                return;
            }

            if (command.Length != 1)
            {
                UnturnedChat.Say(caller, $"You need to specify a slot number from 1-{config.slotsMaxSlots}.");
                return;
            }

            int thisSlotNum = Int32.Parse(command[0]);

            /*
             * This next for loop was taken from uEssentials /clearinventory command
             */
            for (byte page = 0; page < PlayerInventory.PAGES; page++)
            {
                if (page == PlayerInventory.AREA)
                    continue;

                var count = player.Inventory.getItemCount(page);

                for (byte index = 0; index < count; index++)
                {
                    player.Inventory.removeItem(page, 0);
                }
            }

            // Dequip anything in hand and remove from inventory.
            player.Player.equipment.dequip();
            for (byte p = 0; p < PlayerInventory.PAGES - 1; p++)
            {
                for (byte i = 0; i < player.Inventory.getItemCount(p); i++)
                {
                    player.Inventory.removeItem(p, 0);
                }
            }

            // Remove & replace the player's clothing.
            ushort hisShirt = 0;
            ushort hisShirtQuality = 0;
            ushort hisPants = 0;
            ushort hisPantsQuality = 0;
            ushort hisHat = 0;
            ushort hisHatQuality = 0;
            ushort hisBackpack = 0;
            ushort hisBackpackQuality = 0;
            ushort hisVest = 0;
            ushort hisVestQuality = 0;
            ushort hisMask = 0;
            ushort hisMaskQuality = 0;
            ushort hisGlasses = 0;
            ushort hisGlassesQuality = 0;

            TaskDispatcher.RunAsync(() =>
            {
                int[,] hisClothing = DBHandler.dbLoadClothing(player, Int32.Parse(command[0]));
                List<string[]> hisInventory = DBHandler.dbLoadInventory(player, Int32.Parse(command[0]));

                TaskDispatcher.QueueOnMainThread(() =>
                {
                    for (int i = 0; i < hisClothing.GetLength(0); i++)
                    {
                        switch (i)
                        {
                            case 0:
                                hisShirt = (ushort)hisClothing[i, 0];
                                hisShirtQuality = (ushort)hisClothing[i, 1];
                                break;
                            case 1:
                                hisPants = (ushort)hisClothing[i, 0];
                                hisPantsQuality = (ushort)hisClothing[i, 1];
                                break;
                            case 2:
                                hisHat = (ushort)hisClothing[i, 0];
                                hisHatQuality = (ushort)hisClothing[i, 1];
                                break;
                            case 3:
                                hisBackpack = (ushort)hisClothing[i, 0];
                                hisBackpackQuality = (ushort)hisClothing[i, 1];
                                break;
                            case 4:
                                hisVest = (ushort)hisClothing[i, 0];
                                hisVestQuality = (ushort)hisClothing[i, 1];
                                break;
                            case 5:
                                hisMask = (ushort)hisClothing[i, 0];
                                hisMaskQuality = (ushort)hisClothing[i, 1];
                                break;
                            case 6:
                                hisGlasses = (ushort)hisClothing[i, 0];
                                hisGlassesQuality = (ushort)hisClothing[i, 1];
                                break;
                        }
                    }

                    player.Player.clothing.updateClothes(
                        hisShirt, (byte)hisShirtQuality, new byte[0],
                        hisPants, (byte)hisPantsQuality, new byte[0],
                        hisHat, (byte)hisHatQuality, new byte[0],
                        hisBackpack, (byte)hisBackpackQuality, new byte[0],
                        hisVest, (byte)hisVestQuality, new byte[0],
                        hisMask, (byte)hisMaskQuality, new byte[0],
                        hisGlasses, (byte)hisGlassesQuality, new byte[0]
                        );

                    // Fetch all of the stored inventory for the preferred slot.
                    foreach (string[] row in hisInventory)
                    {
                        // Process each row
                        byte thisPage = Convert.ToByte(row[2]);
                        byte thisX = Convert.ToByte(row[6]);
                        byte thisY = Convert.ToByte(row[7]);
                        byte thisRot = Convert.ToByte(row[8]);
                        ushort thisItemID = Convert.ToUInt16(row[3]);
                        byte[] thisMeta = Bytes.stringToByte(row[4]);
                        byte thisAmount = Convert.ToByte(row[9]);
                        byte thisQuality = Convert.ToByte(row[5]);

                        player.Inventory.ReceiveItemAdd(thisPage, thisX, thisY, thisRot, thisItemID, thisAmount, thisQuality, thisMeta);
                    }

                    player.TriggerEffect(10);
                    UnturnedChat.Say(caller, $"Loaded slot {thisSlotNum}.");
                });
            });
        }
    }
}
