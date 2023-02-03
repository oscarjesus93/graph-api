using GraphService.NodoChild.Models;
using GraphService.Response;
using Microsoft.AspNetCore.Mvc;

namespace GraphApi.Controllers.NodoChild
{
    public interface INodoChildController
    {

        public Task<ActionResult<ResponseNodoChild>> Get(int id);

        public Task<ActionResult<ResponseNodoChild>> GetAll();

        public ActionResult<ResponseNodoChild> Post([FromBody] NodoChildRequest.NodoChildRequestPost request);

        public ActionResult<ResponseNodoChild> Put([FromBody] NodoChildRequest.NodoChildRequestPut request, int id);

        public ActionResult<ResponseNodoChild> Delete(int id);

    }
}
