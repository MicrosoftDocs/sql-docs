---
title: "sp_removedistpublisherdbreplication (Transact-SQL)"
description: sp_removedistpublisherdbreplication removes publishing metadata belonging to a specific publication at the Distributor.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_removedistpublisherdbreplication_TSQL"
  - "sp_removedistpublisherdbreplication"
helpviewer_keywords:
  - "sp_removedistpublisherdbreplication"
dev_langs:
  - "TSQL"
---
# sp_removedistpublisherdbreplication (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Removes publishing metadata belonging to a specific publication at the Distributor. This stored procedure is executed at the Distributor on the distribution database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_removedistpublisherdbreplication
    [ @publisher = ] N'publisher'
    , [ @publisher_db = ] N'publisher_db'
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

The name of the Publisher server. *@publisher* is **sysname**, with no default.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the publication database. *@publisher_db* is **sysname**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_removedistpublisherdbreplication` is used by transactional and snapshot replication.

`sp_removedistpublisherdbreplication` is used when a published database must be recreated without also dropping the distribution database. The following metadata is removed:

- All publication metadata.
- Metadata for all articles belong to the publication.
- Metadata for all subscriptions to the publication.
- Metadata for all replication agent jobs that belong to the publication.

## Permissions

Only members of the **sysadmin** fixed server role at the Distributor or members of the **db_owner** fixed database role in the distribution database can execute `sp_removedistpublisherdbreplication`.

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
