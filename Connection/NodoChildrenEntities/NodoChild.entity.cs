using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Connection.NodoChildrenEntities
{
    [Table("nodo_children")]
    public partial class NodoChildEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("parent")]
        public int Parent { get; set; }
        [Column("title")]
        [StringLength(150)]
        public string Title { get; set; } = null!;
        [Column("created_at", TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }

        public static Action<EntityTypeBuilder<NodoChildEntity>> NodoChildBuilder = entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
        };

    }
}
