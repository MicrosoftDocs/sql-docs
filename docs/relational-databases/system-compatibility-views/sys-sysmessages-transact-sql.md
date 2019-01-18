---
title: "sys.sysmessages (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.sysmessages"
  - "sysmessages"
  - "sysmessages_TSQL"
  - "sys.sysmessages_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysmessages system table"
  - "sys.sysmessages compatibility view"
ms.assetid: 44bee7d9-7517-4071-99be-8b36f979c7cc
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# sys.sysmessages (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

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
  
  
