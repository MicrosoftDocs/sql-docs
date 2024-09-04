---
title: "Migrating Oracle Databases to SQL Server (OracleToSQL)"
description: Use this recommended process to migrate Oracle databases to SQL Server or Azure SQL Database using SQL Server Migration Assistant (SSMA).
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 07/10/2023
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.custom:
  - intro-migration
  - sql-migration-content
---
# Migrate Oracle Databases to SQL Server (OracleToSQL)

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Migration Assistant (SSMA) for Oracle is a comprehensive environment that helps you quickly migrate Oracle databases to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], Azure SQL Database, or Azure Synapse Analytics. By using SSMA for Oracle, you can review database objects and data, assess databases for migration, migrate database objects to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], Azure SQL Database, or Azure Synapse Analytics, and then migrate data to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], Azure SQL Database, or Azure Synapse Analytics. You can't migrate SYS and SYSTEM Oracle schemas.

## Database Migration Assessment for Oracle extension

Try the [Database Migration Assessment for Oracle extension in Azure Data Studio](/azure-data-studio/extensions/database-migration-assessment-for-oracle-extension) for Oracle to SQL pre-assessment and workload categorization. This extension is useful if you are:

- in the early phase of Oracle to SQL migration and need to do a high level workload assessment
- interested in sizing an Azure SQL target for your Oracle workload
- looking to understand feature migration parity

For detailed code assessment and conversion, continue with SSMA for Oracle.

## Recommended migration process

To successfully migrate objects and data from Oracle databases to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], Azure SQL Database, or Azure Synapse Analytics, use the following process:

1. [Create a new SSMA project](working-with-ssma-projects-oracletosql.md).

   After you create the project, you can set project conversion, migration, and type mapping options. For information about project settings, see [Setting Project Options (OracleToSQL)](setting-project-options-oracletosql.md). For information about how to customize data type mappings, see [Mapping Oracle and SQL Server Data Types (OracleToSQL)](mapping-oracle-and-sql-server-data-types-oracletosql.md).

1. [Connect to the Oracle database server](connecting-to-oracle-database-oracletosql.md).

1. [Connect to an instance of SQL Server](connecting-to-sql-server-oracletosql.md).

1. [Map Oracle database schemas to SQL Server database schemas](mapping-oracle-schemas-to-sql-server-schemas-oracletosql.md).

1. Optionally, [Create assessment reports](assessing-oracle-schemas-for-conversion-oracletosql.md) to assess database objects for conversion and estimate the conversion time.

1. [Convert Oracle database schemas into SQL Server schemas](converting-oracle-schemas-oracletosql.md).

1. [Load the converted database objects into SQL Server](loading-converted-database-objects-into-sql-server-oracletosql.md).

   You have two options:

   - Save a script and run it in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]
   - Synchronize the database objects

1. [Migrate data to SQL Server](migrating-oracle-data-into-sql-server-oracletosql.md).

1. If necessary, update database applications.

## See also

- [Installing SSMA  for Oracle (OracleToSQL)](installing-ssma-for-oracle-oracletosql.md)
- [Getting Started with SSMA for Oracle (OracleToSQL)](getting-started-with-ssma-for-oracle-oracletosql.md)
- [Migration guide: Oracle to Azure SQL Managed Instance](/azure/azure-sql/migration-guides/managed-instance/oracle-to-managed-instance-guide)
