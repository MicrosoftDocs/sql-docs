---
title: "sp_browsemergesnapshotfolder (Transact-SQL)"
description: Returns the complete path for the latest snapshot generated for a merge publication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_browsemergesnapshotfolder"
  - "sp_browsemergesnapshotfolder_TSQL"
helpviewer_keywords:
  - "sp_browsemergesnapshotfolder"
dev_langs:
  - "TSQL"
---
# sp_browsemergesnapshotfolder (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns the complete path for the latest snapshot generated for a merge publication. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_browsemergesnapshotfolder [ @publication = ] N'publication'
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `snapshot_folder` | **nvarchar(2000)** | Full path to the snapshot directory. |

## Remarks

`sp_browsemergesnapshotfolder` is used in merge replication.

If the publication is set up to generate snapshot files in both the Publisher working directory and Publisher snapshot folder, the result set contains two rows: the first row contains the publication snapshot folder, and the second row contains the publisher working directory.

`sp_browsemergesnapshotfolder` is useful for determining the directory where the merge snapshot files are generated. This folder/path and its contents can then be copied to removable media, and the snapshot used to synchronize a subscription from an alternate snapshot location.

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_browsemergesnapshotfolder`.

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
