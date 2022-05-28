namespace Library.Api.Infrastructure.Integrations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.Shared;
    using Domain.Shared.Core;

    public  class Dispatcher : IDispatcher
    {
        private readonly MessagesChannel _channel;

        public Dispatcher(MessagesChannel channel) => this._channel = channel;

        public async Task PublishAsync<T>(T message) where T : IDomainEvent
            => await this._channel.Writer.WriteAsync(message);

        public async Task PublishAsync<T>(IEnumerable<T> messages) where T : IDomainEvent
        {
            foreach (var message in messages)
            {
                await this.PublishAsync(message);
            }
        }
    }
}
