using MySql.Data.MySqlClient;
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

                    mySqlCommand.CommandText = "ALTER TABLE `" + myTablePrefix + "_Clothing` ADD INDEX(`SteamID`);";
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

                    mySqlCommand.CommandText = "ALTER TABLE `" + myTablePrefix + "_Inventory` ADD INDEX(`SteamID`);";
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

        public static void dbCheckStatsSchema()
        {
            try
            {
                NoIntegrityConfiguration config = NoIntegrity.Instance.Configuration.Instance;

                using MySqlConnection mySqlConnection = DBHandler.dbCreateConnection();
                using MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();

                var myTablePrefix = config.statsPrefix;

                mySqlCommand.CommandText = "SHOW TABLES LIKE '" + myTablePrefix + "_Statistics'";


                mySqlConnection.Open();
                if (mySqlCommand.ExecuteScalar() == null)
                {
                    mySqlCommand.CommandText = "CREATE TABLE `" + myTablePrefix + "_Statistics` (`SteamID` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL, `kKill` int NOT NULL DEFAULT '0', `kHeadshot` int NOT NULL DEFAULT '0', `dBleeding` int NOT NULL DEFAULT '0', `dBones` int NOT NULL DEFAULT '0', `dFreezing` int NOT NULL DEFAULT '0', `dBurning` int NOT NULL DEFAULT '0', `dFood` int NOT NULL DEFAULT '0', `dWater` int NOT NULL DEFAULT '0', `dGun` int NOT NULL DEFAULT '0', `dMelee` int NOT NULL DEFAULT '0', `dZombie` int NOT NULL DEFAULT '0', `dAnimal` int NOT NULL DEFAULT '0', `dKill` int NOT NULL DEFAULT '0', `dInfection` int NOT NULL DEFAULT '0', `dPunch` int NOT NULL DEFAULT '0', `dBreath` int NOT NULL DEFAULT '0', `dRoadkill` int NOT NULL DEFAULT '0', `dVehicle` int NOT NULL DEFAULT '0', `dGrenade` int NOT NULL DEFAULT '0', `dShred` int NOT NULL DEFAULT '0', `dLandmine` int NOT NULL DEFAULT '0', `dArena` int NOT NULL DEFAULT '0', `dSuicide` int NOT NULL DEFAULT '0', `dMissile` int NOT NULL DEFAULT '0', `dCharge` int NOT NULL DEFAULT '0', `dSplash` int NOT NULL DEFAULT '0', `dSentry` int NOT NULL DEFAULT '0', `dAcid` int NOT NULL DEFAULT '0', `dBoulder` int NOT NULL DEFAULT '0', `dBurner` int NOT NULL DEFAULT '0', `dSpit` int NOT NULL DEFAULT '0', `dSpark` int NOT NULL DEFAULT '0', `dOther` int NOT NULL DEFAULT '0', `FirstRecorded` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP, `Timestamp` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;";
                    mySqlCommand.ExecuteNonQuery();

                    mySqlCommand.CommandText = "ALTER TABLE `" + myTablePrefix + "_Statistics` ADD UNIQUE(`SteamID`);";
                    mySqlCommand.ExecuteNonQuery();
                }
                mySqlConnection.Close();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        /*
        public static int dbGetNumSlots(UnturnedPlayer player)
        {
            try
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
                if (myClothingSlotCount == maxSlots)
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
                catch(Exception ex)
            {
                Logger.LogException(ex);
                return 0;
            }
        }
        */

        public static int dbSaveInventory(UnturnedPlayer player, int slotID)
        {
            try
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
                for (int cloIDX = 0; cloIDX < hisClothing.GetLength(0); cloIDX++)
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
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return 0;
            }
        }

        public static int[,] dbLoadClothing(UnturnedPlayer player, int slotID)
        {
            try
            {
                NoIntegrityConfiguration config = NoIntegrity.Instance.Configuration.Instance;

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

                using MySqlConnection mySqlConnection = DBHandler.dbCreateConnection();
                using MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();

                mySqlConnection.Open();
                mySqlCommand.CommandText = $"SELECT `ClothingIDX`, `ClothingID`, `Quality` FROM `{config.DBTablePrefix}_Clothing` WHERE `SteamID`='{player.CSteamID}' and `slotID`='{slotID}' ORDER BY `ClothingIDX` ASC";

                object myResultSet = mySqlCommand.ExecuteNonQuery();

                using (MySqlDataReader myReader = mySqlCommand.ExecuteReader())
                {
                    while (myReader.Read())
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
            catch(Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }

        public static List<string[]> dbLoadInventory(UnturnedPlayer player, int slotID)
        {
            try
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
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }

        public static int dbCheckForBLItem(int thisItemID)
        {
            try
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
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return 0;
            }
        }

        public static int dbAddBLItem(UnturnedPlayer player,  int thisItemID)
        {
            try
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
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return 0;
            }
        }

        public static int dbDelBLItem(int thisItemID)
        {
            try
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
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return 0;
            }
        }

        public static void dbUpdateStats(string hisSteamID, string statType)
        {
            try
            {
                NoIntegrityConfiguration config = NoIntegrity.Instance.Configuration.Instance;
                using MySqlConnection mySqlConnection = DBHandler.dbCreateConnection();
                using MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();

                var myTablePrefix = config.statsPrefix;

                mySqlCommand.CommandText = $"INSERT INTO `{myTablePrefix}_Statistics` (`SteamID`, `{statType}`) VALUES('{hisSteamID}', 1) ON DUPLICATE KEY UPDATE `{statType}` = 1 + (SELECT `{statType}` where `SteamID` = '{hisSteamID}')";

                mySqlConnection.Open();
                mySqlCommand.ExecuteScalar();
                mySqlConnection.Close();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        public static string[] dbFetchStats(string hisSteamID)
        {
            string[] hisStats = new string[] {"0","0","0"};

            try
            {
                NoIntegrityConfiguration config = NoIntegrity.Instance.Configuration.Instance;

                using MySqlConnection mySqlConnection = DBHandler.dbCreateConnection();
                using MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();

                var myTablePrefix = config.statsPrefix;

                // Check to see if the entry for stats exists for this user; if not, create the record.
                using (MySqlConnection connection = DBHandler.dbCreateConnection())
                {
                    connection.Open();

                    string qryGetUserCount = $@"
                        SELECT
                            COUNT(*)
                        FROM
                            `{myTablePrefix}_Statistics`
                        WHERE
                            `SteamID` = '{hisSteamID}'
                    ";

                    MySqlCommand cmd = new MySqlCommand(qryGetUserCount, connection);
                    long myBLItemCount = (long)cmd.ExecuteScalar();

                    if(myBLItemCount == 0)
                    {
                        string qryNewRecord = $@"
                            INSERT INTO
                                `{myTablePrefix}_Statistics`
                                    (`SteamID`)
                                VALUES
                                    ('{hisSteamID}')
                        ";
                        MySqlCommand cmd1 = new MySqlCommand(qryNewRecord, connection);
                        cmd1.ExecuteScalar();
                    }

                    connection.Close();
                }

                // Get his kills
                using (MySqlConnection connection = DBHandler.dbCreateConnection())
                {
                    connection.Open();

                    string qryGetKills = $@"
                        SELECT
                            (
                                `kKill`
                            +   `kHeadshot`
                            ) as hisKills
                        FROM
                            `{myTablePrefix}_Statistics`
                        WHERE
                            `SteamID` = '{hisSteamID}'
                    ";

                    MySqlCommand cmd = new MySqlCommand(qryGetKills, connection);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        hisStats[0] = reader.GetValue(0).ToString();
                    }
                    connection.Close();
                }

                // Get his headshots.
                using (MySqlConnection connection = DBHandler.dbCreateConnection())
                {
                    connection.Open();

                    string qryGetHeadshots = $@"
                        SELECT
                            (
                                `kHeadshot`
                            ) as hisHeadshots
                        FROM
                            `{myTablePrefix}_Statistics`
                        WHERE
                            `SteamID` = '{hisSteamID}'
                    ";

                    MySqlCommand cmd = new MySqlCommand(qryGetHeadshots, connection);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        hisStats[1] = reader.GetValue(0).ToString();
                    }
                    connection.Close();
                }

                // Get his deaths.
                using (MySqlConnection connection = DBHandler.dbCreateConnection())
                {
                    connection.Open();

                    string qryGetDeaths = $@"
                        SELECT
                            (
                                `dBleeding`
                            +   `dBones`
                            +   `dFreezing`
                            +   `dBurning`
                            +   `dFood`
                            +   `dWater`
                            +   `dGun`
                            +   `dMelee`
                            +   `dZombie`
                            +   `dAnimal`
                            +   `dKill`
                            +   `dInfection`
                            +   `dPunch`
                            +   `dBreath`
                            +   `dRoadkill`
                            +   `dVehicle`
                            +   `dGrenade`
                            +   `dShred`
                            +   `dLandmine`
                            +   `dArena`
                            +   `dSuicide`
                            +   `dMissile`
                            +   `dCharge`
                            +   `dSplash`
                            +   `dSentry`
                            +   `dAcid`
                            +   `dBoulder`
                            +   `dBurner`
                            +   `dSpit`
                            +   `dSpark`
                            +   `dOther`
                            ) as hisDeaths
                        FROM
                            `{myTablePrefix}_Statistics`
                        WHERE
                            `SteamID` = '{hisSteamID}'
                    ";

                    MySqlCommand cmd = new MySqlCommand(qryGetDeaths, connection);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        hisStats[2] = reader.GetValue(0).ToString();
                    }
                    connection.Close();
                }

                return hisStats;

            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }

            return hisStats;
        }
    }
}
