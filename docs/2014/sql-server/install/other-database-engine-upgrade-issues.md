---
title: "Other Database Engine Upgrade Issues | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "Database Engine [SQL Server], upgrading"
ms.assetid: 78a1d8e8-fa97-476f-8777-84617d145340
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Other Database Engine Upgrade Issues
  The following upgrade issues cannot be detected by the current version of Upgrade Advisor. Review the issues listed below to evaluate their potential impact to your systems.  
  
## Multiple Database Engine Deprecated Features  
 The following [!INCLUDE[tsql](../../includes/tsql-md.md)] statements or options are deprecated:  
  
-   NO_LOG and TRUNCATE_ONLY options of BACKUP LOG  
  
-   BACKUP TRANSACTION  
  
-   RESTORE TRANSACTION  
  
-   DUMP  
  
-   LOAD  
  
-   DBCC CONCURRENCYVIOLATION  
  
-   sp_addalias  
  
-   sp_addgroup  
  
-   sp_changegroup  
  
-   sp_dropgroup  
  
-   sp_helpgroup  
  
-   syssegments  
  
 Use of the VIA protocol to connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is deprecated.  
  
## New Data Types  
 The following will be reserved system types. Rename the existing conflicting user defined types either before or after upgrade.  
  
-   Geography  
  
-   Geometry  
  
-   Datetime2  
  
-   HierarchyID  
  
## Target Table of the OUTPUT INTO Clause Cannot Have Any Defined Triggers  
 OUTPUT INTO a target table when the table has any enabled triggers is not supported.  
  
## Compile Time Error for UDFs When the Target of an OUTPUT INTO Clause is a Table  
 User-defined functions (UDF) cannot be used to perform actions that modify the database state. For example, a UDF cannot perform any DDL (CREATE/ALTER/DROP) or DML (INSERT/UPDATE/DELETE) actions on any objects, except for table variables.  
  
## MERGE is a Reserved Keyword  
 MERGE is now a fully-reserved keyword. Applications can no longer have objects (table, column, etc.) called MERGE.  
  
## Rename CDC Schema  
 There is a schema name called CDC. This schema name cannot be in used if **Change Data Capture** is enabled for the database.  
  
 You must drop the CDC schema before you enable **Change Data Capture** for the database. This step can be done before or after the upgrade. To drop the schema, use the following steps:  
  
1.  Transfer the objects from CDC schema to a new schema name using ALTER SCHEMA.  
  
2.  Verify permissions for the objects in the new schema.  
  
3.  Make necessary modifications to the application.  
  
4.  Drop the CDC schema using DROP SCHEMA.  
  
## See Also  
 [Database Engine Upgrade Issues](../../../2014/sql-server/install/database-engine-upgrade-issues.md)  
  
  
