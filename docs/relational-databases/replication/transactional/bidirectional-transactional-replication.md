---
title: "Bidirectional Transactional Replication"
description: Bidirectional transactional replication lets two servers exchange changes. Each server publishes data and subscribes to a publication from the other server.
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: concept-article
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "bidirectional replication"
  - "transactional replication, bidirectional replication"
  - "bidirectional transactional replication"
monikerRange: "=azuresqldb-mi-current || >=sql-server-2017"
---
# Bidirectional transactional replication
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]
  Bidirectional transactional replication is a specific transactional replication topology that allows two servers to exchange changes with each other: each server publishes data and then subscribes to a publication with the same data from the other server. Set the `@loopback_detection` parameter of [sp_addsubscription &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md) to TRUE to ensure that changes are only sent to the Subscriber and don't result in the change being sent back to the Publisher.  
  
 In [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] and later versions, peer-to-peer transactional replication also supports this topology, but bidirectional replication can provide improved performance.  

To add a subscription to a bi-directional publication by using the fully qualified domain name (FQDN), verify that the server name (`@@SERVERNAME`) of the subscriber returns the FQDN. If the subscriber server name doesn't return the FQDN, changes that originate from that subscriber might cause primary key violations. 

  
## Related content

- [Peer-to-Peer - Transactional Replication](peer-to-peer-transactional-replication.md)
