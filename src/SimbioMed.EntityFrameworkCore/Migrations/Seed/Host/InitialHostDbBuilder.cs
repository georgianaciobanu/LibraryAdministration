using SimbioMed.EntityFrameworkCore;

namespace SimbioMed.Migrations.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly SimbioMedDbContext _context;

        public InitialHostDbBuilder(SimbioMedDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
