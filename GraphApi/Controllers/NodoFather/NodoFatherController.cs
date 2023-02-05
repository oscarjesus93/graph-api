
using Azure;
using GraphService.NodoFather.Models;
using GraphService.NodoFather.Service;
using GraphService.Response;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.OleDb;
using Utils.Exceptions;

namespace GraphApi.Controllers.NodoFather
{
    [Route("api/[controller]")]
    [ApiController]
    public class NodoFatherController : ControllerBase, INodoFatherController
    {

        public readonly INodoFatherService _service;
        public readonly ILogger<NodoFatherController> _logger;

        public NodoFatherController(INodoFatherService nodoFatherService, ILogger<NodoFatherController> logger)
        {
            _service = nodoFatherService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseGeneric>> Get([FromHeader(Name = "idioma")] string value)
        {
            List<ResponseGeneric> response = new List<ResponseGeneric>();

            try
            {
                List<NodoFatherDTO> nodoFathers = await _service.GetList(value);

                foreach(NodoFatherDTO nodo in nodoFathers)
                {
                    ResponseGeneric generic = new ResponseGeneric();
                    generic.ParseNodoFather(nodo);
                    response.Add(generic);
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

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseGeneric>> Get(
            [FromHeader(Name = "idioma")] string value, 
            int id)
        {
            ResponseGeneric response = new ResponseGeneric();

            try
            {
                NodoFatherDTO nodoFathers = await _service.Get(id, value);

                response.ParseNodoFather(nodoFathers);

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
        public ActionResult<ResponseGeneric> Post([FromHeader(Name = "idioma")] string value)
        {
            if (!ModelState.IsValid)
            {
                string ModelStateJson = JsonConvert.SerializeObject(ModelState);
                _logger.LogWarning(ModelStateJson);
                return BadRequest(ModelState);
            }

            ResponseGeneric response = new ResponseGeneric();

            try
            {
                NodoFatherDTO nodoFather = _service.Create(new NodoFatherRequest.NodoFatherRequestPost(), value);
                response.ParseNodoFather(nodoFather);

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

            return Ok(response);
        }

        [HttpPut("{id}")]
        public ActionResult<ResponseGeneric> Put([FromHeader(Name = "idioma")] string value, [FromBody] NodoFatherRequest.NodoFatherRequestPut request, int id)
        {
            if (!ModelState.IsValid)
            {
                string ModelStateJson = JsonConvert.SerializeObject(ModelState);
                _logger.LogWarning(ModelStateJson);
                return BadRequest(ModelState);
            }

            ResponseGeneric response = new ResponseGeneric();

            try
            {
                NodoFatherDTO nodoFather = _service.Update(request, id, value);
                response.ParseNodoFather(nodoFather);

            }
            catch (SPValidationException sqlEx)
            {
                string jsonRequest = JsonConvert.SerializeObject(request);
                _logger.LogWarning($"Message: {sqlEx.Message}");
                _logger.LogWarning($"Json = {jsonRequest}");

                return BadRequest(sqlEx.Message);
            }
            catch (SPErrorException sqlEx)
            {
                string jsonRequest = JsonConvert.SerializeObject(request);
                _logger.LogWarning($"Message: {sqlEx.Message}");
                _logger.LogWarning($"Json = {jsonRequest}");
                return Problem(sqlEx.Message);
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
        public ActionResult Delete(int id)
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
                return BadRequest(sqlEx.Message);
            }
            catch (SPErrorException sqlEx)
            {
                _logger.LogWarning($"Message: {sqlEx.Message}");
                return Problem(sqlEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Message: {ex.Message}");
                return Problem(ex.Message);
            }

            return Ok();
        }        
    }
}
