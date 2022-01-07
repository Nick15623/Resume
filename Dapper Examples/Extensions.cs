using Models.Backend;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Database
{
    public static class DatabaseExtensions
    {
        public static MethodResponse<T> Insert<T>(this SqlConnection conn, T item) where T : class, IDatabaseModel, new() { return new DatabaseHelper(conn).Insert(item); }
        public static async Task<MethodResponse<T>> InsertAsync<T>(this SqlConnection conn, T item) where T : class, IDatabaseModel, new() { return await new DatabaseHelper(conn).InsertAsync(item); }
        public static MethodResponse<IEnumerable<T>> GetAll<T>(this SqlConnection conn, int? count = null) where T : class, IDatabaseModel, new() { return new DatabaseHelper(conn).GetAll<T>(count); }
        public static async Task<MethodResponse<IEnumerable<T>>> GetAllAsync<T>(this SqlConnection conn, int? count = null) where T : class, IDatabaseModel, new() { return await new DatabaseHelper(conn).GetAllAsync<T>(count); }
        public static MethodResponse<T> GetById<T>(this SqlConnection conn, long id) where T : class, IDatabaseModel, new() { return new DatabaseHelper(conn).GetById<T>(id); }
        public static async Task<MethodResponse<T>> GetByIdAsync<T>(this SqlConnection conn, long id) where T : class, IDatabaseModel, new() { return await new DatabaseHelper(conn).GetByIdAsync<T>(id); }
        public static MethodResponse<T> Get<T>(this SqlConnection conn, T item) where T : class, IDatabaseModel, new() { return new DatabaseHelper(conn).Get<T>(item); }
        public static async Task<MethodResponse<T>> GetAsync<T>(this SqlConnection conn, T item) where T : class, IDatabaseModel, new() { return await new DatabaseHelper(conn).GetAsync<T>(item); }

        public static MethodResponse<T> Update<T>(this SqlConnection conn, T item) where T : class, IDatabaseModel, new() { return new DatabaseHelper(conn).Update<T>(item); }
        public static async Task<MethodResponse<T>> UpdateAsync<T>(this SqlConnection conn, T item) where T : class, IDatabaseModel, new() { return await new DatabaseHelper(conn).UpdateAsync<T>(item); }

        public static MethodResponse DeleteById<T>(this SqlConnection conn, long id) where T : class, IDatabaseModel, new() { return new DatabaseHelper(conn).DeleteById<T>(id); }
        public static async Task<MethodResponse> DeleteByIdAsync<T>(this SqlConnection conn, long id) where T : class, IDatabaseModel, new() { return await new DatabaseHelper(conn).DeleteByIdAsync<T>(id); }

        public static MethodResponse Delete<T>(this SqlConnection conn, T item) where T : class, IDatabaseModel, new() { return new DatabaseHelper(conn).Delete<T>(item); }
        public static async Task<MethodResponse> DeleteAsync<T>(this SqlConnection conn, T item) where T : class, IDatabaseModel, new() { return await new DatabaseHelper(conn).DeleteAsync<T>(item); }
    }
}
