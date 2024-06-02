using examservice.Core.Bases;
using examservice.Domain.Helpers.Dtos.Module;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace examservice.Core.Features.Modules.Commands.Models.Add
{
    public class GenerateModulesCommandModel : IRequest<Response<List<ViewGeneratedModuleDto>>>
    {
        [FromForm]
        public GenerateModuleDto moduleDto { get; set; }
        [FromRoute]
        public Guid instructorId { get; set; }
        [FromRoute]
        public Guid courseId { get; set; }

    }
}
