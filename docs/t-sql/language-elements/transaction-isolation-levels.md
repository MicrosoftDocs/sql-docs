---
title: "Transaction Isolation Levels"
description: Learn about transaction isolation levels in the SQL Server Database Engine.
author: uc-msft
ms.author: umajay
ms.date: 07/15/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "locking [SQL Server], hints"
  - "isolation levels [SQL Server], metadata access"
  - "hints [SQL Server], locking"
dev_langs:
  - "TSQL"
---
# Transaction isolation levels

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't guarantee that lock hints will be honored in queries that access metadata through catalog views, compatibility views, information schema views, metadata-emitting built-in functions.

Internally, the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] only honors the `READ COMMITTED` isolation level for metadata access. If a transaction has an isolation level that is, for example, `SERIALIZABLE` and within the transaction, an attempt is made to access metadata by using catalog views or metadata-emitting built-in functions, those queries will run until they are completed as `READ COMMITTED`. However, under snapshot isolation, access to metadata might fail because of concurrent DDL operations. This is because metadata isn't versioned. Therefore, accessing the following under snapshot isolation might fail:

- Catalog views
- Compatibility views
- Information schema views
- Metadata-emitting built-in functions
- `sp_help` group of stored procedures
- [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client catalog procedures
- Dynamic management views and functions

For more information about isolation levels, see [SET TRANSACTION ISOLATION LEVEL](../statements/set-transaction-isolation-level-transact-sql.md).

The following table provides a summary of metadata access under various isolation levels.

| Isolation level | Supported | Honored |
| --- | --- | --- |
| `READ UNCOMMITTED` | No | Not guaranteed |
| `READ COMMITTED` | Yes | Yes |
| `REPEATABLE READ` | No | No |
| `SNAPSHOT ISOLATION` | No | No |
| `SERIALIZABLE` | No | No |
