using GraphService.NodoChild.Models;
using GraphService.NodoChild.Service;
using GraphService.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Utils.Exceptions;

namespace GraphApi.Controllers.NodoChild
{
    [Route("api/[controller]")]
    [ApiController]
    public class NodoChildController : ControllerBase, INodoChildController
    {

        public readonly INodoChildService _service;
        public readonly ILogger<NodoChildController> _logger;

        public NodoChildController(ILogger<NodoChildController> logger, INodoChildService service)
        {
            _logger = logger;
            _service = service;
        }

        

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseNodoChild>> Get([Required][FromHeader(Name = "idioma")] string idioma, int id)
        {
            ResponseNodoChild response = new ResponseNodoChild();

            try
            {
                NodoChildDTO nodoChildDTO = await _service.Get(id, idioma);
                response.ParseNodoChild(nodoChildDTO);
            }
            catch (EntityException entEx)
            {
                _logger.LogWarning($"Message: {entEx.Message}");
                return BadRequest(new { message = entEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message: {ex.Message}");
                return Problem(ex.Message);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ResponseNodoChild>> GetAll([Required][FromHeader(Name = "idioma")] string idioma)
        {
            List<ResponseNodoChild> response = new List<ResponseNodoChild>();

            try
            {
                List<NodoChildDTO> nodoChildDTOs= await _service.GetAll(idioma);
                foreach (NodoChildDTO item in nodoChildDTOs)
                {
                    ResponseNodoChild responseNodo = new ResponseNodoChild();
                    responseNodo.ParseNodoChild(item);
                    response.Add(responseNodo);
                }
            }
            catch (EntityException entEx)
            {
                _logger.LogWarning($"Message: {entEx.Message}");
                return BadRequest(new { message = entEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message: {ex.Message}");
                return Problem(ex.Message);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseNodoChild>> Post([Required][FromHeader(Name = "idioma")] string idioma, NodoChildRequest.NodoChildRequestPost request)
        {
            if (!ModelState.IsValid)
            {
                string ModelStateJson = JsonConvert.SerializeObject(ModelState);
                _logger.LogWarning(ModelStateJson);
                return BadRequest(ModelState);
            }

            ResponseNodoChild response = new ResponseNodoChild();

            try
            {

                NodoChildDTO nodo = await _service.Create(request, idioma);
                response.ParseNodoChild(nodo);

            }
            catch (SPValidationException sqlEx)
            {
                string jsonRequest = JsonConvert.SerializeObject(request);
                _logger.LogWarning($"Message: {sqlEx.Message}");
                _logger.LogWarning($"Json = {jsonRequest}");
                return BadRequest(new { message = sqlEx.Message });
            }
            catch (SPErrorException sqlEx)
            {
                string jsonRequest = JsonConvert.SerializeObject(request);
                _logger.LogWarning($"Message: {sqlEx.Message}");
                _logger.LogWarning($"Json = {jsonRequest}");
                return BadRequest(new { message = sqlEx.Message });
            }
            catch (Exception ex)
            {
                string jsonRequest = JsonConvert.SerializeObject(request);
                _logger.LogWarning($"Message: {ex.Message}");
                _logger.LogWarning($"Json = {jsonRequest}");
                return Problem(ex.Message);
            }

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseNodoChild>> Put([Required][FromHeader(Name = "idioma")] string idioma, [FromBody] NodoChildRequest.NodoChildRequestPut request, int id)
        {
            if (!ModelState.IsValid)
            {
                string ModelStateJson = JsonConvert.SerializeObject(ModelState);
                _logger.LogWarning(ModelStateJson);
                return BadRequest(ModelState);
            }

            ResponseNodoChild response = new ResponseNodoChild();

            try
            {

                NodoChildDTO nodo = await _service.Update(request, id, idioma);
                response.ParseNodoChild(nodo);

            }
            catch (SPValidationException sqlEx)
            {
                string jsonRequest = JsonConvert.SerializeObject(request);
                _logger.LogWarning($"Message: {sqlEx.Message}");
                _logger.LogWarning($"Json = {jsonRequest}");
                return BadRequest(new { message = sqlEx.Message });
            }
            catch (SPErrorException sqlEx)
            {
                string jsonRequest = JsonConvert.SerializeObject(request);
                _logger.LogWarning($"Message: {sqlEx.Message}");
                _logger.LogWarning($"Json = {jsonRequest}");
                return BadRequest(new { message = sqlEx.Message });
            }
            catch (Exception ex)
            {
                string jsonRequest = JsonConvert.SerializeObject(request);
                _logger.LogWarning($"Message: {ex.Message}");
                _logger.LogWarning($"Json = {jsonRequest}");
                return Problem(ex.Message);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public ActionResult<ResponseNodoChild> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                string ModelStateJson = JsonConvert.SerializeObject(ModelState);
                _logger.LogWarning(ModelStateJson);
                return BadRequest(ModelState);
            }

            try
            {

                 _service.Delete(id);

            }
            catch (SPValidationException sqlEx)
            {
                _logger.LogWarning($"Message: {sqlEx.Message}");
                return BadRequest(new { message = sqlEx.Message });
            }
            catch (SPErrorException sqlEx)
            {
                _logger.LogWarning($"Message: {sqlEx.Message}");
                return BadRequest(new { message = sqlEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Message: {ex.Message}");
                return Problem(ex.Message);
            }

            return Ok(new { message = "nodo eliminado correctamente" });
        }
    }
}
