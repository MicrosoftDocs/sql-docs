---
title: DBCC DROPRESULTSETCACHE (Transact-SQL)
description: DBCC DROPRESULTSETCACHE removes all result set cache entries from an Azure Synapse Analytics database.
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

# DBCC DROPRESULTSETCACHE (Transact-SQL)

[!INCLUDE [asa](../../includes/applies-to-version/asa.md)]

Removes all result set cache entries from an [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DBCC DROPRESULTSETCACHE
[;]
```

> [!NOTE]  
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

## Permissions

Requires membership in the DB_OWNER fixed server role.

## Remarks

- This command empties the result set cache for all queries.

- Turning OFF the result set cache feature for a database also deletes all cached results.

- Pausing a database enabled with result set caching won't delete the cached results.

## See also

- [Performance tuning with result set caching](/azure/sql-data-warehouse/performance-tuning-result-set-caching)</br>
- [ALTER DATABASE SET Options (Transact-SQL)](../statements/alter-database-transact-sql-set-options.md?view=azure-sqldw-latest&preserve-view=true)</br>
- [ALTER DATABASE (Transact-SQL)](../statements/alter-database-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)</br>
- [SET RESULT SET CACHING (Transact-SQL)](../statements/set-result-set-caching-transact-sql.md)</br>
- [DBCC SHOWRESULTCACHESPACEUSED (Transact-SQL)](./dbcc-showresultcachespaceused-transact-sql.md)
