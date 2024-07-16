using Eventing.Example.Events;

namespace Eventing.Example.Commands;

public record CreateAccount(string Username, string EmailAddress) : Command<AccountCreated>
{
    public static CreateAccount Create(IExecutionContext context, SignupFormSubmitted @event)
        => new CreateAccount(@event.Username, @event.EmailAddress);

    public override Task RunAsync(IExecutionContext context)
    {
        System.Console.WriteLine($"Creating account for username: {Username}..");
        PublishAsync(context, new AccountCreated(Username, EmailAddress));

        return Task.CompletedTask;
    }
}