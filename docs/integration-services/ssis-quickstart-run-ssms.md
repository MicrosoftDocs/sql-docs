---
description: "Run an SSIS package with SQL Server Management Studio (SSMS)"
title: "Run an SSIS package with SSMS | Microsoft Docs"
ms.date: "05/21/2018"
ms.topic: quickstart
ms.service: sql
ms.custom:
  - intro-quickstart
ms.subservice: integration-services
author: chugugrace
ms.author: chugu
---
# Run an SSIS package with SQL Server Management Studio (SSMS)

[!INCLUDE[sqlserver-ssis](../includes/applies-to-version/sqlserver-ssis.md)]


This quickstart demonstrates how to use SQL Server Management Studio (SSMS) to connect to the SSIS Catalog database, and then run an SSIS package stored in the SSIS Catalog from Object Explorer in SSMS.

SQL Server Management Studio is an integrated environment for managing any SQL infrastructure, from SQL Server to SQL Database. For more info about SSMS, see [SQL Server Management Studio (SSMS)](../ssms/sql-server-management-studio-ssms.md).

## Prerequisites

Before you start, make sure you have the latest version of SQL Server Management Studio (SSMS). To download SSMS, see [Download SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md).

An Azure SQL Database server listens on port 1433. If you're trying to connect to an Azure SQL Database server from within a corporate firewall, this port must be open in the corporate firewall for you to connect successfully.

## Supported platforms

You can use the information in this quickstart to run an SSIS package on the following platforms:

-   SQL Server on Windows.

-   Azure SQL Database. For more info about deploying and running packages in Azure, see [Lift and shift SQL Server Integration Services workloads to the cloud](lift-shift/ssis-azure-lift-shift-ssis-packages-overview.md).

You cannot use the information in this quickstart to run an SSIS package on Linux. For more info about running packages on Linux, see [Extract, transform, and load data on Linux with SSIS](../linux/sql-server-linux-migrate-ssis.md).

## For Azure SQL Database, get the connection info

To run the package on Azure SQL Database, get the connection information you need to connect to the SSIS Catalog database (SSISDB). You need the fully qualified server name and login information in the procedures that follow.

1. Log in to the [Azure portal](https://portal.azure.com/).
2. Select **SQL Databases** from the left-hand menu, and then select the SSISDB database on the **SQL databases** page. 
3. On the **Overview** page for your database, review the fully qualified server name. To see the **Click to copy** option, hover over the server name. 
4. If you forget your Azure SQL Database server login information, navigate to the SQL Database server page to view the server admin name. You can reset the password if necessary.

## Connect to the SSISDB database

Use SQL Server Management Studio to establish a connection to the SSIS Catalog. 

1. Open SQL Server Management Studio.

2. In the **Connect to Server** dialog box, enter the following information:

   | Setting       | Suggested value | More info | 
   | ------------ | ------------------ | ------------------------------------------------- | 
   | **Server type** | Database engine | This value is required. |
   | **Server name** | The fully qualified server name | If you're connecting to an Azure SQL Database server, the name is in this format: `<server_name>.database.windows.net`. |
   | **Authentication** | SQL Server Authentication | With SQL Server authentication, you can connect to SQL Server or to Azure SQL Database. If you're connecting to an Azure SQL Database server, you can't use Windows authentication. |
   | **Login** | The server admin account | This account is the account that you specified when you created the server. |
   | **Password** | The password for your server admin account | This password is the password that you specified when you created the server. |

3. Click **Connect**. The Object Explorer window opens in SSMS. 

4. In Object Explorer, expand **Integration Services Catalogs** and then expand **SSISDB** to view the objects in the SSIS Catalog database.

## Run a package

1. In Object Explorer, select the package that you want to run.

2. Right-click and select **Execute**. The **Execute Package** dialog box opens.

3.  Configure the package execution by using the settings on the **Parameters**, **Connection Managers**, and **Advanced** tabs in the Execute Package dialog box.

4.  Click OK to run the package.

## Next steps
- Consider other ways to run a package.
    - [Run an SSIS package with Transact-SQL (SSMS)](./ssis-quickstart-run-tsql-ssms.md)
    - [Run an SSIS package with Transact-SQL (VS Code)](ssis-quickstart-run-tsql-vscode.md)
    - [Run an SSIS package from the command prompt](./ssis-quickstart-run-cmdline.md)
    - [Run an SSIS package with PowerShell](ssis-quickstart-run-powershell.md)
    - [Run an SSIS package with C#](./ssis-quickstart-run-dotnet.md)
