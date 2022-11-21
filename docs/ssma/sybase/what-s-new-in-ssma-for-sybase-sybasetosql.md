---
title: "What's New in SSMA for SAP ASE (SybaseToSQL) | Microsoft Docs"
description: Find out about changes to SQL Server Migration Assistant (SSMA) for Sybase (SybaseToSQL) for each release.
author: cpichuka
ms.service: sql
ms.custom:
  - intro-whats-new
ms.date: "04/29/2021"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: 2be0cf8d-6dbe-443a-abbd-036249922205
ms.author: cpichuka
---
# What's New in SSMA for SAP ASE (SybaseToSQL)

This article lists SQL Server Migration Assistant (SSMA) for SAP ASE (formerly SSMA for Sybase) changes in each release.


## SSMA v9.2
The v9.2 release of SSMA for SAP ASE contains the following changes:

 * Enhanced data movement pipelines at scale monintoring 


## SSMA v9.1
The v9.1 release of SSMA for SAP ASE contains the following changes:

 * Minor performance improvements, bug fixes and enhanced get help experience  

## SSMA v9.0

The v9.0 release of SSMA for SAP ASE contains the following changes:

* New offline at scale data migration support
* Enhanced data migration architecture
* Online help viewer support

## SSMA v8.24

The v8.24 release of SSMA for SAP ASE contains the following changes:

* Minor performance improvements and bug fixes

## SSMA v8.23

The v8.23 release of SSMA for SAP ASE contains the following changes:

* Improved messaging for conversion of TRANSACTION_RPC statements
* New option "Allow Statements from Files", for adding statements from file system
* Enhanced XML output when running in console mode
* New "Feedback" link in reports to send feedback
* In reports, enhanced visualization experience

## SSMA v8.22

The v8.22 release of SSMA for SAP ASE contains the following changes:

* Allow to specify additional connection string options for target database connection
* Introduce summary charts in assessment reports
* Improve messages navigation in assessment reports

## SSMA v8.21

The v8.21 release of SSMA for SAP ASE contains the following changes:

* Use `COUNT_BIG` in row count queries for the target database

## SSMA v8.20

The v8.20 release of SSMA for SAP ASE contains the following changes:

* Minor performance improvements and bug fixes

## SSMA v8.19

The v8.19 release of SSMA for SAP ASE contains the following changes:

* Store and run test cases on the client-side

## SSMA v8.18

The v8.18 release of SSMA for SAP ASE contains the following changes:

* Minor performance improvements and bug fixes

## SSMA v8.17

The v8.17 release of SSMA for SAP ASE contains the following changes:

* Add support for computed columns
* Update HTML assessment reports to use modern editor to display SQL text

## SSMA v8.16

The v8.16 release of SSMA for SAP ASE contains the following changes:

* Remove support for legacy parser
* Fix issue with objects not refreshing from database

## SSMA v8.15

In addition to several accessibility improvements, the v8.15 release of SSMA for SAP ASE contains the following changes:

* Revamp assessment reports to work in modern browsers
* Use authority provided by the database for Azure AD authentication
* Improve naming for statements loaded from files

## SSMA v8.14

In addition to several improvements to ensure greater accessibility for people with disabilities, the v8.14 release of SSMA for SAP ASE requires a project upgrade, as it now stores full source/target server version in the project metadata.

## SSMA v8.13

The v8.13 release of SSMA for SAP ASE contains the following changes:

* Consider implicit type casts when converting procedure and function calls
* Improve logging for source connection string to help troubleshoot connection issues

## SSMA v8.12

The v8.12 release of SSMA for SAP ASE contains minor performance improvements and bug fixes.

## SSMA v8.11

The v8.11 release of SSMA for SAP ASE contains the following changes:

* Fix conversion of temporary tables
* Use MSAL.NET library for interactive Azure Active Directory authentication

## SSMA v8.10

The v8.10 release of SSMA for SAP ASE contains minor performance improvements and bug fixes.

## SSMA v8.9

The v8.9 release of SSMA for SAP ASE contains the following changes:

* Improve date and time format conversion
* Fix for the issue with missing characters in SQL definitions for objects

## SSMA v8.8

The v8.8 release of SSMA for SAP ASE includes:

* SQL Server objects synchronization stability improvements
* GUI performance improvements during assessment and conversion
* Fix for the issue with missing characters in SQL definitions for objects

## SSMA v8.7

The v8.7 release of SSMA for SAP ASE has minor fixes and performance improvements in graphical user interface.

> [!IMPORTANT]
> With SSMA v8.5 and later, .NET 4.7.2 is an installation pre-requisite. If you need to install this version, you can download the runtime file from [here](https://dotnet.microsoft.com/download/dotnet-framework/net472).

## SSMA v8.6

In addition to a targeted set of fixes designed to improve usability and performance, the v8.6 release of SSMA for SAP ASE has been enhanced by adding a setting that enables users to omit SSMA extended properties in the converted code.

To leverage this setting, in SSMA for SAP ASE, navigate to **Tools** > **Project Settings** > **General** > **Conversion**, and then under **Misc**, update the value of the **Omit Extended Properties** setting to **Yes**.

![Omit Extended Properties setting](../sybase/media/ssma-omit-extended-properties.png)

> [!IMPORTANT]
> With SSMA v8.5 and later, .NET 4.7.2 is an installation pre-requisite. If you need to install this version, you can download the runtime file from [here](https://dotnet.microsoft.com/download/dotnet-framework/net472).

## SSMA v8.5

The v8.5 release of SSMA for SAP ASE is enhanced with support for Azure Active Directory authentication and basic support for JSON features in SQL server, together with a targeted set of fixes designed to improve usability and performance.

In addition, SSMA for SAP ASE now allows you to hide system tables and views (exclude them from conversion).

> [!IMPORTANT]
> With SSMA v8.5, .NET 4.7.2 is an installation pre-requisite. If you need to install this version, you can download the runtime file from [here](https://dotnet.microsoft.com/download/dotnet-framework/net472).

## SSMA v8.4

The v8.4 release of SSMA for SAP ASE is enhanced with targeted fixes that are designed to address accessibility issues and fix a bug related to max index columns (to allow 32 instead of 16) for SQL Server 2016 and later versions.

> [!IMPORTANT]
> With SSMA versions 7.4 through 8.4, .NET 4.5.2 is an installation pre-requisite.

## SSMA v8.3

The v8.3 release of SSMA for SAP ASE is enhanced with targeted fixes that are designed to improve quality and conversion metrics. In addition, this release of SSMA for SAP ASE provides fixes that:

* Address accessibility issues
* Add basic support for `hierarchyid` type in SQL Server

## SSMA v8.2

The v8.2 release of SSMA for SAP ASE is enhanced with a targeted set of fixes designed to improve quality and conversion metrics, as well as fixes for:

* An issue with disabled non-clustered indexes after data migration.
* Detection of .NET Framework during silent installation.
* An intermittent crash that occurs when a new version is downloaded.

> [!NOTE]
> A known issue with auto-update may cause the failure of an update from SSMA v8.1 to v8.2. If you encounter this error, please download the new version and install it manually.

## SSMA v8.1

The v8.1 release of SSMA for SAP ASE is enhanced with targeted fixes that are designed to improve quality and conversion metrics.

> [!NOTE]
> A known issue with auto-update may cause the failure of an update from SSMA v8.0 to v8.1. If you encounter this error, please download the new version and install it manually.

## SSMA v8.0

The v8.0 release of SSMA for SAP ASE is enhanced with targeted fixes designed to improve quality and conversion metrics. In addition, this release offers the following new features:

* Support for **Azure SQL Managed Instance** as a target. You can now create new projects targeting Azure SQL Managed Instance:

  ![SQL Database MI project](../media/ssma-newproject-sqldbmi.png)

* Post-conversion **Fix advisor**. Learn more about it [here](https://blogs.msdn.microsoft.com/datamigration/2019/02/17/%20accelerate-your-oracle-migrations-with-new-machine-learning-capabilities-in-ssma/).

* Preliminary database/schema selection.

  When connecting to the source, the user can now select databases/schemas of interest. Selecting only the schemas that you plan to migrate will save time during initial connection and improve overall SSMA performance.

  ![SSMA filter objects](../media/ssma-filter-objects.png)

## SSMA v7.10

The v7.10 release of SSMA for SAP ASE is enhanced with targeted fixes designed to provide additional security and privacy protections to meet changes in global requirements.

## SSMA v7.9

The v7.9 release of SSMA for SAP ASE contains the following changes:

* Targeted fixes that improve quality and conversion metrics.
* Support in SSMA command line to alter Data Type mapping and Project Preferences.
* Support for migrating data using SQL Server Integration Services (SSIS). After converting the schema, it's possible to create an SSIS package by using a right-click context menu option.
* The Azure SQL Database connection dialog in SSMA has also been altered to specify the fully qualified server name. In previous versions of SSMA, the Azure SQL Database prefix had to be explicitly mentioned inside projects settings.

## SSMA v7.8

The v7.8 release of SSMA for SAP ASE contains the following changes:

* Change type mapping highlighted in **Project Settings**.
* The ability for users to disable telemetry.

## SSMA v7.7

The v7.7 release of SSMA for SAP ASE contains the following changes:

* SSMA for SAP ASE has been enhanced with targeted fixes that improve quality and conversion metrics.
* Based on the popular demand, the 32-bit version of SSMA for SAP ASE is back. Compared to the previous implementation (before to v7.4), there are two installer packages, but they can't be installed side by side. As a result, you must choose the most appropriate version based on the connectivity components you have. It's always preferable to use the 64-bit version, if possible.

## SSMA v7.6

The v7.6 release of SSMA for SAP ASE contains the following changes:

* Targeted fixes that improve quality and conversion metrics and with support for SQL Server 2017 (public preview). Support for SQL Server 2017 on Windows and Linux is in public preview and shouldn't be used for production migrations.
* Support for conversion of Sybase functions.

## SSMA v7.5

The v7.5 release of SSMA for SAP ASE (formerly SSMA for Sybase) contains the following changes:

* Several improvements to ensure greater accessibility for people with disabilities.
* Support for `CREATE OR REPLACE` syntax.

## SSMA v7.4

The v7.4 release of SSMA for Sybase contains the following changes:

* The **Query timeout** option is now available during schema object discovery at source and target.

  ![query timeout option](../media/query-timeout_red.png)
* The quality and conversion metric has been improved with targeted fixes, based on customer feedback.

  > [!IMPORTANT]
  > .NET 4.5.2 is a pre-requisite for installing SSMA v7.4. In addition, beginning with v7.4, the 32-bit version of SSMA is being discontinued.

## SSMA v7.3

The v7.3 release of SSMA for Sybase contains the following changes:

* Improved quality and conversion metric with targeted fixes based on customer feedback.
* SSMA extensibility framework exposed via the following items:
  * Export functionality to a SQL Server Data Tools (SSDT) project.
    * You can now export schema scripts from SSMA to an SSDT project. You can use the schema scripts to make additional schema changes and deploy your database.

        ![Save as SSDT project command](../media/export-schema-scripts_red.png)
  * Libraries that can be consumed by SSMA for performing custom conversions.
    * You can now construct code that can handle custom syntax conversions and conversions that weren't previously handled by SSMA.
      * Instructions on how to construct a custom converter are available in this blog post, [Extending SQL Server Migration Assistant's conversion capabilities](https://blogs.msdn.microsoft.com/datamigration/2017/02/21/2185/).
      * Download a sample project for conversion from this [blog post](https://blogs.msdn.microsoft.com/datamigration/ssmafororacleconversionsample/).

## SSMA v7.2

The v7.2 release of SSMA for Sybase contains the following changes:

* Improved quality and conversion metric with targeted fixes based on customer feedback.
* Telemetry enhancements to provide better data points to troubleshoot customer issues and improve SSMA's conversion rates.

## SSMA v7.1

The v7.1 release of SSMA for Sybase contains the following changes:

* SQL Server 2017 on Windows and Linux CTP1 is now a supported target platform for migration. This feature is in technical preview and supports schema and data movement to target SQL servers.
* Support for automatic updates to download the latest version of SSMA as soon as it's available.
* SSMA installable binaries are now delivered through Windows Installer package files (.msi).

## May 2016

The May 2016 release of SSMA for Sybase contains the following changes:

* Added support for SQL Server 2016.
* Removed installer check for .NET 2.0.
* Updated Extension Pack dependency from .NET 3.5 to .NET 4.0.
* Fixed `save-project` and `open-project` commands for SSMA Console.
* Fixed `securepassword` command for SSMA Console.
* Fixed counting of objects for initial loading.
* Fixed bug in global settings.

## March 2016

The March 2016 preview release of SSMA for Sybase adds support for migration to SQL Server 2016.

## January 2016

The January 2016 maintenance release of SSMA for Sybase contains the following changes:

* Added View Log Menu Item to SSMA (RFC 5706203).
* Added Telemetry.

## July 2014

The July 2014 release of SSMA for Sybase contains the following changes:

* Improved Azure SQL Database code conversion.
* Moved extension pack functionality to schema to support Azure SQL Database.
* Added performance improvements tested for databases with over 10k objects.
* Added UI improvements for dealing with large number of objects.
* Added the ability to highlight well-known LOB schemas (so they can be ignored in conversion).
* Added conversion speed improvements.
* Added the ability to show object counts in UI.
* Reduced report size by more than 25%.
* Improved error messages for unparsed constructs.

## April 2014

The April 2014 release of SSMA for Sybase contains the following changes:

* Added support of MS SQL Server 2014.
* Fixed bugs regarding conversion to Azure.
* Fixed bugs regarding invisible report pages in IE 10.

## January 2012

The January 2012 release of SSMA for Sybase contains the following changes:

* Added support for rollback trigger conversion.
* Provided fix for converting `@@ROWCOUNT` and `@@ERROR` in the same `SET` statement.

## July 2011

The July 2011 release of SSMA for Sybase provides improved error reporting during data migration.

## April 2011

The April 2011 release of SSMA for Sybase contains the following changes:

* Consolidated "SSMA for Sybase" product, which supports [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)], [!INCLUDE [ssSQL10](../../includes/sssql10-md.md)], [!INCLUDE [ssSQL11](../../includes/ssSQL11-md.md)] and Azure SQL.
* Added support for connecting and migrating to [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)].
* Added a new feature to convert and migrate Sybase databases to Azure SQL.
* Enhanced client-side data migration engine, supporting parallel migration of data.
* Improved data migration performance with Simple and Bulk logged recovery models.
* Added the ability to properly convert and migrate case-sensitive Sybase databases to case-sensitive SQL Server.
* Added support for conversion of Sybase ASE Non-ANSI join statements to SQL Server ANSI join statements has been extended to DELETE and UPDATE statements.
* Provided additional connectivity options for connecting to Sybase ASE servers using Sybase ASE ODBC provider and Sybase ASE ADO.NET providers.
* Removed the dependency on a separate database called `SysDB`, which contains the Sybase emulation functions (installed as part of Extension Pack).
* Added the ability to install SSMA for Sybase Extension Pack on [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] clusters.
* Added backward compatibility of projects created by earlier versions of SSMA (v4.0 and v4.2).
* Added the ability to install the SSMA for Sybase v5.0 product side-by-side (SxS) with older versions of SSMA (v4.0 and v4.2).

## July 2010

The July 2010 release of SSMA for Sybase added:

* Support for migrating to SQL Server 2008 R2.
* A new SSMA Console application for command-line execution.
* Support for Data Migration using both Server-Side and Client-Side Data Migration Engines.
* Support for "Custom SELECT" statement in data migration.
* Support for migrating from Sybase ASE 15.0.3 and 15.5.

## June 2008

The June 2008 release of SSMA for Sybase contains the following changes:

* Added SSMA Tester, which automatically tests the database object conversion and the data migration made by SSMA. After all SSMA migration steps are finished, use SSMA Tester to verify that converted objects work the same way and that all data was transferred properly.
* Added Pre-SQL conversion. User now can specify temporary table (and other object) declarations for each source procedure to be used in conversion.
* Added improvements in object conversion:
  * Joins conversion revised.
  * Aggregates and non-aggregates without having/group by clauses.
  * The `IDENTITY` function with a `SELECT INTO` statement.
  * Clustered constraints and indexes on data-only-locked.
  * Temporary tables created by `SELECT INTO`.
  * Constraints / Indexes for temporary tables.
  * New [!INCLUDE [ssSQL10](../../includes/sssql10-md.md)] datetime types are supported.
  * Sybase 15.0 connectivity and data types support.

## May 2007

The May 2007 release of SSMA for Sybase added:

* The ability to load database content faster when saving a project.
* Support for user-entered comments in the SQL Server formatted SQL mode.
* Improvements in object conversion.

## November 2006

The November 2006 release of SSMA for Sybase contains the following changes:

* Added new global settings:
  * You can opt to show line numbers in editor windows.
  * You can configure SSMA to prompt to replace duplicate objects, or always or never replace duplicate objects during schema conversion.
* Added a new conversion option that lets you configure how SSMA handles the following situations:
  * A `CAST` or `CONVERT` statement that contains a binary string.
  * Checks for null values in equality expressions.
  * Proxy tables.
  * User message error numbers for `RAISERROR`.
  * `UPDATE` statements that contain unresolved identifiers.
* Added a new migration option that lets you specify how SSMA should handle dates that are outside the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] date range.
* Added a **Formatted SQL** setting on the **SQL** tab, which formats the code for improved readability.
* Bug fixes, including:
  * SSMA now converts `LOCK TABLE <table> IN { SHARED | EXCLUSIVE } MODE` statements by adding a `TABLOCK` or `TABLOCKX` hint to the subsequent `SELECT` query on the table.
  * The necessary casts are now added when binary types are used in character expressions.
  * Memory and performance improvements.

## July 2006

The July 2006 release of SSMA for Sybase was the initial release.

## See also

[Getting Started with SSMA for Sybase &#40;SybaseToSQL&#41;](../../ssma/sybase/getting-started-with-ssma-for-sybase-sybasetosql.md)
