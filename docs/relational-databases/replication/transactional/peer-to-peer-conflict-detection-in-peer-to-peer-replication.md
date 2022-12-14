---
description: "Peer-to-Peer - Conflict Detection in Peer-to-Peer Replication"
title: "Conflict Detection in Peer-to-Peer Replication | Microsoft Docs"
ms.custom: ""
ms.date: 10/05/2021
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "transactional replication, peer-to-peer replication"
  - "peer-to-peer transactional replication, conflict detection"
ms.assetid: 754a1070-59bc-438d-998b-97fdd77d45ca
author: "MashaMSFT"
ms.author: "mathoma"
---
# Peer-to-Peer - Conflict Detection in Peer-to-Peer Replication

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

Peer-to-peer transactional replication lets you insert, update, or delete data at any node in a topology and have data changes propagated to the other nodes. Because you can change data at any node, data changes at different nodes could conflict with each other. If a row is modified at more than one node, it can cause a conflict or even a lost update when the row is propagated to other nodes.

Peer-to-peer replication provides the option to enable conflict detection across a peer-to-peer topology. This option helps prevent the issues that are caused by undetected conflicts, including inconsistent application behavior and lost updates. With this option enabled, by default a conflicting change is treated as a critical error that causes the failure of the Distribution Agent. In the event of a conflict, the topology remains in an inconsistent state until the conflict is resolved and the data is made consistent across the topology.

> [!NOTE]  
> To avoid potential data inconsistency, make sure that you avoid conflicts in a peer-to-peer topology, even with conflict detection enabled. To ensure that write operations for a particular row are performed at only one node, applications that access and change data must partition insert, update, and delete operations. This partitioning ensures that modifications to a given row that is originating at one node are synchronized with all other nodes in the topology before the row is modified by a different node. If an application requires sophisticated conflict detection and resolution capabilities, use merge replication. For more information, see [Merge Replication](../../../relational-databases/replication/merge/merge-replication.md) and [Detect and Resolve Merge Replication Conflicts](../../../relational-databases/replication/merge/advanced-merge-replication-conflict-detection-and-resolution.md).  

## Understanding conflicts and conflict detection  

In a single database, changes that are made to the same row by different applications do not cause a conflict. This is because transactions are serialized, and locks handle concurrent changes. In an asynchronous distributed system such as peer-to-peer replication, transactions act independently on each node; and there is no mechanism to serialize transactions across multiple nodes. A protocol like two-phase commit could be used, but this affects performance significantly.
  
In systems such as peer-to-peer replication, conflicts are not detected when changes are committed at individual peers. Instead, they are detected when those changes are replicated and applied at other peers. In peer-to-peer replication, conflicts are detected by the stored procedures that apply changes to each node, based on one or more hidden columns in each published table. 

Prior to [!INCLUDE [sssql19-md](../../../includes/sssql19-md.md)] CU 13, SQL Server adds one hidden column to each published table: `$sys_p2p_cd_id`. This hidden column stores an ID that combines an *originator ID* that you specify for each node and the version of the row.

On [!INCLUDE [sssql19-md](../../../includes/sssql19-md.md)] CU 13 and later, if you create the peer to peer publication with `@p2p_conflictdetection_policy = 'lastwriter'` then SQL Server adds an additional hidden column to each published table: `$sys_md_cd_id`. This hidden column stores the datetime as a `datetime2` data type.

During synchronization, the Distribution Agent executes procedures for each table. These procedures apply insert, update, and delete operations from other peers. If one of the procedures detects a conflict when it reads the hidden column value or values, it raises error 22815 that has a severity level of 16:  
  
`A conflict of type '%s' was detected at peer %d between peer %d (incoming), transaction id %s  and peer %d (on disk), transaction id %s`  
  
By default, this error causes the Distribution Agent to stop applying changes to that node. For information about how to handle the conflicts that are detected, see [Handling conflicts](#handling-conflicts).  
  
> [!NOTE]  
> The hidden column can be accessed only by a user that is logged in through the Dedicated Administrator Connection (DAC). For information about DAC, see [Diagnostic Connection for Database Administrators](../../../database-engine/configure-windows/diagnostic-connection-for-database-administrators.md).  
  
Peer-to-peer replication detects the following types of conflicts:  
  
- Insert-insert  
  
     All rows in each table participating in peer-to-peer replication are uniquely identified by using primary key values. An insert-insert conflict occurs when a row with the same key value was inserted at more than one node.  
  
- Update-update  
  
     Occurs when the same row was updated at more than one node.  
  
- Insert-update  
  
     Occurs if a row was updated at one node, but the same row was deleted and then reinserted at another node.  
  
- Insert-delete  
  
     Occurs if a row was deleted at one node, but the same row was deleted and then reinserted at another node.  
  
- Update-delete  
  
     Occurs if a row was updated at one node, but the same row was deleted at another node.  
  
- Delete-delete  
  
     Occurs when a row was deleted at more than one node.  
  
## Enabling Conflict Detection  

To use conflict detection, enable detection for all nodes. By default, conflict detection is enabled when you configure peer-to-peer replication in [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)]. We recommend that you have detection enabled, even in scenarios in which you do not expect any conflicts. Conflict detection can be enabled and disabled by using [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)] stored procedures:  
  
- You can enable and disable detection in [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)] either by using the **Subscription Options** page of the **Publication Properties** dialog box or the **Configure Topology** page of the Configure Peer-to-Peer Topology Wizard. 
  
     If you configure conflict detection by using [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)], the Distribution Agent is configured to stop applying changes when a conflict is detected.  
  
- You can also enable and disable detection by using the following stored procedures: 

  - [sp_addpublication](../../../relational-databases/system-stored-procedures/sp-addpublication-transact-sql.md)
  - [sp_configure_peerconflictdetection](../../../relational-databases/system-stored-procedures/sp-configure-peerconflictdetection-transact-sql.md).  
  
     If you configure conflict detection by using stored procedures, you can specify whether the Distribution Agent should stop applying changes when a conflict is detected. The default is for the agent to stop. We recommend that you use the default setting.  
  
## Handling conflicts

 When a conflict occurs in peer-to-peer replication, the peer-to-peer conflict detection alert is raised. Configure this alert so that you are notified when a conflict occurs. For more information about alerts, see [Use Alerts for Replication Agent Events](../../../relational-databases/replication/agents/use-alerts-for-replication-agent-events.md).  
 
 After the Distribution Agent stops and the alert is raised, use one of the following approaches to handle the conflicts that occurred:  
  
- Reinitialize the node where the conflict was detected from the backup of a node that contains the required data (the recommended approach). This method ensures that data is in a consistent state.  
  
- Try to synchronize the node again by enabling the Distribution Agent to continue to apply changes:  
  
    1.  Execute [sp_changepublication](../../../relational-databases/system-stored-procedures/sp-changepublication-transact-sql.md): specify 'p2p_continue_onconflict' for the @property parameter and **true** for the @value parameter.  
  
    2.  Restart the Distribution Agent.  
  
    3.  Verify the conflicts that were detected by using the conflict viewer and determine the rows that were involved, the type of conflict, and the winner. The conflict is resolved based on the originator ID value that you specified during configuration: the row that originated at the node with the highest ID wins the conflict. For more information, see [View Data Conflicts for Transactional Publications &#40;SQL Server Management Studio&#41;](../../../relational-databases/replication/view-data-conflicts-for-transactional-publications-sql-server-management-studio.md).  
  
    4.  Run validation to ensure that the conflicting rows converged correctly. For more information, see [Validate Replicated Data](../../../relational-databases/replication/validate-data-at-the-subscriber.md).  
  
        > [!NOTE]  
        >  If data is inconsistent after this step, you must manually update rows on the node that has the highest priority, and then let the changes propagate from this node. If there are no further conflicting changes in the topology, all nodes will be brought to a consistent state.  
  
    5.  Execute [sp_changepublication](../../../relational-databases/system-stored-procedures/sp-changepublication-transact-sql.md): specify 'p2p_continue_onconflict' for the @property parameter and **false** for the @value parameter.  

## Automatically handle conflicts with last write wins

Beginning with [!INCLUDE [sssql19-md](../../../includes/sssql19-md.md)] CU 13, you can configure peer-to-peer replication to automatically resolve conflicts by allowing the most recent insert or update to win the conflict. If either write deletes the row, SQL Server allows the delete to win the conflict. This method is known as last write wins.

Use stored procedures to configure last write wins. See [Configure last writer conflict detection & resolution](../../../relational-databases/replication/transactional/peer-to-peer/configure-last-writer.md). You can't configure last write wins with SQL Server Management Studio.

> [!Note]
> Peer-to-Peer replication with Last Writer Wins depends on having the clocks of participating nodes in sync for reliable outcomes. If the clocks of participating servers become too far out of sync, the outcome of a conflict resolution may be unexpected and/or undesired. For example, if server A has an accurate clock, but server B is a week behind, then server A will be chosen to win every conflict, even if objectively it was not last to update the row.  If it is not possible to keep the clocks within the tolerance you require for outcome resolution, you may wish to choose a different conflict resolution strategy.

## See Also  
 [Peer-to-Peer Transactional Replication](../../../relational-databases/replication/transactional/peer-to-peer-transactional-replication.md)  
  
  
