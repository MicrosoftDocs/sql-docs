---
title: "What's New in SSMA for Access (AccessToSQL) | Microsoft Docs"
description: Find out about changes to SQL Server Migration Assistant (SSMA) for Access (AccessToSQL) for each release.
author: cpichuka
ms.service: sql
ms.custom:
  - intro-whats-new
ms.date: "04/29/2021"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.author: cpichuka
---
# What's New in SSMA for Access (AccessToSQL)

This article lists SQL Server Migration Assistant (SSMA) for Access changes in each release.

## SSMA v9.2

The v9.2 release of SSMA for Access contains the following changes:

* Support for DECIMAL type for Office 365 version 2209 and above.

## SSMA v9.1

The v9.1 release of SSMA for Access contains minor performance improvements,bug fixes and enhanced get help experience.

## SSMA v9.0

The v9.0 release of SSMA for Access contains minor performance improvements, bug fixes and online help viewer support.

## SSMA v8.24

The v8.24 release of SSMA for Access contains minor performance improvements and bug fixes.

## SSMA v8.23

The v8.23 release of SSMA for Access contains the following changes:

* Enhanced conversion for tables without primary key
* Enhanced XML output when running in console mode
* New "Feedback" link in reports to send feedback
* In reports, improved visualization experience

## SSMA v8.22

The v8.22 release of SSMA for Access contains the following changes:

* Allow to specify additional connection string options for target database connection
* Introduce summary charts in assessment reports
* Improve messages navigation in assessment reports

## SSMA v8.21

The v8.21 release of SSMA for Access contains the following changes:

* Use `COUNT_BIG` in row count queries for the target database

## SSMA v8.20

The v8.20 release of SSMA for Access contains the following changes:

* Minor performance improvements and bug fixes

## SSMA v8.19

The v8.19 release of SSMA for Access contains the following changes:

* Minor performance improvements and bug fixes

## SSMA v8.18

The v8.18 release of SSMA for Access contains the following changes:

* Minor performance improvements and bug fixes

## SSMA v8.17

The v8.17 release of SSMA for Access contains the following changes:

* Update HTML assessment reports to use modern editor to display SQL text

## SSMA v8.16

The v8.16 release of SSMA for Access contains the following changes:

* Show SQL text for queries in the HTML conversion report
* Remove support for legacy parser
* Fix issue with objects not refreshing from database

## SSMA v8.15

In addition to several accessibility improvements, the v8.15 release of SSMA for Access contains the following changes:

* Ignore indexes auto-created for foreign keys
* Revamp assessment reports to work in modern browsers
* Use authority provided by the database for Azure AD authentication
* Improve naming for statements loaded from files

## SSMA v8.14

In addition to several improvements to ensure greater accessibility for people with disabilities, the v8.14 release of SSMA for Access requires a project upgrade, as it now stores full source/target server version in the project metadata.

## SSMA v8.13

The v8.13 release of SSMA for Access contains the following changes:

* Fix `ORDER BY` conversion with `UNION` clause
* Support for filtered unique indexes
* Consider implicit type casts when converting procedure and function calls

## SSMA v8.12

The v8.12 release of SSMA for Access contains the following changes:

* Support for `BigInt` (`Large Number`) data type
* Improved column type resolution
* Improved conversion of column validation rules
* Using latest available ACE OLE DB provider for data migration

## SSMA v8.11

The v8.11 release of SSMA for Access contains the following changes:

* Use MSAL.NET library for interactive Azure Active Directory authentication

## SSMA v8.10

The v8.10 release of SSMA for Access contains minor performance improvements and bug fixes.

## SSMA v8.9

The v8.9 release of SSMA for Access contains the following changes:

* Improved conversion for self-referencing queries
* Fix for the issue with special characters in project name

## SSMA v8.8

The v8.8 release of SSMA for Access includes:

* SQL Server objects synchronization stability improvements
* GUI performance improvements during assessment and conversion
* Brand new Access syntax parser to further improve conversion performance

## SSMA v8.7

The v8.7 release of SSMA for Access has improved conversion for `IIF` function in queries, as well as minor fixes and performance improvements in graphical user interface.

> [!IMPORTANT]
> With SSMA v8.5 and later, .NET 4.7.2 is an installation pre-requisite. If you need to install this version, you can download the runtime file from [here](https://dotnet.microsoft.com/download/dotnet-framework/net472).

## SSMA v8.6

In addition to a targeted set of fixes designed to improve usability and performance, the v8.6 release of SSMA for Access has been enhanced by adding a setting that enables users to omit SSMA extended properties in the converted code.

To leverage this setting, in SSMA for Access, navigate to **Tools** > **Project Settings** > **General** > **Conversion**, and then under **Misc**, update the value of the **Omit Extended Properties** setting to **Yes**.

![Omit Extended Properties setting](../access/media/ssma-omit-extended-properties.png)

> [!IMPORTANT]
> With SSMA v8.5 and later, .NET 4.7.2 is an installation pre-requisite. If you need to install this version, you can download the runtime file from [here](https://dotnet.microsoft.com/download/dotnet-framework/net472).

## SSMA v8.5

The v8.5 release of SSMA for Access is enhanced with support for Azure Active Directory authentication and basic support for JSON features in SQL server, together with a targeted set of fixes designed to improve usability and performance.

In addition, SSMA for Access now supports conversion of multiple standard functions (`ISNULL`, `IIF`, etc.).

> [!IMPORTANT]
> With SSMA v8.5, .NET 4.7.2 is an installation pre-requisite. If you need to install this version, you can download the runtime file from [here](https://dotnet.microsoft.com/download/dotnet-framework/net472).

## SSMA v8.4

The v8.4 release of SSMA for Access is enhanced with targeted fixes that are designed to address accessibility issues and fix a bug related to max index columns (to allow 32 instead of 16) for SQL Server 2016 and later versions.

> [!IMPORTANT]
> With SSMA versions 7.4 though 8.4, .NET 4.5.2 is an installation pre-requisite.

## SSMA v8.3

The v8.3 release of SSMA for Access is enhanced with targeted fixes that are designed to improve quality and conversion metrics. In addition, this release of SSMA for Access provides fixes that:

* Address accessibility issues.
* Add basic support for `hierarchyid` type in SQL Server.

## SSMA v8.2

The v8.2 release of SSMA for Access is enhanced with targeted fixes that are designed to improve quality and conversion metrics.

> [!NOTE]
> A known issue with auto-update may cause the failure of an update from SSMA v8.1 to v8.2. If you encounter this error, please download the new version and install it manually.

## SSMA v8.1

The v8.1 release of SSMA for Access is enhanced with targeted fixes that are designed to improve quality and conversion metrics.

> [!NOTE]
> A known issue with auto-update may cause the failure of an update from SSMA v8.0 to v8.1. If you encounter this error, please download the new version and install it manually.

## SSMA v8.0

The v8.0 release of SSMA for Access is enhanced with targeted fixes designed to improve quality and conversion metrics. This release also offers the following new features:

* Support for **Azure SQL Managed Instance** as a target. You can now create new projects targeting Azure SQL Managed Instance:

  ![SQL MI project](../media/ssma-newproject-sqldbmi.png)

* Post-conversion **Fix advisor**. Learn more about it [here](https://techcommunity.microsoft.com/t5/Microsoft-Data-Migration/Accelerate-your-Oracle-migrations-with-new-machine-learning/ba-p/368733).

* Preliminary database/schema selection.

    When connecting to the source, the user can now select databases/schemas of interest. Selecting only the schemas that you plan to migrate will save time during initial connection and improve overall SSMA performance.

   ![SSMA filter objects](../media/ssma-filter-objects.png)

## SSMA v7.10

The v7.10 release of SSMA for Access is enhanced with targeted fixes designed to provide additional security and privacy protections to meet changes in global requirements.

## SSMA v7.9

The v7.9 release of SSMA for Access contains the following changes:

* Targeted fixes that improve quality and conversion metrics.
* Support in SSMA command line to alter Data Type mapping and Project Preferences.
* The Azure SQL Database connection dialog in SSMA has also been altered to specify the fully qualified server name. In previous versions of SSMA, the Azure SQL Database prefix had to be explicitly mentioned inside projects settings.

## SSMA v7.8

The v7.8 release of SSMA for Access contains the following changes:

* Change type mapping highlighted in Project Settings.
* The ability for users to disable telemetry.

## SSMA v7.7

The v7.7 release of SSMA for Access contains the following changes:

* Targeted fixes that improve quality and conversion metrics.
* Based on the popular demand, the 32-bit version of SSMA for Access is back. Compared to the previous implementation (before v7.4), there are two installer packages, but they can't be installed side by side. As a result, you must choose the most appropriate version based on the connectivity components you have. It's always preferable to use the 64-bit version, if possible.

## SSMA v7.6

The v7.6 release of SSMA for Access is enhanced with targeted fixes that improve quality and conversion metrics and with support for SQL Server 2017 (public preview). Support for SQL Server 2017 on Windows and Linux is in public preview and shouldn't be used for production migrations.

## SSMA v7.5

The v7.5 release of SSMA for Access is enhanced with several improvements to ensure greater accessibility for people with disabilities.

## SSMA v7.4

The v7.4 release of SSMA for Access contains the following changes:

* The **Query timeout** option is now available during schema object discovery at source and target.

  ![query timeout option](../media/query-timeout_red.png)

* The quality and conversion metric has been improved with targeted fixes, based on customer feedback.

  > [!IMPORTANT]
  > .NET 4.5.2 is a pre-requisite for installing SSMA v7.4. In addition, beginning with v7.4, the 32-bit version of SSMA has been discontinued.

## SSMA v7.3

The v7.3 release of SSMA for Access contains the following changes:

* Improved quality and conversion metric with targeted fixes based on customer feedback.
* SSMA extensibility framework exposed via the following items:
  * Export functionality to a SQL Server Data Tools (SSDT) project.
    * You can now export schema scripts from SSMA to an SSDT project. You can use the schema scripts to make additional schema changes and deploy your database.

        ![Save as SSDT project command](../media/export-schema-scripts_red.png)
  * Libraries that can be consumed by SSMA for performing custom conversions.
    * You can now construct code that can handle custom syntax conversions and conversions that weren't previously handled by SSMA.
      * Instructions on how to construct a custom converter, along with a sample project for conversion, are available in the blog post [Extending SQL Server Migration Assistant's conversion capabilities](https://techcommunity.microsoft.com/t5/Microsoft-Data-Migration/Extending-SQL-Server-Migration-Assistant-s-conversion/ba-p/1004181).

## SSMA v7.2

The v7.2 release of SSMA for Access contains the following changes:

* Improved quality and conversion metric with targeted fixes based on customer feedback.
* Telemetry enhancements to provide better data points to troubleshoot customer issues and improve SSMA's conversion rates.

## SSMA v7.1

The v7.1 release of SSMA for Access contains the following changes:

* SQL Server 2017 on Windows and Linux CTP1 is now a supported target platform for migration. This feature is in technical preview and supports schema and data movement to target SQL servers.
* SSMA now supports automatic updates to download the latest version of SSMA as soon as it's available.
* SSMA installable binaries are now delivered through Windows Installer package files (.msi).

## May 2016

The May 2016 release of SSMA for Access contains the following changes:

* Added official support for SQL Server 2016.
* Removed installer check for .NET 2.0.
* Fixed `save-project` and `open-project` commands for SSMA Console.
* Fixed `securepassword` command for SSMA Console.
* Fixed counting of objects for initial loading.
* Fixed tables data loading for UI tabs for Access.
* Fixed bug in global settings.

## March 2016

The March 2016 preview release of SSMA for Access adds support for migration to SQL Server 2016.

## January 2016

The January 2016 maintenance release of SSMA for Access contains the following changes:

* Fixed invalid function for default of a GUID field (RFC 3894811).
* Fixed issue where system stops responding when importing records to SQL Database (Azure) (RFC 4919573).
* Added View Log Menu Item to SSMA (RFC 5706203).
* Added Telemetry.

## July 2014

The July 2014 release of SSMA for Access contains the following changes:

* Improved Azure SQL Database code conversion.
* Moved extension pack functionality to schema to support Azure SQL Database.
* Tested performance improvements for databases with over 10k objects.
* Added UI improvements for dealing with large number of objects.
* Added support for highlighting of "well known" LOB schemas (so they can be ignored in conversion).
* Added conversion speed improvements.
* Added support for showing object counts in UI.
* Reduced report size by more than 25%.
* Improved error messages for unparsed constructs.

## April 2014

The April 2014 release of SSMA for Access contains the following changes:

* Added support for MS SQL Server 2014.
* Fixed bugs related to conversion to Azure.
* Fixed bugs related to invisible report pages in IE 10.

## January 2012

The January 2012 release of SSMA for Access contains the following changes:

* Provided the option to not persist username and password for MS Access linked tables after migration.
* Set cascade actions for circular references to No Action.
* Provided proper messages indicating cascade actions for circular references have been set to No Action.

## July 2011

The July 2011 release of SSMA for Access adds improved error reporting during data migration.

## April 2011

The April 2011 release of SSMA for Access contains the following changes:

* Added a single installable of "SSMA for Access", which supports [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)], [!INCLUDE [ssSQL10](../../includes/sssql10-md.md)], [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and Azure SQL.
* Added the ability to connect to [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)].
* Added SSMA for Access Console version support for backward compatibility. You can open the projects created by versions earlier to SSMA v5.0.
* Added the ability to install SSMA v5.0 product side by side (SxS) with older versions of SSMA Product.

## July 2010

The July 2010 release of SSMA for Access contains the following changes:

* Added support for migrating to SQL Server 2008 R2 and Azure SQL.
* Added a secure connection to both SQL Server and Azure SQL.
* Added support for Access 2010 databases.
* Added a new SSMA Console application for command-line execution.
* Added support for the SQL Server `DateTime2` data type.

## June 2008

The June 2008 release of SSMA for Access adds support for Access 2007 databases.

## May 2007

The May 2007 release of SSMA for Access contains the following changes:

* Added support for Access databases that use workgroup policies.
* Provided the ability to delete converted objects from the SQL Server metadata explorer.
* Added support for user-entered comments in the SQL Server formatted SQL mode.
* Added improvements in object conversion.

## November 2006

The November 2006 release of SSMA for Access contains the following changes:

* Added a new Database Migration Wizard that guides you through the migration of a single database from Access to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].
* Added a new Convert, Load, and Migrate command that converts Access databases, loads the converted objects into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], and migrates data into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] all in one step.
* Improved query migration. Query migration now converts more SELECT queries to views. For more information, see [Converting Access Database Objects](converting-access-database-objects-accesstosql.md).
* Added the ability to edit table and index properties on the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] **Table** tab.
* Added new global settings:
  * You can opt to show line numbers in editor windows.
  * You can configure SSMA to prompt to replace duplicate objects, or always or never replace duplicate objects during schema conversion.
* Added a new conversion option that lets you specify whether SSMA displays a warning when a complex query contains a wildcard.

## July 2006

The July 2006 release of SSMA for Access was the initial release.
