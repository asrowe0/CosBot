using CosBot.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace CosBot.Modules
{
    [GroupAttribute(">")]
    public class MemeModule : ModuleBase
    {
        [Command("berned")]
        [Summary("Bernie would've won")]
        public async Task Berned()
        {
            await Context.Channel.SendMessageAsync("http://i.imgur.com/qSWdNnI.jpg");
        }

        [Command("yeet")]
        [Summary("Y'all mind if I hit that?")]
        public async Task Yeet()
        {
            await Context.Channel.SendMessageAsync("http://i.imgur.com/mdJveVn.png");
        }

        [Command("roll")]
        [Summary("Choose a random number from 1 to the specified value")]
        public async Task Roll(int bound)
        {
            Random r = new Random();
            var val = r.Next(Convert.ToInt32(bound));
            await Context.Channel.SendMessageAsync($"{Context.User.Mention} rolled a **{val}**.");
        }

    }
}

//commands.CreateCommand("msg")
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