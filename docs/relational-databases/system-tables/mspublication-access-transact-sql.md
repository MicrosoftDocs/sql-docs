---
title: "MSpublication_access (Transact-SQL)"
description: MSpublication_access (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSpublication_access_TSQL"
  - "MSpublication_access"
helpviewer_keywords:
  - "MSpublication_access system table"
dev_langs:
  - "TSQL"
ms.assetid: 7bebe47e-3153-4579-8092-5723667a24c6
---
# MSpublication_access (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSpublication_access** table contains a row for each [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login that has access to the specific publication or Publisher. This table is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**publication_id**|**int**|The ID of the publication.|  
|**login**|**sysname**|[!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows accounts that exist at both Publisher and Distributor side.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
