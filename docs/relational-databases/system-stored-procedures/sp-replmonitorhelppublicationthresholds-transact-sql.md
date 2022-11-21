---
title: "sp_replmonitorhelppublicationthresholds (T-SQL)"
description: Describes the sp_replmonitorhelppublicationthresholds stored procedure which returns the threshold metrics set for a monitored publication.
ms.custom: seo-lt-2019
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_replmonitorhelppublicationthresholds"
  - "sp_replmonitorhelppublicationthresholds_TSQL"
helpviewer_keywords: 
  - "sp_replmonitorhelppublicationthresholds"
ms.assetid: d6b1aa4b-3369-4255-a892-c0e5cc9cb693
author: markingmyname
ms.author: maghan
---
# sp_replmonitorhelppublicationthresholds (Transact-SQL)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  Returns the threshold metrics set for a monitored publication. This stored procedure, which is used to monitor replication, is executed at the Distributor on the distribution database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_replmonitorhelppublicationthresholds [ @publisher = ] 'publisher'  
        , [ @publisher_db = ] 'publisher_db'  
        , [ @publication = ] 'publication'   
    [ , [ @publication_type = ] publication_type ]   
    [ , [ @thresholdmetricname = ] 'thresholdmetricname'  
```  
  
## Arguments  
`[ @publisher = ] 'publisher'`
 Is the name of the Publisher. *publisher* is **sysname**, with no default.  
  
`[ @publisher_db = ] 'publisher_db'`
 Is the name of the published database. *publisher_db* is **sysname**, with no default.  
  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
`[ @publication_type = ] publication_type`
 If the type of publication. *publication_type* is **int**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**0**|Transactional publication.|  
|**1**|Snapshot publication.|  
|**2**|Merge publication.|  
|NULL (default)|Replication tries to determine the publication type.|  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**metric_id**|**int**|ID of the replication performance metric, which can be one of the following.<br /><br /> **1expiration** - monitors for imminent expiration of subscriptions to transactional publications.<br /><br /> **2latency** - monitors for the performance of subscriptions to transactional publications.<br /><br /> **4mergeexpiration** - monitors for imminent expiration of subscriptions to merge publications.<br /><br /> **5mergeslowrunduration** - monitors the duration of merge synchronizations over low-bandwidth (dial-up) connections.<br /><br /> **6mergefastrunduration** - monitors the duration of merge synchronizations over high-bandwidth (LAN) connections.<br /><br /> **7mergefastrunspeed** - monitors the synchronization rate of merge synchronizations over high-bandwidth (LAN) connections.<br /><br /> **8mergeslowrunspeed** - monitors the synchronization rate of merge synchronizations over low-bandwidth (dial-up) connections.|  
|**title**|**sysname**|Name of the replication performance metric.|  
|**value**|**int**|The threshold value of the performance metric.|  
|**shouldalert**|**bit**|Is if an alert should be generated when the metric exceeds the defined threshold for this publication; a value of **1** indicates that an alert should be raised.|  
|**isenabled**|**bit**|Is if monitoring is enabled for this replication performance metric for this publication; a value of **1** indicates that monitoring is enabled.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_replmonitorhelppublicationthresholds** is used with all types of replication.  
  
## Permissions  
 Only members of the **db_owner** or **replmonitor** fixed database role on the distribution database can execute **sp_replmonitorhelppublicationthresholds**.  
  
## See Also  
 [Programmatically Monitor Replication](../../relational-databases/replication/monitor/programmatically-monitor-replication.md)  
  
  
