using GraphService.NodoChild.Models;
using GraphService.NodoFather.Models;
using GraphService.Resource;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace GraphService.Response
{
    public class ResponseGeneric
    {

        public int id { get; set; }
        public List<NodoChildResume>  paternt { get; set; }
        public string title { get; set; }
        public DateTime? create_at { get; set; }     


        public void ParseNodoFather(NodoFatherDTO nodo)
        {
            id = nodo.id;
            title = nodo.title;
            create_at = nodo.created_at;
            paternt = nodo.listChilds;
        }


    }
}
