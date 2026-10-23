---
title: Compare SQL Data Migration Tools
titleSuffix: SQL Server
description: Compare SQL data migration tools including Azure Migrate, Azure Database Migration Service (DMS), SQL Server Migration Assistant (SSMA), and Fabric Migration Assistant.
author: abhims14
ms.author: abhishekum
ms.reviewer: roblescarlos
ms.date: 04/20/2026
ms.service: sql
ms.subservice: migration-guide
ms.topic: product-comparison
helpviewer_keywords:
  - "migration, on-premises SQL Server"
---
# Compare SQL data migration tools

Microsoft provides tools and services for migrating databases to different target environments.

This article compares the capabilities of the migration and assessment tools available across SQL Server, Azure SQL, and Microsoft Fabric.

## Azure Database Migration Service (Azure DMS)

Azure Database Migration Service (Azure DMS) is a fully managed service that enables migrations from multiple database sources to Azure data platforms with minimal downtime.

It provides a migration pipeline that requires minimal user involvement during the migration process. You can access Azure DMS through the [Azure portal](https://portal.azure.com/#create/Microsoft.AzureDMS) or [PowerShell and Azure CLI](/azure/dms/migration-dms-powershell-cli).

For more information, see [Azure Database Migration Service documentation](/azure/dms/).

## Azure Migrate

Azure Migrate provides a centralized hub to discover and assess on-premises servers, infrastructure, applications, and data for migration to Azure.

Use Azure Migrate to discover SQL Server instances across your datacenter, assess application dependencies, and determine the readiness of these instances for migration to Azure SQL. Azure Migrate provides recommendations for Azure SQL deployment options, sizing based on workload performance needs, and monthly cost estimates that account for your licensing benefits.

Use Azure Migrate in the following scenarios:

- Assess and discover your SQL Server data estate.
- Get Azure SQL deployment recommendations, target sizing, and monthly estimates.
- Lift your entire data estate to SQL Server on Azure Virtual Machines.

For more information, see [Azure Migrate documentation](/azure/migrate/).

## SQL Server Migration Assistant (SSMA)

SQL Server Migration Assistant (SSMA) automates database migration to SQL Server and Azure SQL from other database engines.

Use SSMA to migrate from:

- Microsoft Access
- Db2
- MySQL
- Oracle
- SAP ASE

You can migrate to SQL Server on-premises, Azure SQL Managed Instance, Azure SQL Database, or SQL Server on Azure VMs.

For more information, see [SQL Server Migration Assistant](../../ssma/sql-server-migration-assistant.md).

## Fabric Migration Assistant

Fabric Migration Assistant is a built-in Fabric experience that migrates schema and data to Microsoft Fabric. It imports schema metadata, identifies compatibility issues, and provides guided fixes (including AI-powered assistance) before copying data to the target.

Use Fabric Migration Assistant in the following scenarios:

- Migrate SQL Server databases to SQL database in Microsoft Fabric.
- Migrate Azure Synapse Analytics dedicated SQL pools, SQL Server, and other SQL database platforms to Fabric Data Warehouse.

For more information, see:

- [Fabric Migration Assistant for SQL database](/fabric/database/sql/migration-assistant)
- [Fabric Migration Assistant for Data Warehouse](/fabric/data-warehouse/migration-assistant)

## Migration tool comparison

Use the following chart to compare capabilities of the SQL migration tools:

| Capability | Azure Migrate | SQL migration component | SSMA | Azure Arc | DMS (Azure portal / PowerShell / `az` cmdlet) | Fabric Migration Assistant |
| --- | --- | --- | --- | --- | --- | --- |
| Discover and assess SQL data estate | At scale | Yes | No | Yes | Using PowerShell / cmdlet | Assess only |
| Migrate SQL Server objects to SQL Database or SQL Managed Instance | No | No | No | Yes | Yes | No |
| Lift and shift SQL Server to SQL Server on Azure VM | Yes | No | No | No | Yes | No |
| Migrate (and/or upgrade) SQL Server to SQL Server on Azure VM | No | Yes | No | Yes | Yes | No |
| Migrate SQL Server to SQL database in Fabric | No | No | No | No | No | Yes |
| Migrate to Fabric Data Warehouse | No | No | No | No | No | Yes |
| Migrate non-SQL objects<br />(Oracle, Access, MySQL, Db2, SAP ASE) | No | No | Yes | No | No | No |

## Related content

- [Azure SQL migration guides](/azure/azure-sql/migration-guides/)
- [Azure Migrate](/azure/migrate/how-to-create-azure-sql-assessment)
- [SQL Server Migration Assistant](../../ssma/sql-server-migration-assistant.md)
- [Fabric Migration Assistant for SQL database](/fabric/database/sql/migration-assistant)
- [Fabric Migration Assistant for Data Warehouse](/fabric/data-warehouse/migration-assistant)
