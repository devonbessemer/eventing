namespace Eventing;

public abstract record Command
{
    public abstract Task RunAsync(IExecutionContext context);
}

public abstract record Command<TPublishedEvent1> : Command
    where TPublishedEvent1 : notnull
{
    public Task PublishAsync(IExecutionContext context, TPublishedEvent1 @event) 
        => context.PublishAsync(@event);
}

public abstract record Command<TPublishedEvent1, TPublishedEvent2> : Command
    where TPublishedEvent1 : notnull
    where TPublishedEvent2 : notnull
{
    public Task PublishAsync(IExecutionContext context, TPublishedEvent1 @event) 
        => context.PublishAsync(@event);

    public Task PublishAsync(IExecutionContext context, TPublishedEvent2 @event) 
        => context.PublishAsync(@event);
}