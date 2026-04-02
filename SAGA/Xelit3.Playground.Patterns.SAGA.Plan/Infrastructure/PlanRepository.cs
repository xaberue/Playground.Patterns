using Xelit3.Playground.Patterns.SAGA.Plans.Models;

namespace Xelit3.Playground.Patterns.SAGA.Plans.Infrastructure;

public class PlanRepository
{

    private readonly List<Plan> _data = new();


    public PlanRepository()
    {
        for (var i = 0; i < 30; i++)
        {
            for (var j = 0; j < 20; j++)
            {
                _data.Add(new Plan(i));
            }
        }
    }


    public Plan[] GetAll(int day)
    {
        return _data
            .Where(x => x.ExecutionDay == day)
            .ToArray();
    }
}
