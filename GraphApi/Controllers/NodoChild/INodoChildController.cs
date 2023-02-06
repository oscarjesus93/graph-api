using GraphService.NodoChild.Models;
using GraphService.Response;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GraphApi.Controllers.NodoChild
{
    public interface INodoChildController
    {

        public Task<ActionResult<ResponseNodoChild>> Get([Required][FromHeader(Name = "idioma")] string idioma, int id);

        public Task<ActionResult<ResponseNodoChild>> GetAll([Required][FromHeader(Name = "idioma")] string idioma);

        public Task<ActionResult<ResponseNodoChild>> Post([Required][FromHeader(Name = "idioma")] string idioma, [FromBody] NodoChildRequest.NodoChildRequestPost request);

        public Task<ActionResult<ResponseNodoChild>> Put([Required][FromHeader(Name = "idioma")] string idioma, [FromBody] NodoChildRequest.NodoChildRequestPut request, int id);

        public ActionResult<ResponseNodoChild> Delete(int id);

    }
}
