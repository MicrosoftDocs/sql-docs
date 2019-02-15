---
title: "What's New in SSMA for SAP ASE (SybaseToSQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/22/2018"
ms.prod: sql
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: 2be0cf8d-6dbe-443a-abbd-036249922205
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# What's New in SSMA for SAP ASE (SybaseToSQL)
This article lists SSMA for SAP ASE (formerly SSMA for Sybase) changes in each release. 

## SSMA v7.10
The v7.10 release of SSMA for SAP ASE has been enhanced with targeted fixes designed to provide additional security and privacy protections to meet changes in global requirements.

> [!IMPORTANT]
> With SSMA v7.4 and later versions, .Net 4.5.2 is an installation pre-requisite.

## SSMA v7.9
The v7.9 release of SSMA for SAP ASE contains the following changes:
- Targeted fixes that improve quality and conversion metrics.
- Support in SSMA command line to alter Data Type mapping and Project Preferences.
- Support for migrating data using SQL Server Integration Services (SSIS). After converting the schema, it's possible to create an SSIS package by using a right-click context menu option.
- The Azure SQL Database connection dialog in SSMA has also been altered to specify the fully qualified server name. In previous versions of SSMA, the Azure SQL Database prefix had to be explicitly mentioned inside projects settings.

> [!IMPORTANT]
> With SSMA v7.4 and later versions, .Net 4.5.2 is an installation pre-requisite.

## SSMA v7.8
The v7.8 release of SSMA for SAP ASE contains the following changes:
- Highlighted change type mapping in Project Settings.
- Provided the ability for users to disable telemetry.

> [!IMPORTANT]
> With SSMA v7.4 and later versions, .Net 4.5.2 is an installation pre-requisite.

## SSMA v7.7
The v7.7 release of SSMA for SAP ASE contains the following changes:
- SSMA for SAP ASE has been enhanced with targeted fixes that improve quality and conversion metrics.
- Based on the popular demand, the 32-bit version of SSMA for SAP ASE is back. Compared to the previous implementation (prior to v7.4), there are two installer packages, but they can't be installed side by side. As a result, you must choose the most appropriate version based on the connectivity components you have. It's always preferable to use the 64-bit version, if possible.

> [!IMPORTANT]
> With SSMA v7.4 and later versions, .Net 4.5.2 is an installation pre-requisite.

## SSMA v7.6
The v7.6 release of SSMA for SAP ASE contains the following changes:
- SSMA for SAP ASE has been enhanced with targeted fixes that improve quality and conversion metrics and with support for SQL Server 2017 (public preview). Support for SQL Server 2017 on Windows and Linux is in public preview and shouldn't be used for production migrations.
- SSMA for SAP ASE has been updated to provide support for conversion of Sybase functions.

> [!IMPORTANT]
> With SSMA v7.4 and later versions, .Net 4.5.2 is an installation pre-requisite, and the 32-bit version of the tool has been discontinued.

## SSMA v7.5
The v7.5 release of SSMA for SAP ASE contains the following changes:
-	Enhanced with several improvements to ensure greater accessibility for people with disabilities.
-	Updated to provide support for CREATE OR REPLACE syntax.

> [!IMPORTANT]
> .Net 4.5.2 is a pre-requisite for installing SSMA v7.5. In addition, beginning with v7.4, the 32-bit version of SSMA is being discontinued.  

## SSMA v7.4
The v7.4 release of SSMA for Sybase contains the following changes:
- The **Query timeout** option is now available during schema object discovery at source and target.
![query timeout option](../media/query-timeout_red.png)
- The quality and conversion metric has been improved with targeted fixes, based on customer feedback.

> [!IMPORTANT]
> .Net 4.5.2 is a pre-requisite for installing SSMA v7.4. In addition, beginning with v7.4, the 32-bit version of SSMA is being discontinued.  

## SSMA v7.3
The v7.3 release of SSMA for Sybase contains the following changes:
- Improved quality and conversion metric with targeted fixes based on customer feedback.
- SSMA extensibility framework exposed via the following items:
  - Export functionality to a SQL Server Data Tools (SSDT) project.
    -   You can now export schema scripts from SSMA to an SSDT project. You can use the schema scripts to make additional schema changes and deploy your database.
![Save as SSDT project command](../media/export-schema-scripts_red.png)
  - Libraries that can be consumed by SSMA for performing custom conversions.
    - You can now construct code that can handle custom syntax conversions and conversions that weren't previously handled by SSMA.
      - Instructions on how to construct a custom converter are available in this blog post, [Extending SQL Server Migration Assistant's conversion capabilities](https://blogs.msdn.microsoft.com/datamigration/2017/02/21/2185/).
      - Download a sample project for conversion from this [blog post](https://blogs.msdn.microsoft.com/datamigration/ssmafororacleconversionsample/).

## SSMA v7.2
The v7.2 release of SSMA for Sybase contains the following changes:
- Improved quality and conversion metric with targeted fixes based on customer feedback.
- Telemetry enhancements to provide better data points to troubleshoot customer issues and improve SSMA's conversion rates.

## SSMA v7.1
The v7.1 release of SSMA for Access contains the following changes:
- SQL Server 2017 on Windows and Linux CTP1 is now a supported target platform for migration. This feature is in technical preview and supports schema and data movement to target SQL servers.
- SSMA now supports automatic updates to download the latest version of SSMA as soon as it's available.
- SSMA installable binaries are now delivered through Windows installer package files (.msi).

**Resources**

[Extending SQL Server Migration Assistant's conversion capabilities](https://blogs.msdn.microsoft.com/datamigration/2017/02/21/2185/)

[Assess and migrate data from non Microsoft data platforms to SQL Server *(with Oracle example)*](https://blogs.msdn.microsoft.com/datamigration/2016/11/16/sql-server-migration-assistant-how-to-assess-and-migrate-databases-from-non-microsoft-data-platforms-to-sql-server/) 

## May 2016  
The May  2016 release of SSMA for Sybase contains  the following changes:  

-  Added support for SQL Server 2016.
-  Removed installer check for .Net 2.0.
-  Updated Extension Pack dependency from .Net 3.5 to .Net 4.0.
-  Fixed "save project" and "open project" commands for SSMA Console.
-  Fixed "securepassword" command for SSMA Console.
-  Fixed counting of objects for initial loading.
-  Fixed bug in global settings.

## March 2016  
The March 2016 preview release of SSMA for Sybase contains  the following changes:  
  
-  Added support for migration to SQL Server 2016.  
  
## January 2016  
The January 2016 maintenance release of SSMA for Sybase contains the following changes:  
  
-  Added View Log Menu Item to SSMA (RFC 5706203).  
-  Added Telemetry. 
  
## July 2014  
The July 2014 release of SSMA for Sybase contains the following changes:  
  
-  Improved Azure SQL DB code conversion.  
-  Moved extension pack functionality to schema to support Azure SQL DB.  
-  Added performance improvements tested for databases with over 10k objects.  
-  Added UI improvements for dealing with large number of objects.  
-  Added the ability to highlight "well known" LOB schemas (so they can be ignored in conversion).  
-  Added conversion speed improvements.  
-  Added the ability to show object counts in UI. 
-  Reduced report size by more than 25%.  
-  Improved error messages for unparsed constructs.  
  
## April 2014  
The April 2014 release of SSMA for Sybase contains the following changes:  
  
-   Added support of MS SQL Server 2014.  
-   Fixed bugs regarding conversion to Azure.  
-   Fixed bugs regarding invisible report pages in IE 10.  
  
## January 2012  
The January 2012 release of SSMA for Sybase contains the following changes:  
  
-   Added support for rollback trigger conversion.  
-   Provided fix for converting @@ROWCOUNT and @@ERROR in the same SET statement.  
  
## July 2011  
The July 2011 release of SSMA for Sybase contains the following changes:  
  
-   Improved error reporting during data migration.  
  
## April 2011  
The April 2011 release of SSMA for Sybase contains the following changes:  
  
-   Consolidated "SSMA for Sybase" product, which supports [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2005, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2008, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] "Denali" and Azure SQL.  
-   Added support for connecting and migrating to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] "Denali."  
-   Added a new feature to convert and migrate Sybase databases to Azure SQL.  
-   Enhanced client-side data migration engine, supporting parallel migration of data.  
-   Improved data migration performance with Simple and Bulk logged recovery models.  
-   Added the ability to properly convert and migrate case-sensitive Sybase databases to case-sensitive SQL Server.  
-   Added support for conversion of Sybase ASE Non-ANSI join statements to SQL Server ANSI join statements has been extended to DELETE and UPDATE statements.  
-   Provided additional connectivity options for connecting to Sybase ASE servers using Sybase ASE ODBC provider and Sybase ASE ADO.Net providers.  
-   Removed the dependency on a separate database called **SysDB**, which contains the Sybase emulation functions (installed as part of Extension Pack).  
-   Added the ability to install SSMA for Sybase Extension Pack on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] clusters.  
-   Added backward compatibility of projects created by earlier versions of SSMA (v4.0 and v4.2).  
-   Added the ability to install the SSMA for Sybase v5.0 product side-by-side (SxS) with older versions of SSMA (v4.0 and v4.2).  
  
## July 2010  
The July 2010 release of SSMA for Sybase contains the following changes:  
  
-   Added support for migrating to SQL Server 2008 R2.  
-   Added a new SSMA Console application for command-line execution.  
-   Added support for Data Migration using both Server-Side and Client-Side Data Migration Engines.  
-   Added support for "Custom SELECT" statement in data migration.  
-   Added support for migrating from Sybase ASE 15.0.3 and 15.5.  
  
## June 2008  
The June 2008 release of SSMA for Sybase contains the following changes:  
  
-   Added SSMA Tester, which automatically tests the database object conversion and the data migration made by SSMA. After all SSMA migration steps are finished, use SSMA Tester to verify that converted objects work the same way and that all data was transferred properly.  
-   Added Pre-SQL conversion. User now can specify temporary table (and other object) declarations for each source procedure to be used in conversion.  
-   Added improvements in object conversion:  
    -   Joins conversion revised.  
    -   Aggregates and non-aggregates without having/group by clauses.  
    -   The IDENTITY function with a SELECT INTO statement.  
    -   Clustered constraints and indexes on data-only-locked.  
    -   Temporary tables created by SELECT INTO.  
    -   Constraints / Indexes for temporary tables.  
    -   New [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2008 datetime types are supported.  
    -   Sybase 15.0 connectivity and datatypes support.  
  
## May 2007  
The May 2007 release of SSMA for Sybase contains the following changes:  
  
-   Added the ability to load database content faster when saving a project.  
-   Added support for user-entered comments in the SQL Server formatted SQL mode.  
-   Added improvements in object conversion.  
  
The Help file wasn't updated for this release. For more information, see the Documentation Notes section later in this article.  
  
## November 2006  
The November 2006 release of SSMA for Sybase contains the following changes:  
  
-   Added new global settings:  
    -   You can opt to show line numbers in editor windows.  
    -   You can configure SSMA to prompt to replace duplicate objects, or always or never replace duplicate objects during schema conversion.  
-   Added a new conversion option that lets you configure how SSMA handles the following situations:  
    -   A CAST or CONVERT statement that contains a binary string.  
    -   Checks for null values in equality expressions.  
    -   Proxy tables.  
    -   User message error numbers for RAISERROR.  
    -   UPDATE statements that contain unresolved identifiers.  
-   Added a new migration option that lets you specify how SSMA should handle dates that are outside the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] date range.  
-   Added a **Formatted SQL** setting on the **SQL** tab, which formats the code for improved readability.  
-   Bug fixes, including:  
    -   SSMA now converts LOCK TABLE *table* IN { SHARED | EXCLUSIVE } MODE statements by adding a TABLOCK or TABLOCKX hint to the subsequent SELECT query on the table.  
    -   The necessary casts are now added when binary types are used in character expressions.  
    -   Memory and performance improvements.  
  
## July 2006  
The July 2006 release of SSMA for Sybase was the initial release.  
  
## See Also  
[Getting Started with SSMA for Sybase &#40;SybaseToSQL&#41;](../../ssma/sybase/getting-started-with-ssma-for-sybase-sybasetosql.md)
