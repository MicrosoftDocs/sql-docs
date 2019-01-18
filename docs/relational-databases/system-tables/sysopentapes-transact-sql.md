---
title: "sysopentapes (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sysopentapes"
  - "sysopentapes_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "backup media [SQL Server], sysopentapes system table"
  - "sysopentapes system table"
ms.assetid: c066ca9b-9cfd-46b1-90a3-5c8dc9e7b6ae
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sysopentapes (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

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
  
  
