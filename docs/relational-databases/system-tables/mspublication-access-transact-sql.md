---
title: "MSpublication_access (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "MSpublication_access_TSQL"
  - "MSpublication_access"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MSpublication_access system table"
ms.assetid: 7bebe47e-3153-4579-8092-5723667a24c6
author: stevestein
ms.author: sstein
manager: craigg
---
# MSpublication_access (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **MSpublication_access** table contains a row for each [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login that has access to the specific publication or Publisher. This table is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**publication_id**|**int**|The ID of the publication.|  
|**login**|**sysname**|[!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows accounts that exist at both Publisher and Distributor side.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
