﻿using MySql.Data.MySqlClient;
using Rocket.Core.Logging;
using Rocket.Unturned.Player;
using SDG.Unturned;
using NoIntegrity.Models;
using System;
using System.Collections.Generic;

namespace NoIntegrity.Functions
{
    public class DBHandler
    {
        private static MySqlConnection dbCreateConnection()
        {
            MySqlConnection result = null;

            NoIntegrityConfiguration config = NoIntegrity.Instance.Configuration.Instance;

            try
            {
                result = new MySqlConnection(
                    String.Format("SERVER={0};DATABASE={1};UID={2};PASSWORD={3};PORT={4};",
                    config.DBHost,
                    config.DBName, 
                    config.DBUsername,
                    config.DBPassword,
                    config.DBPort)
                );

                return result;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return result;
        }

        public static void dbCheckClothingSchema()
        {
            try
            {
                NoIntegrityConfiguration config = NoIntegrity.Instance.Configuration.Instance;

                using MySqlConnection mySqlConnection = DBHandler.dbCreateConnection();
                using MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();

                var myTablePrefix = config.DBTablePrefix;

                mySqlCommand.CommandText = "SHOW TABLES LIKE '" + myTablePrefix + "_Clothing'";

                mySqlConnection.Open();
                if(mySqlCommand.ExecuteScalar() == null)
                {
                    mySqlCommand.CommandText = "CREATE TABLE `" + myTablePrefix + "_Clothing` (`SteamID` varchar(32) NOT NULL, `SlotID` int NOT NULL, `ClothingIDX` int NOT NULL, `ClothingID` int NOT NULL, `Quality` int NOT NULL, `Timestamp` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;";
                    mySqlCommand.ExecuteNonQuery();
                }
                mySqlConnection.Close();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        public static void dbCheckInventorySchema()
        {
            try
            {
                NoIntegrityConfiguration config = NoIntegrity.Instance.Configuration.Instance;

                using MySqlConnection mySqlConnection = DBHandler.dbCreateConnection();
                using MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();

                var myTablePrefix = config.DBTablePrefix;

                mySqlCommand.CommandText = "SHOW TABLES LIKE '" + myTablePrefix + "_Inventory'";

                mySqlConnection.Open();
                if (mySqlCommand.ExecuteScalar() == null)
                {
                    mySqlCommand.CommandText = "CREATE TABLE `" + myTablePrefix + "_Inventory` (`SteamID` varchar(32) NOT NULL, `SlotID` int NOT NULL, `Page` int NOT NULL, `ItemID` int NOT NULL, `Metadata` varchar(256) NOT NULL, `Quality` int NOT NULL, `pageX` int NOT NULL, `pageY` int NOT NULL, `pageRot` int NOT NULL, `MagAmount` int NOT NULL, `Timestamp` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;";
                    mySqlCommand.ExecuteNonQuery();
                }
                mySqlConnection.Close();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        public static void dbCheckBlacklistSchema()
        {
            try
            {
                NoIntegrityConfiguration config = NoIntegrity.Instance.Configuration.Instance;

                using MySqlConnection mySqlConnection = DBHandler.dbCreateConnection();
                using MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();

                var myTablePrefix = config.DBTablePrefix;

                mySqlCommand.CommandText = "SHOW TABLES LIKE '" + myTablePrefix + "_BlacklistItems'";

                mySqlConnection.Open();
                if (mySqlCommand.ExecuteScalar() == null)
                {
                    mySqlCommand.CommandText = "CREATE TABLE `" + myTablePrefix + "_BlacklistItems` (`ID` int NOT NULL, `SteamID` varchar(32) NOT NULL, `SteamName` varchar(128) NOT NULL, `Timestamp` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;";
                    mySqlCommand.ExecuteNonQuery();

                    mySqlCommand.CommandText = "ALTER TABLE `" + myTablePrefix + "_BlacklistItems` ADD PRIMARY KEY (`ID`);";
                    mySqlCommand.ExecuteNonQuery();
                }
                mySqlConnection.Close();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        public static int dbGetNumSlots(UnturnedPlayer player)
        {
            NoIntegrityConfiguration config = NoIntegrity.Instance.Configuration.Instance;
            
            using MySqlConnection mySqlConnection = DBHandler.dbCreateConnection();
            using MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();

            var hisSteamID = player.CSteamID;

            var myTablePrefix = config.DBTablePrefix;
            int maxSlots = config.slotsMaxSlots;

            int tooManyClothesSlots = 0;
            int tooManyInventorySlots = 0;

            mySqlCommand.CommandText = "SELECT COUNT(DISTINCT `SlotID`) FROM `" + myTablePrefix + "_Clothing` WHERE `SteamID`='" + hisSteamID + "'";

            mySqlConnection.Open();
            long myClothingSlotCount = (long)mySqlCommand.ExecuteScalar();
            if ( myClothingSlotCount == maxSlots)
            {
                tooManyClothesSlots = 1;
            }
            mySqlConnection.Close();

            mySqlCommand.CommandText = "SELECT COUNT(DISTINCT `SlotID`) FROM `" + myTablePrefix + "_Inventory` WHERE `SteamID`='" + hisSteamID + "'";

            mySqlConnection.Open();
            long myInventorySlotCount = (long)mySqlCommand.ExecuteScalar();
            if (myInventorySlotCount == maxSlots)
            {
                tooManyInventorySlots = 1;
            }
            mySqlConnection.Close();

            int tooManySlots = tooManyClothesSlots + tooManyInventorySlots;
            if (tooManySlots > 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public static int dbSaveInventory(UnturnedPlayer player, int slotID)
        {
            NoIntegrityConfiguration config = NoIntegrity.Instance.Configuration.Instance;

            using MySqlConnection mySqlConnection = DBHandler.dbCreateConnection();
            using MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();

            PlayerClothing clo = player.Player.clothing;

            SLOTS_Clothing hisHat = new SLOTS_Clothing(clo.hat, clo.hatQuality, clo.hatState);
            SLOTS_Clothing hisGlasses = new SLOTS_Clothing(clo.glasses, clo.glassesQuality, clo.glassesState);
            SLOTS_Clothing hisMask = new SLOTS_Clothing(clo.mask, clo.maskQuality, clo.maskState);
            SLOTS_Clothing hisShirt = new SLOTS_Clothing(clo.shirt, clo.shirtQuality, clo.shirtState);
            SLOTS_Clothing hisVest = new SLOTS_Clothing(clo.vest, clo.vestQuality, clo.vestState);
            SLOTS_Clothing hisPants = new SLOTS_Clothing(clo.pants, clo.pantsQuality, clo.pantsState);
            SLOTS_Clothing hisBackpack = new SLOTS_Clothing(clo.backpack, clo.backpackQuality, clo.backpackState);

            int[,] hisClothing = {
                { hisShirt.id, hisShirt.quality },
                { hisPants.id, hisPants.quality },
                { hisHat.id, hisHat.quality },
                { hisBackpack.id, hisBackpack.quality },
                { hisVest.id, hisVest.quality },
                { hisMask.id, hisMask.quality },
                { hisGlasses.id, hisGlasses.quality }
            };

            // Delete the clothing slot requested.
            mySqlConnection.Open();
            mySqlCommand.CommandText = $"DELETE FROM `{config.DBTablePrefix}_Clothing` WHERE `SteamID`='{player.CSteamID}' and `SlotID`='{slotID}'";
            mySqlCommand.ExecuteScalar();

            // Populate the slot with new clothing or empty asset.
            for ( int cloIDX = 0; cloIDX < hisClothing.GetLength(0); cloIDX++)
            {
                mySqlCommand.CommandText = $"INSERT INTO `{config.DBTablePrefix}_Clothing` (`SteamID`, `SlotID`, `ClothingIDX`, `ClothingID`, `Quality`, `Timestamp`) VALUES('{player.CSteamID}', '{slotID}', '{cloIDX}', '{hisClothing[cloIDX, 0]}', '{hisClothing[cloIDX, 1]}', CURRENT_TIMESTAMP)";
                mySqlCommand.ExecuteScalar();
            }

            // Delete the inventory slot requested.
            mySqlCommand.CommandText = $"DELETE FROM `{config.DBTablePrefix}_Inventory` WHERE `SteamID`='{player.CSteamID}' and `SlotID`='{slotID}'";
            mySqlCommand.ExecuteScalar();

            // Populate the slot with new inventory.
            for (byte p = 0; p < PlayerInventory.PAGES - 1; p++)
            {
                for (byte i = 0; i < player.Inventory.getItemCount(p); i++)
                {
                    Item item = player.Inventory.getItem(p, i).item; // Get the user's item in current page

                    string hisMetaData = "0";
                    if (Bytes.bytesToCDString(item.metadata).Length > 0)
                    {
                        hisMetaData = Bytes.bytesToCDString(item.metadata);
                    }

                    var itemX = player.Inventory.getItem(p, i).x;
                    var itemY = player.Inventory.getItem(p, i).y;
                    var itemRot = player.Inventory.getItem(p, i).rot;
                    var itemMag = player.Inventory.getItem(p, i).item.amount;

                    mySqlCommand.CommandText = $"INSERT INTO `{config.DBTablePrefix}_Inventory` (`SteamID`, `SlotID`, `Page`, `ItemID`, `Metadata`, `Quality`, `pageX`, `pageY`, `pageRot`, `MagAmount`, `Timestamp`) VALUES ('{player.CSteamID}', '{slotID}', '{p}', '{item.id}', '{hisMetaData}', '{item.quality}', '{itemX}', '{itemY}', '{itemRot}', '{itemMag}', CURRENT_TIMESTAMP);";
                    mySqlCommand.ExecuteScalar();
                }
            }
            mySqlConnection.Close();

            return 1;
        }

        public static int[,] dbLoadClothing(UnturnedPlayer player, int slotID)
        {
            NoIntegrityConfiguration config = NoIntegrity.Instance.Configuration.Instance;

            using MySqlConnection mySqlConnection = DBHandler.dbCreateConnection();
            using MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();

            mySqlConnection.Open();
            mySqlCommand.CommandText = $"SELECT `ClothingIDX`, `ClothingID`, `Quality` FROM `{config.DBTablePrefix}_Clothing` WHERE `SteamID`='{player.CSteamID}' and `slotID`='{slotID}' ORDER BY `ClothingIDX` ASC";

            int hisClothingIndex = 0;
            int hisClothingID = 0;
            int hisClothingQuality = 0;

            int[,] hisClothingLoadout = new int[,] {
                { 0, 0 },
                { 0, 0 },
                { 0, 0 },
                { 0, 0 },
                { 0, 0 },
                { 0, 0 },
                { 0, 0 }
            };

            object myResultSet = mySqlCommand.ExecuteNonQuery();
            using (MySqlDataReader myReader = mySqlCommand.ExecuteReader())
            {
                while(myReader.Read())
                {
                    hisClothingIndex = Convert.ToInt32(myReader.GetString(0));
                    hisClothingID = Convert.ToInt32(myReader.GetString(1));
                    hisClothingQuality = Convert.ToInt32((myReader.GetString(2)));

                    hisClothingLoadout[hisClothingIndex, 0] = hisClothingID;
                    hisClothingLoadout[hisClothingIndex, 1] = hisClothingQuality;
                }
            }
            mySqlConnection.Close();
            
            return hisClothingLoadout;
        }

        public static List<string[]> dbLoadInventory(UnturnedPlayer player, int slotID)
        {
            /*
             * Compliments of ChatGPT - 20240204
             * "In C#, how do I tell a function to return MySQL rows as an array?"
             */
            NoIntegrityConfiguration config = NoIntegrity.Instance.Configuration.Instance;

            var hisSteamID = player.CSteamID;
            var myTablePrefix = config.DBTablePrefix;

            var query = "SELECT * FROM `" + myTablePrefix + "_Inventory` WHERE `SteamID`='" + hisSteamID + "' and `SlotID`='" + slotID + "'";

            List<string[]> rows = new List<string[]>();

            using (MySqlConnection connection = DBHandler.dbCreateConnection())
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Assuming all columns are strings; you may need to adjust this based on your table structure
                        string[] rowValues = new string[reader.FieldCount];

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            rowValues[i] = reader[i].ToString();
                        }

                        rows.Add(rowValues);
                    }
                }
            }

            return rows;
        }

        public static int dbCheckForBLItem(int thisItemID)
        {
            NoIntegrityConfiguration config = NoIntegrity.Instance.Configuration.Instance;

            var myTablePrefix = config.DBTablePrefix;

            using MySqlConnection mySqlConnection = DBHandler.dbCreateConnection();
            using MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();

            mySqlCommand.CommandText = "SELECT COUNT(*) FROM `" + myTablePrefix + "_BlacklistItems` WHERE `ID`='" + thisItemID + "'";

            mySqlConnection.Open();
            long myBLItemCount = (long)mySqlCommand.ExecuteScalar();
            mySqlConnection.Close();

            return Convert.ToInt32(myBLItemCount);
        }

        public static int dbAddBLItem(UnturnedPlayer player,  int thisItemID)
        {
            NoIntegrityConfiguration config = NoIntegrity.Instance.Configuration.Instance;

            int checkForItem = dbCheckForBLItem(thisItemID);

            if (checkForItem == 1)
            {
                return 0;
            }
                else
            {
                var myTablePrefix = config.DBTablePrefix;
                var hisSteamID = player.CSteamID;
                var hisSteamName = player.SteamName;

                using MySqlConnection mySqlConnection = DBHandler.dbCreateConnection();
                using MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();

                mySqlConnection.Open();
                mySqlCommand.CommandText = $"INSERT INTO `{myTablePrefix}_BlacklistItems` (`ID`, `SteamID`, `SteamName`, `Timestamp`) VALUES ('{thisItemID}', '{hisSteamID}', '{hisSteamName}', CURRENT_TIMESTAMP)";
                mySqlCommand.ExecuteScalar();
                mySqlConnection.Close();

                return 1;
            }
        }

        public static int dbDelBLItem(int thisItemID)
        {
            NoIntegrityConfiguration config = NoIntegrity.Instance.Configuration.Instance;

            int checkForItem = dbCheckForBLItem(thisItemID);

            if (checkForItem == 0)
            {
                return 0;
            }
            else
            {
                var myTablePrefix = config.DBTablePrefix;

                using MySqlConnection mySqlConnection = DBHandler.dbCreateConnection();
                using MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();

                mySqlConnection.Open();
                mySqlCommand.CommandText = $"DELETE FROM `{myTablePrefix}_BlacklistItems` WHERE `ID`={thisItemID}";
                mySqlCommand.ExecuteScalar();
                mySqlConnection.Close();

                return 1;
            }
        }


    }
}
