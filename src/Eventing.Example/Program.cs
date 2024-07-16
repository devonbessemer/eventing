// See https://aka.ms/new-console-template for more information
using Eventing;
using Eventing.Example.Commands;
using Eventing.Example.Events;

Console.WriteLine("Hello, World!");

var context = new InMemoryExecutionContext(System.Reflection.Assembly.GetExecutingAssembly(), null!, Guid.NewGuid().ToString());
await context.PublishAsync(new SignupFormSubmitted("Test", "test@example.com"));
