---
title: "Configure Topology (Peer-to-Peer Replication) | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql12.rep.p2pwizard.peers.f1"
ms.assetid: 5377c59f-2e25-4852-a306-c87ae3dca9fd
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Configure Topology (Peer-to-Peer Replication)
  Use the **Configure topology** page to perform common configuration tasks, such as adding new nodes, deleting nodes, and adding new connections between existing nodes. The node you selected on the **Publication** page of this wizard is displayed on the design surface. To specify configuration options, right-click a node, a connection, or the design surface.  
  
> [!NOTE]  
>  The Configure Peer to Peer Topology Wizard requests topology information when the wizard is closed. If the wizard is closed and reopened before all nodes respond to the request for information, the wizard might show a partial network.  
  
## Options  
 The **Configure topology** page contains interface elements and options that are available when you right-click an element. The following table describes each interface element.  
  
|Interface Element|Description|  
|-----------------------|-----------------|  
|Design surface|Displays other interface elements. To add elements, right-click the design surface.|  
|![The first node in a topology](media/p2pwizard-firstnode.gif "The first node in a topology")|The original node in the topology. New nodes are initialized by using a copy of the publication database from the original node.|  
|![A node for which we have complete information](media/p2pwizard-complete.gif "A node for which we have complete information") or a later version, for which replication has complete information. To specify configuration options, right-click the node.|  
|![A node for which we have incomplete information](media/p2pwizard-incomplete.gif "A node for which we have incomplete information")|A node for which replication has incomplete information. To specify configuration options, right-click the node. Replication has incomplete information because of one of the following reasons:<br /><br /> The node is running an instance of [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], which does not store all the metadata required by the wizard.<br /><br /> The node is running a later version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], but replication cannot retrieve subscription information from the node. To troubleshoot this situation:<br />-Make sure that the database at the node is online and that you can connect to it by using the same credentials as the Distribution Agents that connect to the node.<br />-Make sure that the Log Reader Agent and all Distribution Agents that connect to the node are running.<br />-Make sure that the refresh timeout is set high enough to gather all topology information. To set the time-out, right-click the design surface, and click **Set Refresh Timeout**.|  
|Gray line with arrows|The connection between two nodes. To add a connection, right-click one of the nodes that you want to connect. To remove a connection, right-click the connection.<br /><br /> If the line has a single arrow, replication has incomplete information for one of the nodes.|  
  
### Options for the Design Surface  
 **Redraw Graph**  
 Redraw the objects on the design surface without refreshing the topology. Redrawing might provide a better view of the topology.  
  
 **Refresh Topology**  
 Query each node in the topology, and display updated information about each node. With many nodes, this process can take several minutes.  
  
 If the wizard requests topology information, and then you close and reopen the wizard before all nodes respond to the request, this page might not display all nodes in the topology.  
  
 **Add a New Peer Node**  
 Add an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to the peer-to-peer topology. Adding an instance as a node creates a publication at that instance after the wizard completes. After you add the node, right-click it to add a connection between the new node and an existing node.  
  
 To participate in a peer-to-peer topology, the instance must meet the following requirements:  
  
-   It must already be configured as a Distributor or be associated with a remote Distributor.  
  
-   It must contain a copy of the database involved in replication. This copy is typically a restored backup of the original publication database.  
  
 **Select Node(s) to View**  
 Select which nodes to view on the design surface. This option is useful if the topology has a large number of nodes. Be aware that you can add connections only between nodes that are visible on the design surface.  
  
 **Set Refresh Timeout**  
 Specify how long the refresh process can run before the operation times out.  
  
### Options for Each Node  
 **Add a New Peer Connection**  
 Add a connection between two nodes. For example, if you add a connection between Node A and Node B, replication adds two subscriptions: The first enables Node A to receive changes from the publication at Node B, and the second enables Node B to receive changes from the publication at Node A.  
  
 **Delete Peer Node**  
 Removes a node from the topology. For example, if you remove Node C, the publication at that node is removed. Subscriptions between Node A and Node C, and Node B and Node C are also removed. The database at Node C is not deleted, and publishing and distribution are not disabled.  
  
> [!NOTE]  
>  When you configure peer-to-peer replication, you specify an ID for each node. This ID, which must be unique across all nodes in the topology, is stored in the originator_id column in the [MSpeer_originatorid_history](/sql/relational-databases/system-tables/mspeer-originatorid-history-transact-sql) system table. If a node is removed from the topology, the ID is still retained in the history table. The ID is retained to prevent false conflicts from occurring if there are changes from the removed node that are still being replicated across the topology. If you want to reuse the ID for a new node, you must first manually delete the ID from the MSpeer_originatorid_history table at all nodes. Before you delete an ID for a node, execute [sp_requestpeerresponse](/sql/relational-databases/system-stored-procedures/sp-requestpeerresponse-transact-sql) to verify that all changes that originated from that node have been replicated.  
  
 **Connect to ALL Displayed Nodes**  
 Adds connections between the selected node and all other nodes. For example, if you selected this option for Node C in a three node topology, replication adds four subscriptions: two that enable Node A and Node B to receive changes from the publication at Node C, and two that enable Node C to receive changes from the publications at Node A and Node B.  
  
 **Select Node(s) to View**  
 Select which nodes to view on the design surface. This option is useful if the topology has a large number of nodes. Be aware that you can add connections only between nodes that are visible on the design surface.  
  
### Options for the Connection Arrows  
 **Remove Peer Connection**  
 Remove a connection between two nodes. For example, if you remove a connection between Node A and Node B, replication drops two subscriptions: the one that enables Node A to receive changes from the publication at Node B, and the one that enables Node B to receive changes from the publication at Node A.  
  
## See Also  
 [Configure Publishing and Distribution](configure-publishing-and-distribution.md)   
 [Administer a Peer-to-Peer Topology &#40;Replication Transact-SQL Programming&#41;](administration/administer-a-peer-to-peer-topology-replication-transact-sql-programming.md)   
 [Peer-to-Peer Transactional Replication](transactional/peer-to-peer-transactional-replication.md)  
  
  
