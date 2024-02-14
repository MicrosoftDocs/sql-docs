---
title: "sys.sp_rda_set_query_mode (Transact-SQL)"
description: Use sys.sp_rda_set_query_mode to specify if queries against the current Stretch-enabled database and its tables return local and remote data, or local data only.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/26/2023
ms.service: sql
ms.subservice: stored-procedures
ms.topic: "reference"
f1_keywords:
  - "sys.sp_rda_set_query_mode"
  - "sys.sp_rda_set_query_mode_TSQL"
helpviewer_keywords:
  - "sys.sp_rda_set_query_mode stored procedure"
dev_langs:
  - "TSQL"
---
# sys.sp_rda_set_query_mode (Transact-SQL)

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

Specifies whether queries against the current Stretch-enabled database and its tables return both local and remote data (the default), or local data only.

> [!IMPORTANT]  
> [!INCLUDE [stretch-database-deprecation](../../includes/stretch-database-deprecation.md)]

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_rda_set_query_mode
    [ @mode = ] @mode
    [ , [ @force = ] @force ]
[ ; ]
```

## Arguments

#### [ @mode = ] *@mode*

One of the following values:

- `DISABLED` All queries against Stretch-enabled tables fail.

- `LOCAL_ONLY` Queries against Stretch-enabled tables return local data only.

- `LOCAL_AND_REMOTE` Queries against Stretch-enabled tables return both local and remote data. This is the default behavior.

#### [ @force = ] *@force*

An optional **bit** value that you can set to 1 if you want to change query mode without validation.

## Return code values

`0` (success) or `> 0` (failure).

## Permissions

Requires **db_owner** permissions.

## Remarks

The following extended stored procedures also set the query mode for a Stretch-enabled database.

- `sp_rda_deauthorize_db`:

  After you run `sp_rda_deauthorize_db` , all queries against Stretch-enabled databases and tables fail. That is, the query mode is set to `DISABLED`. To exit this mode, do one of the following things.

  - Run [sys.sp_rda_reauthorize_db (Transact-SQL)](sys-sp-rda-reauthorize-db-transact-sql.md) to reconnect to the remote Azure database. This operation automatically resets the query mode to `LOCAL_AND_REMOTE`, which is the default behavior for Stretch Database. That is, queries return results from both local and remote data.

  - Run [sys.sp_rda_set_query_mode](sys-sp-rda-set-query-mode-transact-sql.md) with the `LOCAL_ONLY` argument to let queries continue to run against local data only.

- `sp_rda_reauthorize_db`:

  When you run [sys.sp_rda_reauthorize_db (Transact-SQL)](sys-sp-rda-reauthorize-db-transact-sql.md) to reconnect to the remote Azure database, this operation automatically resets the query mode to `LOCAL_AND_REMOTE`, which is the default behavior for Stretch Database. That is, queries return results from both local and remote data.

## Related content

- [sys.sp_rda_deauthorize_db (Transact-SQL)](sys-sp-rda-deauthorize-db-transact-sql.md)
- [Stretch Database](../../sql-server/stretch-database/stretch-database.md)
