---
title: "What's New in SSMA for DB2 (DB2ToSQL) | Microsoft Docs"
description: Find out about changes to SQL Server Migration Assistant (SSMA) for DB2 (DB2ToSQL) for each release.
author: nahk-ivanov

ms.prod: sql
ms.custom: ""
ms.date: "7/31/2020"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: 1cc38f85-3caa-42d0-8c76-a380c1d15c67
ms.author: alexiva
---
# What's New in SSMA for DB2 (DB2ToSQL)

This article lists SQL Server Migration Assistant (SSMA) for DB2 changes in each release.

## SSMA v8.12

The v8.12 release of SSMA for DB2 contains the following changes:

* Conversion of `STRIP` function
* Improved parsing of procedure options

## SSMA v8.11

The v8.11 release of SSMA for DB2 contains the following changes:

* Support for DB2 for i (v7.1 and above)
* Translation of `SQLSTATE` and `SQLCODE`
* Conversion error message for side-effecting operators within a function
* Use MSAL.NET library for interactive Azure Active Directory authentication

## SSMA v8.10

The v8.10 release of SSMA for DB2 addresses a regression in foreign keys discovery and contains minor performance improvements.

## SSMA v8.9

The v8.9 release of SSMA for DB2 contains the following changes:

* Fix for conversion of `TIMESTAMPDIFF` function
* Fix for indexes discovery when partitioned index is present
* Fix for foreign keys discovery when primary index is defined in another schema
* Improved conversion for columns that match built-in function names
* Fix for the issue with special characters in project name

## SSMA v8.8

The v8.8 release of SSMA for DB2 includes:

* SQL Server objects synchronization stability improvements
* GUI performance improvements during assessment and conversion
* Updated mapping from `ROWID` to `varbinary(40)` to facilitate data migration
* Improved conversion of `SELECT ... FROM NEW/OLD TABLE` statements
* New conversion of `ALTER` statements for procedures and functions
* New conversion of destructuring assignments

## SSMA v8.7

The v8.7 release of SSMA for DB2 includes brand new DB2 syntax parser, as well as minor fixes and performance improvements in graphical user interface.

In addition, SSMA for DB2 now provides:

* A fix for discovery of foreign keys when migrating from DB2 on LUW.
* Improved conversion of `SELECT ... FOR UPDATE` statement.
* Improved conversion for `COUNT` function in MQ tables.
* Conversion of `SAVEPOINT` statements.
* Conversion to emulate DB2's behavior for `NULL` values in `ORDER BY` clause.
* Parsing support for `ASSOCIATE RESULT SET` statement.

> [!IMPORTANT]
> With SSMA v8.5 and later, .NET 4.7.2 is an installation pre-requisite. If you need to install this version, you can download the runtime file from [here](https://dotnet.microsoft.com/download/dotnet-framework/net472).

## SSMA v8.6

In addition to a targeted set of fixes designed to improve usability and performance, the v8.6 release of SSMA for DB2 has been enhanced by adding a setting that enables users to omit SSMA extended properties in the converted code.

To leverage this setting, in SSMA for DB2, navigate to **Tools** > **Project Settings** > **General** > **Conversion**, and then under **Misc**, update the value of the **Omit Extended Properties** setting to **Yes**.

![Omit Extended Properties setting](../db2/media/ssma-omit-extended-properties.png)

In addition, SSMA for DB2 now provides:

* A fix for conversion of functions that use default argument values.
* Improved parsing of the `PARAMETER` clause for functions.
* The ability to convert the `LEAVE` statement.

> [!IMPORTANT]
> With SSMA v8.5 and later, .NET 4.7.2 is an installation pre-requisite. If you need to install this version, you can download the runtime file from [here](https://dotnet.microsoft.com/download/dotnet-framework/net472).

## SSMA v8.5

The v8.5 release of SSMA for DB2 is enhanced with support for Azure Active Directory authentication and basic support for JSON features in SQL server, together with a targeted set of fixes designed to improve usability and performance.

In addition, SSMA for DB2 has been enhanced with:

* Support for adding conversion for `GET DIAGNOSTICS` statement with `ROW_NUMBER`.
* A fix for a bug related to spaces at the beginning of the object name not being respected.

> [!IMPORTANT]
> With SSMA v8.5, .NET 4.7.2 is an installation pre-requisite. If you need to install this version, you can download the runtime file from [here](https://dotnet.microsoft.com/download/dotnet-framework/net472).

## SSMA v8.4

The v8.4 release of SSMA for DB2 is enhanced with targeted fixes that are designed to address accessibility issues and fix a bug related to max index columns (to allow 32 instead of 16) for SQL Server 2016 and later versions.

> [!IMPORTANT]
> With SSMA versions 7.4 though 8.4, .NET 4.5.2 is an installation pre-requisite.

## SSMA v8.3

The v8.3 release of SSMA for DB2 is enhanced with targeted fixes that are designed to improve quality and conversion metrics. In addition, this release of SSMA for DB2 provides fixes that:

* Address accessibility issues.
* Add basic support for `hierarchyid` type in SQL Server.
* Replace TRIM function usage in z/OS discovery queries with `RTRIM`/`LTRIM`.
* Allow user to specify Package Collection when connecting in 'Standard mode' (defaults to `NULLID`).
* Add conversion for `CREATE TABLE AS SELECT`.
* Improve conversions for global temp tables.
* Address an issue with object uniqueness check order to prioritize tables over constraints, if names collide.
* Address an issue with loading of default column values for `DATE` and `TIMESTAMP` for z/OS.
* Support Unicode line feed character (also known as `NEL`).
* Address an issue with cursor conversion with missing `RETURN TO` clause.
* Add support for labels and `GOTO`.

## SSMA v8.2

The v8.2 release of SSMA for DB2 is enhanced with to address issues with connections to Azure SQL Database from the SSMA console tool and missing COUNT_BIG column in views declaration during conversion. In addition, this version includes a targeted set of fixes designed to improve quality and conversion metrics, as well as fixes for:

* An issue with disabled non-clustered indexes after data migration.
* Detection of .NET Framework during silent installation.
* An intermittent crash that occurs when a new version is downloaded.

> [!NOTE]
> A known issue with auto-update may cause the failure of an update from SSMA v8.1 to v8.2. If you encounter this error, please download the new version and install it manually.

## SSMA v8.1

The v8.1 release of SSMA for DB2 is enhanced to provide targeted fixes that are designed to improve quality and conversion metrics.

> [!NOTE]
> A known issue with auto-update may cause the failure of an update from SSMA v8.0 to v8.1. If you encounter this error, please download the new version and install it manually.

## SSMA v8.0

The v8.0 release of SSMA for DB2 is enhanced to provide targeted fixes designed to improve quality and conversion metrics. This release also offers the following new features:

* Support for **Azure SQL Database Managed Instance** as a target. You can now create new projects targeting Azure SQL Database Managed Instance:

  ![SQL MI project](../media/ssma-newproject-sqldbmi.png)

* Post-conversion **Fix advisor**. Learn more about it [here](https://blogs.msdn.microsoft.com/datamigration/2019/02/17/%20accelerate-your-oracle-migrations-with-new-machine-learning-capabilities-in-ssma/).

* Preliminary database/schema selection.

  When connecting to the source, the user can now select databases/schemas of interest. Selecting only the schemas that you plan to migrate will save time during initial connection and improve overall SSMA performance.

  ![SSMA filter objects](../media/ssma-filter-objects.png)

## SSMA v7.10

The v7.10 release of SSMA for DB2 contains the following changes:

* Targeted fixes designed to provide additional security and privacy protections to meet changes in global requirements.
* A fix for conversion of `BEGIN-END` blocks.

## SSMA v7.9

The v7.9 release of SSMA for DB2 contains the following changes:

* Targeted fixes that improve quality and conversion metrics.
* Support in SSMA command line to alter Data Type mapping and Project Preferences.
* Support for migrating data using SQL Server Integration Services (SSIS). After converting the schema, it's possible to create an SSIS package by using a right-click context menu option.
* The Azure SQL Database connection dialog in SSMA has also been altered to specify the fully qualified server name. In previous versions of SSMA, the Azure SQL Database prefix had to be explicitly mentioned inside projects settings.

## SSMA v7.8

The v7.8 release of SSMA for DB2 contains the following changes:

* Change type mapping highlighted in *Project Settings*.
* The ability for users to disable telemetry.

## SSMA v7.7

The v7.7 release of SSMA for DB2 contains the following changes:

* Targeted fixes that improve quality and conversion metrics.
* Based on the popular demand, the 32-bit version of SSMA for DB2 is back. Compared to the previous implementation (prior to v7.4), there are two installer packages, but they can't be installed side by side. As a result, you must choose the most appropriate version based on the connectivity components you have. It's always preferable to use the 64-bit version, if possible.

## SSMA v7.6

The v7.6 release of SSMA for DB2 is enhanced with targeted fixes that improve quality and conversion metrics and with support for SQL Server 2017 (public preview). Support for SQL Server 2017 on Windows and Linux is in public preview and shouldn't be used for production migrations.

## SSMA v7.5

The v7.5 release of SSMA for DB2 is enhanced with several improvements to ensure greater accessibility for people with disabilities.

## SSMA v7.4

The v7.4 release of SSMA for DB2 contains the following changes:

* The **Query timeout** option is now available during schema object discovery at source and target.

  ![query timeout option](../media/query-timeout_red.png)

* The quality and conversion metric has been improved with targeted fixes, based on customer feedback.

  > [!IMPORTANT]
  > .NET 4.5.2 is a pre-requisite for installing SSMA v7.4. In addition, beginning with v7.4, the 32-bit version of SSMA has been discontinued.

## SSMA v7.3

The v7.3 release of SSMA for DB2 contains the following changes:

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

The v7.2 release of SSMA for DB2 contains the following changes:

* Improved quality and conversion metric with targeted fixes based on customer feedback.
* Telemetry enhancements to provide better data points to troubleshoot customer issues and improve SSMA's conversion rates.

## SSMA v7.1

The v7.1 release of SSMA for DB2 contains the following changes:

* SQL Server 2017 on Windows and Linux CTP1 is now a supported target platform for migration. This feature is in technical preview and allows schema and data movement to target SQL servers.
* Support for automatic updates to download the latest version of SSMA as soon as it's available.
* SSMA installable binaries are now delivered through Windows Installer package files (.msi).

## May 2016

The May 2016 release of SSMA for DB2 contains the following changes:

* Added support for SQL Server 2016.
* Added conversion of DB2 in-memory and regular tables to SQL Server in-memory and hekaton features.
* Added conversion of DB2 access controls to SQL Server Policy objects (Row Level Security for DB2).
* Added conversion of DB2 system-versioned tables to SQL Server temporal tables.
* Improved DB2 parser and resolver.
* Removed installer check for .NET 2.0.
* Removed unnecessary \*.dll from Db2 installer.
* Fixed `save-project` and `open-project` commands for SSMA Console.
* Fixed `securepassword` command for SSMA Console.
* Fixed counting of objects for initial loading.
* Fixed bug in global settings.
  
## March 2016

The March 2016 preview release of SSMA for DB2 adds support for migration to SQL Server 2016.

## January 2016

The January 2016 maintenance release of SSMA for DB2 contains the following changes:
  
* Added support for a number of standard functions.
* Fixed DB2 Parser Errors.
* Fixed DB2 v9 zOS Support (RFC 5690920).
* Fixed DB2 unresolved identifier errors during conversion.
* Added View Log Menu Item to SSMA (RFC 5706203).
* Added Telemetry.
  
## November 2014

The November 2014 release of SSMA for DB2 was the initial release.
