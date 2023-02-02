
using Connection.NodoChildrenEntities;
using Utils.Interfaces.Map;

namespace GraphService.NodoChild.Models
{
    public class NodoChildDTO : IMap<NodoChildEntity>
    {

        public int id { get; set; }
        public int parent { get; set; }
        public string title { get; set; }
        public DateTime? created_at { get; set; }

        public static readonly Func<NodoChildEntity, NodoChildDTO> singleSelect = childEntity => new NodoChildDTO(childEntity);

        public NodoChildDTO() { 
        
        }

        public NodoChildDTO(NodoChildEntity entity)
        {
            MapEntityToDto(entity);
        }


        public NodoChildEntity MapDtoToEntity()
        {
            return new NodoChildEntity()
            {
                Id = id,
                Parent = parent,
                Title = title
            };
        }

        public void MapEntityToDto(NodoChildEntity entity)
        {
            id = entity.Id;
            parent = entity.Parent;
            title = entity.Title;
            created_at = entity.CreatedAt;
        }
    }
}
