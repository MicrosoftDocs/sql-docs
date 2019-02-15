---
title: "What's new in Data Migration Assistant (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "10/20/2018"
ms.prod: sql
ms.prod_service: "dma"
ms.reviewer: ""
ms.technology: dma
ms.topic: conceptual
keywords: ""
helpviewer_keywords: 
  - "Data Migration Assistant, new features"
ms.assetid: ""
author: pochiraju
ms.author: rajpo
manager: craigg
---

# What's new in Data Migration Assistant
This article lists the additions in each release of Data Migration Assistant (DMA).

## DMA v4.1
The v4.1 release of DMA introduces support for comprehensive assessment of on-premises SQL Server databases migrating to Azure SQL Database Managed Instance.

The assessment workflow helps you detect the following issues, which can affect your migration to Azure SQL Database Managed Instance:

- **Unsupported or partially supported features**. DMA assesses your source SQL Server database for features in use that are partially supported or unsupported on the target Azure SQL Database Managed Instance. The tool then provides a comprehensive set of recommendations, alternative approaches available in Azure, and mitigating steps so that customers can take this information into account when planning their migration projects.

- **Compatibility issues**. DMA also identifies compatibility issues related to the following areas:

    - Breaking changes:  The specific schema objects that may break the functionality migrating to the target database.  We recommend fixing these schema objects after the database migration.
    - Behavioral changes: The schema objects reported may continue to work, but they may exhibit a different behavior, for example performance degradation.
    - Informational issues:  These objects will not impact the migration but may have been deprecated from feature SQL Server releases.

After the assessment is complete, use our [Azure Database Migration Service](https://azure.microsoft.com/services/database-migration/) (DMS) to perform the migration of your SQL Server databases to Azure SQL Database Managed Instance.  DMS supports both [offline](https://docs.microsoft.com/azure/dms/tutorial-sql-server-to-managed-instance) (one-time) and [online](https://docs.microsoft.com/azure/dms/tutorial-sql-server-managed-instance-online) (minimal-downtime) database migrations to Azure SQL Database Managed Instance.

## DMA v4.0
The v4.0 release of DMA introduces the Azure SQL Database SKU recommendations feature, which allows users to identify the minimum recommended Azure SQL Database SKU based on performance counters collected from the computer(s) hosting your databases. This feature provides recommendations related to pricing tier, compute level, and max data size, as well as estimated cost per month. It also offers the ability to provision all your databases to Azure in bulk.

> [!NOTE]
> This functionality is currently be available only via the Command Line Interface (CLI). Support for this feature via the DMA user interface is planned for delivery later this year.

For additional detail, see the article [Identify the right Azure SQL Database SKU for your on-premises database](dma-sku-recommend-sql-db.md).

## DMA v3.6
The v3.6 release of DMA introduces "Auto fix" for the schema objects that are impacted by the most common migration blockers.

This release provides autofix for the following migration blocker and behavior change issues:
- The schema objects that use Unqualified Join syntax.
- The schema objects that use the legacy RAISEERROR statement.
- SQL statements that use Order By Integer Literal.

DMA performs automatic schema conversion for the objects impacted by the listed issues and prompts the user for confirmation before proceeding with the schema conversion. Users can review the suggested code changes and then either accept or reject all conversions for any given database object.

DMA uses Microsoft Program Synthesis (PROSE) technology to suggest the code fixes. Learn more about [PROSE](https://microsoft.github.io/prose/).

## DMA v3.5
The v3.5 release of DMA includes the following additions:
- Significant performance improvements for migrating to Azure SQL Database (benchmark tests indicate the process is four times faster than with prior versions of DMA).
- The memory footprint is further optimized to improve the stability of the migration workflow.
- The ability to skip assessments during the schema and data migrations (if you've already performed the assessment and addressed any breaking schema objects prior to migration).
- A fix to address an issue with the tool crashing when an invalid network share path is provided for backup files, when performing an upgrade of a legacy version of SQL Server on-premises to a later version or to SQL Server on Azure VMs.

## DMA v3.4
The v3.4 release of DMA includes the following additions:
- Support for SQL Server 2017 as a source for migrations to Azure SQL Database.
- Enhancements to stability, performance, and assessment rule correctness.

## DMA v3.3
The v3.3 release of DMA enables migration of an on-premises SQL Server instance to the new version of SQL Server 2017, on both Windows and Linux. While the overall migration workflow for Windows and Linux is the same, the move to SQL Server 2017 for Linux requires a couple of additional considerations.

### Specifying the back-up path
Linux and Windows use different path formats. As a result, migrating to SQL Server 2017 on Linux requires that the user provide both the Windows and Linux versions of the path to the location of the physical file. You can provide both versions of the path in different ways depending on the location of the physical file.
If the physical back-up file is on a computer running:
- Linux, use a 'samba' share to share the file with other computers on the network.
- Windows, use the 'mnt' command to mount the share onto the computer running Linux.

> [!NOTE]
> Details of using a 'samba' share or the 'mnt' command are beyond the scope of this article.

### Migrating Windows logins
While the migration of Active Directory (AD) logins is officially supported by SQL Server 2017 on Linux, it requires additional configuration to work successfully. Refer to the article [Active Directory Authentication with SQL Server on Linux](https://docs.microsoft.com/sql/linux/sql-server-linux-active-directory-authentication) for detailed information about setting up Active Directory logins on SQL Server 2017 on Linux. After performing the required configuration, the setup is complete and you can migrate Active Directory logins as usual. Standard SQL Authentication works as expected without any additional setup.

## DMA v3.2
The v3.2 release of DMA includes the following additions:

- Schema and data migration are enabled from on-premises SQL Server databases to Azure SQL Database with a new migration workflow.
- During schema migration to Azure SQL Database, DMA scripts your source database objects, provides guidance on how to fix any potential compatibility issues, and then deploys your schema to Azure.

## DMA v3.1
The v3.1 release of DMA includes the following additions:

- Improved assessment recommendations for Azure SQL Databases in terms of database collations, use of unsupported system stored procedures, and CLR objects.
- Assessment guidance for compatibility levels 130, 120, 110, and 100 when migrating to Azure SQL Databases.

## DMA v3.0
The v3.0 release of DMA extends the Azure SQL database assessment to provide comprehensive recommendations to help fix issues related to:

- Migration blocking issues.
- Partially or unsupported features and functions.

## DMA v2.1
The v2.1 release of DMA includes the following additions:
- Command-line support for running assessments in an unattended mode, which helps to run assessments at scale. For additional detail, refer to the article [Run Data Migration Assistant from the command line](dma-commandline.md).
- Performance improvements when users launch and close DMA.
- The ability to configure SQL connection time-out. For additional detail, refer to the article [Configuration settings for Data Migration Assistant](dma-configurationsettings.md).

## DMA v2.0
The v2.0 release of DMA includes improved Stretch database feature recommendations to provide proper prioritized tables that maximize the storage savings.

## DMA v1.0
The v1.0 release of DMA is the initial release, and it provides for:
- Discovery of issues that can affect an upgrade to an on-premises version of SQL Server. Any findings are described as compatibility issues, and they're categorized into the following areas:
    - Breaking changes
    - Behavior changes
    - Deprecated features
- Discovery of new features in the target SQL Server platform that the database can benefit from an upgrade. Any findings are described as feature recommendations, and they're categorized into the following areas:
    - Performance
    - Security
    - Storage
-	Modern user experience to perform assessments.

## See also
[Overview of Data Migration Assistant](../dma/dma-overview.md)
