---
title: "MSreplication_subscriptions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "MSreplication_subscriptions"
  - "MSreplication_subscriptions_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MSreplication_subscriptions system table"
ms.assetid: fd0c5843-4e9b-4448-8bfb-0a4067d1d8d1
author: stevestein
ms.author: sstein
manager: craigg
---
# MSreplication_subscriptions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **MSreplication_subscriptions** table contains one row of replication information for each Distribution Agent servicing the local Subscriber database. This table is stored in the subscription database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**publisher**|**sysname**|The name of the Publisher.|  
|**publisher_db**|**sysname**|The name of the Publisher database.|  
|**publication**|**sysname**|The name of the publication.|  
|**independent_agent**|**bit**|Indicates whether there is a stand-alone Distribution Agent for this publication.|  
|**subscription_type**|**int**|The type of subscription:<br /><br /> 0 = Push.<br /><br /> 1 = Pull.<br /><br /> 2 = Anonymous.|  
|**distribution_agent**|**sysname**|The name of the Distribution Agent.|  
|**Time**|**smalldatetime**|The time of the last update by Distribution Agent.|  
|**description**|**nvarchar(255)**|The description of the subscription.|  
|**transaction_timestamp**|**varbinary(16)**|Internal-use only.|  
|**update_mode**|**tinyint**|The type of update.|  
|**agent_id**|**binary(16)**|The ID of the agent.|  
|**subscription_guid**|**binary(16)**|The global identifier for the version of the subscription on the publication.|  
|**subid**|**binary(16)**|The global identifier for an anonymous subscription.|  
|**immediate_sync**|**bit**|Indicates whether synchronization files are created or re-created each time the Snapshot Agent runs.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)   
 [sp_helpsubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpsubscription-transact-sql.md)  
  
  
