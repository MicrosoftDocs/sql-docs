---
title: "sp_estimated_rowsize_reduction_for_vardecimal (Transact-SQL)"
description: sp_estimated_rowsize_reduction_for_vardecimal estimates the reduction in the average size of rows if you enable vardecimal storage format on a table.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/16/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_estimated_rowsize_reduction_for_vardecimal"
  - "sp_estimated_rowsize_reduction_for_vardecimal_TSQL"
helpviewer_keywords:
  - "sp_estimated_rowsize_reduction_for_vardecimal"
  - "decimal data type, compressing"
  - "numeric data type, compressing"
  - "estimate decimal compression"
  - "table compression [SQL Server]"
dev_langs:
  - "TSQL"
---
# sp_estimated_rowsize_reduction_for_vardecimal (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Estimates the reduction in the average size of rows if you enable **vardecimal** storage format on a table. Use this number to estimate the overall reduction in the size of the table. Since the statistical sampling is used to compute the average reduction in the rowsize, regard it as an estimate only. In rare cases, rowsize might increase after enabling the **vardecimal** storage format.

> [!NOTE]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use `ROW` and `PAGE` compression instead. For more information, see [Data compression](../data-compression/data-compression.md). For compression effects on the size of tables and indexes, see [sp_estimate_data_compression_savings](sp-estimate-data-compression-savings-transact-sql.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_estimated_rowsize_reduction_for_vardecimal [ @table_name = ] N'table_name'
[ ; ]
```

## Arguments

#### [ @table_name = ] N'*table_name*'

The three part name of the table for which the storage format is to be changed. *@table_name* is **nvarchar(776)**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Result set

The following result set is returned to provide current and estimated table size information.

| Column name | Data type | Description |
| --- | --- | --- |
| `avg_rowlen_fixed_format` | **decimal (12,2)** | Represents the length of the row in fixed decimal storage format. |
| `avg_rowlen_vardecimal_format` | **decimal (12,2)** | Represents average rowsize when **vardecimal** storage format is used. |
| `row_count` | **int** | Number of rows in the table. |

## Remarks

Use `sp_estimated_rowsize_reduction_for_vardecimal` to estimate the savings that result if you enable a table for **vardecimal** storage format. For instance if the average size of the row can be reduced by 40%, you can potentially reduce the size of the table by 40%. You might not receive a space savings depending on the fill factor and the size of the row. For example, if you have a row that is 8,000 bytes long, and you reduce its size by 40%, you can still fit only one row on a data page, resulting in no savings.

If the results of `sp_estimated_rowsize_reduction_for_vardecimal` indicate that the table can grow, many rows in the table use nearly the entire precision of the decimal data types, and the addition of the small overhead needed for **vardecimal** storage format is greater than the savings from **vardecimal** storage format. In this rare case, don't enable **vardecimal** storage format.

If a table is enabled for **vardecimal** storage format, use `sp_estimated_rowsize_reduction_for_vardecimal` to estimate the average size of the row if **vardecimal** storage format is disabled.

## Permissions

Requires CONTROL permission on the table.

## Examples

The following example estimates the rowsize reduction if the `Production.WorkOrderRouting` table in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database is compressed.

```sql
USE AdventureWorks2022;
GO
EXEC sp_estimated_rowsize_reduction_for_vardecimal 'Production.WorkOrderRouting' ;
GO
```

## Related content

- [sp_db_vardecimal_storage_format (Transact-SQL)](sp-db-vardecimal-storage-format-transact-sql.md)
- [sp_tableoption (Transact-SQL)](sp-tableoption-transact-sql.md)
