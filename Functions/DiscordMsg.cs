using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.IO;
using System.Net;
using System.Threading;

namespace NoIntegrity.Functions
{
    public class DiscordMsg
    {
        public static void OnPlayerConnected_DMJoin(UnturnedPlayer player)
        {
                NoIntegrityConfiguration config = NoIntegrity.Instance.Configuration.Instance;

                string thisWebhook = config.discordPlayerConnectWebhook;

                string thisPlayer = player.CharacterName;
                string thisServerName = Provider.serverName.ToString();
                string thisServerMap = Provider.map.ToString();
                string thisServerID = SteamGameServer.GetSteamID().ToString();
                string thisBotName = config.discordPlayerStatesBotname;
                string thisMessage = $"{thisPlayer} has joined {thisServerName} - {thisServerMap}\\nConnect directly using this server code:  {thisServerID}";
                string thisContent = "{\"username\":\"" + thisBotName + "\",\"content\":\"" + thisMessage + "\"}";

            Thread thread = new Thread(() =>
            {
                sendDiscordWebhook(thisWebhook, thisContent);
            });
            thread.Start();
        }

        public static void OnPlayerDisconnected_DMLeft(UnturnedPlayer player)
        {
                NoIntegrityConfiguration config = NoIntegrity.Instance.Configuration.Instance;

                string thisWebhook = config.discordPlayerDisconnectWebhook;

                string thisPlayer = player.CharacterName;
                string thisServerName = Provider.serverName.ToString();
                string thisBotName = config.discordPlayerStatesBotname;
                string thisServerMap = Provider.map.ToString();
                string thisMessage = $"{thisPlayer} has left {thisServerName} - {thisServerMap}";
                string thisContent = "{\"username\":\"" + thisBotName + "\",\"content\":\"" + thisMessage + "\"}";

            Thread thread = new Thread(() =>
            {
                sendDiscordWebhook(thisWebhook, thisContent);
            });
            thread.Start();
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
