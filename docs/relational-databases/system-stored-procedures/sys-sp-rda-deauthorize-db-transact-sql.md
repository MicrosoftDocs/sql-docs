---
title: "sys.sp_rda_deauthorize_db (Transact-SQL)"
description: Removes the authenticated connection between a local Stretch-enabled database and the remote Azure database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/26/2023
ms.service: sql
ms.subservice: stored-procedures
ms.topic: "reference"
f1_keywords:
  - "sys.sp_rda_deauthorize_db"
  - "sys.sp_rda_deauthorize_db_TSQL"
helpviewer_keywords:
  - "sys.sp_rda_deauthorize_db stored procedure"
dev_langs:
  - "TSQL"
---
# sys.sp_rda_deauthorize_db (Transact-SQL)

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

Removes the authenticated connection between a local Stretch-enabled database and the remote Azure database. Run `sp_rda_deauthorize_db`  when the remote database is unreachable or in an inconsistent state and you want to change query behavior for all Stretch-enabled tables in the database.

> [!IMPORTANT]  
> [!INCLUDE [stretch-database-deprecation](../../includes/stretch-database-deprecation.md)]

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_rda_deauthorize_db
[ ; ]
```

## Return code values

`0` (success) or `> 0` (failure).

## Permissions

Requires **db_owner** permissions.

## Remarks

After you run `sp_rda_deauthorize_db`, all queries against Stretch-enabled databases and tables fail. That is, the query mode is set to `DISABLED`. To exit this mode, do one of the following things:

- Run [sys.sp_rda_reauthorize_db (Transact-SQL)](sys-sp-rda-reauthorize-db-transact-sql.md) to reconnect to the remote Azure database. This operation automatically resets the query mode to `LOCAL_AND_REMOTE`, which is the default behavior for Stretch Database. That is, queries return results from both local and remote data.

- Run [sys.sp_rda_set_query_mode (Transact-SQL)](sys-sp-rda-set-query-mode-transact-sql.md) with the `LOCAL_ONLY` argument to let queries continue to run against local data only.

## Related content

- [sys.sp_rda_set_query_mode (Transact-SQL)](sys-sp-rda-set-query-mode-transact-sql.md)
- [sys.sp_rda_reauthorize_db (Transact-SQL)](sys-sp-rda-reauthorize-db-transact-sql.md)
- [Stretch Database](../../sql-server/stretch-database/stretch-database.md)
