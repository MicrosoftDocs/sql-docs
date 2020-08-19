---
description: "MStracer_tokens (Transact-SQL)"
title: "MStracer_tokens (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "MStracer_tokens_TSQL"
  - "MStracer_tokens"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MStracer_tokens system table"
ms.assetid: b273aa48-8188-4213-8e2c-311543c3236f
author: CarlRabeler
ms.author: carlrab
---
# MStracer_tokens (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MStracer_tokens** table maintains a record of tracer token records inserted into a publication. This table is stored in the distribution database and is used by replication for performance monitoring.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**tracer_id**|**int**|Identifies a tracer token record.|  
|**publication_id**|**int**|Identifies the publication into which the tracer token record was inserted.|  
|**publisher_commit**|**datetime**|The date and time when the tracer token record was committed at the Publisher.|  
|**distributor_commit**|**datetime**|The date and time when the tracer token record was committed at the Distributor.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
