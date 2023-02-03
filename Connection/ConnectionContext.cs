using Connection.NodoChildrenEntities;
using Connection.NodoFatherEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Connection
{
    public class ConnectionContext : DbContext
    {

        public DbSet<NodoFatherEntity> nodoFatherEntities { get; set; }
        public DbSet<NodoChildEntity> nodoChildEntities { get; set; }

        private readonly Action<EntityTypeBuilder<NodoFatherEntity>> _NodoFatherBuilder = NodoFatherEntity.NodoFatherBuilder;
        private readonly Action<EntityTypeBuilder<NodoChildEntity>> _NodoChildBuilder = NodoChildEntity.NodoChildBuilder;

        public ConnectionContext(DbContextOptions<ConnectionContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NodoFatherEntity>(_NodoFatherBuilder);
            modelBuilder.Entity<NodoChildEntity>(_NodoChildBuilder);
        }
    }
}
