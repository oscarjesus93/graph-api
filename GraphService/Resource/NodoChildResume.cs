
using Connection.NodoChildrenEntities;

namespace GraphService.Resource
{
    public class NodoChildResume
    {

        public int _id;
        public string _title;

        public NodoChildResume(NodoChildEntity nodoChildEntity)
        {
            _id = nodoChildEntity.Id;
            _title = nodoChildEntity.Title;
        }

    }
}
