---
title: "Upgrading will modify queued updating subscriptions that use Message Queuing | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "subscriptions [SQL Server replication]"
  - "MSMQ [SQL Server replication]"
  - "queues [SQL Server replication]"
  - "queued updating subscriptions [SQL Server replication]"
ms.assetid: 97944de3-fbad-4db1-939a-dcd550bf5893
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Upgrading will modify queued updating subscriptions that use Message Queuing
  Upgrade Advisor detected that you might have one or more queued updating subscriptions that use [!INCLUDE[msCoName](../../includes/msconame-md.md)] Message Queuing (also known as MSMQ). Replication no longer supports Message Queuing; therefore, the subscriptions will be modified to use a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] queue.  
  
 Only a value of **sql** is allowed. Existing publications that use Message Queuing are modified during upgrade to use a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] queue. If you have applications that depend on queued updating using Message Queuing, these applications will have to be rewritten to accommodate a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] queue. For more information about queued updating subscriptions, see "Updatable Subscriptions for Transactional Replication" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
 Upgrade will remove the existing Message Queuing subscription queues if the Message Queuing service is running while [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is being upgraded.  
  
 If the Message Queuing service is not running, remove the queues manually after upgrade is complete. For more information about how to remove queues, see the Windows documentation.  
  
## See Also  
 [Replication Upgrade Issues](../../../2014/sql-server/install/replication-upgrade-issues.md)  
  
  
