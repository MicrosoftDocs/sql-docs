---
title: "sp_dropdevice (Transact-SQL)"
description: Drops a database device or backup device from SQL Server instance, deleting the entry from master.dbo.sysdevices.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/28/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_dropdevice_TSQL"
  - "sp_dropdevice"
helpviewer_keywords:
  - "backup devices [SQL Server], deleting"
  - "sp_dropdevice"
dev_langs:
  - "TSQL"
---
# sp_dropdevice (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Drops a database device or backup device from a [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] instance, deleting the entry from `master.dbo.sysdevices`.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dropdevice
    [ @logicalname = ] N'logicalname'
    [ , [ @delfile = ] 'delfile' ]
[ ; ]
```

## Arguments

#### [ @logicalname = ] N'*logicalname*'

The logical name of the database device or backup device as listed in `master.dbo.sysdevices.name`. *@logicalname* is **sysname**, with no default.

#### [ @delfile = ] '*delfile*'

Specifies whether the physical backup device file should be deleted. *@delfile* is **varchar(7)**, with a default of `NULL`. If specified as `DELFILE`, the physical backup device disk file is deleted.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

`sp_dropdevice` can't be used inside a transaction.

## Permissions

Requires membership in the **diskadmin** fixed server role.

## Examples

The following example drops the `tapedump1` tape dump device from the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

```sql
EXEC sp_dropdevice 'tapedump1';
```

## Related content

- [Backup Devices (SQL Server)](../backup-restore/backup-devices-sql-server.md)
- [Delete a Backup Device (SQL Server)](../backup-restore/delete-a-backup-device-sql-server.md)
- [sp_addumpdevice (Transact-SQL)](sp-addumpdevice-transact-sql.md)
- [sp_helpdb (Transact-SQL)](sp-helpdb-transact-sql.md)
- [sp_helpdevice (Transact-SQL)](sp-helpdevice-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
