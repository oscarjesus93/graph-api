
using GraphService.NodoChild.Models;
using GraphService.NodoFather.Models;

namespace GraphService.Response
{
    public class ResponseNodoChild
    {

        public int id { get; set; }
        public NodoFatherDTO paternt { get; set; }
        public string title { get; set; }
        public DateTime? create_at { get; set; }

        public void ParseNodoChild(NodoChildDTO nodo)
        {
            id = nodo.id;
            paternt = nodo.nodoFatherDTO;
            title = nodo.title;
            create_at = nodo.created_at;
        }

    }
}
