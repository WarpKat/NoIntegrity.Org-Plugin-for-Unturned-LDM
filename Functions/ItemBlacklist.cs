using Rocket.Unturned.Chat;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;

namespace NoIntegrity.Functions
{
    public class ItemBlacklist
    {
        public static void OnTakeItemRequested(Player player1, byte x, byte y, uint instanceID, byte to_x, byte to_y, byte to_rot, byte to_page, ItemData itemData, ref bool shouldAllow)
        {
            int checkForItem = DBHandler.dbCheckForBLItem(itemData.item.id);

            UnturnedPlayer player = UnturnedPlayer.FromPlayer(player1);
            if (checkForItem == 1)
            {
                string itemName = Assets.find(EAssetType.ITEM, itemData.item.id).FriendlyName;
                UnturnedChat.Say(player, $"{itemName} is restricted.");
                shouldAllow = false;
                return;
            }

            return;
        }

        public static void OnPlayerInventoryAdded(UnturnedPlayer player, Rocket.Unturned.Enumerations.InventoryGroup inventoryGroup, byte inventoryIndex, ItemJar P)
        {
            int checkForItem = DBHandler.dbCheckForBLItem(P.item.id);

            if (checkForItem == 1)
            {
                string itemName = Assets.find(EAssetType.ITEM, P.item.id).FriendlyName;

                UnturnedChat.Say(player, $"{itemName} is restricted.");
                
                // Special thanks to MrKan for helping shorten the code and making this part look cleaner!  :)
                if (Assets.find(EAssetType.ITEM, P.item.id) is ItemClothingAsset)
                {
                    return;
                }

                player.Inventory.removeItem((byte)inventoryGroup, inventoryIndex);
                return;
            }
            return;
        }

        public static void OnPlayerWear(UnturnedPlayer player, UnturnedPlayerEvents.Wearables wear, ushort itemID, byte? quality)
        {
            int checkForItem = DBHandler.dbCheckForBLItem(itemID);

            if (checkForItem == 1)
            {
                switch (wear)
                {
                    case UnturnedPlayerEvents.Wearables.Backpack:
                        player.Player.clothing.askWearBackpack(0, 0, new byte[0], true);
                        break;
                    case UnturnedPlayerEvents.Wearables.Glasses:
                        player.Player.clothing.askWearGlasses(0, 0, new byte[0], true);
                        break;
                    case UnturnedPlayerEvents.Wearables.Hat:
                        player.Player.clothing.askWearHat(0, 0, new byte[0], true);
                        break;
                    case UnturnedPlayerEvents.Wearables.Mask:
                        player.Player.clothing.askWearMask(0, 0, new byte[0], true);
                        break;
                    case UnturnedPlayerEvents.Wearables.Pants:
                        player.Player.clothing.askWearPants(0, 0, new byte[0], true);
                        break;
                    case UnturnedPlayerEvents.Wearables.Shirt:
                        player.Player.clothing.askWearShirt(0, 0, new byte[0], true);
                        break;
                    case UnturnedPlayerEvents.Wearables.Vest:
                        player.Player.clothing.askWearVest(0, 0, new byte[0], true);
                        break;
                }

                string itemName = Assets.find(EAssetType.ITEM, itemID)?.FriendlyName;
                UnturnedChat.Say(player, $"{itemName} is restricted.");
                return;
            }
        }
    }
}
