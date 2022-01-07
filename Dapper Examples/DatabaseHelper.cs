using Dapper;
using Models.Backend;
using Models.ModelAttributes;
using Models.ModelExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Database
{
    public class DatabaseHelper
    {
        protected SqlConnection _connection;
        public DatabaseHelper(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;
        }
        public DatabaseHelper(string connectionString) : this(new SqlConnection(connectionString)) { }

        // (C) - Creation
        public MethodResponse<T> Insert<T>(T item) where T : class, IDatabaseModel, new()
        {
            var response = new MethodResponse<T>();
            try
            {
                var sql = QueryBuilder<T>.BuildInsertQuery(item);
                var id = _connection.ExecuteScalar<long>(sql, item);
                if (id >= 0)
                {
                    item.Id = id;
                    response.Pass(item, "Successfully inserted item.");
                }
                else
                {
                    response.Fail("Failed to insert item.");
                }
            }
            catch (Exception ex)
            {
                response = ex.ToMethodResponse<T>("Failed to insert item.");
            }
            return response;
        }
        public async Task<MethodResponse<T>> InsertAsync<T>(T item) where T : class, IDatabaseModel, new()
        {
            var response = new MethodResponse<T>();
            try
            {
                var sql = QueryBuilder<T>.BuildInsertQuery(item);
                var id = await _connection.ExecuteScalarAsync<long>(sql, item);
                if (id >= 0)
                {
                    item.Id = id;
                    response.Pass(item, "Successfully inserted item.");
                }
                else
                {
                    response.Fail("Failed to insert item.");
                }
            }
            catch (Exception ex)
            {
                response = ex.ToMethodResponse<T>("Failed to insert item.");
            }
            return response;
        }

        // (R) - Reading
        public MethodResponse<IEnumerable<T>> GetAll<T>(int? count = null) where T : class, IDatabaseModel, new()
        {
            var response = new MethodResponse<IEnumerable<T>>();
            try
            {
                var sql = QueryBuilder<T>.BuildGetQuery(null, null, count);
                var data = _connection.Query<T>(sql) ?? new List<T>();
                if (data != null)
                {
                    response.Pass(data, "Successfully got items.");
                }
                else
                {
                    response.Fail("Failed to get items.");
                }
            }
            catch (Exception ex)
            {
                response = ex.ToMethodResponse<IEnumerable<T>>("Failed to get items.");
            }
            return response;
        }
        public async Task<MethodResponse<IEnumerable<T>>> GetAllAsync<T>(int? count = null) where T : class, IDatabaseModel, new()
        {
            var response = new MethodResponse<IEnumerable<T>>();
            try
            {
                var sql = QueryBuilder<T>.BuildGetQuery(null, null, count);
                var data = (await _connection.QueryAsync<T>(sql)) ?? new List<T>();
                if (data != null)
                {
                    response.Pass(data, "Successfully got items.");
                }
                else
                {
                    response.Fail("Failed to get items.");
                }
            }
            catch (Exception ex)
            {
                response = ex.ToMethodResponse<IEnumerable<T>>("Failed to get items.");
            }
            return response;
        }
        public MethodResponse<T> GetBy<T>(string property, string value, int? count = null) where T : class, IDatabaseModel, new()
        {
            var response = new MethodResponse<T>();
            try
            {
                var sql = QueryBuilder<T>.BuildGetQuery(value, property, count);
                var data = _connection.Query<T>(sql, new { Value = value })?.FirstOrDefault() ?? null;
                if (data != null)
                {
                    response.Pass(data, "Successfully got item.");
                }
                else
                {
                    response.Fail("Failed to get item.");
                }
            }
            catch (Exception ex)
            {
                response = ex.ToMethodResponse<T>("Failed to get item.");
            }
            return response;
        }
        public async Task<MethodResponse<T>> GetByAsync<T>(string property, string value, int? count = null) where T : class, IDatabaseModel, new()
        {
            var response = new MethodResponse<T>();
            try
            {
                var sql = QueryBuilder<T>.BuildGetQuery(value, property, count);
                var data = (await _connection.QueryAsync<T>(sql, new { Value = value }))?.FirstOrDefault() ?? null;
                if (data != null)
                {
                    response.Pass(data, "Successfully got item.");
                }
                else
                {
                    response.Fail("Failed to get item.");
                }
            }
            catch (Exception ex)
            {
                response = ex.ToMethodResponse<T>("Failed to get item.");
            }
            return response;
        }
        public MethodResponse<T> GetById<T>(long id) where T : class, IDatabaseModel, new()
        {
            var response = new MethodResponse<T>();
            try
            {
                var sql = QueryBuilder<T>.BuildGetQuery(id.ToString());
                var data = _connection.Query<T>(sql, new { Value = id })?.FirstOrDefault() ?? null;
                if (data != null)
                {
                    response.Pass(data, "Successfully got item.");
                }
                else
                {
                    response.Fail("Failed to get item.");
                }
            }
            catch (Exception ex)
            {
                response = ex.ToMethodResponse<T>("Failed to get item.");
            }
            return response;
        }
        public async Task<MethodResponse<T>> GetByIdAsync<T>(long id) where T : class, IDatabaseModel, new()
        {
            var response = new MethodResponse<T>();
            try
            {
                var sql = QueryBuilder<T>.BuildGetQuery(id.ToString());
                var data = (await _connection.QueryAsync<T>(sql, new { Id = id }))?.FirstOrDefault() ?? null;
                if (data != null)
                {
                    response.Pass(data, "Successfully got item.");
                }
                else
                {
                    response.Fail("Failed to get item.");
                }
            }
            catch (Exception ex)
            {
                response = ex.ToMethodResponse<T>("Failed to get item.");
            }
            return response;
        }
        public MethodResponse<T> Get<T>(T item) where T : class, IDatabaseModel, new()
        {
            return GetById<T>(item.Id);
        }
        public async Task<MethodResponse<T>> GetAsync<T>(T item) where T : class, IDatabaseModel, new()
        {
            return await GetByIdAsync<T>(item.Id);
        }

        // (U) - Updating
        public MethodResponse<T> Update<T>(T item) where T : class, IDatabaseModel, new()
        {
            var response = new MethodResponse<T>();
            try
            {
                var sql = QueryBuilder<T>.BuildUpdateQuery(item);
                var rowCount = _connection.ExecuteScalar<long>(sql, item);
                if (rowCount > 0)
                {
                    response.Pass(item, $"Successful update. {rowCount} row affect.");
                }
                else
                {
                    response.Fail("Failed to update item.");
                }
            }
            catch (Exception ex)
            {
                response = ex.ToMethodResponse<T>("Failed to update item.");
            }
            return response;
        }
        public async Task<MethodResponse<T>> UpdateAsync<T>(T item) where T : class, IDatabaseModel, new()
        {
            var response = new MethodResponse<T>();
            try
            {
                var sql = QueryBuilder<T>.BuildUpdateQuery(item);
                var rowCount = await _connection.ExecuteScalarAsync<long>(sql, item);
                if (rowCount > 0)
                {
                    response.Pass(item, $"Successful update. {rowCount} row affect.");
                }
                else
                {
                    response.Fail("Failed to update item.");
                }
            }
            catch (Exception ex)
            {
                response = ex.ToMethodResponse<T>("Failed to update item.");
            }
            return response;
        }

        // (D) - Deletion
        public MethodResponse DeleteById<T>(long id) where T : class, IDatabaseModel, new()
        {
            var response = new MethodResponse();
            try
            {
                var sql = QueryBuilder<T>.BuildDeleteQuery(id);
                var rowCount = _connection.ExecuteScalar<long>(sql, new { Id = id });
                if (rowCount > 0)
                {
                    response.Pass($"Successful delete. {rowCount} row affect.");
                }
                else
                {
                    response.IsSuccess = false;
                    response.Fail("Failed to delete item.");
                }
            }
            catch (Exception ex)
            {
                response = ex.ToMethodResponse<T>("Failed to delete item.");
            }
            return response;
        }
        public async Task<MethodResponse> DeleteByIdAsync<T>(long id) where T : class, IDatabaseModel, new()
        {
            var response = new MethodResponse();
            try
            {
                var sql = QueryBuilder<T>.BuildDeleteQuery(id);
                var rowCount = await _connection.ExecuteScalarAsync<long>(sql, new { Id = id });
                if (rowCount > 0)
                {
                    response.Pass($"Successful delete. {rowCount} row affect.");
                }
                else
                {
                    response.Fail("Failed to delete item.");
                }
            }
            catch (Exception ex)
            {
                response = ex.ToMethodResponse<T>("Failed to delete item.");
            }
            return response;
        }
        public MethodResponse Delete<T>(T item) where T : class, IDatabaseModel, new()
        {
            return DeleteById<T>(item.Id);
        }
        public async Task<MethodResponse> DeleteAsync<T>(T item) where T : class, IDatabaseModel, new()
        {
            return await DeleteByIdAsync<T>(item.Id);
        }
    }
}
