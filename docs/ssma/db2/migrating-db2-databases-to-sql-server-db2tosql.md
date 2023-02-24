---
title: "Migrating DB2 Databases to SQL Server (DB2ToSQL) | Microsoft Docs"
description: Use this recommended process to migrate DB2 databases to SQL Server or Azure SQL Database using SQL Server Migration Assistant (SSMA).
ms.service: sql
ms.custom:
  - intro-migration
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: 14d2e655-af7e-4aa5-ba28-0e3d0d025518
author: cpichuka 
ms.author: cpichuka 
f1_keywords: 
    - "ssma.db2.migratedata.f1"

---
# Migrating DB2 Databases to SQL Server (DB2ToSQL)
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Migration Assistant (SSMA) for DB2 is a comprehensive environment that helps you quickly migrate DB2 databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database. By using SSMA for DB2, you can review database objects and data, assess databases for migration, migrate database objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database, and then migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database. Note that you cannot migrate SYS and SYSTEM DB2 schemas.  
  
## Recommended Migration Process  
To successfully migrate objects and data from DB2 databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database, use the following process:  
  
1.  [New SSMA project](./new-project-db2tosql.md).  
  
    After you create the project, you can set project conversion, migration, and type mapping options. For information about project settings, see [Project Settings &#40;Conversion&#41; &#40;DB2ToSQL&#41;](../../ssma/db2/project-settings-conversion-db2tosql.md) and related sections. For information about how to customize data type mappings, see [Mapping DB2 and SQL Server Data Types &#40;DB2ToSQL&#41;](../../ssma/db2/mapping-db2-and-sql-server-data-types-db2tosql.md).  
  
2.  [Connect to the DB2 database](./connecting-to-db2-database-db2tosql.md).  
  
3.  [Connecting to SQL Server](./connecting-to-sql-server-db2tosql.md).  
  
4.  [Map DB2 schemas to SQL Server schemas](./mapping-db2-schemas-to-sql-server-schemas-db2tosql.md).  
  
5.  Optionally, [Assessment reports](./assessment-report-db2tosql.md) to assess database objects for conversion and estimate the conversion time.  
  
6.  [Convert DB2 schemas](./converting-db2-schemas-db2tosql.md).  
  
7.  [Load the converted database objects into SQL Server](./loading-converted-database-objects-into-sql-server-db2tosql.md).  
  
    You can do this in one of the following ways:  
  
    -   Save a script and run it in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
    -   Synchronize the database objects.  
  
8.  [Migrating DB2 Data into SQL Server](./migrating-db2-data-into-sql-server-db2tosql.md).  
  
9. If necessary, update database applications.  
  
## See Also  
[Installing SSMA for DB2 &#40;DB2ToSQL&#41;](../../ssma/db2/installing-ssma-for-db2-db2tosql.md)  
[Getting Started with SSMA for DB2 &#40;DB2ToSQL&#41;](../../ssma/db2/getting-started-with-ssma-for-db2-db2tosql.md)  
