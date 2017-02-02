---
title: "What&#39;s New in SSMA for MySQL (MySQLToSql) | Microsoft Docs"
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
ms.assetid: 1451a0b0-6713-4d0c-954f-ea3d8fce1d31
caps.latest.revision: 21
author: "sabotta"
ms.author: "carlasab"
manager: "lonnyb"
---
# What&#39;s New in SSMA for MySQL (MySQLToSql)
This topic lists SSMA for MySQL changes in each release.  

## May 2016  
The May 2016 release of SSMA for MySQL contains  the following changes:  .

1.  Added support for SQL Server 2016
2.  Improved parser and resolver
3.  Removed installer check for .Net 2.0
4.  Updated Extension Pack dependency from .Net 3.5 to .Net 4.0
5.  Fixed default BigInt typemapping for MySql
6.  Fixed "save project" and "open project" commands for SSMA Console
7.  Fixed "securepassword" command for SSMA Console
8.  Fixed counting of objects for initial loading
9.  Fixed MsSql objects loading
10. Fixed bug in global settings 
 
## March 2016  
The March 2016 preview release of SSMA for MySQL contains  the following changes:  
  
1.  Support migration to SQL Server 2016  
  
## January 2016  
The January 2016 maintenance release of SSMA for MySQL contains the following changes:  
  
1.  Added View Log Menu Item to SSMA (RFC 5706203)  
  
2.  Added Telemetry  
  
## July 2014  
The July 2014 release of SSMA for MySQL contains the following changes:  
  
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
The July 2011 release of SSMA for MySQL contains the following changes:  
  
1.  Added support of MS SQL Server 2014.  
  
2.  Fixed bugs regarding conversion to Azure  
  
3.  Fixed bugs regarding invisible report pages in IE 10.  
  
## July 2011  
The July 2011 release of SSMA for MySQL contains the following changes:  
  
-   Support for conversion of LIMIT to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] “Denali” OFFSET.  
  
-   Improved error reporting during data migration.  
  
## April 2011  
The April 2011 release of SSMA for MySQL contains the following changes:  
  
-   Single Installable of “SSMA for MySQL”, which supports [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] 2005, [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] 2008, [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] “Denali” and SQL Azure.  
  
-   The ability to connect [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] “Denali”.  
  
-   Enhanced client side data migration engine, supporting parallel migration of data.  
  
-   Improved data migration performance with Simple and Bulk logged recovery models.  
  
-   SSMA for MySQL Console version supports backward compatibility. You will be able to open the projects created by versions earlier to SSMA v5.0.  
  
-   SSMA for MySQL v5.0 product can be installed side by side (SxS) with older versions of SSMA Product.  
  
## July 2010  
The July 2010 release of SSMA for MySQL contains the following features:  
  
1.  **Improvements to User Interface:**  
  
    -   ‘SQL Modes’ Tab for MySQL Database objects  
  
    -   ‘Settings’ Tab for MySQL Database objects  
  
    -   ‘Data’ Tab for MySQL Tables  
  
    -   Updated Project Settings in Conversion and Migration Pages  
  
    -   ‘Data Migration Settings’ at Table level  
  
2.  **Improvements to Connect to MySQL and SQL Server:**  
  
    -   SSL Connectivity in MySQL  
  
    -   Encrypted Connectivity in SQL Server  
  
3.  **Improvements to MySQL Metabase Explorer:**  
  
    -   Loading all the MySQL Database Objects and their respective Tabs.  
  
4.  **Improvements to Object Conversion:**  
  
    -   Conversion of MySQL Metabase objects – Procedures, Functions, Views, Triggers and Statements.  
  
    -   Limited support for Spatial Data Types in Tables.  
  
    -   Option to convert MySQL functions to SQL Server Stored Procedures  
  
    -   Option to apply SQL Modes and Charset mapping during Object Conversion  
  
5.  **Improvements to Data Migration:**  
  
    -   Support for Data Migration using both Server Side and Client Side Data Migration Engines  
  
    -   Support for Spatial Data Migration  
  
    -   Custom SQL for Data Migration for Tables  
  
6.  **SSMA for MySQL Console:**  
  
    -   Support Console Feature for SSMA for MySQL  
  
    -   Support for Script-Level Interfacing  
  
## January 2010  
The January 2010 release of SSMA for MySQL was the initial release. It contained the following features:  
  
-   Migration to both on-premise SQL Server and SQL Azure are supported.  
  
-   **Feature Snapshot:** Schema and Data migration of MySQL Tables/Indexes/Constraints.  
  
