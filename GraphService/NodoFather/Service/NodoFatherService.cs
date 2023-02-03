using Connection;
using Connection.NodoFatherEntities;
using GraphService.NodoFather.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Utils.Exceptions;
using Utils.Interfaces.Cache;

namespace GraphService.NodoFather.Service
{
    public class NodoFatherService : INodoFatherService
    {
        private readonly DbSet<NodoFatherEntity> _nodoFatherRepo;
        private readonly ConnectionContext connectionContext;
        private readonly Func<NodoFatherEntity, NodoFatherDTO> singleSelect = NodoFatherDTO.singleSelect;
        private readonly ICache<NodoFatherDTO> cache;
        
        public NodoFatherService(ConnectionContext _connectionContext, IHttpContextAccessor httpcontextAccessor, ICache<NodoFatherDTO> _cache)
        {
            _nodoFatherRepo = _connectionContext.nodoFatherEntities;
            connectionContext= _connectionContext;
            cache = _cache;
        }

        public NodoFatherDTO Create(NodoFatherRequest.NodoFatherRequestPost request)
        {
            try
            {
                (string spInsert, SqlParameter[] parameters) = request.MapToSqlParameters();

                NodoFatherDTO nodoFather = _nodoFatherRepo.FromSqlRaw(spInsert, parameters).Select(singleSelect).First();

                cache.Set(nodoFather);

                return nodoFather;

            }
            catch (SqlException sqlEx)
            {
                throw HandleSQLException.GetSqlException(sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
