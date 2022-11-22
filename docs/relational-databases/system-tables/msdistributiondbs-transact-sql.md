---
title: "MSdistributiondbs (Transact-SQL)"
description: MSdistributiondbs (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSdistributiondbs_TSQL"
  - "MSdistributiondbs"
helpviewer_keywords:
  - "MSdistributiondbs system table"
dev_langs:
  - "TSQL"
ms.assetid: d7ffa9df-bf1d-41b8-837e-b762c17c2764
---
# MSdistributiondbs (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

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
  
  
