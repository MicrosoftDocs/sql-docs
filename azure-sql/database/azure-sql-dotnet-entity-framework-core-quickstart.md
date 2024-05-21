---
title: Connect to and query Azure SQL Database using .NET and Entity Framework Core
description: Learn how to connect to a database in Azure SQL Database and query data using .NET and Entity Framework Core.
author: alexwolfmsft
ms.author: alexwolf
ms.reviewer: mathoma, vanto
ms.date: 05/17/2024
ms.service: sql-database
ms.subservice: security
ms.topic: quickstart
ms.custom:
  - passwordless-dotnet
monikerRange: "=azuresql || =azuresql-db"
---

# Connect to and query Azure SQL Database using .NET and Entity Framework Core

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

This quickstart describes how to connect an application to a database in Azure SQL Database and perform queries using .NET and Entity Framework Core. This quickstart follows the recommended passwordless approach to connect to the database. You can learn more about passwordless connections on the [passwordless hub](/azure/developer/intro/passwordless-overview).

## Prerequisites

- An [Azure subscription](https://azure.microsoft.com/free/dotnet/).
- A SQL database configured for authentication with Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)). You can create one using the [Create database quickstart](./single-database-create-quickstart.md).
- [.NET 7.0](https://dotnet.microsoft.com/download) or later.
- [Visual Studio](https://visualstudio.microsoft.com/vs/) or later with the **ASP.NET and web development** workload.
- The latest version of the [Azure CLI](/cli/azure/get-started-with-azure-cli).
- The latest version of the Entity Framework Core tools:
  * Visual Studio users should install the [Package Manager Console tools for Entity Framework Core](/ef/core/cli/powershell).
  * .NET CLI users should install the [.NET CLI tools for Entity Framework Core](/ef/core/cli/dotnet).

## Configure the database server

[!INCLUDE [passwordless-configure-server-networking](../includes/passwordless-configure-server-networking.md)]

## Create the project

The steps in this section create a .NET Minimal Web API by using either the .NET CLI or Visual Studio 2022.

## [Visual Studio](#tab/visual-studio)

1. In the Visual Studio menu bar, navigate to **File** > **New** > **Project..**.

1. In the dialog window, enter *ASP.NET* into the project template search box and select the ASP.NET Core Web API result. Choose **Next** at the bottom of the dialog.

1. For the **Project Name**, enter *DotNetSQL*. Leave the default values for the rest of the fields and select **Next**.

1. For the **Framework**, select .NET 7.0 and uncheck **Use controllers (uncheck to use minimal APIs)**. This quickstart uses a Minimal API template to streamline endpoint creation and configuration.

1. Choose **Create**. The new project opens inside the Visual Studio environment.

## [.NET CLI](#tab/dotnet-cli)

1. In a console window (such as cmd, PowerShell, or Bash), use the `dotnet new` command to create a new Web API app with the name *DotNetSQL*. This command creates a simple "Hello World" C# project with a single source file: *Program.cs*.

   ```dotnetcli
   dotnet new web -o DotNetSQL
   ```

1. Navigate into the newly created *DotNetSQL* directory and open the project in Visual Studio.

---

## Add Entity Framework Core to the project

To connect to Azure SQL Database by using .NET and Entity Framework Core, you need to add three NuGet packages to your project using one of the following methods:

## [Visual Studio](#tab/visual-studio)

1. In the **Solution Explorer** window, right-click the project's **Dependencies** node and select **Manage NuGet Packages**.

1. In the resulting window, search for *EntityFrameworkCore*. Locate and install the following packages:

- **Microsoft.EntityFrameworkCore**: Provides essential Entity Framework Core functionality
- **Microsoft.EntityFrameworkCore.SqlServer**: Provides extra components to connect to the logical server
- **Microsoft.EntityFrameworkCore.Design**: Provides support for running Entity Framework migrations

Alternatively, you can also run the `Install-Package` cmdlet in the **Package Manager Console** window:

```powershell
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Design
```

## [.NET CLI](#tab/dotnet-cli)

Use the `dotnet add package` command to install the following packages:

```dotnetcli
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
```

---

## Add the code to connect to Azure SQL Database

The Entity Framework Core libraries rely on the `Microsoft.Data.SqlClient` and `Azure.Identity` libraries to implement passwordless connections to Azure SQL Database. The `Azure.Identity` library provides a class called [DefaultAzureCredential](/dotnet/azure/sdk/authentication#defaultazurecredential) that handles passwordless authentication to Azure.

`DefaultAzureCredential` supports multiple authentication methods and determines which to use at runtime. This approach enables your app to use different authentication methods in different environments (local vs. production) without implementing environment-specific code. The [Azure Identity library overview](/dotnet/api/overview/azure/Identity-readme#defaultazurecredential) explains the order and locations in which `DefaultAzureCredential` looks for credentials.

Complete the following steps to connect to Azure SQL Database using Entity Framework Core and the underlying `DefaultAzureCredential` class:

1. Add a `ConnectionStrings` section to the `appsettings.Development.json` file so that it matches the following code. Remember to update the `<your database-server-name>` and `<your-database-name>` placeholders.

    The passwordless connection string includes a configuration value of `Authentication=Active Directory Default`, which enables Entity Framework Core to use `DefaultAzureCredential` to connect to Azure services. When the app runs locally, it authenticates with the user you're signed into Visual Studio with. Once the app deploys to Azure, the same code discovers and applies the managed identity that is associated with the hosted app, which you'll configure later.

    > [!NOTE]  
    > Passwordless connection strings are safe to commit to source control, since they do not contain any secrets such as usernames, passwords, or access keys.

    ```json
    {
        "Logging": {
            "LogLevel": {
                "Default": "Information",
                "Microsoft.AspNetCore": "Warning"
            }
        },
        "ConnectionStrings": {
            "AZURE_SQL_CONNECTIONSTRING": "Data Source=passwordlessdbserver.database.windows.net;
                Initial Catalog=passwordlessdb; Authentication=Active Directory Default; Encrypt=True;"
        }
    }
    ```

1. Add the following code to the `Program.cs` file above the line of code that reads `var app = builder.Build();`. This code performs the following configurations:

    * Retrieves the passwordless database connection string from the `appsettings.Development.json` file for local development, or from the environment variables for hosted production scenarios.
    * Registers the Entity Framework Core `DbContext` class with the .NET dependency injection container.

        ```csharp
        var connection = String.Empty;
        if (builder.Environment.IsDevelopment())
        {
            builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
            connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
        }
        else
        {
            connection = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
        }

        builder.Services.AddDbContext<PersonDbContext>(options =>
            options.UseSqlServer(connection));
        ```

1. Add the following endpoints to the bottom of the `Program.cs` file above `app.Run()` to retrieve and add entities in the database using the `PersonDbContext` class.

    ```csharp
    app.MapGet("/Person", (PersonDbContext context) =>
    {
        return context.Person.ToList();
    })
    .WithName("GetPersons")
    .WithOpenApi();

    app.MapPost("/Person", (Person person, PersonDbContext context) =>
    {
        context.Add(person);
        context.SaveChanges();
    })
    .WithName("CreatePerson")
    .WithOpenApi();
    ```

    Finally, add the `Person` and `PersonDbContext` classes to the bottom of the `Program.cs` file. The Person class represents a single record in the database's `Persons` table. The `PersonDbContext` class represents the Person database and allows you to perform operations on it through code. You can read more about `DbContext` in the [Getting Started](/ef/core/get-started/overview/first-app) documentation for Entity Framework Core.

    ```csharp
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class PersonDbContext : DbContext
    {
        public PersonDbContext(DbContextOptions<PersonDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }
    }
    ```

## Run the migrations to create the database

To update the database schema to match your data model using Entity Framework Core, you must use a migration. Migrations can create and incrementally update a database schema to keep it in sync with your application's data model. You can learn more about this pattern in the [migrations overview](/ef/core/managing-schemas/migrations).

1. Open a terminal window to the root of your project.
1. Run the following command to generate an initial migration that can create the database:

    ## [Visual Studio](#tab/visual-studio)

    ```powershell
    Add-Migration InitialCreate
    ```

    ## [.NET CLI](#tab/dotnet-cli)

    ```dotnetcli
    dotnet ef migrations add InitialCreate
    ```

    ---

1. A `Migrations` folder should appear in your project directory, along with a file called `InitialCreate` with unique numbers prepended. Run the migration to create the database using the following command:

    ## [Visual Studio](#tab/visual-studio)

    ```powershell
    Update-Database
    ```

    ## [.NET CLI](#tab/dotnet-cli)

    ```dotnetcli
    dotnet ef database update
    ```

    ---

    The Entity Framework Core tooling will create the database schema in Azure defined by the `PersonDbContext` class.

## Test the app locally

The app is ready to be tested locally. Make sure you're signed in to Visual Studio or the Azure CLI with the same account you set as the admin for your database.

1) Press the run button at the top of Visual Studio to launch the API project.

1) On the Swagger UI page, expand the POST method and select **Try it**.

1) Modify the sample JSON to include values for the first and last name. Select **Execute** to add a new record to the database. The API returns a successful response.

    :::image type="content" source="media/passwordless-connections/api-testing-small.png" alt-text="Screenshot showing how to test the API." lightbox="media/passwordless-connections/api-testing.png":::

1) Expand the **GET** method on the Swagger UI page and select **Try it**. Select **Execute**, and the person you just created is returned.

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

        :::image type="content" source="media/passwordless-connections/create-app-service-small.png" alt-text="Screenshot showing how to deploy with Visual Studio." lightbox="media/passwordless-connections/create-app-service.png":::

1. Once the resource is created, make sure it's selected in the list of app services, and then select **Next**.
1. On the **API Management** step, select the **Skip this step** checkbox at the bottom and then select **Finish**.

1. Select **Publish** in the upper right of the publishing profile summary to deploy the app to Azure.

When the deployment finishes, Visual Studio launches the browser to display the hosted app, but at this point the app doesn't work correctly on Azure. You still need to configure the secure connection between the App Service and the SQL database to retrieve your data.

## Connect the App Service to Azure SQL Database

The following steps are required to connect the App Service instance to Azure SQL Database:

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

The Azure portal allows you to work with managed identities and run queries against Azure SQL Database. Complete the following steps to create a passwordless connection from your App Service instance to Azure SQL Database:

### Create the managed identity

1) In the Azure portal, navigate to your App Service and select **Identity** on the left navigation.

2) On the identity page, make sure the **Enable system-assigned managed identity** option is enabled. When this setting is enabled, a system-assigned managed identity is created with the same name as your App Service. System-assigned identities are tied to the service instance and are destroyed with the app when it's deleted.

### Create the database user and assign roles

1) In the Azure portal, browse to your SQL database and select **Query editor (preview)**.

2) Select **Continue as `<your-username>` on the right side of the screen to sign into the database using your account.

3) On the query editor view, run the following T-SQL commands:

    ```sql
    CREATE USER <your-app-service-name> FROM EXTERNAL PROVIDER;
    ALTER ROLE db_datareader ADD MEMBER <your-app-service-name>;
    ALTER ROLE db_datawriter ADD MEMBER <your-app-service-name>;
    ALTER ROLE db_ddladmin ADD MEMBER <your-app-service-name>;
    GO
    ```

    :::image type="content" source="media/passwordless-connections/query-editor-small.png" alt-text="Screenshot showing how to use the Azure Query editor." lightbox="media/passwordless-connections/query-editor.png":::

    This SQL script creates a SQL database user that maps back to the managed identity of your App Service instance. It also assigns the necessary SQL roles to the user to allow your app to read, write, and modify the data and schema of your database. After this step is completed, your services are connected.

---

> [!IMPORTANT]  
> Although this solution provides a simple approach for getting started, it is not a best practice for enterprise production environments. In those scenarios the app should not perform all operations using a single, elevated identity. You should try to implement the principle of least privilege by configuring multiple identities with specific permissions for specific tasks.
>
> You can read more about configuring database roles and security on the following resources:
>
> [Tutorial: Secure a database in Azure SQL Database](./secure-database-tutorial.md)
>
> [Authorize database access to SQL Database](./logins-create-manage.md)

## Test the deployed application

Browse to the URL of the app to test that the connection to Azure SQL Database is working. You can locate the URL of your app on the App Service overview page. Append the `/person` path to the end of the URL to browse to the same endpoint you tested locally.

The person you created locally should display in the browser. Congratulations! Your application is now connected to Azure SQL Database in both local and hosted environments.

[!INCLUDE [passwordless-resource-cleanup](../includes/passwordless-resource-cleanup.md)]

## Related content

- [Tutorial: Secure a database in Azure SQL Database](./secure-database-tutorial.md)
- [Authorize database access to SQL Database](./logins-create-manage.md)
- [An overview of Azure SQL Database security capabilities](security-overview.md)
- [Azure SQL Database security best practices](security-best-practice.md)
