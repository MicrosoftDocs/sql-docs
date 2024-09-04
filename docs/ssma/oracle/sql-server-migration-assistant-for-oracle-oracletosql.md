---
title: "SQL Server Migration Assistant for Oracle (OracleToSQL)"
description: Learn about SSMA for Oracle and follow step-by-step instructions for migrating Oracle databases to SQL Server.
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 07/10/2023
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.custom:
  - sql-migration-content
f1_keywords:
  - "ssma.oracle.general.f1"
---
# SQL Server Migration Assistant for Oracle (OracleToSQL)

[!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Migration Assistant (SSMA) for Oracle is a tool for migrating Oracle databases to [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] through [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] on Windows and Linux, or [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]. SSMA for Oracle converts Oracle database objects to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL objects, loads those objects into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL, and then migrates data from Oracle to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL.

This documentation introduces you to SSMA for Oracle and provides step-by-step instructions for migrating Oracle databases to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. The following table shows articles to help you learn more:

## Database Migration Assessment for Oracle extension

Try the [Database Migration Assessment for Oracle extension in Azure Data Studio](/azure-data-studio/extensions/database-migration-assessment-for-oracle-extension) for Oracle to SQL pre-assessment and workload categorization. This extension is useful if you are:

- in the early phase of Oracle to SQL migration and need to do a high level workload assessment
- interested in sizing an Azure SQL target for your Oracle workload
- looking to understand feature migration parity

For detailed code assessment and conversion, continue with SSMA for Oracle.

## Contents

| Section | Description |
| --- | --- |
| [What's New in SSMA for Oracle](./what-s-new-in-ssma-for-oracle-oracletosql.md) | What's new in this version of SSMA for Oracle. |
| [Installing SSMA for Oracle (OracleToSQL)](installing-ssma-for-oracle-oracletosql.md) | Contains articles that provide prerequisites and instructions for installing the SSMA for Oracle client and required components on the computer that is running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| [Getting Started with SSMA for Oracle (OracleToSQL)](getting-started-with-ssma-for-oracle-oracletosql.md) | Introduces the user interface, projects, and configuration options. |
| [Migrating Oracle Databases to SQL Server (OracleToSQL)](migrating-oracle-databases-to-sql-server-oracletosql.md) | Provides an overview of the conversion process and detailed information about each step in the process. |
| [Migration guide: Oracle to Azure SQL Managed Instance](/azure/azure-sql/migration-guides/managed-instance/oracle-to-managed-instance-guide) | This guide teaches you to migrate your Oracle schemas to Azure SQL Managed Instance by using SQL Server Migration Assistant for Oracle. |
| [User Interface Reference (OracleToSQL)](user-interface-reference-oracletosql.md) | Contains documentation for SSMA for Oracle dialog boxes. |
| [Working with SSMA for Oracle Console](working-with-ssma-for-oracle-console-oracletosql.md) | Contains documentation on the SSMA Console application. |
| [Getting SSMA for Oracle Assistance](../sql-server-migration-assistant.md) | Provides information about getting additional assistance. |
