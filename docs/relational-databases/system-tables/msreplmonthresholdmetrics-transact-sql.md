---
title: "MSreplmonthresholdmetrics (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "msreplmonthresholdmetrics_TSQL"
  - "msreplmonthresholdmetrics"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MSreplmonthresholdmetrics system table"
ms.assetid: 0cc9b40a-36ce-485b-9bc2-d4abd5aa6727
author: stevestein
ms.author: sstein
manager: craigg
---
# MSreplmonthresholdmetrics (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **MSreplmonthresholdmetrics** table defines the metrics provided for monitoring replication. This table is stored in the **msdb** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**metric_id**|**int**|Identifies a replication performance metric, and can be one of the following values:<br /><br /> **1** = expiration<br /><br /> **2** = latency<br /><br /> **4** = mergeexpiration<br /><br /> **5** = mergeslowrunduration<br /><br /> **6** = mergefastrunduration<br /><br /> **7** = mergefastrunspeed<br /><br /> **8** = mergeslowrunspeed|  
|**title**|**sysname**|The name of the replication performance metric.|  
|**warningbitstatus**|**int**|The bitwise identifier used to provide a warning of a threshold violation for one of the following metrics:<br /><br /> **1** = expiration - A subscription to a transactional publication has exceeded the retention period by more than the allowable threshold, as a percentage of the retention period.<br /><br /> **2** = latency - The time taken to replicate data from a transactional Publisher to the Subscriber exceeds the threshold, in seconds.<br /><br /> **4** = mergeexpiration - A subscription to a merge publication has exceeded the retention period by more than the allowable threshold, as a percentage of the retention period.<br /><br /> **8** = mergefastrunduration - The time taken to complete synchronization of a merge subscription exceeds the threshold, in seconds, over a fast network connection.<br /><br /> **16** = mergeslowrunduration - The time taken to complete synchronization of a merge subscription exceeds the threshold, in seconds, over a slow or dial-up network connection.<br /><br /> **32** = mergefastrunspeed - The delivery rate for rows during synchronization of a merge subscription has failed to maintain the threshold rate, in rows per second, over a fast network connection.<br /><br /> **64** = mergeslowrunspeed - The delivery rate for rows during synchronization of a merge subscription has failed to maintain the threshold rate, in rows per second, over a slow or dial-up network connection.|  
|**alertmessageid**|**int**|The ID of the error message that is displayed when the threshold warning condition occurs.|  
|**description**|**nvarchar(3000)**|The description of the replication performance metric|  
|**default_value**|**sql_variant**|A default value for the replication performance metric.|  
|**min_value**|**sql_variant**|The minimum value for a bounded replication performance metric.|  
|**max_value**|**sql_variant**|The maximum value for a bounded replication performance metric.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
