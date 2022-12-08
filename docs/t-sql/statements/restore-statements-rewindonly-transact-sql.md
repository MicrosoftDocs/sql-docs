---
title: "RESTORE REWINDONLY (Transact-SQL)"
description: RESTORE Statements - REWINDONLY (Transact-SQL)
author: MikeRayMSFT
ms.author: mikeray
ms.date: "08/10/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "RESTORE_REWINDONLY_TSQL"
  - "RESTORE REWINDONLY"
helpviewer_keywords:
  - "closing backup devices"
  - "backup devices [SQL Server], rewinding"
  - "media [SQL Server]"
  - "open back devices"
  - "rewinding backup devices"
  - "RESTORE REWINDONLY statement"
dev_langs:
  - "TSQL"
---
# RESTORE Statements - REWINDONLY (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Rewinds and closes specified tape devices that were left open by BACKUP or RESTORE statements executed with the NOREWIND option. This command is supported only for tape devices.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
  
RESTORE REWINDONLY   
FROM <backup_device> [ ,...n ]  
[ WITH {UNLOAD | NOUNLOAD}]  
}   
[;]  
  
<backup_device> ::=  
{   
   { logical_backup_device_name |  
      @logical_backup_device_name_var }  
   | TAPE = { 'physical_backup_device_name' |  
       @physical_backup_device_name_var }   
}   
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 **\<backup_device> ::=** 
  
 Specifies the logical or physical backup devices to use for the restore operation.  
  
 { *logical_backup_device_name* | **@**_logical\_backup\_device\_name\_var_ }  
 Is the logical name, which must follow the rules for identifiers, of the backup devices created by **sp_addumpdevice** from which the database is restored. If supplied as a variable (**@**_logical\_backup\_device\_name\_var_), the backup device name can be specified either as a string constant (**@**_logical\_backup\_device\_name\_var_ = _logical\_backup\_device\_name_) or as a variable of character string data type, except for the **ntext** or **text** data types.  
  
 {DISK | TAPE } **=** { **'**_physical\_backup\_device\_name_**'** | **@**_physical\_backup\_device\_name\_var_ }  
 Allows backups to be restored from the named disk or tape device. The device types of disk and tape should be specified with the actual name (for example, complete path and file name) of the device: DISK = 'C:\Program Files\Microsoft SQL Server\MSSQL\BACKUP\Mybackup.bak' or TAPE = '\\\\.\TAPE0'. If specified as a variable (**@**_physical\_backup\_device\_name\_var_), the device name can be specified either as a string constant (**@**_physical\_backup\_device\_name\_var_ = '*physical_backup_device_name*') or as a variable of character string data type, except for the **ntext** or **text** data types.  
  
 If using a network server with a UNC name (which must contain machine name), specify a device type of disk. For more information about using UNC names, see [Backup Devices &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-devices-sql-server.md).  
  
 The account under which you are running Microsoft SQL Server must have READ access to the remote computer or network server in order to perform a RESTORE operation.  
  
 *n*  
 Is a placeholder that indicates multiple backup devices and logical backup devices can be specified. The maximum number of backup devices or logical backup devices is **64**.  
  
 Whether a restore sequence requires as many backup devices as were used to create the media set to which the backups belong, depends on whether the restore is offline or online. Offline restore allows a backup to be restored using fewer devices than were used to create the backup. Online restore requires all the backup devices of the backup. An attempt to restore with fewer devices fails.  
  
 For more information, see [Backup Devices &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-devices-sql-server.md).  
  
> [!NOTE]  
>  When restoring a backup from a mirrored media set, you can specify only a single mirror for each media family. In the presence of errors, however, having the other mirror(s) enables some restore problems to be resolved quickly. You can substitute a damaged media volume with the corresponding volume from another mirror. Note that for offline restores you can restore from fewer devices than media families, but each family is processed only once.  
  
 **WITH Options**  
  
 UNLOAD  
 Specifies that the tape is automatically rewound and unloaded when the RESTORE is finished. UNLOAD is set by default when a new user session is started. It remains set until NOUNLOAD is specified. This option is used only for tape devices. If a non-tape device is being used for RESTORE, this option is ignored.  
  
 NOUNLOAD  
 Specifies that the tape is not unloaded automatically from the tape drive after a RESTORE. NOUNLOAD remains set until UNLOAD is specified.  
  
## General Remarks  
 RESTORE REWINDONLY is an alternative to RESTORE LABELONLY FROM TAPE = \<name> WITH REWIND. You can get a list of opened tape drives from the [sys.dm_io_backup_tapes](../../relational-databases/system-dynamic-management-views/sys-dm-io-backup-tapes-transact-sql.md) dynamic management view.  
  
## Security  
  
### Permissions  
 Any user may use RESTORE REWINDONLY.  
  
## See Also  
 [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md)   
 [Media Sets, Media Families, and Backup Sets &#40;SQL Server&#41;](../../relational-databases/backup-restore/media-sets-media-families-and-backup-sets-sql-server.md)   
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)   
 [Backup History and Header Information &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-history-and-header-information-sql-server.md)  
  
  

