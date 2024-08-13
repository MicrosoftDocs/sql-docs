---
title: "sp_copysubscription (Transact-SQL)"
description: sp_copysubscription copies a subscription database that's pull subscriptions, but no push subscriptions.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_copysubscription"
  - "sp_copysubscription_TSQL"
helpviewer_keywords:
  - "sp_copysubscription"
dev_langs:
  - "TSQL"
---
# sp_copysubscription (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Copies a subscription database that's pull subscriptions, but no push subscriptions. Only single file databases can be copied. This stored procedure is executed at the Subscriber on the subscription database.

> [!IMPORTANT]  
> [!INCLUDE [ssnotedepfutureavoid-md](../../includes/ssnotedepfutureavoid-md.md)] For merge publications that are partitioned using parameterized filters, we recommend using the new features of partitioned snapshots, which simplify the initialization of a large number of subscriptions. For more information, see [Create a Snapshot for a Merge Publication with Parameterized Filters](../replication/create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md). For publications that aren't partitioned, you can initialize a subscription with a backup. For more information, see [Initialize a Transactional Subscription Without a Snapshot](../replication/initialize-a-transactional-subscription-without-a-snapshot.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_copysubscription
    [ @filename = ] N'filename'
    [ , [ @temp_dir = ] N'temp_dir' ]
    [ , [ @overwrite_existing_file = ] overwrite_existing_file ]
[ ; ]
```

## Arguments

#### [ @filename = ] N'*filename*'

The string that specifies the complete path, including file name, to which a copy of the data file (`.mdf`) is saved. *@filename* is **nvarchar(260)**, with no default.

#### [ @temp_dir = ] N'*temp_dir*'

The name of the directory that contains the temp files. *@temp_dir* is **nvarchar(260)**, with a default of `NULL`. If `NULL`, the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] default data directory is used. The directory should have enough space to hold a file the size of all the subscriber database files combined.

#### [ @overwrite_existing_file = ] *overwrite_existing_file*

An optional Boolean flag that specifies whether or not to overwrite an existing file of the same name specified in *@filename*. *@overwrite_existing_file* is **bit**, with a default of `0`.

- If `1`, it overwrites the file specified by *@filename*, if it exists.
- If `0`, the stored procedure fails if the file exists, and the file isn't overwritten.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_copysubscription` is used in all types of replication to copy a subscription database to a file as an alternative to applying a snapshot at the Subscriber. The database must be configured to only support pull subscriptions. Users having appropriate permissions can make copies of the subscription database and then e-mail, copy, or transport the subscription file (`.msf`) to another Subscriber, where it can then be attached as a subscription.

The size of the subscription database being copied must be less than 2 gigabytes (GB).

`sp_copysubscription` is only supported for databases with client subscriptions and can't be executed when the database has server subscriptions.

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_copysubscription`.

## Related content

- [Modify Snapshot Initialization Options for SQL Replication](../replication/snapshot-options.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
