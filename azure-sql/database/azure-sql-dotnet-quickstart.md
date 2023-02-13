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

When your application is deployed to App Service, the following steps are required to make that connection work:

1) Create a managed identity for the App Service

## Test the deployed application