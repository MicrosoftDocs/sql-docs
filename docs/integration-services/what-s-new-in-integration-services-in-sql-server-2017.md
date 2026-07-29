---
title: "What's New in Integration Services in SQL Server 2017"
description: What's New in Integration Services in SQL Server 2017
ms.reviewer: randolphwest
ms.date: 10/22/2025
ms.service: sql
ms.subservice: integration-services
ms.topic: whats-new
ms.custom:
  - intro-whats-new
---
# What's new in Integration Services in SQL Server 2017

[!INCLUDE [sqlserver-ssis](../includes/applies-to-version/sqlserver-ssis.md)]

This article describes the features that have been added or updated in [!INCLUDE [ssSQLv14_md](../includes/sssql17-md.md)] [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)].

> [!NOTE]  
> SQL Server 2017 also includes the features of SQL Server 2016 and the features added in SQL Server 2016 updates. For info about the new SSIS features in SQL Server 2016, see [What's New in Integration Services in SQL Server 2016](what-s-new-in-integration-services-in-sql-server-2016.md).

## Highlights of this release

Here are the most important new features of Integration Services in SQL Server 2017.

- **Scale Out**. Distribute SSIS package execution more easily across multiple worker computers, and manage executions and workers from a single master computer. For more info, see [Integration Services (SSIS) Scale Out](scale-out/integration-services-ssis-scale-out.md).

- **Integration Services on Linux**. Run SSIS packages on Linux computers. For more info, see [Extract, transform, and load data on Linux with SSIS](../linux/sql-server-linux-migrate-ssis.md).

- **Connectivity improvements**. Connect to the OData feeds of Microsoft Dynamics AX Online and Microsoft Dynamics CRM Online with the updated OData components.

## New in Azure Data Factory

With the public preview of Azure Data Factory version 2 in September 2017, you can now do the following things:

- Deploy packages to the SSIS Catalog database (SSISDB) on Azure SQL Database.
- Run packages deployed to Azure on the Azure-SSIS Integration Runtime, a component of Azure Data Factory version 2.

For more info, see [Lift and shift SQL Server Integration Services workloads to the cloud](lift-shift/ssis-azure-lift-shift-ssis-packages-overview.md).

These new capabilities require SQL Server Data Tools (SSDT) version 17.2 or later, but don't require SQL Server 2017 or SQL Server 2016. When you deploy packages to Azure, the Package Deployment Wizard always upgrades the packages to the latest package format.

## New in the Azure Feature Pack

In addition to the connectivity improvements in SQL Server, the Integration Services Feature Pack for Azure has added support for Azure Data Lake Store. For more info, see the blog post [New Azure Feature Pack Release Strengthening ADLS Connectivity](https://techcommunity.microsoft.com/category/sql-server/blog/ssis). Also see [Azure Feature Pack for Integration Services (SSIS)](azure-feature-pack-for-integration-services-ssis.md).

## New in SQL Server Data Tools (SSDT)

You can now develop SSIS projects and packages that target SQL Server versions 2012 through 2017 in Visual Studio 2017 or in Visual Studio 2015. For more info, see [Install SQL Server Data Tools (SSDT) for Visual Studio](../ssdt/download-sql-server-data-tools-ssdt.md).

## New in SSIS in SQL Server 2017

### Scale out for SSIS

[!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] [!INCLUDE [ssisnoversion-md](../includes/ssisnoversion-md.md)] (SSIS) Scale Out provides high-performance execution of SSIS packages by distributing package executions across multiple computers. After you set up Scale Out, you can run multiple package executions in parallel, in scale-out mode, from SQL Server Management Studio (SSMS).

For more information, see [Integration Services (SSIS) Scale Out](scale-out/integration-services-ssis-scale-out.md).

### Support for Microsoft Dynamics online resources

The OData Source and OData Connection Manager now support connecting to the OData feeds of Microsoft Dynamics AX Online and Microsoft Dynamics CRM Online.
