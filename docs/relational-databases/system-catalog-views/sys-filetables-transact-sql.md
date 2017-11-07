---
title: "sys.filetables (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "filetables"
  - "filetables_TSQL"
  - "sys.filetables"
  - "sys.filetables_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.filetables catalog view"
ms.assetid: a740be59-cd52-4707-9ad2-5203669a63ac
caps.latest.revision: 15
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# sys.filetables (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Returns a row for each FileTable in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. For more information about FileTables, see [FileTables &#40;SQL Server&#41;](../../relational-databases/blob/filetables-sql-server.md).    
  
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
  
  
