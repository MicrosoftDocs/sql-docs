---
title: Transact-SQL statements
description: Transact-SQL statements
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 04/17/2020
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "Alter_TSQL"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Transact-SQL statements

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

A SQL statement is an atomic unit of work and either completely succeeds or completely fails. A SQL statement is a set of instruction that consists of identifiers, parameters, variables, names, data types, and SQL reserved words that compiles successfully. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] creates an *implicit* transaction for a SQL statement if a `BeginTransaction` command does not specify the start of a transaction. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] always commits an implicit transaction if the statement succeeds, and rolls back an implicit transaction if the command fails.  

There are many types of statements. Perhaps the most important is the [SELECT](../queries/select-transact-sql.md) that retrieves rows from the database and enables the selection of one or many rows or columns from one or many tables in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This article summarizes the categories of statements for use with Transact-SQL (T-SQL) in addition to the `SELECT` statement. You can find all of the statements listed in the left-hand navigation.

## Backup and restore

The backup and restore statements provide ways to create backups and restore from backups.  For more information, see the [Backup and restore overview](../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md).

## Data Definition Language

Data Definition Language (DDL) statements defines data structures. Use these statements to create, alter, or drop data structures in a database. These statements include:

- ALTER
- Collations
- CREATE
- DROP
- DISABLE TRIGGER
- ENABLE TRIGGER
- RENAME
- UPDATE STATISTICS
- TRUNCATE TABLE

## Data Manipulation Language

Data Manipulation Language (DML) affect the information stored in the database. Use these statements to insert, update, and change the rows in the database.

- BULK INSERT
- DELETE
- INSERT
- SELECT
- UPDATE
- MERGE

## Permissions statements

Permissions statements determine which users and logins can access data and perform operations. For more information about authentication and access, see the [Security center](../../relational-databases/security/security-center-for-sql-server-database-engine-and-azure-sql-database.md).

## Service Broker statements

Service Broker is a feature that provides native support for messaging and queuing applications. For more information, see [Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md).

## Session settings

SET statements determine how the current session handles run time settings. For an overview, see [SET statements](set-statements-transact-sql.md).
