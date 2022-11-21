---
title: "Types of Replication | Microsoft Docs"
description: Learn about the different types of replication that SQL Server provides for use in distributed applications.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "replication [SQL Server], types"
ms.assetid: c1655e8d-d14c-455a-a7f9-9d2f43e88ab4
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-current||>=sql-server-2016"
---
# Types of Replication
[!INCLUDE[sql-asdb-asdbmi](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides the following types of replication for use in distributed applications:  

| **Type** | **Description** |
|:-------- | :-------------- |
| [Transactional replication](transactional/transactional-replication.md)| Changes at the Publisher are delivered to the Subscriber as they occur (in near real time). The data changes are applied to the Subscriber in the same order and within the same transaction boundaries as they occurred on the publisher. | 
| [Merge replication](merge/merge-replication.md) | Data can be changed on both the Publisher and Subscriber, and are tracked with triggers. The Subscriber synchronizes with the Publisher when connected to the network and exchanges all rows that have changed between the Publisher and Subscriber since the last time synchronization occurred. | 
| [Snapshot replication](snapshot-replication.md) | Applies a snapshot from the Publisher to the Subscriber, which distributes data exactly as it appears at a specific moment in time, and does not monitor for updates to the data. When synchronization occurs, the entire snapshot is generated and sent to Subscribers.| 
| [Peer-to-peer](transactional/peer-to-peer-transactional-replication.md) | Built on the foundation of transactional replication, peer-to-peer replication propagates transactionally consistent changes in near real-time between multiple server instances. | 
| [Bidirectional](transactional/bidirectional-transactional-replication.md)| Bidirectional transactional replication is a specific transactional replication topology that allows two servers to exchange changes with each other: each server publishes data and then subscribes to a publication with the same data from the other server. | 
| [Updatable Subscriptions](transactional/updatable-subscriptions-for-transactional-replication.md) | Built on the foundation of transactional replication, when data is updated at a Subscriber for an updatable subscription, it is first propagated to the Publisher and then propagated to other Subscribers. | 
  
 
The type of replication you choose for an application depends on many factors, including the physical replication environment, the type and quantity of data to be replicated, and whether the data is updated at the Subscriber. The physical environment includes the number and location of computers involved in replication and whether these computers are clients (workstations, laptops, or handheld devices) or servers.  
  
Each type of replication typically begins with an initial synchronization of the published objects between the Publisher and Subscribers. This initial synchronization can be performed by replication with a *snapshot*, which is a copy of all of the objects and data specified by a publication. After the snapshot is created, it is delivered to the Subscribers. For some applications, snapshot replication is all that is required. For other types of applications, it is important that subsequent data changes flow to the Subscriber incrementally over time. Some applications also require that changes flow from the Subscriber back to the Publisher. Transactional replication and merge replication provide options for these types of applications.  
  
 
## See Also  
 [Replication Agents Overview](../../relational-databases/replication/agents/replication-agents-overview.md)
  
  
