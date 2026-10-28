---
title: Connection String Builders in Microsoft.Data.SqlClient
description: Use SqlConnectionStringBuilder to build, validate, and change Microsoft.Data.SqlClient connection strings safely, and prevent connection string injection.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, davidengel, paulmedynski, cmalhotra
ms.date: 10/21/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
dev_langs:
  - csharp
ai-usage: ai-assisted
---

# Connection string builders in Microsoft.Data.SqlClient

Use <xref:Microsoft.Data.SqlClient.SqlConnectionStringBuilder> to create and edit connection strings through typed properties. It inherits from <xref:System.Data.Common.DbConnectionStringBuilder>. Use it whenever your code adds or changes a value, instead of joining strings.

The builder:

- Exposes each keyword as a typed property, such as `ConnectTimeout` as an **int** and `Encrypt` as a `SqlConnectionEncryptOption`.
- Rejects unknown keywords and many out-of-range values with <xref:System.ArgumentException> when you set them.
- Maps synonyms such as `Server` and `uid` to canonical keywords such as `Data Source` and `User ID`.
- Quotes values that contain semicolons, quotation marks, or leading or trailing spaces.

For the keyword list, see [Connection options](connection-options.md). For the text format, see [Parsing rules](connection-strings.md#parsing-rules).

Each sample is a complete program. The **.NET Framework** tabs use C# 7.3. To create a project that runs the samples, see [Create the project](connecting-to-data-source.md#create-the-project).

## Prevent connection string injection

When your code inserts untrusted text into a connection string, a semicolon in that text can add a keyword or replace an earlier one, because the last value wins. The builder stores the whole value in one property and quotes it, so the text can't become another keyword.

This program sets `InitialCatalog` to a value that tries to turn off pooling:

### [.NET](#tab/dotnet)

```csharp
using Microsoft.Data.SqlClient;

// Simulates a value that came from a user or another untrusted source.
string databaseFromUser = "Orders;Pooling=false";

var builder = new SqlConnectionStringBuilder
{
    DataSource = "tcp:sql.example.com,1433",
    InitialCatalog = databaseFromUser,
    IntegratedSecurity = true,
    Encrypt = SqlConnectionEncryptOption.Strict,
};

Console.WriteLine(builder.ConnectionString);
Console.WriteLine($"Pooling: {builder.Pooling}");
```

### [.NET Framework](#tab/netframework)

```csharp
using System;
using Microsoft.Data.SqlClient;

class Program
{
    static void Main()
    {
        // Simulates a value that came from a user or another untrusted source.
        string databaseFromUser = "Orders;Pooling=false";

        var builder = new SqlConnectionStringBuilder
        {
            DataSource = "tcp:sql.example.com,1433",
            InitialCatalog = databaseFromUser,
            IntegratedSecurity = true,
            Encrypt = SqlConnectionEncryptOption.Strict,
        };

        Console.WriteLine(builder.ConnectionString);
        Console.WriteLine($"Pooling: {builder.Pooling}");
    }
}
```

---

The output is:

```output
Data Source=tcp:sql.example.com,1433;Initial Catalog="Orders;Pooling=false";Integrated Security=True;Encrypt=Strict
Pooling: True
```

The builder quotes the value, and `Pooling` stays `True`. The driver sends `Orders;Pooling=false` to the server as the database name.

Quoting stops keyword injection. It doesn't decide whether a value is allowed. Check untrusted input against a list of allowed servers or databases before you use it.

## Complete a base connection string

Store the parts of a connection string that don't change, such as the server and database, in configuration. Add the secret and workload-specific values at run time. Pass the base string to the builder constructor, and then set properties.

This program reads a base string and a password from separate environment variables, connects with SQL authentication, and confirms that the password isn't visible after the connection opens.

### [.NET](#tab/dotnet)

```csharp
using Microsoft.Data.SqlClient;

// The base string holds no secret. The password comes from a separate secret source.
string baseConnectionString =
    Environment.GetEnvironmentVariable("SQL_BASE_CONNECTION_STRING")
    ?? throw new InvalidOperationException(
        "Set the SQL_BASE_CONNECTION_STRING environment variable.");
string password =
    Environment.GetEnvironmentVariable("SQL_PASSWORD")
    ?? throw new InvalidOperationException(
        "Set the SQL_PASSWORD environment variable.");

var builder = new SqlConnectionStringBuilder(baseConnectionString)
{
    Password = password,
    ApplicationName = "Orders.Worker",
};

await using var connection = new SqlConnection(builder.ConnectionString);
await connection.OpenAsync();

await using var command = new SqlCommand("SELECT APP_NAME();", connection);
Console.WriteLine($"APP_NAME(): {await command.ExecuteScalarAsync()}");

var opened = new SqlConnectionStringBuilder(connection.ConnectionString);
Console.WriteLine($"Password visible after open: {opened.ContainsKey("Password") && opened.Password.Length > 0}");
```

### [.NET Framework](#tab/netframework)

```csharp
using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

class Program
{
    static async Task Main()
    {
        // The base string holds no secret. The password comes from a separate secret source.
        string baseConnectionString =
            Environment.GetEnvironmentVariable("SQL_BASE_CONNECTION_STRING")
            ?? throw new InvalidOperationException(
                "Set the SQL_BASE_CONNECTION_STRING environment variable.");
        string password =
            Environment.GetEnvironmentVariable("SQL_PASSWORD")
            ?? throw new InvalidOperationException(
                "Set the SQL_PASSWORD environment variable.");

        var builder = new SqlConnectionStringBuilder(baseConnectionString)
        {
            Password = password,
            ApplicationName = "Orders.Worker",
        };

        using (var connection = new SqlConnection(builder.ConnectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand("SELECT APP_NAME();", connection))
            {
                Console.WriteLine($"APP_NAME(): {await command.ExecuteScalarAsync()}");
            }

            var opened = new SqlConnectionStringBuilder(connection.ConnectionString);
            Console.WriteLine($"Password visible after open: {opened.ContainsKey("Password") && opened.Password.Length > 0}");
        }
    }
}
```

---

Set `SQL_BASE_CONNECTION_STRING` to a value such as `Server=tcp:<server>,1433;Database=<database>;User ID=<user>;Encrypt=true`. The output is:

```output
APP_NAME(): Orders.Worker
Password visible after open: False
```

`Persist Security Info` defaults to `false`, so the driver removes the password from `SqlConnection.ConnectionString` after the connection opens. Keep that default.

Prefer an authentication method that needs no password, such as Windows integrated authentication or a Microsoft Entra ID managed identity. For more information, see [Microsoft Entra ID authentication](sql/azure-active-directory-authentication.md) and [Protect connection information](protecting-connection-information.md).

## Read and change values

The builder also works as a dictionary. The indexer, <xref:Microsoft.Data.SqlClient.SqlConnectionStringBuilder.ContainsKey%2A>, <xref:Microsoft.Data.SqlClient.SqlConnectionStringBuilder.Remove%2A>, and <xref:Microsoft.Data.SqlClient.SqlConnectionStringBuilder.TryGetValue%2A> accept a keyword or any of its synonyms.

### [.NET](#tab/dotnet)

```csharp
using Microsoft.Data.SqlClient;

var b = new SqlConnectionStringBuilder("Server=sql.example.com;Database=Orders;uid=app_user;pwd=x");
b["server"] = "tcp:sql2.example.com,1433";
Console.WriteLine($"DataSource: {b.DataSource}");
Console.WriteLine($"ContainsKey(\"uid\"): {b.ContainsKey("uid")}");
Console.WriteLine($"Remove(\"pwd\"): {b.Remove("pwd")}");
Console.WriteLine($"TryGetValue: {b.TryGetValue("Database", out object? db)} {db}");
Console.WriteLine($"ShouldSerialize(\"Pooling\"): {b.ShouldSerialize("Pooling")}");
Console.WriteLine(b.ConnectionString);
try { b["Bogus"] = 1; } catch (Exception e) { Console.WriteLine($"{e.GetType().Name}: {e.Message}"); }
try { b.ConnectTimeout = -1; } catch (Exception e) { Console.WriteLine($"{e.GetType().Name}: {e.Message}"); }
```

### [.NET Framework](#tab/netframework)

```csharp
using System;
using Microsoft.Data.SqlClient;

class Program
{
    static void Main()
    {
        var b = new SqlConnectionStringBuilder("Server=sql.example.com;Database=Orders;uid=app_user;pwd=x");
        b["server"] = "tcp:sql2.example.com,1433";
        Console.WriteLine($"DataSource: {b.DataSource}");
        Console.WriteLine($"ContainsKey(\"uid\"): {b.ContainsKey("uid")}");
        Console.WriteLine($"Remove(\"pwd\"): {b.Remove("pwd")}");
        Console.WriteLine($"TryGetValue: {b.TryGetValue("Database", out object db)} {db}");
        Console.WriteLine($"ShouldSerialize(\"Pooling\"): {b.ShouldSerialize("Pooling")}");
        Console.WriteLine(b.ConnectionString);
        try { b["Bogus"] = 1; } catch (Exception e) { Console.WriteLine($"{e.GetType().Name}: {e.Message}"); }
        try { b.ConnectTimeout = -1; } catch (Exception e) { Console.WriteLine($"{e.GetType().Name}: {e.Message}"); }
    }
}
```

---

The output is:

```output
DataSource: tcp:sql2.example.com,1433
ContainsKey("uid"): True
Remove("pwd"): True
TryGetValue: True Orders
ShouldSerialize("Pooling"): False
Data Source=tcp:sql2.example.com,1433;Initial Catalog=Orders;User ID=app_user
ArgumentException: Keyword not supported: 'Bogus'.
ArgumentException: Invalid value for key 'Connect Timeout'.
```

`ShouldSerialize` returns `false` for a keyword that isn't in the string. `ContainsKey` returns `true` for every supported keyword, whether or not the string sets it. The `ConnectionString` property includes only the keywords that you set.

## Understand what the builder doesn't check

The builder rejects unknown keywords and many invalid values, but it doesn't check every value. For example, it accepts any text for `Type System Version` and `Transaction Binding`, and it doesn't check string lengths. It also doesn't check combinations of keywords, so it accepts `Authentication=Active Directory Default` together with `Integrated Security=true`. `SqlConnection` rejects that combination with `ArgumentException` when you pass it the string. For the full list of rules, see [Validation rules](connection-options.md#validation-rules).

The builder writes `Authentication` values without spaces, such as `Authentication=ActiveDirectoryDefault`. The driver accepts both forms.

The builder also doesn't protect a password after it enters process memory, and it doesn't decide whether a server, identity, or certificate setting is safe.

## Keep one canonical connection string

Each distinct connection string gets its own connection pool. Two hand-written strings that differ only in keyword order or synonyms create two pools. The builder writes canonical keywords in a consistent order, so `Server=a;Database=d` and `Database=d;Server=a` both become `Data Source=a;Initial Catalog=d`. Build the connection string once at startup and reuse `builder.ConnectionString`. Don't add per-request values, such as user names or request IDs. For more pooling guidance, see [Use connection pooling](connection-strings.md#use-connection-pooling).

## Related content

- [Connection strings](connection-strings.md)
- [Connection options](connection-options.md)
- [Protect connection information](protecting-connection-information.md)
- [Microsoft.Data.SqlClient for SQL Server](microsoft-ado-net-sql-server.md)
