---
title: "Migrating DB2 Databases to SQL Server (DB2ToSQL) | Microsoft Docs"
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
ms.assetid: 14d2e655-af7e-4aa5-ba28-0e3d0d025518
caps.latest.revision: 4
author: "sabotta"
ms.author: "carlasab"
manager: "lonnyb"
---
# Migrating DB2 Databases to SQL Server (DB2ToSQL)
[!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Migration Assistant (SSMA) for DB2 is a comprehensive environment that helps you quickly migrate DB2 databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or Azure SQL DB. By using SSMA for DB2, you can review database objects and data, assess databases for migration, migrate database objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or Azure SQL DB, and then migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or Azure SQL DB. Note that you cannot migrate SYS and SYSTEM DB2 schemas.  
  
## Recommended Migration Process  
To successfully migrate objects and data from DB2 databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or Azure SQL DB, use the following process:  
  
1.  [New SSMA project](http://msdn.microsoft.com/en-us/66437b45-4686-4fc7-a91b-ebde45e0f1b0).  
  
    After you create the project, you can set project conversion, migration, and type mapping options. For information about project settings, see [Project Settings &#40;Conversion&#41; &#40;DB2ToSQL&#41;](../../ssma/db2/project-settings-conversion-db2tosql.md) and related sections. For information about how to customize data type mappings, see [Mapping DB2 and SQL Server Data Types &#40;DB2ToSQL&#41;](../../ssma/db2/mapping-db2-and-sql-server-data-types-db2tosql.md).  
  
2.  [Connect to the DB2 database](http://msdn.microsoft.com/en-us/5eb5801d-f0c3-4127-97c0-0b1ef49f4844).  
  
3.  [Connecting to SQL Server](http://msdn.microsoft.com/en-us/b59803cb-3cc6-41cc-8553-faf90851410e).  
  
4.  [Map DB2 schemas to SQL Server schemas](http://msdn.microsoft.com/en-us/05ff7bd4-e60b-4f48-a893-bc2346aa9a8a).  
  
5.  Optionally, [Assessment reports](http://msdn.microsoft.com/en-us/9e13eba0-e3cf-4205-974f-c00f982061de) to assess database objects for conversion and estimate the conversion time.  
  
6.  [Convert DB2 schemas](http://msdn.microsoft.com/en-us/7947efc3-ca86-4ec5-87ce-7603059c75a0).  
  
7.  [Load the converted database objects into SQL Server](http://msdn.microsoft.com/en-us/f4ea1ced-9f9f-4a9d-88ab-81dbab64adc3).  
  
    You can do this in one of the following ways:  
  
    -   Save a script and run it in [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)].  
  
    -   Synchronize the database objects.  
  
8.  [Migrating DB2 Data into SQL Server](http://msdn.microsoft.com/en-us/86cbd39f-6dac-409a-9ce1-7dd54403f84b).  
  
9. If necessary, update database applications.  
  
## See Also  
[Installing SSMA for DB2 &#40;DB2ToSQL&#41;](../../ssma/db2/installing-ssma-for-db2-db2tosql.md)  
[Getting Started with SSMA for DB2 &#40;DB2ToSQL&#41;](../../ssma/db2/getting-started-with-ssma-for-db2-db2tosql.md)  
  
