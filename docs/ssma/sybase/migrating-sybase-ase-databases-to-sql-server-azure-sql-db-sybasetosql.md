---
title: "Migrate Sybase ASE Databases to SQL Server - Azure SQL Database | Microsoft Docs"
description: Use this recommended process to migrate SAP Adaptive Server Enterprise databases to SQL Server or Azure SQL Database using SQL Server Migration Assistant (SSMA).
ms.custom:
  - intro-migration
ms.date: "11/30/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: ed7952d4-8331-44d7-bccf-3440e17238b2
author: cpichuka 
ms.author: cpichuka 
---
# Migrating SAP ASE databases to SQL Server - Azure SQL Database (SybaseToSQL)
SQL Server Migration Assistant (SSMA) for SAP Adaptive Server Enterprise (ASE) is a comprehensive environment that helps you quickly migrate SAP ASE databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database. By using SSMA for SAP ASE, you can review database objects and data, assess databases for migration, migrate database objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database, and then migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database.  
  
## Recommended migration process  
To successfully migrate objects and data from SAP ASE databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database, use the following process:  
  
1.  [Create a new SSMA project](working-with-ssma-projects-sybasetosql.md).  
  
    After you create the project, you can set project conversion, migration, and type mapping options. For information about project settings, see [Setting Project Options &#40;SybaseToSQL&#41;](../../ssma/sybase/setting-project-options-sybasetosql.md). For information about customizing data type mappings, see [Mapping Sybase ASE and SQL Server Data Types &#40;SybaseToSQL&#41;](../../ssma/sybase/mapping-sybase-ase-and-sql-server-data-types-sybasetosql.md).  
  
2.  [Connect to the SAP ASE database server](connecting-to-sybase-ase-sybasetosql.md).  
  
3.  [Connect to an instance SQL Server](connecting-to-sql-server-sybasetosql.md) or [Connect to an instance of Azure SQL Database](connecting-to-azure-sql-db-sybasetosql.md).  
  
4.  [Map SAP ASE database schemas to SQL Server / Azure SQL Database database schemas](./mapping-sybase-ase-schemas-to-sql-server-schemas-sybasetosql.md).  
  
5.  Optionally, [create assessment reports](assessing-sybase-ase-database-objects-for-conversion-sybasetosql.md) to assess database objects for conversion and estimate the conversion time.  
  
6.  [Convert SAP ASE database schemas into SQL Server / Azure SQL Database schemas](./converting-sybase-ase-database-objects-sybasetosql.md).  
  
7.  [Load the converted database objects into SQL Server / Azure SQL Database](./loading-converted-database-objects-into-sql-server-sybasetosql.md).  
  
    Either save a script and run it in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database, or synchronize the database objects.  
  
8.  [Migrate data to SQL Server / Azure SQL Database](./migrating-sybase-ase-data-into-sql-server-azure-sql-db-sybasetosql.md).  
  
9. If necessary, update your database applications.  
  
## See also  
[Installing SSMA for SAP ASE &#40;SybaseToSQL&#41;](../../ssma/sybase/installing-ssma-for-sybase-sybasetosql.md)  
[Getting Started with SSMA for SAP ASE &#40;SybaseToSQL&#41;](../../ssma/sybase/getting-started-with-ssma-for-sybase-sybasetosql.md)  
