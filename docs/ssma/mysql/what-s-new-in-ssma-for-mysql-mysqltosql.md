---
title: "What's New in SSMA for MySQL (MySQLToSql) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "09/22/2018"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: 1451a0b0-6713-4d0c-954f-ea3d8fce1d31
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# What's New in SSMA for MySQL (MySQLToSql)
This article lists SSMA for MySQL changes in each release. 

## SSMA v7.10
The v7.10 release of SSMA for MySQL contains the following changes:
- Targeted fixes designed to provide additional security and privacy protections to meet changes in global requirements.
- A fix for conversion of spaces between function name and arguments list.

> [!IMPORTANT]
> With SSMA v7.4 and later versions, .Net 4.5.2 is an installation pre-requisite.

## SSMA v7.9
The v7.9 release of SSMA for MySQL contains the following changes:
- Targeted fixes that improve quality and conversion metrics.
- Partial support for migrating spatial data types from MySQL to Azure SQL Database.
- Support in SSMA command line to alter Data Type mapping and Project Preferences.
- Support for migrating data using SQL Server Integration Services (SSIS). After converting the schema, it is possible to create an SSIS package by using a right-click context menu option.
- The Azure SQL Database connection dialog in SSMA has also been altered to specify the fully qualified server name. In previous versions of SSMA, the Azure SQL Database prefix had to be explicitly mentioned inside projects settings.

> [!IMPORTANT]
> With SSMA v7.4 and later versions, .Net 4.5.2 is an installation pre-requisite.

## SSMA v7.8
The v7.8 release of SSMA for MySQL contains the following changes:
- Highlighted change type mapping in Project Settings.
- Provided the ability for users to disable telemetry.

> [!IMPORTANT]
> With SSMA v7.4 and later versions, .Net 4.5.2 is an installation pre-requisite.

## SSMA v7.7
The v7.7 release of SSMA for MySQL contains the following changes:
- SSMA for MySQL  has been enhanced with targeted fixes that improve quality and conversion metrics.
- Based on the popular demand, the 32-bit version of SSMA for MySQL is back. Compared to the previous implementation (prior to v7.4), there are two installer packages, but they cannot be installed side by side. As a result, you must choose the most appropriate version based on the connectivity components you have. It is always preferable to use the 64-bit version, if possible.
- SSMA for MySQL now has ODBC Connection String connection mode, which allows you to use any third-party ODBC drivers that are compatible with MySQL.

> [!IMPORTANT]
> With SSMA v7.4 and later versions, .Net 4.5.2 is an installation pre-requisite.

## SSMA v7.6
The v7.6 release of SSMA for MySQL has been enhanced with targeted fixes that improve quality and conversion metrics and with support for SQL Server 2017 (public preview). Support for SQL Server 2017 on Windows and Linux is in public preview and should not be used for production migrations.

> [!IMPORTANT]
> With SSMA v7.4 and later versions, .Net 4.5.2 is an installation pre-requisite, and the 32-bit version of the tool has been discontinued.

## SSMA v7.5
The v7.5 release of SSMA for MySQL has been enhanced with several improvements to ensure greater accessibility for people with disabilities.

> [!IMPORTANT]
> .Net 4.5.2 is a pre-requisite for installing SSMA v7.5. In addition, beginning with v7.4, the 32-bit version of SSMA is being discontinued.

## SSMA v7.4
The v7.4 release of SSMA for MySQL contains the following changes:
- The **Query timeout** option is now available during schema object discovery at source and target.
![query timeout option](../media/query-timeout_red.png)
- The quality and conversion metric has been improved with targeted fixes, based on customer feedback.

> [!IMPORTANT]
> .Net 4.5.2 is a pre-requisite for installing SSMA v7.4. In addition, beginning with v7.4, the 32-bit version of SSMA is being discontinued. 

## SSMA v7.3
The v7.3 release of SSMA for MySQL contains the following changes:
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
The v7.2 release of SSMA for MySQL contains the following changes:
- Improved quality and conversion metric with targeted fixes based on customer feedback.
- Telemetry enhancements to provide better data points to troubleshoot customer issues and improve SSMA's conversion rates.

## SSMA v7.1
The v7.1 release of SSMA for Access contains the following changes:
- SQL Server 2017 on Windows and Linux CTP1 is now a supported target platform for migration. This feature is in technical preview and allows schema and data movement to target SQL servers.
- SSMA now supports automatic updates to download the latest version of SSMA as soon as it is available.
- SSMA installable binaries are now delivered through Windows installer package files (.msi).

**Resources**

[Extending SQL Server Migration Assistant's conversion capabilities](https://blogs.msdn.microsoft.com/datamigration/2017/02/21/2185/)

[Assess and migrate data from non Microsoft data platforms to SQL Server *(with Oracle example)*](https://blogs.msdn.microsoft.com/datamigration/2016/11/16/sql-server-migration-assistant-how-to-assess-and-migrate-databases-from-non-microsoft-data-platforms-to-sql-server/) 

## May 2016  
The May 2016 release of SSMA for MySQL contains  the following changes:  .

-  Added support for SQL Server 2016.
-  Improved parser and resolver.
-  Removed installer check for .Net 2.0.
-  Updated Extension Pack dependency from .Net 3.5 to .Net 4.0.
-  Fixed default BigInt typemapping for MySql.
-  Fixed "save project" and "open project" commands for SSMA Console.
-  Fixed "securepassword" command for SSMA Console.
-  Fixed counting of objects for initial loading.
-  Fixed MsSql objects loading.
-  Fixed bug in global settings.
 
## March 2016  
The March 2016 preview release of SSMA for MySQL contains  the following changes:  
  
-  Added support for migration to SQL Server 2016. 
  
## January 2016  
The January 2016 maintenance release of SSMA for MySQL contains the following changes:  
  
-  Added View Log Menu Item to SSMA (RFC 5706203).  
-  Added Telemetry.  
  
## July 2014  
The July 2014 release of SSMA for MySQL contains the following changes:  
  
-  Improved Azure SQL DB code conversion. 
-  Extension pack functionality moved to schema to support Azure SQL DB.  
-  Performance improvements tested for databases with over 10k objects.  
-  UI improvements for dealing with large number of objects.  
-  Highlighting of "well known" LOB schemas (so they can be ignored in conversion).  
-  Conversion speed improvements.  
-  Show object counts in UI.  
-  Report size reduction by more than 25%.  
-  Improved error messages for unparsed constructs.  
  
## April 2014  
The July 2011 release of SSMA for MySQL contains the following changes:  
  
-  Added support of MS SQL Server 2014.  
-  Fixed bugs regarding conversion to Azure  
-  Fixed bugs regarding invisible report pages in IE 10.  
  
## July 2011  
The July 2011 release of SSMA for MySQL contains the following changes:  
  
-   Support for conversion of LIMIT to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] "Denali" OFFSET.  
-   Improved error reporting during data migration.  
  
## April 2011  
The April 2011 release of SSMA for MySQL contains the following changes:  
  
-   Single installable of "SSMA for MySQL", which supports [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2005, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2008, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] "Denali" and Azure SQL.  
-   The ability to connect [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] "Denali."  
-   Enhanced client-side data migration engine, supporting parallel migration of data.  
-   Improved data migration performance with Simple and Bulk logged recovery models.  
-   SSMA for MySQL Console version supports backward compatibility. You can open the projects created by versions earlier to SSMA v5.0.  
-   SSMA for MySQL v5.0 product can be installed side by side (SxS) with older versions of SSMA Product.  
  
## July 2010  
The July 2010 release of SSMA for MySQL contains the following features:  
  
1.  **Improvements to User Interface:**  
  
    -   'SQL Modes' Tab for MySQL Database objects  
    -   'Settings' Tab for MySQL Database objects  
    -   'Data' Tab for MySQL Tables  
    -   Updated Project Settings in Conversion and Migration Pages  
    -   'Data Migration Settings' at Table level  
  
2.  **Improvements to Connect to MySQL and SQL Server:**  
  
    -   SSL Connectivity in MySQL  
    -   Encrypted Connectivity in SQL Server  
  
3.  **Improvements to MySQL Metabase Explorer:**  
  
    -   Loading all the MySQL Database Objects and their respective Tabs.  
  
4.  **Improvements to Object Conversion:**  
  
    -   Conversion of MySQL Metabase objects - Procedures, Functions, Views, Triggers, and Statements.  
    -   Limited support for Spatial Data Types in Tables.  
    -   Option to convert MySQL functions to SQL Server Stored Procedures  
    -   Option to apply SQL Modes and Charset mapping during Object Conversion  
  
5.  **Improvements to Data Migration:**  
  
    -   Support for Data Migration using both Server-Side and Client-Side Data Migration Engines  
    -   Support for Spatial Data Migration  
    -   Custom SQL for Data Migration for Tables  
  
6.  **SSMA for MySQL Console:**  
  
    -   Support Console Feature for SSMA for MySQL  
    -   Support for Script-Level Interfacing  
  
## January 2010  
The January 2010 release of SSMA for MySQL was the initial release. It contained the following features:  
  
-   Added support for migration to both on-premises SQL Server and Azure SQL.  
  
-   **Feature Snapshot:** Schema and Data migration of MySQL Tables/Indexes/Constraints.
