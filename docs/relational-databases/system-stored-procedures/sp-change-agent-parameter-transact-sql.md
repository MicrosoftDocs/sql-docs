---
title: "sp_change_agent_parameter (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_change_agent_parameter_TSQL"
  - "sp_change_agent_parameter"
helpviewer_keywords: 
  - "sp_change_agent_parameter"
ms.assetid: f1fbecc7-e64f-405c-8067-6b38c1f3c0a0
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_change_agent_parameter (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Changes a parameter of a replication agent profile stored in the [MSagent_parameters](../../relational-databases/system-tables/msagent-parameters-transact-sql.md) system table. This stored procedure is executed at the Distributor where the agent is running, on any database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_change_agent_parameter [ @profile_id= ] profile_id, [ @parameter_name= ] 'parameter_name', [ @parameter_value= ] 'parameter_value'  
```  
  
## Arguments  
`[ @profile_id = ] profile_id,`
 Is the ID of the profile. *profile_id* is **int**, with no default.  
  
`[ @parameter_name = ] 'parameter_name'`
 Is the name of the parameter. *parameter_name* is **sysname**, with no default. For system profiles, the parameters that can be changed depend on the type of agent. To find out what type of agent this *profile_id* represents, locate the *profile_id* column in the **Msagent_profiles** table, and note the *agent_type* value.  
  
> [!NOTE]  
>  If a parameter is supported for a given *agent_type*, but has not been defined in the agent profile, an error is returned. To add a parameter to an agent profile you must execute [sp_add_agent_parameter](../../relational-databases/system-stored-procedures/sp-add-agent-parameter-transact-sql.md).  
  
 For a Snapshot Agent (*agent_type*=**1**), if defined in the profile, the following properties can be changed:  
  
-   **70Subscribers**  
  
-   **BcpBatchSize**  
  
-   **HistoryVerboseLevel**  
  
-   **LoginTimeout**  
  
-   **MaxBcpThreads**  
  
-   **MaxNetworkOptimization**  
  
-   **Output**  
  
-   **OutputVerboseLevel**  
  
-   **PacketSize**  
  
-   **QueryTimeout**  
  
-   **StartQueueTimeout**  
  
-   **UsePerArticleContentsView**  
  
 For a Log Reader Agent (*agent_type*=**2**), if defined in the profile, the following properties can be changed:  
  
-   **HistoryVerboseLevel**  
  
-   **LoginTimeout**  
  
-   **MessageInterval**  
  
-   **Output**  
  
-   **OutputVerboseLevel**  
  
-   **PacketSize**  
  
-   **PollingInterval**  
  
-   **QueryTimeout**  
  
-   **ReadBatchSize**  
  
-   **ReadBatchThreshold**  
  
 For a Distribution Agent (*agent_type*=**3**), if defined in the profile, the following properties can be changed:  
  
-   **BcpBatchSize**  
  
-   **CommitBatchSize**  
  
-   **CommitBatchThreshold**  
  
-   **FileTransferType**  
  
-   **HistoryVerboseLevel**  
  
-   **KeepAliveMessageInterval**  
  
-   **LoginTimeout**  
  
-   **MaxBcpThreads**  
  
-   **MaxDeliveredTransactions**  
  
-   **MessageInterval**  
  
-   **Output**  
  
-   **OutputVerboseLevel**  
  
-   **PacketSize**  
  
-   **PollingInterval**  
  
-   **QueryTimeout**  
  
-   **QuotedIdentifier**  
  
-   **SkipErrors**  
  
-   **TransactionsPerHistory**  
  
 For a Merge Agent (*agent_type*=**4**), if defined in the profile, the following properties can be changed:  
  
-   **AltSnapshotFolder**  
  
-   **BcpBatchSize**  
  
-   **ChangesPerHistory**  
  
-   **DestThreads**  
  
-   **DownloadGenerationsPerBatch**  
  
-   **DownloadReadChangesPerBatch**  
  
-   **DownloadWriteChangesPerBatch**  
  
-   **DynamicSnapshotLocation**  
  
-   **ExchangeType**  
  
-   **FastRowCount**  
  
-   **FileTransferType**  
  
-   **GenerationChangeThreshold**  
  
-   **HistoryVerboseLevel**  
  
-   **InputMessageFile**  
  
-   **InteractiveResolution**  
  
-   **InterruptOnMessagePattern**  
  
-   **KeepAliveMessageInterval**  
  
-   **LoginTimeout**  
  
-   **MaxBcpThreads**  
  
-   **MaxDownloadChanges**  
  
-   **MaxUploadChanges**  
  
-   **MetadataRetentionCleanup**  
  
-   **NumDeadlockRetries**  
  
-   **Output**  
  
-   **OutputMessageFile**  
  
-   **OutputVerboseLevel**  
  
-   **PacketSize**  
  
-   **ParallelUploadDownload**  
  
-   **PauseOnMessagePattern**  
  
-   **PauseTime**  
  
-   **PollingInterval**  
  
-   **ProcessMessagesAtPublisher**  
  
-   **ProcessMessagesAtSubscriber**  
  
-   **QueryTimeout**  
  
-   **QueueSizeMultiplier**  
  
-   **SrcThreads**  
  
-   **StartQueueTimeout**  
  
-   **SyncToAlternate**  
  
-   **UploadGenerationsPerBatch**  
  
-   **UploadReadChangesPerBatch**  
  
-   **UploadWriteChangesPerBatch**  
  
-   **UseInprocLoader**  
  
-   **Validate**  
  
-   **ValidateInterval**  
  
 For a Queue Reader Agent (*agent_type*=**9**), if defined in the profile, the following properties can be changed:  
  
-   **HistoryVerboseLevel**  
  
-   **LoginTimeout**  
  
-   **Output**  
  
-   **OutputVerboseLevel**  
  
-   **PollingInterval**  
  
-   **QueryTimeout**  
  
-   **ResolverState**  
  
-   **SQLQueueMode**  
  
 To see what parameters have been defined for a given profile, run **sp_help_agent_profile** and note the *profile_name* associated with the *profile_id*. With the appropriate *profile_id*, next run **sp_help_agent_parameters** using that *profile_id* to see the parameters associated with the profile. Parameters can be added to a profile by executing [sp_add_agent_parameter](../../relational-databases/system-stored-procedures/sp-add-agent-parameter-transact-sql.md).  
  
`[ @parameter_value = ] 'parameter_value'`
 Is the new value of the parameter. *parameter_value* is **nvarchar(255)**, with no default.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_change_agent_parameter** is used in all types of replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_change_agent_parameter**.  
  
## See Also  
 [Replication Agent Profiles](../../relational-databases/replication/agents/replication-agent-profiles.md)   
 [Replication Distribution Agent](../../relational-databases/replication/agents/replication-distribution-agent.md)   
 [Replication Log Reader Agent](../../relational-databases/replication/agents/replication-log-reader-agent.md)   
 [Replication Merge Agent](../../relational-databases/replication/agents/replication-merge-agent.md)   
 [Replication Queue Reader Agent](../../relational-databases/replication/agents/replication-queue-reader-agent.md)   
 [Replication Snapshot Agent](../../relational-databases/replication/agents/replication-snapshot-agent.md)   
 [sp_add_agent_parameter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-agent-parameter-transact-sql.md)   
 [sp_drop_agent_parameter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-drop-agent-parameter-transact-sql.md)   
 [sp_help_agent_parameter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-agent-parameter-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
