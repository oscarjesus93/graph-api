using GraphService.NodoFather.Models;

namespace GraphService.NodoFather.Service
{
    public interface INodoFatherService
    {

        public Task<List<NodoFatherDTO>> GetList(string language);

        public Task<NodoFatherDTO> Get(int id, string language);

        public NodoFatherDTO Create(NodoFatherRequest.NodoFatherRequestPost request, string language);

        public NodoFatherDTO Update(NodoFatherRequest.NodoFatherRequestPut request, int id, string language);

        public bool Delete(int id);

    }
}
