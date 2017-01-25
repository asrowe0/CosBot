using CosBot.Enums;
using CosBot.Modules;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosBot
{
    public class CosBot
    {
        private DiscordSocketClient client;
        private CommandHandler handler;

        public static void Main(string[] args) =>
            new CosBot().Start().GetAwaiter().GetResult();

        public async Task Start()
        {
            client = new DiscordSocketClient(
                new DiscordSocketConfig()
                {
                    LogLevel = LogSeverity.Info
                });

            client.Log += Log;

            var token = "token";

            var map = new DependencyMap();
            map.Add(client);

            handler = new CommandHandler();
            await handler.Install(map);

            await client.LoginAsync(TokenType.Bot, token);
            await client.ConnectAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}


//namespace CosBot
//{
//    class CosBot
//    {
        
//        DiscordClient _client;
//        CommandService commands;

//        static void Main(string[] args) => new CosBot();

//        public CosBot()
//        {
//            _client = new DiscordClient(x =>
//            {
//                x.LogLevel = LogSeverity.Info;
//                x.LogHandler = Log;              
//            })
//            .UsingCommands(x =>
//            {
//                x.PrefixChar = '!';
//                x.AllowMentionPrefix = true;
//                x.HelpMode = HelpMode.Public;
//            })
//            .UsingPermissionLevels((u, c) => (int)GetPermission(u, c))
//            .UsingModules();

//            CreateCommands();
//            BindEvents();
//            _client.AddModule<ModerationModule>("Moderation", ModuleFilter.None);


//            _client.ExecuteAndWait(async () =>
//            {
//                while (true)
//                {
//                    try
//                    {
//                        await _client.Connect("MjczNDg1MDI3MTY4MzU0MzA1.C2kOjg.KRoEwdTSpkpoFKhZeExhFldfZls", TokenType.Bot);
//                        break;
//                    }
//                    catch (Exception ex)
//                    {
//                        _client.Log.Error("Login Failed", ex);
//                        await Task.Delay(_client.Config.FailedReconnectDelay);
//                    }
//                    await _client.Connect("", TokenType.Bot);
//                }
//            });
//        }


//        public void CreateCommands()
//        {
//            commands = _client.GetService<CommandService>();

//            commands.CreateCommand("msg")
//                .Do(async (e) => 
//                {
//                    await e.Channel.SendMessage("_y'all cuckin'?_");
//                });

//            commands.CreateCommand("yeet")
//                .Do(async (e) =>
//                {
//                    var channel = e.Server.FindChannels("yeet", ChannelType.Text).FirstOrDefault();
//                    await channel.SendMessage($"{e.User.Mention} your yeet is ready: \n" +
//                        "https://cdn.discordapp.com/attachments/273524183361781761/273524636585558017/yVdxXqeCoWs2BuZuDVrhxOqWhUth-iUv4C7rRHy-8p168f-5z35qod7XyARqbUQjX7DNi5ffukUrku5pdfCmsAw506-h380.png");
//                });
//            commands.CreateCommand("berned")
//                .Do(async (e) =>
//                {
//                    await e.Channel.SendMessage("https://cdn.discordapp.com/attachments/273485227433656320/273602339498360832/PicsArt_01-20-11.19.50.jpg");
//                });
//            commands.CreateCommand("roll")
//                .Description("Choose a random number from 1 to the specified value")
//                .Parameter("bound",ParameterType.Required)
//                .Do(async (e) =>
//                {
//                    Random r = new Random();
//                    var val = r.Next(Convert.ToInt32(e.GetArg("bound")));
//                    await e.Channel.SendMessage($"{e.User.Mention} rolled a **{val}**.");
//                });
//            commands.CreateCommand("iam")
//                .Description("Assign a role to yourself (based on permission level)")
//                .MinPermissions((int)AccessLevel.User)
//                .Parameter("role", ParameterType.Unparsed)
//                .Do(async (e) =>
//                {
//                    var toGive = e.Args[0];
//                    var roles = e.Server.Roles;
//                    if (toGive != "mod" || toGive != "bot" || toGive != "Owner")
//                    {
//                        foreach (Role r in roles)
//                        {
//                            if (r.Name == toGive)
//                            {
//                                await e.User.AddRoles(r);
//                                await e.Channel.SendMessage($":ok: You now have the {r.Name} role!");
//                            }
//                        }
//                    }
//                });
//            commands.CreateCommand("iamnot")
//                .Description("Remove a role from yourself (based on permission level)")
//                .MinPermissions((int)AccessLevel.User)
//                .Parameter("role", ParameterType.Unparsed)
//                .Do(async (e) =>
//                {
//                    var toRemove = e.Args[0];
//                    var roles = e.Server.Roles;
//                    if (toRemove != "mod" || toRemove != "bot" || toRemove != "Owner")
//                    {
//                        foreach (Role r in roles)
//                        {
//                            if (r.Name == toRemove)
//                            {
//                                await e.User.RemoveRoles(r);
//                                await e.Channel.SendMessage($":ok: Sucessfully removed the {r.Name} role from you.");
//                            }
//                        }
//                    }
//                });
//        }

//        public void BindEvents()
//        {
//            _client.UserJoined += async (s, e) =>
//            {
//                var channel = e.Server.FindChannels("welcome-center", ChannelType.Text).FirstOrDefault();
//                var channel2 = e.Server.FindChannels("server-logs", ChannelType.Text).FirstOrDefault();
//                var user = e.User;
//                var userId = e.User.Id;
//                var userName = e.User.Name;

//                await channel.SendMessage(string.Format("Hello <@{0}>, welcome to the server. You must read our #rules-board channel to get started.", userId));
//                await channel2.SendMessage(string.Format("【{0}】:white_check_mark: `User Joined:` **{1}** ({2})", DateTime.Now.ToString("hh:mm:ss"), userName, userId));
//            };

//            _client.UserLeft += async (s, e) =>
//            {
//                var channel = e.Server.FindChannels("server-logs", ChannelType.Text).FirstOrDefault();
//                var user = e.User;
//                var userId = e.User.Id;
//                var userName = e.User.Name;

//                await channel.SendMessage(string.Format("【{0}】:exclamation:`User left:` **{1}** ({2})", DateTime.Now.ToString("hh:mm:ss"), userName, userId));
//            };
//        }

//        private AccessLevel GetPermission(User user, Channel channel)
//        {

//            if (user.IsBot)                                         //Prevent other bots from executing commands
//                return AccessLevel.Blocked;

//            if (user.Roles.Where(x => x.Name == "Owner").Any())     //Server owner
//                return AccessLevel.Owner;
  
//            if (user.Roles.Where(x => x.Name == "Admin").Any())    //Server admin
//                return AccessLevel.Admin;

//            if (user.Roles.Where(x => x.Name == "Mod").Any())      //Server moderator       
//                return AccessLevel.Mod;

//            if (user.Roles.Where(x => x.Name == "TimeOut").Any())  //Timed out
//                return AccessLevel.TimeOut;

//            return AccessLevel.User;                            
//        }

//        private void Log(object sender, LogMessageEventArgs e)
//        {
//            Console.WriteLine($"[{e.Severity}] {e.Source}: {e.Message}");
//        }
        
//    }
//}
