---
title: "Database Schema Conversion Toolkit (Oracle to MS SQL) (Preview)"
description: Learn about Database Schema Conversion Toolkit (Oracle to MS SQL) and follow step-by-step instructions for migrating Oracle databases to Microsoft SQL platform.
ms.prod: azure-data-studio
ms.technology: azure-data-studio
author: "nahk-ivanov"
ms.author: "alexiva"
ms.reviewer: "maghan"
ms.topic: conceptual
ms.custom:
ms.date: "10/4/2021"
---

# Database Schema Conversion Toolkit (Oracle to MS SQL) (Preview)

The Database Schema Conversion Toolkit (Oracle to MS SQL) is an [Azure Data Studio](../../../what-is-azure-data-studio.md) extension for migrating Oracle database schemas to Microsoft SQL platform. While the [SQL Server Migration Assistant](../../../../ssma/oracle/sql-server-migration-assistant-for-oracle-oracletosql.md) is the Microsoft migration tool for enterprise customers and complex migrations (including Oracle customers with the ability to provision a Windows VM in their environment), the Database Schema Conversion Toolkit in Azure Data Studio enables the previously unsupported migration and modernization of Oracle workloads in exclusively Linux environments. Oracle data marts and reporting solutions are key opportunities to leverage the new Database Schema Conversion Toolkit.

This section introduces you to Database Schema Conversion Toolkit (Oracle to MS SQL) and provides step-by-step instructions for migrating Oracle databases.

## Supported sources

Oracle databases version 11.2.0.4 and above are supported as the source.

> [!NOTE]
> Database Schema Conversion Toolkit (Oracle to MS SQL) extension relies on the [Extension for Oracle](../../extension-for-oracle.md) to establish connection to the Oracle database.

## Supported targets

The Database Schema Conversion Toolkit (Oracle to MS SQL) extension currently supports the following targets:

- Microsoft SQL Server 2012 and above
- Azure SQL Database
- Azure SQL Managed Instance
- Azure Synapse Analytics

## Currently supported objects

The Database Schema Conversion Toolkit (Oracle to MS SQL) supports automated conversion of the following database objects:

- Users (schemas)
- Basic table definitions (columns, indexes, constraints)
- Sequences
- Synonyms

> [!NOTE]
> If your database contains a significant amount of objects that the Database Schema Conversion Toolkit does not currently support, you may consider using [SQL Server Migration Assistant for Oracle](../../../../ssma/oracle/sql-server-migration-assistant-for-oracle-oracletosql.md), which provides automated conversion for additional object types, but can only be used on Windows.
