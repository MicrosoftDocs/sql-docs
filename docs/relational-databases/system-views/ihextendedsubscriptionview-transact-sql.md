---
title: "IHextendedSubscriptionView (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "IHextendedSubscriptionView_TSQL"
  - "IHextendedSubscriptionView"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "IHextendedSubscriptionView view"
ms.assetid: 124756a4-463a-4a81-bf5b-de7e8ffc7a62
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# IHextendedSubscriptionView (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **IHextendedSubscriptionView** view exposes information on subscription to a non-SQL Server publication. This view is stored in the **distribution** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**article_id**|**int**|The unique identifier for an article.|  
|**dest_db**|**sysname**|The name of the destination database.|  
|**srvid**|**smallint**|The unique identifier for a Subscriber.|  
|**login_name**|**sysname**|The login used for connecting to a Subscriber.|  
|**distribution_jobid**|**binary**|Identifies the Distribution Agent job.|  
|**publisher_database_id**|**int**|Identifies the publication database.|  
|**subscription_type**|**int**|The type of subscription:<br /><br /> **0** = Push - the distribution agent runs at the Subscriber.<br /><br /> **1** = Pull - the distribution agent runs at the Distributor.|  
|**sync_type**|**tinyint**|The type of initial synchronization:<br /><br /> **1** = Automatic<br /><br /> **2** = None|  
|**status**|**tinyint**|The status of the subscription:<br /><br /> **0** = Inactive<br /><br /> **1** = Subscribed<br /><br /> **2** = Active|  
|**snapshot_seqno_flag**|**bit**|Indicates if a snapshot sequence number is being used.|  
|**independent_agent**|**bit**|Specifies whether there is a stand-alone Distribution Agent for this publication.<br /><br /> **0** = The publication uses a shared Distribution Agent, and each Publisher database/Subscriber database pair has a single, shared Agent.<br /><br /> **1** = There is a stand-alone Distribution Agent for this publication.|  
|**subscription_time**|**datetime**|Internal use only.|  
|**loopback_detection**|**bit**|Applies to subscriptions that are part of a bidirectional transactional replication topology. Loopback detection determines whether the Distribution Agent sends transactions originated at the Subscriber back to the Subscriber:<br /><br /> **1** = Does not send back.<br /><br /> **0** = Sends back.|  
|**agent_id**|**int**|The unique identifier of the Distribution Agent.|  
|**update_mode**|**tinyint**|Indicates the type of updating mode, which can be one of the following:<br /><br /> **0** = Read-only.<br /><br /> **1** = Immediate update.<br /><br /> **2** = Queued update using Message Queuing.<br /><br /> **3** = Immediate update with queued update as failover using Message Queuing.<br /><br /> **4** = Queued update using SQL Server queue.<br /><br /> **5** = immediate update with queued update failover, using SQL Server queue.|  
|**publisher_seqno**|**varbinary(16)**|The sequence number of the transaction at the Publisher for this subscription.|  
|**ss_cplt_seqno**|**varbinary(16)**|The sequence number used to signify the completion of the concurrent snapshot processing.|  
  
## See Also  
 [Heterogeneous Database Replication](../../relational-databases/replication/non-sql/heterogeneous-database-replication.md)   
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
