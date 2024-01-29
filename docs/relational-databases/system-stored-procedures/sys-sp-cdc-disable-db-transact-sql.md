---
title: "sys.sp_cdc_disable_db (Transact-SQL)"
description: "Disables change data capture (CDC) for the current database."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/13/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_cdc_disable_db"
  - "sys.sp_cdc_disable_db_TSQL"
  - "sp_cdc_disable_db_TSQL"
  - "sys.sp_cdc_disable_db"
helpviewer_keywords:
  - "sp_cdc_disable_db"
  - "sys.sp_cdc_disable_db"
  - "change data capture [SQL Server], disabling databases"
dev_langs:
  - "TSQL"
---
# sys.sp_cdc_disable_db (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Disables change data capture (CDC) for the current database. Change data capture isn't available in every edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_cdc_disable_db
[ ; ]
```

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

`sys.sp_cdc_disable_db` disables change data capture for all tables in the database currently enabled. All system objects related to change data capture, such as change tables, jobs, stored procedures and functions, are dropped. The `is_cdc_enabled` column for the database entry in the [sys.databases](../system-catalog-views/sys-databases-transact-sql.md) catalog view is set to `0`.

If there are many capture instances defined for the database at the time change data capture is disabled, a long running transaction can cause the execution of `sys.sp_cdc_disable_db` to fail. This problem can be avoided by disabling the individual capture instances by using `sys.sp_cdc_disable_table` before running `sys.sp_cdc_disable_db`.

## Permissions

Requires membership in the **sysadmin** fixed server role for change data capture on Azure SQL Managed Instance or SQL Server. Requires membership in the **db_owner** for Change Data Capture on Azure SQL Database.

## Examples

The following example disables change data capture for the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database.

```sql
USE AdventureWorks2022;
GO

EXECUTE sys.sp_cdc_disable_db;
GO
```

## Related content

- [sys.sp_cdc_enable_db (Transact-SQL)](sys-sp-cdc-enable-db-transact-sql.md)
- [sys.sp_cdc_disable_table (Transact-SQL)](sys-sp-cdc-disable-table-transact-sql.md)
