---
title: "Migrating Oracle Databases to SQL Server (OracleToSQL) | Microsoft Docs"
description: Use this recommended process to migrate Oracle databases to SQL Server or Azure SQL Database using SQL Server Migration Assistant (SSMA).
ms.service: sql
ms.custom:
  - intro-migration
ms.date: "04/22/2018"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: 1d196dd6-4322-4c98-bb72-602c57d96134
author: cpichuka 
ms.author: cpichuka 
---
# Migrating Oracle Databases to SQL Server (OracleToSQL)
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Migration Assistant (SSMA) for Oracle is a comprehensive environment that helps you quickly migrate Oracle databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], Azure SQL Database, or Azure Synapse Analytics. By using SSMA for Oracle, you can review database objects and data, assess databases for migration, migrate database objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], Azure SQL Database, or Azure Synapse Analytics, and then migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], Azure SQL Database, or Azure Synapse Analytics. Note that you cannot migrate SYS and SYSTEM Oracle schemas.

>[!Note]
> Try new [Database Migration Assessment for Oracle extension in Azure Data Studio](../../azure-data-studio/extensions/database-migration-assessment-for-oracle-extension.md) for Oracle to SQL pre-assessment and workload categorization. If you are in early phase of Oracle to SQL migration and would need to do a high level workload assessment , interested in sizing Azure SQL target for the Oracle workload  or understand feature migration parity, try the new extension. For detailed code assessment and conversion, continue with SSMA for Oracle.

## Recommended Migration Process  
To successfully migrate objects and data from Oracle databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], Azure SQL Database, or Azure Synapse Analytics, use the following process:
  
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
  
