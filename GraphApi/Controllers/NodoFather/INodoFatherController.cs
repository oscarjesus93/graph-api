using GraphService.NodoFather.Models;
using GraphService.Response;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GraphApi.Controllers.NodoFather
{
    public interface INodoFatherController
    {
        public Task<ActionResult<ResponseGeneric>> Get(
            [Required][FromHeader(Name = "idioma")] string value);

        public Task<ActionResult<ResponseGeneric>> Get(
            [Required][FromHeader(Name = "idioma")] string value, 
            [FromQuery(Name = "child")] int valorChild, 
            int id);

        public ActionResult<ResponseGeneric> Post(
            [Required][FromHeader(Name = "idioma")] string value);

        public ActionResult<ResponseGeneric> Put(
            [Required][FromHeader(Name = "idioma")] string value, 
            NodoFatherRequest.NodoFatherRequestPut request, 
            int id);

        public ActionResult Delete(int id);
    }
}
