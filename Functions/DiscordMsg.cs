using Rocket.Unturned;
using Rocket.Unturned.Player;
using Rocket.Unturned.Serialisation;
using SDG.Unturned;
using Steamworks;
using System;
using System.IO;
using System.Net;

namespace NoIntegrity.Functions
{
    public class DiscordMsg
    {
        public static void OnPlayerConnected_DMJoin(UnturnedPlayer player)
        {
            NoIntegrityConfiguration config = NoIntegrity.Instance.Configuration.Instance;

            string thisWebhook = config.discordPlayerConnectWebhook;

            string thisPlayer = player.CharacterName;
            string thisServerIP = SteamGameServer.GetPublicIP().ToString();
            string thisServerPort = Provider.port.ToString();
            string thisServerName = Provider.serverName.ToString();
            string thisServerID = SteamGameServer.GetSteamID().ToString();
            string thisBotName = config.discordPlayerStatesBotname;
            string thisMessage = $"{thisPlayer} has joined {thisServerName}.\\nConnect directly using this server code:  {thisServerID}";
            string thisContent = "{\"username\":\"" + thisBotName + "\",\"content\":\"" + thisMessage + "\"}";

            sendDiscordWebhook(thisWebhook, thisContent);
        }

        public static void OnPlayerDisconnected_DMLeft(UnturnedPlayer player)
        {
            NoIntegrityConfiguration config = NoIntegrity.Instance.Configuration.Instance;

            string thisWebhook = config.discordPlayerDisconnectWebhook;

            string thisPlayer = player.CharacterName;
            string thisServerName = Provider.serverName.ToString();
            string thisBotName = config.discordPlayerStatesBotname;
            string thisMessage = $"{thisPlayer} has left {thisServerName}.";
            string thisContent = "{\"username\":\"" + thisBotName + "\",\"content\":\"" + thisMessage + "\"}";

            sendDiscordWebhook(thisWebhook, thisContent);
        }


        // Borrowed from https://github.com/xxfogs/Report-Discord
        private static void sendDiscordWebhook(string url, string escapedJson)
        {
            var WebReq = WebRequest.Create(url);
            WebReq.ContentType = "application/json";
            WebReq.Method = "POST";
            using (var StrWrt = new StreamWriter(WebReq.GetRequestStream())) StrWrt.Write(escapedJson);
            WebResponse Res = WebReq.GetResponse();
            StreamReader Reader = new StreamReader(Res.GetResponseStream());
            string str = Reader.ReadLine();
            while (str != null)
            {
                Console.WriteLine(str);
                str = Reader.ReadLine();
            }
            Reader.Close();
            Res.Close();
        }

        private static string discordDate()
        {
            return DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
