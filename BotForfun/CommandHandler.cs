using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord.Commands;
using System.Reflection;

class CommandHandler
{
    DiscordSocketClient m_client;
    CommandService m_service;

    public async Task InitializeAsync(DiscordSocketClient client)
    {
        m_client = client;
        m_service = new CommandService();

        await m_service.AddModulesAsync(Assembly.GetEntryAssembly(), null);
        m_client.MessageReceived += HandleCommandAsync;
    }

    private async Task HandleCommandAsync(SocketMessage s)
    {
        var msg = s as SocketUserMessage;

        if (msg == null)
            return;

        var context = new SocketCommandContext(m_client, msg);
        int argPos = 0;

        if(msg.HasStringPrefix(Config.bot.cmdPrefix, ref argPos)
            || msg.HasMentionPrefix(m_client.CurrentUser, ref argPos))
        {
            var result = await m_service.ExecuteAsync(context, argPos, null);
            if(!result.IsSuccess && result.Error != CommandError.UnknownCommand)
            {
                Console.WriteLine(result.ErrorReason);
            }
        }
        return;
    }
}
