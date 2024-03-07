using Rocket.Core.Utils;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using Rocket.Unturned.Skills;

namespace NoIntegrity.Functions
{
    public class Skills
    {
        public static void OnPlayerConnected_MaximizeSkills(UnturnedPlayer player)
        {
            GiveMaxSkills(player);
            UnturnedChat.Say(player, "Your skills have been maximized.");
        }

        public static void OnPlayerRevive_MaximizeSkills(UnturnedPlayer player, UnityEngine.Vector3 position, byte angle)
        {
            GiveMaxSkills(player);
            UnturnedChat.Say(player, "Your skills have been maximized.");
        }

        public static void SetSkill(UnturnedPlayer player, UnturnedSkill skill, byte level)
        {
            player.SetSkillLevel(skill, level);
        }

        public static void GiveMaxSkills(UnturnedPlayer player)
        {
            SetSkill(player, UnturnedSkill.Overkill, 7);
            SetSkill(player, UnturnedSkill.Sharpshooter, 7);
            SetSkill(player, UnturnedSkill.Dexerity, 5);
            SetSkill(player, UnturnedSkill.Cardio, 5);
            SetSkill(player, UnturnedSkill.Exercise, 5);
            SetSkill(player, UnturnedSkill.Diving, 5);
            SetSkill(player, UnturnedSkill.Parkour, 5);

            SetSkill(player, UnturnedSkill.Sneakybeaky, 7);
            SetSkill(player, UnturnedSkill.Vitality, 5);
            SetSkill(player, UnturnedSkill.Immunity, 5);
            SetSkill(player, UnturnedSkill.Toughness, 5);
            SetSkill(player, UnturnedSkill.Strength, 5);
            SetSkill(player, UnturnedSkill.Warmblooded, 5);
            SetSkill(player, UnturnedSkill.Survival, 5);

            SetSkill(player, UnturnedSkill.Healing, 7);
            SetSkill(player, UnturnedSkill.Crafting, 3);
            SetSkill(player, UnturnedSkill.Outdoors, 5);
            SetSkill(player, UnturnedSkill.Cooking, 3);
            SetSkill(player, UnturnedSkill.Fishing, 5);
            SetSkill(player, UnturnedSkill.Agriculture, 7);
            SetSkill(player, UnturnedSkill.Mechanic, 5);
            SetSkill(player, UnturnedSkill.Engineer, 3);
        }

    }
}
