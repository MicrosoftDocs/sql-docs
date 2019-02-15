---
title: "Subscription, Undistributed Commands (Transactional Subscription, SQL Server 2005 and Later) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql12.rep.monitor.subscription.performance.f1"
ms.assetid: 5451561e-0ce3-4bb5-844a-88cd15b0b371
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Subscription, Undistributed Commands (Transactional Subscription, SQL Server 2005 and Later)
  The **Undistributed Commands** tab displays information about the number of commands in the distribution database that have not been delivered to the selected Subscriber, and the estimated time to deliver those commands. For information about viewing the commands in the distribution database, see [sp_replshowcmds &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-replshowcmds-transact-sql).  
  
## Options  
 **Number of commands in the distribution database waiting to be applied to this Subscriber**  
 The number of commands in the distribution database that have not been delivered to the selected Subscriber. A command consists of one Transact-SQL data manipulation language (DML) statement or one data definition language (DDL) statement.  
  
 **Estimated time to apply these commands, based on past performance**  
 The estimated amount of time to deliver commands to the Subscriber. If this value is greater than the amount of time required to generate and apply a snapshot to the Subscriber, consider reinitializing the Subscriber. For more information, see [Reinitialize Subscriptions](reinitialize-subscriptions.md).  
  
## See Also  
 [Start the Replication Monitor](monitor/start-the-replication-monitor.md)   
 [Monitor Performance with Replication Monitor](monitor/monitor-performance-with-replication-monitor.md)   
 [Monitoring Replication](monitoring-replication.md)  
  
  
