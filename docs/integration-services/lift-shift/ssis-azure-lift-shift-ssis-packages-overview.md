---
title: "Move SQL Server Integration Services workloads to the cloud | Microsoft Docs"
ms.date: "09/25/2017"
ms.topic: "article"
ms.prod: "sql-server-2017"
ms.technology: 
  - "integration-services"
author: "douglaslMS"
ms.author: "douglasl"
manager: "craigg"
---
# Move SQL Server Integration Services workloads to the cloud
You can now move your SQL Server Integration Services (SSIS) packages and workloads to the Azure cloud. You can store and manage SSIS packages in the SSIS Catalog database (SSISDB) on Azure SQL Database. When you run packages, they run in an instance of the Azure SSIS Integration Runtime.

## Benefits
Moving your on-premises SSIS workloads to Azure has the following potential benefits:
-   **Reduce operational costs** by reducing on-premises infrastructure.
-   **Increase high availability** with multiple nodes per cluster, as well as the high availability features of Azure and of Azure SQL Database.
-   **Increase scalability** with the ability to specify multiple cores per node (scale up) and multiple nodes per cluster (scale out).
-   **Avoid the limitations** of running SSIS on Azure virtual machines.

## Architecture overview
Azure Data Factory hosts the runtime engine for SSIS packages, which is called the Azure SSIS Integration Runtime (SSIS IR). 

When you provision the SSIS IR, you can specify values for the following options:
-   The node size (including the number of cores) and the number of nodes in the cluster.
-   The existing instance of Azure SQL Database to host the SSIS Catalog Database (SSISDB), and the service tier for the database.
-   The maximum parallel executions per node.

You only have to provision the SSIS IR one time. After that, you can use familiar tools such as SQL Server Data Tools (SSDT) and SQL Server Management Studio (SSMS) to deploy, configure, run, monitor, schedule, and manage packages.

Data Factory also supports other types of Integration Runtimes. To learn more about the SSIS IR and the other types of Integration Runtimes, see [Integration runtime in Azure Data Factory](/azure/data-factory/concepts-integration-runtime.md).

## Package features
When you provision an instance of SQL Database to host SSISDB, the Azure Feature Pack for SSIS and the Access Redistributable are installed. These components provide connectivity to Excel and Access files and to various Azure data sources. You can't install third-party components for SSIS at this time.

You continue to design and build packages on-premises in SSDT, or in Visual Studio with SSDT installed.

You have to use the project deployment model, not the package deployment model, for projects you deploy to SSISDB on Azure SQL Database.

The name of the SQL Database that hosts SSISDB becomes the first part of the four-part name to use when you deploy and manage packages from SSDT and SSMS - `<sql_database_name>.database.windows.net`.

For info about how to connect to on-premises data sources from the cloud with Windows authentication, see[my windows auth article](ssis-azure-connect-with-windows-auth.md).

## Common tasks

### Provision
Before you can deploy and run SSIS packages in Azure, you have to provision the Azure SSIS Integration Runtime. Follow the provisioning steps in this article: [Lift and shift SQL Server Integration Services (SSIS) packages to Azure](/azure/data-factory/quickstart-lift-shift-ssis-packages-powershell.md).

### Deploy and run packages
To deploy projects and run packages on SQL Database, use one of several familiar tools and scripting options, as shown in the following table:


|Option  |Deploy a project  |Run a package |
|---------|---------|---------|
|SSMS     | [Deploy with SSMS](ssis-everest-quickstart-deploy-ssms.md)         | [Run with SSMS](ssis-everest-quickstart-run-ssms.md)        |
|Transact-SQL scripts running in SSMS     | [Deploy with T-SQL in SSMS](ssis-everest-quickstart-deploy-tsql-ssms.md)        | [Run with T-SQL in SSMS](ssis-everest-quickstart-run-tsql-ssms.md)        |
|Transact-SQL scripts running in Visual Studio Code     | [Deploy with T-SQL in VS Code](ssis-everest-quickstart-deploy-tsql-vscode.md)        | [Run with T-SQL in VS Code](ssis-everest-quickstart-run-tsql-vscode.md)        |
|SSIS object model code running in PowerShell     | [Deploy with PowerShell](ssis-everest-quickstart-deploy-powershell.md)        | [Run with PowerShell](ssis-everest-quickstart-deploy-powershell.md)        |
|SSIS object model code running in a C# app     | [Deploy with C#](ssis-everest-quickstart-deploy-dotnet.md)       | [Run with C#](ssis-everest-quickstart-run-dotnet.md)        |
| | |

### Monitor packages
To monitor running packages in SSMS, do one of the following things in SSMS.
-   Right-click **SSISDB**, and then select **Active Operations** to open the **Active Operations** dialog box.
-   Select a package in Object Explorer, right-click and select **Reports**, then **Standard Reports**, then **All Executions**.

### Schedule packages
To schedule the execution of packages stored in SQL Database, you can use the following tools:
-   SQL Server Agent on-premises
-   The Data Factory SQL Server Stored Procedure activity

For more info, see [Schedule packages](ssis-everest-howto-schedule-package.md).

## Next steps
To get started with SSIS workloads on Azure SQL Database, see the following articles:
-   [Provision](ssis-everest-quickstart-provision.md)
-   [Tutorial - Deploy, run, monitor a package](ssis-everest-tutorial-deploy-run-monitor.md)
