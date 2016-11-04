using System.Data.Entity;

namespace DatabaseAuthenticator
{
    public partial class BasicTokenDbContext : DbContext
    {
        public BasicTokenDbContext()
            : base("name=BasicTokenDbConnection")
        {
        }

        public virtual DbSet<Token> Tokens { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.Tokens)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
