---
title: "sp_adjustpublisheridentityrange (Transact-SQL)"
description: Adjusts the identity range on a publication and reallocates new ranges based on the threshold value on the publication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 03/04/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_adjustpublisheridentityrange_TSQL"
  - "sp_adjustpublisheridentityrange"
helpviewer_keywords:
  - "sp_adjustpublisheridentityrange"
dev_langs:
  - "TSQL"
---
# sp_adjustpublisheridentityrange (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Adjusts the identity range on a publication and reallocates new ranges based on the threshold value on the publication. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_adjustpublisheridentityrange
    [ [ @publication = ] N'publication' ]
    [ , [ @table_name = ] N'table_name' ]
    [ , [ @table_owner = ] N'table_owner' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication in which new identity ranges are reallocated. *@publication* is **sysname**, with a default of `NULL`.

#### [ @table_name = ] N'*table_name*'

The name of the table in which new identity ranges are reallocated. *@table_name* is **sysname**, with a default of `NULL`.

#### [ @table_owner = ] N'*table_owner*'

The owner of the table at the Publisher. *@table_owner* is **sysname**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_adjustpublisheridentityrange` is used in all types of replication.

For a publication that has the auto identity range enabled, the Distribution Agent or Merge Agent is responsible for automatically adjusting the identity range in a publication based on its threshold value. However, if for some reason the Distribution Agent or Merge Agent wasn't run for some time, and identity range resources are consumed heavily to the point of threshold, you can call `sp_adjustpublisheridentityrange` to allocate a new range of values for a Publisher.

When you execute `sp_adjustpublisheridentityrange`, either *@publication* or *@table_name* must be specified. If both or neither are specified, an error is returned.

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_adjustpublisheridentityrange`.

## Related content

- [Replicate Identity Columns](../replication/publish/replicate-identity-columns.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
