
using GraphService.NodoFather.Models;
using GraphService.NodoFather.Service;
using GraphService.Response;
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

        [HttpPost]
        public ActionResult<ResponseGeneric> Post(NodoFatherRequest.NodoFatherRequestPost request)
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
                NodoFatherDTO nodoFather = _service.Create(request);
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
    }
}
