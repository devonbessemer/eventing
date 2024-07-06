# Eventing POC

## Goals:

1. Enable a complex system design that always handles events in a consistent manner.
2. Make commands and events easily discoverable and easy to diagram.
3. Simplify debugging of complex workflows compared to other eventing frameworks.

## Basic Flow

- An initial command or event occurs
- A command is run to perform a single workflow and publish events
- Events are handled by creating new commands

## Discoverability

- Commands can extend a Command<TPublishedEvent> abstract class, this provides two benefits:
1. It can be easily discovered which events are produced from a command workflow.
2. A `PublishAsync` method for the event is made available to the workflow.

- Commands can define a static factory method for creating a command from an event.  This allows for easy discoverability of how events are handled within a system.

```c#
    public static SendWelcomeEmail Create(IEventingContext context, AccountCreated @event) 
        => new SendWelcomeEmail(@event.EmailAddress);
```

## Execution Contexts

Execution contexts currently solve for three concerns:

1. Providing a strategy for how to execute commands and publish events.  (In Memory, Queues, Topics, etc.)
2. Providing access to services from an IoC container when handling commands
3. Correlating various actions of a larger workflow