---
title: "Run an SSIS package with SSMS | Microsoft Docs"
ms.date: "09/25/2017"
ms.topic: "article"
ms.prod: "sql-server-2017"
ms.technology: 
  - "integration-services"
author: "douglaslMS"
ms.author: "douglasl"
manager: "craigg"
---
# Run an SSIS package with SQL Server Management Studio (SSMS)
This quick start demonstrates how to use SQL Server Management Studio (SSMS) to connect to the SSIS Catalog database, and then run an SSIS package stored in the SSIS Catalog from Object Explorer in SSMS.

SQL Server Management Studio is an integrated environment for managing any SQL infrastructure, from SQL Server to SQL Database. For more info about SSMS, see [SQL Server Management Studio (SSMS)](../ssms/sql-server-management-studio-ssms.md).

## Prerequisites

Before you start, make sure you have the latest version of SQL Server Management Studio (SSMS). To download SSMS, see [Download SQL Server Management Studio (SSMS)](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms).

## Connect to the SSISDB database

Use SQL Server Management Studio to establish a connection to the SSIS Catalog. 

> [!NOTE]
> An Azure SQL Database server listens on port 1433. If you're trying to connect to an Azure SQL Database server from within a corporate firewall, this port must be open in the corporate firewall for you to connect successfully.

1. Open SQL Server Management Studio.

2. In the **Connect to Server** dialog box, enter the following information:

   | Setting       | Suggested value | More info | 
   | ------------ | ------------------ | ------------------------------------------------- | 
   | **Server type** | Database engine | This value is required. |
   | **Server name** | The fully qualified server name | If you're connecting to an Azure SQL Database server, the name is in this format: `<server_name>.database.windows.net`. |
   | **Authentication** | SQL Server Authentication | This quickstart uses SQL authentication. |
   | **Login** | The server admin account | This is the account that you specified when you created the server. |
   | **Password** | The password for your server admin account | This is the password that you specified when you created the server. |

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
