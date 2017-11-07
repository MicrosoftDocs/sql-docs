---
title: "Publication Types for Transactional Replication | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "transactional replication, publications"
ms.assetid: ad66aa34-3e37-401e-a6a1-fc1514eb6d50
caps.latest.revision: 32
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Publication Types for Transactional Replication
  Transactional replication offers three publication types:  
  
|Publication Type|Description|  
|----------------------|-----------------|  
|Standard transactional publication|Appropriate for topologies in which all data at the Subscriber is read-only (transactional replication does not enforce this at the Subscriber).<br /><br /> Standard transactional publications are created by default when using Transact-SQL or Replication Management Objects (RMO). When using the New Publication Wizard, they are created by selecting **Transactional publication** on the **Publication Type** page.<br /><br /> For more information about creating publications, see [Publish Data and Database Objects](../../../relational-databases/replication/publish/publish-data-and-database-objects.md).|  
|Transactional publication in a peer-to-peer topology|The characteristics of this publication type are:<br /><br /> -Each location has identical data and acts as both a Publisher and Subscriber.<br /><br /> -The same row can be changed only at one location at a time.<br /><br /> -This topology is best suited for server environments requiring high availability and read scalability.<br /><br /> <br /><br /> For more information, see [Peer-to-Peer Transactional Replication](../../../relational-databases/replication/transactional/peer-to-peer-transactional-replication.md).|  
  
## See Also  
 [Transactional Replication](../../../relational-databases/replication/transactional/transactional-replication.md)  
  
  