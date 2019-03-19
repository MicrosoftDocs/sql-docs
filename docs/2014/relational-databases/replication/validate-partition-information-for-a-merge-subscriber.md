---
title: "Validate Partition Information for a Merge Subscriber | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "merge replication data validation [SQL Server replication], partitions"
  - "parameterized filters [SQL Server replication], validating partition information"
  - "validating partition information"
ms.assetid: c059553e-df2c-4333-ba79-e8d6e2890c34
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Validate Partition Information for a Merge Subscriber
  When you define a parameterized row filter for a merge publication, you use a function that references Subscriber information, such as the Subscriber's login name. By default, replication validates Subscriber information based on that function before each synchronization and whenever a snapshot is applied at the Subscriber. The validation process ensures that data is partitioned correctly for each Subscriber. Validation behavior is controlled by the **validate_subscriber_info** publication property, which can be changed using [sp_changemergepublication &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-changemergepublication-transact-sql) or on the **Subscription Options** page of the **Publication Properties** dialog box. For more information about changing publication properties, see [View and Modify Publication Properties](publish/view-and-modify-publication-properties.md).  
  
## How Partition Validation Works  
 When a publication is filtered, for example, using the function **SUSER_SNAME()**, the Merge Agent applies the initial snapshot to each Subscriber based on data that is valid for the **SUSER_SNAME()** expression.  
  
 If validation is enabled, when the Subscriber reconnects to the Publisher for the next synchronization, the Merge Agent validates the information at the Subscriber and ensures that each Subscriber's partition is the same as the one received in the initial snapshot. For each subsequent merge or snapshot application, the Merge Agent validates each Subscriber's partition.  
  
 If the Merge Agent detects that the function used in the filtering expression returns a different value than it did at the initial snapshot, the merge or snapshot application fails, and that Subscriber's subscription might require reinitialization. Reinitialization might be necessary to prevent problems that can arise if the merge settings of a Subscriber are changed, but it might be sufficient to change information at the Subscriber, such as the login name, back to the value used at the time of the original snapshot.  
  
 When the Merge Agent validates a partition, in addition to validating the partition against the values returned by any functions used in filtering expressions, the agent also checks whether the snapshot was generated prior to changes that invalidate it, such as metadata cleanup operations or schema changes. If a partitioned snapshot is too old, the Merge Agent will return an error and you must regenerate a partitioned snapshot for that Subscriber based on a current regular snapshot.  
  
## See Also  
 [Replication Administration FAQ](administration/frequently-asked-questions-for-replication-administrators.md)   
 [Best Practices for Replication Administration](administration/best-practices-for-replication-administration.md)   
 [Reinitialize Subscriptions](reinitialize-subscriptions.md)   
 [Validate Replicated Data](validate-data-at-the-subscriber.md)  
  
  
