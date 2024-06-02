using examservice.Domain.Entities;
using examservice.Infrastructure.Data;
using examservice.Infrastructure.Interfaces;
using ExamService.Infrastructure.Bases;
using Microsoft.EntityFrameworkCore;

namespace examservice.Infrastructure.Repositories;

public class ModuleRepository : GenericRepositoryAsync<Module>, IModuleRepository
{
    private readonly DbSet<Module> _modules;
    public ModuleRepository(ApplicationDbContext context) : base(context)
    {
        _modules = context.Set<Module>();
    }

    public void DetachQuestions(Module module)
    {
        foreach (var moduleQuestion in module.ModuleQuestions)
        {
            var question = moduleQuestion.Question;

            // Detach the question if already tracked
            var trackedQuestion = _context.ChangeTracker.Entries<Question>()
                .FirstOrDefault(e => e.Entity.Id == question.Id);
            if (trackedQuestion != null)
            {
                _context.Entry(trackedQuestion.Entity).State = EntityState.Detached;
            }

            // Attach the question to the context
            _context.Entry(question).State = EntityState.Unchanged;

            foreach (var option in question.Options)
            {
                // Detach the option if already tracked
                var trackedOption = _context.ChangeTracker.Entries<Option>()
                    .FirstOrDefault(e => e.Entity.Id == option.Id);
                if (trackedOption != null)
                {
                    _context.Entry(trackedOption.Entity).State = EntityState.Detached;
                }

                // Attach the option to the context
                _context.Entry(option).State = EntityState.Unchanged;
            }
        }
    }

}
