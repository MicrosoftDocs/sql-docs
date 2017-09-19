---
title: "Run a package with SSMS | Microsoft Docs"
ms.date: "08/21/2017"
ms.topic: "article"
ms.prod: "sql-server-2017"
ms.technology: 
  - "integration-services"
author: "douglaslMS"
ms.author: "douglasl"
manager: "craigg"
---
# Run an SSIS package with SQL Server Management Studio (SSMS)
This quick start demonstrates how to use SQL Server Management Studio (SSMS) to connect to the SSIS Catalog database on an Azure SQL database server, and then run an SSIS package stored in the SSIS Catalog from Object Explorer in SSMS.

SQL Server Management Studio is an integrated environment for managing any SQL infrastructure, from SQL Server to SQL Database. For more info about SSMS, see [SQL Server Management Studio (SSMS)](../ssms/sql-server-management-studio-ssms.md).

## Prerequisites

Before you start, make sure you have the latest version of SQL Server Management Studio (SSMS). To download SSMS, see [Download SQL Server Management Studio (SSMS)](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms).

## Connect to the SSISDB database

Use SQL Server Management Studio to establish a connection to the SSIS Catalog on your Azure SQL Database server. 

> [!IMPORTANT]
> An Azure SQL Database server listens on port 1433. If you are attempting to connect to an Azure SQL Database server from within a corporate firewall, this port must be open in the corporate firewall for you to connect successfully.
>

1. Open SQL Server Management Studio.

2. In the **Connect to Server** dialog box, enter the following information:

   | Setting       | Suggested value | Description | 
   | ------------ | ------------------ | ------------------------------------------------- | 
   | **Server type** | Database engine | This value is required. |
   | **Server name** | The fully qualified server name | The name should be something like this: **mysqldbserver.database.windows.net**. |
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
- Schedule a package. For more info, see [Schedule page](ssis-everest-howto-schedule-package.md)
