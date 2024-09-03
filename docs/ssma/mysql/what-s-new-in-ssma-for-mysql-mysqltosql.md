---
title: "What's New in SSMA for MySQL (MySQLToSQL)"
description: Find out about changes to SQL Server Migration Assistant (SSMA) for MySQL (MySQLToSQL) for each release.
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 08/03/2023
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.custom:
  - intro-whats-new
  - sql-migration-content
---
# What's New in SSMA for MySQL (MySQLToSQL)

This article lists SQL Server Migration Assistant (SSMA) for MySQL changes in each release.

[!INCLUDE [entra-id](../../includes/entra-id-hard-coded.md)]

## SSMA v9.5

The v9.5 release of SSMA for MySQL contains the following changes:

- Support for exclude schema option in console mode

## SSMA v9.4

The v9.4 release of SSMA for MySQL contains the following changes:

- Fix for missing subscriptions in migration through migration at scale.

## SSMA v9.3

The v9.3 release of SSMA for MySQL contains the following changes:

- Add support for MySQL 8.x
- Add support for common table expressions
- Add support for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] target
- Fix for precision loss of **datetime** data type column migration

## SSMA v9.2

The v9.2 release of SSMA for MySQL contains the following changes:

- Enhanced data movement pipelines at scale monitoring

## SSMA v9.1

The v9.1 release of SSMA for MySQL contains the following changes:

- Add support for at scale data migration from MySQL to SQL target
- Improve get help experience

## SSMA v9.0

The v9.0 release of SSMA for MySQL contains minor performance improvements, bug fixes and online help viewer support.

## SSMA v8.24

The v8.24 release of SSMA for MySQL contains the following changes:

- Add support for **YEAR** data type
- Improve conversion of comment styles
- Improve naming for "Save As" statements
- Fix issue

## SSMA v8.23

The v8.23 release of SSMA for MySQL contains the following changes:

- New option, **Allow Statements from Files**, for adding statements from file system
- Enhanced XML output when running in console mode
- New **Feedback** link in reports to send feedback
- In reports, enhanced visualization experience

## SSMA v8.22

The v8.22 release of SSMA for MySQL contains the following changes:

- Allow to specify additional connection string options for target database connection
- Introduce summary charts in assessment reports
- Improve messages navigation in assessment reports

## SSMA v8.21

The v8.21 release of SSMA for MySQL contains the following changes:

- Use `COUNT_BIG` in row count queries for the target database

## SSMA v8.20

The v8.20 release of SSMA for MySQL contains the following changes:

- Minor performance improvements and bug fixes

## SSMA v8.19

The v8.19 release of SSMA for MySQL contains the following changes:

- Minor performance improvements and bug fixes

## SSMA v8.18

The v8.18 release of SSMA for MySQL contains the following changes:

- Minor performance improvements and bug fixes

## SSMA v8.17

The v8.17 release of SSMA for MySQL contains the following changes:

- Update HTML assessment reports to use modern editor to display SQL text

## SSMA v8.16

The v8.16 release of SSMA for MySQL contains the following changes:

- Add support for computed columns
- Fix issues when converting `INSERT` statement for tables with unique constraints and primary keys
- Update parser to respect `ANSI_QUOTES` and `NO_BACKSLASH_ESCAPES` server modes
- Remove support for legacy parser
- Fix issue with objects not refreshing from database

## SSMA v8.15

In addition to several accessibility improvements, the v8.15 release of SSMA for MySQL contains the following changes:

- Revamp assessment reports to work in modern browsers
- Use authority provided by the database for Microsoft Entra/Azure AD authentication
- Improve naming for statements loaded from files

## SSMA v8.14

In addition to several improvements to ensure greater accessibility for people with disabilities, the v8.14 release of SSMA for MySQL requires a project upgrade, as it now stores full source/target server version in the project metadata.

## SSMA v8.13

The v8.13 release of SSMA for MySQL contains the following changes:

- Consider implicit type casts when converting procedure and function calls
- Improve logging for source connection string to help troubleshoot connection issues

## SSMA v8.12

The v8.12 release of SSMA for MySQL contains the following changes:

- Conversion of temporary tables DDL

## SSMA v8.11

The v8.11 release of SSMA for MySQL contains the following changes:

- Use MSAL.NET library for interactive Microsoft Entra/Azure AD authentication

## SSMA v8.10

The v8.10 release of SSMA for MySQL contains minor performance improvements and bug fixes.

## SSMA v8.9

The v8.9 release of SSMA for MySQL contains the following changes:

- Fix for data migration of spatial types
- Fix for the issue with special characters in project name

## SSMA v8.8

The v8.8 release of SSMA for MySQL includes:

- [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] objects synchronization stability improvements
- GUI performance improvements during assessment and conversion

## SSMA v8.7

The v8.7 release of SSMA for MySQL has minor fixes and performance improvements in graphical user interface.

In addition, SSMA for MySQL now provides conversion for `LIMIT` clause when targeting Azure SQL.

> [!IMPORTANT]  
> With SSMA v8.5 and later, .NET 4.7.2 is an installation prerequisite. If you need to install this version, you can [download the runtime file](https://dotnet.microsoft.com/download/dotnet-framework/net472).

## SSMA v8.6

In addition to a targeted set of fixes designed to improve usability and performance, the v8.6 release of SSMA for MySQL has been enhanced by adding a setting that enables users to omit SSMA extended properties in the converted code.

To use this setting, in SSMA for MySQL, navigate to **Tools** > **Project Settings** > **General** > **Conversion**, and then under **Misc**, update the value of the **Omit Extended Properties** setting to **Yes**.

:::image type="content" source="../mysql/media/ssma-omit-extended-properties.png" alt-text="Screenshot of Omit Extended Properties setting.":::

> [!IMPORTANT]  
> With SSMA v8.5 and later, .NET 4.7.2 is an installation prerequisite. If you need to install this version, you can [download the runtime file](https://dotnet.microsoft.com/download/dotnet-framework/net472).

## SSMA v8.5

The v8.5 release of SSMA for MySQL is enhanced with support for Microsoft Entra/Azure AD authentication and basic support for JSON features in SQL Server, together with a targeted set of fixes designed to improve usability and performance.

> [!IMPORTANT]  
> With SSMA v8.5, .NET 4.7.2 is an installation prerequisite. If you need to install this version, you can [download the runtime file](https://dotnet.microsoft.com/download/dotnet-framework/net472).

## SSMA v8.4

The v8.4 release of SSMA for MySQL is enhanced with targeted fixes that are designed to address accessibility issues and fix a bug related to max index columns (to allow 32 instead of 16) for [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions.

> [!IMPORTANT]  
> With SSMA versions 7.4 though 8.4, .NET 4.5.2 is an installation prerequisite.

## SSMA v8.3

The v8.3 release of SSMA for MySQL is enhanced with targeted fixes that are designed to improve quality and conversion metrics. In addition, this release of SSMA for MySQL provides fixes that:

- Address accessibility issues.
- Add basic support for **hierarchyid** type in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

## SSMA v8.2

The v8.2 release of SSMA for MySQL is enhanced with a targeted set of fixes designed to improve quality and conversion metrics, and fixes for:

- An issue with disabled nonclustered indexes after data migration.
- Detection of .NET Framework during silent installation.
- An intermittent crash that occurs when a new version is downloaded.

> [!NOTE]  
> A known issue with auto-update may cause the failure of an update from SSMA v8.1 to v8.2. If you encounter this error, please download the new version and install it manually.

## SSMA v8.1

The v8.1 release of SSMA for MySQL is enhanced with targeted fixes that are designed to improve quality and conversion metrics.

> [!NOTE]  
> A known issue with auto-update may cause the failure of an update from SSMA v8.0 to v8.1. If you encounter this error, please download the new version and install it manually.

## SSMA v8.0

The v8.0 release of SSMA for MySQL is enhanced with targeted fixes designed to improve quality and conversion metrics. This release also offers the following new features:

- Support for **Azure SQL Managed Instance** as a target. You can now create new projects targeting Azure SQL Managed Instance:

  :::image type="content" source="../media/ssma-newproject-sqldbmi.png" alt-text="Screenshot of SQL Managed Instance project.":::

- Post-conversion **Fix advisor**. For more information, see more about it [here](https://techcommunity.microsoft.com/t5/microsoft-data-migration-blog/accelerate-your-oracle-migrations-with-new-machine-learning/ba-p/368733).

- Preliminary database/schema selection.

  When connecting to the source, the user can now select databases/schemas of interest. Selecting only the schemas that you plan to migrate saves time during initial connection and improve overall SSMA performance.

  :::image type="content" source="../media/ssma-filter-objects.png" alt-text="Screenshot of SSMA filter objects.":::

## SSMA v7.10

The v7.10 release of SSMA for MySQL contains the following changes:

- Targeted fixes designed to provide additional security and privacy protections to meet changes in global requirements.
- A fix for conversion of spaces between function name and arguments list.

## SSMA v7.9

The v7.9 release of SSMA for MySQL contains the following changes:

- Targeted fixes that improve quality and conversion metrics.
- Partial support for migrating spatial data types from MySQL to Azure SQL Database.
- Support in SSMA command line to alter Data Type mapping and Project Preferences.
- Support for migrating data using SQL Server Integration Services (SSIS). After converting the schema, it's possible to create an SSIS package by using a right-click context menu option.
- The Azure SQL Database connection dialog in SSMA has also been altered to specify the fully qualified server name. In previous versions of SSMA, the Azure SQL Database prefix had to be explicitly mentioned inside projects settings.

## SSMA v7.8

The v7.8 release of SSMA for MySQL contains the following changes:

- Change type mapping highlighted in Project Settings.
- The ability for users to disable telemetry.

## SSMA v7.7

The v7.7 release of SSMA for MySQL contains the following changes:

- SSMA for MySQL has been enhanced with targeted fixes that improve quality and conversion metrics.
- Based on the popular demand, the 32-bit version of SSMA for MySQL is back. Compared to the previous implementation (before v7.4), there are two installer packages, but they can't be installed side by side. As a result, you must choose the most appropriate version based on the connectivity components you have. It's always preferable to use the 64-bit version, if possible.
- SSMA for MySQL now has ODBC Connection String connection mode, which allows you to use any third-party ODBC drivers that are compatible with MySQL.

## SSMA v7.6

The v7.6 release of SSMA for MySQL has been enhanced with targeted fixes that improve quality and conversion metrics and with support for [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] (public preview). Support for [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] on Windows and Linux is in public preview and shouldn't be used for production migrations.

## SSMA v7.5

The v7.5 release of SSMA for MySQL has been enhanced with several improvements to ensure greater accessibility for people with disabilities.

## SSMA v7.4

The v7.4 release of SSMA for MySQL contains the following changes:

- The **Query timeout** option is now available during schema object discovery at source and target.

  :::image type="content" source="../media/query-timeout_red.png" alt-text="Screenshot of Query timeout option.":::

- The quality and conversion metric has been improved with targeted fixes, based on customer feedback.

> [!IMPORTANT]  
> .NET 4.5.2 is a prerequisite for installing SSMA v7.4. In addition, beginning with v7.4, the 32-bit version of SSMA is being discontinued.

## SSMA v7.3

The v7.3 release of SSMA for MySQL contains the following changes:

- Improved quality and conversion metric with targeted fixes based on customer feedback.

- SSMA extensibility framework exposed via the following items:

  - Export functionality to a SQL Server Data Tools (SSDT) project.

    - You can now export schema scripts from SSMA to an SSDT project. You can use the schema scripts to make extra schema changes and deploy your database.

      :::image type="content" source="../media/export-schema-scripts_red.png" alt-text="Screenshot of Save as SSDT project command.":::

  - Libraries that SSMA consumes for performing custom conversions.

    - You can now construct code that can handle custom syntax conversions and conversions that weren't previously handled by SSMA.

      - For more information on how to construct a custom converter, see [Extending SQL Server Migration Assistant's conversion capabilities](https://techcommunity.microsoft.com/t5/microsoft-data-migration-blog/extending-sql-server-migration-assistant-s-conversion/ba-p/1004181).

      - You can also [download a sample project for conversion](https://techcommunity.microsoft.com/gxcuf89792/attachments/gxcuf89792/MicrosoftDataMigration/127/1/SsmaForOracleConversionSample.zip) from the same blog post.

## SSMA v7.2

The v7.2 release of SSMA for MySQL contains the following changes:

- Improved quality and conversion metric with targeted fixes based on customer feedback.
- Telemetry enhancements to provide better data points to troubleshoot customer issues and improve SSMA's conversion rates.

## SSMA v7.1

The v7.1 release of SSMA for MySQL contains the following changes:

- [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] on Windows and Linux CTP 1 is now a supported target platform for migration. This feature is in technical preview and allows schema and data movement to target SQL Server instances.
- SSMA now supports automatic updates to download the latest version of SSMA as soon as it's available.
- SSMA installable binaries are now delivered through Windows Installer package files (`.msi`).

## May 2016

The May 2016 release of SSMA for MySQL contains  the following changes:

- Added support for [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)].
- Improved parser and resolver.
- Removed installer check for .NET 2.0.
- Updated Extension Pack dependency from .NET 3.5 to .NET 4.0.
- Fixed default BigInt type mapping for MySQL.
- Fixed `save-project` and `open-project` commands for SSMA Console.
- Fixed `securepassword` command for SSMA Console.
- Fixed counting of objects for initial loading.
- Fixed MsSql objects loading.
- Fixed bug in global settings.

## March 2016

The March 2016 preview release of SSMA for MySQL adds support for migration to [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)].

## January 2016

The January 2016 maintenance release of SSMA for MySQL contains the following changes:

- Added View Log Menu Item to SSMA (RFC 5706203).
- Added Telemetry.

## July 2014

The July 2014 release of SSMA for MySQL contains the following changes:

- Improved Azure SQL Database code conversion.
- Extension pack functionality moved to schema to support Azure SQL Database.
- Performance improvements tested for databases with over 10k objects.
- UI improvements for dealing with large number of objects.
- Highlighting of well-known LOB schemas (so they can be ignored in conversion).
- Conversion speed improvements.
- Show object counts in UI.
- Report size reduction by more than 25%.
- Improved error messages for unparsed constructs.

## April 2014

The April 2014 release of SSMA for MySQL contains the following changes:

- Added support for [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)].
- Fixed bugs regarding conversion to Azure.
- Fixed bugs regarding invisible report pages in IE 10.

## July 2011

The July 2011 release of SSMA for MySQL contains the following changes:

- Support for conversion of `LIMIT` to [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] `OFFSET`.
- Improved error reporting during data migration.

## April 2011

The April 2011 release of SSMA for MySQL contains the following changes:

- Single installable of SSMA for MySQL, which supports [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)], [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)], [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and Azure SQL.
- The ability to connect [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)].
- Enhanced client-side data migration engine, supporting parallel migration of data.
- Improved data migration performance with Simple and Bulk logged recovery models.
- SSMA for MySQL Console version supports backward compatibility. You can open the projects created by versions earlier to SSMA v5.0.
- SSMA for MySQL v5.0 product can be installed side by side (SxS) with older versions of SSMA Product.

## July 2010

The July 2010 release of SSMA for MySQL contains the following features:

**1. Improvements to User Interface:**

- 'SQL Modes' Tab for MySQL Database objects
- 'Settings' Tab for MySQL Database objects
- 'Data' Tab for MySQL Tables
- Updated Project Settings in Conversion and Migration Pages
- 'Data Migration Settings' at Table level

**2. Improvements to Connect to MySQL and SQL Server:**

- SSL/TLS connectivity in MySQL
- Encrypted connectivity in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]

**3. Improvements to MySQL Metabase Explorer:**

- Loading all the MySQL Database Objects and their respective Tabs.

**4. Improvements to Object Conversion:**

- Conversion of MySQL metabase objects - procedures, functions, views, triggers, and statements.
- Limited support for spatial data types in tables.
- Option to convert MySQL functions to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] stored procedures
- Option to apply SQL modes and charset mapping during object conversion

**5. Improvements to Data Migration:**

- Support for Data Migration using both Server-Side and Client-Side Data Migration Engines
- Support for Spatial Data Migration
- Custom SQL for Data Migration for Tables

**6. SSMA for MySQL Console:**

- Support Console Feature for SSMA for MySQL
- Support for Script-Level Interfacing

## January 2010

The January 2010 release of SSMA for MySQL was the initial release. It contained the following features:

- Added support for migration to both on-premises [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and Azure SQL.
- **Feature Snapshot:** Schema and Data migration of MySQL Tables/Indexes/Constraints.
