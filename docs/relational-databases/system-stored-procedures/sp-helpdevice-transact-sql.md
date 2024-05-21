---
title: "sp_helpdevice (Transact-SQL)"
description: sp_helpdevice reports information about SQL Server backup devices.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_helpdevice"
  - "sp_helpdevice_TSQL"
helpviewer_keywords:
  - "sp_helpdevice"
dev_langs:
  - "TSQL"
---
# sp_helpdevice (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Reports information about [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] backup devices.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] We recommend that you use the [sys.backup_devices](../system-catalog-views/sys-backup-devices-transact-sql.md) catalog view instead

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpdevice [ [ @devname = ] N'devname' ]
[ ; ]
```

## Arguments

#### [ @devname = ] N'*devname*'

The name of the backup device for which information is reported. *@devname* is **sysname**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `device_name` | **sysname** | Logical device name. |
| `physical_name` | **nvarchar(260)** | Physical file name. |
| `description` | **nvarchar(255)** | Description of the device. |
| `status` | **int** | A number that corresponds to the status description in the `description` column. |
| `cntrltype` | **smallint** | Controller type of the device:<br /><br />`2` = Disk device<br />`5` = Tape device |
| `size` | **int** | Device size in 2-KB pages. |

## Remarks

If *@devname* is specified, `sp_helpdevice` displays information about the specified dump device. If *@devname* isn't specified, `sp_helpdevice` displays information about all dump devices in the `sys.backup_devices` catalog view.

Dump devices are added to the system by using `sp_addumpdevice`.

## Permissions

Requires membership in the **public** role.

## Examples

The following example reports information about all dump devices on an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

```sql
EXEC sp_helpdevice;
```

## Related content

- [sp_addumpdevice (Transact-SQL)](sp-addumpdevice-transact-sql.md)
- [sp_dropdevice (Transact-SQL)](sp-dropdevice-transact-sql.md)
- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
