---
title: "sys.sp_cdc_disable_table (Transact-SQL)"
description: "Disables change data capture for the specified source table and capture instance in the current database."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/13/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.sp_cdc_disable_table"
  - "sp_cdc_disable_table"
  - "sys.sp_cdc_disable_table_TSQL"
  - "sp_cdc_disable_table_TSQL"
helpviewer_keywords:
  - "sp_cdc_disable_table"
  - "sys.sp_cdc_disable_table"
  - "change data capture [SQL Server], disabling tables"
dev_langs:
  - "TSQL"
---
# sys.sp_cdc_disable_table (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Disables change data capture for the specified source table and capture instance in the current database. Change data capture isn't available in every edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_cdc_disable_table
    [ @source_schema = ] 'source_schema'
      , [ @source_name = ] 'source_name'
    [ , [ @capture_instance = ] { 'capture_instance' | 'all' } ]
[ ; ]
```

## Arguments

#### [ @source_schema = ] '*source_schema*'

The name of the schema in which the source table is contained. *@source_schema* is **sysname**, with no default, and can't be NULL.

*@source_schema* must exist in the current database.

#### [ @source_name = ] '*source_name*'

The name of the source table from which change data capture is to be disabled. *@source_name* is **sysname**, with no default, and can't be NULL.

*@source_name* must exist in the current database.

#### [ @capture_instance = ] { '*capture_instance*' | 'all' }

The name of the capture instance to disable for the specified source table. *@capture_instance* is **sysname** and can't be NULL.

When `'all'` is specified, all capture instances defined for *@source_name* are disabled.

## Return code values

`0` (success) or `1` (failure).

## Result sets

None.

## Remarks

`sys.sp_cdc_disable_table` drops the change data capture change table and system functions associated with the specified source table and capture instance. It deletes any rows associated with the specified capture instance from the change data capture system tables and sets the `is_tracked_by_cdc` column for the table entry in the [sys.tables](../system-catalog-views/sys-tables-transact-sql.md) catalog view to `0`.

## Permissions

Requires membership in the **db_owner** fixed database role.

## Examples

The following example disables change data capture for the `HumanResources.Employee` table.

```sql
USE AdventureWorks2022;
GO

EXECUTE sys.sp_cdc_disable_table
    @source_schema = N'HumanResources',
    @source_name = N'Employee',
    @capture_instance = N'HumanResources_Employee';
```

## See also

- [sys.sp_cdc_enable_table (Transact-SQL)](sys-sp-cdc-enable-table-transact-sql.md)
