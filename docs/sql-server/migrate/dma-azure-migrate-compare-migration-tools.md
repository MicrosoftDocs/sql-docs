---
title: "Compare SQL data migration tools"
titleSuffix: SQL Server
description: "Compare SQL data migration tools to determine which tool best suits your business needs, such as Data Migration Assistant (DMA), Azure Migrate, Azure Database Migration Service, SQL Server Migration Assistant (SSMA), Database Experimentation Assistant (DEA). "
author: croblesm
ms.author: roblescarlos
ms.reviewer: randolphwest, maghan
ms.date: 03/24/2023
ms.service: sql
ms.subservice: dma
ms.topic: conceptual
helpviewer_keywords:
  - "Data Migration Assistant, on-premises SQL Server"
---

# Compare SQL data migration tools

Microsoft provides a suite of tools and services to assist users with migrating various source databases to different target environments.

This article briefly overviews the tools available to migrate to SQL Server and Azure SQL.

## Azure Database Migration Service (DMS)

Azure Database Migration Service enables seamless migrations from multiple database sources to Azure Data platforms with minimal downtime. The Database Migration Service provides a resilient and reliable migration pipeline that requires minimal user involvement during the overall migration process.

Use the Database Migration Service in the following scenarios:

- Migrate both databases to Azure SQL, especially at scale, and for extensive (in terms of number and size of databases) migrations.
- Migrate databases to Azure Database.

For more information, visit [Azure Database Migration Service documentation](/azure/dms/).

## Azure Migrate

Azure Migrate provides a centralized hub to discover and assess on-premises servers, infrastructure, applications, and data to Azure at scale. Azure Migrate offers a unified migration across your servers, databases, and applications.

Use Azure Migrate to discover all your SQL Server instances across your datacenter, assess application dependencies, understand the readiness of these SQL Server instances migrating to Azure SQL, and get Microsoft recommendations, such as the optimal Azure SQL deployment option and the correct SKU that can fit the performance needs for your workloads. You can also get the monthly estimates running these databases on Azure SQL to cater to your licensing benefits.

Use Azure Migrate in the following scenarios:

- Assess and discover your SQL Server data estate.
- Get Azure SQL deployment recommendations, target sizing, and monthly estimates.
- Lift your entire data estate to SQL Server on Azure VMs.

For more information, visit [Azure Migrate documentation](/azure/migrate/).

## Azure SQL migration extension for Azure Data Studio

Azure SQL Migration Extension for Azure Data Studio is a powerful tool that simplifies the process of migrating SQL Server databases to Azure SQL Database. This extension is designed to work with Azure Data Studio, a cross-platform database development tool that enables developers to work with SQL Server, Azure SQL Database, and other data platforms. The extension provides a streamlined user interface that guides users through the migration process, offering several options for customization and optimization.

Use Azure Migrate in the following scenarios:

- Easily migrate SQL Server databases to Azure SQL Database without the need for complex scripts or manual steps.
- Migrate small or large databases.

## Database Experimentation Assistant (DEA)

Database Experimentation Assistant (DEA) is an experimentation solution for SQL Server upgrades. DEA can help you evaluate a targeted version of SQL Server for a specific workload. Customers upgrading from SQL Server 2005 and later versions, can use the analysis metrics that the tool provides.

Use the Database Experimentation Assistant in the following scenario:

- Capture the workload of a source SQL Server environment and evaluate the workload on a source SQL Server to prepare for migration.
- Identify compatibility errors and possible degraded queries for your SQL Server migration scenario.

For more information, visit [Database Experimentation Assistant documentation](../../dea/database-experimentation-assistant-overview.md).

## Data Migration Assistant (DMA)

The Data Migration Assistant (DMA) helps you upgrade to a modern data platform by detecting compatibility issues that can impact database functionality in your new version of SQL Server or Azure SQL Database. DMA recommends performance and reliability improvements for your target environment and allows you to move your schema, data, and uncontained objects from your source server to your target server.

Use DMA in the following scenarios:

- Upgrade of SQL Server 2005 and later versions to SQL Server 2012, SQL Server 2014, SQL Server 2016, SQL Server 2017 and later on Windows and Linux, and SQL Server on Azure VM.
- Detect compatibility issues that can impact database functionality on a newer target version of SQL Server or Azure SQL and provide mitigating steps.
- Move the schema, data, and uncontained objects from a source server to SQL Server or Azure SQL.

For more information, visit [Data Migration Assistant documentation](../../dma/dma-overview.md).

## SQL Server Migration Assistant (SSMA)

SQL Server Migration Assistant (SSMA) is a tool designed to automate database migration to SQL Server and Azure SQL from alternative database engines.

Use SSMA in the following scenario:

- Migrate Microsoft Access, DB2, MySQL, Oracle, and SAP ASE databases to SQL Server.
- Migrate Microsoft Access, DB2, MySQL, Oracle, and SAP ASE databases to Azure SQL.

For more information, visit [SQL Server Migration Assistant documentation](../../ssma/sql-server-migration-assistant.md).

## Quick comparison

Use the following chart to compare capabilities of the SQL migration tools:

| Capability | Azure Migrate | DMA | SSMA | DMS | DEA | Azure Data Studio extension |
| ---------- | ------------- | --- | ---- | --- | --- | ----------------------------|
| Discover and assess SQL data estate | At scale | Yes | No | No | No | Yes |
| Migrate SQL Server objects to SQL Database or SQL Managed Instance | No | Yes | No | Yes | No | Yes |
| Lift and shift SQL Server to SQL Server on Azure VM | Yes | No | No | No | No | No |
| Migrate (and/or upgrade) SQL Server to SQL Server on Azure VM | No | Yes | No | No | No | No |
| Migrate non-SQL objects<br />(Oracle, Access, DB2, and so on) | No | No | Yes | No | No | No |
| Migrate open source databases<br />(MySQL, PostgreSQL, MariaDB, and so on) | No | No | No | Yes | No | No |
| Compare workloads between source and target SQL Server | No | No | No | No | Yes | Yes |

## Next steps

- Get started with migrating to [SQL Server](../../ssma/sql-server-migration-assistant.md) from another database engine, migrate to [Azure SQL](/azure/azure-sql/migration-guides/), or assess your SQL data estate with [Azure Migrate](/azure/migrate/how-to-create-azure-sql-assessment).
