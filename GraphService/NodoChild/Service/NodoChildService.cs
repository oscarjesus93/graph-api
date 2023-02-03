
using Azure.Core;
using Connection;
using Connection.NodoChildrenEntities;
using Connection.NodoFatherEntities;
using GraphService.NodoChild.Models;
using GraphService.NodoFather.Models;
using GraphService.NodoFather.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Utils.Exceptions;
using Utils.Interfaces.Cache;

namespace GraphService.NodoChild.Service
{
    public class NodoChildService : INodoChildService
    {

        private readonly DbSet<NodoChildEntity> nodoChildRepo;
        private readonly DbSet<NodoFatherEntity> nodoFatherRepo;
        private readonly ConnectionContext connectionContext;
        private readonly Func<NodoChildEntity, NodoChildDTO> singleSelect = NodoChildDTO.singleSelect;
        private readonly ICache<NodoChildDTO> _cache;


        public NodoChildService(ConnectionContext _connectionContext, IHttpContextAccessor httpcontextAccessor, ICache<NodoChildDTO> cache)
        {
            nodoChildRepo = _connectionContext.nodoChildEntities;
            nodoFatherRepo = _connectionContext.nodoFatherEntities;
            connectionContext = _connectionContext;
            _cache = cache;
        }

        public async Task<List<NodoChildDTO>> GetAll()
        {
            List<NodoChildDTO> nodoChildrenCache = _cache.Find();

            if (nodoChildrenCache.Any() == false)
            {
                List<NodoChildEntity> nodoChildEntities = await nodoChildRepo.ToListAsync();

                if (!nodoChildEntities.Any())
                {
                    EntityException entityException = new("No se encontraron registros");
                    throw entityException;
                }

                List<NodoChildDTO> nodoChildren = nodoChildEntities.Select(singleSelect).ToList();

                _cache.Set(nodoChildren);

                return nodoChildren;
            }

            return nodoChildrenCache;
        }

        public async Task<NodoChildDTO> Get(int id)
        {
            NodoChildEntity entity = await nodoChildRepo.Where(op => op.Id == id).FirstAsync();

            if (entity == null)
            {
                EntityException entityException = new("No se encontraro el nodo registrado");
                throw entityException;
            }

            //BUSCAMOS AL PADRE
            NodoFatherEntity entityFather = await nodoFatherRepo.Where(op => op.Id == entity.Parent).FirstAsync();
            NodoFatherDTO nodoFather = new NodoFatherDTO(entityFather);

            NodoChildDTO nodo = new NodoChildDTO(entity);
            nodo.MapNodoFather(nodoFather);

            return nodo;
        }

        public NodoChildDTO Create(NodoChildRequest.NodoChildRequestPost request)
        {
            try
            {
                (string spInsert, SqlParameter[] parameters) = request.MapToSqlParameters();

                NodoChildDTO nodoChild = nodoChildRepo.FromSqlRaw(spInsert, parameters)
                                                        .Select(singleSelect).First();

                _cache.Set(nodoChild); 

                return nodoChild;

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

        public NodoChildDTO Update(NodoChildRequest.NodoChildRequestPut request, int id)
        {
            try
            {
                (string spUpdate, SqlParameter[] parameters) = request.MapToSqlParameters(id);

                NodoChildDTO nodoChild = nodoChildRepo.FromSqlRaw(spUpdate, parameters)
                                                        .Select(singleSelect).First();

                _cache.Update(nodoChild, op => op.id == id);

                return nodoChild;

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
                (string spDelete, SqlParameter[] parameters) = NodoChildRequest.NodoChildRequestDelete.MapToSqlParameters (id);

                connectionContext.Database.ExecuteSqlRaw(spDelete, parameters);

                _cache.Remove(op => op.id == id);

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
