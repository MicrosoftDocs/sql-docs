---
title: "Lift and shift SQL Server Integration Services workloads to the cloud | Microsoft Docs"
ms.date: "09/25/2017"
ms.topic: "article"
ms.prod: "sql-server-2017"
ms.technology: 
  - "integration-services"
author: "douglaslMS"
ms.author: "douglasl"
manager: "craigg"
---
# Lift and shift SQL Server Integration Services workloads to the cloud
You can now move your SQL Server Integration Services (SSIS) packages and workloads to the Azure cloud.
-   Store and manage SSIS projects and packages in the SSIS Catalog database (SSISDB) on Azure SQL Database.
-   Run packages in an instance of the Azure SSIS Integration Runtime, introduced as part of Azure Data Factory version 2.
-   Use familiar tools such as SQL Server Management Studio (SSMS) for these common tasks.

## Benefits
Moving your on-premises SSIS workloads to Azure has the following potential benefits:
-   **Reduce operational costs** by reducing on-premises infrastructure.
-   **Increase high availability** with multiple nodes per cluster, as well as the high availability features of Azure and of Azure SQL Database.
-   **Increase scalability** with the ability to specify multiple cores per node (scale up) and multiple nodes per cluster (scale out).
-   **Avoid the limitations** of running SSIS on Azure virtual machines.

## Architecture overview
The following table highlights the differences between SSIS on premises and SSIS on Azure. The most significant difference is the separation of storage from compute.

| Storage | Runtime | Scalability |
|---|---|---|
| On premises (SQL Server) | SSIS runtime hosted by SQL Server | SSIS Scale Out (in SQL Server 2017 and later)<br/><br/>Custom solutions (in prior versions of SQL Server) |
| On Azure (SQL Database) | Azure SSIS Integration Runtime, a component of Azure Data Factory version 2 | Scaling options for the SSIS IR |
| | | |

Azure Data Factory hosts the runtime engine for SSIS packages on Azure. The runtime engine is called the Azure SSIS Integration Runtime (SSIS IR).

When you provision the SSIS IR, you can scale up and scale out by specifying values for the following options:
-   The node size (including the number of cores) and the number of nodes in the cluster.
-   The existing instance of Azure SQL Database to host the SSIS Catalog Database (SSISDB), and the service tier for the database.
-   The maximum parallel executions per node.

You only have to provision the SSIS IR one time. After that, you can use familiar tools such as SQL Server Data Tools (SSDT) and SQL Server Management Studio (SSMS) to deploy, configure, run, monitor, schedule, and manage packages.

Data Factory also supports other types of Integration Runtimes. To learn more about the SSIS IR and the other types of Integration Runtimes, see [Integration runtime in Azure Data Factory](/azure/data-factory/concepts-integration-runtime.md).

## Package features on Azure
When you provision an instance of SQL Database to host SSISDB, the Azure Feature Pack for SSIS and the Access Redistributable are installed. These components provide connectivity to Excel and Access files and to various Azure data sources. You can't install third-party components for SSIS at this time.

You continue to design and build packages on-premises in SSDT, or in Visual Studio with SSDT installed.

You have to use the project deployment model, not the package deployment model, for projects you deploy to SSISDB on Azure SQL Database.

The name of the SQL Database that hosts SSISDB becomes the first part of the four-part name to use when you deploy and manage packages from SSDT and SSMS - `<sql_database_name>.database.windows.net`.

For info about how to connect to on-premises data sources from the cloud with Windows authentication, see [Connect to on-premises data sources with Windows Authentication](ssis-azure-connect-with-windows-auth.md).

## Common tasks

### Provision
Before you can deploy and run SSIS packages in Azure, you have to provision the SSISDB Catalog database and the Azure SSIS Integration Runtime. Follow the provisioning steps in this article: [Lift and shift SQL Server Integration Services (SSIS) packages to Azure](/azure/data-factory/quickstart-lift-shift-ssis-packages-powershell.md).

### Deploy and run packages
To deploy projects and run packages on SQL Database, you can use one of several familiar tools and scripting options:
-   SQL Server Management Studio (SSMS)
-   Transact-SQL (from SSMS, Visual Studio Code, or another tool)
-   A command-line tool
-   PowerShell
-   C# and the SSIS management object model

### Monitor packages
To monitor running packages in SSMS, you can use one of the following reporting tools in SSMS.
-   Right-click **SSISDB**, and then select **Active Operations** to open the **Active Operations** dialog box.
-   Select a package in Object Explorer, right-click and select **Reports**, then **Standard Reports**, then **All Executions**.

### Schedule packages
To schedule the execution of packages stored in SQL Database, you can use the following tools:
-   SQL Server Agent on-premises
-   The Data Factory SQL Server Stored Procedure activity

For more info, see [Schedule packages](ssis-everest-howto-schedule-package.md).

## Next steps
To get started with SSIS workloads on Azure, see the following articles:
-   [Lift and shift SQL Server Integration Services (SSIS) packages to Azure](/azure/data-factory/quickstart-lift-shift-ssis-packages-powershell.md)
-   [Deploy, run, and monitor an SSIS package on Azure](ssis-azure-deploy-run-monitor-tutorial.md)
