---
title: "Deploy, run, and monitor an SSIS package on Azure | Microsoft Docs"
ms.date: "09/25/2017"
ms.topic: "article"
ms.prod: "sql-server-2017"
ms.technology: 
  - "integration-services"
author: "douglaslMS"
ms.author: "douglasl"
manager: "craigg"
---
# Deploy, run, and monitor an SSIS package on Azure
This tutorial shows you how to deploy a SQL Server Integration Services project to the SSISDB Catalog database on Azure SQL Database, run a package in the Azure-SSIS Integration Runtime, and monitor the running package.

## Prerequisites

Before you start, make sure you have version 17.2 or later of SQL Server Management Studio. To download the latest version of SSMS, see [Download SQL Server Management Studio (SSMS)](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms).

Also make sure that you have set up the SSISDB database and provisioned the Azure-SSIS Integration Runtime. For info about how to provision SSIS on Azure, see [Lift and shift SQL Server Integration Services (SSIS) packages to Azure](/azure/data-factory/quickstart-lift-shift-ssis-packages-powershell.md).

## Connect to the SSISDB database

Use SQL Server Management Studio to connect to the SSIS Catalog on your Azure SQL Database server. For more info, see [Connect to the SSISDB Catalog database on Azure](ssis-azure-connect-to-catalog-database.md).

> [!IMPORTANT]
> An Azure SQL Database server listens on port 1433. If you are attempting to connect to an Azure SQL Database server from within a corporate firewall, this port must be open in the corporate firewall for you to connect successfully.

1. Open SQL Server Management Studio.

2. **Connect to the server**. In the **Connect to Server** dialog box, enter the following information:

   | Setting       | Suggested value | Description | 
   | ------------ | ------------------ | ------------------------------------------------- | 
   | **Server type** | Database Engine | This value is required. |
   | **Server name** | The fully qualified server name | The name should be in this format: **mysqldbserver.database.windows.net**. If you need the server name, see [Connect to the SSISDB Catalog database on Azure](ssis-azure-connect-to-catalog-database.md). |
   | **Authentication** | SQL Server Authentication | This quickstart uses SQL authentication. |
   | **Login** | The server admin account | This is the account that you specified when you created the server. |
   | **Password** | The password for your server admin account | This is the password that you specified when you created the server. |

3. **Connect to the SSISDB database**. Select **Options** to expand the **Connect to Server** dialog box. In the expanded **Connect to Server** dialog box, select the **Connection Properties** tab. In the **Connect to database** field, select or enter `SSISDB`.

4. Then select **Connect**. The Object Explorer window opens in SSMS. 

5. In Object Explorer, expand **Integration Services Catalogs** and then expand **SSISDB** to view the objects in the SSIS Catalog database.

## Deploy a project

### Start the Integration Services Deployment Wizard
1. In Object Explorer in SSMS, with the **Integration Services Catalogs** node and the **SSISDB** node expanded, expand a project folder.

2.  Select the **Projects** node.

3.  Right-click on the **Projects** node and select **Deploy project**. The Integration Services Deployment Wizard opens. You can deploy a project from an SSIS Catalog database or from the file system.

### Deploy a project with the Deployment Wizard
1. On the **Introduction** page of the Deployment Wizard, review the introduction. Select **Next** to open the **Select Source** page.

2. On the **Select Source** page, select the existing SSIS project to deploy.
    -   To deploy a project deployment file that you created, select **Project deployment file** and enter the path to the .ispac file.
    -   To deploy a project that resides in an SSIS catalog, select **Integration Services catalog**, and then enter the server name and the path to the project in the catalog.
    -   Select **Next** to see the **Select Destination** page.
  
3.  On the **Select Destination** page, select the destination for the project.
    -   Enter the fully qualified server name in the format `<server_name>.database.windows.net`.
    -   Then select **Browse** to select the target folder in SSISDB.
    -   Select **Next** to open the **Review** page.  
  
4.  On the **Review** page, review the settings you selected.
    -   You can change your selections by selecting **Previous**, or by selecting any of the steps in the left pane.
    -   Select **Deploy** to start the deployment process.
  
5.  After the deployment process is complete, the **Results** page opens. This page displays the success or failure of each action.
    -   If the action failed, select **Failed** in the **Result** column to display an explanation of the error.
    -   Optionally, select **Save Report...** to save the results to an XML file.
    -   Select **Close** to exit the wizard.

## Run a package

1. In Object Explorer in SSMS, select the package that you want to run.

2. Right-click and select **Execute** to open the **Execute Package** dialog box.

3.  In the **Execute Package** dialog box, configure the package execution by using the settings on the **Parameters**, **Connection Managers**, and **Advanced** tabs.

4.  Select **OK** to run the package.

## Monitor the running package in SSMS

To view the status of currently running Integration Services operations on the Integration Services server, such as deployment, validation, and package execution, use the **Active Operations** dialog box in SSMS. To open the **Active Operations** dialog box, right-click **SSISDB**, and then select **Active Operations**.

You can also select a package in Object Explorer, right-click and select **Reports**, then **Standard Reports**, then **All Executions**.

For more info about how to monitor running packages in SSMS, see [Monitor Running Packages and Other Operations](https://docs.microsoft.com/en-us/sql/integration-services/performance/monitor-running-packages-and-other-operations).

## Monitor the Azure-SSIS Integration Runtime

To get status info about the Azure-SSIS Integration Runtime in which packages are running, use the following PowerShell commands: For each of the commands, provide the names of the Data Factory, the Azure-SSIS IR, and the resource group.

### Get metadata about the Azure-SSIS Integration Runtime

```powershell
Get-AzureRmDataFactoryV2IntegrationRuntime -DataFactoryName $DataFactoryName -Name $AzureSsisIRName -ResourceGroupName $ResourceGroupName
```

### Get the status of the Azure-SSIS Integration Runtime

```powershell
Get-AzureRmDataFactoryV2IntegrationRuntimeStatus -DataFactoryName $DataFactoryName -Name $AzureSsisIRName -ResourceGroupName $ResourceGroupName
```

## Next steps
- Learn how to schedule package execution. For more info, see [Schedule page](ssis-everest-howto-schedule-package.md)
