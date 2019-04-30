---
title: "Run an SSIS package with Transact-SQL (VS Code) | Microsoft Docs"
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
# Run an SSIS package from Visual Studio Code with Transact-SQL

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


This quickstart demonstrates how to use Visual Studio Code to connect to the SSIS Catalog database, and then use Transact-SQL statements to run an SSIS package stored in the SSIS Catalog.

Visual Studio Code is a code editor for Windows, macOS, and Linux that supports extensions, including the `mssql` extension for connecting to Microsoft SQL Server, Azure SQL Database, or Azure SQL Data Warehouse. For more info about VS Code, see [Visual Studio Code](https://code.visualstudio.com/).

## Prerequisites

Before you start, make sure you have installed the latest version of Visual Studio Code and loaded the `mssql` extension. To download these tools, see the following pages:
-   [Download Visual Studio Code](https://code.visualstudio.com/Download)
-   [mssql extension](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql)

## Supported platforms

You can use the information in this quickstart to run an SSIS package on the following platforms:

-   SQL Server on Windows.

-   Azure SQL Database. For more info about deploying and running packages in Azure, see [Lift and shift SQL Server Integration Services workloads to the cloud](lift-shift/ssis-azure-lift-shift-ssis-packages-overview.md).

You cannot use the information in this quickstart to run an SSIS package on Linux. For more info about running packages on Linux, see [Extract, transform, and load data on Linux with SSIS](../linux/sql-server-linux-migrate-ssis.md).

## Set language mode to SQL in VS Code

To enable `mssql` commands and T-SQL IntelliSense, set the language mode is set to **SQL** in Visual Studio Code.

1. Open Visual Studio Code and then open a new window. 

2. Click **Plain Text** in the lower right-hand corner of the status bar.

3. In the **Select language mode** drop-down menu that opens, select or enter **SQL**, and then press **ENTER** to set the language mode to SQL. 

## For Azure SQL Database, get the connection info

To run the package on Azure SQL Database, get the connection information you need to connect to the SSIS Catalog database (SSISDB). You need the fully qualified server name and login information in the procedures that follow.

1. Log in to the [Azure portal](https://portal.azure.com/).
2. Select **SQL Databases** from the left-hand menu, and then select the SSISDB database on the **SQL databases** page. 
3. On the **Overview** page for your database, review the fully qualified server name. To see the **Click to copy** option, hover over the server name. 
4. If you forget your Azure SQL Database server login information, navigate to the SQL Database server page to view the server admin name. You can reset the password if necessary.

## Connect to the SSIS Catalog database

Use Visual Studio Code to establish a connection to the SSIS Catalog.

> [!IMPORTANT]
> Before continuing, make sure that you have your server, database, and login information ready. If you change your focus from Visual Studio Code after you begin entering the connection profile information, you have to restart creating the connection profile.

1. In VS Code, press **CTRL+SHIFT+P** (or **F1**) to open the Command Palette.

2. Type **sqlcon** and press **ENTER**.

3. Press **ENTER** to select **Create Connection Profile**. This step creates a connection profile for your SQL Server instance.

4. Follow the prompts to specify the connection properties for the new connection profile. After specifying each value, press **ENTER** to continue. 

   | Setting       | Suggested value | More info |
   | ------------ | ------------------ | ------------------------------------------------- | 
   | **Server name** | The fully qualified server name | If you're connecting to an Azure SQL Database server, the name is in this format: `<server_name>.database.windows.net`. |
   | **Database name** | **SSISDB** | The name of the database to which to connect. |
   | **Authentication** | SQL Login | With SQL Server authentication, you can connect to SQL Server or to Azure SQL Database. If you're connecting to an Azure SQL Database server, you can't use Windows authentication. |
   | **User name** | The server admin account | This account is the account that you specified when you created the server. |
   | **Password (SQL Login)** | The password for your server admin account | This password is the password that you specified when you created the server. |
   | **Save Password?** | Yes or No | If you do not want to enter the password each time, select Yes. |
   | **Enter a name for this profile** | A profile name, such as **mySSISServer** | A saved profile name speeds your connection on subsequent logins. | 

5. Press the **ESC** key to close the info message that informs you that the profile is created and connected.

6. Verify your connection in the status bar.

## Run the T-SQL code
Run the following Transact-SQL code to run an SSIS package.

1. In the **Editor** window, enter the following query in the empty query window. (This code is the code generated by the **Script** option in the **Execute Package** dialog box in SSMS.)

2. Update the parameter values in the `catalog.create_execution` stored procedure for your system.

3. Press **CTRL+SHIFT+E** to run the code and run the package.

```sql
Declare @execution_id bigint
EXEC [SSISDB].[catalog].[create_execution] @package_name=N'Package.dtsx',
    @execution_id=@execution_id OUTPUT,
    @folder_name=N'Deployed Projects',
	  @project_name=N'Integration Services Project1',
  	@use32bitruntime=False,
	  @reference_id=Null
Select @execution_id
DECLARE @var0 smallint = 1
EXEC [SSISDB].[catalog].[set_execution_parameter_value] @execution_id,
    @object_type=50,
	  @parameter_name=N'LOGGING_LEVEL',
	  @parameter_value=@var0
EXEC [SSISDB].[catalog].[start_execution] @execution_id
GO
```

## Next steps
- Consider other ways to run a package.
    - [Run an SSIS package with SSMS](./ssis-quickstart-run-ssms.md)
    - [Run an SSIS package with Transact-SQL (SSMS)](./ssis-quickstart-run-tsql-ssms.md)
    - [Run an SSIS package from the command prompt](./ssis-quickstart-run-cmdline.md)
    - [Run an SSIS package with PowerShell](ssis-quickstart-run-powershell.md)
    - [Run an SSIS package with C#](./ssis-quickstart-run-dotnet.md) 
