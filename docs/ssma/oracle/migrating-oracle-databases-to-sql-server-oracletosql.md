---
title: "Migrating Oracle Databases to SQL Server (OracleToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "04/22/2018"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: 1d196dd6-4322-4c98-bb72-602c57d96134
author: "Shamikg"
ms.author: "Shamikg"
manager: "v-thobro"
---
# Migrating Oracle Databases to SQL Server (OracleToSQL)
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Migration Assistant (SSMA) for Oracle is a comprehensive environment that helps you quickly migrate Oracle databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], Azure SQL DB, or Azure SQL Data Warehouse. By using SSMA for Oracle, you can review database objects and data, assess databases for migration, migrate database objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], Azure SQL DB, or Azure SQL Data Warehouse, and then migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], Azure SQL DB, or Azure SQL Data Warehouse. Note that you cannot migrate SYS and SYSTEM Oracle schemas.
  
## Recommended Migration Process  
To successfully migrate objects and data from Oracle databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], Azure SQL DB, or Azure SQL Data Warehouse, use the following process:
  
1.  [Create a new SSMA project](working-with-ssma-projects-oracletosql.md).  
  
    After you create the project, you can set project conversion, migration, and type mapping options. For information about project settings, see [Setting Project Options &#40;OracleToSQL&#41;](../../ssma/oracle/setting-project-options-oracletosql.md). For information about how to customize data type mappings, see [Mapping Oracle and SQL Server Data Types &#40;OracleToSQL&#41;](../../ssma/oracle/mapping-oracle-and-sql-server-data-types-oracletosql.md).  
  
2.  [Connect to the Oracle database server](connecting-to-oracle-database-oracletosql.md).  
  
3.  [Connect to an instance of SQL Server](connecting-to-sql-server-oracletosql.md).  
  
4.  [Map Oracle database schemas to SQL Server database schemas](mapping-oracle-schemas-to-sql-server-schemas-oracletosql.md).  
  
5.  Optionally, [Create assessment reports](assessing-oracle-schemas-for-conversion-oracletosql.md) to assess database objects for conversion and estimate the conversion time.  
  
6.  [Convert Oracle database schemas into SQL Server schemas](converting-oracle-schemas-oracletosql.md).  
  
7.  [Load the converted database objects into SQL Server](loading-converted-database-objects-into-sql-server-oracletosql.md).  
  
    You can do this in one of the following ways:  
  
    -   Save a script and run it in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
    -   Synchronize the database objects.  
  
8.  [Migrate data to SQL Server](migrating-oracle-data-into-sql-server-oracletosql.md).  
  
9. If necessary, update database applications.  
  
## See Also  
[Installing SSMA  for Oracle &#40;OracleToSQL&#41;](../../ssma/oracle/installing-ssma-for-oracle-oracletosql.md)  
[Getting Started with SSMA for Oracle &#40;OracleToSQL&#41;](../../ssma/oracle/getting-started-with-ssma-for-oracle-oracletosql.md)  
  
