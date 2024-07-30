---
title: Connect to and query Azure SQL Database using .NET and the Microsoft.Data.SqlClient library
description: Learn how to connect to a database in Azure SQL Database and query data using .NET
author: alexwolfmsft
ms.author: alexwolf
ms.reviewer: mathoma
ms.custom: passwordless-dotnet
ms.date: 07/11/2023
ms.service: azure-sql-database
ms.subservice: security
ms.topic: quickstart
monikerRange: "= azuresql || = azuresql-db"
---

# Connect to and query Azure SQL Database using .NET and the Microsoft.Data.SqlClient library
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

This quickstart describes how to connect an application to a database in Azure SQL Database and perform queries using .NET and the [Microsoft.Data.SqlClient](https://www.nuget.org/packages/Microsoft.Data.SqlClient) library. This quickstart follows the recommended passwordless approach to connect to the database. You can learn more about passwordless connections on the [passwordless hub](/azure/developer/intro/passwordless-overview).

## Prerequisites

* An [Azure subscription](https://azure.microsoft.com/free/dotnet/).
* An Azure SQL database configured for authentication with Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)). You can create one using the [Create database quickstart](./single-database-create-quickstart.md).
* The latest version of the [Azure CLI](/cli/azure/get-started-with-azure-cli).
* [Visual Studio](https://visualstudio.microsoft.com/vs/) or later with the **ASP.NET and web development** workload.
* [.NET 7.0](https://dotnet.microsoft.com/download) or later.

## Configure the database

[!INCLUDE [passwordless-configure-server-networking](../includes/passwordless-configure-server-networking.md)]

## Create the project

For the steps ahead, create a .NET Minimal Web API using either the .NET CLI or Visual Studio 2022.

## [Visual Studio](#tab/visual-studio)

1. In the Visual Studio menu, navigate to **File** > **New** > **Project..**.

1. In the dialog window, enter *ASP.NET* into the project template search box and select the ASP.NET Core Web API result. Choose **Next** at the bottom of the dialog.

1. For the **Project Name**, enter *DotNetSQL*. Leave the default values for the rest of the fields and select **Next**.

1. For the **Framework**, select .NET 7.0 and uncheck **Use controllers (uncheck to use minimal APIs)**. This quickstart uses a Minimal API template to streamline endpoint creation and configuration. 

1. Choose **Create**. The new project opens inside the Visual Studio environment.

## [.NET CLI](#tab/net-cli)

1. In a console window (such as cmd, PowerShell, or Bash), use the `dotnet new` command to create a new Web API app with the name *DotNetSQL*. This command creates a simple "Hello World" C# project with a single source file: *Program.cs*.

   ```dotnetcli
   dotnet new web -o DotNetSQL
   ```

1. Navigate into the newly created *DotNetSQL* directory and open the project in Visual Studio.

---

## Add the Microsoft.Data.SqlClient library

To connect to Azure SQL Database by using .NET, install `Microsoft.Data.SqlClient`. This package acts as a data provider for connecting to databases, executing commands, and retrieving results.

> [!NOTE]
> Make sure to install `Microsoft.Data.SqlClient` and not `System.Data.SqlClient`. `Microsoft.Data.SqlClient` is a newer version of the SQL client library that provides additional capabilities.

## [Visual Studio](#tab/visual-studio)

1. In the **Solution Explorer** window, right-click the project's **Dependencies** node and select **Manage NuGet Packages**.

1. In the resulting window, search for *SqlClient*. Locate the `Microsoft.Data.SqlClient` result and select **Install**.

## [.NET CLI](#tab/net-cli)

Use the following command to install the `Microsoft.Data.SqlClient` package:

```dotnetcli
dotnet add package Microsoft.Data.SqlClient
```

---

## Configure the connection string

## [Passwordless (Recommended)](#tab/passwordless)

For local development with passwordless connections to Azure SQL Database, add the following `ConnectionStrings` section to the `appsettings.json` file. Replace the `<database-server-name>` and `<database-name>` placeholders with your own values.

```json
"ConnectionStrings": {
    "AZURE_SQL_CONNECTIONSTRING": "Server=tcp:<database-server-name>.database.windows.net,1433;Initial Catalog=<database-name>;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=\"Active Directory Default\";"
}
```

The passwordless connection string sets a configuration value of `Authentication="Active Directory Default"`, which instructs the `Microsoft.Data.SqlClient` library to connect to Azure SQL Database using a class called [`DefaultAzureCredential`](/dotnet/azure/sdk/authentication#defaultazurecredential). `DefaultAzureCredential` enables passwordless connections to Azure services and is provided by the Azure Identity library on which the SQL client library depends. `DefaultAzureCredential` supports multiple authentication methods and determines which to use at runtime for different environments.

For example, when the app runs locally, `DefaultAzureCredential` authenticates via the user you're signed into Visual Studio with, or other local tools like the Azure CLI. Once the app deploys to Azure, the same code discovers and applies the managed identity that is associated with the hosted app, which you'll configure later. The [Azure Identity library overview](/dotnet/api/overview/azure/Identity-readme#defaultazurecredential) explains the order and locations in which `DefaultAzureCredential` looks for credentials.

> [!NOTE]
> Passwordless connection strings are safe to commit to source control, since they don't contain secrets such as usernames, passwords, or access keys.

## [SQL Authentication](#tab/sql-auth)

For local development with SQL Authentication to Azure SQL Database, add the following `ConnectionStrings` section to the `appsettings.json` file. Replace the `<database-server-name>`, `<database-name>`, `<user-id>`, and `<password>` placeholders with your own values.

```json
"ConnectionStrings": {
    "AZURE_SQL_CONNECTIONSTRING": "Server=tcp:<database-server-name>.database.windows.net,1433;Initial Catalog=<database-name>;Persist Security Info=False;User ID=<user-id>;Password=<password>;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;"
}
```

> [!WARNING]
> Use caution when managing connection strings that contain secrets such as usernames, passwords, or access keys. These secrets shouldn't be committed to source control or placed in unsecure locations where they might be accessed by unintended users. During local development, on a real app, you'll generally connect to a local database that doesn't require storing secrets or connecting directly to Azure.

---

## Add the code to connect to Azure SQL Database

Replace the contents of the `Program.cs` file with the following code, which performs the following important steps:

* Retrieves the passwordless connection string from `appsettings.json`
* Creates a `Persons` table in the database during startup (for testing scenarios only)
* Creates an HTTP GET endpoint to retrieve all records stored in the `Persons` table
* Creates an HTTP POST endpoint to add new records to the `Persons` table

```csharp
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// For production scenarios, consider keeping Swagger configurations behind the environment check
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

string connectionString = app.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")!;

try
{
    // Table would be created ahead of time in production
    using var conn = new SqlConnection(connectionString);
    conn.Open();

    var command = new SqlCommand(
        "CREATE TABLE Persons (ID int NOT NULL PRIMARY KEY IDENTITY, FirstName varchar(255), LastName varchar(255));",
        conn);
    using SqlDataReader reader = command.ExecuteReader();
}
catch (Exception e)
{
    // Table may already exist
    Console.WriteLine(e.Message);
}

app.MapGet("/Person", () => {
    var rows = new List<string>();

    using var conn = new SqlConnection(connectionString);
    conn.Open();

    var command = new SqlCommand("SELECT * FROM Persons", conn);
    using SqlDataReader reader = command.ExecuteReader();

    if (reader.HasRows)
    {
        while (reader.Read())
        {
            rows.Add($"{reader.GetInt32(0)}, {reader.GetString(1)}, {reader.GetString(2)}");
        }
    }

    return rows;
})
.WithName("GetPersons")
.WithOpenApi();

app.MapPost("/Person", (Person person) => {
    using var conn = new SqlConnection(connectionString);
    conn.Open();

    var command = new SqlCommand(
        "INSERT INTO Persons (firstName, lastName) VALUES (@firstName, @lastName)",
        conn);

    command.Parameters.Clear();
    command.Parameters.AddWithValue("@firstName", person.FirstName);
    command.Parameters.AddWithValue("@lastName", person.LastName);

    using SqlDataReader reader = command.ExecuteReader();
})
.WithName("CreatePerson")
.WithOpenApi();

app.Run();
```

Finally, add the `Person` class to the bottom of the `Program.cs` file. This class represents a single record in the database's `Persons` table.

```csharp
public class Person
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}
```

## Run and test the app locally

The app is ready to be tested locally. Make sure you're signed in to Visual Studio or the Azure CLI with the same account you set as the admin for your database.

1) Press the run button at the top of Visual Studio to launch the API project.

1) On the Swagger UI page, expand the POST method and select **Try it**.

1) Modify the sample JSON to include values for the first and last name. Select **Execute** to add a new record to the database. The API returns a successful response.

    :::image type="content" source="media/passwordless-connections/api-testing-small.png" lightbox="media/passwordless-connections/api-testing.png" alt-text="A screenshot showing how to test the API.":::

1) Expand the **GET** method on the Swagger UI page and select **Try it**. Choose **Execute**, and the person you just created is returned.

## Deploy to Azure App Service

The app is ready to be deployed to Azure. Visual Studio can create an Azure App Service and deploy your application in a single workflow.

1. Make sure the app is stopped and builds successfully.
1. In Visual Studio's **Solution Explorer** window, right-click on the top-level project node and select **Publish**.
1. In the publishing dialog, select **Azure** as the deployment target, and then select **Next**.
1. For the specific target, select **Azure App Service (Windows)**, and then select **Next**.
1. Select the **+** icon to create a new App Service to deploy to and enter the following values:

    * **Name**: Leave the default value.
    * **Subscription name**: Select the subscription to deploy to.
    * **Resource group**: Select **New** and create a new resource group called *msdocs-dotnet-sql*.
    * **Hosting Plan**: Select **New** to open the hosting plan dialog. Leave the default values and select **OK**.
    * Select **Create** to close the original dialog. Visual Studio creates the App Service resource in Azure.

        :::image type="content" source="media/passwordless-connections/create-app-service-small.png" lightbox="media/passwordless-connections/create-app-service.png" alt-text="A screenshot showing how to deploy with Visual Studio.":::

1. Once the resource is created, make sure it's selected in the list of app services, and then select **Next**.
1. On the **API Management** step, select the **Skip this step** checkbox at the bottom and then choose **Finish**.
1. On the Finish step, select **Close** if the dialog does not close automatically.

1. Select **Publish** in the upper right of the publishing profile summary to deploy the app to Azure.

When the deployment finishes, Visual Studio launches the browser to display the hosted app, but at this point the app doesn't work correctly on Azure. You still need to configure the secure connection between the App Service and the SQL database to retrieve your data.

## Connect the App Service to Azure SQL Database

## [Passwordless (Recommended)](#tab/passwordless)

[!INCLUDE [passwordless-connect-azure-sql](../includes/passwordless-connect-azure-sql.md)]

## [SQL Authentication](#tab/sql-auth)

[!INCLUDE [password-connect-azure-sql](../includes/password-connect-azure-sql.md)]

---

## Test the deployed application

1) Select the **Browse** button at the top of App Service overview page to launch the root url of your app.

2) Append the `/swagger/index.html` path to the URL to load the same Swagger test page you used locally.

3) Execute test GET and POST requests to verify that the endpoints work as expected.

> [!TIP]
> If you receive a 500 Internal Server error while testing, it may be due to your database networking configurations. Verify that your logical server is configured with the settings outlined in the [Configure the database](/azure/azure-sql/database/azure-sql-dotnet-quickstart#configure-the-database) section.

Congratulations! Your application is now connected to Azure SQL Database in both local and hosted environments.

[!INCLUDE [passwordless-resource-cleanup](../includes/passwordless-resource-cleanup.md)]
