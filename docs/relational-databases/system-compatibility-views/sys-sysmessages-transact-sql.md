---
title: "sys.sysmessages (Transact-SQL)"
description: "sys.sysmessages (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.sysmessages"
  - "sysmessages"
  - "sysmessages_TSQL"
  - "sys.sysmessages_TSQL"
helpviewer_keywords:
  - "sysmessages system table"
  - "sys.sysmessages compatibility view"
dev_langs:
  - "TSQL"
---
# sys.sysmessages (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains one row for each system error or warning that can be returned by the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] displays the error description on the user's screen.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssnoteCompView](../../includes/ssnotecompview-md.md)]  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**error**|**int**|Unique error number.|  
|**severity**|**tinyint**|Severity level of the error.|  
|**dlevel**|**smallint**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**description**|**nvarchar(255)**|Explanation of the error with placeholders for parameters.|  
|**msglangid**|**smallint**|System message group ID.|  
  
## See Also  
 [Mapping System Tables to System Views &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mapping-system-tables-to-system-views-transact-sql.md)   
 [Compatibility Views &#40;Transact-SQL&#41;](~/relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md)  
  
  
