---
title: Connect to and query Azure SQL Database using .NET and the Microsoft.Data.SqlClient library
description: Learn how to connect to a database in Azure SQL Database and query data using .NET
author: alexwolfmsft
ms.author: alexwolf
ms.custom: passwordless-dotnet
ms.date: 04/12/2023
ms.service: sql-database
ms.subservice: security
ms.topic: quickstart
monikerRange: "= azuresql || = azuresql-db"
---

# Connect to and query Azure SQL Database using .NET and the Microsoft.Data.SqlClient library

This quickstart describes how to connect an application to a database in Azure SQL Database and perform queries using .NET and the [Microsoft.Data.SqlClient](https://www.nuget.org/packages/Microsoft.Data.SqlClient) library. This quickstart follows the recommended passwordless approach to connect to the database. You can learn more about passwordless connections on the [passwordless hub](/azure/developer/intro/passwordless-overview).

## Prerequisites

* An [Azure subscription](https://azure.microsoft.com/free/dotnet/).
* An Azure SQL database configured with Azure Active Directory (Azure AD) authentication. You can create one using the [Create database quickstart](./single-database-create-quickstart.md).
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

> [!WARNING]
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

For local development, update the `environmentVariables` section of the `appsettings.Development.json` file with the correct Azure SQL connection string. Different connection string formats are required depending on which approach you choose.

## [Passwordless (Recommended)](#tab/passwordless)

The passwordless connection string includes a configuration value of `Authentication=Active Directory Default`, which instructs the app and the `Microsoft.Data.SqlClient` library to use a class called [`DefaultAzureCredential`](/dotnet/azure/sdk/authentication#defaultazurecredential) to connect to Azure SQL Database. `DefaultAzureCredential` is provided by the Azure Identity library on which the SQL client library depends.

 When the app runs locally, it authenticates via the user you're signed into Visual Studio with, or other local tools like the Azure CLI. Once the app deploys to Azure, the same code discovers and applies the managed identity that is associated with the hosted app, which you'll configure later. The [Azure Identity library overview](/dotnet/api/overview/azure/Identity-readme#defaultazurecredential) explains the order and locations in which `DefaultAzureCredential` looks for credentials.

```json
"environmentVariables": {
    "ASPNETCORE_ENVIRONMENT": "Development",
    "AZURE_SQL_CONNECTIONSTRING": "Server=tcp:<database-server-name>.database.windows.net;Database=<database-name>;Authentication=Active Directory Default;"
}
```

> [!NOTE]
> Passwordless connection strings are safe to commit to source control, since they do not contain any secrets such as usernames, passwords, or access keys.

## [SQL Authentication](#tab/sql-auth)

Connect to Azure SQL Database with SQL Authentication using the following connection string:

```json
"environmentVariables": {
    "ASPNETCORE_ENVIRONMENT": "Development",
    "AZURE_SQL_CONNECTIONSTRING": "Server=tcp:<database-server-name>.database.windows.net,1433;Initial Catalog=<database-name>;Persist Security Info=False;User ID=<user-id>;Password=<password>;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}
```

> [!WARNING]
> Use caution when managing connection strings that contain secrets such as usernames, passwords, or access keys. These secrets should not be committed to source control or placed in unsecure locations where they might be accessed by unintended users. During local development you will often connect to a local database that doesn't require storing secrets.


---

## Add the code to connect to Azure SQL Database

Add the following sample code to the bottom of the `Program.cs` file above `app.Run()`. This code performs the following important steps:

* Retrieves the passwordless connection string from the environment variables
* Creates a `Persons` table in the database during startup (for testing scenarios only)
* Creates an HTTP GET endpoint to retrieve all records stored in the `Persons` table
* Creates an HTTP POST endpoint to add new records to the `Persons` table

```csharp
string connectionString = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");

try
{
    // Table would be created ahead of time in production
    using var conn = new SqlConnection(connectionString);
    conn.Open();

    var command = new SqlCommand(
        "CREATE TABLE Persons (ID int NOT NULL PRIMARY KEY IDENTITY, FirstName varchar(255), LastName varchar(255));",
        conn);
    using SqlDataReader reader = command.ExecuteReader();
} catch(Exception e)
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
        $"INSERT INTO Persons (firstName, lastName) VALUES ('{person.FirstName}', '{person.LastName}')",
        conn);

    using SqlDataReader reader = command.ExecuteReader();
})
.WithName("CreatePerson")
.WithOpenApi();
```

Finally, add the `Person` class to the bottom of the `Program.cs` file. This class represents a single record in the database's `Persons` table.

```csharp
public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
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
1. Select the green **+** icon to create a new App Service to deploy to and enter the following values:

    * **Name**: Leave the default value.
    * **Subscription name**: Select the subscription to deploy to.
    * **Resource group**: Select **New** and create a new resource group called *msdocs-dotnet-sql*.
    * **Hosting Plan**: Select **New** to open the hosting plan dialog. Leave the default values and select **OK**.
    * Select **Create** to close the original dialog. Visual Studio creates the App Service resource in Azure.

        :::image type="content" source="media/passwordless-connections/create-app-service-small.png" lightbox="media/passwordless-connections/create-app-service.png" alt-text="A screenshot showing how to deploy with Visual Studio.":::

1. Once the resource is created, make sure it's selected in the list of app services, and then select **Next**.
1. On the **API Management** step, select the **Skip this step** checkbox at the bottom and then choose **Finish**.

1. Select **Publish** in the upper right of the publishing profile summary to deploy the app to Azure.

When the deployment finishes, Visual Studio launches the browser to display the hosted app, but at this point the app doesn't work correctly on Azure. You still need to configure the secure connection between the App Service and the SQL database to retrieve your data.

## Connect the App Service to Azure SQL Database

## [Passwordless](#tab/paswordless)

[!INCLUDE [passwordless-connect-azure-sql](../includes/passwordless-connect-azure-sql.md)]

## [SQL Authentication](#tab/sql-auth)

[!INCLUDE [password-connect-azure-sql](../includes/password-connect-azure-sql.md)]

---

## Test the deployed application

Browse to the URL of the app to test that the connection to Azure SQL Database is working. You can locate the URL of your app on the App Service overview page. Append the `/person` path to the end of the URL to browse to the same endpoint you tested locally.

The person you created locally should display in the browser. Congratulations! Your application is now connected to Azure SQL Database in both local and hosted environments.