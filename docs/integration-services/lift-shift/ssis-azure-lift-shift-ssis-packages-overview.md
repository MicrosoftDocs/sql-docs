---
title: "Lift and shift SQL Server Integration Services workloads to the cloud | Microsoft Docs"
ms.date: "05/22/2018"
ms.topic: conceptual
ms.prod: sql
ms.prod_service: "integration-services"
ms.component: "lift-shift"
ms.suite: "sql"
ms.custom: ""
ms.technology: 
  - "integration-services"
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Lift and shift SQL Server Integration Services workloads to the cloud
You can now move your SQL Server Integration Services (SSIS) packages and workloads to the Azure cloud.
-   Store and manage SSIS projects and packages in the SSIS Catalog database (SSISDB) on Azure SQL Database or SQL Database Managed Instance (Preview).
-   Run packages in an instance of the Azure-SSIS Integration Runtime, a component of Azure Data Factory.
-   Use familiar tools such as SQL Server Management Studio (SSMS) for common tasks.

## Benefits
Moving your on-premises SSIS workloads to Azure has the following potential benefits:
-   **Reduce operational costs** and reduce the burden of managing infrastructure that you have when you run SSIS on-premises or on Azure virtual machines.
-   **Increase high availability** with the ability to specify multiple nodes per cluster, as well as the high availability features of Azure and of Azure SQL Database.
-   **Increase scalability** with the ability to specify multiple cores per node (scale up) and multiple nodes per cluster (scale out).

## Architecture overview
The following table highlights the differences between SSIS on premises and SSIS on Azure. The most significant difference is the separation of storage from runtime. Azure Data Factory hosts the runtime engine for SSIS packages on Azure. The runtime engine is called the Azure-SSIS Integration Runtime (Azure-SSIS IR). For more info, see [Azure-SSIS Integration Runtime](https://docs.microsoft.com/azure/data-factory/concepts-integration-runtime#azure-ssis-integration-runtime).

| Storage | Runtime | Scalability |
|---|---|---|
| On premises (SQL Server) | SSIS runtime hosted by SQL Server | SSIS Scale Out (in SQL Server 2017 and later)<br/><br/>Custom solutions (in prior versions of SQL Server) |
| On Azure (SQL Database or SQL Database Managed Instance (Preview)) | Azure-SSIS Integration Runtime, a component of Azure Data Factory | Scaling options for the Azure-SSIS Integration Runtime |
| | | |

You only have to provision the Azure-SSIS IR one time. After that, you can use familiar tools such as SQL Server Data Tools (SSDT) and SQL Server Management Studio (SSMS) to deploy, configure, run, monitor, schedule, and manage packages.

## Version support

You can deploy a package created with any version of SSIS to Azure. When you deploy a package to Azure, if there are no validation errors, the package is automatically upgraded to the latest package format. In other words, it is always upgraded to the latest version of SSIS.

The deployment process validates packages to ensure that they can run on the Azure-SSIS Integration Runtime. For more info, see [Validate SSIS packages deployed to Azure](ssis-azure-validate-packages.md).

## Prerequisites

To deploy SSIS packages to Azure, you have to have one of the following versions of SQL Server Data Tools (SSDT):
-   For Visual Studio 2017, version 15.3 or later.
-   For Visual Studio 2015,  version 17.2 or later.

For info about the prerequisites for the Azure-SSIS Integration Runtime, see [Deploy SQL Server Integration Services packages to Azure - Prerequisites](https://docs.microsoft.com/azure/data-factory/tutorial-deploy-ssis-packages-azure#prerequisites).

> [!NOTE]
> During this public preview, the Azure-SSIS Integration Runtime is not yet available in all regions. For info about the supported regions, see [Products available by region - Microsoft Azure](https://azure.microsoft.com/regions/services/).

## Provision SSIS on Azure

Before you can deploy and run SSIS packages in Azure, you have to provision the SSIS Catalog database (SSISDB) and the Azure-SSIS Integration Runtime. Follow the provisioning steps in this article: [Deploy SQL Server Integration Services packages to Azure](https://docs.microsoft.com/azure/data-factory/tutorial-deploy-ssis-packages-azure).

When you provision the Azure-SSIS IR, you can scale up and scale out by specifying values for the following options:
-   The node size (including the number of cores) and the number of nodes in the cluster.
-   The existing instance of Azure SQL Database to host the SSIS Catalog Database (SSISDB), and the service tier for the database.
-   The maximum parallel executions per node.

For more info about performance, see [Configure the Azure-SSIS Integration Runtime for high performance](https://docs.microsoft.com/azure/data-factory/configure-azure-ssis-integration-runtime-performance).

## Design packages

You continue to **design and build packages** on-premises in SSDT, or in Visual Studio with SSDT installed.

### Connect to data sources

For info about how to connect to on-premises data sources from the cloud with **Windows authentication**, see [Connect to on-premises data sources and Azure file shares with Windows Authentication](ssis-azure-connect-with-windows-auth.md).

For info about how to connect to files and file shares, see [Store and retrieve files on file shares on premises and in Azure with SSIS](ssis-azure-files-file-shares.md).

### Available SSIS components

When you provision an instance of SQL Database to host SSISDB, the Azure Feature Pack for SSIS and the Access Redistributable are also installed. These components provide connectivity to various **Azure** data sources and to **Excel and Access** files, in addition to the data sources supported by the built-in components.

You can also install additional components - for example, you can install a driver that's not installed by default. For more info, see [Custom setup for the Azure-SSIS integration runtime](/azure/articles/data-factory/how-to-configure-azure-ssis-ir-custom-setup.md).

If you're an ISV, you can update the installation of your licensed components to make them available on Azure. For more info, see [Develop paid or licensed custom components for the Azure-SSIS integration runtime](https://docs.microsoft.com/azure/data-factory/how-to-develop-azure-ssis-ir-licensed-components).

### Transaction support

With SQL Server on premises and on Azure virtual machines, you can use Microsoft Distributed Transaction Coordinator (MSDTC) transactions. To configure MSDTC on each node of the Azure-SSIS IR, use the custom setup capability. For more info, see [Custom setup for the Azure-SSIS integration runtime](https://docs.microsoft.com/azure/data-factory/how-to-configure-azure-ssis-ir-custom-setup).

With Azure SQL Database, you can only use elastic transactions. For more info, see [Distributed transactions across cloud databases](https://docs.microsoft.com/azure/sql-database/sql-database-elastic-transactions-overview).

## Deploy and run packages

To get started, see [Deploy, run, and monitor an SSIS package on Azure](ssis-azure-deploy-run-monitor-tutorial.md).

### Connect to SSISDB

The **name of the SQL Database** that hosts SSISDB becomes the first part of the four-part name to use when you deploy and run packages from SSDT and SSMS, in the following format - `<sql_database_name>.database.windows.net`. For info about how to connect to the SSIS Catalog database in Azure, see [Connect to the SSISDB Catalog database on Azure](ssis-azure-connect-to-catalog-database.md).

### Deploy projects and packages

You have to use the **project deployment model**, not the package deployment model, when you deploy projects to SSISDB on Azure.

To deploy projects on Azure, you can use one of several familiar tools and scripting options:
-   SQL Server Management Studio (SSMS)
-   Transact-SQL (from SSMS, Visual Studio Code, or another tool)
-   A command-line tool
-   PowerShell or C# and the SSIS management object model

For a deployment example that uses SSMS and the Integration Services Deployment Wizard, see [Deploy, run, and monitor an SSIS package on Azure](ssis-azure-deploy-run-monitor-tutorial.md).

### Run packages

For an overview of the methods that you can use to run SSIS packages deployed to Azure, see [Run an SSIS package in Azure](ssis-azure-run-packages.md).

## Pass runtime values with environments

To pass one or more runtime values to packages that you run as part of an Azure Data Factory pipeline, create SSIS execution environments in SSISDB with SQL Server Management Studio (SSMS). In each environment, create variables and assign values that correspond to the parameters for your projects or packages. Configure your SSIS packages in SSMS to associate those environment variables with your project or package parameters. When you run the packages in a Data Factory pipeline, switch between environments by specifying different environment paths on the Settings tab of the Execute SSIS Package activity UI.

For more info about SSIS environments, see [Create and Map a Server Environment
](../packages/deploy-integration-services-ssis-projects-and-packages.md#create-and-map-a-server-environment). For more info about running a package as part of an Azure Data Factory pipeline, see [Run an SSIS package using the Execute SSIS Package Activity in Azure Data Factory](https://docs.microsoft.com/azure/data-factory/how-to-invoke-ssis-package-ssis-activity).

## Monitor packages
To monitor running packages in SSMS, you can use the following reporting tools in SSMS.
-   Right-click **SSISDB**, and then select **Active Operations** to open the **Active Operations** dialog box.
-   Select a package in Object Explorer, right-click and select **Reports**, then **Standard Reports**, then **All Executions**.

To monitor the Azure-SSIS Integration Runtime, see [Monitor the Azure-SSIS integration runtime](https://docs.microsoft.com/azure/data-factory/monitor-integration-runtime#azure-ssis-integration-runtime).

## Schedule packages
To schedule the execution of packages stored in Azure SQL Database, you can use a variety of tools. For more info, see [Schedule SSIS package execution on Azure](ssis-azure-schedule-packages.md).

## Next steps
To get started with SSIS workloads on Azure, see the following articles:
-   [Deploy SQL Server Integration Services packages to Azure](https://docs.microsoft.com/azure/data-factory/tutorial-deploy-ssis-packages-azure)
-   [Deploy, run, and monitor an SSIS package on Azure](ssis-azure-deploy-run-monitor-tutorial.md)
