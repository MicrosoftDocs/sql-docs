---
title: "Bidirectional Transactional Replication | Microsoft Docs"
description: Bidirectional transactional replication lets two servers exchange changes. Each server publishes data and subscribes to a publication from the other server.
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "bidirectional replication"
  - "transactional replication, bidirectional replication"
  - "bidirectional transactional replication"
ms.assetid: 98772e95-67ed-4010-8108-5113dbe709ff
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Bidirectional Transactional Replication
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]
  Bidirectional transactional replication is a specific transactional replication topology that allows two servers to exchange changes with each other: each server publishes data and then subscribes to a publication with the same data from the other server. The `@loopback_detection` parameter of [sp_addsubscription &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md) is set to TRUE to ensure that changes are only sent to the Subscriber and do not result in the change being sent back to the Publisher.  
  
 In [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] and later versions, this topology is also supported by peer-to-peer transactional replication, but bidirectional replication can provide improved performance.  

If you want to add a subscription to a bi-directional publication by using the  the fully-qualified domain name (FQDN), verify that the server name (`@@SERVERNAME`) of the subscriber returns the FQDN. If the subscriber server name does not return the FQDN, changes that originate from that subscriber may cause primary key violations. 

  
## See Also  
 [Peer-to-Peer Transactional Replication](../../../relational-databases/replication/transactional/peer-to-peer-transactional-replication.md)  
  
  
