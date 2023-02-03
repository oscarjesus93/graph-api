using GraphService.NodoFather.Models;

namespace GraphService.NodoFather.Service
{
    public interface INodoFatherService
    {

        public Task<List<NodoFatherDTO>> GetList();

        public Task<NodoFatherDTO> Get(int id);

        public NodoFatherDTO Create(NodoFatherRequest.NodoFatherRequestPost request);

        public NodoFatherDTO Update(NodoFatherRequest.NodoFatherRequestPut request, int id);

        public bool Delete(int id);

    }
}
