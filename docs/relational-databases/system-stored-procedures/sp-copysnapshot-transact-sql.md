---
title: "sp_copysnapshot (Transact-SQL)"
description: sp_copysnapshot copies the snapshot folder of the specified publication to the folder listed in the destination folder.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_copysnapshot"
  - "sp_copysnapshot_TSQL"
helpviewer_keywords:
  - "sp_copysnapshot"
dev_langs:
  - "TSQL"
---
# sp_copysnapshot (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Copies the snapshot folder of the specified publication to the folder listed in the *@destination_folder*. This stored procedure is executed at the Publisher on the publication database. This stored procedure is useful for copying a snapshot to removable media.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_copysnapshot
    [ @publication = ] N'publication'
    , [ @destination_folder = ] N'destination_folder'
    [ , [ @subscriber = ] N'subscriber' ]
    [ , [ @subscriber_db = ] N'subscriber_db' ]
    [ , [ @publisher = ] N'publisher' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication whose snapshot contents are to be copied. *@publication* is **sysname**, with no default.

#### [ @destination_folder = ] N'*destination_folder*'

The name of the folder where the contents of the publication snapshot are to be copied. *@destination_folder* is **nvarchar(255)**, with no default. The *@destination_folder* can be an alternate location such as on another server, on a network drive, or on removable media.

#### [ @subscriber = ] N'*subscriber*'

The name of the Subscriber. *@subscriber* is **sysname**, with a default of `NULL`.

#### [ @subscriber_db = ] N'*subscriber_db*'

The name of the subscription database. *@subscriber_db* is **sysname**, with a default of `NULL`.

#### [ @publisher = ] N'*publisher*'

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_copysnapshot` is used in all types of replication.

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_copysnapshot`.

## Related content

- [Modify Snapshot Initialization Options for SQL Replication](../replication/snapshot-options.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
