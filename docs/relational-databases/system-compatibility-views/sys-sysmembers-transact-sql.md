---
title: "sys.sysmembers (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.sysmembers_TSQL"
  - "sysmembers"
  - "sys.sysmembers"
  - "sysmembers_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysmembers system table"
  - "sys.sysmembers compatibility view"
ms.assetid: ceb18341-f985-4849-ac83-21d67ab7b980
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# sys.sysmembers (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Contains a row for each member of a database role.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssnoteCompView](../../includes/ssnotecompview-md.md)]  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**memberuid**|**smallint**|User ID for the role member. Overflows or returns NULL if the number of users and roles exceeds 32,767.|  
|**groupuid**|**smallint**|User ID for the role. Overflows or returns NULL if the number of users and roles exceeds 32,767.|  
  
## See Also  
 [Mapping System Tables to System Views &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mapping-system-tables-to-system-views-transact-sql.md)   
 [Compatibility Views &#40;Transact-SQL&#41;](~/relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md)  
  
  
