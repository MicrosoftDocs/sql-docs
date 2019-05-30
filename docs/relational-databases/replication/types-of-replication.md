---
title: "Types of Replication | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "replication [SQL Server], types"
ms.assetid: c1655e8d-d14c-455a-a7f9-9d2f43e88ab4
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Types of Replication
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md.md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides the following types of replication for use in distributed applications:  
  
-   Transactional replication. For more information, see [Transactional Replication](../../relational-databases/replication/transactional/transactional-replication.md) 
  
-   Merge replication. For more information, see [Merge Replication](../../relational-databases/replication/merge/merge-replication.md).  
  
-   Snapshot replication. For more information, see [Snapshot Replication](../../relational-databases/replication/snapshot-replication.md).  
  
 The type of replication you choose for an application depends on many factors, including the physical replication environment, the type and quantity of data to be replicated, and whether the data is updated at the Subscriber. The physical environment includes the number and location of computers involved in replication and whether these computers are clients (workstations, laptops, or handheld devices) or servers.  
  
 Each type of replication typically begins with an initial synchronization of the published objects between the Publisher and Subscribers. This initial synchronization can be performed by replication with a *snapshot*, which is a copy of all of the objects and data specified by a publication. After the snapshot is created, it is delivered to the Subscribers. For some applications, snapshot replication is all that is required. For other types of applications, it is important that subsequent data changes flow to the Subscriber incrementally over time. Some applications also require that changes flow from the Subscriber back to the Publisher. Transactional replication and merge replication provide options for these types of applications.  
  
 Data changes are not tracked for snapshot replication; each time a snapshot is applied, it completely overwrites the existing data. Transactional replication tracks changes through the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] transaction log, and merge replication tracks changes through triggers and metadata tables.  

## Azure SQL Database replication supportability

The following table describes the roles that an Azure SQL Database resource can be in a replication topology: 

| Role | Single and pooled databases | Instance databases |
| :----| :------------- | :--------------- |
| **Publisher** | No | Yes | 
| **Distributor** | No | Yes|
| **Pull subscriber** | No | Yes|
| **Push Subscriber**| Yes | Yes|
| &nbsp; | &nbsp; | &nbsp; |

The following table describes what implementing of Azure SQL database supports what type of replication: 

| Replication | Single and pooled databases | Instance  databases|
| :----| :------------- | :--------------- |
| [**Transactional**](https://docs.microsoft.com/sql/relational-databases/replication/transactional/transactional-replication) | Yes (only as subscriber) | Yes | 
| [**Snapshot**](https://docs.microsoft.com/sql/relational-databases/replication/snapshot-replication) | Yes (only as subscriber) | Yes|
| [**Merge replication**](https://docs.microsoft.com/sql/relational-databases/replication/merge/merge-replication) | No | No|
| [**Peer-to-peer**](https://docs.microsoft.com/sql/relational-databases/replication/transactional/peer-to-peer-transactional-replication) | No | No|
| **One-way** | Yes | Yes|
| [**Bidirectional**](https://docs.microsoft.com/sql/relational-databases/replication/transactional/bidirectional-transactional-replication) | No | Yes|
| [**Updatable subscriptions**](https://docs.microsoft.com/sql/relational-databases/replication/transactional/updatable-subscriptions-for-transactional-replication) | No | No|
| &nbsp; | &nbsp; | &nbsp; |
  
## See Also  
 [Replication Agents Overview](../../relational-databases/replication/agents/replication-agents-overview.md)  
  
  
