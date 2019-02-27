---
title: "What's New in SSMA for Access(AccessToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "02/27/2019"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: a24d3fc0-6911-4bfa-828a-197abf222e02
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# What's New in SSMA for Access (AccessToSQL)
This article lists SQL Server Migration Assistant (SSMA) for Access changes in each release.  

## SSMA v8.0
The v8.0 release of SSMA for Access has been enhanced to provide targeted fixes designed to improve quality and conversion metrics. This release also offers the following new features:

* Support for **Azure SQL Database Managed Instance** as a target. You can now create new projects targeting Azure SQL Database Managed Instance:

  ![SQL DB MI project](../media/ssma-newproject-sqldbmi.png)

*	Post-conversion **Fix advisor**. Learn more about it [here](https://blogs.msdn.microsoft.com/datamigration/2019/02/17/%20accelerate-your-oracle-migrations-with-new-machine-learning-capabilities-in-ssma/).

* Preliminary database/schema selection.

    When connecting to the source, the user can now select databases/schemas of interest. Selecting only the schemas that you plan to migrate will save time during initial connection and improve overall SSMA performance.

    ![SSMA filter objects](../media/ssma-filter-objects.png)

> [!IMPORTANT]
> With SSMA v7.4 and later versions, .Net 4.5.2 is an installation pre-requisite.


## SSMA v7.10
The v7.10 release of SSMA for Access has been enhanced with targeted fixes designed to provide additional security and privacy protections to meet changes in global requirements.

> [!IMPORTANT]
> With SSMA v7.4 and later versions, .Net 4.5.2 is an installation pre-requisite.

## SSMA v7.9
The v7.9 release of SSMA for Access contains the following changes:
- Targeted fixes that improve quality and conversion metrics.
- Support in SSMA command line to alter Data Type mapping and Project Preferences.
- The Azure SQL Database connection dialog in SSMA has also been altered to specify the fully qualified server name. In previous versions of SSMA, the Azure SQL Database prefix had to be explicitly mentioned inside projects settings.

> [!IMPORTANT]
> With SSMA v7.4 and later versions, .Net 4.5.2 is an installation pre-requisite.

## SSMA v7.8
The v7.8 release of SSMA for Access contains the following changes:
- Highlighted change type mapping in Project Settings.
- Provided the ability for users to disable telemetry.

> [!IMPORTANT]
> With SSMA v7.4 and later versions, .Net 4.5.2 is an installation pre-requisite.

## SSMA v7.7
The v7.7 release of SSMA for Access contains the following changes:
- SSMA for Access has been enhanced with targeted fixes that improve quality and conversion metrics.
- Based on the popular demand, the 32-bit version of SSMA for Access is back. Compared to the previous implementation (prior to v7.4), there are two installer packages, but they cannot be installed side by side. As a result, you must choose the most appropriate version based on the connectivity components you have. It's always preferable to use the 64-bit version, if possible.

> [!IMPORTANT]
> With SSMA v7.4 and later versions, .Net 4.5.2 is an installation pre-requisite.

## SSMA v7.6
The v7.6 release of SSMA for Access has been enhanced with targeted fixes that improve quality and conversion metrics and with support for SQL Server 2017 (public preview). Support for SQL Server 2017 on Windows and Linux is in public preview and should not be used for production migrations.

> [!IMPORTANT]
> With SSMA v7.4 and later versions, .Net 4.5.2 is an installation pre-requisite, and the 32-bit version of the tool has been discontinued.

## SSMA v7.5
The v7.5 release of SSMA for Access has been enhanced with several improvements to ensure greater accessibility for people with disabilities.

> [!IMPORTANT]
> .Net 4.5.2 is a pre-requisite for installing SSMA v7.5. In addition, beginning with v7.4, the 32-bit version of SSMA is being discontinued.

## SSMA v7.4
The v7.4 release of SSMA for Access contains the following changes:
- The **Query timeout** option is now available during schema object discovery at source and target.
![query timeout option](../media/query-timeout_red.png)

- The quality and conversion metric has been improved with targeted fixes, based on customer feedback.

> [!IMPORTANT]
> .Net 4.5.2 is a pre-requisite for installing SSMA v7.4. In addition, beginning with v7.4, the 32-bit version of SSMA is being discontinued.

## SSMA v7.3
The v7.3 release of SSMA for Access contains the following changes:
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
The v7.2 release of SSMA for Access contains the following changes:
- Improved quality and conversion metric with targeted fixes based on customer feedback.
- Telemetry enhancements to provide better data points to troubleshoot customer issues and improve SSMA's conversion rates.

## SSMA v7.1
The v7.1 release of SSMA for Access contains the following changes:
- SQL Server 2017 on Windows and Linux CTP1 is now a supported target platform for migration. This feature is in technical preview and supports schema and data movement to target SQL servers.
- SSMA now supports automatic updates to download the latest version of SSMA as soon as it's available.
- SSMA installable binaries are now delivered through Windows installer package files (.msi).

**Resources**

[Extending SQL Server Migration Assistant's conversion capabilities](https://blogs.msdn.microsoft.com/datamigration/2017/02/21/2185/)
  
## May 2016  
The May 2016  release of SSMA for Access contains  the following changes:  
  
-  Added official support for SQL Server 2016
-  Removed installer check for .Net 2.0.
-  Fixed "save project" and "open project" commands for SSMA Console.
-  Fixed "securepassword" command for SSMA Console.
-  Fixed counting of objects for initial loading.
-  Fixed tables data loading for UI tabs for Access.
-  Fixed bug in global settings. 
   
## March 2016  
The March 2016 preview release of SSMA for Access contains  the following changes:  
  
-  Added support for migration to SQL Server 2016.  
   
## January 2016  
The January 2016 maintenance release of SSMA for Access contains the following changes:  
  
-  Fixed invalid function for default of a GUID field (RFC 3894811).  
-  Fixed hang on importing records to SQL Database (Azure) (RFC 4919573).  
-  Added View Log Menu Item to SSMA (RFC 5706203).  
-  Added Telemetry.  
  
## July 2014  
The July 2014 release of SSMA for Access contains the following changes:  
  
-   Improved Azure SQL DB code conversion.  
-   Moved extension pack functionality to schema to support Azure SQL DB.  
-   Tested performance improvements for databases with over 10k objects.  
-   Added UI improvements for dealing with large number of objects.  
-   Added support for highlighting of "well known" LOB schemas (so they can be ignored in conversion).  
-   Added conversion speed improvements.
-   Added support for showing object counts in UI.
-   Reduced report size by more than 25%.
-   Improved error messages for unparsed constructs.  
  
## April 2014  
The April 2014 release of SSMA for Access contains the following changes:  
  
-   Added support for MS SQL Server 2014.  
-   Fixed bugs regarding conversion to Azure.  
-   Fixed bugs regarding invisible report pages in IE 10.  
  
## January 2012  
The January 2012 release of SSMA for Access contains the following changes:  
  
-   Provided the option to not persist username and password for MS Access linked tables after migration.  
-   Set cascade actions for circular references to No Action.  
-   Provided proper messages indicating cascade actions for circular references have been set to No Action.  
  
## July 2011  
The July 2011 release of SSMA for Access contains the following changes:  
  
-   Improved error reporting during data migration.  
  
## April 2011  
The April 2011 release of SSMA for Access contains the following changes:  
  
-   Added a single installable of "SSMA for Access", which supports [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2005, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2008, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] "Denali" and Azure SQL.  
-   Added the ability to connect [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] "Denali."  
-   Added SSMA for Access Console version support for backward compatibility. You can open the projects created by versions earlier to SSMA v5.0.
-   Added the ability to install SSMA v5.0 product side by side (SxS) with older versions of SSMA Product.  
  
## July 2010  
The July 2010 release of SSMA for Access contains the following changes:  
  
-   Added support for migrating to SQL Server 2008 R2 and Azure SQL.
-   Added a secure connection to both SQL Server and Azure SQL.  
-   Added support for Access 2010 databases.
-   Added a new SSMA Console application for command-line execution.
-   Added support for the SQL Server DateTime2 data type.
  
## June 2008  
The June 2008 release of SSMA for Access contains the following changes:  
  
-   Added support for Access 2007 databases.  
  
## May 2007  
The May 2007 release of SSMA for Access contains the following changes:  
  
-   Added support for Access databases that use workgroup policies.  
-   Provided the ability to delete converted objects from the SQL Server metadata explorer.  
-   Added support for user-entered comments in the SQL Server formatted SQL mode.  
-   Added improvements in object conversion.  
  
## November 2006  
The November 2006 release of SSMA for Access contains the following changes:  
  
-   Added a  new Database Migration Wizard that guides you through the migration of a single database from Access to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
-   Added a new Convert, Load, and Migrate command that converts Access databases, loads the converted objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and migrates data into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] all in one step.  
-   Improved query migration. Query migration now converts more SELECT queries to views. For more information, see [Converting Access Database Objects](converting-access-database-objects-accesstosql.md).  
-   Added the ability to edit table and index properties on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **Table** tab.  
-   Added new global settings:  
    -   You can opt to show line numbers in editor windows.  
    -   You can configure SSMA to prompt to replace duplicate objects, or always or never replace duplicate objects during schema conversion.  
-   Added a new conversion option that lets you specify whether SSMA displays a warning when a complex query contains a wildcard.  
  
## July 2006  
The July 2006 release of SSMA for Access was the initial release.
