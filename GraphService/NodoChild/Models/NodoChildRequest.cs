using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using Utils.Validations;

namespace GraphService.NodoChild.Models
{
    public class NodoChildRequest
    {

        public abstract class Base
        {
            [Required]
            public int parent { get; set; }

            [DescriptionValidations(maxLength: 150, required: true)]
            public string title { get; set; }

            protected SqlParameter GetTitleParameter()
            {
                return new SqlParameter("@title", title);
            }

            protected SqlParameter GetParentParameter() {
                return new SqlParameter("@parent", parent);
            }

            protected static SqlParameter GetParameterId(int id)
            {
                return new SqlParameter("@id", id);
            }
        }

        public sealed class NodoChildRequestPost : Base
        {
            public (string spInsert, SqlParameter[] sqlParameters) MapToSqlParameters()
            {
                string sql = $"SP_NODO_CHILD_INSERT  " +
                                         $"@parent, " +
                                         $"@title";

                SqlParameter[] parameters = new[]
                {
                    GetParentParameter(),
                    GetTitleParameter()
                };

                return (sql, parameters);
            }
        }

        public sealed class NodoChildRequestPut : Base
        {            

            public (string spInsert, SqlParameter[] sqlParameters) MapToSqlParameters(int id)
            {
                string sql = $"SP_NODO_CHILD_UPDATE  " +
                                         $"@id, " +
                                         $"@parent, " +
                                         $"@title";

                SqlParameter[] parameters = new[]
                {
                    GetParameterId(id),
                    GetParentParameter(),
                    GetTitleParameter()
                };

                return (sql, parameters);
            }
        }

        public sealed class NodoChildRequestDelete : Base
        {
            public static (string spInsert, SqlParameter[] sqlParameters) MapToSqlParameters(int id)
            {
                string sql = $"SP_NODO_CHILD_DELETE  " +
                                         $"@id, ";

                SqlParameter[] parameters = new[]
                {
                    GetParameterId(id)
                };

                return (sql, parameters);
            }
        }

    }
}
