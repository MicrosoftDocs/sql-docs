---
title: "Migrating Oracle Databases to SQL Server (OracleToSQL)"
description: Use this recommended process to migrate Oracle databases to SQL Server or Azure SQL Database using SQL Server Migration Assistant (SSMA).
author: nilabjaball
ms.author: niball
ms.reviewer: randolphwest
ms.date: 11/19/2024
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
ms.custom:
  - intro-migration
---
# Migrate Oracle Databases to SQL Server (OracleToSQL)

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Migration Assistant (SSMA) for Oracle is a comprehensive environment that helps you quickly migrate Oracle databases to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], Azure SQL Database, or Azure Synapse Analytics. By using SSMA for Oracle, you can review database objects and data, assess databases for migration, migrate database objects to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], Azure SQL Database, or Azure Synapse Analytics, and then migrate data to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], Azure SQL Database, or Azure Synapse Analytics. You can't migrate SYS and SYSTEM Oracle schemas.

## Database Migration Assessment for Oracle extension

Try the [Database Migration Assessment for Oracle extension](/azure-data-studio/extensions/database-migration-assessment-for-oracle-extension) for Oracle to SQL pre-assessment and workload categorization. This extension is useful:

- if you're in the early phase of Oracle to SQL migration and need to do a high level workload assessment
- if you're interested in sizing an Azure SQL target for your Oracle workload
- if you're looking to understand feature migration parity

For detailed code assessment and conversion, continue with SSMA for Oracle.

## Recommended migration process

To successfully migrate objects and data from Oracle databases to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], Azure SQL Database, or Azure Synapse Analytics, use the following process:

1. [Work with SSMA Projects (OracleToSQL)](working-with-ssma-projects-oracletosql.md).

   After you create the project, you can set project conversion, migration, and type mapping options. For information about project settings, see [Setting Project Options (OracleToSQL)](setting-project-options-oracletosql.md). For information about how to customize data type mappings, see [Mapping Oracle and SQL Server Data Types (OracleToSQL)](mapping-oracle-and-sql-server-data-types-oracletosql.md).

1. [Connecting to Oracle Database (OracleToSQL)](connecting-to-oracle-database-oracletosql.md).

1. [Connecting to SQL Server (OracleToSQL)](connecting-to-sql-server-oracletosql.md).

1. [Mapping Oracle Schemas to SQL Server Schemas (OracleToSQL)](mapping-oracle-schemas-to-sql-server-schemas-oracletosql.md).

1. Optionally, [Assessing Oracle Schemas for Conversion (OracleToSQL)](assessing-oracle-schemas-for-conversion-oracletosql.md) to assess database objects for conversion and estimate the conversion time.

1. [Converting Oracle Schemas (OracleToSQL)](converting-oracle-schemas-oracletosql.md).

1. [Loading Converted Database Objects into SQL Server (OracleToSQL)](loading-converted-database-objects-into-sql-server-oracletosql.md).

   You have two options:

   - Save a script and run it in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]
   - Synchronize the database objects

1. [Migrating Oracle Data into SQL Server (OracleToSQL)](migrating-oracle-data-into-sql-server-oracletosql.md).

1. If necessary, update database applications.

## Related content

- [Install SSMA for Oracle (OracleToSQL)](installing-ssma-for-oracle-oracletosql.md)
- [Explore SSMA for Oracle interface](getting-started-with-ssma-for-oracle-oracletosql.md)
- [Migration guide: Oracle to Azure SQL Managed Instance](/azure/azure-sql/migration-guides/managed-instance/oracle-to-managed-instance-guide)
