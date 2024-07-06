using Eventing.Example.Events;

namespace Eventing.Example.Commands;

public record CreateAccount(string Username, string EmailAddress) : Command<AccountCreated>
{
    public override Task RunAsync(IExecutionContext context)
    {
        PublishAsync(context, new AccountCreated(Username, EmailAddress));

        return Task.CompletedTask;
    }
}