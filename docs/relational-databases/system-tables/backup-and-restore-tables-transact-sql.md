---
title: "Backup and Restore Tables (Transact-SQL)"
description: Backup and Restore Tables (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
helpviewer_keywords:
  - "system tables [SQL Server], backup tables"
  - "backup system tables [SQL Server]"
  - "system tables [SQL Server], restore tables"
  - "restore system tables [SQL Server]"
dev_langs:
  - "TSQL"
ms.assetid: aa615add-54e6-40f5-8b55-3728b26884ee
---
# Backup and Restore Tables (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The topics in this section describe the system tables that store information used by database backup and restore operations.  
  
## In This Section  
 [backupfile](../../relational-databases/system-tables/backupfile-transact-sql.md)  
 Contains one row for each data or log file of a database.  
  
 [backupfilegroup](../../relational-databases/system-tables/backupfilegroup-transact-sql.md)  
 Contains one row for each filegroup in a database at the time of backup.  
  
 [backupmediafamily](../../relational-databases/system-tables/backupmediafamily-transact-sql.md)  
 Contains a row for each media family.  
  
 [backupmediaset](../../relational-databases/system-tables/backupmediaset-transact-sql.md)  
 Contains one row for each backup media set.  
  
 [backupset](../../relational-databases/system-tables/backupset-transact-sql.md)  
 Contains a row for each backup set.  
  
 [logmarkhistory](../../relational-databases/system-tables/logmarkhistory-transact-sql.md)  
 Contains one row for each marked transaction that has been committed.  
  
 [restorefile](../../relational-databases/system-tables/restorefile-transact-sql.md)  
 Contains one row for each restored file. These include files restored indirectly by filegroup name.  
  
 [restorefilegroup](../../relational-databases/system-tables/restorefilegroup-transact-sql.md)  
 Contains one row for each restored filegroup.  
  
 [restorehistory](../../relational-databases/system-tables/restorehistory-transact-sql.md)  
 Contains one row for each restore operation.  
  
 [suspect_pages](../../relational-databases/system-tables/suspect-pages-transact-sql.md)  
 Contains one row per page that failed with an 824 error (with a limit of 1,000 rows).  
  
 [sysopentapes](../../relational-databases/system-tables/sysopentapes-transact-sql.md)  
 Contains one row for each currently open tape device.  
  
  
