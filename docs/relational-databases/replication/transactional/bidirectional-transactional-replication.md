---
title: "Bidirectional Transactional Replication | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "bidirectional replication"
  - "transactional replication, bidirectional replication"
  - "bidirectional transactional replication"
ms.assetid: 98772e95-67ed-4010-8108-5113dbe709ff
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Bidirectional Transactional Replication
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Bidirectional transactional replication is a specific transactional replication topology that allows two servers to exchange changes with each other: each server publishes data and then subscribes to a publication with the same data from the other server. The **@loopback_detection** parameter of [sp_addsubscription &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md) is set to TRUE to ensure that changes are only sent to the Subscriber and do not result in the change being sent back to the Publisher.  
  
 In [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] and later versions, this topology is also supported by peer-to-peer transactional replication, but bidirectional replication can provide improved performance.  
  
## See Also  
 [Peer-to-Peer Transactional Replication](../../../relational-databases/replication/transactional/peer-to-peer-transactional-replication.md)  
  
  
