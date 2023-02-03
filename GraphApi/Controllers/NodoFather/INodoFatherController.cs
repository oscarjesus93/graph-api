using GraphService.NodoFather.Models;
using GraphService.Response;
using Microsoft.AspNetCore.Mvc;

namespace GraphApi.Controllers.NodoFather
{
    public interface INodoFatherController
    {
        public Task<ActionResult<ResponseGeneric>> Get();

        public Task<ActionResult<ResponseGeneric>> Get(int id);

        public ActionResult<ResponseGeneric> Post(NodoFatherRequest.NodoFatherRequestPost request);

        public ActionResult<ResponseGeneric> Put(NodoFatherRequest.NodoFatherRequestPut request, int id);

        public ActionResult Delete(int id);
    }
}
