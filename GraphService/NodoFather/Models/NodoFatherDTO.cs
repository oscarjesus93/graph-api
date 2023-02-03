using Connection.NodoFatherEntities;
using Utils.Interfaces.Map;

namespace GraphService.NodoFather.Models
{
    public class NodoFatherDTO : IMap<NodoFatherEntity>
    {
        public int id { get; set; }
        public int parent { get; set; }
        public string title { get; set; }
        public DateTime? created_at { get; set; }

        public static readonly Func<NodoFatherEntity, NodoFatherDTO> singleSelect = entity => new NodoFatherDTO(entity);

        public NodoFatherDTO() { }

        public NodoFatherDTO(NodoFatherEntity entity)
        {
            MapEntityToDto(entity);
        }

        public NodoFatherEntity MapDtoToEntity()
        {
            return new NodoFatherEntity()
            {
                Id = id,
                Title = title
            };
        }

        public void MapEntityToDto(NodoFatherEntity entity)
        {
            id = entity.Id;
            title = entity.Title;
            created_at = entity.CreatedAt;
        }
    }
}
