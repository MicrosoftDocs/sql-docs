---
title: "Deploy an SSIS project with Transact-SQL (SSMS) | Microsoft Docs"
ms.date: "09/25/2017"
ms.topic: "article"
ms.prod: "sql-server-2017"
ms.technology: 
  - "integration-services"
author: "douglaslMS"
ms.author: "douglasl"
manager: "craigg"
---
# Deploy an SSIS project from SSMS with Transact-SQL

This quick start demonstrates how to use SQL Server Management Studio (SSMS) to connect to the SSIS Catalog database, and then use Transact-SQL statements to deploy an SSIS project to the SSIS Catalog. 

> [!NOTE]
> The method described in this article is not available when you connect to an Azure SQL Database server with SSMS. The `catalog.deploy_project` stored procedure expects path to the `.ispac` file in the local (on premises) file system.

SQL Server Management Studio is an integrated environment for managing any SQL infrastructure, from SQL Server to SQL Database. For more info about SSMS, see [SQL Server Management Studio (SSMS)](../ssms/sql-server-management-studio-ssms.md).

## Prerequisites

Before you start, make sure you have the latest version of SQL Server Management Studio. To download SSMS, see [Download SQL Server Management Studio (SSMS)](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms).

## Connect to the SSIS Catalog database

Use SQL Server Management Studio to establish a connection to the SSIS Catalog. 

> [!NOTE]
> An Azure SQL Database server listens on port 1433. If you're trying to connect to an Azure SQL Database server from within a corporate firewall, this port must be open in the corporate firewall for you to connect successfully.

1. Open SQL Server Management Studio.

2. In the **Connect to Server** dialog box, enter the following information:

   | Setting       | Suggested value | More info | 
   | ------------ | ------------------ | ------------------------------------------------- | 
   | **Server type** | Database engine | This value is required. |
   | **Server name** | The fully qualified server name |  |
   | **Authentication** | SQL Server Authentication | This quickstart uses SQL authentication. |
   | **Login** | The server admin account | This is the account that you specified when you created the server. |
   | **Password** | The password for your server admin account | This is the password that you specified when you created the server. |

3. Click **Connect**. The Object Explorer window opens in SSMS. 

4. In Object Explorer, expand **Integration Services Catalogs** and then expand **SSISDB** to view the objects in the SSIS Catalog database.

## Run the T-SQL code
Run the following Transact-SQL code to deploy an SSIS project.

1.  In SSMS, open a new query window and paste the following code.

2.  Update the parameter values in the `catalog.deploy_project` stored procedure for your system.

3.  Make sure that SSISDB is the current database.

4.  Run the script.

5. In Object Explorer, refresh the contents of **SSISDB** if necessary and check for the project that you just deployed.

```sql
DECLARE @ProjectBinary AS varbinary(max)
DECLARE @operation_id AS bigint
SET @ProjectBinary = (SELECT * FROM OPENROWSET(BULK '<project_file_path>.ispac', SINGLE_BLOB) AS BinaryData)

EXEC catalog.deploy_project @folder_name = '<target_folder>', @project_name = '<project_name',
    @Project_Stream = @ProjectBinary, @operation_id = @operation_id out
```

## Next steps
- Consider other ways to deploy a package.
    - [Deploy an SSIS package with SSMS](./ssis-quickstart-deploy-ssms.md)
    - [Deploy an SSIS package with Transact-SQL (VS Code)](ssis-quickstart-deploy-tsql-vscode.md)
    - [Deploy an SSIS package from the command prompt](./ssis-quickstart-deploy-cmdline.md)
    - [Deploy an SSIS package with PowerShell](ssis-quickstart-deploy-powershell.md)
    - [Deploy an SSIS package with C#](./ssis-quickstart-deploy-dotnet.md) 
- Run a deployed package. To run a package, you can choose from several tools and languages. For more info, see the following articles:
    - [Run an SSIS package with SSMS](./ssis-quickstart-run-ssms.md)
    - [Run an SSIS package with Transact-SQL (SSMS)](./ssis-quickstart-run-tsql-ssms.md)
    - [Run an SSIS package with Transact-SQL (VS Code)](ssis-quickstart-run-tsql-vscode.md)
    - [Run an SSIS package from the command prompt](./ssis-quickstart-run-cmdline.md)
    - [Run an SSIS package with PowerShell](ssis-quickstart-run-powershell.md)
    - [Run an SSIS package with C#](./ssis-quickstart-run-dotnet.md) 
