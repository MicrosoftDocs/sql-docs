---
title: "What's new in Data Migration Assistant (SQL Server)"
description: Learn about the new features in each release of Data Migration Assistant for SQL Server and Azure SQL Database.
author: aciortea
ms.author: aciortea
ms.reviewer: randolphwest
ms.date: 07/25/2022
ms.service: sql
ms.subservice: dma
ms.topic: conceptual
ms.custom: intro-whats-new
helpviewer_keywords:
  - "Data Migration Assistant, new features"
keywords: ""
---

# What's new in Data Migration Assistant

This article lists the additions in each release of Data Migration Assistant.

## Data Migration Assistant v5.6

The v5.6 release of the Data Migration Assistant provides support for:

- Added new premium-series and premium-series memory optimized Azure SQL Managed Instance preview SKUs to SKU recommendation feature.
- Added new E v5 and Eb v5 (preview) SQL Server on Azure Virtual Machine SKUs to SKU recommendation feature.
- Added feature flag to SKU recommendation console app to toggle whether or not new preview SKUs are considered.
- Improved the SKU recommendation logic for SQL Server on Azure Virtual Machine to better align with best practices: https://aka.ms/sqliaasperf

In addition, this release of Data Migration Assistant provides bug fixes and improvements for the following issues:

- Fixed CPU utilization calculation causing incorrect SKU recommendation results.
- Fixed case-sensitive collation issue causing data collection to fail during SKU recommendation.
- Fixed accessibility issues.
- Fixed Azure SQL Database migration row count mismatch.
- Fixed T-SQL parser bug.
- Changed some blocking issues to warnings.

## Data Migration Assistant v5.5

The v5.5 release of the Data Migration Assistant provides support for:

- Elastic SKU recommendations option that generates an unique price-to-performance curve based on heuristic analysis of the collected performance data and workload pattern comparison with workloads in Azure SQL.
- Improved user experience with the new HTML report for SKU recommendation results, in addition to the existing JSON file output.
- Connection string wizard to enable users to intuitively provide SQL connection information for single SQL instances.
- Enabling user selection of databases to include/exclude in the SKU recommendations.

In addition, this release of Data Migration Assistant provides bug fixes and improvements for the following issues:

- Fixed an error preventing collected performance data being read correctly in certain locales.
- Fixed an issue related to incorrect SKU recommendations for environments with highly variable workload patterns.
- Fixed an issue affecting data collection in high memory environments.

## Data Migration Assistant v5.4

The v5.4 release of the Data Migration Assistant provides support for:
- New console experience that provides recommendations as well as explanations for target Azure SQL Database, Azure SQL Managed Instance and SQL Server on Azure VM SKUs based on performance data points.
- SQL Server 2005 has been deprecated and will be removed as a possible source in future releases.

In addition, this release of Data Migration Assistant provides bug fixes for the following issues:
- Fixed an RDS for SQL Server permissions error raised when assessing Azure SQL Managed Instance targets.
- Fixed incorrectly reported blockers for Memory Optimized Data Filegroup (FX) in assessments.
- Fixed missing rule to detect three-part or four-part cross-database references when assessing Azure SQL Database targets.
- Fixed `specified value for 'AssessmentDatabases' is invalid` error when using AssessTargetReadiness option in DMA command line.

## Data Migration Assistant v5.3

The v5.3 release of the Data Migration Assistant provides support for:
- External application ad hoc query assessment in command line. 
- Users to customize command timeout parameter in Dma.exe.config configuration file.
- Assessment configuration sample file and PowerShell sample script, which explain how to use configuration file to run DMA command line.
- Backup and restore operations in on-premises migration without the timeout limitation.

In addition, this release of Data Migration Assistant has been updated to .NET 4.8 and provides bug fixes for the following issues:
- Cannot run assessment or connect to Azure SQL Database without sysadmin permission in migration phase.
- Users cannot run assessment without sysadmin permission for RDS assessments.
- Users encountered issues uploading JSON assessment reports to Azure Migrate.
- Cannot migrate the objects containing Chinese characters.

## Data Migration Assistant v5.2

The v5.2 release of the Data Migration Assistant provides support for:
- Uploading assessments to Azure Migrate with support for Azure Government and national clouds (sovereign offering).  This feature enables to assess the readiness of SQL Server data estate migrating to Azure SQL.
- Command line support for uploading assessments to Azure Migrate with support for Azure Government and national clouds.  Now, you can completely automate uploading the assessments to Azure migrate project to get a consolidated Azure SQL readiness report. 

## Data Migration Assistant v5.0

The v5.0 release of the Data Migration Assistant provides support for:

- SQL Server 2019 for Windows and SQL Server 2019 for Linux as targets for assessment and upgrade.
- Saving and loading assessments, including support for saving and loading assessments created in earlier versions of the Data Migration Assistant.
- Assessing SQL Server Integration Services (SSIS) projects hosted in SSISDB and SSIS packages hosted in package store. Database Migration Assistant detects unsupported, partially supported or deprecated features and compatibility issues that are used in source packages and provides recommendations to help you address those issues.
- Assessing SQL queries from external application, e.g. SQL queries in C# source code. Users can use the Data Access Migration Toolkit to generate a full JSON report for the SQL queries used in C# source code and then upload the report to Data Migration Assistant.

In addition, this release of Data Migration Assistant provides additional enhancements and bug fixes, and the tool has been updated to .NET 4.7.2.

## Data Migration Assistant v4.5

The v4.5 release of Data Migration Assistant provides support for assessment of migrating SQL Server Integration Services (SSIS) packages hosted in File system to Azure SQL Database or SQL Managed Instance.

## Data Migration Assistant v4.4

The v4.4 release of Data Migration Assistant provides support for uploading assessments to Azure Migrate.

## Data Migration Assistant v4.3

The v4.3 release of Data Migration Assistant provides support for:

- SKU Recommendations for Azure SQL Managed Instance based on workload assessment.
- RDS SQL Server as a source for assessments.
- Agent job assessments for Azure SQL Managed Instance as a target.
- The ability to ignore certain assessment rules; the list of error codes specified in the 'ignoreErrorCodes' property configured in DMA won't show up in DMA assessment results.
- Assessment of T-SQL queries in job activity steps and providing appropriate recommendations
- Extended events assessments (Public Preview).

In addition, this release of DMA provides improved performance for handling a large number of schema objects in databases, as well as bug fixes related to:

- Procedures compiled with native compilation, in some cases.
- Complicated database schemas.

## Data Migration Assistant v4.2

The v4.2 release of Data Migration Assistant provides command-line support for target readiness assessment for one or more server instances when migrating from on-premises SQL Server to a SQL Managed Instance. Customers can now use the Data Migration Assistant command line to collect metadata about their database schema, detect the blockers, and learn about partially supported or unsupported features that affect migration to a SQL Managed Instance. The results can then be rendered using the Power BI template provided.

## Data Migration Assistant v4.1

The v4.1 release of Data Migration Assistant introduces support for comprehensive assessment of on-premises SQL Server databases migrating to SQL Managed Instance.

The assessment workflow helps you detect the following issues, which can affect your migration to SQL Managed Instance:

- **Unsupported or partially supported features**. Data Migration Assistant assesses your source SQL Server database for features in use that are partially supported or unsupported on the target SQL Managed Instance. The tool then provides a comprehensive set of recommendations, alternative approaches available in Azure, and mitigating steps so that customers can take this information into account when planning their migration projects.

- **Compatibility issues**. Data Migration Assistant also identifies compatibility issues related to the following areas:

  - Breaking changes: The specific schema objects that may break the functionality migrating to the target database.  We recommend fixing these schema objects after the database migration.
  - Behavioral changes: The schema objects reported may continue to work, but they may exhibit a different behavior, for example performance degradation.
  - Informational issues: These objects won't impact the migration but may have been deprecated from feature SQL Server releases.

After the assessment is complete, use our [Azure Database Migration Service](https://azure.microsoft.com/services/database-migration/) (DMS) to perform the migration of your SQL Server databases to SQL Managed Instance.  DMS supports both [offline](/azure/dms/tutorial-sql-server-to-managed-instance) (one-time) and [online](/azure/dms/tutorial-sql-server-managed-instance-online) (minimal-downtime) database migrations to SQL Managed Instance.

## Data Migration Assistant v4.0

The v4.0 release of Data Migration Assistant introduces the Azure SQL Database SKU recommendations feature, which allows users to identify the minimum recommended Azure SQL Database SKU based on performance counters collected from the computer(s) hosting your databases. This feature provides recommendations related to pricing tier, compute level, and max data size, as well as estimated cost per month. It also offers the ability to provision all your databases to Azure in bulk.

> [!NOTE]
> This functionality is currently be available only via the Command Line Interface (CLI).

For additional detail, see the article [Identify the right Azure SQL Database SKU for your on-premises database](dma-sku-recommend-sql-db.md).

## Data Migration Assistant v3.6

The v3.6 release of Data Migration Assistant introduces "Auto fix" for the schema objects that are impacted by the most common migration blockers.

This release provides autofix for the following migration blocker and behavior change issues:

- The schema objects that use Unqualified Join syntax.
- The schema objects that use the legacy RAISEERROR statement.
- SQL statements that use Order By Integer Literal.

Data Migration Assistant performs automatic schema conversion for the objects impacted by the listed issues and prompts the user for confirmation before proceeding with the schema conversion. Users can review the suggested code changes and then either accept or reject all conversions for any given database object.

Data Migration Assistant uses Microsoft Program Synthesis (PROSE) technology to suggest the code fixes. Learn more about [PROSE](https://microsoft.github.io/prose/).

## Data Migration Assistant v3.5

The v3.5 release of Data Migration Assistant includes the following additions:

- Significant performance improvements for migrating to Azure SQL Database (benchmark tests indicate the process is four times faster than with prior versions of Data Migration Assistant).
- The memory footprint is further optimized to improve the stability of the migration workflow.
- The ability to skip assessments during the schema and data migrations (if you've already performed the assessment and addressed any breaking schema objects prior to migration).
- A fix to address an issue with the tool crashing when an invalid network share path is provided for backup files, when performing an upgrade of a legacy version of SQL Server on-premises to a later version or to SQL Server on Azure VMs.

## Data Migration Assistant v3.4

The v3.4 release of Data Migration Assistant includes the following additions:

- Support for SQL Server 2017 as a source for migrations to Azure SQL Database.
- Enhancements to stability, performance, and assessment rule correctness.

## Data Migration Assistant v3.3

The v3.3 release of Data Migration Assistant enables migration of an on-premises SQL Server instance to the new version of SQL Server 2017, on both Windows and Linux. While the overall migration workflow for Windows and Linux is the same, the move to SQL Server 2017 for Linux requires a couple of additional considerations.

### Specifying the back-up path

Linux and Windows use different path formats. As a result, migrating to SQL Server 2017 on Linux requires that the user provide both the Windows and Linux versions of the path to the location of the physical file. You can provide both versions of the path in different ways depending on the location of the physical file.
If the physical back-up file is on a computer running:

- Linux, use a 'samba' share to share the file with other computers on the network.
- Windows, use the 'mnt' command to mount the share onto the computer running Linux.

> [!NOTE]
> Details of using a 'samba' share or the 'mnt' command are beyond the scope of this article.

### Migrating Windows logins

While the migration of Active Directory (AD) logins is officially supported by SQL Server 2017 on Linux, it requires additional configuration to work successfully. Refer to the article [Active Directory Authentication with SQL Server on Linux](../linux/sql-server-linux-active-directory-authentication.md) for detailed information about setting up Active Directory logins on SQL Server 2017 on Linux. After performing the required configuration, the setup is complete and you can migrate Active Directory logins as usual. Standard SQL Authentication works as expected without any additional setup.

## Data Migration Assistant v3.2

The v3.2 release of Data Migration Assistant includes the following additions:

- Schema and data migration are enabled from on-premises SQL Server databases to Azure SQL Database with a new migration workflow.
- During schema migration to Azure SQL Database, DMA scripts your source database objects, provides guidance on how to fix any potential compatibility issues, and then deploys your schema to Azure.

## Data Migration Assistant v3.1

The v3.1 release of Data Migration Assistant includes the following additions:

- Improved assessment recommendations for Azure SQL Database in terms of database collations, use of unsupported system stored procedures, and CLR objects.
- Assessment guidance for compatibility levels 130, 120, 110, and 100 when migrating to Azure SQL Database.

## Data Migration Assistant v3.0

The v3.0 release of Data Migration Assistant extends the Azure SQL Database assessment to provide comprehensive recommendations to help fix issues related to:

- Migration blocking issues.
- Partially or unsupported features and functions.

## Data Migration Assistant v2.1

The v2.1 release of Data Migration Assistant includes the following additions:

- Command-line support for running assessments in an unattended mode, which helps to run assessments at scale. For additional detail, refer to the article [Run Data Migration Assistant from the command line](dma-commandline.md).
- Performance improvements when users launch and close DMA.
- The ability to configure SQL connection time-out. For additional detail, refer to the article [Configuration settings for Data Migration Assistant](dma-configurationsettings.md).

## Data Migration Assistant v2.0

The v2.0 release of Data Migration Assistant includes improved Stretch database feature recommendations to provide proper prioritized tables that maximize the storage savings.

> [!IMPORTANT]  
> Stretch Database is deprecated in [!INCLUDE [sssql22-md](../includes/sssql22-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid-md](../includes/ssnotedepfutureavoid-md.md)]

## Data Migration Assistant v1.0

The v1.0 release of Data Migration Assistant is the initial release, and it provides for:

- Discovery of issues that can affect an upgrade to an on-premises version of SQL Server. Any findings are described as compatibility issues, and they're categorized into the following areas:
  - Breaking changes
  - Behavior changes
  - Deprecated features
- Discovery of new features in the target SQL Server platform that the database can benefit from after an upgrade. Any findings are described as feature recommendations, and they're categorized into the following areas:
  - Performance
  - Security
  - Storage
- Modern user experience to perform assessments.

## See also

[Overview of Data Migration Assistant](dma-overview.md)
