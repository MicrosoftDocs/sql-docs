---
title: "Run a package with .NET code (C#)  | Microsoft Docs"
ms.date: "08/21/2017"
ms.topic: "article"
ms.prod: "sql-server-2017"
ms.technology: 
  - "integration-services"
author: "douglaslMS"
ms.author: "douglasl"
manager: "craigg"
---
# Run an SSIS package with C# code in a .NET app
This quick start tutorial demonstrates how to write C# code to connect to an Azure SQL database and run an SSIS package.

You can use Visual Studio, Visual Studio Code, or another tool of your choice to create a C# app.

## Prerequisites

Before you start, make sure you have Visual Studio or Visual Studio Code installed. Download the free Community edition of Visual Studio, or the free Visual Studio Code, from [Visual Studio Downloads](https://www.visualstudio.com/downloads/).

You also have to have a server-level firewall rule for the public IP address of the computer you use for this quick start tutorial. To create a server-level firewall rule, see [Create a server-level firewall rule](sql-database-get-started-portal.md#create-a-server-level-firewall-rule).

## Get the SSISDB connection information

Get the connection information you need to connect to the SSIS Catalog database (SSISDB) on SQL database. You need the fully qualified server name and login information in the next procedures.

1. Log in to the [Azure portal](https://portal.azure.com/).
2. Select **SQL Databases** from the left-hand menu, and click the SSISDB database on the **SQL databases** page. 
3. On the **Overview** page for your database, review the fully qualified server name. To bring up the **Click to copy** option, hover over the server name . 
4. If you forget your Azure SQL Database server login information, navigate to the SQL Database server page to view the server admin name. You can reset the password if necessary.
5. Click **Show database connection strings**.
6. Review the complete **ADO.NET** connection string. The sample code uses a `SqlConnectionStringBuilder` to recreate this connection string with the individual parameter values that you provide.

## Create a new Visual Studio project

1. In Visual Studio, choose **File**, **New**, **Project**. 
2. In the **New Project** dialog, and expand **Visual C#**.
3. Select **Console App** and enter *deploy_ssis_project* for the project name.
4. Click **OK** to create and open the new project in Visual Studio.
4. In Solution Explorer, right-click **deploy_ssis_project** and click **Manage NuGet Packages**. 
5. On the **Browse**, search for `System.Data.SqlClient` and then select it.
6. On the **System.Data.SqlClient** page, click **Install**.
7. When the installation completes, review the changes and then click **OK** to close the **Preview** window. 
8. If a **License Acceptance** window appears, click **I Accept**.

## Add code to 
1. Open **Program.cs**.

2. Replace the contents of **Program.cs** with the following code. Add the appropriate values for your server, database, user, and password.

    ```csharp
    ```

## Run the code

1. Press **F5** to run the application.
2. Verify that TBD and then close the application window.

## Next steps
- Schedule a package. For more info, see [Schedule page](ssis-everest-howto-schedule-package.md)
