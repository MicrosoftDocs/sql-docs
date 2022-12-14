---
description: "sp_helpmergesubscription (Transact-SQL)"
title: "sp_helpmergesubscription (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_helpmergesubscription"
  - "sp_helpmergesubscription_TSQL"
helpviewer_keywords: 
  - "sp_helpmergesubscription"
ms.assetid: da564112-f769-4e67-9251-5699823e8c86
author: markingmyname
ms.author: maghan
---
# sp_helpmergesubscription (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns information about a subscription to a merge publication, both push and pull. This stored procedure is executed at the Publisher on the publication database or at a republishing Subscriber on the subscription database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helpmergesubscription [ [ @publication=] 'publication']  
    [ , [ @subscriber=] 'subscriber']  
    [ , [ @subscriber_db=] 'subscriber_db']  
    [ , [ @publisher=] 'publisher']  
    [ , [ @publisher_db=] 'publisher_db']  
    [ , [ @subscription_type=] 'subscription_type']  
    [ , [ @found=] 'found' OUTPUT]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with a default of **%**. The publication must already exist and conform to the rules for identifiers. If NULL or **%**, information about all merge publications and subscriptions in the current database is returned.  
  
`[ @subscriber = ] 'subscriber'`
 Is the name of the Subscriber. *subscriber* is **sysname**, with a default of **%**. If NULL or %, information about all subscriptions to the given publication is returned.  
  
`[ @subscriber_db = ] 'subscriber_db'`
 Is the name of the subscription database. *subscriber_db*is **sysname**, with a default of **%**, which returns information about all subscription databases.  
  
`[ @publisher = ] 'publisher'`
 Is the name of the Publisher. The Publisher must be a valid server. *publisher*is **sysname**, with a default of **%**, which returns information about all Publishers.  
  
`[ @publisher_db = ] 'publisher_db'`
 Is the name of the Publisher database. *publisher_db*is **sysname**, with a default of **%**, which returns information about all Publisher databases.  
  
`[ @subscription_type = ] 'subscription_type'`
 Is the type of subscription. *subscription_type*is **nvarchar(15)**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**push** (default)|Push subscription|  
|**pull**|Pull subscription|  
|**both**|Both a push and pull subscription|  
  
`[ @found = ] 'found'OUTPUT`
 Is a flag to indicate returning rows. *found*is **int** and an OUTPUT parameter, with a default of NULL. **1** indicates the publication is found. **0** indicates the publication is not found.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**subscription_name**|**sysname**|Name of the subscription.|  
|**publication**|**sysname**|Name of the publication.|  
|**publisher**|**sysname**|Name of the Publisher.|  
|**publisher_db**|**sysname**|Name of the Publisher database.|  
|**subscriber**|**sysname**|Name of the Subscriber.|  
|**subscriber_db**|**sysname**|Name of the subscription database.|  
|**status**|**int**|Status of the subscription:<br /><br /> **0** = All jobs are waiting to start<br /><br /> **1** = One or more jobs are starting<br /><br /> **2** = All jobs have executed successfully<br /><br /> **3** = At least one job is executing<br /><br /> **4** = All jobs are scheduled and idle<br /><br /> **5** = At least one job is attempting to execute after a previous failure<br /><br /> **6** = At least one job has failed to execute successfully|  
|**subscriber_type**|**int**|Type of Subscriber.|  
|**subscription_type**|**int**|Type of subscription:<br /><br /> **0** = Push<br /><br /> **1** = Pull<br /><br /> **2** = Both|  
|**priority**|**float(8)**|Number indicating the priority for the subscription.|  
|**sync_type**|**tinyint**|Subscription sync type.|  
|**description**|**nvarchar(255)**|Brief description of this merge subscription.|  
|**merge_jobid**|**binary(16)**|Job ID of the Merge Agent.|  
|**full_publication**|**tinyint**|Whether the subscription is to a full or filtered publication.|  
|**offload_enabled**|**bit**|Specifies if offload execution of a replication agent has been set to run at the Subscriber. If NULL, execution is run at the Publisher.|  
|**offload_server**|**sysname**|Name of the server to where the agent is running.|  
|**use_interactive_resolver**|**int**|Returns whether or not the interactive resolver is used during reconciliation. If **0**, the interactive resolver not is used.|  
|**hostname**|**sysname**|Value supplied when a subscription is filtered by the value of the [HOST_NAME](../../t-sql/functions/host-name-transact-sql.md) function.|  
|**subscriber_security_mode**|**smallint**|Is the security mode at the Subscriber, where **1** means Windows Authentication, and **0** means [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.|  
|**subscriber_login**|**sysname**|Is the login name at the Subscriber.|  
|**subscriber_password**|**sysname**|Actual Subscriber password is never returned. The result is masked by a "**\*\*\*\*\*\***" string.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_helpmergesubscription** is used in merge replication to return subscription information stored at the Publisher or republishing Subscriber.  
  
 For anonymous subscriptions, the *subscription_type*value is always **1** (pull). However, you must execute [sp_helpmergepullsubscription](../../relational-databases/system-stored-procedures/sp-helpmergepullsubscription-transact-sql.md) at the Subscriber for information on anonymous subscriptions.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role, the **db_owner** fixed database role or the publication access list for the publication to which the subscription belongs can execute **sp_helpmergesubscription**.  
  
## See Also  
 [sp_addmergesubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergesubscription-transact-sql.md)   
 [sp_changemergesubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changemergesubscription-transact-sql.md)   
 [sp_dropmergesubscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropmergesubscription-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
