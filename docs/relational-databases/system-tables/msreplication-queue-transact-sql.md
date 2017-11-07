---
title: "MSreplication_queue (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
applies_to: 
  - "SQL Server"
f1_keywords: 
  - "MSreplication_queue"
  - "MSreplication_queue_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MSreplication_queue system table"
ms.assetid: 664bf817-8021-4417-96d6-2bb1e4baabff
caps.latest.revision: 27
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSreplication_queue (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **MSreplication_queue** table is used by the replication process to store the queued commands issued by all the queued updating subscriptions that are using SQL-based queued. This table is stored in the subscription database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**publisher**|**sysname**|The name of the Publisher.|  
|**publisher_db**|**sysname**|The name of the publication database.|  
|**publication**|**sysname**|The name of the publication.|  
|**tranid**|**sysname**|The transaction ID under which the queued command was executed.|  
|**data**|**varbinary(8000)**|The packed bytestream that stored information about the queued command.|  
|**datalen**|**int**|The length of data, in bytes.|  
|**commandtype**|**int**|The type of command being queued:<br /><br /> 1 = User command in transaction.<br /><br /> 2 = Subscription synchronization command.|  
|**insertdate**|**datetime**|The date of insertion.|  
|**orderkey**|**bigint**|The identity column that increases monotonically.|  
|**cmdstate**|**bit**|The command state:<br /><br /> 0 = Complete.<br /><br /> 1 = Partial.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  