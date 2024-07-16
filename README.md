# Eventing POC

## Goals:

1. Enable a complex system design that always handles events in a consistent manner.
2. Make commands and events easily discoverable and easy to diagram.
3. Simplify debugging of complex workflows compared to other eventing frameworks.

## Definitions

Events
- An event is a message that indicates something that has happened
- All events are considered domain events
- Domain events can optionally be integration events.  When an integration event is published, it should be published on a message bus that provides cross-domain support.
- Events are not handled directly but can use used to trigger 1 or more actions via requests

Requests
- A request is a message that indicates an action should be performed

Actions (Commands)
- An action is an automated task to perform part of a business process.
- While performing a task, actions should usually publish events.

Triggers
- Triggers are used as the start of workflows.  
- These can be HTTP Requests (Webhooks), Messages Published to a Queue, Database Row Added, Email Received
- These can be setup as a Push Process or Monitors with a polling process (ex. Checking an email inbox every 5 minutes)

Workflows
- Workflows define a trigger and any events that directly occur as a result.
- These events can be used to trigger other actions


## Basic Workflow

1. A trigger is activated causing an event to be published
2. Event subscriptions send requests for actions (commands) to be performed
3. Actions perform tasks and publish new events
4. Repeat Step 2 and 3 until no new actions are requested.

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