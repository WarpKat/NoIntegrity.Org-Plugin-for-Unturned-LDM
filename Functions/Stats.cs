using Rocket.Core.Utils;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;

namespace NoIntegrity.Functions
{
    public class Stats
    {
        public static void UpdateStats_OnPlayerDeath(UnturnedPlayer player, EDeathCause cause, ELimb limb, CSteamID murderer)
        {

            string hisKillerSteamID = murderer.ToString();
            string hisSteamID = player.CSteamID.ToString();
            string death = cause.ToString();

            try
            {
                Rocket.Core.Logging.Logger.Log($"Murderer:  {hisKillerSteamID}");
                Rocket.Core.Logging.Logger.Log($"Cause of death:  {death}");

                TaskDispatcher.RunAsync(() =>
                {
                    switch (death)
                    {
                        case "BLEEDING":
                            DBHandler.dbUpdateStats(hisSteamID, "dBleeding");
                            break;
                        case "BONES":
                            DBHandler.dbUpdateStats(hisSteamID, "dBones");
                            break;
                        case "FREEZING":
                            DBHandler.dbUpdateStats(hisSteamID, "dFreezing");
                            break;
                        case "BURNING":
                            DBHandler.dbUpdateStats(hisSteamID, "dBurning");
                            break;
                        case "FOOD":
                            DBHandler.dbUpdateStats(hisSteamID, "dFood");
                            break;
                        case "WATER":
                            DBHandler.dbUpdateStats(hisSteamID, "dWater");
                            break;
                        case "GUN":
                            DBHandler.dbUpdateStats(hisSteamID, "dGun");
                            if (limb == ELimb.SKULL)
                            {
                                DBHandler.dbUpdateStats(hisKillerSteamID, "kHeadshot");
                            }
                            else
                            {
                                DBHandler.dbUpdateStats(hisKillerSteamID, "kKill");
                            }
                            break;
                        case "MELEE":
                            DBHandler.dbUpdateStats(hisSteamID, "dMelee");
                            if (limb == ELimb.SKULL)
                            {
                                DBHandler.dbUpdateStats(hisKillerSteamID, "kHeadshot");
                            }
                            else
                            {
                                DBHandler.dbUpdateStats(hisKillerSteamID, "kKill");
                            }
                            break;
                        case "ZOMBIE":
                            DBHandler.dbUpdateStats(hisSteamID, "dZombie");
                            break;
                        case "ANIMAL":
                            DBHandler.dbUpdateStats(hisSteamID, "dAnimal");
                            break;
                        case "KILL":
                            DBHandler.dbUpdateStats(hisSteamID, "dKill");
                            break;
                        case "INFECTION":
                            DBHandler.dbUpdateStats(hisSteamID, "dInfection");
                            break;
                        case "PUNCH":
                            DBHandler.dbUpdateStats(hisSteamID, "dPunch");
                            if (limb == ELimb.SKULL)
                            {
                                DBHandler.dbUpdateStats(hisKillerSteamID, "kHeadshot");
                            }
                            else
                            {
                                DBHandler.dbUpdateStats(hisKillerSteamID, "kKill");
                            }
                            break;
                        case "BREATH":
                            DBHandler.dbUpdateStats(hisSteamID, "dBreath");
                            break;
                        case "ROADKILL":
                            DBHandler.dbUpdateStats(hisSteamID, "dRoadkill");
                            break;
                        case "VEHICLE":
                            DBHandler.dbUpdateStats(hisSteamID, "dVehicle");
                            break;
                        case "GRENADE":
                            DBHandler.dbUpdateStats(hisSteamID, "dGrenade");
                            if (hisKillerSteamID != hisSteamID)
                            {
                                if (limb == ELimb.SKULL)
                                {
                                    DBHandler.dbUpdateStats(hisKillerSteamID, "kHeadshot");
                                }
                                else
                                {
                                    DBHandler.dbUpdateStats(hisKillerSteamID, "kKill");
                                }
                            }
                            break;
                        case "SHRED":
                            DBHandler.dbUpdateStats(hisSteamID, "dShred");
                            break;
                        case "LANDMINE":
                            DBHandler.dbUpdateStats(hisSteamID, "dLandmine");
                            break;
                        case "ARENA":
                            DBHandler.dbUpdateStats(hisSteamID, "dArena");
                            break;
                        case "SUICIDE":
                            DBHandler.dbUpdateStats(hisSteamID, "dSuicide");
                            break;
                        case "MISSILE":
                            DBHandler.dbUpdateStats(hisSteamID, "dMissile");
                            if (hisKillerSteamID != hisSteamID)
                            {
                                if (limb == ELimb.SKULL)
                                {
                                    DBHandler.dbUpdateStats(hisKillerSteamID, "kHeadshot");
                                }
                                else
                                {
                                    DBHandler.dbUpdateStats(hisKillerSteamID, "kKill");
                                }
                            }
                            break;
                        case "CHARGE":
                            DBHandler.dbUpdateStats(hisSteamID, "dCharge");
                            if (hisKillerSteamID != hisSteamID)
                            {
                                if (limb == ELimb.SKULL)
                                {
                                    DBHandler.dbUpdateStats(hisKillerSteamID, "kHeadshot");
                                }
                                else
                                {
                                    DBHandler.dbUpdateStats(hisKillerSteamID, "kKill");
                                }
                            }
                            break;
                        case "SPLASH":
                            DBHandler.dbUpdateStats(hisSteamID, "dSplash");
                            break;
                        case "SENTRY":
                            DBHandler.dbUpdateStats(hisSteamID, "dSentry");
                            if (hisKillerSteamID != hisSteamID)
                            {
                                if (limb == ELimb.SKULL)
                                {
                                    DBHandler.dbUpdateStats(hisKillerSteamID, "kHeadshot");
                                }
                                else
                                {
                                    DBHandler.dbUpdateStats(hisKillerSteamID, "kKill");
                                }
                            }
                            break;
                        case "ACID":
                            DBHandler.dbUpdateStats(hisSteamID, "dAcid");
                            break;
                        case "BOULDER":
                            DBHandler.dbUpdateStats(hisSteamID, "dBoulder");
                            break;
                        case "BURNER":
                            DBHandler.dbUpdateStats(hisSteamID, "dBurner");
                            break;
                        case "SPIT":
                            DBHandler.dbUpdateStats(hisSteamID, "dSpit");
                            break;
                        case "SPARK":
                            DBHandler.dbUpdateStats(hisSteamID, "dSpark");
                            break;
                        default:
                            DBHandler.dbUpdateStats(hisSteamID, "dOther");
                            Rocket.Core.Logging.Logger.Log("Flag of OTHER given.");
                            break;
                    }
                });
            }
            catch (Exception exc)
            {
                Rocket.Core.Logging.Logger.LogException(exc);
            }
        }
    }
}
