---
title: "What&#39;s New in SSMA  for Sybase (SybaseToSQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "sql-ssma"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "Azure SQL Database"
  - "SQL Server"
ms.assetid: 2be0cf8d-6dbe-443a-abbd-036249922205
caps.latest.revision: 21
author: "sabotta"
ms.author: "carlasab"
manager: "lonnyb"
---
# What&#39;s New in SSMA  for Sybase (SybaseToSQL)
This topic lists SSMA for Sybase changes in each release.  

## May 2016  
The May  2016 release of SSMA for Sybase contains  the following changes:  

1.  Added support for SQL Server 2016
2.  Removed installer check for .Net 2.0
3.  Updated Extension Pack dependency from .Net 3.5 to .Net 4.0
4.  Fixed "save project" and "open project" commands for SSMA Console
5.  Fixed "securepassword" command for SSMA Console
6.  Fixed counting of objects for initial loading
7.  Fixed bug in global settings

## March 2016  
The March 2016 preview release of SSMA for Sybase contains  the following changes:  
  
1.  Support migration to SQL Server 2016  
  
## January 2016  
The January 2016 maintenance release of SSMA for Sybase contains the following changes:  
  
1.  Added View Log Menu Item to SSMA (RFC 5706203)  
  
2.  Added Telemetry  
  
## July 2014  
The July 2014 release of SSMA for Sybase contains the following changes:  
  
1.  Improved Azure SQL DB code conversion  
  
2.  Extension pack functionality moved to schema to support Azure SQL DB  
  
3.  Performance improvements tested for databases with over 10k objects  
  
4.  UI improvements for dealing with large number of objects  
  
5.  Highlighting of “well known” LOB schemas (so they can be ignored in conversion)  
  
6.  Conversion speed improvements  
  
7.  Show object counts in UI  
  
8.  Report size reduction by more than 25%  
  
9. Improved error messages for unparsed constructs.  
  
## April 2014  
The April 2014 release of SSMA for Sybase contains the following changes:  
  
-   Added support of MS SQL Server 2014.  
  
-   Fixed bugs regarding conversion to Azure  
  
-   Fixed bugs regarding invisible report pages in IE 10.  
  
## January 2012  
The January 2012 release of SSMA for Sybase contains the following changes:  
  
-   Support for rollback trigger conversion.  
  
-   Provided fix for converting @@ROWCOUNT and @@ERROR in the same SET statement.  
  
## July 2011  
The July 2011 release of SSMA for Sybase contains the following changes:  
  
-   Improved error reporting during data migration.  
  
## April 2011  
The April 2011 release of SSMA for Sybase contains the following changes:  
  
-   Consolidated “SSMA for Sybase” product, which supports [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] 2005, [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] 2008, [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] “Denali” and SQL Azure.  
  
-   Support for connecting and migrating to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] “Denali”.  
  
-   Brand new feature to convert and migrate Sybase databases to SQL Azure.  
  
-   Enhanced client side data migration engine, supporting parallel migration of data.  
  
-   Improved data migration performance with Simple and Bulk logged recovery models.  
  
-   Case sensitive Sybase databases can be properly converted and migrated to case sensitive SQL Server.  
  
-   Support for conversion of Sybase ASE Non-ANSI join statements to SQL Server ANSI join statements has been extended to DELETE and UPDATE statements.  
  
-   Additional connectivity options for connecting to Sybase ASE servers using Sybase ASE ODBC provider and Sybase ASE ADO.Net providers.  
  
-   Removal of dependency on a separate database called **SysDB** which contains the Sybase emulation functions (installed as part of Extension Pack).  
  
-   SSMA for Sybase Extension Pack can now be installed on [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] clusters.  
  
-   Backward compatibility of projects created by earlier versions of SSMA (v4.0 and v4.2).  
  
-   SSMA for Sybase v5.0 product can be installed side by side (SxS) with older versions of SSMA (v4.0 and v4.2).  
  
## July 2010  
The July 2010 release of SSMA for Sybase contains the following changes:  
  
-   Support for migrating to SQL Server 2008 R2  
  
-   New SSMA Console application for command line execution  
  
-   Support for Data Migration using both Server Side and Client Side Data Migration Engines  
  
-   Support for “Custom SELECT” statement in data migration  
  
-   Support for migrating from Sybase ASE 15.0.3 and 15.5  
  
## June 2008  
The June 2008 release of SSMA for Sybase contains the following changes:  
  
-   SSMA Tester added, it automatically tests the database object conversion and the data migration made by SSMA. After all SSMA migration steps are finished, use SSMA Tester to verify that converted objects work the same way and that all data was transferred properly.  
  
-   Pre-SQL conversion added. User now can specify temporary tables (and other objects) declarations for each source procedure to be used in conversion.  
  
-   Improvements in object conversion:  
  
    -   Joins conversion revised.  
  
    -   Aggregates and non-aggregates without having/group by clauses.  
  
    -   The IDENTITY function with a SELECT INTO statement.  
  
    -   Clustered constraints and indexes on data-only-locked.  
  
    -   Temporary tables created by SELECT INTO.  
  
    -   Constraints / Indexes for temporary tables.  
  
    -   New [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] 2008 datetime types are supported.  
  
    -   Sybase 15.0 connectivity and datatypes support.  
  
## May 2007  
The May 2007 release of SSMA for Sybase contains the following changes:  
  
-   When you save a project, loading database content is faster.  
  
-   Support for user-entered comments the SQL Server formatted SQL mode.  
  
-   Improvements in object conversion.  
  
Note that the Help file was not updated for this release. For more information, see the Documentation Notes section later in this topic.  
  
## November 2006  
The November 2006 release of SSMA for Sybase contains the following changes:  
  
-   New global settings:  
  
    -   You can opt to show line numbers in editor windows.  
  
    -   You can configure SSMA to prompt to replace duplicate objects, or always or never replace duplicate objects during schema conversion.  
  
-   New conversion options let you configure how SSMA handles the following situations:  
  
    -   A CAST or CONVERT statement that contains a binary string  
  
    -   Checks for null values in equality expressions  
  
    -   Proxy tables  
  
    -   User message error numbers for RAISERROR  
  
    -   UPDATE statements that contain unresolved identifiers  
  
-   A new migration option lets you specify how SSMA should handle dates that are outside the [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] date range.  
  
-   A **Formatted SQL** setting on the **SQL** tab, which formats the code for improved readability.  
  
-   Bug fixes that include the following:  
  
    -   SSMA now converts LOCK TABLE *table* IN { SHARED | EXCLUSIVE } MODE statements by adding a TABLOCK or TABLOCKX hint to the subsequent SELECT query on the table.  
  
    -   The necessary casts are now added when binary types are used in character expressions.  
  
    -   Memory and performance improvements.  
  
## July 2006  
The July 2006 release of SSMA for Sybase was the initial release.  
  
## See Also  
[Getting Started with SSMA for Sybase &#40;SybaseToSQL&#41;](../../ssma/sybase/getting-started-with-ssma-for-sybase-sybasetosql.md)  
  
