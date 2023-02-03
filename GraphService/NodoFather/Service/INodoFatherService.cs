using GraphService.NodoFather.Models;

namespace GraphService.NodoFather.Service
{
    public interface INodoFatherService
    {

        public NodoFatherDTO Create(NodoFatherRequest.NodoFatherRequestPost request);

    }
}
