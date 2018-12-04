---
title: "sysmergesubscriptions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sysmergesubscriptions_TSQL"
  - "sysmergesubscriptions"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysmergesubscriptions system table"
ms.assetid: 6adc78da-991d-4c08-98c3-ecb4762e0e99
author: stevestein
ms.author: sstein
manager: craigg
---
# sysmergesubscriptions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Contains one row for each known Subscriber and is a local table at the Publisher. This table is stored in the publication and subscription databases.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|subscriber_server|**sysname**|The ID of the server. Used to map the srvid field to the server-specific value when migrating a copy of the subscription database to a different server.|  
|db_name|**sysname**|The name of the subscribing database.|  
|pubid|**uniqueidentifier**|The ID of the publication from which the current subscription was created.|  
|datasource_type|**int**|The type of data source:<br /><br /> **0** = [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> **2** = Jet OLE DB.|  
|subid|**uniqueidentifier**|The unique identification number for Subscription.|  
|replnickname|**binary**|The compressed nickname for the replica.|  
|replicastate|**uniqueidentifier**|A unique identifier that is used to determine if the previous synchronization was successful by comparing the value at the Publisher with the value at the Subscriber.|  
|status|**tinyint**|The status of the subscription:<br /><br /> **0** = Inactive.<br /><br /> **1** = Active.<br /><br /> **2** = Deleted.|  
|subscriber_type|**int**|The type of Subscriber:<br /><br /> **1** = Global.<br /><br /> **2** = Local.<br /><br /> **3** = Anonymous.|  
|subscription_type|**int**|The type of subscription:<br /><br /> **0** = Push.<br /><br /> **1** = Pull.<br /><br /> **2** = Anonymous.|  
|sync_type|**tinyint**|The type of synchronization:<br /><br /> **1** = Automatic.<br /><br /> **2** = No synchronization.|  
|description|**nvarchar(255)**|A brief description of the subscription.|  
|priority|**real**|Specifies the subscription priority and allows the implementation of priority-based conflict resolution. Equals **0.00** for all local or anonymous subscriptions.|  
|recgen|**bigint**|The number of the last generation received.|  
|recguid|**uniqueidentifier**|The unique ID of the last generation received.|  
|sentgen|**bigint**|Number of the last generation sent.|  
|sentguid|**uniqueidentifier**|The unique ID of the last generation sent.|  
|schemaversion|**int**|The number of the last schema received.|  
|schemaguid|**uniqueidentifier**|The unique ID of the last schema received.|  
|last_validated|**datetime**|The **datetime** of the last successful validation of Subscriber data.|  
|attempted_validate|**datetime**|The last **datetime** that validation was attempted on the subscription.|  
|last_sync_date|**datetime**|The **datetime** of the synchronization.|  
|last_sync_status|**int**|The subscription status:<br /><br /> **0** = All jobs are waiting to start.<br /><br /> **1** = One or more jobs are starting.<br /><br /> **2** = All jobs have executed successfully.<br /><br /> **3** = At least one job is executing.<br /><br /> **4** = All jobs are scheduled and idle.<br /><br /> **5** = At least one job is attempting to execute after a previous failure.<br /><br /> **6** = At least one job has failed to execute successfully.|  
|last_sync_summary|**sysname**|The description of last synchronization results.|  
|metadatacleanuptime|**datetime**|The last **datetime** that expired metadata was removed from merge replication system tables.|  
|partition_id|**int**|Identifies the pre-computed partition to which the subscription belongs.|  
|cleanedup_unsent_changes|**bit**|Identifies that metadata for unsent changes have been cleaned up at the Subscriber.|  
|replica_version|**int**|Identifies the version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for the Subscriber to which the subscription belongs, which can be one of the following values:<br /><br /> **90** = [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]<br /><br /> **100** = [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]|  
|supportability_mode|**int**|Internal use only.|  
|application_name|**nvarchar(128)**|Internal use only.|  
|subscriber_number|**int**|Internal use only.|  
|last_makegeneration_datetime|**datetime**|The last **datetime** that the makegeneration process ran for the Publisher. For more information, see the -MakeGenerationInterval parameter in [Replication Merge Agent](../../relational-databases/replication/agents/replication-merge-agent.md).|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)  
  
  
