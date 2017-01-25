using CosBot.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
    class SelfAssignRolesModule : ModuleBase
    {
        private static string _rolesFile = ConfigurationManager.AppSettings["RolesFile"];
        private List<string> _roles = File.ReadAllLines(_rolesFile).ToList();

        [Command("iam")]
        [Summary("Add a role to yourself")]
        public async Task Iam([Remainder] IRole role)
        {
            IGuildUser u = null;
            foreach (string r in _roles)
            {
                if(role.Name == r)
                {
                    u = (IGuildUser)Context.User;
                    await u.AddRolesAsync(role);
                    var msg = await Context.Channel.SendMessageAsync($"🆗 You now have **{role.Name}** role.").ConfigureAwait(false);

                    msg.DeleteAfter(3);
                    Context.Message.DeleteAfter(3);
                }                       
            }
            if (u == null)
            {
                var msg = await Context.Channel.SendMessageAsync("That role is not self-assignable.");

                msg.DeleteAfter(3);
                Context.Message.DeleteAfter(3);
            }
        }

        [Command("iamnot")]
        [Summary("Remove a role from yourself")]
        public async Task Iamnot([Remainder] IRole role)
        {
            IGuildUser u = null;
            foreach (string r in _roles)
            {
                //if role is in the self-assignable list
                if (role.Name == r)
                {
                    u = (IGuildUser)Context.User;
                    var roles = u.GetRoles();
                    
                    //if user has role
                    if(roles.Select(x => x.Name == role.Name).Any())
                    {
                        await u.RemoveRolesAsync(role);
                        var msg = await Context.Channel.SendMessageAsync($"🆗 You no longer have **{role.Name}** role.").ConfigureAwait(false);

                        msg.DeleteAfter(3);
                        Context.Message.DeleteAfter(3);
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync($"❎ You don't have **{role.Name}** role.");
                    }                   
                }
            }
            if (u == null)
            {
                var msg = await Context.Channel.SendMessageAsync("❎ You may not remove this role.");

                msg.DeleteAfter(3);
                Context.Message.DeleteAfter(3);
            }
        }

        [Command("asar")]
        [Summary("Add a role to the self assignable list")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task Asar([Remainder] IRole role)
        {
            string msg;

            if (_roles.Any(x => x == role.Name))
            {
                msg = $"💢 Role **{role.Name}** is already in the list.";
            }
            else
            {
                using (StreamWriter w = new StreamWriter(_rolesFile)) w.WriteLine(role.Name);

                msg = $"🆗 Role **{role.Name}** added to the list.";
            }
            await Context.Channel.SendMessageAsync(msg.ToString()).ConfigureAwait(false);
        }

        [Command("rsar")]
        [Summary("Remove a role from the self assignable list")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task Rsar([Remainder] IRole role)
        {
            string msg;

            if (_roles.Any(x => x == role.Name))
            {
                _roles.Remove(role.Name);

                msg = $"🆗 Role **{role.Name}** removed from the list.";               
            }
            else
            {
                msg = $"💢 Role **{role.Name}** is not in the list.";
            }
            await Context.Channel.SendMessageAsync(msg.ToString()).ConfigureAwait(false);
        }
    }
}
