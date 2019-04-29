---
title: "Locks Event Category | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "Locks event category [SQL Server]"
  - "SQL Server event classes, Locks event category"
  - "event classes [SQL Server], Locks event category"
  - "lock escalation [SQL Server], locks event category"
ms.assetid: 27d6afa2-7dab-4fe7-a1ad-064b879dc654
author: stevestein
ms.author: sstein
manager: craigg
---
# Locks Event Category
  Use the event classes in the **Locks** event category to monitor locking activity in an instance of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. These event classes can help you investigate locking problems caused by multiple users reading and modifying data concurrently.  
  
 Because the [!INCLUDE[ssDE](../../includes/ssde-md.md)] often processes many locks, capturing the **Locks** event classes during a trace can incur significant overhead and result in large trace files or tables.  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[Deadlock Graph Event Class](deadlock-graph-event-class.md)|Provides an XML description of a deadlock.|  
|[Lock:Acquired Event Class](lock-acquired-event-class.md)|Indicates that a lock has been acquired on a resource, such as a row in a table.|  
|[Lock:Cancel Event Class](lock-cancel-event-class.md)|Tracks requests for locks that were canceled before the lock was acquired (for example, to prevent a deadlock).|  
|[Lock:Deadlock Chain Event Class](lock-deadlock-chain-event-class.md)|Monitors when deadlock conditions occur and which objects are involved.|  
|[Lock:Deadlock Event Class](lock-deadlock-event-class.md)|Tracks when a transaction has requested a lock on a resource already locked by another transaction, resulting in a deadlock.|  
|[Lock:Escalation Event Class](lock-escalation-event-class.md)|Indicates that a finer-grained lock has been converted to a coarser-grained lock.|  
|[Lock:Released Event Class](lock-released-event-class.md)|Tracks when a lock is released.|  
|[Lock:Timeout &#40;timeout &#62; 0&#41; Event Class](lock-timeout-timeout-0-event-class.md)|Tracks when lock requests cannot be completed because another transaction has a blocking lock on the requested resource. This event occurs only in situations where the lock time-out value is greater than zero.|  
|[Lock:Timeout Event Class](lock-timeout-event-class.md)|Tracks when lock requests cannot be completed because another transaction has a blocking lock on the requested resource.|  
  
  
