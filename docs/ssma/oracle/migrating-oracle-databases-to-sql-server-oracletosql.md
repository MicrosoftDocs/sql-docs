---
title: "Migrating Oracle Databases to SQL Server (OracleToSQL) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "sql-ssma"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 1d196dd6-4322-4c98-bb72-602c57d96134
caps.latest.revision: 9
author: "Shamikg"
ms.author: "Shamikg"
manager: "v-thobro"
---
# Migrating Oracle Databases to SQL Server (OracleToSQL)
[!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Migration Assistant (SSMA) for Oracle is a comprehensive environment that helps you quickly migrate Oracle databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or Azure SQL DB. By using SSMA for Oracle, you can review database objects and data, assess databases for migration, migrate database objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or Azure SQL DB, and then migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or Azure SQL DB. Note that you cannot migrate SYS and SYSTEM Oracle schemas.  
  
## Recommended Migration Process  
To successfully migrate objects and data from Oracle databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or Azure SQL DB, use the following process:  
  
1.  [Create a new SSMA project](http://msdn.microsoft.com/en-us/ee5d94c0-c7a6-4779-bd32-729bdaf61e1b).  
  
    After you create the project, you can set project conversion, migration, and type mapping options. For information about project settings, see [Setting Project Options &#40;OracleToSQL&#41;](../../ssma/oracle/setting-project-options-oracletosql.md). For information about how to customize data type mappings, see [Mapping Oracle and SQL Server Data Types &#40;OracleToSQL&#41;](../../ssma/oracle/mapping-oracle-and-sql-server-data-types-oracletosql.md).  
  
2.  [Connect to the Oracle database server](http://msdn.microsoft.com/en-us/e276cdbf-3ebc-4ba8-b40d-a7a42befa2b6).  
  
3.  [Connect to an instance of SQL Server](http://msdn.microsoft.com/en-us/1b2a8059-1829-4904-a82f-9c06de1e245f).  
  
4.  [Map Oracle database schemas to SQL Server database schemas](http://msdn.microsoft.com/en-us/0edeaa08-9c5d-4e3a-bc15-b9a1f0c8a9dc).  
  
5.  Optionally, [Create assessment reports](http://msdn.microsoft.com/en-us/4de9bcf6-1346-4740-87f9-7f24a8226357) to assess database objects for conversion and estimate the conversion time.  
  
6.  [Convert Oracle database schemas into SQL Server schemas](http://msdn.microsoft.com/en-us/e021182d-31da-443d-b110-937f5db27272).  
  
7.  [Load the converted database objects into SQL Server](http://msdn.microsoft.com/en-us/a8ae33b2-1883-4785-922b-ea0e31c0b37a).  
  
    You can do this in one of the following ways:  
  
    -   Save a script and run it in [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)].  
  
    -   Synchronize the database objects.  
  
8.  [Migrate data to SQL Server](http://msdn.microsoft.com/en-us/e23c5268-41ed-4e55-9fe7-a11376202a13).  
  
9. If necessary, update database applications.  
  
## See Also  
[Installing SSMA  for Oracle &#40;OracleToSQL&#41;](../../ssma/oracle/installing-ssma-for-oracle-oracletosql.md)  
[Getting Started with SSMA for Oracle &#40;OracleToSQL&#41;](../../ssma/oracle/getting-started-with-ssma-for-oracle-oracletosql.md)  
  
