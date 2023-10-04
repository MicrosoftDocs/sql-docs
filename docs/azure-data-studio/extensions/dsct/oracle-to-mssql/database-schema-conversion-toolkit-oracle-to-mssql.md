---
title: "Database Schema Conversion Toolkit (Preview)"
description: Learn about Database Schema Conversion Toolkit and follow step-by-step instructions for migrating Oracle databases to Microsoft SQL platform.
author: tdoshin
ms.author: timioshin
ms.reviewer: maghan
ms.date: "10/4/2021"
ms.service: azure-data-studio
ms.topic: conceptual
---

# Database Schema Conversion Toolkit (Preview)

The Database Schema Conversion Toolkit is an [Azure Data Studio](../../../what-is-azure-data-studio.md) extension for converting Oracle database schemas to Microsoft SQL platform. It helps in converting majority of the database storage objects and code objects to a format compatible with the target database. The Database Schema Conversion Toolkit in Azure Data Studio enables the previously unsupported migration and modernization of Oracle workloads in exclusively Linux environments compared to [SQL Server Migration Assistant](../../../../ssma/oracle/sql-server-migration-assistant-for-oracle-oracletosql.md).

This section introduces you to Database Schema Conversion Toolkit and provides step-by-step instructions for converting Oracle databases schema.

## Supported sources

Oracle databases version 11.2.0.4 and above are supported as the source.

> [!NOTE]
> Database Schema Conversion Toolkit extension is dependent on the [Extension for Oracle](../../extension-for-oracle.md) to establish connection to the Oracle database and [SQL Database Projects extension](../../sql-database-project-extension.md) to display the converted schema output and its deployment on target.

## Supported targets

The Database Schema Conversion Toolkit extension currently supports the following targets:

- Microsoft SQL Server 2017 and above
- Azure SQL Database
- Azure SQL Managed Instance

## Currently supported objects

The Database Schema Conversion Toolkit supports automated conversion of the following database objects from Oracle to Microsoft SQL platform:

- Basic table definitions (columns, indexes, primary key, foreign key, unique and check constraints)
- Procedures
- Views
- Triggers
- Sequences
- Synonyms
- Basic Routines and calls to user defined routines
- Basic DDL, TCL statements including built-in functions
- Programability constructs like Exceptions, CTEs
- Support for expressions like CASE, LOOPs, Conditions

> [!NOTE]
> If your database contains a significant amount of objects that the Database Schema Conversion Toolkit does not currently support, you may consider using [SQL Server Migration Assistant for Oracle](../../../../ssma/oracle/sql-server-migration-assistant-for-oracle-oracletosql.md), which provides automated conversion for additional object types, but can only be used on Windows.
