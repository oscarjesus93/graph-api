using Microsoft.Data.SqlClient;
using System.Drawing;
using Utils.Validations;

namespace GraphService.NodoFather.Models
{
    public class NodoFatherRequest
    {

        public abstract class Base
        {
            [DescriptionValidations(maxLength: 150, required: true)]
            public string tittle { get; set; }

            protected SqlParameter GetTitleParameter()
            {
                return new SqlParameter("@title", tittle);
            }

            protected SqlParameter GetParameterId(int id)
            {
                return new SqlParameter("@id", id);
            }
        }

        public sealed class NodoFatherRequestPost : Base 
        { 
            public (string spInsert, SqlParameter[] sqlParameters) MapToSqlParameters()
            {
                string sql = $"SP_NODO_FATHER_INSERT " +
                                    "@title";

                SqlParameter[] parameters = new[]
                {
                    GetTitleParameter()
                };

                return (sql, parameters);
            }
        }

        public sealed class NodoFatherRequestPut : Base
        {
            public (string spUpdate, SqlParameter[]parameters ) MapToSqlParameters(int id) {

                string sql = $"[SP_NODO_FATHER_UPDATE] " +
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
            public (string spDelete, SqlParameter[] parameters) MapToSqlParameters(int id)
            {

                string sql = $"[SP_NODO_FATHER_DELETE] " +
                                 $"@id ," +
                                 $"@title";

                SqlParameter[] parameters = new[]
                {
                     GetParameterId(id)
                };

                return (sql, parameters);
            }
        }

    }
}
