---
title: DBCC SHOWRESULTCACHESPACEUSED (Transact-SQL)
description: DBCC SHOWRESULTCACHESPACEUSED shows the storage space used result set caching for an Azure Synapse Analytics database.
author: mstehrani
ms.author: emtehran
ms.reviewer: wiassaf, randolphwest
ms.date: 12/05/2022
ms.service: sql
ms.subservice: data-warehouse
ms.topic: "language-reference"
dev_langs:
  - "TSQL"
monikerRange: "= azure-sqldw-latest"
---

# DBCC SHOWRESULTCACHESPACEUSED (Transact-SQL)

[!INCLUDE [asa](../../includes/applies-to-version/asa.md)]

Shows the storage space used result set caching for an [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DBCC SHOWRESULTCACHESPACEUSED
[;]
```

> [!NOTE]  
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

## Remarks

The `DBCC SHOWRESULTCACHESPACEUSED` command doesn't take any parameters and returns the space used by the database where the command is run.

## Permissions

Requires **VIEW SERVER STATE** permission.

## Result sets

| Column | Data type | Description |
| --- | --- | --- |
| reserved_space | bigint | Total space used for the database, in KB. This number will change as the cached result set increases. |
| data_space | bigint | Space used for data, in KB. |
| index_space | bigint | Space used for indexes, in KB. |
| unused_space | bigint | Space that is part of the reserved space and not used, in KB. |

## See also

- [Performance tuning with result set caching](/azure/sql-data-warehouse/performance-tuning-result-set-caching)</br>
- [ALTER DATABASE SET Options (Transact-SQL)](../statements/alter-database-transact-sql-set-options.md?view=azure-sqldw-latest&preserve-view=true)</br>
- [ALTER DATABASE (Transact-SQL)](../statements/alter-database-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)</br>
- [SET RESULT SET CACHING (Transact-SQL)](../statements/set-result-set-caching-transact-sql.md)</br>
- [DBCC DROPRESULTSETCACHE  (Transact-SQL)](./dbcc-dropresultsetcache-transact-sql.md)
