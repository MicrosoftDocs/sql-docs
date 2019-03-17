---
title: "Replication Agent Profiles | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "Distribution Agent, profiles"
  - "replication [SQL Server], agents and profiles"
  - "replication agent profiles [SQL Server]"
  - "Merge Agent, profiles"
  - "agents [SQL Server replication], profiles"
  - "Queue Reader Agent, profiles"
  - "profiles [SQL Server], replication agents"
  - "Snapshot Agent, profiles"
  - "Log Reader Agent, profiles"
ms.assetid: 0e980725-e42f-4283-94cb-d8a6dba5df62
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Replication Agent Profiles
  When replication is configured, a set of agent profiles is installed on the Distributor. An agent profile contains a set of parameters that are used each time an agent runs: each agent logs in to the Distributor during its startup process and queries for the parameters in its profile. For merge subscriptions that use Web synchronization, profiles are downloaded and stored at the Subscriber. If the profile is changed, the profile at the Subscriber is updated the next time the Merge Agent runs. For more information about Web synchronization, see [Web Synchronization for Merge Replication](../web-synchronization-for-merge-replication.md).  
  
 Replication provides a default profile for each agent and additional predefined profiles for the Log Reader Agent, Distribution Agent, and Merge Agent. In addition to the profiles provided, you can create profiles suited to your application requirements. An agent profile allows you to change key parameters easily for all agents associated with that profile. For example, if you have 20 Snapshot Agents and need to change the query timeout value (the **-QueryTimeout** parameter), you can update the profile used by the Snapshot Agents and all agents of that type will begin using the new value automatically the next time they run.  
  
 You might also have different profiles for different instances of an agent. For example, a Merge Agent that connects to the Publisher and Distributor over a dialup connection could use a set of parameters that are better suited to the slower communications link by using the **slow link** profile.  
  
> [!NOTE]  
>  If you specify a value for an agent parameter on the command line, that value overrides the value set for the same parameter in the agent profile.  
  
 **To use and modify agent profiles**  
  
-   [Work with Replication Agent Profiles](replication-agent-profiles.md)  
  
## Snapshot Agent Profiles  
 The following table shows the parameters defined in the default profile for the Snapshot Agent. For more information on these parameters, see [Replication Snapshot Agent](replication-snapshot-agent.md).  
  
||default|  
|-|-------------|  
|**-BcpBatchSize**|100000|  
|**-HistoryVerboseLevel**|2|  
|**-LoginTimeout**|15|  
|**-QueryTimeout**|1800|  
  
## Log Reader Agent Profiles  
 The following table shows the parameters defined in the profiles for the Log Reader Agent. Each column in the table represents a named profile. For more information on these parameters, see [Replication Log Reader Agent](replication-log-reader-agent.md).  
  
||default|verbose history|  
|-|-------------|---------------------|  
|**-HistoryVerboseLevel**|1|2|  
|**-LoginTimeout**|15|15|  
|**-LogScanThreshold**|500000|500000|  
|**-PollingInterval**|5|5|  
|**-QueryTimeout**|1800|1800|  
|**-ReadBatchSize**|500|500|  
  
## Distribution Agent Profiles  
 The following table shows the parameters defined in the profiles for the Distribution Agent. Each column in the table represents a named profile. For more information on these parameters, see [Replication Distribution Agent](replication-distribution-agent.md).  
  
||default|verbose history|Windows Synchronization Manager|Continue on data consistency errors|Distribution Profile for OLEDB streaming|  
|-|-------------|---------------------|-------------------------------------|-----------------------------------------|----------------------------------------------|  
|**-BcpBatchSize**|100000|100000|1000|100000|2147473647|  
|**-CommitBatchSize**|100|100|100|100|100|  
|**-CommitBatchThreshold**|1000|1000|1000|1000|1000|  
|**-HistoryVerboseLevel**|1|2|1|1|1|  
|**-KeepAliveMessageInterval**|300|300|300|300|300|  
|**-LoginTimeout**|15|15|15|15|15|  
|**-MaxBcpThreads**|1|1|1|1|1|  
|**-MaxDeliveredTransactions**|0|0|0|0|0|  
|**-OledbStreamThreshold**|NULL|NULL|NULL|NULL|32768|  
|**-PacketSize**|NULL|NULL|NULL|NULL|32768|  
|**-PollingInterval**|5|5|5|5|5|  
|**-QueryTimeout**|1800|1800|1800|1800|1800|  
|**-SkipErrors**|NULL|NULL|NULL|**-SkipErrors** 2601:2627:20598|NULL|  
|**-TransactionsPerHistory**|100|100|100|100|100|  
|**-UseOledbStreaming**|NULL|NULL|NULL|NULL|**-UseOledbStreaming**|  
  
## Merge Agent Profiles  
 The following table shows the parameters defined in the profiles for the Merge Agent. Each column in the table represents a named profile. For more information on these parameters, see [Replication Merge Agent](replication-merge-agent.md).  
  
||default|verbose history|Windows Synchronization Manager|rowcount validation|rowcount and checksum validation|slow link|high volume server-to-server|  
|-|-------------|---------------------|-------------------------------------|-------------------------|--------------------------------------|---------------|------------------------------------|  
|**-BcpBatchSize**|100000|100000|1000|100000|100000|100000|100000|  
|**-ChangesPerHistory**|100|50|50|100|100|100|1000|  
|**-DestThreads**|2|1|1|1|1|1|4|  
|**-DownloadGenerationsPerBatch**|50|50|50|50|50|1|500|  
|**-DownloadReadChangesPerBatch**|100|100|100|100|100|100|100|  
|**-DownloadWriteChangesPerBatch**|100|100|100|100|100|100|100|  
|**-FastRowCount**|1|1|1|1|1|1|1|  
|**-HistoryVerboseLevel**|2|3|1|1|2|1|2|  
|**-KeepAliveMessageInterval**|300|300|300|300|300|300|300|  
|**-LoginTimeout**|15|15|15|15|15|15|15|  
|**-MaxDownloadChanges**|0|0|0|0|0|0|0|  
|**-MaxUploadChanges**|0|0|0|0|0|0|0|  
|**-MetadataRetentionCleanup**|1|1|1|1|1|1|1|  
|**-NumDeadlockRetries**|5|5|5|5|5|5|5|  
|**-ParallelUploadDownload**|NULL|NULL|NULL|NULL|NULL|NULL|1|  
|**-PollingInterval**|60|60|60|60|60|60|60|  
|**-QueryTimeout**|300|300|300|300|300|300|600|  
|**-QueueSizeMultiplier**|NULL|NULL|NULL|NULL|NULL|NULL|5|  
|**-SrcThreads**|2|2|2|2|2|1|3|  
|**-StartQueueTimeout**|0|0|0|0|0|0|0|  
|**-UploadGenerationsPerBatch**|50|50|50|50|50|1|500|  
|**-UploadReadChangesPerBatch**|100|100|100|100|100|100|100|  
|**-UploadWriteChangesPerBatch**|100|100|100|100|100|100|100|  
|**-Validate**|0|0|0|1|3|0|0|  
|**-ValidateInterval**|60|60|60|60|60|60|60|  
  
## Queue Reader Agent Profiles  
 The following table shows the parameters defined in the default profile for the Queue Reader Agent. For more information on these parameters, see [Replication Queue Reader Agent](replication-queue-reader-agent.md).  
  
||default|  
|-|-------------|  
|**-HistoryVerboseLevel**|1|  
|**-LoginTimeout**|15|  
|**-PollingInterval**|5|  
|**-QueryTimeout**|1800|  
  
## See Also  
 [Replication Agent Administration](replication-agent-administration.md)   
 [View and Modify Replication Agent Command Prompt Parameters &#40;SQL Server Management Studio&#41;](view-and-modify-replication-agent-command-prompt-parameters.md)   
 [Replication Agent Executables Concepts](../concepts/replication-agent-executables-concepts.md)  
  
  
