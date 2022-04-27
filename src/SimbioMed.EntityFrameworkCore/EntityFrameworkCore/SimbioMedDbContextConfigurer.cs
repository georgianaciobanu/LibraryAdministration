using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace SimbioMed.EntityFrameworkCore
{
    public static class SimbioMedDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<SimbioMedDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<SimbioMedDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}