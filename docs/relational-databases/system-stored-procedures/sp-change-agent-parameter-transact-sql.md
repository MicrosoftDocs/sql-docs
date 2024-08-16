---
title: "sp_change_agent_parameter (Transact-SQL)"
description: Changes a parameter of a replication agent profile stored in the MSagent_parameters system table.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_change_agent_parameter_TSQL"
  - "sp_change_agent_parameter"
helpviewer_keywords:
  - "sp_change_agent_parameter"
dev_langs:
  - "TSQL"
---
# sp_change_agent_parameter (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Changes a parameter of a replication agent profile stored in the [MSagent_parameters](../system-tables/msagent-parameters-transact-sql.md) system table. This stored procedure is executed at the Distributor where the agent is running, on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_change_agent_parameter
    [ @profile_id = ] profile_id
    , [ @parameter_name = ] N'parameter_name'
    , [ @parameter_value = ] N'parameter_value'
[ ; ]
```

## Arguments

#### [ @profile_id = ] *profile_id*

The ID of the profile. *@profile_id* is **int**, with no default.

#### [ @parameter_name = ] N'*parameter_name*'

The name of the parameter. *@parameter_name* is **sysname**, with no default. For system profiles, the parameters that can be changed depend on the type of agent. To find out what type of agent this *@profile_id* represents, locate the `profile_id` column in the `Msagent_profiles` table, and note the `agent_type` value.

If a parameter is supported for a given `agent_type`, but isn't defined in the agent profile, an error is returned. To add a parameter to an agent profile, you must execute [sp_add_agent_parameter](sp-add-agent-parameter-transact-sql.md).

For a Snapshot Agent (`agent_type = 1`), if defined in the profile, the following properties can be changed:

- `70Subscribers`
- `BcpBatchSize`
- `HistoryVerboseLevel`
- `LoginTimeout`
- `MaxBcpThreads`
- `MaxNetworkOptimization`
- `Output`
- `OutputVerboseLevel`
- `PacketSize`
- `QueryTimeout`
- `StartQueueTimeout`
- `UsePerArticleContentsView`

For a Log Reader Agent (`agent_type = 2`), if defined in the profile, the following properties can be changed:

- `HistoryVerboseLevel`
- `LoginTimeout`
- `MessageInterval`
- `Output`
- `OutputVerboseLevel`
- `PacketSize`
- `PollingInterval`
- `QueryTimeout`
- `ReadBatchSize`
- `ReadBatchThreshold`

For a Distribution Agent (`agent_type = 3`), if defined in the profile, the following properties can be changed:

- `BcpBatchSize`
- `CommitBatchSize`
- `CommitBatchThreshold`
- `FileTransferType`
- `HistoryVerboseLevel`
- `KeepAliveMessageInterval`
- `LoginTimeout`
- `MaxBcpThreads`
- `MaxDeliveredTransactions`
- `MessageInterval`
- `Output`
- `OutputVerboseLevel`
- `PacketSize`
- `PollingInterval`
- `QueryTimeout`
- `QuotedIdentifier`
- `SkipErrors`
- `TransactionsPerHistory`

For a Merge Agent (`agent_type = 4`), if defined in the profile, the following properties can be changed:

- `AltSnapshotFolder`
- `BcpBatchSize`
- `ChangesPerHistory`
- `DestThreads`
- `DownloadGenerationsPerBatch`
- `DownloadReadChangesPerBatch`
- `DownloadWriteChangesPerBatch`
- `DynamicSnapshotLocation`
- `ExchangeType`
- `FastRowCount`
- `FileTransferType`
- `GenerationChangeThreshold`
- `HistoryVerboseLevel`
- `InputMessageFile`
- `InteractiveResolution`
- `InterruptOnMessagePattern`
- `KeepAliveMessageInterval`
- `LoginTimeout`
- `MaxBcpThreads`
- `MaxDownloadChanges`
- `MaxUploadChanges`
- `MetadataRetentionCleanup`
- `NumDeadlockRetries`
- `Output`
- `OutputMessageFile`
- `OutputVerboseLevel`
- `PacketSize`
- `ParallelUploadDownload`
- `PauseOnMessagePattern`
- `PauseTime`
- `PollingInterval`
- `ProcessMessagesAtPublisher`
- `ProcessMessagesAtSubscriber`
- `QueryTimeout`
- `QueueSizeMultiplier`
- `SrcThreads`
- `StartQueueTimeout`
- `SyncToAlternate`
- `UploadGenerationsPerBatch`
- `UploadReadChangesPerBatch`
- `UploadWriteChangesPerBatch`
- `UseInprocLoader`
- `Validate`
- `ValidateInterval`

For a Queue Reader Agent (`agent_type = 9`), if defined in the profile, the following properties can be changed:

- `HistoryVerboseLevel`
- `LoginTimeout`
- `Output`
- `OutputVerboseLevel`
- `PollingInterval`
- `QueryTimeout`
- `ResolverState`
- `SQLQueueMode`

To see what parameters are defined for a given profile, run `sp_help_agent_profile` and note the `profile_name` associated with the `profile_id`. With the appropriate `profile_id`, next run `sp_help_agent_parameters` using that `profile_id` to see the parameters associated with the profile. Parameters can be added to a profile by executing [sp_add_agent_parameter](sp-add-agent-parameter-transact-sql.md).

#### [ @parameter_value = ] N'*parameter_value*'

The new value of the parameter. *@parameter_value* is **nvarchar(255)**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_change_agent_parameter` is used in all types of replication.

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_change_agent_parameter`.

## Related content

- [Replication Agent Profiles](../replication/agents/replication-agent-profiles.md)
- [Replication Distribution Agent](../replication/agents/replication-distribution-agent.md)
- [Replication Log Reader Agent](../replication/agents/replication-log-reader-agent.md)
- [Replication Merge Agent](../replication/agents/replication-merge-agent.md)
- [Replication Queue Reader Agent](../replication/agents/replication-queue-reader-agent.md)
- [Replication Snapshot Agent](../replication/agents/replication-snapshot-agent.md)
- [sp_add_agent_parameter (Transact-SQL)](sp-add-agent-parameter-transact-sql.md)
- [sp_drop_agent_parameter (Transact-SQL)](sp-drop-agent-parameter-transact-sql.md)
- [sp_help_agent_parameter (Transact-SQL)](sp-help-agent-parameter-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
