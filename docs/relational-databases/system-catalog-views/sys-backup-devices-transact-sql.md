---
title: "sys.backup_devices (Transact-SQL)"
description: sys.backup_devices contains a row for each backup-device registered by using sp_addumpdevice or created in SQL Server Management Studio.
author: rwestMSFT
ms.author: randolphwest
ms.date: 02/05/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "backup_devices_TSQL"
  - "backup_devices"
  - "sys.backup_devices"
  - "sys.backup_devices_TSQL"
helpviewer_keywords:
  - "backup devices [SQL Server], viewing information"
  - "sys.backup_devices catalog view"
dev_langs:
  - "TSQL"
---
# sys.backup_devices (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns a row for each backup-device registered by using `sp_addumpdevice` or created in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].

| Column name | Data type | Description |  
| --- | --- | --- |
| `name` | **sysname** | Name of the backup device. Unique in the set. |
| `type` | **tinyint** | Type of backup device:<br /><br />2 = Disk<br /><br />3 = Diskette (obsolete)<br /><br />5 = Tape<br /><br />6 = Pipe (obsolete)<br /><br />7 = Virtual device (for optional use by third-party backup vendors)<br /><br />9 = URL<br /><br />Typically, only disk (2) and URL (9) are used. |
| `type_desc` | **nvarchar(60)** | Description of backup device type:<br /><br />DISK<br /><br />DISKETTE (obsolete)<br /><br />TAPE<br /><br />PIPE (obsolete)<br /><br />VIRTUAL_DEVICE (for optional use by third party backup vendors)<br /><br />URL<br /><br />Typically, only DISK and URL are used. |
| `physical_name` | **nvarchar(260)** | Physical file name or path of the backup device. |  
  
## Permissions

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).

### Permissions for SQL Server 2022 and later

Requires VIEW SERVER SECURITY STATE permission on the server.

## Related content

- [Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)
- [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)
- [Backup Devices (SQL Server)](../../relational-databases/backup-restore/backup-devices-sql-server.md)
- [sp_addumpdevice (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-addumpdevice-transact-sql.md)
- [Databases and Files Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/databases-and-files-catalog-views-transact-sql.md)
- [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.yml)  
  
  
