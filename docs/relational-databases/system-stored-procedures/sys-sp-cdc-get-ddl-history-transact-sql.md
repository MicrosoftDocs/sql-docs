---
title: "sys.sp_cdc_get_ddl_history (Transact-SQL)"
description: "Returns the data definition language (DDL) change history associated with the specified capture instance since CDC was enabled for that instance."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_cdc_get_ddl_history"
  - "sp_cdc_get_ddl_history_TSQL"
  - "sys.sp_cdc_get_ddl_history_TSQL"
  - "sys.sp_cdc_get_ddl_history"
helpviewer_keywords:
  - "change data capture [SQL Server], querying metadata"
  - "sp_cdc_get_ddl_history"
  - "sys.sp_cdc_get_ddl_history"
dev_langs:
  - "TSQL"
---
# sys.sp_cdc_get_ddl_history (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns the data definition language (DDL) change history associated with the specified capture instance since change data capture was enabled for that capture instance. Change data capture isn't available in every edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_cdc_get_ddl_history [ @capture_instance = ] 'capture_instance'
[ ; ]
```

## Arguments

#### [ @capture_instance = ] '*capture_instance*'

The name of the capture instance associated with a source table. *@capture_instance* is **sysname** and can't be `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `source_schema` | **sysname** | Name of the source table schema. |
| `source_table` | **sysname** | Name of the source table. |
| `capture_instance` | **sysname** | Name of the capture instance. |
| `required_column_update` | **bit** | Indicates the DDL change required a column in the change table to be altered to reflect a data type change made to the source column. |
| `ddl_command` | **nvarchar(max)** | The DDL statement applied to the source table. |
| `ddl_lsn` | **binary(10)** | Log sequence number (LSN) associated with the DDL change. |
| `ddl_time` | **datetime** | Time associated with the DDL change. |

## Remarks

DDL modifications to the source table that change the source table column structure, such as adding or dropping a column, or changing the data type of an existing column, are maintained in the [cdc.ddl_history](../system-tables/cdc-ddl-history-transact-sql.md) table. These changes can be reported by using this stored procedure. Entries in `cdc.ddl_history` are made at the time the capture process reads the DDL transaction in the log.

## Permissions

Requires membership in the **db_owner** fixed database role to return rows for all capture instances in the database. For all other users, requires SELECT permission on all captured columns in the source table and, if a gating role for the capture instance was defined, membership in that database role.

## Examples

The following example adds a column to the source table `HumanResources.Employee` and then runs the `sys.sp_cdc_get_ddl_history` stored procedure to report the DDL changes that apply to the source table associated with the capture instance `HumanResources_Employee`.

```sql
USE AdventureWorks2022;
GO

ALTER TABLE HumanResources.Employee
ADD Test_Column INT NULL;
GO

-- Pause 10 seconds to allow the event to be logged.
WAITFOR DELAY '00:00:10';
GO

EXECUTE sys.sp_cdc_get_ddl_history
    @capture_instance = 'HumanResources_Employee';
GO
```

## Related content

- [sys.sp_cdc_help_change_data_capture (Transact-SQL)](sys-sp-cdc-help-change-data-capture-transact-sql.md)
