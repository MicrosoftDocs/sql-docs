---
title: "sys.filetables (Transact-SQL)"
description: sys.filetables (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "filetables"
  - "filetables_TSQL"
  - "sys.filetables"
  - "sys.filetables_TSQL"
helpviewer_keywords:
  - "sys.filetables catalog view"
dev_langs:
  - "TSQL"
ms.assetid: a740be59-cd52-4707-9ad2-5203669a63ac
---
# sys.filetables (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns a row for each FileTable in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)]. For more information about FileTables, see [FileTables &#40;SQL Server&#41;](../../relational-databases/blob/filetables-sql-server.md).    
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**object_id**||Object identification number. Is unique within a database.<br /><br /> For more information, [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md).|  
|**is_enabled**|**bit**|1 = FileTable is in 'enabled' state.|  
|**directory_name**|**varchar(255)**|Name of the root directory for a FileTable.|  
|**filename_collation_id**||Is the collation identifier defined for the FileTable|  
|**filename_collation_name**||Is the collation name defined for the FileTable.|  
  
## See Also  
 [Manage FileTables](../../relational-databases/blob/manage-filetables.md)   
 [FileTables &#40;SQL Server&#41;](../../relational-databases/blob/filetables-sql-server.md)  
  
  
