using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDG.Unturned;
using Rocket.Unturned.Items;

namespace NoIntegrity.Commands
{
    public class CMD_Ammo : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "ammo";

        public string Help => "This command provides ammo for the currently held weapon.  Provide a number after /ammo to get that number of items for your weapon.";

        public string Syntax => "<ammount of ammo>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string> { "ammo" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            NoIntegrityConfiguration config = NoIntegrity.Instance.Configuration.Instance;
            ushort ammoAmountToSpawn = 0;
            var enteredAmount = false;
            SDG.Unturned.ItemGunAsset currentWeapon;
            SDG.Unturned.ItemAsset currentEquipped;
            UnturnedPlayer player = (UnturnedPlayer)caller;

            if(command.Length >= 1)
            {
                if (ushort.TryParse(command[0], out ammoAmountToSpawn))
                {
                    enteredAmount = true;
                }
            }
            else
            {
                UnturnedChat.Say(caller, Help);
            }

            currentEquipped = player.Player.equipment.asset;
            if (currentEquipped == null)
            {
                UnturnedChat.Say(caller, "You have no weapon equipped.");
                return;
            }
            if (currentEquipped.type != SDG.Unturned.EItemType.GUN)
            {
                UnturnedChat.Say(caller, "You have no weapon equipped.");
                return;
            }

            currentWeapon = (SDG.Unturned.ItemGunAsset)currentEquipped;
            if (currentWeapon == null)
            {
                UnturnedChat.Say(caller, "You have no weapon equipped.");
                return;
            }

            if(enteredAmount)
            {
                if (ammoAmountToSpawn > 0)
                {
                    if (ammoAmountToSpawn <= config.ammoClipLimit)
                    {
                        SpawnMags((ushort)ammoAmountToSpawn, caller, currentWeapon, player, command);
                    }
                    else if (ammoAmountToSpawn > config.ammoClipLimit)
                    {
                        SpawnMags((ushort)config.ammoClipLimit, caller, currentWeapon, player, command);
                    }
                    else
                    {
                        SpawnMags((ushort)1, caller, currentWeapon, player, command);
                    }
                }
                else
                {
                    SpawnMags((ushort)1, caller, currentWeapon, player, command);
                }
            }
        }

        public void SpawnMags(ushort ammoAmountToSpawn, IRocketPlayer caller, ItemGunAsset currentWeapon,
            UnturnedPlayer uPlayer, string[] command)
        {
            if (uPlayer.GiveItem(GetMagId(uPlayer, currentWeapon, command), (byte)ammoAmountToSpawn))
            {
                UnturnedChat.Say(caller, "Spawning ammo.");
            }
            else
            {
                UnturnedChat.Say(caller, "Unable to spawn ammo.");
            }
        }

        public ushort GetMagId(UnturnedPlayer player, ItemGunAsset gun, string[] command)
        {
            ushort magId = 0;

            if (command.Length == 2 || command.Length == 1)
            {
                if ((command.Length == 1 && command[0].ToLower() == "c") ||
                    (command.Length == 2 && command[1].ToLower() == "c"))
                {
                    magId = player.Player.equipment.state[8];
                }
            }

            if (magId == 0 || UnturnedItems.GetItemAssetById(magId).type != EItemType.MAGAZINE)
            {
                magId = gun.getMagazineID();
            }

            return magId;
        }
    }
}
