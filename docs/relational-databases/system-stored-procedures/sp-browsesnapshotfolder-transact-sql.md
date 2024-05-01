---
title: "sp_browsesnapshotfolder (Transact-SQL)"
description: sp_browsesnapshotfolder returns the complete path for the latest snapshot generated for a publication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 03/04/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_browsesnapshotfolder"
  - "sp_browsesnapshotfolder_TSQL"
helpviewer_keywords:
  - "sp_browsesnapshotfolder"
dev_langs:
  - "TSQL"
---
# sp_browsesnapshotfolder (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Returns the complete path for the latest snapshot generated for a publication. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_browsesnapshotfolder
    [ @publication = ] N'publication'
    [ , [ @subscriber = ] N'subscriber' ]
    [ , [ @subscriber_db = ] N'subscriber_db' ]
    [ , [ @publisher = ] N'publisher' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication that contains the article. *@publication* is **sysname**, with no default.

#### [ @subscriber = ] N'*subscriber*'

The name of the Subscriber. *@subscriber* is **sysname**, with a default of `NULL`.

#### [ @subscriber_db = ] N'*subscriber_db*'

The name of the subscription database. *@subscriber_db* is **sysname**, with a default of `NULL`.

#### [ @publisher = ] N'*publisher*'

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `snapshot_folder` | **nvarchar(512)** | Full path to the snapshot directory. |

## Remarks

`sp_browsesnapshotfolder` is used in snapshot replication and transactional replication.

If the *@subscriber* and *@subscriber_db* fields are left `NULL`, the stored procedure returns the snapshot folder of the most recent snapshot it can find for the publication. If the *@subscriber* and *@subscriber_db* fields are specified, the stored procedure returns the snapshot folder for the specified subscription. If a snapshot hasn't been generated for the publication, an empty result set is returned.

If the publication is set up to generate snapshot files in both the Publisher working directory and Publisher snapshot folder, the result set contains two rows. The first row contains the publication snapshot folder and the second row contains the publisher working directory. `sp_browsesnapshotfolder` is useful for determining the directory where snapshot files are generated.

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_browsesnapshotfolder`.

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
