using Microsoft.AspNetCore.Mvc;
using Xelit3.Playground.Patterns.SAGA.Plans.Infrastructure;
using Xelit3.Playground.Patterns.SAGA.Plans.Models;

namespace Xelit3.Playground.Patterns.SAGA.Plans.Modules.GetCurrentPlans;

[ApiController]
[Route("plans")]
public class GetTodayPlans : ControllerBase
{

    private readonly PlanRepository _planRepository;


    public GetTodayPlans(PlanRepository planRepository)
    {
        _planRepository = planRepository;
    }


    [HttpGet("today")]
    public Plan[] Get()
    {
        var day = DateTime.Now.Day;

        return _planRepository.GetAll(day);
    }
}
