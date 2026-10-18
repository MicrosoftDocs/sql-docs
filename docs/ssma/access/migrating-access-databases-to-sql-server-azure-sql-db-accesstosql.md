---
title: "Migrate Access Databases to SQL Server and Azure SQL Database"
description: Use this recommended process to migrate Access databases to SQL Server or Azure SQL Database using SQL Server Migration Assistant (SSMA).
author: nilabjaball
ms.author: niball
ms.reviewer: vanto
ms.date: 12/30/2025
ms.service: sql
ms.subservice: ssma
ms.topic: upgrade-and-migration-article
ms.collection:
  - sql-migration-content
ms.custom:
  - intro-migration
f1_keywords:
  - "ssma.access.migratedata.f1"
helpviewer_keywords:
  - "instructions, migration"
  - "migrating databases, overview"
  - "overview, migration process"
  - "procedure, migration"
  - "recommended migration process"
---
# Migrate Access databases to SQL Server and Azure SQL (AccessToSQL)

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Migration Assistant (SSMA) is a tool that provides a comprehensive environment that helps you quickly migrate Access databases to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL. By using SSMA, you can review Access and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database objects, assess the Access database for migration, convert Access database objects, load them into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL, and then migrate data.

## Recommended migration process

To successfully migrate objects and data from Access to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL, use the following process:

1. [Create and manage projects](creating-and-managing-projects-accesstosql.md). After you create the project, you can [set project options](setting-conversion-and-migration-options-accesstosql.md), including conversion options, migration options, and data type mappings.

1. [Add and remove Access database files](adding-and-removing-access-database-files-accesstosql.md) to the project.

   You can add individual files, including files that you find on the computer or network.

1. [Connect to SQL Server](connecting-to-sql-server-accesstosql.md) or [Connect to Azure SQL](connecting-to-azure-sql-db-accesstosql.md).

   You can connect either to SQL Server or Azure SQL.

1. To customize the mapping between one or more Access databases and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL schemas, [map the source and target databases](mapping-source-and-target-databases-accesstosql.md).

1. Optionally, you can [create an assessment report](assessing-access-database-objects-for-conversion-accesstosql.md) to determine whether the Access database objects can be successfully converted to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL.

1. [Convert Access database objects](converting-access-database-objects-accesstosql.md) to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL object definitions.

1. [Load converted database objects into SQL Server](loading-converted-database-objects-into-sql-server-accesstosql.md).

   You can load either the database objects into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL by using SSMA, or you can save [!INCLUDE [tsql](../../includes/tsql-md.md)] scripts.

1. [Migrate Access data into SQL Server and Azure SQL](migrating-access-data-into-sql-server-azure-sql-db-accesstosql.md).

   You can convert, load, and migrate schemas and data in one step. To perform a single-step migration, select the **Convert, Load, and Migrate** button.

1. If you want your Access applications to use the data in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL, use [link the Access tables to the SQL Server tables](linking-access-applications-to-sql-server-azure-sql-db-accesstosql.md).

You can also use the Migration Wizard to guide you through this process. For more information, see [Migration Wizard](migration-wizard-accesstosql.md).

## Related content

- [Get started with SQL Server Migration Assistant for Access](getting-started-with-sql-server-migration-assistant-for-access-accesstosql.md)
- [Prepare Access databases for migration](preparing-access-databases-for-migration-accesstosql.md)
