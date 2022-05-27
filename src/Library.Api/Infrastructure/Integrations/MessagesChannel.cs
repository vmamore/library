namespace Library.Api.Infrastructure.Integrations;

using System.Threading.Channels;
using Domain.Core;

public sealed class MessagesChannel
{
    private static Channel<IDomainEvent> _channel = Channel.CreateUnbounded<IDomainEvent>();

    public ChannelReader<IDomainEvent> Reader => _channel.Reader;
    public ChannelWriter<IDomainEvent> Writer => _channel.Writer;
}
