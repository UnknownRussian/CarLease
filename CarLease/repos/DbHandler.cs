using System.Reflection;
using System.Text.Json;
using DotNetEnv;
using MySql.Data.MySqlClient;

public static class DbHandler
{
    private static string GetConnectionString()
    {
        Env.Load();

        return $"server={Environment.GetEnvironmentVariable("MYSQL_SERVER")};" +
                $"port={Environment.GetEnvironmentVariable("MYSQL_PORT")};" +
                $"database={Environment.GetEnvironmentVariable("MYSQL_DATABASE")};" +
                $"user={Environment.GetEnvironmentVariable("MYSQL_USER")};" +
                $"password={Environment.GetEnvironmentVariable("MYSQL_PASSWORD")}";
    }

    public static T? GetById<T>(int id)
    {
        var props = typeof(T).GetProperties();
        var name = typeof(T).ToString();
        List<string> propNames = [.. props.ToList().Select(p => p.Name)];

        var sql = "SELECT";

        props.ToList().ForEach(p =>
        {
            var prop = ToSnakeCase(p.Name);
            if (props.ToList().IndexOf(p) == props.Length - 1)
                sql += $" {prop} FROM";
            else
                sql += $" {prop},";
        });

        sql += $" {ToSnakeCase(name)} WHERE {ToSnakeCase(propNames[0])} = @id";

        using var connection = new MySqlConnection(GetConnectionString());
        connection.Open();

        using var cmd = new MySqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@id", id);

        using var reader = cmd.ExecuteReader();

        T? obj = default;
        if (reader.Read())
        {
            obj = GetObjectFromReader<T>(reader, propNames, [.. props]);
        }
        connection.Close();
        return obj;
    }

    public static IEnumerable<T> GetAll<T>()
    {
        var props = typeof(T).GetProperties();
        var name = typeof(T).ToString();
        List<string> propNames = [.. props.ToList().Select(p => p.Name)];


        var sql = "SELECT";

        props.ToList().ForEach(p =>
        {
            var prop = ToSnakeCase(p.Name);
            if (props.ToList().IndexOf(p) == props.Length - 1)
                sql += $" {prop} FROM";
            else
                sql += $" {prop},";
        });

        sql += $" {ToSnakeCase(name)}";

        //Console.WriteLine(sql);

        using var connection = new MySqlConnection(GetConnectionString());
        connection.Open();

        using var cmd = new MySqlCommand(sql, connection);
        using var reader = cmd.ExecuteReader();

        var data = new List<T>();
        while (reader.Read())
        {
            data.Add(GetObjectFromReader<T>(reader, propNames, [.. props]));
        }
        connection.Close();
        return data;
    }

    private static T GetObjectFromReader<T>(MySqlDataReader reader, List<string> propNames, List<PropertyInfo> props)
    {
        #pragma warning disable JSON001 // Invalid JSON pattern
        string json = "{";
        #pragma warning restore JSON001 // Invalid JSON pattern

        for (int i = 0; i < reader.FieldCount; i++)
        {
            string type = props.ToArray()[i].PropertyType.ToString();
            if (type.Equals("System.String"))
            {
                json += $"\"{propNames[i]}\":\"{reader.GetValue(i)}\"";
            }
            else if (type.Equals("System.Int32"))
            {
                json += $"\"{propNames[i]}\":{reader.GetValue(i)}";
            }

            if (i < reader.FieldCount - 1)
                json += ",";
            else
                json += "}";
        }

        return JsonSerializer.Deserialize<T>(json)!;
    }

    private static string ToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;

        var result = new System.Text.StringBuilder();
        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];
            if (char.IsUpper(c))
            {
                if (i > 0) result.Append('_');
                result.Append(char.ToLower(c));
            }
            else
            {
                result.Append(c);
            }
        }
        return result.ToString();
    }
}
