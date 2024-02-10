using NoIntegrity.Models;
using Rocket.Unturned.Enumerations;
using Rocket.Unturned.Player;
using Rocket.Core.Logging;
using SDG.Unturned;

namespace NoIntegrity.Functions
{
    public class Helper
    {
        public static void ListItems(UnturnedPlayer player, InventoryGroup inventoryGroup, byte inventoryIndex, ItemJar P)
        {
            PlayerClothing clo = player.Player.clothing;

            SLOTS_Clothing hisHat = new SLOTS_Clothing(clo.hat, clo.hatQuality, clo.hatState);
            SLOTS_Clothing hisShirt = new SLOTS_Clothing(clo.shirt, clo.shirtQuality, clo.shirtState);
            SLOTS_Clothing hisVest = new SLOTS_Clothing(clo.vest, clo.vestQuality, clo.vestState);
            SLOTS_Clothing hisMask = new SLOTS_Clothing(clo.mask, clo.maskQuality, clo.maskState);
            SLOTS_Clothing hisPants = new SLOTS_Clothing(clo.pants, clo.pantsQuality, clo.pantsState);
            SLOTS_Clothing hisGlasses = new SLOTS_Clothing(clo.glasses, clo.glassesQuality, clo.glassesState);
            SLOTS_Clothing hisBackpack = new SLOTS_Clothing(clo.backpack, clo.backpackQuality, clo.backpackState);

            Logger.Log("--------------------------------------------------------------------");
            Logger.Log($"Steam ID:  {player.CSteamID}");

            Logger.Log($"Hat ID:  {hisHat.id}");
            Logger.Log($"Hat Quality:  {hisHat.quality}");
            Logger.Log($"Glasses ID:  {hisGlasses.id}");
            Logger.Log($"Glasses Quality:  {hisGlasses.quality}");
            Logger.Log($"Mask ID:  {hisMask.id}");
            Logger.Log($"Mask Quality:  {hisMask.quality}");
            Logger.Log($"Shirt ID:  {hisShirt.id}");
            Logger.Log($"Shirt Quality:  {hisShirt.quality}");
            Logger.Log($"Vest ID:  {hisVest.id}");
            Logger.Log($"Vest Quality:  {hisVest.quality}");
            Logger.Log($"Pants ID:  {hisPants.id}");
            Logger.Log($"Pants Quality:  {hisPants.quality}");
            Logger.Log($"Backpack ID:  {hisBackpack.id}");
            Logger.Log($"Backpack Quality:  {hisBackpack.quality}");



            for (byte p = 0; p < PlayerInventory.PAGES - 1; p++)
            {
                Logger.Log($"Page: {p}");
                for (byte i = 0; i < player.Inventory.getItemCount(p); i++)
                {
                    Item item = player.Inventory.getItem(p, i).item; // Get the user's item in current page
                    //Items inventory = new Items(p);  // Assign page to inventory

                    var hisMetaData = Bytes.bytesToCDString(item.metadata);

                    Logger.Log($"-- Item:  {item.id}");
                    Logger.Log($"-- Item Metadata:  {hisMetaData}");
                    Logger.Log($"-- Item Quality:  {item.quality}");


                    ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, item.id);
                    var itemType = player.Inventory.getItem(p, i).GetType();
                    var itemX = player.Inventory.getItem(p, i).x;
                    var itemY = player.Inventory.getItem(p, i).y;
                    var itemRot = player.Inventory.getItem(p, i).rot;
                    var itemMag = player.Inventory.getItem(p, i).item.amount;

                    Logger.Log($"-- Size x:  {itemAsset.size_x}");
                    Logger.Log($"-- Size y:  {itemAsset.size_y}");
                    Logger.Log($"-- Page x:  {itemX}");
                    Logger.Log($"-- Page y:  {itemY}");
                    Logger.Log($"-- Rotation:  {itemRot}");
                    Logger.Log($"-- Item Type:  {itemType}");
                    Logger.Log($"-- Magazine Amount:  {itemMag}");

                }
            }
            Logger.Log("--------------------------------------------------------------------");
        }

    }
}
