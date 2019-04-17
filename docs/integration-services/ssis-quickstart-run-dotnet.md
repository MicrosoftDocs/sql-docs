---
title: "Run an SSIS project with .NET code (C#) | Microsoft Docs"
ms.date: "05/21/2018"
ms.topic: quickstart
ms.prod: sql
ms.prod_service: "integration-services"
ms.custom: ""
ms.technology: integration-services
author: janinezhang
ms.author: janinez
manager: craigg
---
# Run an SSIS package with C# code in a .NET app
This quickstart demonstrates how to write C# code to connect to a database server and run an SSIS package.

You can use Visual Studio, Visual Studio Code, or another tool of your choice to create a C# app.

## Prerequisites

Before you start, make sure you have Visual Studio or Visual Studio Code installed. Download the free Community edition of Visual Studio, or the free Visual Studio Code, from [Visual Studio Downloads](https://www.visualstudio.com/downloads/).

An Azure SQL Database server listens on port 1433. If you're trying to connect to an Azure SQL Database server from within a corporate firewall, this port must be open in the corporate firewall for you to connect successfully.

## For Azure SQL Database, get the connection info

To run the package on Azure SQL Database, get the connection information you need to connect to the SSIS Catalog database (SSISDB). You need the fully qualified server name and login information in the procedures that follow.

1. Log in to the [Azure portal](https://portal.azure.com/).
2. Select **SQL Databases** from the left-hand menu, and then select the SSISDB database on the **SQL databases** page. 
3. On the **Overview** page for your database, review the fully qualified server name. To see the **Click to copy** option, hover over the server name. 
4. If you forget your Azure SQL Database server login information, navigate to the SQL Database server page to view the server admin name. You can reset the password if necessary.
5. Click **Show database connection strings**.
6. Review the complete **ADO.NET** connection string. Optionally, your code can use a `SqlConnectionStringBuilder` to recreate this connection string with the individual parameter values that you provide.

## Create a new Visual Studio project

1. In Visual Studio, choose **File**, **New**, **Project**. 
2. In the **New Project** dialog, and expand **Visual C#**.
3. Select **Console App** and enter *run_ssis_project* for the project name.
4. Click **OK** to create and open the new project in Visual Studio.

## Add references
1. In Solution Explorer, right-click the **References** folder and select **Add Reference**. The **Reference Manager** dialog box opens.
2. In the **Reference Manager** dialog box, expand **Assemblies** and select **Extensions**.
3. Select the following two references to add:
    -   Microsoft.SqlServer.Management.Sdk.Sfc
    -   Microsoft.SqlServer.Smo
4. Click the **Browse** button to add a reference to **Microsoft.SqlServer.Management.IntegrationServices**. (This assembly is installed only in the global assembly cache (GAC).) The **Select the files to reference** dialog box opens.
5. In the **Select the files to reference** dialog box, navigate to the GAC folder that contains the assembly. Typically this folder is `C:\Windows\assembly\GAC_MSIL\Microsoft.SqlServer.Management.IntegrationServices\14.0.0.0__89845dcd8080cc91`.
6. Select the assembly (that is, the .dll file) in the folder and click **Add**.
7. Click **OK** to close the **Reference Manager** dialog box and add the three references. To make sure the references are there, check the **References** list in Solution Explorer.

## Add the C# code 
1. Open **Program.cs**.

2. Replace the contents of **Program.cs** with the following code. Add the appropriate values for your server, database, user, and password.

> [!NOTE]
> The following example uses Windows Authentication. To use SQL Server authentication, replace the `Integrated Security=SSPI;` argument with `User ID=<user name>;Password=<password>;`. If you're connecting to an Azure SQL Database server, you can't use Windows authentication.


```csharp
using Microsoft.SqlServer.Management.IntegrationServices;
using System.Data.SqlClient;

namespace run_ssis_package
{
    class Program
    {
        static void Main(string[] args)
        {
            // Variables
            string targetServerName = "localhost";
            string folderName = "Project1Folder";
            string projectName = "Integration Services Project1";
            string packageName = "Package.dtsx";

            // Create a connection to the server
            string sqlConnectionString = "Data Source=" + targetServerName +
                ";Initial Catalog=master;Integrated Security=SSPI;";
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionString);

            // Create the Integration Services object
            IntegrationServices integrationServices = new IntegrationServices(sqlConnection);

            // Get the Integration Services catalog
            Catalog catalog = integrationServices.Catalogs["SSISDB"];

            // Get the folder
            CatalogFolder folder = catalog.Folders[folderName];

            // Get the project
            ProjectInfo project = folder.Projects[projectName];

            // Get the package
            PackageInfo package = project.Packages[packageName];

            // Run the package
            package.Execute(false, null);

        }
    }
}
```

## Run the code

1. To run the application, press **F5**.
2. Verify that the package ran as expected and then close the application window.

## Next steps
- Consider other ways to run a package.
    - [Run an SSIS package with SSMS](./ssis-quickstart-run-ssms.md)
    - [Run an SSIS package with Transact-SQL (SSMS)](./ssis-quickstart-run-tsql-ssms.md)
    - [Run an SSIS package with Transact-SQL (VS Code)](ssis-quickstart-run-tsql-vscode.md)
    - [Run an SSIS package from the command prompt](./ssis-quickstart-run-cmdline.md)
    - [Run an SSIS package with PowerShell](ssis-quickstart-run-powershell.md)
