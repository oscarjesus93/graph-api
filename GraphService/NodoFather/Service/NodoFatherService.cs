using Azure.Core;
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

        public async Task<NodoFatherDTO> Get(int id)
        {
            NodoFatherEntity nodoFather = await _nodoFatherRepo.Where(x => x.Id == id).FirstOrDefaultAsync();

            if(nodoFather == null)
            {
                EntityException entityException = new("No se encontraro el nodo registrado");
                throw entityException;
            }

            return new NodoFatherDTO(nodoFather);
        }

        public async Task<List<NodoFatherDTO>> GetList()
        {
            List<NodoFatherDTO> list = cache.Find();

            if (!list.Any())
            {
                List<NodoFatherEntity> fatherEntities = await _nodoFatherRepo.ToListAsync();

                if (!fatherEntities.Any())
                {
                    EntityException entityException = new("No se encontraron registros");
                    throw entityException;
                }

                List<NodoFatherDTO> nodoFathers = fatherEntities.Select(singleSelect).ToList();

                cache.Set(nodoFathers);

                return nodoFathers;

            }

            return list;
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

        public NodoFatherDTO Update(NodoFatherRequest.NodoFatherRequestPut request, int id)
        {
            try
            {
                (string spUpdate, SqlParameter[] parameters) = request.MapToSqlParameters(id);

                NodoFatherDTO nodoFather = _nodoFatherRepo.FromSqlRaw(spUpdate, parameters).Select(singleSelect).First();

                cache.Update(nodoFather, up => up.id == id);

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

        public bool Delete(int id)
        {
            try
            {
                (string spDelete, SqlParameter[] parameters) = NodoFatherRequest.NodoFatherRequestDelete.MapToSqlParameters(id);

                connectionContext.Database.ExecuteSqlRaw(spDelete, parameters);
                cache.Remove(up => up.id == id);

                return true;

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
