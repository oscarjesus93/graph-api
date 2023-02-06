
using GraphService.NodoChild.Models;
using GraphService.Resource;

namespace GraphService.NodoChild.Service
{
    public interface INodoChildService
    {

        public Task<List<NodoChildDTO>> GetAll(string language);

        public Task<NodoChildDTO> Get(int id, string language);

        public Task<List<NodoChildResume>> GetAllParent(int parent, string language);


        public Task<NodoChildDTO> Create(NodoChildRequest.NodoChildRequestPost request, string language);

        public Task<NodoChildDTO> Update(NodoChildRequest.NodoChildRequestPut request, int id, string language);

        public bool Delete(int id);
    }
}
