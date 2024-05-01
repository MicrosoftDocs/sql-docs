---
title: "sp_addumpdevice (Transact-SQL)"
description: Adds a backup device to an instance of SQL Server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_addumpdevice_TSQL"
  - "sp_addumpdevice"
helpviewer_keywords:
  - "backup devices [SQL Server], defining"
  - "sp_addumpdevice"
dev_langs:
  - "TSQL"
---
# sp_addumpdevice (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Adds a backup device to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addumpdevice
    [ @devtype = ] 'devtype'
    , [ @logicalname = ] N'logicalname'
    , [ @physicalname = ] N'physicalname'
    [ , [ @cntrltype = ] cntrltype ]
    [ , [ @devstatus = ] 'devstatus' ]
[ ; ]
```

## Arguments

#### [ @devtype = ] '*devtype*'

The type of backup device. *@devtype* is **varchar(20)**, with no default, and can be one of the following values.

| Value | Description |
| --- | --- |
| `disk` | Hard disk file as a backup device. |
| `tape` | Any tape devices supported by [!INCLUDE [msCoName](../../includes/msconame-md.md)] Windows.<br /><br />**Note**: Support for tape backup devices will be removed in a future version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. |

#### [ @logicalname = ] N'*logicalname*'

The logical name of the backup device used in the BACKUP and RESTORE statements. *@logicalname* is **sysname**, with no default, and can't be `NULL`.

#### [ @physicalname = ] N'*physicalname*'

The physical name of the backup device. *@physicalname* is **nvarchar(260)**, with no default, and can't be `NULL`. Physical names must follow the rules for operating system file names, or universal naming conventions for network devices, and must include a full path.

When creating a backup device on a remote network location, be sure that the name under which the [!INCLUDE [ssDE](../../includes/ssde-md.md)] was started has appropriate write capabilities on the remote computer.

If you add a tape device, this parameter must be the physical name assigned to the local tape device by Windows; for example, `\\.\TAPE0` for the first tape device on the computer. The tape device must be attached to the server computer; it can't be used remotely. Enclose names that contain nonalphanumeric characters in quotation marks.

> [!NOTE]  
> This procedure enters the specified physical name into the catalog. The procedure doesn't attempt to access or create the device.

#### [ @cntrltype = ] *cntrltype*

Obsolete. If specified, this parameter is ignored. Supported for backward compatibility. New uses of `sp_addumpdevice` should omit this parameter.

#### [ @devstatus = ] '*devstatus*'

Obsolete. If specified, this parameter is ignored. Supported for backward compatibility. New uses of `sp_addumpdevice` should omit this parameter.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

`sp_addumpdevice` adds a backup device to the `sys.backup_devices` catalog view. The device can then be referred to logically in `BACKUP` and `RESTORE` statements. `sp_addumpdevice` doesn't perform any access to the physical device. Access to the specified device only occurs when a `BACKUP` or `RESTORE` statement is performed. Creating a logical backup device can simplify `BACKUP` and `RESTORE` statements, where specifying the device name is an alternative using a `TAPE =` or `DISK =` clause to specify the device path.

Ownership and permissions problems can interfere with the use of disk or file backup devices. Make sure that appropriate file permissions are given to the Windows account under which the [!INCLUDE [ssDE](../../includes/ssde-md.md)] was started.

The [!INCLUDE [ssDE](../../includes/ssde-md.md)] supports tape backups to tape devices supported by Windows. For more information about Windows-supported tape devices, see the hardware compatibility list for Windows. To view the tape devices available on the computer, use [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)].

Use only the recommended tapes for the specific tape drive suggested by the drive manufacturer. If you're using digital audio tape (DAT) drives, use computer-grade DAT tapes (Digital Data Storage (DDS)).

`sp_addumpdevice` can't be executed inside a transaction.

To delete a device, use [sp_dropdevice](sp-dropdevice-transact-sql.md) or [Delete a Backup Device (SQL Server)](../backup-restore/delete-a-backup-device-sql-server.md).

## Permissions

Requires membership in the **diskadmin** fixed server role.

Requires permission to write to the disk.

## Examples

### A. Add a disk dump device

The following example adds a disk backup device named `mydiskdump`, with the physical name `C:\dump\dump1.bak`.

```sql
USE master;
GO
EXEC sp_addumpdevice 'disk', 'mydiskdump', 'C:\dump\dump1.bak';
```

### B. Add a network disk backup device

The following example shows adding a remote disk backup device called `networkdevice`. The name under which the [!INCLUDE [ssDE](../../includes/ssde-md.md)] was started must have permissions to that remote file (`\\<servername>\<sharename>\<path>\<filename>.bak`).

```sql
USE master;
GO
EXEC sp_addumpdevice 'disk', 'networkdevice',
    '\\<servername>\<sharename>\<path>\<filename>.bak';
```

### C. Add a tape backup device

The following example adds the `tapedump1` device with the physical name `\\.\tape0`.

```sql
USE master;
GO
EXEC sp_addumpdevice 'tape', 'tapedump1', '\\.\tape0';
```

### D. Back up to a logical backup device

The following example creates a logical backup device, `AdvWorksData`, for a backup disk file. The example then backs up the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database to this logical backup device.

```sql
USE master;
GO
EXEC sp_addumpdevice
    'disk',
    'AdvWorksData',
    'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\BACKUP\AdvWorksData.bak';
GO
BACKUP DATABASE AdventureWorks2022
 TO AdvWorksData WITH FORMAT;
GO
```

## Related content

- [Backup Devices (SQL Server)](../backup-restore/backup-devices-sql-server.md)
- [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)
- [Define a Logical Backup Device for a Disk File (SQL Server)](../backup-restore/define-a-logical-backup-device-for-a-disk-file-sql-server.md)
- [Define a Logical Backup Device for a Tape Drive (SQL Server)](../backup-restore/define-a-logical-backup-device-for-a-tape-drive-sql-server.md)
- [RESTORE Statements (Transact-SQL)](../../t-sql/statements/restore-statements-transact-sql.md)
- [sp_dropdevice (Transact-SQL)](sp-dropdevice-transact-sql.md)
- [sys.backup_devices (Transact-SQL)](../system-catalog-views/sys-backup-devices-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
