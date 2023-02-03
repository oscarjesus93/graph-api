using GraphService.NodoFather.Models;
using GraphService.Response;
using Microsoft.AspNetCore.Mvc;

namespace GraphApi.Controllers.NodoFather
{
    public interface INodoFatherController
    {

        public ActionResult<ResponseGeneric> Post(NodoFatherRequest.NodoFatherRequestPost request);

    }
}
