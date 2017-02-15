---
title: "What&#39;s New in SSMA  for Oracle (OracleToSQL) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "sql-ssma"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: f305ebb6-7393-4a43-abb3-6332b739d690
caps.latest.revision: 24
author: "sabotta"
ms.author: "carlasab"
manager: "v-thobro"
---
# What&#39;s New in SSMA  for Oracle (OracleToSQL)
This topic lists SSMA for Oracle changes in each release.  

## May 2016  
The May 2016 release of SSMA for Oracle contains the following changes:  

1.  Added support for SQL Server 2016
2.  Added conversion of Oracle flashback archive tables to SQL Server temporal tables
3.  Added conversion of Oracle VPD Policy converting to SQL Server Policy objects (Row Level Security for Oracle)
4.  Decreased time of initial loading for Oracle
5.  Improved parser and resolver
6.  Removed installer check for .Net 2.0
7.  Updated Extension Pack dependency from .Net 3.5 to .Net 4.0
8.  Fixed "save project" and "open project" commands for SSMA Console
9.  Fixed "securepassword" command for SSMA Console
10. Fixed counting of objects for initial loading
11. Fixed converting of character data types for Oracle
12. Fixed bug in global settings
  
## March 2016  
The March 2016 preview release of SSMA for Oracle contains the following changes:  
  
1.  Support migration to SQL Server 2016  
  
2.  Support for migrating Oracle Row Level Security (with some limitations)  
  
3.  Support for migrating Oracle in memory tables to SQL Server Column Store  
  
## January 2016  
The January 2014 Maintenance release of SSMA for Oracle contains the following changes:  
  
1.  Added support for Clustered Indexes  
  
2.  Fixed slow Oracle schema queries (RFC 5076207)  
  
3.  Fixed connect to Azure from console  
  
4.  Added View Log Menu Item to SSMA (RFC 5706203)  
  
5.  Added Telemetry  
  
## July 2014  
The July 2014 release of SSMA for Oracle contains the following changes:  
  
1.  Support for Azure SQL DB  
  
2.  Extension pack functionality moved to schema to support Azure SQL DB  
  
3.  Support for Oracle Materialized views  
  
4.  Support for SQL Server 2014 Memory optimized tables  
  
5.  Performance improvements tested for databases with over 10k objects  
  
6.  UI improvements for dealing with large number of objects  
  
7.  Highlighting of “well known” LOB schemas  
  
8.  Conversion speed improvements  
  
9. Show object counts in UI  
  
10. Report size reduction by more than 25%  
  
11. Improved error messages for unparsed constructs.  
  
## April 2014  
The April 2014 release of SSMA for Oracle contains the following changes:  
  
1.  Added support of MS SQL Server 2014.  
  
2.  Added support of Oracle 12 and query optimization.  
  
3.  Fixed bugs regarding conversion to Azure.  
  
4.  Fixed bugs regarding invisible report pages in IE 10.  
  
## January 2012  
The January 2012 release of SSMA for Oracle contains the following changes:  
  
-   Support RowType and RecordType input parameters defaulted to NULL.  
  
## July 2011  
The July 2011 release of SSMA for Oracle contains the following changes:  
  
-   Support for conversion of Oracle sequence to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] “Denali” sequence generator.  
  
-   Improved error reporting during data migration.  
  
-   Improved conversion of statement using reserved words.  
  
-   Improved implicit conversion of date value in a function.  
  
## April 2011  
The April 2011 release of SSMA for Oracle contains the following changes:  
  
-   Consolidated “SSMA for Oracle” product, which supports [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] 2005, [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] 2008 and [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] “Denali”.  
  
-   Support for connecting and migrating to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] “Denali”.  
  
-   Enhanced client side data migration engine, supporting parallel migration of data.  
  
-   Improved data migration performance with Simple and Bulk logged recovery models.  
  
-   Backward compatibility of projects created by earlier versions of SSMA (v4.0 and v4.2).  
  
-   SSMA for Oracle v5.0 product can be installed side by side (SxS) with older versions of SSMA (v4.0 and v4.2).  
  
-   Support for reporting User Defined Types (includes subtype, VARRAY, NESTED TABLE, object table and object view) and their usages in PL/SQL blocks with special error messages.  
  
## July 2010  
The July 2010 release of SSMA for Oracle contains the following changes:  
  
-   Support for migrating to SQL Server 2008 R2  
  
-   New SSMA Console application for command line execution  
  
-   Support for Data Migration using both Server Side and Client Side Data Migration Engines  
  
-   Support for “Custom SELECT” statement in data migration  
  
-   Support for migrating from Oracle 11g R2  
  
## June 2008  
The June 2008 release of SSMA for Oracle contains the following changes:  
  
-   Improvements in Assessment Report were completed. It includes additional information for synonyms, raw source for parsable objects, panels and SQL Server logo removal, and layout persistence.  
  
-   Improvements in object conversion:  
  
    -   Packages DBMS_LOB, DBMS_SQL conversion added.  
  
    -   Joins conversion revised.  
  
    -   Modification of collections and records conversion, now conversion of records in simple cases released via separate variables for each field.  
  
    -   Improvements of records and collections implementation.  
  
    -   Windowing aggregation functions added.  
  
    -   ROLLUP/CUBE clause added.  
  
    -   Improvement for NEXTVAL/CURVAL.  
  
    -   Columns grouping in SET clause, Grouping sets and grouping ID were added.  
  
    -   MERGE statement added.  
  
    -   Support of new datetime types and conversion of records and collections as CLR data types added.  
  
-   New features of Tester were added. Tables now can be tested as objects using Tester, a call order of several testable objects in test case can be altered, user can test procedures and functions with records and collections as parameters and return values, and a dependencies analyzer was added to check only used tables.  
  
## August 2007  
The August 2007 release of SSMA for Oracle contains the following changes:  
  
-   A new TESTER component lets you create, manage, and run test cases to verify converted SQL code.  
  
-   Conversion of Oracle subtypes, collections, and local modules have been added to SQL converter.  
  
-   A new synchronization feature lets you synchronize specific objects with SQL Server database.  
  
-   New conversion options added.  
  
## April 2007  
The April 2007 release of SSMA for Oracle was the initial release.  
  
