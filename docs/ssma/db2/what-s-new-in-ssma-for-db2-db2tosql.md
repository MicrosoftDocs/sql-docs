---
title: "What's new in SSMA for Db2 (Db2ToSQL)"
description: Find out about changes to SQL Server Migration Assistant (SSMA) for Db2 (Db2ToSQL) for each release.
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 10/16/2024
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
ms.custom:
  - intro-whats-new
---
# What's new in SSMA for Db2 (Db2ToSQL)

This article lists SQL Server Migration Assistant (SSMA) for Db2 changes in each release.

[!INCLUDE [entra-id](../../includes/entra-id-hard-coded.md)]

## SSMA v10.0

The v10.0 release of SSMA for DB2 contains the following changes:

- Improve conversion of FETCH FIRST ROW ONLY syntax
- Improve conversion support of LOCATE_IN_STRING behavior
- Support of range-partitioned tables in DB2-ZOS
- Improve handling of INCLUDE clause in indexes

## SSMA v9.5

The v9.5 release of SSMA for Db2 contains the following changes:

- Support for large data migration using Azure Data factory
- Fixes to improve code conversion

## SSMA v9.4.1

The v9.4.1 release of SSMA for Db2

- Bug fix for Table Loading in I-series Db2 Instances

## SSMA v9.4

The v9.4 release of SSMA for Db2 contains the following changes:

- Fix for conversion of multiple DELETE statements.
- Improve conversion of handlers
- Improve conversion of Fetch Clause

## SSMA v9.3

The v9.3 release of SSMA for Db2 contains the following changes:

- Support for system version tables
- Support for SQL Server 2022 target

## SSMA v9.2

The v9.2 release of SSMA for Db2 contains minor performance improvements, bug fixes, and enhanced get help experience.

## SSMA v9.1

The v9.1 release of SSMA for Db2 contains minor performance improvements, bug fixes, and enhanced get help experience.

## SSMA v9.0

The v9.0 release of SSMA for Db2 contains minor performance improvements, bug fixes, and online help viewer support.

- Add support for LISTAGG() for Db2 LUW

## SSMA v8.24

The v8.24 release of SSMA for Db2 contains the following changes:

- Add support for ORDER BY clause in subquery
- Add support for ROW_NUMBER function
- Improve conversion for UNION/UNION ALL statements
- Improve naming for "Save As" statements

## SSMA v8.23

The v8.23 release of SSMA for Db2 contains the following changes:

- Improvements to TOTALORDER function
- Conversion support for ANCHORED data type
- New "Allow Statements from Files" option, for adding statements from file system
- Enhanced XML output when running in console mode
- New "Feedback" link in reports to send feedback
- In reports, enhanced visualization experience

## SSMA v8.22

The v8.22 release of SSMA for Db2 contains the following changes:

- Fix AM/PM handling in `ssma_db2.TIMESTAMP_FORMAT` emulation function
- Improve day-of-week validation in `ssma_db2.TIMESTAMP_FORMAT` emulation function
- Allow to specify extra connection string options for target database connection
- Introduce summary charts in assessment reports
- Improve messages navigation in assessment reports

## SSMA v8.21

The v8.21 release of SSMA for Db2 contains the following changes:

- Load aliases for old/new row/table within the triggers definition for Db2 for i
- Add conversion for the `LCASE`, `UCASE`, `LOWER`, `UPPER` functions with two and three arguments
- Add conversion for the `LTRIM` and `RTRIM` functions with two arguments
- Improve conversion for `LOCATE` and `POSITION` functions
- Fix return type of the `ssma_db2.CHAR` emulation function
- Fix arguments of the `ssma_db2.DECIMAL` emulation function
- Use `COUNT_BIG` in row count queries for the target database

## SSMA v8.20

The v8.20 release of SSMA for Db2 contains the following changes:

- Improve `VARCHAR_FORMAT` emulation function
- Fix tables discovery for Db2 for i

## SSMA v8.19

The v8.19 release of SSMA for Db2 contains the following changes:

- Improve `TIMESTAMP_FORMAT` emulation function
- Improve foreign keys discovery for z/OS platform

## SSMA v8.18

The v8.18 release of SSMA for Db2 contains the following changes:

- Add support for identity columns

## SSMA v8.17

The v8.17 release of SSMA for Db2 contains the following changes:

- Improve conversion of TRANSLATE function
- Fix data migration for tables with computed columns
- Update HTML assessment reports to use modern editor to display SQL text

## SSMA v8.16

The v8.16 release of SSMA for Db2 contains the following changes:

- Fix conversion of column aliases with special characters
- Fix conversion for `SELECTIVITY` clause
- Improve conversion for `WITH ROW MOVEMENT` clause
- Remove support for legacy parser
- Fix issue with objects not refreshing from database

## SSMA v8.15

In addition to several accessibility improvements, the v8.15 release of SSMA for Db2 contains the following changes:

- Fix conversion of `MIN`/`MAX` aggregate functions with date/time arguments
- Fix bug in `VARCHAR_FORMAT` emulation function when `DD` placeholder is used
- Improve type mappings for `TIME` data type
- Improve conversion of `ROUND` and `TRUNC` functions with numeric arguments
- Revamp assessment reports to work in modern browsers
- Use authority provided by the database for Microsoft Entra/Azure AD authentication
- Improve naming for statements loaded from files

## SSMA v8.14

In addition to several improvements to ensure greater accessibility for people with disabilities, the v8.14 release of SSMA for Db2 requires a project upgrade, as it now stores full source/target server version in the project metadata.

## SSMA v8.13

The v8.13 release of SSMA for Db2 contains the following changes:

- Support for filtered unique indexes
- Consider implicit type casts when converting procedure and function calls
- Improve logging for source connection string to help troubleshoot connection issues

## SSMA v8.12

The v8.12 release of SSMA for Db2 contains the following changes:

- Conversion of `STRIP` function
- Improved parsing of procedure options

## SSMA v8.11

The v8.11 release of SSMA for Db2 contains the following changes:

- Support for Db2 for i (v7.1 and above)
- Translation of `SQLSTATE` and `SQLCODE`
- Conversion error message for side-effecting operators within a function
- Use MSAL.NET library for interactive Microsoft Entra/Azure AD authentication

## SSMA v8.10

The v8.10 release of SSMA for Db2 addresses a regression in foreign keys discovery and contains minor performance improvements.

## SSMA v8.9

The v8.9 release of SSMA for Db2 contains the following changes:

- Fix for conversion of `TIMESTAMPDIFF` function
- Fix for indexes discovery when partitioned index is present
- Fix for foreign keys discovery when primary index is defined in another schema
- Improved conversion for columns that match built-in function names
- Fix for the issue with special characters in project name

## SSMA v8.8

The v8.8 release of SSMA for Db2 includes:

- SQL Server objects synchronization stability improvements
- GUI performance improvements during assessment and conversion
- Updated mapping from `ROWID` to `varbinary(40)` to facilitate data migration
- Improved conversion of `SELECT ... FROM NEW/OLD TABLE` statements
- New conversion of `ALTER` statements for procedures and functions
- New conversion of destructuring assignments

## SSMA v8.7

The v8.7 release of SSMA for Db2 includes brand new Db2 syntax parser, and minor fixes and performance improvements in graphical user interface.

In addition, SSMA for Db2 now provides:

- A fix for discovery of foreign keys when migrating from Db2 on LUW.
- Improved conversion of `SELECT ... FOR UPDATE` statement.
- Improved conversion for `COUNT` function in MQ tables.
- Conversion of `SAVEPOINT` statements.
- Conversion to emulate Db2's behavior for `NULL` values in `ORDER BY` clause.
- Parsing support for `ASSOCIATE RESULT SET` statement.

> [!IMPORTANT]  
> With SSMA v8.5 and later, .NET 4.7.2 is an installation prerequisite. If you need to install this version, you can download the runtime file from [here](https://dotnet.microsoft.com/download/dotnet-framework/net472).

## SSMA v8.6

In addition to a targeted set of fixes designed to improve usability and performance, the v8.6 release of SSMA for Db2 has been enhanced by adding a setting that allows you to omit SSMA extended properties in the converted code.

To use this setting, in SSMA for Db2, navigate to **Tools** > **Project Settings** > **General** > **Conversion**, and then under **Misc**, update the value of the **Omit Extended Properties** setting to **Yes**.

:::image type="content" source="media/ssma-omit-extended-properties.png" alt-text="Screenshot of Omit Extended Properties setting." lightbox="media/ssma-omit-extended-properties.png":::

In addition, SSMA for Db2 now provides:

- A fix for conversion of functions that use default argument values.
- Improved parsing of the `PARAMETER` clause for functions.
- The ability to convert the `LEAVE` statement.

> [!IMPORTANT]  
> With SSMA v8.5 and later, .NET 4.7.2 is an installation prerequisite. If you need to install this version, you can download the runtime file from [here](https://dotnet.microsoft.com/download/dotnet-framework/net472).

## SSMA v8.5

The v8.5 release of SSMA for Db2 is enhanced with support for Microsoft Entra/Azure AD authentication and basic support for JSON features in SQL Server, together with a targeted set of fixes designed to improve usability and performance.

In addition, SSMA for Db2 has been enhanced with:

- Support for adding conversion for `GET DIAGNOSTICS` statement with `ROW_NUMBER`.
- A fix for a bug related to spaces at the beginning of the object name not being respected.

> [!IMPORTANT]  
> With SSMA v8.5, .NET 4.7.2 is an installation prerequisite. If you need to install this version, you can download the runtime file from [here](https://dotnet.microsoft.com/download/dotnet-framework/net472).

## SSMA v8.4

The v8.4 release of SSMA for Db2 is enhanced with targeted fixes that are designed to address accessibility issues and fix a bug related to max index columns (to allow 32 instead of 16) for SQL Server 2016 and later versions.

> [!IMPORTANT]  
> With SSMA versions 7.4 though 8.4, .NET 4.5.2 is an installation prerequisite.

## SSMA v8.3

The v8.3 release of SSMA for Db2 is enhanced with targeted fixes that are designed to improve quality and conversion metrics. In addition, this release of SSMA for Db2 provides fixes that:

- Address accessibility issues.
- Add basic support for `hierarchyid` type in SQL Server.
- Replace TRIM function usage in z/OS discovery queries with `RTRIM`/`LTRIM`.
- Allow user to specify Package Collection when connecting in 'Standard mode' (defaults to `NULLID`).
- Add conversion for `CREATE TABLE AS SELECT`.
- Improve conversions for global temp tables.
- Address an issue with object uniqueness check order to prioritize tables over constraints, if names collide.
- Address an issue with loading of default column values for `DATE` and `TIMESTAMP` for z/OS.
- Support Unicode line feed character (also known as `NEL`).
- Address an issue with cursor conversion with missing `RETURN TO` clause.
- Add support for labels and `GOTO`.

## SSMA v8.2

The v8.2 release of SSMA for Db2 is enhanced with to address issues with connections to Azure SQL Database from the SSMA console tool and missing COUNT_BIG column in views declaration during conversion. In addition, this version includes a targeted set of fixes designed to improve quality and conversion metrics, and fixes for:

- An issue with disabled nonclustered indexes after data migration.
- Detection of .NET Framework during silent installation.
- An intermittent crash that occurs when a new version is downloaded.

> [!NOTE]  
> A known issue with auto-update might cause the failure of an update from SSMA v8.1 to v8.2. If you encounter this error, please download the new version and install it manually.

## SSMA v8.1

The v8.1 release of SSMA for Db2 is enhanced to provide targeted fixes that are designed to improve quality and conversion metrics.

> [!NOTE]  
> A known issue with auto-update might cause the failure of an update from SSMA v8.0 to v8.1. If you encounter this error, please download the new version and install it manually.

## SSMA v8.0

The v8.0 release of SSMA for Db2 is enhanced to provide targeted fixes designed to improve quality and conversion metrics. This release also offers the following new features:

- Support for **Azure SQL Managed Instance** as a target. You can now create new projects targeting Azure SQL Managed Instance:

:::image type="content" source="../media/ssma-newproject-sqldbmi.png" alt-text="Screenshot of SQL MI project.":::

- Post-conversion **Fix advisor**. Learn more about it [here](https://techcommunity.microsoft.com/t5/microsoft-data-migration-blog/bg-p/MicrosoftDataMigration).

- Preliminary database/schema selection.

  When you connect to the source, you can now select databases/schemas of interest. Selecting only the schemas that you plan to migrate saves time during initial connection and improve overall SSMA performance.

:::image type="content" source="../media/ssma-filter-objects.png" alt-text="Screenshot of SSMA filter objects.":::

## SSMA v7.10

The v7.10 release of SSMA for Db2 contains the following changes:

- Targeted fixes designed to provide more security and privacy protections to meet changes in global requirements.
- A fix for conversion of `BEGIN-END` blocks.

## SSMA v7.9

The v7.9 release of SSMA for Db2 contains the following changes:

- Targeted fixes that improve quality and conversion metrics.
- Support in SSMA command line to alter Data Type mapping and Project Preferences.
- Support for migrating data using SQL Server Integration Services (SSIS). After converting the schema, it's possible to create an SSIS package by using a right-click context menu option.
- The Azure SQL Database connection dialog in SSMA has also been altered to specify the fully qualified server name. In previous versions of SSMA, the Azure SQL Database prefix had to be explicitly mentioned inside projects settings.

## SSMA v7.8

The v7.8 release of SSMA for Db2 contains the following changes:

- Change type mapping highlighted in *Project Settings*.
- The ability for users to disable telemetry.

## SSMA v7.7

The v7.7 release of SSMA for Db2 contains the following changes:

- Targeted fixes that improve quality and conversion metrics.
- Based on the popular demand, the 32-bit version of SSMA for Db2 is back. Compared to the previous implementation (before v7.4), there are two installer packages, but they can't be installed side by side. As a result, you must choose the most appropriate version based on the connectivity components you have. It's always preferable to use the 64-bit version, if possible.

## SSMA v7.6

The v7.6 release of SSMA for Db2 is enhanced with targeted fixes that improve quality and conversion metrics and with support for SQL Server 2017 (public preview). Support for SQL Server 2017 on Windows and Linux is in public preview and shouldn't be used for production migrations.

## SSMA v7.5

The v7.5 release of SSMA for Db2 is enhanced with several improvements to ensure greater accessibility for people with disabilities.

## SSMA v7.4

The v7.4 release of SSMA for Db2 contains the following changes:

- The **Query timeout** option is now available during schema object discovery at source and target.

:::image type="content" source="../media/query-timeout_red.png" alt-text="Screenshot of query timeout option.":::

- The quality and conversion metric has been improved with targeted fixes, based on customer feedback.

  > [!IMPORTANT]  
  > .NET 4.5.2 is a prerequisite for installing SSMA v7.4. In addition, beginning with v7.4, the 32-bit version of SSMA has been discontinued.

## SSMA v7.3

The v7.3 release of SSMA for Db2 contains the following changes:

- Improved quality and conversion metric with targeted fixes based on customer feedback.
- SSMA extensibility framework exposed via the following items:
  - Export functionality to a SQL Server Data Tools (SSDT) project.
    - You can now export schema scripts from SSMA to an SSDT project. You can use the schema scripts to make additional schema changes and deploy your database.

      :::image type="content" source="../media/export-schema-scripts_red.png" alt-text="Screenshot of Save as SSDT project command.":::

  - Libraries that can be consumed by SSMA for performing custom conversions.
    - You can now construct code that can handle custom syntax conversions and conversions that weren't previously handled by SSMA.
      - Instructions on how to construct a custom converter are available in this blog post, [Extending SQL Server Migration Assistant's conversion capabilities](https://techcommunity.microsoft.com/t5/microsoft-data-migration-blog/bg-p/MicrosoftDataMigration).
      - Download a sample project for conversion from this [blog post](https://techcommunity.microsoft.com/t5/microsoft-data-migration-blog/bg-p/MicrosoftDataMigration).

## SSMA v7.2

The v7.2 release of SSMA for Db2 contains the following changes:

- Improved quality and conversion metric with targeted fixes based on customer feedback.
- Telemetry enhancements to provide better data points to troubleshoot customer issues and improve SSMA's conversion rates.

## SSMA v7.1

The v7.1 release of SSMA for Db2 contains the following changes:

- SQL Server 2017 on Windows and Linux CTP1 is now a supported target platform for migration. This feature is in technical preview and allows schema and data movement to target SQL Server instances.

- Support for automatic updates to download the latest version of SSMA as soon as it's available.

- SSMA installable binaries are now delivered through Windows Installer package files (.msi).

## May 2016

The May 2016 release of SSMA for Db2 contains the following changes:

- Added support for SQL Server 2016.
- Added conversion of Db2 in-memory and regular tables to SQL Server in-memory and hekaton features.
- Added conversion of Db2 access controls to SQL Server Policy objects (Row Level Security for Db2).
- Added conversion of Db2 system-versioned tables to SQL Server temporal tables.
- Improved Db2 parser and resolver.
- Removed installer check for .NET 2.0.
- Removed unnecessary `*.dll` files from Db2 installer.
- Fixed `save-project` and `open-project` commands for SSMA console.
- Fixed `securepassword` command for SSMA console.
- Fixed counting of objects for initial loading.
- Fixed bug in global settings.

## March 2016

The March 2016 preview release of SSMA for Db2 adds support for migration to SQL Server 2016.

## January 2016

The January 2016 maintenance release of SSMA for Db2 contains the following changes:

- Added support for several standard functions.
- Fixed Db2 parser errors.
- Fixed Db2 v9 zOS support (RFC 5690920).
- Fixed Db2 unresolved identifier errors during conversion.
- Added View Log Menu Item to SSMA (RFC 5706203).
- Added Telemetry.

## November 2014

The November 2014 release of SSMA for Db2 was the initial release.
