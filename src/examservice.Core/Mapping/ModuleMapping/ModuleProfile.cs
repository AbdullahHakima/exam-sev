using AutoMapper;

namespace examservice.Core.Mapping.ModuleMapping
{
    public partial class ModuleProfile : Profile
    {
        public ModuleProfile()
        {
            GenerateModuleMapping();
            ViewModuleQuestionsMapping();
        }
    }
}
