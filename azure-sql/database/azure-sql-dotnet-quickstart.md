---
title: Connect to and query Azure SQL Database using .NET and the Microsoft.Data.SqlClient library
description: Learn how to connect to a database in Azure SQL Database and query data using .NET
author: alexwolfmsft
ms.author: alexwolf
ms.custom: passwordless-dotnet
ms.date: 02/10/2023
ms.service: sql-database
ms.subservice: security
ms.topic: quickstart
monikerRange: "= azuresql || = azuresql-db"
---

# Connect to and query Azure SQL Database using .NET and the Microsoft.Data.SqlClient library

This quickstart describes how to connect an application to a database in Azure SQL Database and perform queries using .NET and the `Microsoft.Data.SqlClient` library. 

This quickstart follows the recommended passwordless approach to connect to the database without the use of connection strings. You can learn more about passwordless connections on the [passwordless hub](/azure/developer/intro/passwordless-overview).

## Prerequisites

* An [Azure subscription](https://azure.microsoft.com/free/dotnet/).
* A SQL database configured with Azure Active Directory (Azure AD) authentication. You can create one using the [Create database quickstart](/azure/azure-sql/database/single-database-create-quickstart).
* The latest version of the [Azure CLI](/cli/azure/get-started-with-azure-cli).
* [Visual Studio 2022(https://visualstudio.microsoft.com/vs/) or later.
* [.NET 7.0](https://dotnet.microsoft.com/download) or later.

## Configure the database

Secure, passwordless connections to Azure SQL Database with .NET require certain database configurations. Verify the following settings on your database server to properly connect to Azure SQL Database in both local and hosted environments:

1) For local development connections, make sure your [logical server](logical-servers.md) has a firewall rule enabled to allow your client IP address to connect. You can configure firewall rules on the **Networking** page of your server by selecting **Add your client IPv4 address(xx.xx.xx.xx)**.

    :::image type="content" source="media/passwordless-connections/configure-firewall-small.png" lightbox="media/passwordless-connections/configure-firewall.png" alt-text="A screenshot showing how to configure firewall rules.":::

1) The SQL Server must also have Azure AD authentication enabled with an admin account assigned. For local development connections, the admin account should be an account you can also log into Visual Studio or the Azure CLI with locally. You can verify whether your server has Azure AD authentication enabled on the **Azure Active Directory** page.

    :::image type="content" source="media/passwordless-connections/enable-active-directory-small.png" lightbox="media/passwordless-connections/enable-active-directory.png" alt-text="A screenshot showing how to enable Active Directory authentication.":::

1) If you're using a personal Azure account, make sure you have [Azure Active Directory setup and configured for Azure SQL](/azure/azure-sql/database/authentication-aad-configure) in order to assign your account as a SQL Server admin. If you're using a corporate account, Azure Active Directory will most likely already be configured for you.

## Create the project

For the steps ahead, create a .NET Minimal Web API using either the .NET CLI or Visual Studio 2022.

## [Visual Studio 2022(#tab/visual-studio)

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

## [Visual Studio 2022(#tab/visual-studio)

1. In the **Solution Explorer** window, right-click the project's **Dependencies** node and select **Manage NuGet Packages**.

1. In the resulting window, search for *SqlClient*. Locate the `Microsoft.Data.SqlClient` result and select **Install**.

## [.NET CLI](#tab/net-cli)

Use the following command to install the `Microsoft.Data.SqlClient` package:

```dotnetcli
dotnet add package Microsoft.Data.SqlClient
```

---

## Add the code to connect to Azure SQL

The `Microsoft.Data.SqlClient` library uses a class called `DefaultAzureCredential` to implement passwordless connections to Azure SQL Database, which you can learn more about on the [DefaultAzureCredential overview](/dotnet/azure/sdk/authentication#defaultazurecredential). **DefaultAzureCredential** is provided by the Azure Identity library on which the SQL client library depends.

`DefaultAzureCredential` supports multiple authentication methods and determines which to use at runtime. This approach enables your app to use different authentication methods in different environments (local vs. production) without implementing environment-specific code. The [Azure Identity library overview](/dotnet/api/overview/azure/Identity-readme#defaultazurecredential) explains the order and locations in which `DefaultAzureCredential` looks for credentials.

Complete the following steps to connect to Azure SQL Database by using the SqlClient library and `DefaultAzureCredential`:

1) Update the `environmentVariables` section of the `launchSettings.json` file to match the following code. Remember to update the `<your database-server-name>` and `<your-database-name>` placeholders.

    The passwordless connection string includes a configuration value of `Authentication=Active Directory Default`, which instructs the app to use `DefaultAzureCredential` to connect to Azure services. This functionality is implemented internally by the `Microsoft.Data.SqlClient` library. When the app runs locally, it authenticates with the user you're signed into Visual Studio with. Once the app deploys to Azure, the same code discovers and applies the managed identity that is associated with the hosted app, which you'll configure later.
    
    > [!NOTE]
    > Passwordless connection strings are safe to commit to source control, since they do not contain any secrets such as usernames, passwords, or access keys.
    
    ```json
    "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development",
        "AZURE_SQL_CONNECTIONSTRING": "Server=tcp:<your-database-servername>.database.windows.net;Database=<your-database-name>;Authentication=Active Directory Default;"
    }
    ```

1) Add the following sample code to the bottom of the `Program.cs` file above `app.Run()`. This code performs the following important steps:

    * Retrieves the passwordless connection string from the environment variables
    * Creates a Person table in the database during startup (for testing scenarios only)
    * Creates an endpoint to retrieve the Person records stored in the database
    * Creates an endpoint to add new Person records to the database

    ```csharp
    string connectionString = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
    
    try
    {
        // Table would be created ahead of time in production
        var conn = new SqlConnection(connectionString);
        conn.Open();
    
        var command = new SqlCommand(
            "CREATE TABLE Persons (ID int NOT NULL PRIMARY KEY IDENTITY, FirstName varchar(255), LastName varchar(255));",
            conn);
        SqlDataReader reader = command.ExecuteReader();
    
        reader.Close();
    } catch(Exception e)
    {
        // Table may already exist
        Console.WriteLine(e.Message);
    }
    
    app.MapGet("/Person", () =>
    {
        var rows = new List<string>();

        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();

            var command = new SqlCommand("SELECT * FROM Persons", conn);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    rows.Add($"{reader.GetInt32(0)}, {reader.GetString(1)}, {reader.GetString(2)}");
                }
            }

            reader.Close();
        };

        return rows;
    })
    .WithName("GetPersons")
    .WithOpenApi();
    
    app.MapPost("/Person", (Person person) =>
    {
        using(var conn = new SqlConnection(connectionString)) 
        {
            conn.Open();
        
            var command = new SqlCommand(
                  $"INSERT INTO Persons (firstName, lastName) VALUES ('{person.FirstName}', '{person.LastName}')",
                  conn);
        
            SqlDataReader reader = command.ExecuteReader();
            reader.Close();
        };
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
1. In Visual Studio's **Solution Explorer** window, right select on the top-level project node and select **Publish**.
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

## Connect the App Service to Azure SQL

The following steps are required to connect the App Service instance to Azure SQL:

1) Create a managed identity for the App Service. The `Microsoft.Data.SqlClient` library included in your app will automatically discover the managed identity, just like it discovered your local Visual Studio user.
2) Create a SQL database user and associate it with the App Service managed identity.
3) Assign SQL roles to the database user that allow for read, write, and potentially other permissions.

There are multiple tools available to implement these steps:

## [Service Connector (Recommended)](#tab/service-connector)

Service Connector is a tool that streamlines authenticated connections between different services in Azure. Service Connector currently supports connecting an App Service to a SQL database via the Azure CLI using the `az webapp connection create sql` command. This single command completes the three steps mentioned above for you.

```azurecli
az webapp connection create sql
-g <your-resource-group>
-n <your-app-service-name>
--tg <your-database-server-resource-group>
--server <your-database-server-name>
--database <your-database-name>
--system-identity
```

You can verify the changes made by Service Connector on the App Service settings.

1) Navigate to the **Identity** page for your App Service. Under the **System assigned** tab, the **Status** should be set to **On**. This value means that a system-assigned managed identity was enabled for your app.

2) Navigate to the **Configuration** page for your App Service. Under the **Connection strings** tab, you should see a connection string called **AZURE_SQL_CONNECTIONSTRING**. Select the **Click to show value** text to view the generated passwordless connection string. The name of this connection string aligns with the one you configured in your app, so it will be discovered automatically when running in Azure.

## [Azure portal](#tab/azure-portal)

The Azure portal allows you to work with managed identities and run queries against Azure SQL Database. Complete the following steps to create a passwordless connection from your App Service instance to Azure SQL:

### Create the managed identity

1) In the Azure portal, navigate to your App Service and select **Identity** on the left navigation.

2) On the identity page, make sure the **Enable system-assigned managed identity** option is enabled. When this setting is enabled, a system-assigned managed identity is created with the same name as your App Service. System-assigned identities are tied to the service instance and are destroyed with the app when it's deleted.

### Create the database user and assign roles

1) In the Azure portal, browse to your SQL database and select **Query editor (preview)**.

2) Select **Continue as `<your-username>`** on the right side of the screen to sign into the database using your account.

3) On the query editor view, run the following T-SQL commands:

    ```sql
    CREATE USER <your-app-service-name> FROM EXTERNAL PROVIDER;
    ALTER ROLE db_datareader ADD MEMBER <your-app-service-name>;
    ALTER ROLE db_datawriter ADD MEMBER <your-app-service-name>;
    ALTER ROLE db_ddladmin ADD MEMBER <your-app-service-name>;
    GO
    ```

    :::image type="content" source="media/passwordless-connections/query-editor-small.png" lightbox="media/passwordless-connections/query-editor.png" alt-text="A screenshot showing how to use the Azure Query editor.":::

    This SQL script creates a SQL database user that maps back to the managed identity of your App Service instance. It also assigns the necessary SQL roles to the user to allow your app to read, write, and modify the data and schema of your database. After this step is completed, your services are connected.

---

## Test the deployed application

Browse to the URL of the app to test that the connection to Azure SQL Database is working. You can locate the URL of your app on the App Service overview page. Append the `/person` path to the end of the URL to browse to the same endpoint you tested locally.

The person you created locally should display in the browser. Congratulations! Your application is now connected to Azure SQL Database in both local and hosted environments.
