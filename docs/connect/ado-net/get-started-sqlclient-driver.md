---
title: Use Microsoft.Data.SqlClient in a .NET app
description: Create a .NET console app that connects to SQL database in Microsoft Fabric, Azure SQL, or SQL Server, then writes and reads data asynchronously.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, davidengel, paulmedynski, cmalhotra
ms.date: 09/16/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: quickstart
dev_langs:
  - csharp
ai-usage: ai-assisted
---

# Use Microsoft.Data.SqlClient in a .NET app

In this quickstart, you create a .NET console application that:

- Reads its connection string from the environment instead of source code.
- Opens a connection asynchronously.
- Creates a table if it doesn't exist.
- Inserts a row with a parameterized command.
- Reads rows with a parameterized query.
- Handles SQL and cancellation errors.

The example uses Microsoft.Data.SqlClient 7.0.3, the current stable release.

## Prerequisites

You need the [.NET 10 SDK or a later supported .NET SDK](https://dotnet.microsoft.com/download).

[!INCLUDE [prereq-create-sql-database](../../includes/paragraph-content/prereq-create-sql-database.md)]

The quickstart creates its own table, so sample data isn't required. The database identity needs permission to connect and to create, insert into, and select from a table.

For SQL database in Microsoft Fabric, [copy the server and database names from the SQL database item](/fabric/database/sql/connect#find-sql-connection-string). Don't use the SQL analytics endpoint. The identity needs Read item permission, which a workspace role or item permission can provide. For more information, see [Authentication in SQL database](/fabric/database/sql/authentication). SQL authentication isn't supported.

For Azure SQL Database, [configure Microsoft Entra ID authentication and database access](/azure/azure-sql/database/authentication-aad-configure).

## Create the project

Run these commands:

```console
dotnet new console --framework net10.0 --name SqlClientQuickstart
cd SqlClientQuickstart
dotnet add package Microsoft.Data.SqlClient --version 7.0.3
dotnet add package Microsoft.Data.SqlClient.Extensions.Azure --version 7.0.3
```

The extension package supplies driver-provided Microsoft Entra ID authentication modes. An application that uses only Windows integrated authentication or SQL authentication can omit `Microsoft.Data.SqlClient.Extensions.Azure`.

## Configure the connection

Set the `SQL_CONNECTION_STRING` environment variable for your database. Don't put a password, access token, or production connection string in source code.

Choose one of these starting points and replace the placeholders.

### Fabric SQL or Azure SQL with passwordless authentication

Sign in with an identity in Microsoft Entra ID that has access to the database. For local development, use a developer tool such as the Azure CLI:

```console
az login
```

Copy the exact server and database names from the SQL database item in Fabric or the Azure SQL database. For PowerShell:

```powershell
$env:SQL_CONNECTION_STRING = 'Server=tcp:<server>,1433;Database=<database>;Authentication=Active Directory Default;Encrypt=Strict;MultiSubnetFailover=true;Connect Timeout=30'
```

For Bash:

```bash
export SQL_CONNECTION_STRING='Server=tcp:<server>,1433;Database=<database>;Authentication=Active Directory Default;Encrypt=Strict;MultiSubnetFailover=true;Connect Timeout=30'
```

For an application hosted in Azure that connects to Azure SQL, grant its managed identity database access, then use `Authentication=Active Directory Managed Identity`. For other Microsoft Entra ID options, see [Microsoft Entra ID authentication](sql/azure-active-directory-authentication.md).

### SQL Server over TCP

Use the server, port, database, and login from your existing SQL Server or the setup guide that you followed. The following SQL authentication example is for a local development container. For PowerShell:

```powershell
$env:SQL_CONNECTION_STRING = 'Server=tcp:<server>,1433;Database=<database>;User ID=<user_id>;Password=<password>;Encrypt=true;TrustServerCertificate=true;Connect Timeout=30'
```

For Bash:

```bash
export SQL_CONNECTION_STRING='Server=tcp:<server>,1433;Database=<database>;User ID=<user_id>;Password=<password>;Encrypt=true;TrustServerCertificate=true;Connect Timeout=30'
```

> [!CAUTION]
> `TrustServerCertificate=true` skips server certificate validation. Use it only with a local development instance that doesn't have a trusted certificate. For shared or production SQL Server instances, install a certificate that the client trusts, use the server name on that certificate, and remove `TrustServerCertificate=true`.

If the environment supports Windows integrated authentication or Kerberos, replace `User ID` and `Password` with `Integrated Security=true`. For setup requirements, see [SQL Server authentication](sql/authentication-sql-server.md).

## Add the application code

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

    using (var createCommand =
        new SqlCommand(createTableSql, connection) { CommandTimeout = 30 })
    {
        await createCommand.ExecuteNonQueryAsync(cancellation.Token);
    }

    const string insertSql = """
        INSERT INTO dbo.SqlClientQuickstart (Message)
        OUTPUT INSERTED.Id
        VALUES (@message);
        """;

    int insertedId;
    using (var insertCommand =
        new SqlCommand(insertSql, connection) { CommandTimeout = 30 })
    {
        insertCommand.Parameters.Add(
            new SqlParameter("@message", SqlDbType.NVarChar, 200)
            {
                Value = "Hello from Microsoft.Data.SqlClient"
            });

        object? result =
            await insertCommand.ExecuteScalarAsync(cancellation.Token);
        insertedId = Convert.ToInt32(result);
    }

    const string querySql = """
        SELECT Id, Message, CreatedAt
        FROM dbo.SqlClientQuickstart
        WHERE Id = @id
        ORDER BY Id;
        """;

    using var queryCommand =
        new SqlCommand(querySql, connection) { CommandTimeout = 30 };
    queryCommand.Parameters.Add(
        new SqlParameter("@id", SqlDbType.Int) { Value = insertedId });

    await using SqlDataReader reader =
        await queryCommand.ExecuteReaderAsync(cancellation.Token);

    while (await reader.ReadAsync(cancellation.Token))
    {
        Console.WriteLine(
            $"{reader.GetInt32(0)}: {reader.GetString(1)} " +
            $"at {reader.GetDateTimeOffset(2):O}");
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
    return 3;
}
```

The parameter types and sizes match the table columns. Parameters send values separately from SQL text, which prevents those values from changing the command syntax and helps SQL Server reuse query plans.

`await using` disposes the reader and connection even when an exception occurs. Disposing the connection returns its physical connection to the connection pool instead of keeping one connection open for the life of the application.

## Run the application

Run the application:

```console
dotnet run
```

The application prints the row that it inserted:

```output
1: Hello from Microsoft.Data.SqlClient at <timestamp>
```

The identity value and timestamp differ on each database.

If the connection fails, use the SQL error number and client connection ID from the error output. Check the server and database names, network access, database permissions, authentication setup, and certificate configuration. Don't add `TrustServerCertificate=true` to an Azure SQL or production connection as a general connection fix.

## Use the pattern in an application

Keep these boundaries when you move the sample into an API, service, desktop application, or background worker:

- Load connection information through the application's configuration system.
- Open one connection for a short unit of work, then dispose it.
- Pass a `CancellationToken` through open, command, and reader calls.
- Set command timeouts based on the operation.
- Use parameters for every value that comes from outside the SQL statement.
- Log `SqlException.Number` and `ClientConnectionId` without logging credentials or access tokens.
- Add retries only for transient failures and only when repeating the operation is safe.

## Next steps

- [Choose and build a connection string](connection-strings.md).
- [Configure connection behavior](connection-options.md).
- [Manage the connection lifecycle](connecting-to-data-source.md).
- [Use SQL Server connection pooling](sql-server-connection-pooling.md).
- [Configure retry logic](configurable-retry-logic.md).
- [Use commands and parameters](commands-parameters.md).
