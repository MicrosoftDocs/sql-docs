---
title: "Migrate Access Databases to SQL Server - Azure SQL DB | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "08/15/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "instructions, migration"
  - "migrating databases, overview"
  - "overview, migration process"
  - "procedure, migration"
  - "recommended migration process"
ms.assetid: 76a3abcf-2998-4712-9490-fe8d872c89ca
author: "Shamikg"
ms.author: "Shamikg"
manager: "murato"
---
# Migrating Access databases to SQL Server - Azure SQL DB (AccessToSQL)
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Migration Assistant (SSMA) is a tool that provides a comprehensive environment that helps you quickly migrate Access databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure. By using SSMA, you can review Access and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure database objects, assess the Access database for migration, convert Access database objects, load them into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, and then migrate data.  
  
## Recommended migration process  
To successfully migrate objects and data from Access to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, use the following process:  
  
1.  [Create a new SSMA Project](creating-and-managing-projects-accesstosql.md). After you create the project, you can [set project options](setting-conversion-and-migration-options-accesstosql.md), including conversion options, migration options, and data type mappings.  
  
2.  [Add Access database files](adding-and-removing-access-database-files-accesstosql.md) to the project.  
  
    You can add individual files, including files that you find on the computer or network.  
  
3.  [Connect to the target instance of SQL Server](connecting-to-sql-server-accesstosql.md) or [Connect to the target instance of SQL Azure](connecting-to-azure-sql-db-accesstosql.md).  
  
    You can connect either to SQL Server or SQL Azure.  
  
4.  To customize the mapping between one or more Access databases and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure schemas,  [map the source and target databases](mapping-source-and-target-databases-accesstosql.md).  
  
5.  Optionally, you can [create an assessment report](assessing-access-database-objects-for-conversion-accesstosql.md) to determine whether the Access database objects can be successfully converted to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure.  
  
6.  [Convert Access database objects](converting-access-database-objects-accesstosql.md) to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure object definitions.  
  
7.  [Load the converted database objects into SQL Server](loading-converted-database-objects-into-sql-server-accesstosql.md).  
  
    You can load either the database objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure by using SSMA, or you can save [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts.  
  
8.  [Migrate Access data into SQL Server](migrating-access-data-into-sql-server-azure-sql-db-accesstosql.md).  
  
    > [!NOTE]  
    > You can convert, load, and migrate schemas and data in one step. To perform one-click migration, click the **Convert, Load, and Migrate** button.  
  
9. If you want your Access applications to use the data in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, use [link the Access tables to the SQL Server tables](linking-access-applications-to-sql-server-azure-sql-db-accesstosql.md).  
  
You can also use the Migration Wizard to guide you through this process. For more information, see [Migration Wizard](migration-wizard-accesstosql.md).  
  
## See also  
[Getting Started with SQL Server Migration Assistant for Access](getting-started-with-sql-server-migration-assistant-for-access-accesstosql.md)  
[Preparing Access Databases for Migration](preparing-access-databases-for-migration-accesstosql.md)
