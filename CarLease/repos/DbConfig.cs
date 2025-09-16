using DotNetEnv;

public static class DbConfig
{
    public static string GetConnectionString()
    {
        Env.Load();

        return $"server={Environment.GetEnvironmentVariable("MYSQL_SERVER")};" +
                $"port={Environment.GetEnvironmentVariable("MYSQL_PORT")};" +
                $"database={Environment.GetEnvironmentVariable("MYSQL_DATABASE")};" +
                $"user={Environment.GetEnvironmentVariable("MYSQL_USER")};" +
                $"password={Environment.GetEnvironmentVariable("MYSQL_PASSWORD")}";
    }
}
