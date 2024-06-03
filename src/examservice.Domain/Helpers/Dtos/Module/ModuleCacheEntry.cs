namespace examservice.Domain.Helpers.Dtos.Module
{
    public class ModuleCacheEntry
    {
        public List<examservice.Domain.Entities.Module> Modules { get; set; }
        public bool? IsSaved { get; set; }
    }
}
