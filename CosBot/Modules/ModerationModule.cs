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
    [GroupAttribute(".")]
    public class ModerationModule : ModuleBase
    {

        [Alias("bean")]
        [Command("ban")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [Summary("Ban the specified user and preserve their messages")]
        public async Task Ban(IGuildUser u, [Remainder] string msg = null)
        {
            await (await u.CreateDMChannelAsync()).SendMessageAsync($"⛔️ **You have been BANNED from `{Context.Guild.Name}` server.**\nReason: {msg}");
            await Task.Delay(1000);

            await Context.Channel.SendMessageAsync($"⛔️ **Banned** user **{u.Username}** ID: `{u.Id}`");
            await Context.Guild.AddBanAsync(u);
        }

        [Alias("edgyban")]
        [Command("reap")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [Summary("Ban the specified user and delete their messages")]
        public async Task Reap(IGuildUser u, [Remainder] string msg = null)
        {
            await (await u.CreateDMChannelAsync()).SendMessageAsync($"⛔️ **You have been BANNED from `{Context.Guild.Name}` server.**\nReason: {msg}");
            await Task.Delay(1000);

            await Context.Channel.SendMessageAsync($"⛔️ **Banned** user **{u.Username}** ID: `{u.Id}`");
            await Context.Guild.AddBanAsync(u, 7);
        }

        [Alias("meme, kek, cuck")]
        [Command("kick")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [Summary("Kick the user from the server")]
        public async Task Kick(IGuildUser u, [Remainder] string msg = null)
        {
            await (await u.CreateDMChannelAsync()).SendMessageAsync($"⛔️ **You have been KICKED from `{Context.Guild.Name}` server.**\n⚖ Reason: {msg}");
            await Task.Delay(1000);

            await Context.Channel.SendMessageAsync($"⛔️ **Kicked** user **{u.Username}** ID: `{u.Id}`");
            await u.KickAsync();
        }
    }
}





//using CosBot.Enums;
//using Discord;
//using Discord.Commands;
//using Discord.Commands.Permissions.Levels;
//using Discord.Modules;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CosBot.Modules
//{
//    public class ModerationModule : IModule                                  // Inherit the Module interface
//    {
//        private ModuleManager _manager;                                     // Create variables for manager and client
//        private DiscordClient _client;

//        void IModule.Install(ModuleManager manager)
//        {
//            _manager = manager;                                             // Initiate variables
//            _client = manager.Client;

//            manager.CreateCommands("", cmd =>                               // Create commands with the manager
//            {
//                cmd.CreateCommand("reap")
//                    .Description("Ban the specified user and delete their messages")
//                    .MinPermissions((int)AccessLevel.Mod)
//                    .Parameter("user", ParameterType.Required)
//                    .Parameter("reason", ParameterType.Unparsed)
//                    .Do(async (e) =>
//                    {
//                        var user = e.GetArg("user");
//                        var reason = e.GetArg("reason");
//                        User u = null;

//                        if (!string.IsNullOrWhiteSpace(user))
//                        {
//                            u = e.Message.MentionedUsers.FirstOrDefault();
//                            await u.SendMessage($" **You have been BANNED from `/r/Anime_IRL` server.**\nReason: {reason}");
//                            await Task.Delay(1000);
//                            await e.Server.Ban(u, 7);
//                        }
//                    });
//                cmd.CreateCommand("ban")
//                    .Alias("bean")
//                    .Description("Ban the specified user without deleting message history")
//                    .MinPermissions((int)AccessLevel.Mod)
//                    .Parameter("user", ParameterType.Required)
//                    .Parameter("reason", ParameterType.Unparsed)
//                    .Do(async (e) =>
//                    {
//                        var user = e.GetArg("user");
//                        var reason = e.GetArg("reason");
//                        User u = null;

//                        if (!string.IsNullOrWhiteSpace(user))
//                        {
//                            u = e.Message.MentionedUsers.FirstOrDefault();
//                            await u.SendMessage($" **You have been BANNED from `/r/Anime_IRL` server.**\nReason: {reason}");
//                            await Task.Delay(1000);
//                            await e.Server.Ban(u);
//                        }
//                    });
//                cmd.CreateCommand("kick")
//                    .Alias("meme","kek","cuck")
//                    .Description("Kick the specified user")
//                    .MinPermissions((int)AccessLevel.Mod)
//                    .Parameter("user", ParameterType.Required)
//                    .Parameter("reason", ParameterType.Unparsed)
//                    .Do(async (e) =>
//                    {
//                        var user = e.GetArg("user");
//                        var reason = e.GetArg("reason");
//                        User u = null;

//                        if (!string.IsNullOrWhiteSpace(user))
//                        {
//                            u = e.Message.MentionedUsers.FirstOrDefault();
//                            await u.SendMessage($" **You have been KICKED from `/r/Anime_IRL` server.**\nReason: {reason}");
//                            await Task.Delay(1000);
//                            await u.Kick();
//                        }
//                    });
//            });
//        }
//    }
//}
