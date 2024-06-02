using examservice.API.Base;
using examservice.Core.Features.Modules.Commands.Models.Add;
using examservice.Domain.Helpers.Dtos.Module;
using examservice.Domain.MetaData;
using Microsoft.AspNetCore.Mvc;

namespace examservice.API.Controllers;

[ApiController]
public class ModuleController : ApplicationController
{
    [HttpPost(Router.ModuleReouting.GenerateModuels)]
    public async Task<IActionResult> GenerateModules([FromRoute] Guid courseId, [FromRoute] Guid instructorId, [FromForm] GenerateModuleDto moduleDto)
    {
        var response = await Mediator.Send(new GenerateModulesCommandModel
        {
            moduleDto = moduleDto,
            courseId = courseId,
            instructorId = instructorId
        });
        return NewResult(response);
    }
}

