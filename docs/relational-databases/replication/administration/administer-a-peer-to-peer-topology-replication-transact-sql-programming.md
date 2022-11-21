---
title: "Administer a Peer-to-Peer topology (Replication SP)"
description: Learn how to use replication stored procedures to administer a peer-to-peer topology, such as to add an article, or make a schema change. 
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "transactional replication, peer-to-peer replication"
ms.assetid: 4d0fa941-f9ea-4a14-aed9-34df593fc6f2
author: "MashaMSFT"
ms.author: "mathoma"
---
# Administer a Peer-to-Peer Topology (Replication Transact-SQL Programming)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Administering a peer-to-peer topology is similar to administering a typical transactional replication topology, but there are a number of areas with special considerations. The principal difference in administering a peer-to-peer topology is that some changes require the system to be *quiesced*. Quiescing a system involves stopping activity on published tables at all nodes and ensuring that each node has received all changes from all other nodes. For more information, see [Quiesce a Replication Topology &#40;Replication Transact-SQL Programming&#41;](../../../relational-databases/replication/administration/quiesce-a-replication-topology-replication-transact-sql-programming.md).  
  
> [!NOTE]  
>  In a peer-to-peer topology, the distributor cannot be using an earlier version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] than a pull subscriber.  
  
### To add an article to an existing configuration  
  
1.  Quiesce the system.  
  
2.  Stop the Distribution Agent at each node in the topology. For more information, see [Replication Agent Executables Concepts](../../../relational-databases/replication/concepts/replication-agent-executables-concepts.md) or [Start and Stop a Replication Agent &#40;SQL Server Management Studio&#41;](../../../relational-databases/replication/agents/start-and-stop-a-replication-agent-sql-server-management-studio.md).  
  
3.  Execute the CREATE TABLE statement to add the new table at each node in the topology.  
  
4.  Bulk copy the data for the new table manually at all nodes by using the [bcp utility](../../../tools/bcp-utility.md).  
  
5.  Execute [sp_addarticle](../../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md) to create the new article at each node in the topology. For more information, see [Define an Article](../../../relational-databases/replication/publish/define-an-article.md).  
  
    > [!NOTE]  
    >  After [sp_addarticle](../../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md) is executed, replication automatically adds the article to the subscriptions in the topology.  
  
6.  Restart the Distribution Agents at each node in the topology.  

### To make schema changes to a publication database  
  
1.  Quiesce the system.  
  
2.  Execute the data definition language (DDL) statements to modify the schema of published tables. For more information about supported schema changes, see [Make Schema Changes on Publication Databases](../../../relational-databases/replication/publish/make-schema-changes-on-publication-databases.md).  
  
3.  Before you resume activity on published tables, quiesce the system again. This ensures that schema changes have been received by all nodes before any new data changes are replicated.  
  
## Example  
 The following example demonstrates how to add a new table article to an existing peer-to-peer replication topology that has two nodes.  
  
 [!code-sql[HowTo#sp_addp2particle_createtables](../../../relational-databases/replication/codesnippet/tsql/administer-a-peer-to-pee_1.sql)]  
  
 [!code-sql[HowTo#sp_addp2particle_cmdline](../../../relational-databases/replication/codesnippet/tsql/administer-a-peer-to-pee_2.sql)]  
  
 [!code-sql[HowTo#sp_addp2particle_createarticle](../../../relational-databases/replication/codesnippet/tsql/administer-a-peer-to-pee_3.sql)]  
  
## See Also  
 [Replication Administration FAQ](../../../relational-databases/replication/administration/frequently-asked-questions-for-replication-administrators.yml)   
 [Back Up and Restore of SQL Server Databases](../../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md)   
 [Peer-to-Peer Transactional Replication](../../../relational-databases/replication/transactional/peer-to-peer-transactional-replication.md)  
  
  
