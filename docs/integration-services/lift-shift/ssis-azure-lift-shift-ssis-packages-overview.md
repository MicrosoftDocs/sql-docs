---
title: "Lift and shift SQL Server Integration Services workloads to the cloud | Microsoft Docs"
ms.date: "04/13/2018"
ms.topic: "article"
ms.prod: "sql-non-specified"
ms.prod_service: "integration-services"
ms.service: ""
ms.component: "lift-shift"
ms.suite: "sql"
ms.custom: ""
ms.technology: 
  - "integration-services"
author: "douglaslMS"
ms.author: "douglasl"
manager: "craigg"
ms.workload: "Inactive"
---
# Lift and shift SQL Server Integration Services workloads to the cloud
You can now move your SQL Server Integration Services (SSIS) packages and workloads to the Azure cloud.
-   Store and manage SSIS projects and packages in the SSIS Catalog database (SSISDB) on Azure SQL Database or SQL Database Managed Instance (Preview).
-   Run packages in an instance of the Azure SSIS Integration Runtime, introduced as part of Azure Data Factory version 2.
-   Use familiar tools such as SQL Server Management Studio (SSMS) for these common tasks.

## Benefits
Moving your on-premises SSIS workloads to Azure has the following potential benefits:
-   **Reduce operational costs** and reduce the burden of managing infrastructure that you have when you run SSIS on-premises or on Azure virtual machines.
-   **Increase high availability** with the ability to specify multiple nodes per cluster, as well as the high availability features of Azure and of Azure SQL Database.
-   **Increase scalability** with the ability to specify multiple cores per node (scale up) and multiple nodes per cluster (scale out).

## Architecture overview
The following table highlights the differences between SSIS on premises and SSIS on Azure. The most significant difference is the separation of storage from compute.

| Storage | Runtime | Scalability |
|---|---|---|
| On premises (SQL Server) | SSIS runtime hosted by SQL Server | SSIS Scale Out (in SQL Server 2017 and later)<br/><br/>Custom solutions (in prior versions of SQL Server) |
| On Azure (SQL Database or SQL Database Managed Instance (Preview)) | Azure SSIS Integration Runtime, a component of Azure Data Factory version 2 | Scaling options for the SSIS IR |
| | | |

Azure Data Factory hosts the runtime engine for SSIS packages on Azure. The runtime engine is called the Azure SSIS Integration Runtime (SSIS IR).

When you provision the SSIS IR, you can scale up and scale out by specifying values for the following options:
-   The node size (including the number of cores) and the number of nodes in the cluster.
-   The existing instance of Azure SQL Database to host the SSIS Catalog Database (SSISDB), and the service tier for the database.
-   The maximum parallel executions per node.

You only have to provision the SSIS IR one time. After that, you can use familiar tools such as SQL Server Data Tools (SSDT) and SQL Server Management Studio (SSMS) to deploy, configure, run, monitor, schedule, and manage packages.

> [!NOTE]
> During this public preview, the Azure SSIS Integration Runtime is not yet available in all regions. For info about the supported regions, see [Products available by region - Microsoft Azure](https://azure.microsoft.com/regions/services/).

Data Factory also supports other types of Integration Runtimes. To learn more about the SSIS IR and the other types of Integration Runtimes, see [Integration runtime in Azure Data Factory](https://docs.microsoft.com/azure/data-factory/concepts-integration-runtime).

## Version info

You can deploy a package created with any version of SSIS to Azure. When you deploy a package to Azure, if there are no validation errors, the package is automatically upgraded to the latest package format. In other words, it is always upgraded to the latest version of SSIS.

The deployment process validates packages to ensure that they can run on the Azure-SSIS Integration Runtime. For more info, see [Validate SSIS packages deployed to Azure](ssis-azure-validate-packages.md).

## Prerequisites

The capabilities described in this article require the following versions of SQL Server Data Tools (SSDT):
-   For Visual Studio 2017, version 15.3 (preview) or later.
-   For Visual Studio 2015,  version 17.2 or later.

For more info about prerequisites in Azure, see [Deploy SSIS packages to Azure](https://docs.microsoft.com/azure/data-factory/tutorial-create-azure-ssis-runtime-portal).

## Provision SSIS on Azure
Before you can deploy and run SSIS packages in Azure, you have to provision the SSIS Catalog database (SSISDB) and the Azure-SSIS Integration Runtime. Follow the provisioning steps in this article: [Deploy SSIS packages to Azure](https://docs.microsoft.com/azure/data-factory/tutorial-create-azure-ssis-runtime-portal).

The **name of the SQL Database** that hosts SSISDB becomes the first part of the four-part name to use when you deploy and manage packages from SSDT and SSMS - `<sql_database_name>.database.windows.net`.

For info about how to connect to the SSIS Catalog database, see [Connect to the SSISDB Catalog database on Azure](ssis-azure-connect-to-catalog-database.md).

## Design packages
You continue to **design and build packages** on-premises in SSDT, or in Visual Studio with SSDT installed.

For info about how to connect to on-premises data sources from the cloud with **Windows authentication**, see [Connect to on-premises data sources and Azure file shares with Windows Authentication](ssis-azure-connect-with-windows-auth.md).

For info about how to connect to files and file shares, see [Store and retrieve files on file shares on premises and in Azure with SSIS](ssis-azure-files-file-shares.md).

When you provision an instance of SQL Database to host SSISDB, the Azure Feature Pack for SSIS and the Access Redistributable are also installed. These components provide connectivity to various **Azure** data sources and to **Excel and Access** files, in addition to the data sources supported by the built-in components.

You can also install additional components. For more info, see [Custom setup for the Azure-SSIS integration runtime](/azure/articles/data-factory/how-to-configure-azure-ssis-ir-custom-setup.md).

## Deploy and run packages
You have to use the **project deployment model**, not the package deployment model, when you deploy projects to SSISDB on Azure.

To deploy projects and run packages on Azure, you can use one of several familiar tools and scripting options:
-   SQL Server Management Studio (SSMS)
-   Transact-SQL (from SSMS, Visual Studio Code, or another tool)
-   A command-line tool
-   PowerShell
-   C# and the SSIS management object model

To get started, see [Deploy, run, and monitor an SSIS package on Azure](ssis-azure-deploy-run-monitor-tutorial.md).

## Monitor packages
To monitor running packages in SSMS, you can use one of the following reporting tools in SSMS.
-   Right-click **SSISDB**, and then select **Active Operations** to open the **Active Operations** dialog box.
-   Select a package in Object Explorer, right-click and select **Reports**, then **Standard Reports**, then **All Executions**.

## Schedule packages
To schedule the execution of packages stored in SQL Database, you can use the following tools:
-   SQL Server Agent on-premises
-   The Data Factory SQL Server Stored Procedure activity

For more info, see [Schedule SSIS package execution on Azure](ssis-azure-schedule-packages.md).

## Next steps
To get started with SSIS workloads on Azure, see the following articles:
-   [Deploy SSIS packages to Azure](https://docs.microsoft.com/azure/data-factory/tutorial-create-azure-ssis-runtime-portal)
-   [Deploy, run, and monitor an SSIS package on Azure](ssis-azure-deploy-run-monitor-tutorial.md)
