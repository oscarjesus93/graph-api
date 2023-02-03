
using GraphService.NodoChild.Models;

namespace GraphService.NodoChild.Service
{
    public interface INodoChildService
    {

        public Task<List<NodoChildDTO>> GetAll();

        public Task<NodoChildDTO> Get(int id);


        public NodoChildDTO Create(NodoChildRequest.NodoChildRequestPost request);

        public NodoChildDTO Update(NodoChildRequest.NodoChildRequestPut request, int id);

        public bool Delete(int id);
    }
}
