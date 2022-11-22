---
title: "sysopentapes (Transact-SQL)"
description: sysopentapes (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysopentapes"
  - "sysopentapes_TSQL"
helpviewer_keywords:
  - "backup media [SQL Server], sysopentapes system table"
  - "sysopentapes system table"
dev_langs:
  - "TSQL"
ms.assetid: c066ca9b-9cfd-46b1-90a3-5c8dc9e7b6ae
---
# sysopentapes (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains one row for each currently open tape device. This view is stored in the **master** database.  
  
> [!IMPORTANT]  
>  This system table is included as a view for backward compatibility. Instead, use the [sys.dm_io_backup_tapes &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-io-backup-tapes-transact-sql.md) dynamic management view.  
  
> [!NOTE]  
>  You cannot drop the **sysopentapes** view.  

  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**openTape**|**nvarchar(64)**|Physical file name of open tape device. For more information about opening and releasing tape devices, see [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md) and [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md).|  
  
## Permissions  
 The user needs VIEW SERVER STATE permission on the server.  
  
  
