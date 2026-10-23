---
title: "Quickstart: Connect and Query with Microsoft.Data.SqlClient"
description: Create a .NET console app that connects to SQL Server, Azure SQL, or SQL database in Microsoft Fabric, then creates a table, inserts rows with parameters, and reads them back.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, davidengel, paulmedynski, cmalhotra
ms.date: 10/21/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: quickstart
dev_langs:
  - csharp
ai-usage: ai-assisted
---

# Quickstart: Connect and query with Microsoft.Data.SqlClient

In this quickstart, you build a .NET console app with Microsoft.Data.SqlClient 7.1.1.

You start with an empty folder, the .NET SDK, and a SQL Server or Azure SQL database that you can reach from your computer. You finish with a console app that creates a table, inserts two rows with parameters, and reads them back.

Along the way, the app:

- Reads its connection string from an environment variable instead of source code.
- Opens a connection asynchronously.
- Uses parameters for every value that it sends to the database.
- Handles SQL errors and cancellation.

To build the same data access into a web API, see [Quickstart: Use Microsoft.Data.SqlClient in an ASP.NET Core app](quickstart-asp-net-core-sql-client.md). It uses the same table.

## Prerequisites

- The [.NET 10 SDK or a later supported .NET SDK](https://dotnet.microsoft.com/download). The SDK runs on Windows, Linux, and macOS. The SDK also builds the **.NET Framework** version of the sample, which runs only on Windows and needs .NET Framework 4.8.1. Use the **.NET** version for new development.
- A code editor. You can use Visual Studio, Visual Studio Code, or any editor together with the .NET CLI. This article uses the .NET CLI.
- A database that you can connect to. If you don't have one, create one by using the following options.

[!INCLUDE [prereq-create-sql-database](../../includes/paragraph-content/prereq-create-sql-database.md)]

### Database permissions

The app creates its own table, so you don't need sample data. The database identity needs permission to connect and to create, insert into, and select from a table.

For SQL database in Microsoft Fabric, [copy the server and database names from the SQL database item](/fabric/database/sql/connect#find-sql-connection-string). Don't use the SQL analytics endpoint. The identity needs Read item permission, which a workspace role or item permission can provide. For more information, see [Authentication in SQL database](/fabric/database/sql/authentication). SQL authentication isn't supported.

For Azure SQL Database, [configure Microsoft Entra ID authentication and database access](/azure/azure-sql/database/authentication-aad-configure).

## Create the project

### [.NET](#tab/dotnet)

In an empty folder, run these commands:

```console
dotnet new console --framework net10.0 --name SqlClientQuickstart
cd SqlClientQuickstart
dotnet add package Microsoft.Data.SqlClient --version 7.1.1
dotnet add package Microsoft.Data.SqlClient.Extensions.Azure --version 7.1.1
```

### [.NET Framework](#tab/netframework)

The console template doesn't offer .NET Framework targets. In an empty folder, create the project:

```console
dotnet new console --framework net10.0 --name SqlClientQuickstart
cd SqlClientQuickstart
```

Replace the contents of `SqlClientQuickstart.csproj` with this project file:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net481</TargetFramework>
  </PropertyGroup>

</Project>
```

Then add the packages:

```console
dotnet add package Microsoft.Data.SqlClient --version 7.1.1
dotnet add package Microsoft.Data.SqlClient.Extensions.Azure --version 7.1.1
```

---

The extension package supplies the driver's Microsoft Entra authentication modes, such as `Active Directory Default`. If your app uses only SQL authentication or Windows integrated authentication, you can omit `Microsoft.Data.SqlClient.Extensions.Azure`. When you use both packages, keep their versions the same. For other install options, see [Install, update, and deploy Microsoft.Data.SqlClient](download-microsoft-sqlclient-data-provider.md).

## Configure the connection

Set the `SQL_CONNECTION_STRING` environment variable for your database. Don't put a password, access token, or production connection string in source code.

Choose one of these starting points and replace the placeholders.

### Azure SQL or SQL database in Fabric with Microsoft Entra ID

Sign in with a Microsoft Entra identity that has access to the database. For local development, use a developer tool such as the Azure CLI:

```console
az login
```

Copy the exact server and database names from the Azure SQL database or the SQL database item in Fabric. For PowerShell:

```powershell
$env:SQL_CONNECTION_STRING = 'Server=tcp:<server>,1433;Database=<database>;Authentication=Active Directory Default;Encrypt=Strict;MultiSubnetFailover=true;Connect Timeout=30'
```

For Bash:

```bash
export SQL_CONNECTION_STRING='Server=tcp:<server>,1433;Database=<database>;Authentication=Active Directory Default;Encrypt=Strict;MultiSubnetFailover=true;Connect Timeout=30'
```

For an app hosted in Azure, grant its managed identity database access, then use `Authentication=Active Directory Managed Identity`. For other options, see [Microsoft Entra authentication with Microsoft.Data.SqlClient](sql/azure-active-directory-authentication.md).

### SQL Server with SQL authentication

Use the server, port, database, and login for your SQL Server instance. The following example is for a local development instance that doesn't have a trusted certificate. For PowerShell:

```powershell
$env:SQL_CONNECTION_STRING = 'Server=tcp:<server>,1433;Database=<database>;User ID=<user-id>;Password=<password>;Encrypt=true;TrustServerCertificate=true;Connect Timeout=30'
```

For Bash:

```bash
export SQL_CONNECTION_STRING='Server=tcp:<server>,1433;Database=<database>;User ID=<user-id>;Password=<password>;Encrypt=true;TrustServerCertificate=true;Connect Timeout=30'
```

> [!CAUTION]
> `TrustServerCertificate=true` skips server certificate validation. Use it only with a local development instance that doesn't have a trusted certificate. For shared or production SQL Server instances, install a certificate that the client trusts, use the server name on that certificate, and remove `TrustServerCertificate=true`. For more information, see [Encryption and certificate validation in Microsoft.Data.SqlClient](encryption-and-certificate-validation.md).

If your environment supports Windows integrated authentication or Kerberos, replace `User ID` and `Password` with `Integrated Security=true`. For setup requirements, see [SQL Server and Windows authentication](sql/authentication-sql-server.md).

## Add the code

### [.NET](#tab/dotnet)

Replace the contents of `Program.cs` with this code:

```csharp
using System.Data;
using Microsoft.Data.SqlClient;

string? connectionString =
    Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING");

if (string.IsNullOrWhiteSpace(connectionString))
{
    Console.Error.WriteLine(
        "Set the SQL_CONNECTION_STRING environment variable.");
    return 1;
}

using var cancellation = new CancellationTokenSource();
Console.CancelKeyPress += (_, eventArgs) =>
{
    eventArgs.Cancel = true;
    cancellation.Cancel();
};

try
{
    await using var connection = new SqlConnection(connectionString);
    await connection.OpenAsync(cancellation.Token);

    const string createTableSql = """
        IF OBJECT_ID(N'dbo.SqlClientQuickstart', N'U') IS NULL
        BEGIN
            CREATE TABLE dbo.SqlClientQuickstart
            (
                Id int IDENTITY(1, 1) PRIMARY KEY,
                Message nvarchar(200) NOT NULL,
                CreatedAt datetimeoffset NOT NULL
                    CONSTRAINT DF_SqlClientQuickstart_CreatedAt
                    DEFAULT sysdatetimeoffset()
            );
        END;
        """;

    await using (var createCommand =
        new SqlCommand(createTableSql, connection) { CommandTimeout = 30 })
    {
        await createCommand.ExecuteNonQueryAsync(cancellation.Token);
    }

    const string insertSql = """
        INSERT INTO dbo.SqlClientQuickstart (Message)
        OUTPUT INSERTED.Id
        VALUES (@message);
        """;

    string[] messages =
    [
        "Hello from Microsoft.Data.SqlClient",
        "Parameters keep values out of the SQL text"
    ];

    int firstId = 0;
    await using (var insertCommand =
        new SqlCommand(insertSql, connection) { CommandTimeout = 30 })
    {
        SqlParameter messageParameter =
            insertCommand.Parameters.Add("@message", SqlDbType.NVarChar, 200);

        foreach (string message in messages)
        {
            messageParameter.Value = message;
            int id = (int)(await insertCommand.ExecuteScalarAsync(
                cancellation.Token))!;
            Console.WriteLine($"Inserted row {id}.");
            firstId = firstId == 0 ? id : firstId;
        }
    }

    const string querySql = """
        SELECT Id, Message, CreatedAt
        FROM dbo.SqlClientQuickstart
        WHERE Id >= @firstId
        ORDER BY Id;
        """;

    await using var queryCommand =
        new SqlCommand(querySql, connection) { CommandTimeout = 30 };
    queryCommand.Parameters.Add("@firstId", SqlDbType.Int).Value = firstId;

    await using SqlDataReader reader =
        await queryCommand.ExecuteReaderAsync(cancellation.Token);

    while (await reader.ReadAsync(cancellation.Token))
    {
        Console.WriteLine(
            $"{reader.GetInt32(0)}: {reader.GetString(1)} " +
            $"(created {reader.GetDateTimeOffset(2):u})");
    }

    return 0;
}
catch (OperationCanceledException)
{
    Console.Error.WriteLine("The operation was canceled.");
    return 2;
}
catch (SqlException ex)
{
    Console.Error.WriteLine(
        $"SQL error {ex.Number}, connection {ex.ClientConnectionId}: " +
        ex.Message);
    if (ex.InnerException is not null)
    {
        Console.Error.WriteLine(ex.InnerException.Message);
    }
    return 3;
}
```

### [.NET Framework](#tab/netframework)

Replace the contents of `Program.cs` with this code:

```csharp
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

class Program
{
    static async Task<int> Main()
    {
        string connectionString =
            Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            Console.Error.WriteLine(
                "Set the SQL_CONNECTION_STRING environment variable.");
            return 1;
        }

        using (var cancellation = new CancellationTokenSource())
        {
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true;
                cancellation.Cancel();
            };

            try
            {
                await RunAsync(connectionString, cancellation.Token);
                return 0;
            }
            catch (OperationCanceledException)
            {
                Console.Error.WriteLine("The operation was canceled.");
                return 2;
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine(
                    $"SQL error {ex.Number}, connection {ex.ClientConnectionId}: " +
                    ex.Message);
                if (ex.InnerException != null)
                {
                    Console.Error.WriteLine(ex.InnerException.Message);
                }
                return 3;
            }
        }
    }

    static async Task RunAsync(string connectionString, CancellationToken token)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync(token);

            const string createTableSql = @"
IF OBJECT_ID(N'dbo.SqlClientQuickstart', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.SqlClientQuickstart
    (
        Id int IDENTITY(1, 1) PRIMARY KEY,
        Message nvarchar(200) NOT NULL,
        CreatedAt datetimeoffset NOT NULL
            CONSTRAINT DF_SqlClientQuickstart_CreatedAt
            DEFAULT sysdatetimeoffset()
    );
END;";

            using (var createCommand =
                new SqlCommand(createTableSql, connection) { CommandTimeout = 30 })
            {
                await createCommand.ExecuteNonQueryAsync(token);
            }

            const string insertSql = @"
INSERT INTO dbo.SqlClientQuickstart (Message)
OUTPUT INSERTED.Id
VALUES (@message);";

            string[] messages =
            {
                "Hello from Microsoft.Data.SqlClient",
                "Parameters keep values out of the SQL text"
            };

            int firstId = 0;
            using (var insertCommand =
                new SqlCommand(insertSql, connection) { CommandTimeout = 30 })
            {
                SqlParameter messageParameter =
                    insertCommand.Parameters.Add("@message", SqlDbType.NVarChar, 200);

                foreach (string message in messages)
                {
                    messageParameter.Value = message;
                    int id = (int)await insertCommand.ExecuteScalarAsync(token);
                    Console.WriteLine($"Inserted row {id}.");
                    firstId = firstId == 0 ? id : firstId;
                }
            }

            const string querySql = @"
SELECT Id, Message, CreatedAt
FROM dbo.SqlClientQuickstart
WHERE Id >= @firstId
ORDER BY Id;";

            using (var queryCommand =
                new SqlCommand(querySql, connection) { CommandTimeout = 30 })
            {
                queryCommand.Parameters.Add("@firstId", SqlDbType.Int).Value = firstId;

                using (SqlDataReader reader =
                    await queryCommand.ExecuteReaderAsync(token))
                {
                    while (await reader.ReadAsync(token))
                    {
                        Console.WriteLine(
                            $"{reader.GetInt32(0)}: {reader.GetString(1)} " +
                            $"(created {reader.GetDateTimeOffset(2):u})");
                    }
                }
            }
        }
    }
}
```

---

## Run the app

Run the app:

```console
dotnet run
```

The first time you run the app, the table is empty and the output looks like this:

```output
Inserted row 1.
Inserted row 2.
1: Hello from Microsoft.Data.SqlClient (created 2026-10-20 20:56:10Z)
2: Parameters keep values out of the SQL text (created 2026-10-20 20:56:10Z)
```

Your timestamps differ. Each later run adds two more rows, so the IDs continue from the last run.

If the app can't connect, it prints the SQL error number, the client connection ID, the error message, and the inner exception message, then exits with code 3. For common errors, see [Troubleshoot](#troubleshoot).

## How the code works

- `OpenAsync`, `ExecuteNonQueryAsync`, `ExecuteScalarAsync`, `ExecuteReaderAsync`, and `ReadAsync` all receive the same `CancellationToken`. Press Ctrl+C to cancel the work in progress.
- The insert command is created once and runs once for each message. Only the parameter value changes.
- The parameter types and sizes match the table columns. Parameters send values separately from the SQL text, which prevents those values from changing the command and helps SQL Server reuse query plans.
- `OUTPUT INSERTED.Id` returns the new identity value, so `ExecuteScalarAsync` returns the ID without a second query.
- `await using` disposes the commands, the reader, and the connection, even when an exception occurs. Disposing the connection returns it to the connection pool. The .NET Framework version uses `using` blocks, because these types don't implement `IAsyncDisposable` on .NET Framework.
- `SqlException.Number` identifies the error, and `ClientConnectionId` lets you match a client failure to server-side logs. For connection failures on Linux and macOS, the cause is often only in `InnerException`, such as a certificate validation failure behind the generic `error: 35 - An internal exception was caught`.

## Troubleshoot

| Error or symptom | What to check | More information |
| --- | --- | --- |
| `The certificate chain was issued by an authority that is not trusted` on Windows. On Linux and macOS, `error: 35 - An internal exception was caught`, followed by the inner exception `Certificate failed chain validation` with `UntrustedRoot`. | The server certificate isn't trusted by the client, or the server name in the connection string doesn't match the certificate. Install a trusted certificate. Use `TrustServerCertificate=true` only with a local development instance. | [Encryption and certificate validation in Microsoft.Data.SqlClient](encryption-and-certificate-validation.md#development-certificates-and-troubleshooting) |
| `SQL error 18456` with `Login failed for user` | The user name, password, or database name is wrong, or the login doesn't have access to the database. For Microsoft Entra ID, check that the identity was added as a database user. | [SqlClient troubleshooting guide](sqlclient-troubleshooting-guide.md#login-phase-errors) |
| `A network-related or instance-specific error occurred` with `error: 26`, `error: 40`, or `The wait operation timed out`. On Linux and macOS, a timeout shows `SQL error 258` with `error: 35`, followed by the inner exception `The connection attempt timed out`. | The server name or port is wrong, the server doesn't accept remote TCP connections, or a firewall blocks the port. For Azure SQL, check the server firewall rules. | [SqlClient troubleshooting guide](sqlclient-troubleshooting-guide.md#exceptions-when-connecting-to-sql-server) |
| `System.ArgumentException: Cannot find an authentication provider for 'ActiveDirectoryDefault'` | The connection string uses an `Active Directory` authentication mode, but the project doesn't reference `Microsoft.Data.SqlClient.Extensions.Azure`. Add the package with the same version as `Microsoft.Data.SqlClient`. | [Microsoft Entra authentication moves to an extension package in 7.0](microsoft-data-sql-client-release-notes.md#entra-auth-extension-package) |
| `CS0234: The type or namespace name 'Data' does not exist in the namespace 'Microsoft'` | The project doesn't reference the `Microsoft.Data.SqlClient` package. Run the `dotnet add package` command from the folder that contains the project file. | [Install, update, and deploy Microsoft.Data.SqlClient](download-microsoft-sqlclient-data-provider.md#install-the-core-package) |
| Restore reports `NU1902` or `NU1903` for a transitive package | A package that the driver depends on has a published advisory. | [Actions when packages with known vulnerabilities are reported](/nuget/concepts/auditing-packages#actions-when-packages-with-known-vulnerabilities-are-reported) |

## Clean up resources

The table stays in your database after the app exits. If you don't plan to continue with the ASP.NET Core quickstart, which uses the same table, drop it with any query tool:

```sql
DROP TABLE IF EXISTS dbo.SqlClientQuickstart;
```

Then delete the project folder. From the parent folder, for PowerShell:

```powershell
Remove-Item -Recurse -Force SqlClientQuickstart
```

For Bash:

```bash
rm -rf SqlClientQuickstart
```

Clear the environment variable so the connection string doesn't stay in your shell session. For PowerShell, run `Remove-Item Env:\SQL_CONNECTION_STRING`. For Bash, run `unset SQL_CONNECTION_STRING`.

## Use the pattern in an application

Keep these boundaries when you move the sample into an API, service, desktop app, or background worker:

- Load connection information through the app's configuration system.
- Open one connection for a short unit of work, then dispose it.
- Pass a `CancellationToken` through open, command, and reader calls.
- Set command timeouts based on the operation.
- Use parameters for every value that comes from outside the SQL statement.
- Log `SqlException.Number` and `ClientConnectionId` without logging credentials or access tokens.
- Add retries only for transient failures and only when repeating the operation is safe.

## Next steps

- [Connect to a data source with Microsoft.Data.SqlClient](connecting-to-data-source.md)
- [Execute a command](execute-command.md)
- [Configure parameters with Microsoft.Data.SqlClient](configure-parameters.md)
- [Retrieve data by using a DataReader](retrieve-data-by-datareader.md)
- [Handle errors and messages](handle-errors-messages.md)
- [Production baseline for Azure SQL](microsoft-ado-net-sql-server.md#production-baseline-for-azure-sql)
