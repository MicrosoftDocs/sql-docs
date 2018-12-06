---
title: "Deploy an SSIS project with SSMS | Microsoft Docs"
ms.date: "05/21/2018"
ms.topic: quickstart
ms.prod: sql
ms.prod_service: "integration-services"
ms.custom: ""
ms.technology: integration-services
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Deploy an SSIS project with SQL Server Management Studio (SSMS)
This quickstart how to use SQL Server Management Studio (SSMS) to connect to the SSIS Catalog database, and then run the Integration Services Deployment Wizard to deploy an SSIS project to the SSIS Catalog. 

SQL Server Management Studio is an integrated environment for managing any SQL infrastructure, from SQL Server to SQL Database. For more info about SSMS, see [SQL Server Management Studio (SSMS)](../ssms/sql-server-management-studio-ssms.md).

## Prerequisites

Before you start, make sure you have the latest version of SQL Server Management Studio. To download SSMS, see [Download SQL Server Management Studio (SSMS)](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms).

The validation described in this article for deployment to Azure SQL Database requires SQL Server Data Tools (SSDT) version 17.4 or later. To get the latest version of SSDT, see [Download SQL Server Data Tools (SSDT)](../ssdt/download-sql-server-data-tools-ssdt.md).

An Azure SQL Database server listens on port 1433. If you're trying to connect to an Azure SQL Database server from within a corporate firewall, this port must be open in the corporate firewall for you to connect successfully.

## Supported platforms

You can use the information in this quickstart to deploy an SSIS project to the following platforms:

-   SQL Server on Windows.

-   Azure SQL Database. For more info about deploying and running packages in Azure, see [Lift and shift SQL Server Integration Services workloads to the cloud](lift-shift/ssis-azure-lift-shift-ssis-packages-overview.md).

You cannot use the information in this quickstart to deploy an SSIS package to SQL Server on Linux. For more info about running packages on Linux, see [Extract, transform, and load data on Linux with SSIS](../linux/sql-server-linux-migrate-ssis.md).

## For Azure SQL Database, get the connection info

To deploy the project to Azure SQL Database, get the connection information you need to connect to the SSIS Catalog database (SSISDB). You need the fully qualified server name and login information in the procedures that follow.

1. Log in to the [Azure portal](https://portal.azure.com/).
2. Select **SQL Databases** from the left-hand menu, and then select the SSISDB database on the **SQL databases** page. 
3. On the **Overview** page for your database, review the fully qualified server name. To see the **Click to copy** option, hover over the server name. 
4. If you forget your Azure SQL Database server login information, navigate to the SQL Database server page to view the server admin name. You can reset the password if necessary.

## <a name="wizard_auth"></a> Authentication methods in the Deployment Wizard

If you're deploying to a SQL Server with the Deployment Wizard, you have to use Windows authentication; you can't use SQL Server authentication.

If you're deploying to an Azure SQL Database server, you have to use SQL Server authentication or Azure Active Directory authentication; you can't use Windows authentication.

## Connect to the SSISDB database

Use SQL Server Management Studio to establish a connection to the SSIS Catalog. 

1. Open SQL Server Management Studio.

2. In the **Connect to Server** dialog box, enter the following information:

   | Setting       | Suggested value | More info | 
   | ------------ | ------------------ | ------------------------------------------------- | 
   | **Server type** | Database engine | This value is required. |
   | **Server name** | The fully qualified server name | If you're connecting to an Azure SQL Database server, the name is in this format: `<server_name>.database.windows.net`. |
   | **Authentication** | SQL Server Authentication | With SQL Server authentication, you can connect to SQL Server or to Azure SQL Database. See [Authentication methods in the Deployment Wizard](#wizard_auth) in this article. |
   | **Login** | The server admin account | This account is the account that you specified when you created the server. |
   | **Password** | The password for your server admin account | This password is the password that you specified when you created the server. |

3. Click **Connect**. The Object Explorer window opens in SSMS. 

4. In Object Explorer, expand **Integration Services Catalogs** and then expand **SSISDB** to view the objects in the SSIS Catalog database.

## Start the Integration Services Deployment Wizard
1. In Object Explorer, with the **Integration Services Catalogs** node and the **SSISDB** node expanded, expand a folder.

2.  Select the **Projects** node.

3.  Right-click on the **Projects** node and select **Deploy project**. The Integration Services Deployment Wizard opens. You can deploy a project from the current catalog or from the file system.

## Deploy a project with the wizard
1. On the **Introduction** page of the wizard, review the introduction. Click **Next** to open the **Select Source** page.

2. On the **Select Source** page, select the existing SSIS project to deploy.
    -   To deploy a project deployment file that you created by building a project in the development environment, select **Project deployment file** and enter the path to the .ispac file.
    -   To deploy a project that is already deployed to an SSIS catalog database, select **Integration Services catalog**, and then enter the server name and the path to the project in the catalog.
    Click **Next** to see the **Select Destination** page.
  
3.  On the **Select Destination** page, select the destination for the project.
    -   Enter the fully qualified server name. If the target server is an Azure SQL Database server, the name is in this format `<server_name>.database.windows.net`.
    -   Provide authentication information, and then select **Connect**. See [Authentication methods in the Deployment Wizard](#wizard_auth) in this article.
    -   Then select **Browse** to select the target folder in SSISDB.
    -   Then select **Next** to open the **Review** page. (The **Next** button is enabled only after you select **Connect**.)
  
4.  On the **Review** page, review the settings you selected.
    -   You can change your selections by clicking **Previous**, or by clicking any of the steps in the left pane.
    -   Click **Deploy** to start the deployment process.

5.  If you're deploying to an Azure SQL Database server, the **Validate** page opens and checks the packages in the project for known issues that may prevent the packages from running as expected in the Azure-SSIS Integration Runtime. For more info, see [Validate SSIS packages deployed to Azure](lift-shift/ssis-azure-validate-packages.md).

6.  After the deployment process is complete, the **Results** page opens. This page displays the success or failure of each action.
    -   If the action failed, click **Failed** in the **Result** column to display an explanation of the error.
    -   Optionally, click **Save Report...** to save the results to an XML file.
    -   Click **Close** to exit the wizard.

## Next steps
- Consider other ways to deploy a package.
    - [Deploy an SSIS package with Transact-SQL (SSMS)](./ssis-quickstart-deploy-tsql-ssms.md)
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
