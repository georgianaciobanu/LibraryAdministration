using Abp.IdentityServer4vNext;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimbioMed.Authorization.Delegation;
using SimbioMed.Authorization.Roles;
using SimbioMed.Authorization.Users;
using SimbioMed.Chat;
using SimbioMed.Editions;
using SimbioMed.Friendships;
using SimbioMed.MultiTenancy;
using SimbioMed.MultiTenancy.Accounting;
using SimbioMed.MultiTenancy.Payments;
using SimbioMed.Storage;

namespace SimbioMed.EntityFrameworkCore
{
    public class SimbioMedDbContext : AbpZeroDbContext<Tenant, Role, User, SimbioMedDbContext>, IAbpPersistedGrantDbContext
    {
        /* Define an IDbSet for each entity of the application */

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<Book.Book> Books { get; set; }
        public virtual DbSet<Category.Category> Categories { get; set; }
        public virtual DbSet<City.City> Cities { get; set; }
        public virtual DbSet<Store.Store> Stores { get; set; }
        public virtual DbSet<Author.Author> Authors { get; set; }
        public virtual DbSet<Publisher.Publisher> Publishers { get; set; }
        public virtual DbSet<Book.BookCategory> BookCategories { get; set; }
        public virtual DbSet<Sale.Sale> Sale { get; set; }
        public virtual DbSet<Sale.SaleDetail> SaleDetail { get; set; }
        public virtual DbSet<Sale.DiscountSale> DiscountSale { get; set; }

        public virtual DbSet<Customer.Customer> Customers { get; set; }
        public virtual DbSet<BookUnit.BookUnit> BookUnit { get; set; }
        public virtual DbSet<BookUnit.DiscountBook> DiscountBook { get; set; }

        public virtual DbSet<Discount.Discount> Discount { get; set; }




        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<PersistedGrantEntity> PersistedGrants { get; set; }

        public virtual DbSet<SubscriptionPaymentExtensionData> SubscriptionPaymentExtensionDatas { get; set; }

        public virtual DbSet<UserDelegation> UserDelegations { get; set; }

        public SimbioMedDbContext(DbContextOptions<SimbioMedDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BinaryObject>(b =>
            {
                b.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.FriendUserId });
                b.HasIndex(e => new { e.FriendTenantId, e.UserId });
                b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { PaymentId = e.ExternalPaymentId, e.Gateway });
            });

            modelBuilder.Entity<SubscriptionPaymentExtensionData>(b =>
            {
                b.HasQueryFilter(m => !m.IsDeleted)
                    .HasIndex(e => new { e.SubscriptionPaymentId, e.Key, e.IsDeleted })
                    .IsUnique();
            });

            modelBuilder.Entity<UserDelegation>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.SourceUserId });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId });
            });

            modelBuilder.ConfigurePersistedGrantEntity();
        }
    }
}
