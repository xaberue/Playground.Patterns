namespace Xelit3.Playground.Patterns.SAGA.Plans.Models;

public record Plan
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int ExecutionDay { get; set; }


    public Plan(int executionDay)
    {
        Id = Guid.NewGuid();
        Name = $"Plan-{Id}";
        ExecutionDay = executionDay;
    }

}