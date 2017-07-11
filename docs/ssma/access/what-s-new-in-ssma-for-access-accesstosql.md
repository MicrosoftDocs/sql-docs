---
title: "What&#39;s New in SSMA for Access(AccessToSQL) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "sql-ssma"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "Azure SQL Database"
  - "SQL Server"
ms.assetid: a24d3fc0-6911-4bfa-828a-197abf222e02
caps.latest.revision: 37
author: "sabotta"
ms.author: "carlasab"
manager: "lonnyb"
---
# What&#39;s New in SSMA for Access(AccessToSQL)
This topic lists SSMA for Access changes in each release.  
  
## May 2016  
The May 2016  release of SSMA for Access contains  the following changes:  
  
1.  Official support for SQL Server 2016
2.  Removed installer check for .Net 2.0
3.  Fixed "save project" and "open project" commands for SSMA Console
4.  Fixed "securepassword" command for SSMA Console
5.  Fixed counting of objects for initial loading
6.  Fixed tables data loading for UI tabs for Access
7.  Fixed bug in global settings 
   
## March 2016  
The March 2016 preview release of SSMA for Access contains  the following changes:  
  
1.  Support migration to SQL Server 2016  
   
## January 2016  
The January 2016 maintenance release of SSMA for Access contains the following changes:  
  
1.  Fixed invalid function for default of a GUID field (RFC 3894811)  
  
2.  Fixed hang on importing records to SQL Database (Azure) (RFC 4919573)  
  
3.  Added View Log Menu Item to SSMA (RFC 5706203)  
  
4.  Added Telemetry  
  
## July 2014  
The July 2014 release of SSMA for Access contains the following changes:  
  
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
The April 2014 release of SSMA for Access contains the following changes:  
  
1.  Added support of MS SQL Server 2014.  
  
2.  Fixed bugs regarding conversion to Azure.  
  
3.  Fixed bugs regarding invisible report pages in IE 10.  
  
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
  
-   Single Installable of “SSMA for Access”, which supports [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] 2005, [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] 2008, [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] “Denali” and SQL Azure.  
  
-   The ability to connect [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] “Denali”  
  
-   SSMA for Access Console version supports backward compatibility. You will be able to open the projects created by versions earlier to SSMA v5.0  
  
-   SSMA v5.0 product can be installed side by side (SxS) with older versions of SSMA Product.  
  
## July 2010  
The July 2010 release of SSMA for Access contains the following changes:  
  
-   Support for migrating to SQL Server 2008 R2 and SQL Azure  
  
-   Secure connection to both SQL Server and SQL Azure.  
  
-   Support for Access 2010 databases  
  
-   New SSMA Console application for command line execution  
  
-   Support for SQL Server DateTime2 data type  
  
## June 2008  
The June 2008 release of SSMA for Access contains the following changes:  
  
-   Support for Access 2007 databases.  
  
## May 2007  
The May 2007 release of SSMA for Access contains the following changes:  
  
-   Support for Access databases that use workgroup policies.  
  
-   The ability to delete converted objects from the SQL Server metadata explorer.  
  
-   Support for user-entered comments the SQL Server formatted SQL mode.  
  
-   Improvements in object conversion.  
  
## November 2006  
The November 2006 release of SSMA for Access contains the following changes:  
  
-   A new Database Migration Wizard that guides you through the migration of a single database from Access to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)].  
  
-   A new Convert, Load, and Migrate command that converts Access databases, loads the converted objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)], and migrates data into [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] all in one step.  
  
-   Improved query migration. Query migration now converts more SELECT queries to views. For more information, see [Converting Access Database Objects](http://msdn.microsoft.com/en-us/e0ef67bf-80a6-4e6c-a82d-5d46e0623c6c).  
  
-   You can now edit table and index properties on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] **Table** tab.  
  
-   New global settings:  
  
    -   You can opt to show line numbers in editor windows.  
  
    -   You can configure SSMA to prompt to replace duplicate objects, or always or never replace duplicate objects during schema conversion.  
  
-   A new conversion option lets you specify if SSMA displays a warning when a complex query contains a wildcard.  
  
## July 2006  
The July 2006 release of SSMA for Access was the initial release.x  
  
