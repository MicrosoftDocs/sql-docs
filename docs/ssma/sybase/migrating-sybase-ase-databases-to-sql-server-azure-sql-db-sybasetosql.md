---
title: "Migrate Sybase ASE Databases to SQL Server - Azure SQL DB | Microsoft Docs"
ms.custom: ""
ms.date: "11/30/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: ed7952d4-8331-44d7-bccf-3440e17238b2
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Migrating SAP ASE databases to SQL Server - Azure SQL Database (SybaseToSQL)
SQL Server Migration Assistant (SSMA) for SAP Adaptive Server Enterprise (ASE) is a comprehensive environment that helps you quickly migrate SAP ASE databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database. By using SSMA for SAP ASE, you can review database objects and data, assess databases for migration, migrate database objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database, and then migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database.  
  
## Recommended migration process  
To successfully migrate objects and data from SAP ASE databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database, use the following process:  
  
1.  [Create a new SSMA project](working-with-ssma-projects-sybasetosql.md).  
  
    After you create the project, you can set project conversion, migration, and type mapping options. For information about project settings, see [Setting Project Options &#40;SybaseToSQL&#41;](../../ssma/sybase/setting-project-options-sybasetosql.md). For information about customizing data type mappings, see [Mapping Sybase ASE and SQL Server Data Types &#40;SybaseToSQL&#41;](../../ssma/sybase/mapping-sybase-ase-and-sql-server-data-types-sybasetosql.md).  
  
2.  [Connect to the SAP ASE database server](connecting-to-sybase-ase-sybasetosql.md).  
  
3.  [Connect to an instance SQL Server](connecting-to-sql-server-sybasetosql.md) or [Connect to an instance of Azure SQL Database](connecting-to-azure-sql-db-sybasetosql.md).  
  
4.  [Map SAP ASE database schemas to SQL Server / Azure SQL Database database schemas](https://msdn.microsoft.com/2c927003-c49d-4fe1-8e3e-5b2899166268).  
  
5.  Optionally, [create assessment reports](assessing-sybase-ase-database-objects-for-conversion-sybasetosql.md) to assess database objects for conversion and estimate the conversion time.  
  
6.  [Convert SAP ASE database schemas into SQL Server / Azure SQL Database schemas](https://msdn.microsoft.com/509cb65d-2f54-427a-83d7-37919cc4e3e3).  
  
7.  [Load the converted database objects into SQL Server / Azure SQL Database](https://msdn.microsoft.com/4c59256f-99a8-4351-9559-a455813dbd06).  
  
    Either save a script and run it in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database, or synchronize the database objects.  
  
8.  [Migrate data to SQL Server / Azure SQL Database](https://msdn.microsoft.com/54a39f5e-9250-4387-a3ae-eae47c799811).  
  
9. If necessary, update your database applications.  
  
## See also  
[Installing SSMA for SAP ASE &#40;SybaseToSQL&#41;](../../ssma/sybase/installing-ssma-for-sybase-sybasetosql.md)  
[Getting Started with SSMA for SAP ASE &#40;SybaseToSQL&#41;](../../ssma/sybase/getting-started-with-ssma-for-sybase-sybasetosql.md)  
  
