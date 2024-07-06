// See https://aka.ms/new-console-template for more information
using Eventing;
using Eventing.Example.Commands;

Console.WriteLine("Hello, World!");

var context = new InMemoryExecutionContext(System.Reflection.Assembly.GetExecutingAssembly(), null!, Guid.NewGuid().ToString());
await context.RunAsync(new CreateAccount("Test", "test@example.com"));
