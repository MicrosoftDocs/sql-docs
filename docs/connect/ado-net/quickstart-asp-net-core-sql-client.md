---
title: "Quickstart: Use Microsoft.Data.SqlClient in an ASP.NET Core App"
description: Build an ASP.NET Core minimal API that reads its connection string from configuration, registers a data class with dependency injection, and writes and reads SQL data with Microsoft.Data.SqlClient.
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

# Quickstart: Use Microsoft.Data.SqlClient in an ASP.NET Core app

In this quickstart, you build an ASP.NET Core minimal API with Microsoft.Data.SqlClient 7.1.1.

You start with an empty folder, the .NET SDK, and a SQL Server or Azure SQL database that you can reach from your computer. You finish with a web API that adds a message to a table and reads it back by ID.

Along the way, the app:

- Reads its connection string from ASP.NET Core configuration. For local development, the value comes from user secrets, so it stays out of your project folder.
- Registers a data class with dependency injection.
- Opens and disposes a connection for each request, which lets the default connection pool reuse physical connections.
- Passes the request's `CancellationToken` to every database call.

The app uses the same `dbo.SqlClientQuickstart` table as [Quickstart: Connect and query with Microsoft.Data.SqlClient](get-started-sqlclient-driver.md). You can do either quickstart first.

## Prerequisites

- The [.NET 10 SDK or a later supported .NET SDK](https://dotnet.microsoft.com/download). The SDK runs on Windows, Linux, and macOS.
- A database that you can connect to. To create one, see the [prerequisites in the console quickstart](get-started-sqlclient-driver.md#prerequisites).
- `curl` or PowerShell to send requests to the API.

Current ASP.NET Core runs on .NET, not .NET Framework. To use the driver from .NET Framework, see [.NET Framework projects](download-microsoft-sqlclient-data-provider.md#net-framework-projects).

The database identity needs permission to connect and to create, insert into, and select from a table.

## Create the project

In an empty folder, run these commands:

```console
dotnet new web --framework net10.0 --name SqlClientWebQuickstart
cd SqlClientWebQuickstart
dotnet add package Microsoft.Data.SqlClient --version 7.1.1
dotnet add package Microsoft.Data.SqlClient.Extensions.Azure --version 7.1.1
dotnet user-secrets init
```

`dotnet user-secrets init` adds a `UserSecretsId` to the project file. ASP.NET Core loads user secrets only when the app runs in the `Development` environment, which is the default for `dotnet run`.

The extension package supplies the driver's Microsoft Entra authentication modes. If your app uses only SQL authentication, you can omit `Microsoft.Data.SqlClient.Extensions.Azure`. When you use both packages, keep their versions the same.

## Configure the connection

Store the connection string as `ConnectionStrings:Quickstart` in user secrets. User secrets are stored in your user profile, not in the project folder, so they aren't committed to source control. Don't put a password or production connection string in `appsettings.json`.

Choose one of these starting points and replace the placeholders. The `dotnet user-secrets set` commands use single quotes for PowerShell and Bash. In Command Prompt, use double quotes instead, because Command Prompt stores single quotes as part of the key and value.

### Azure SQL with Microsoft Entra ID

Sign in with a Microsoft Entra identity that has access to the database. For local development, use a developer tool such as the Azure CLI:

```console
az login
```

Then store the connection string. Copy the exact server and database names from the Azure SQL database. The same connection string works for SQL database in Microsoft Fabric when you use the server and database names from the SQL database item, as described in the [console quickstart](get-started-sqlclient-driver.md#prerequisites).

```console
dotnet user-secrets set 'ConnectionStrings:Quickstart' 'Server=tcp:<server>,1433;Database=<database>;Authentication=Active Directory Default;Encrypt=Strict;Connect Timeout=30'
```

This connection string doesn't contain a secret. `Active Directory Default` uses your developer sign-in locally. When you deploy to Azure App Service or Azure Container Apps, grant the app's managed identity database access, change the value to `Authentication=Active Directory Managed Identity`, and set it in the host's configuration as `ConnectionStrings__Quickstart`. For other options, see [Microsoft Entra authentication with Microsoft.Data.SqlClient](sql/azure-active-directory-authentication.md).

### Local SQL Server with SQL authentication

Store the connection string for a local development instance that doesn't have a trusted certificate. Use single quotes to prevent PowerShell and Bash from expanding a `$` character in the password.

```console
dotnet user-secrets set 'ConnectionStrings:Quickstart' 'Server=tcp:<server>,1433;Database=<database>;User ID=<user-id>;Password=<password>;Encrypt=true;TrustServerCertificate=true;Connect Timeout=30'
```

> [!CAUTION]
> `TrustServerCertificate=true` skips server certificate validation. Use it only with a local development instance. For shared or production SQL Server instances, install a certificate that the client trusts and remove `TrustServerCertificate=true`. For more information, see [Encryption and certificate validation in Microsoft.Data.SqlClient](encryption-and-certificate-validation.md).

## Add the code

Replace the contents of `Program.cs` with this code:

```csharp
using System.Data;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<MessageStore>();

var app = builder.Build();

await app.Services.GetRequiredService<MessageStore>()
    .EnsureTableAsync(app.Lifetime.ApplicationStopping);

app.MapPost("/messages", async (
    NewMessage input, MessageStore store, CancellationToken cancellationToken) =>
{
    if (string.IsNullOrWhiteSpace(input.Text) || input.Text.Length > 200)
    {
        return Results.BadRequest("Text must be 1 to 200 characters.");
    }

    Message message = await store.AddAsync(input.Text, cancellationToken);
    return Results.Created($"/messages/{message.Id}", message);
});

app.MapGet("/messages/{id:int}", async (
    int id, MessageStore store, CancellationToken cancellationToken) =>
    await store.GetAsync(id, cancellationToken) is Message message
        ? Results.Ok(message)
        : Results.NotFound());

app.Run();

record NewMessage(string Text);

record Message(int Id, string Text, DateTimeOffset CreatedAt);

sealed class MessageStore(IConfiguration configuration)
{
    private readonly string _connectionString =
        configuration.GetConnectionString("Quickstart")
        ?? throw new InvalidOperationException(
            "Set the ConnectionStrings:Quickstart configuration value.");

    public async Task EnsureTableAsync(CancellationToken cancellationToken)
    {
        const string sql = """
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

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        await using var command =
            new SqlCommand(sql, connection) { CommandTimeout = 30 };
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task<Message> AddAsync(
        string text, CancellationToken cancellationToken)
    {
        const string sql = """
            INSERT INTO dbo.SqlClientQuickstart (Message)
            OUTPUT INSERTED.Id, INSERTED.Message, INSERTED.CreatedAt
            VALUES (@message);
            """;

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        await using var command =
            new SqlCommand(sql, connection) { CommandTimeout = 30 };
        command.Parameters.Add("@message", SqlDbType.NVarChar, 200).Value = text;

        await using SqlDataReader reader =
            await command.ExecuteReaderAsync(cancellationToken);
        await reader.ReadAsync(cancellationToken);
        return new Message(
            reader.GetInt32(0), reader.GetString(1), reader.GetDateTimeOffset(2));
    }

    public async Task<Message?> GetAsync(
        int id, CancellationToken cancellationToken)
    {
        const string sql = """
            SELECT Id, Message, CreatedAt
            FROM dbo.SqlClientQuickstart
            WHERE Id = @id;
            """;

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        await using var command =
            new SqlCommand(sql, connection) { CommandTimeout = 30 };
        command.Parameters.Add("@id", SqlDbType.Int).Value = id;

        await using SqlDataReader reader =
            await command.ExecuteReaderAsync(cancellationToken);
        return await reader.ReadAsync(cancellationToken)
            ? new Message(
                reader.GetInt32(0), reader.GetString(1), reader.GetDateTimeOffset(2))
            : null;
    }
}
```

## Run the app

Start the app on a fixed port:

```console
dotnet run --urls http://localhost:5000
```

At startup, the app creates the table if it doesn't exist. When the app is ready, the output includes these lines:

```output
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
```

If the app can't reach the database, it stops at startup with a `SqlException`. For common causes, see [Troubleshoot](#troubleshoot).

## Call the API

Leave the app running and open a second terminal. Add a message with a POST request, and then read it back by ID with a GET request. If the table already has rows, such as from the console quickstart, replace `1` in the GET request with the `id` that your POST request returns.

For PowerShell:

```powershell
Invoke-RestMethod -Method Post -Uri http://localhost:5000/messages -ContentType application/json -Body (@{ text = 'Hello from ASP.NET Core' } | ConvertTo-Json)
Invoke-RestMethod http://localhost:5000/messages/1
```

PowerShell formats each JSON response as a table. The first row is the `201 Created` response to the POST request, and the second row is the GET response:

```output
id text                    createdAt
-- ----                    ---------
 1 Hello from ASP.NET Core 2026-10-21T00:04:43.3588978+00:00
 1 Hello from ASP.NET Core 2026-10-21T00:04:43.3588978+00:00
```

Windows PowerShell 5.1 shows `createdAt` as the string that the API returns. PowerShell 7 converts it to a date in local time, such as `10/20/2026 7:04:43 PM`.

In Windows PowerShell, `curl` is an alias for `Invoke-WebRequest`, which doesn't accept the curl options. Use the PowerShell commands, or run `curl.exe` from Command Prompt.

For Command Prompt:

```cmd
curl -X POST http://localhost:5000/messages -H "Content-Type: application/json" -d "{\"text\":\"Hello from ASP.NET Core\"}"
curl http://localhost:5000/messages/1
```

Command Prompt doesn't treat single quotes as quote characters, so the JSON body uses double quotes escaped with backslashes. Each command prints the JSON response:

```output
{"id":1,"text":"Hello from ASP.NET Core","createdAt":"2026-10-21T00:05:15.4936763+00:00"}
{"id":1,"text":"Hello from ASP.NET Core","createdAt":"2026-10-21T00:05:15.4936763+00:00"}
```

For Bash:

```bash
curl -X POST http://localhost:5000/messages -H "Content-Type: application/json" -d '{"text":"Hello from ASP.NET Core"}'
curl http://localhost:5000/messages/1
```

```output
{"id":1,"text":"Hello from ASP.NET Core","createdAt":"2026-10-20T20:44:17.8634925+00:00"}
{"id":1,"text":"Hello from ASP.NET Core","createdAt":"2026-10-20T20:44:17.8634925+00:00"}
```

Your IDs and timestamps differ. If you ran the console quickstart first, the IDs continue from its rows. A request for an ID that doesn't exist returns `404 Not Found`. An empty message returns `400 Bad Request`.

Press Ctrl+C in the first terminal to stop the app.

## How the code works

- The app registers `MessageStore` as a singleton. It holds only the connection string, which it reads from `IConfiguration` once. It doesn't hold a connection.
- Each method creates a `SqlConnection`, opens it, runs one command, and disposes it with `await using`. Disposing returns the physical connection to the connection pool, and the next request reuses it. Connection pooling is on by default, so you don't need to configure it. For tuning options, see [SQL Server connection pooling with Microsoft.Data.SqlClient](sql-server-connection-pooling.md).
- ASP.NET Core binds the `CancellationToken` parameter to the request. If the client disconnects, the token cancels the open, the command, and the read.
- The `@message` and `@id` parameters match the column types and sizes. The API validates the message length before it sends the value to the database.
- `OUTPUT INSERTED.Id, INSERTED.Message, INSERTED.CreatedAt` returns the inserted row, including the server-generated ID and timestamp, without a second query.
- In other environments, ASP.NET Core reads the same `ConnectionStrings:Quickstart` key from environment variables, such as `ConnectionStrings__Quickstart`, or from another configuration provider, such as Azure Key Vault.

## Troubleshoot

| Error or symptom | What to check | More information |
| --- | --- | --- |
| `Set the ConnectionStrings:Quickstart configuration value.` | The app didn't find the connection string. Run `dotnet user-secrets list` in the project folder, and check that the app runs in the `Development` environment. | [Safe storage of app secrets in development in ASP.NET Core](/aspnet/core/security/app-secrets) |
| `System.ArgumentException: Cannot find an authentication provider for 'ActiveDirectoryDefault'` at startup | The connection string uses an `Active Directory` authentication mode, but the project doesn't reference `Microsoft.Data.SqlClient.Extensions.Azure`. Add the package with the same version as `Microsoft.Data.SqlClient`. | [Microsoft Entra authentication moves to an extension package in 7.0](microsoft-data-sql-client-release-notes.md#entra-auth-extension-package) |
| `SqlException` at startup | The app couldn't connect or create the table. The error number identifies the cause, such as 18456 for a failed login. | [Troubleshoot in the console quickstart](get-started-sqlclient-driver.md#troubleshoot) |
| `415 Unsupported Media Type` | The request doesn't set `Content-Type: application/json`. | [Minimal APIs quick reference](/aspnet/core/fundamentals/minimal-apis) |
| `Cannot bind parameter 'Headers'` | You ran the curl command in Windows PowerShell, where `curl` runs `Invoke-WebRequest`. Use the PowerShell commands in [Call the API](#call-the-api). | [Invoke-RestMethod](/powershell/module/microsoft.powershell.utility/invoke-restmethod) |
| `''' is an invalid start of a value` | You ran the Bash curl command in Command Prompt, which sends the single quotes as part of the body. Use the Command Prompt commands in [Call the API](#call-the-api). | [Call the API](#call-the-api) |

## Clean up resources

Drop the table with any query tool:

```sql
DROP TABLE IF EXISTS dbo.SqlClientQuickstart;
```

Remove the stored connection string. From the project folder, run:

```console
dotnet user-secrets clear
```

Then delete the project folder. From the parent folder, run the following command in PowerShell:

```powershell
Remove-Item -Recurse -Force SqlClientWebQuickstart
```

For Bash:

```bash
rm -rf SqlClientWebQuickstart
```

## Next steps

- [Connect to a data source with Microsoft.Data.SqlClient](connecting-to-data-source.md)
- [Execute a command](execute-command.md)
- [Configure parameters with Microsoft.Data.SqlClient](configure-parameters.md)
- [Retrieve data by using a DataReader](retrieve-data-by-datareader.md)
- [Handle errors and messages](handle-errors-messages.md)
- [Production baseline for Azure SQL](microsoft-ado-net-sql-server.md#production-baseline-for-azure-sql)
