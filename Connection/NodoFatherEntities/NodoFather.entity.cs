using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connection.NodoFatherEntities
{
    [Table("nodo_father")]
    public partial class NodoFatherEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("title")]
        [StringLength(150)]
        public string Title { get; set; } = null!;
        [Column("created_at", TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }

        public static Action<EntityTypeBuilder<NodoFatherEntity>> NodoFatherBuilder = entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
        };
    }
}
