using Models.Backend;
using Models.ModelAttributes;
using Models.ModelExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Database
{
    public static class QueryBuilder<T> where T : class, IDatabaseModel, new()
    {
        public static string BuildInsertQuery(T item)
        {
            var sql = new StringBuilder();
            sql.Append($"INSERT INTO {GetTableName()} ");
            var columns = new List<string>();
            var values = new List<string>();
            var properties = item.GetType().GetProperties();
            foreach (var prop in properties)
            {
                if (prop.Name.Equals("Id") == false)
                {
                    if (CheckIfShouldIgnore(prop) == false)
                    {
                        var columnName = GetColumnName(prop);
                        if (columnName.IsNullOrEmpty())
                        {
                            columns.Add(prop.Name);
                            values.Add($"@{prop.Name}");
                        }
                        else
                        {
                            columns.Add($"{columnName}");
                            values.Add($"@{prop.Name}");
                        }
                    }
                }
            }
            sql.Append($"({string.Join(", ", columns)}) VALUES ({string.Join(", ", values)}); ");
            sql.Append("SELECT SCOPE_IDENTITY()");
            return sql.ToString();
        }
        public static string BuildGetByQuery(string propertyName, string propertyValue, int? count = null)
        {
            var sql = new StringBuilder();
            var properties = new T()?.GetType()?.GetProperties();
            var values = new List<string>();
            foreach (var prop in properties)
            {
                if (CheckIfShouldIgnore(prop) == false)
                {
                    var columnName = GetColumnName(prop);
                    if (columnName.IsNullOrEmpty())
                    {
                        values.Add(prop.Name);
                    }
                    else
                    {
                        values.Add($"{columnName} as {prop.Name}");
                    }
                }
            }
            sql.Append($"SELECT{(count != null ? $" TOP({count})" : "")} {string.Join(", ", values)} ");
            sql.Append($"FROM {GetTableName()} ");
            if (propertyValue.IsNullOrEmpty() == false) { sql.Append($"WHERE {propertyName}=@Value"); }
            return sql.ToString();
        }
        public static string BuildGetQuery(string value = null, string property = null, int? count = null)
        {
            var sql = new StringBuilder();
            var properties = new T()?.GetType()?.GetProperties();
            var values = new List<string>();
            foreach (var prop in properties)
            {
                if (CheckIfShouldIgnore(prop) == false) 
                { 
                    var columnName = GetColumnName(prop);
                    if (columnName.IsNullOrEmpty())
                    {
                        values.Add(prop.Name);
                    }
                    else
                    {
                        values.Add($"{columnName} as {prop.Name}");
                    }
                }
            }
            sql.Append($"SELECT{(count != null ? $" TOP({count})" : "")} {string.Join(", ", values)} ");
            sql.Append($"FROM {GetTableName()} ");
            if (value != null) { 
                if (property.IsNullOrEmpty() == false)
                {
                    sql.Append($"WHERE {property}=@Value");
                }
                else
                {
                    sql.Append($"WHERE Id=@Value"); 
                }
            }
            return sql.ToString();
        }
        public static string BuildUpdateQuery(T item)
        {
            var sql = new StringBuilder();
            sql.Append($"UPDATE {GetTableName()} ");
            var values = new List<string>();
            var properties = item.GetType().GetProperties();
            foreach (var prop in properties)
            {
                if (prop.Name.Equals("Id") == false)
                {
                    if (CheckIfShouldIgnore(prop) == false) 
                    {
                        var columnName = GetColumnName(prop);
                        if (columnName.IsNullOrEmpty())
                        {
                            values.Add($"{prop.Name}=@{prop.Name}");
                        }
                        else
                        {
                            values.Add($"{columnName}=@{prop.Name}");
                        }
                    }
                }
            }
            sql.Append($"SET {string.Join(", ", values)} ");
            sql.Append("WHERE Id=@Id ");
            sql.Append("SELECT @@ROWCOUNT");
            return sql.ToString();
        }
        public static string BuildDeleteQuery(long? value = null)
        {
            var sql = new StringBuilder();
            sql.Append($"DELETE FROM {GetTableName()} ");
            sql.Append($"WHERE Id=@Id ");
            sql.Append("SELECT @@ROWCOUNT");
            return sql.ToString();
        }

        public static string GetTableName() 
        {
            return ((TableNameAttribute)new T()?.GetType()?.GetCustomAttributes()?.Where(x => x is TableNameAttribute)?.FirstOrDefault())?.TableName ?? "";
        }
        public static string GetColumnName(PropertyInfo property)
        {
            return ((ColumnNameAttribute)property?.GetCustomAttributes()?.Where(x => x is ColumnNameAttribute)?.FirstOrDefault())?.ColumnName ?? "";
        }
        public static bool CheckIfShouldIgnore(PropertyInfo property)
        {
            return ((IgnoreAttribute)property?.GetCustomAttributes()?.Where(x => x is IgnoreAttribute)?.FirstOrDefault()) != null;
        }
    }
}
