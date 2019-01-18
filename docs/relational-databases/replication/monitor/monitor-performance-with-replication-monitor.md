---
title: "Monitor Performance with Replication Monitor | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "monitoring performance [SQL Server replication], Replication Monitor"
  - "Log Reader Agent, monitoring"
  - "Replication Monitor, performance"
  - "Merge Agent, monitoring"
  - "Queue Reader Agent, monitoring"
  - "Snapshot Agent, monitoring"
  - "Distribution Agent, monitoring"
  - "monitoring performance [SQL Server replication]"
ms.assetid: f212397d-1bfd-496b-a246-668952891d09
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Monitor Performance with Replication Monitor
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Replication Monitor allows you to monitor the performance of transactional replication and merge replication in the following ways:  
  
-   Setting warnings and thresholds  
  
-   Viewing performance measurements  
  
-   Determining latency with tracer tokens (transactional replication)  
  
-   Viewing detailed synchronization statistics (merge replication)  
  
-   Viewing transactions and delivery time (transactional replication)  
  
## Set Warnings and Thresholds  
 Replication Monitor allows you to enable warnings for a number of performance conditions. When you enable a warning, you specify a threshold. When that threshold is met or exceeded, a warning is displayed in the **Status** column for the subscription and the publication with which it synchronizes (unless an issue with a higher priority needs to be displayed). In addition to displaying a warning in Replication Monitor, reaching a threshold can also trigger an alert. You can enable warnings for the following performance conditions:  
  
-   Exceeding the specified latency (the amount of time that elapses between a transaction being committed at the Publisher and the corresponding transaction being committed at the Subscriber).  
  
     This applies to transactional replication. If the specified threshold is met or exceeded, the status is displayed as **Performance critical**.  
  
-   Exceeding the specified synchronization time.  
  
     This applies to merge replication. If the specified threshold is met or exceeded, the status is displayed as **Long-running merge**. You can specify different thresholds for dial-up and Local Area Network (LAN) connections.  
  
-   Falling short of processing the specified number of rows in a given amount of time.  
  
     This applies to merge replication. If the specified threshold is met or exceeded, the status is displayed as **Performance critical**. You can specify different thresholds for dial-up and LAN connections.  
  
 For more information, see [Set Thresholds and Warnings in Replication Monitor](../../../relational-databases/replication/monitor/set-thresholds-and-warnings-in-replication-monitor.md).  
  
## View Performance Measurements  
 Replication Monitor displays performance quality values for transactional replication and merge replication in the **Current Average Performance** and **Current Worst Performance** columns for publications and the **Performance** column for subscriptions. The values are:  
  
-   Excellent  
  
-   Good  
  
-   Fair  
  
-   Poor  
  
-   Critical (transactional replication only)  
  
 The values are determined in the following ways:  
  
-   For transactional replication, performance quality is determined by the latency threshold. If the threshold is not set, a value is not displayed. The following table shows the correlation between the threshold and the performance quality value. For example, if the threshold is set to 60 seconds and the actual latency is 30 seconds, latency is 50% of the threshold, resulting in a value of Good.  
  
    |Excellent|Good|Fair|Poor|Critical|  
    |---------------|----------|----------|----------|--------------|  
    |0 – 34%|35 – 59%|60 – 84%|85 – 99%|100% +|  
  
-   For merge replication, performance quality is independent of either threshold (the row processing threshold does determine if a value of **Performance critical** is displayed in the **Status** column). Performance quality is determined by comparing individual subscription performance to the average historical performance of subscriptions to the publication that have the same connection type (dial-up or LAN). Replication Monitor displays a value after five synchronizations have occurred with 50 or more changes each over the same type of connection. If there have been less than five synchronizations with 50 or more changes or the most recent synchronization has less than 50 changes, Replication Monitor does not display a value.  
  
     The following table shows the correlation between the average performance and the performance quality value. For example, if ten Subscribers have synchronized over a LAN connection with an average rate of 100 rows per second, and one of the subscriptions then synchronizes at a rate of 125 rows per second, the performance for that Subscriber's synchronization is 125% of the average, resulting in a value of Good.  
  
    |Excellent|Good|Fair|Poor|  
    |---------------|----------|----------|----------|  
    |151+%|76 – 150%|26 – 75%|0 – 25%|  
  
 For more information about viewing subscription information, see [View Information and Perform Tasks using Replication Monitor](../../../relational-databases/replication/monitor/view-information-and-perform-tasks-replication-monitor.md).  
  
## Determine Latency with Tracer Tokens  
 Transactional replication allows you to measure the latency in a system by inserting a token (a small amount of data) in the transaction log of the publication database and recording how long it takes to arrive at the Distributor and Subscribers. The token also allows you to identify if data is not reaching the Distributor or Subscriber. For more information, see [Measure Latency and Validate Connections for Transactional Replication](../../../relational-databases/replication/monitor/measure-latency-and-validate-connections-for-transactional-replication.md).  
  
## View Detailed Synchronization Performance for Merge Replication  
 For merge replication, Replication Monitor displays detailed statistics for each article processed during synchronization, including the amount of time spent in each processing phase (uploading changes, downloading changes, and so on). It can help pinpoint specific tables that are causing slow downs and is the best place to troubleshoot performance issues with merge subscriptions. For more information on viewing detailed statistics, see [View Information and Perform Tasks using Replication Monitor](../../../relational-databases/replication/monitor/view-information-and-perform-tasks-replication-monitor.md).  
  
## View Transactions and Delivery Time for Transactional Replication  
 For transactional replication, Replication Monitor displays information about the number of transactions in the distribution database that have not yet been distributed to a Subscriber and the estimated time for distributing these transactions. For more information, see [View Information and Perform Tasks using Replication Monitor](../../../relational-databases/replication/monitor/view-information-and-perform-tasks-replication-monitor.md).  
  
## See Also  
 [Monitoring Replication](../../../relational-databases/replication/monitor/monitoring-replication.md)   
 [Set Thresholds and Warnings in Replication Monitor](../../../relational-databases/replication/monitor/set-thresholds-and-warnings-in-replication-monitor.md)  
  
  
