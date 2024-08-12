---
title: "sp_drop_openrowset_statistics (Transact-SQL)"
description: "The sp_drop_openrowset_statistics system stored procedure removes column statistics for a column in the OPENROWSET path of Azure Synapse SQL resources."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 11/28/2023
ms.service: azure-synapse-analytics
ms.topic: "reference"
f1_keywords:
  - "sp_drop_openrowset_statistics_TSQL"
  - "sp_drop_openrowset_statistics"
helpviewer_keywords:
  - "sp_drop_openrowset_statistics"
dev_langs:
  - "TSQL"
monikerRange: "=azure-sqldw-latest || =azuresqldb-mi-current"
---
# sp_drop_openrowset_statistics (Transact-SQL)

[!INCLUDE [asdbmi-asa-svrless-poolonly](../../includes/applies-to-version/asdbmi-asa-svrless-poolonly.md)]

Drops column statistics for a column in the `OPENROWSET` path of Azure Synapse serverless SQL pools. For more information, see [Statistics in Synapse SQL](/azure/synapse-analytics/sql/develop-tables-statistics). This procedure is also used by [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] for column statistics in external data sources via `OPENROWSET`.

There's no direct method to update existing statistics. Instead, drop and create statistics using [sp_create_openrowset_statistics](sp-create-openrowset-statistics.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_drop_openrowset_statistics
[ @stmt = ] N'stmt'
```

## Arguments

#### [ @stmt = ] N'*stmt*'

Specifies a Transact-SQL statement that returns column values to be used for statistics. You can use `TABLESAMPLE` within *@stmt* to specify samples of data to be used. If `TABLESAMPLE` isn't specified, `FULLSCAN` is used.

`<tablesample_clause> ::= TABLESAMPLE ( sample_number PERCENT )`

## Remarks

Statistics metadata isn't available for `OPENROWSET` columns.

## Permissions

Requires `ADMINISTER BULK OPERATIONS` or `ADMINISTER DATABASE BULK OPERATIONS` permissions.

## Examples

For usage scenarios and examples, review [Update statistics](/azure/synapse-analytics/sql/develop-tables-statistics#examples-update-statistics-1).

## Related content

- [sp_create_openrowset_statistics (Transact-SQL)](sp-create-openrowset-statistics.md)
- [Statistics in Synapse SQL](/azure/synapse-analytics/sql/develop-tables-statistics)
