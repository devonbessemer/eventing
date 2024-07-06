using System.Collections.Immutable;
using System.Reflection;

namespace Eventing;

public interface IExecutionContext {
    public string CorrelationId { get; }
    public IServiceProvider Services { get; }
    public Task PublishAsync(object @event);
    public Task RunAsync(Command @command);
}

public record InMemoryExecutionContext(Assembly assembly, IServiceProvider Services, string CorrelationId) : IExecutionContext
{
    public async Task PublishAsync(object @event)
    {
        Console.WriteLine($"Publishing event {@event.GetType().Name}..");
        // Find all factory methods with the expected signature
        var commandFactories = FindCommandFactories(@event.GetType());

        // Execute the factory methods and run the command
        foreach(var commandFactory in commandFactories) {
            var command = commandFactory.Invoke(null, [this, @event]) as Command;
            if (command is not null) {
                await RunAsync(command);
            }
        }
    }

    public Task RunAsync(Command command)
    {
        Console.WriteLine($"Running command {command.GetType().Name}..");
        return command.RunAsync(this);
    }

    private ImmutableList<MethodInfo> FindCommandFactories(Type eventType)
    {
        return assembly.GetTypes()
            .SelectMany(x => x.GetMethods(BindingFlags.Static | BindingFlags.Public))
            .Where(x => x.GetParameters().Count() == 2)
            .Where(x => x.GetParameters().ToList()[0].ParameterType == typeof(IExecutionContext))
            .Where(x => x.GetParameters().ToList()[1].ParameterType == eventType)
            .Where(x => x.ReturnType.IsSubclassOf(typeof(Command)))
            .ToImmutableList();

    }
}