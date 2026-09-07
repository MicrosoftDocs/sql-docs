---
title: "What's New in SSMA for Access (AccessToSQL)"
description: Find out about changes to SQL Server Migration Assistant (SSMA) for Access (AccessToSQL) for each release.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: niball, subasak
ms.date: 07/24/2026
ms.service: sql
ms.subservice: ssma
ms.topic: whats-new
ms.collection:
  - sql-migration-content
ms.custom:
  - intro-whats-new
---
# What's new in SSMA for Access (AccessToSQL)

This article lists SQL Server Migration Assistant (SSMA) for Access changes in each release.

[!INCLUDE [entra-id](../../includes/entra-id-hard-coded.md)]

## SSMA v10.6

The v10.6 release of SSMA for Access contains the following changes:

- Important security, compliance, UX, and accessibility fixes.

## SSMA v10.5

The v10.5 release of SSMA for Access contains the following changes:

- Resolved Windows authentication issues for Access.

- Important security, compliance, UX, and accessibility fixes.

## SSMA v10.4

The v10.4 release of SSMA for Access contains the following changes:

- Support for assessment and migration of your project to [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

- Important security, compliance, UX, and accessibility fixes.

## SSMA v10.3

The v10.3 release of SSMA for Access contains important security, compliance, and accessibility fixes.

## SSMA v10.2

The v10.2 release of SSMA for Access contains minor performance improvements and bug fixes.

## SSMA v10.1

The v10.1 release of SSMA for Access contains minor performance improvements and bug fixes.

## SSMA v10.0

The v10.0 release of SSMA for Access contains minor performance improvements, bug fixes, and an enhanced help experience.

## SSMA v9.5

The v9.5 release of SSMA for Access contains the following change:

- Support for exclude schema in SSMA Console mode.

## SSMA v9.4

The v9.4 release of SSMA for Access contains minor performance improvements, bug fixes, and an enhanced help experience.

## SSMA v9.3

The v9.3 release of SSMA for Access contains the following change:

- Added support for SQL Server 2022 target.

## SSMA v9.2

The v9.3 release of SSMA for Access contains the following change:

- Added support for DECIMAL type for Office 365 version 2209 and later versions.

## SSMA v9.1

The v9.1 release of SSMA for Access contains minor performance improvements, bug fixes, and an enhanced get help experience.

## SSMA v9.0

The v9.0 release of SSMA for Access contains minor performance improvements, bug fixes, and online help viewer support.

## SSMA v8.24

The v8.24 release of SSMA for Access includes minor performance improvements and bug fixes.

## SSMA v8.23

The v8.23 release of SSMA for Access includes the following changes:

- Enhanced conversion for tables without primary key.
- Enhanced XML output when running in console mode.
- New **Feedback** link in reports to send feedback.
- Improved visualization experience in reports.

## SSMA v8.22

The v8.22 release of SSMA for Access includes the following changes:

- Extra connection string options for target database connection.
- Summary charts in assessment reports.
- Improved messages navigation in assessment reports.

## SSMA v8.21

The v8.21 release of SSMA for Access includes the following change:

- Use `COUNT_BIG` in row count queries for the target database.

## SSMA v8.20

The v8.20 release of SSMA for Access includes the following changes:

- Minor performance improvements and bug fixes

## SSMA v8.19

The v8.19 release of SSMA for Access includes the following changes:

- Minor performance improvements and bug fixes

## SSMA v8.18

The v8.18 release of SSMA for Access includes the following changes:

- Minor performance improvements and bug fixes

## SSMA v8.17

The v8.17 release of SSMA for Access includes the following changes:

- Update HTML assessment reports to use modern editor to display SQL text

## SSMA v8.16

The v8.16 release of SSMA for Access contains the following changes:

- Shows SQL text for queries in the HTML conversion report.
- Removes support for legacy parser.
- Fixes an issue with objects not refreshing from database.

## SSMA v8.15

In addition to several accessibility improvements, the v8.15 release of SSMA for Access contains the following changes:

- Ignores indexes autocreated for foreign keys.
- Revamps assessment reports to work in modern browsers.
- Uses authority provided by the database for Microsoft Entra authentication.
- Improves naming for statements loaded from files.

## SSMA v8.14

In addition to several improvements to ensure greater accessibility for people with disabilities, the v8.14 release of SSMA for Access requires a project upgrade, as it now stores full source and target server version in the project metadata.

## SSMA v8.13

The v8.13 release of SSMA for Access contains the following changes:

- Fixes `ORDER BY` conversion with `UNION` clause.
- Supports filtered unique indexes.
- Considers implicit type casts when converting procedure and function calls.

## SSMA v8.12

The v8.12 release of SSMA for Access contains the following changes:

- Support for `BigInt` (`Large Number`) data type.
- Improved column type resolution.
- Improved conversion of column validation rules.
- Uses latest available ACE OLE DB provider for data migration.

## SSMA v8.11

The v8.11 release of SSMA for Access contains the following change:

- Uses the MSAL.NET library for interactive Microsoft Entra authentication.

## SSMA v8.10

The v8.10 release of SSMA for Access contains minor performance improvements and bug fixes.

## SSMA v8.9

The v8.9 release of SSMA for Access contains the following changes:

- Improved conversion for self-referencing queries.
- Fix for the issue with special characters in project name.

## SSMA v8.8

The v8.8 release of SSMA for Access includes:

- Stability improvements for SQL Server objects synchronization
- GUI performance improvements during assessment and conversion
- A new Access syntax parser that improves conversion performance

## SSMA v8.7

The v8.7 release of SSMA for Access improves conversion for the `IIF` function in queries. It also includes minor fixes and performance improvements in the graphical user interface.

> [!IMPORTANT]  
> Starting with SSMA v8.5, .NET 4.7.2 is a prerequisite for installation. If you need to install this version, you can [download the runtime](https://dotnet.microsoft.com/download/dotnet-framework/net472).

## SSMA v8.6

In addition to a targeted set of fixes that improve usability and performance, the v8.6 release of SSMA for Access adds a setting that enables users to omit SSMA extended properties in the converted code.

To use this setting, in SSMA for Access, go to **Tools** > **Project Settings** > **General** > **Conversion**. Under **Misc**, set the **Omit Extended Properties** setting to **Yes**.

:::image type="content" source="media/ssma-omit-extended-properties.png" alt-text="Screenshot of Omit Extended Properties setting." lightbox="media/ssma-omit-extended-properties.png":::

> [!IMPORTANT]  
> Starting with SSMA v8.5, .NET 4.7.2 is a prerequisite for installation. If you need to install this version, you can [download the runtime](https://dotnet.microsoft.com/download/dotnet-framework/net472).

## SSMA v8.5

The v8.5 release of SSMA for Access adds support for Microsoft Entra authentication. It also adds basic support for JSON features in SQL Server. This version includes a targeted set of fixes that improve usability and performance.

In addition, SSMA for Access now supports conversion of multiple standard functions (`ISNULL`, `IIF`, and others).

> [!IMPORTANT]  
> SSMA v8.5 requires .NET 4.7.2. If you need to install this version, you can [download the runtime](https://dotnet.microsoft.com/download/dotnet-framework/net472).

## SSMA v8.4

The v8.4 release of SSMA for Access includes targeted fixes that address accessibility problems. It also fixes a bug related to max index columns (to allow 32 instead of 16) for SQL Server 2016 and later versions.

> [!IMPORTANT]  
> SSMA versions 7.4 through 8.4 require .NET 4.5.2.

## SSMA v8.3

The v8.3 release of SSMA for Access includes targeted fixes that improve quality and conversion metrics. In addition, this release provides fixes that:

- Address accessibility problems.
- Add basic support for the **hierarchyid** type in SQL Server.

## SSMA v8.2

The v8.2 release of SSMA for Access includes targeted fixes that improve quality and conversion metrics.

> [!NOTE]  
> A known issue with autoupdate might cause the failure of an update from SSMA v8.1 to v8.2. If you encounter this error, download the new version and install it manually.

## SSMA v8.1

The v8.1 release of SSMA for Access includes targeted fixes that improve quality and conversion metrics.

> [!NOTE]  
> A known issue with autoupdate might cause an update from SSMA v8.0 to v8.1 to fail. If you encounter this error, download the new version and install it manually.

## SSMA v8.0

The v8.0 release of SSMA for Access includes targeted fixes that improve quality and conversion metrics. This release also offers the following new features:

- Support for **Azure SQL Managed Instance** as a target. You can now create new projects that target Azure SQL Managed Instance:

  :::image type="content" source="../media/ssma-newproject-managed-instance.png" alt-text="Screenshot of SQL MI project.":::

- Post-conversion **Fix advisor**. For more information, see [Accelerate your Oracle migrations with new machine learning capabilities in SSMA](https://techcommunity.microsoft.com/blog/microsoftdatamigration/accelerate-your-oracle-migrations-with-new-machine-learning-capabilities-in-ssma/368733).

- Preliminary database/schema selection.

  When you connect to the source, you can now select databases and schemas of interest. Selecting only the schemas that you plan to migrate saves time during initial connection and improves overall SSMA performance.

  :::image type="content" source="../media/ssma-filter-objects.png" alt-text="Screenshot of SSMA filter objects.":::

## SSMA v7.10

The v7.10 release of SSMA for Access includes targeted fixes that provide more security and privacy protections to meet changes in global requirements.

## SSMA v7.9

The v7.9 release of SSMA for Access contains the following changes:

- Targeted fixes that improve quality and conversion metrics.
- Support in SSMA command line to alter Data Type mapping and Project Preferences.
- The Azure SQL Database connection dialog in SSMA is updated to specify the fully qualified server name. In previous versions of SSMA, you had to explicitly mention the Azure SQL Database prefix inside projects settings.

## SSMA v7.8

The v7.8 release of SSMA for Access contains the following changes:

- Change type mapping highlighted in Project Settings.
- The ability for users to disable data collection.

## SSMA v7.7

The v7.7 release of SSMA for Access contains the following changes:

- Targeted fixes that improve quality and conversion metrics.
- Based on popular demand, the 32-bit version of SSMA for Access is back. Compared to the previous implementation (before v7.4), there are two installer packages, but you can't install them side by side. As a result, you must choose the most appropriate version based on the connectivity components you have. Always use the 64-bit version, if possible.

## SSMA v7.6

The v7.6 release of SSMA for Access includes targeted fixes that improve quality and conversion metrics and support for SQL Server 2017 (public preview). Support for SQL Server 2017 on Windows and Linux is in public preview and shouldn't be used for production migrations.

## SSMA v7.5

The v7.5 release of SSMA for Access includes several improvements to ensure greater accessibility for people with disabilities.

## SSMA v7.4

The v7.4 release of SSMA for Access contains the following changes:

- The **Query timeout** option is now available during schema object discovery at source and target.

  :::image type="content" source="../media/query-timeout-red.png" alt-text="Screenshot of query timeout option.":::

- The quality and conversion metric is improved with targeted fixes, based on customer feedback.

  > [!IMPORTANT]  
  > .NET 4.5.2 is a prerequisite for installing SSMA v7.4. In addition, starting with v7.4, the 32-bit version of SSMA is no longer available.

## SSMA v7.3

The v7.3 release of SSMA for Access contains the following changes:

- Improved quality and conversion metric with targeted fixes based on customer feedback.
- SSMA extensibility framework exposed through the following items:
  - Export functionality to a SQL Server Data Tools (SSDT) project.
    - You can now export schema scripts from SSMA to an SSDT project. Use the schema scripts to make further schema changes and deploy your database.

      :::image type="content" source="../media/export-schema-scripts-red.png" alt-text="Screenshot of Save as SSDT project command.":::

  - Libraries that SSMA can use to perform custom conversions.
    - You can now construct code that handles custom syntax conversions and conversions that SSMA didn't previously handle.
      - Instructions on how to construct a custom converter, along with a sample project for conversion, are available in the blog post [Extending SQL Server Migration Assistant's conversion capabilities](https://techcommunity.microsoft.com/blog/microsoftdatamigration/extending-sql-server-migration-assistants-conversion-capabilities/1004181).

## SSMA v7.2

The v7.2 release of SSMA for Access contains the following changes:

- Improved quality and conversion metric with targeted fixes based on customer feedback.
- Data collection enhancements to provide better data points to troubleshoot customer issues and improve SSMA's conversion rates.

## SSMA v7.1

The v7.1 release of SSMA for Access contains the following changes:

- SQL Server 2017 Preview on Windows and Linux is now a supported target platform for migration. This feature is in technical preview and supports schema and data movement to target SQL Server instances.
- SSMA now supports automatic updates to download the latest version of SSMA as soon as it's available.
- SSMA installable binaries are now delivered through Windows Installer package files (.msi).

## May 2016

The May 2016 release of SSMA for Access contains the following changes:

- Added official support for SQL Server 2016.
- Removed installer check for .NET 2.0.
- Fixed `save-project` and `open-project` commands for SSMA Console.
- Fixed `securepassword` command for SSMA Console.
- Fixed counting of objects for initial loading.
- Fixed tables data loading for UI tabs for Access.
- Fixed bug in global settings.

## March 2016

The March 2016 preview release of SSMA for Access adds support for migration to SQL Server 2016.

## January 2016

The January 2016 maintenance release of SSMA for Access contains the following changes:

- Fixed invalid function for default of a GUID field (RFC 3894811).
- Fixed issue where system stops responding when importing records to SQL Database (Azure) (RFC 4919573).
- Added View Log Menu Item to SSMA (RFC 5706203).
- Added data collection.

## July 2014

The July 2014 release of SSMA for Access contains the following changes:

- Improved Azure SQL Database code conversion.
- Moved extension pack functionality to schema to support Azure SQL Database.
- Tested performance improvements for databases with over 10,000 objects.
- Added UI improvements for dealing with large number of objects.
- Added support for highlighting of "well known" LOB schemas (so they can be ignored in conversion).
- Added conversion speed improvements.
- Added support for showing object counts in UI.
- Reduced report size by more than 25 percent.
- Improved error messages for unparsed constructs.

## April 2014

The April 2014 release of SSMA for Access includes the following changes:

- Added support for MS SQL Server 2014.
- Fixed bugs related to conversion to Azure.
- Fixed bugs related to invisible report pages in IE 10.

## January 2012

The January 2012 release of SSMA for Access includes the following changes:

- Provided the option to not persist username and password for MS Access linked tables after migration.
- Set cascade actions for circular references to **No Action**.
- Provided proper messages indicating cascade actions for circular references are set to **No Action**.

## July 2011

The July 2011 release of SSMA for Access adds improved error reporting during data migration.

## April 2011

The April 2011 release of SSMA for Access includes the following changes:

- Added a single installable of **SSMA for Access**, which supports [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, and Azure SQL.
- Added the ability to connect to [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)].
- Added SSMA for Access Console version support for backward compatibility. You can open the projects created by versions earlier to SSMA v5.0.
- Added the ability to install SSMA v5.0 product side by side (SxS) with older versions of SSMA Product.

## July 2010

The July 2010 release of SSMA for Access includes the following changes:

- Support for migrating to SQL Server 2008 R2 and Azure SQL.
- A secure connection to both SQL Server and Azure SQL.
- Support for Access 2010 databases.
- A new SSMA Console application for command-line execution.
- Support for the SQL Server `DateTime2` data type.

## June 2008

The June 2008 release of SSMA for Access adds support for Access 2007 databases.

## May 2007

The May 2007 release of SSMA for Access includes the following changes:

- Support for Access databases that use workgroup policies.
- The ability to delete converted objects from the SQL Server metadata explorer.
- Support for user-entered comments in the SQL Server formatted SQL mode.
- Improvements in object conversion.

## November 2006

The November 2006 release of SSMA for Access contains the following changes:

- Added a new Database Migration Wizard that guides you through the migration of a single database from Access to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].
- Added a new Convert, Load, and Migrate command that converts Access databases, loads the converted objects into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], and migrates data into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] all in one step.
- Improved query migration. Query migration now converts more `SELECT` queries to views. For more information, see [Convert Access database objects](converting-access-database-objects-accesstosql.md).
- Added the ability to edit table and index properties on the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] **Table** tab.
- Added new global settings:
  - You can opt to show line numbers in editor windows.
  - You can configure SSMA to prompt to replace duplicate objects, or always or never replace duplicate objects during schema conversion.
- Added a new conversion option that lets you specify whether SSMA displays a warning when a complex query contains a wildcard.

## July 2006

The July 2006 release of SSMA for Access is the initial release.
