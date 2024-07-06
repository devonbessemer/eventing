using Eventing.Example.Events;

namespace Eventing.Example.Commands;

public record SendWelcomeEmail(string EmailAddress) : Command<WelcomeEmailSent>
{
    public static SendWelcomeEmail Create(IExecutionContext context, AccountCreated @event) 
        => new SendWelcomeEmail(@event.EmailAddress);

    public override Task RunAsync(IExecutionContext context)
    {
        Console.WriteLine($"Sending Welcome Email to {EmailAddress}..");
        PublishAsync(context, new WelcomeEmailSent(EmailAddress));

        return Task.CompletedTask;
    }
}