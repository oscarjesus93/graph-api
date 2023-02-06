using Microsoft.Data.SqlClient;
using System.Drawing;
using Utils.Validations;

namespace GraphService.NodoFather.Models
{
    public class NodoFatherRequest
    {

        public abstract class Base
        {           

            protected static SqlParameter GetParameterId(int id)
            {
                return new SqlParameter("@id", id);
            }
        }

        public sealed class NodoFatherRequestPost : Base 
        { 
            public string MapToSqlParameters()
            {
                string sql = $"SP_NODO_FATHER_INSERT ";

                return sql;
            }
        }

        public sealed class NodoFatherRequestPut : Base
        {
            [DescriptionValidations(maxLength: 150)]
            public string title { get; set; }

            protected SqlParameter GetTitleParameter()
            {
                return new SqlParameter("@title", title);
            }

            public (string spUpdate, SqlParameter[]parameters ) MapToSqlParameters(int id) {

                string sql = $"SP_NODO_FATHER_UPDATE " +
                                 $"@id ," +
                                 $"@title";

                SqlParameter[] parameters = new[]
                {
                    GetTitleParameter(),
                    GetParameterId(id)
                };

                return (sql, parameters);
            }
        }

        public sealed class NodoFatherRequestDelete : Base
        {
            public static (string spDelete, SqlParameter[] parameters) MapToSqlParameters(int id)
            {

                string sql = $"[SP_NODO_FATHER_DELETE] " +
                                 $"@id";

                SqlParameter[] parameters = new[]
                {
                      GetParameterId(id)
                };

                return (sql, parameters);
            }
        }

    }
}
