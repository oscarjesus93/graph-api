using GraphService.NodoFather.Models;
using GraphService.Response;
using Microsoft.AspNetCore.Mvc;

namespace GraphApi.Controllers.NodoFather
{
    public interface INodoFatherController
    {
        public Task<ActionResult<ResponseGeneric>> Get([FromHeader(Name = "idioma")] string value);

        public Task<ActionResult<ResponseGeneric>> Get([FromHeader(Name = "idioma")] string value, int id);

        public ActionResult<ResponseGeneric> Post([FromHeader(Name = "idioma")] string value);

        public ActionResult<ResponseGeneric> Put([FromHeader(Name = "idioma")] string value, NodoFatherRequest.NodoFatherRequestPut request, int id);

        public ActionResult Delete(int id);
    }
}
