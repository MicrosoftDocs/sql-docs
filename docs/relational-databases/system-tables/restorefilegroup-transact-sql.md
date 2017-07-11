---
title: "restorefilegroup (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "restorefilegroup_TSQL"
  - "restorefilegroup"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "filegroups [SQL Server], restorefilegroup system table"
  - "restorefilegroup system table"
ms.assetid: 3aa15c55-6b72-4f76-97d7-bd88391d105c
caps.latest.revision: 25
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# restorefilegroup (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Contains one row for each restored filegroup. This table is stored in the **msdb** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**restore_history_id**|**int**|Unique identification number that identifies the corresponding restore operation. References **restorehistory(restore_history_id)**.|  
|**filegroup_name**|**nvarchar(128)**|Name of the filegroup being restored. Can be NULL.<br /><br /> When a database is reverted to a database snapshot, this value is populated in the same way as for a full restore.|  
  
## Remarks  
 To reduce the number of rows in this table and in other backup and history tables, execute the [sp_delete_backuphistory](../../relational-databases/system-stored-procedures/sp-delete-backuphistory-transact-sql.md) stored procedure.  
  
## See Also  
 [Backup and Restore Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backup-and-restore-tables-transact-sql.md)   
 [restorefile &#40;Transact-SQL&#41;](../../relational-databases/system-tables/restorefile-transact-sql.md)   
 [restorehistory &#40;Transact-SQL&#41;](../../relational-databases/system-tables/restorehistory-transact-sql.md)   
 [System Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/system-tables-transact-sql.md)  
  
  