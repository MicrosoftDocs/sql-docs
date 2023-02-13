---
title: Create and connect to an Azure SQL database using .NET
description: Learn how to connect to an Azure SQL database and query data using .NET
author: alexwolf
ms.author: alexwolfmsft
ms.date: 02/10/2023
ms.service: sql-db-mi
ms.subservice: security
ms.topic: quickstart
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---

# Create and query an Azure SQL database using .NET and the SqlClient library

In this quickstart, you'll connect to an Azure SQL database and perform queries using .NET and the SqlClient library. This quickstart follows the recommended passwordless approach to connect to the database without the use of connection strings. You can learn more about passwordless connections on the passwordless hub.

## Prerequisites

* An active Azure subscription. If you don't have one, create a free account.
* An Azure SQL database configured with Azure AD authentication. You can create one using Azure SQL create a database quickstart.
* The latest version of the Azure CLI.
* A recent version of .NET installed.

## Create the project

For the steps ahead, create a .NET Web API using either the .NET CLI or Visual Studio 2022.

## [Visual Studio 2022](#tab/visual-studio)

1. At the top of Visual Studio, navigate to **File** > **New** > **Project..**.

1. In the dialog window, enter *asp.net* into the project template search box and select the ASP.NET Core Web API result. Choose **Next** at the bottom of the dialog.

1. For the **Project Name**, enter *DotnetSQL*. Leave the default values for the rest of the fields and select **Next**.

1. For the **Framework**, ensure .NET 7.0 is selected. Then choose **Create**. The new project will open inside the Visual Studio environment.

## [.NET CLI](#tab/net-cli)

1. In a console window (such as cmd, PowerShell, or Bash), use the `dotnet new` command to create a new console app with the name *DotNetSQL*. This command creates a simple "Hello World" C# project with a single source file: *Program.cs*.

   ```dotnetcli
   dotnet new console -n BlobQuickstart
   ```

1. Switch to the newly created *BlobQuickstart* directory.

   ```console
   cd BlobQuickstart
   ```

1. Open the project in your desired code editor. To open the project in:
    * Visual Studio, locate and double-click the `DotNetSQL.csproj` file.
    * Visual Studio Code, run the following command:

    ```bash
    code .
    ```

---

## Add the SqlClient library

To connect to Azure SQL, install the `Microsoft.Data.SqlClient` library for .NET.

### [Visual Studio 2022](#tab/visual-studio)

1. In **Solution Explorer**, right-click the **Dependencies** node of your project. Select **Manage NuGet Packages**.

1. In the resulting window, search for *SqlClient*. Select the `Microsoft.Data.SqlClient` result, and select **Install**.

### [.NET CLI](#tab/net-cli)

Use the following command to install the `Microsoft.Data.SqlClient` package:

```dotnetcli
dotnet add package Microsoft.Data.SqlClient
```

## Add the sample code

Add the following configuration to the `appsettings.json` file. This passwordless connection string includes a configuration value of `Authentication=Active Directory Default`. The `Active Directory Default` value instructs the app to use `DefaultAzureCredential` to connect to Azure services. When running locally the app will authenticate with the user you are signed into Visual Studio with. Once the app is deployed to Azure, the same code will discover and apply the managed identity that is associated with the hosted app, which you'll configure later.

```json

```

Add the following sample code to the bottom of the `Program.cs` file above `app.Run()`. This code performs the following important steps:

* Creates a Person table in the database during startup
* Creates an endpoint to retrieve the Person records stored in the database
* Creates an endpoint to add new Person records to the database

```csharp

```

## Run the app locally



## Deploy to Azure App Service



## Connect the App Service to Azure SQL

When your application is deployed to App Service, the following steps are required to connect the app to Azure SQL:

1) Create a managed identity for the App Service. The managed identity will automatically be discovered by the SqlClient library included in your app, just like it discovered your local Visual Studio user.
2) Create an Azure SQL database user and associate it with the App Service managed identity.
3) Assign SQL roles to the database user that allow for read, write, and potentially other permissions.

There are multiple tools available to implement these steps:

## [Service Connector](#tab/service-connector)

Service connector is a tool that streamlines authenticated connections between different services in Azure. Service Connector currently supports connecting an App Service to an Azure SQL database via the Azure CLI using the `az webapp connection create sql` command. This single command will complete the three steps mentioned above for you.

```bash
az webapp connection create sql
-g <your-resource-group>
-n <your-app-service-name>
--tg <your-database-server-resource-group>
--server <your-database-server-name>
--database <your-database-name>
--system-identity
```

## [Azure Portal](#tab/azure-portal)

The Azure portal allows you to work with managed identities and run queries against an Azure SQL database. Complete the following steps to create a passwordless connection from your App Service instance to Azure SQL:

### Create the managed identity

1) In the Azure portal, navigate to your App Service and select **Identity** on the left navigation.

2) On the identity page, make sure the **Enable system-assigned managed identity** option is enabled. When this setting is enabled, a system-assigned managed identity is created with the same name as your App Service. System-assigned identities are tied to the service instance and are destroyed with the app when it is deleted.

### Create the database user and assign roles

1) In the Azure portal, browse to your Azure SQL database instance and select **Query editor (preview)**.

2) Select **Continue as <your-username>** on the right side of the screen to sign into the database using your account.

3) On the query editor view, run the following SQL commands:

```sql
CREATE USER <your-app-service-name> FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER <your-app-service-name>;
ALTER ROLE db_datawriter ADD MEMBER <your-app-service-name>;
ALTER ROLE db_ddladmin ADD MEMBER <your-app-service-name>;
GO
```

This SQL script will create an Azure SQL database user that maps back to the managed identity of your App Service instance. It also assigns the necessary SQL roles to the user to allow your app to read, write, and modify the data and schema of your database. After this step is completed your services are connected.

---

## Test the deployed application

Browse to the URL of the app to test that the connection to Azure SQL is working. You can locate the URL of your app on the App Service overview page. Append the `/person` path to the end of the URL to browse to the same endpoint you tested locally.

The person you created locally should display in the browser.