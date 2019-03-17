---
title: "MSdistributiondbs (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "MSdistributiondbs_TSQL"
  - "MSdistributiondbs"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MSdistributiondbs system table"
ms.assetid: d7ffa9df-bf1d-41b8-837e-b762c17c2764
author: stevestein
ms.author: sstein
manager: craigg
---
# MSdistributiondbs (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **MSdistributiondbs** table contains one row for each distribution database defined on the local Distributor. This table is stored in the **msdb** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|The name of the distribution database.|  
|**min_distretention**|**int**|The minimum retention period, in hours, before transactions are deleted.|  
|**max_distretention**|**int**|The maximum retention period, in hours, before transactions are deleted.|  
|**history_retention**|**int**|The number of hours to retain history.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
