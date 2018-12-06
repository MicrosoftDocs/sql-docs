---
title: "Other Replication Upgrade Issues | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "system tables [SQL Server], replication"
  - "passwords [SQL Server replication]"
  - "versions [SQL Server replication]"
  - "connections [SQL Server replication]"
  - "scripts [SQL Server replication]"
  - "ActiveX controls [SQL Server replication]"
ms.assetid: 8a5e74be-4992-4f17-b20c-c3dce8f49329
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Other Replication Upgrade Issues
  This topic covers a number of upgrade issues that are not reported by Upgrade Advisor.  
  
## Versions Supported  
 [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] supports upgrading replicated databases from earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You do not have to stop activity at other nodes while a node is being upgraded. Ensure that you adhere to the rules regarding which versions are supported in a topology.  
  
 When you replicate between or among different versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you are usually limited to the functionality of the earliest version that is being used.  
  
> [!NOTE]  
>  Because the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on-disk storage format is the same in the 64-bit and 32-bit environments, a replication topology can combine server instances that run in a 32-bit environment and server instances that run in a 64-bit environment.  
  
 For all types of replication, the Distributor version must be no earlier than the Publisher version. (Frequently, the Distributor is the same instance as the Publisher.)  
  
 For transactional replication, a Subscriber to a transactional publication can be any version within two versions of the Publisher version.  
  
 For merge replication, a Subscriber to a merge publication can be any version no later than the Publisher version.  
  
## Merge Replication Batches Changes  
 Changes that are made by the Merge Agent are batched to improve performance; therefore, more than one row can be inserted, updated, or deleted within a single statement. If any published tables in the publication or subscription databases have triggers, ensure that the triggers can handle multirow inserts, updates, and deletes. For more information, see "Multirow Considerations for DML Triggers" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## ActiveX Control Changes  
 The following changes have been made for replication ActiveX controls:  
  
-   All ActiveX controls are marked as unsafe for scripting and initialization.  
  
-   The Snapshot ActiveX control has been dropped. You can create and manage snapshots by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or programmatically by using replication stored procedures. For more information, see the topics "How to: Create and Apply the Initial Snapshot ([!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)])" and "How to: Create the Initial Snapshot (Replication Transact-SQL Programming)" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
-   The Distribution ActiveX control and Merge ActiveX control have been deprecated. Similar functionality is provided for managed code applications using Replication Management Objects (RMO). For more information, see "Synchronizing Subscriptions (RMO Programming)" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## See Also  
 [Replication Upgrade Issues](../../../2014/sql-server/install/replication-upgrade-issues.md)  
  
  
