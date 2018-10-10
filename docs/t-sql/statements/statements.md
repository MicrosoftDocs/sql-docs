---
title: "Statements | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "sql-data-warehouse, database-engine, pdw, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: d8d6f62a-e815-425c-a80e-a63fd34ec275
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Transact-SQL statements
[!INCLUDE[tsql-appliesto-ss2008-asdb-asdw-pdw-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

This reference topic summarizes the categories of statements for use with Transact-SQL (T-SQL). You can find all of the statements listed in the left-hand navigation.

## Backup and restore
The backup and restore statements provide ways to create backups and restore from backups.  For more information, see the [Backup and restore overview](../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md).

## Data Definition Language
Data Definition Language (DDL) statements defines data structures. Use these statements to create, alter, or drop data structures in a database.
- ALTER
- Collations
- CREATE
- DROP
- DISABLE TRIGGER
- ENABLE TRIGGER
- RENAME
- UPDATE STATISTICS

## Data Manipulation Language
Data Manipulation Language (DML) affect the information stored in the database. Use these statements to insert, update, and change the rows in the database.

- BULK INSERT
- DELETE
- INSERT
- UPDATE
- MERGE
- TRUNCATE TABLE

## Permissions statements
Permissions statements determine which users and logins can access data and perform operations. For more information about authentication and access, see the [Security center](../../relational-databases/security/security-center-for-sql-server-database-engine-and-azure-sql-database.md).

## Service Broker statements
Service Broker is a feature that provides native support for messaging and queuing applications. For more information, see [Service Broker](../../relational-databases/service-broker/event-notifications.md).

## Session settings
SET statements determine how the current session handles run time settings. For an overview, see [SET statements](set-statements-transact-sql.md).
