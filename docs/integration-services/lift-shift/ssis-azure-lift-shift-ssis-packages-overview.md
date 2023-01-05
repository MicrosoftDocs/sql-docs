---
title: "Deploy and run SSIS packages in Azure | Microsoft Docs"
description: Learn how you can move your SQL Server Integration Services (SSIS) projects, packages, and workloads to the Microsoft Azure cloud.
ms.date: "04/06/2022"
ms.topic: conceptual
ms.service: sql
ms.custom:
  - intro-deployment
ms.subservice: integration-services
author: swinarko
ms.author: sawinark
ms.reviewer: maghan
---
# Lift and shift SQL Server Integration Services workloads to the cloud

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


You can now move your SQL Server Integration Services (SSIS) projects, packages, and workloads to the Azure cloud. Deploy, run, and manage SSIS projects and packages in the SSIS Catalog (SSISDB) on Azure SQL Database or SQL Managed Instance with familiar tools such as SQL Server Management Studio (SSMS).

## Benefits
Moving your on-premises SSIS workloads to Azure has the following potential benefits:
-   **Reduce operational costs** and reduce the burden of managing infrastructure that you have when you run SSIS on-premises or on Azure virtual machines.
-   **Increase high availability** with the ability to specify multiple nodes per cluster, as well as the high availability features of Azure and of Azure SQL Database.
-   **Increase scalability** with the ability to specify multiple cores per node (scale up) and multiple nodes per cluster (scale out).

## Architecture of SSIS on Azure
The following table highlights the differences between SSIS on premises and SSIS on Azure.

The most significant difference is the separation of storage from runtime. Azure Data Factory hosts the runtime engine for SSIS packages on Azure. The runtime engine is called the Azure-SSIS Integration Runtime (Azure-SSIS IR). For more info, see [Azure-SSIS Integration Runtime](/azure/data-factory/concepts-integration-runtime#azure-ssis-integration-runtime).

| Location | Storage | Runtime | Scalability |
|---|---|---|---|
| On premises | SQL Server | SSIS runtime hosted by SQL Server | SSIS Scale Out (in SQL Server 2017 and later)<br/><br/>Custom solutions (in prior versions of SQL Server) |
| On Azure | SQL Database or SQL Managed Instance | Azure-SSIS Integration Runtime, a component of Azure Data Factory | Scaling options for the Azure-SSIS Integration Runtime |

## Provision SSIS on Azure

**Provision**. Before you can deploy and run SSIS packages in Azure, you have to provision the SSIS Catalog (SSISDB) and the Azure-SSIS Integration Runtime.

-   To provision SSIS on Azure in the Azure portal, follow the provisioning steps in this article: [Provision the Azure-SSIS Integration Runtime in Azure Data Factory](/azure/data-factory/tutorial-deploy-ssis-packages-azure). 

-   To provision SSIS on Azure with PowerShell, follow the provisioning steps in this article: [Provision the Azure-SSIS Integration Runtime in Azure Data Factory with PowerShell](/azure/data-factory/tutorial-deploy-ssis-packages-azure-powershell).

You only have to provision the Azure-SSIS IR one time. After that, you can use familiar tools such as SQL Server Data Tools (SSDT) and SQL Server Management Studio (SSMS) to deploy, configure, run, monitor, schedule, and manage packages.

> [!NOTE]
> The Azure-SSIS Integration Runtime is not yet available in all Azure regions. For info about the supported regions, see [Products available by region - Microsoft Azure](https://azure.microsoft.com/regions/services/).

**Scale up and out**. When you provision the Azure-SSIS IR, you can scale up and scale out by specifying values for the following options:
-   The node size (including the number of cores) and the number of nodes in the cluster.
-   The existing instance of Azure SQL Database to host the SSIS Catalog Database (SSISDB), and the service tier for the database.
-   The maximum parallel executions per node.

**Improve performance**. For more info, see [Configure the Azure-SSIS Integration Runtime for high performance](/azure/data-factory/configure-azure-ssis-integration-runtime-performance).

**Reduce costs**. To reduce costs, run the Azure-SSIS IR only when you need it. For more info, see [How to schedule starting and stopping of an Azure SSIS integration runtime](/azure/data-factory/how-to-schedule-azure-ssis-integration-runtime).

## Design packages

You continue to **design and build packages** on-premises in SSDT, or in Visual Studio with SSDT installed.

### Connect to data sources

To connect to on-premises data sources from the cloud with **Windows authentication**, see [Connect to data sources and file shares with Windows Authentication from SSIS packages in Azure](/azure/data-factory/ssis-azure-connect-with-windows-auth).

To connect to files and file shares, see [Open and save files on premises and in Azure with SSIS packages deployed in Azure](/azure/data-factory/ssis-azure-files-file-shares).

### Available SSIS components

When you provision an instance of SQL Database to host SSISDB, the Azure Feature Pack for SSIS and the Access Redistributable are also installed. These components provide connectivity to various **Azure** data sources and to **Excel and Access** files, in addition to the data sources supported by the built-in components.

You can also install additional components - for example, you can install a driver that's not installed by default. For more info, see [Customize setup for the Azure-SSIS integration runtime](/azure/data-factory/how-to-configure-azure-ssis-ir-custom-setup).

If you have an Enterprise Edition license, additional components are available. For more info, see [Provision Enterprise Edition for the Azure-SSIS Integration Runtime](/azure/data-factory/how-to-configure-azure-ssis-ir-enterprise-edition).

If you're an ISV, you can update the installation of your licensed components to make them available on Azure. For more info, see [Install paid or licensed custom components for the Azure-SSIS integration runtime](/azure/data-factory/how-to-develop-azure-ssis-ir-licensed-components).

## Deploy and run packages

To get started, see [Tutorial: Deploy and run a SQL Server Integration Services (SSIS) package in Azure](ssis-azure-deploy-run-monitor-tutorial.md).

### Prerequisites

To deploy SSIS packages to Azure, you have to have one of the following versions of SQL Server Data Tools (SSDT):
-   For Visual Studio 2017, version 15.3 or later.
-   For Visual Studio 2015,  version 17.2 or later.

### Connect to SSISDB

The **name of the SQL Database** that hosts SSISDB becomes the first part of the four-part name to use when you deploy and run packages from SSDT and SSMS, in the following format - `<sql_database_name>.database.windows.net`. For more info about how to connect to the SSIS Catalog database in Azure, see [Connect to the SSIS Catalog (SSISDB) in Azure](ssis-azure-connect-to-catalog-database.md).

### Deploy projects and packages

You have to use the **project deployment model**, not the package deployment model, when you deploy projects to SSISDB on Azure.

To deploy projects on Azure, you can use one of several familiar tools and scripting options:
-   SQL Server Management Studio (SSMS)
-   Transact-SQL (from SSMS, Visual Studio Code, or another tool)
-   A command-line tool
-   PowerShell or C# and the SSIS management object model

The deployment process validates packages to ensure that they can run on the Azure-SSIS Integration Runtime. For more info, see [Validate SQL Server Integration Services (SSIS) packages deployed to Azure](ssis-azure-validate-packages.md).

For a deployment example that uses SSMS and the Integration Services Deployment Wizard, see [Tutorial: Deploy and run a SQL Server Integration Services (SSIS) package in Azure](ssis-azure-deploy-run-monitor-tutorial.md).

### Version support

You can deploy a package created with any version of SSIS to Azure. When you deploy a package to Azure, if there are no validation errors, the package is automatically upgraded to the latest package format. In other words, it is always upgraded to the latest version of SSIS.

### Run packages

To run SSIS packages deployed in Azure, you can use a variety of methods. For more info, see [Run SQL Server Integration Services (SSIS) packages deployed in Azure](ssis-azure-run-packages.md).

### Run packages in an Azure Data Factory pipeline

To run an SSIS package in an Azure Data Factory pipeline, use the Execute SSIS Package Activity. For more info, see [Run an SSIS package using the Execute SSIS Package Activity in Azure Data Factory](/azure/data-factory/how-to-invoke-ssis-package-ssis-activity).

When you run a package in a Data Factory pipeline with the Execute SSIS Package Activity, you can pass values to the package at runtime. To pass one or more runtime values, create SSIS execution environments in SSISDB with SQL Server Management Studio (SSMS). In each environment, create variables and assign values that correspond to the parameters for your projects or packages. Configure your SSIS packages in SSMS to associate those environment variables with your project or package parameters. When you run the packages in the pipeline, switch between environments by specifying different environment paths on the Settings tab of the Execute SSIS Package activity UI. For more info about SSIS environments, see [Create and Map a Server Environment](../packages/deploy-integration-services-ssis-projects-and-packages.md#create-and-map-a-server-environment).

## Monitor packages

To monitor running packages, use the following reporting options in SSMS.
-   Right-click **SSISDB**, and then select **Active Operations** to open the **Active Operations** dialog box.
-   Select a package in Object Explorer, right-click and select **Reports**, then **Standard Reports**, then **All Executions**.

To monitor the Azure-SSIS Integration Runtime, see [Monitor the Azure-SSIS integration runtime](/azure/data-factory/monitor-integration-runtime#azure-ssis-integration-runtime).

## Schedule packages
To schedule the execution of packages deployed in Azure, you can use a variety of tools. For more info, see [Schedule the execution of SQL Server Integration Services (SSIS) packages deployed in Azure](ssis-azure-schedule-packages.md).

## Next steps
To get started with SSIS workloads on Azure, see the following articles:
-   [Tutorial: Deploy and run a SQL Server Integration Services (SSIS) package in Azure](ssis-azure-deploy-run-monitor-tutorial.md)
-   [Provision the Azure-SSIS Integration Runtime in Azure Data Factory](/azure/data-factory/tutorial-deploy-ssis-packages-azure)
