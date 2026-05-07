using Npgsql;
using System.Data;

public static class SupabasePgConnection
{
    // Caracteres especiales en la contraseña deben codificarse:
    // ! = %21   $ = %24   * = %2A
    // ZhX_V!$63fM$jh*  →  ZhX_V%21%2463fM%24jh%2A

    private static string PoolUrl =>
        "Host=aws-1-us-west-2.pooler.supabase.com;" +
        "Port=6543;" +
        "Database=postgres;" +
        "Username=postgres.urhgyxyhekfvjdipujjf;" +
        "Password=ZhX_V!$63fM$jh*;" +   // En formato Key=Value la pass va literal
        "SSL Mode=Require;" +
        "Trust Server Certificate=true;" +
        "Pooling=true;" +
        "Timeout=30;";

    private static string DirectUrl =>
        "Host=aws-1-us-west-2.pooler.supabase.com;" +
        "Port=5432;" +
        "Database=postgres;" +
        "Username=postgres.urhgyxyhekfvjdipujjf;" +
        "Password=ZhX_V!$63fM$jh*;" +
        "SSL Mode=Require;" +
        "Trust Server Certificate=true;" +
        "Timeout=30;";

    public static NpgsqlConnection OpenPoolConnection()
    {
        var conn = new NpgsqlConnection(PoolUrl);
        conn.Open();
        return conn;
    }

    public static NpgsqlConnection OpenDirectConnection()
    {
        var conn = new NpgsqlConnection(DirectUrl);
        conn.Open();
        return conn;
    }

    public static bool TestConnection()
    {
        try
        {
            using (var conn = OpenPoolConnection())
                return conn.State == ConnectionState.Open;
        }
        catch
        {
            return false;
        }
    }
}