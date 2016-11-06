using Discord;
using Discord.Commands;
using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EZBotWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public class Bot
        {
            public static bool SayCmd = false;
            public static bool CatCmd = true;
            private DiscordClient _client;

            public void Start(string token, char prefix)
            {
                _client = new DiscordClient();
                _client.UsingCommands(x =>
                {
                    x.PrefixChar = prefix;
                    x.HelpMode = HelpMode.Public;
                });

                _client.GetService<CommandService>().CreateCommand("say")
    .Description("Say message [msg]")
    .Parameter("msg", ParameterType.Required)
    .Do(async e =>
    {
        if (SayCmd == true)
        {
            await e.Channel.SendMessage(e.GetArg("msg"));
        }
        else
        {
            await e.Channel.SendMessage("Error: The bot owner has disabled the say command in the EZBot control panel.");
        }
    });

                _client.GetService<CommandService>().CreateCommand("cat")
                    .Description("Adorable cats! Meow!")
                    .Do(async e =>
                    {
                        if (CatCmd == true)
                        {
                            WebRequest request = WebRequest.Create("http://random.cat/meow");
                            WebResponse response = request.GetResponse();
                            Stream dataStream = response.GetResponseStream();
                            StreamReader reader = new StreamReader(dataStream);
                            string responseFromServer = reader.ReadToEnd();
                            dynamic catparsed = JsonConvert.DeserializeObject(responseFromServer);
                            string file = catparsed.file;
                            await e.Channel.SendMessage(file);
                        }
                        else
                        {
                            await e.Channel.SendMessage("Error: The bot owner has disabled the cat command in the EZBot control panel.");
                        }
                    });
                _client.MessageReceived += (s, e) =>
                        {

                            Console.WriteLine("New message: " + e.Message.Text);
                        };
                _client.ExecuteAndWait(async () =>
                {
                    await _client.Connect(token, TokenType.Bot);
                });
            }
        }
    }
}
