using Rocket.API;

namespace NoIntegrity
{
    public class NoIntegrityConfiguration : IRocketPluginConfiguration
    {
        public string LoadMessage { get; set; }

        public string DBHost;
        public int DBPort;
        public string DBName;
        public string DBTablePrefix;
        public string DBUsername;
        public string DBPassword;

        public bool isCreative;

        public int slotsMaxSlots;

        public bool maxSkillsAuto;

        public string statsPrefix;
        public bool statsEnable;

        public bool discordPlayerStates;
        public string discordPlayerStatesBotname;
        public string discordPlayerConnectWebhook;
        public string discordPlayerDisconnectWebhook;

        public void LoadDefaults()
        {
            DBHost = "127.0.0.1";
            DBPort = 3306;
            DBName = "myDBName";
            DBTablePrefix = "myDBTablePrefix";
            DBUsername = "myUsername";
            DBPassword = "myPassword";

            isCreative = false;

            slotsMaxSlots = 3;

            maxSkillsAuto = true;

            statsPrefix = "Unturned00";
            statsEnable = false;

            discordPlayerStates = false;
            discordPlayerStatesBotname = "My Discord Bot Name";
            discordPlayerConnectWebhook = "https://your.discord.webhook.here";
            discordPlayerDisconnectWebhook = "https://your.discord.webhook.here";
        }
    }
}
