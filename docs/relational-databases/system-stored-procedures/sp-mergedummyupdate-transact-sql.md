---
title: "sp_mergedummyupdate (Transact-SQL)"
description: "Does a dummy update on the given row so that it sends again during the next merge."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_mergedummyupdate_TSQL"
  - "sp_mergedummyupdate"
helpviewer_keywords:
  - "sp_mergedummyupdate"
dev_langs:
  - "TSQL"
---
# sp_mergedummyupdate (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Does a dummy update on the given row, so that it sends again during the next merge. This stored procedure can be executed at the Publisher, on the publication database, or at the Subscriber, on the subscription database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_mergedummyupdate
    [ @source_object = ] N'source_object'
    , [ @rowguid = ] 'rowguid'
[ ; ]
```

## Arguments

#### [ @source_object = ] N'*source_object*'

The name of the source object. *@source_object* is **nvarchar(386)**, with no default.

#### [ @rowguid = ] '*rowguid*'

The row identifier. *@rowguid* is **uniqueidentifier**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_mergedummyupdate` is used in merge replication.

`sp_mergedummyupdate` is useful if you write your own alternative to the Replication Conflict Viewer (`wzcnflct.exe`).

## Permissions

Only members of the **db_owner** fixed database role can execute `sp_mergedummyupdate`.

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
