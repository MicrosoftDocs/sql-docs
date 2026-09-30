---
title: Monitor Performance with Replication Monitor
description: Replication Monitor helps you track transactional and merge replication performance in SQL Server. Learn to set thresholds, read metrics, and use tracer tokens.
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: how-to
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "monitoring performance [SQL Server replication], Replication Monitor"
  - "Log Reader Agent, monitoring"
  - "Replication Monitor, performance"
  - "Merge Agent, monitoring"
  - "Queue Reader Agent, monitoring"
  - "Snapshot Agent, monitoring"
  - "Distribution Agent, monitoring"
  - "monitoring performance [SQL Server replication]"
monikerRange: "=azuresqldb-mi-current || >=sql-server-2017"
---
# Monitor performance with Replication Monitor
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]
  [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] By using Replication Monitor, you can monitor the performance of Transactional Replication and merge replication in the following ways:  
  
-   Set warnings and thresholds  
  
-   View performance measurements  
  
-   Determine latency with tracer tokens (Transactional Replication)  
  
-   View detailed synchronization statistics (merge replication)  
  
-   View transactions and delivery time (Transactional Replication)  
  
## Set warnings and thresholds  
 Replication Monitor allows you to enable warnings for a number of performance conditions. When you enable a warning, you specify a threshold. When that threshold is met or exceeded, a warning appears in the **Status** column for the subscription and the publication with which it synchronizes (unless an issue with a higher priority needs to be displayed). In addition to displaying a warning in Replication Monitor, reaching a threshold can also trigger an alert. You can enable warnings for the following performance conditions:  
  
-   Exceeding the specified latency (the amount of time that elapses between a transaction being committed at the Publisher and the corresponding transaction being committed at the Subscriber).  
  
     This condition applies to Transactional Replication. If the specified threshold is met or exceeded, the status appears as **Performance critical**.  
  
-   Exceeding the specified synchronization time.  
  
     This condition applies to merge replication. If the specified threshold is met or exceeded, the status appears as **Long-running merge**. You can specify different thresholds for dial-up and Local Area Network (LAN) connections.  
  
-   Falling short of processing the specified number of rows in a given amount of time.  
  
     This condition applies to merge replication. If the specified threshold is met or exceeded, the status appears as **Performance critical**. You can specify different thresholds for dial-up and LAN connections.  
  
 For more information, see [Set Thresholds and Warnings in Replication Monitor](../../../relational-databases/replication/monitor/set-thresholds-and-warnings-in-replication-monitor.md).  
  
## View performance measurements  
 Replication Monitor displays performance quality values for Transactional Replication and merge replication in the **Current Average Performance** and **Current Worst Performance** columns for publications and the **Performance** column for subscriptions. The values are:  
  
-   Excellent  
  
-   Good  
  
-   Fair  
  
-   Poor  
  
-   Critical (Transactional Replication only)  
  
 You determine the values as follows:  
  
-   For Transactional Replication, performance quality is determined by the latency threshold. If you don't set the threshold, Replication Monitor doesn't display a value. The following table shows the correlation between the threshold and the performance quality value. For example, if you set the threshold to 60 seconds and the actual latency is 30 seconds, latency is 50% of the threshold, resulting in a value of Good.  
  
    |Excellent|Good|Fair|Poor|Critical|  
    |---------------|----------|----------|----------|--------------|  
    |0 – 34%|35 – 59%|60 – 84%|85 – 99%|100% +|  
  
-   For merge replication, performance quality is independent of either threshold (the row processing threshold does determine if a value of **Performance critical** is displayed in the **Status** column). Performance quality is determined by comparing individual subscription performance to the average historical performance of subscriptions to the publication that have the same connection type (dial-up or LAN). Replication Monitor displays a value after five synchronizations occur with 50 or more changes each over the same type of connection. If there are fewer than five synchronizations with 50 or more changes or the most recent synchronization has fewer than 50 changes, Replication Monitor doesn't display a value.  
  
     The following table shows the correlation between the average performance and the performance quality value. For example, if ten Subscribers synchronize over a LAN connection with an average rate of 100 rows per second, and one of the subscriptions then synchronizes at a rate of 125 rows per second, the performance for that Subscriber's synchronization is 125% of the average, resulting in a value of Good.  
  
    |Excellent|Good|Fair|Poor|  
    |---------------|----------|----------|----------|  
    |151+%|76 – 150%|26 – 75%|0 – 25%|  
  
 For more information about viewing subscription information, see [View Information and Perform Tasks using Replication Monitor](../../../relational-databases/replication/monitor/view-information-and-perform-tasks-replication-monitor.md).  
  
## Determine latency with tracer tokens  
 Transactional replication allows you to measure the latency in a system by inserting a token (a small amount of data) in the transaction log of the publication database and recording how long it takes to arrive at the Distributor and Subscribers. The token also helps you identify if data isn't reaching the Distributor or Subscriber. For more information, see [Measure Latency and Validate Connections for Transactional Replication](../../../relational-databases/replication/monitor/measure-latency-and-validate-connections-for-transactional-replication.md).  
  
## View detailed synchronization performance for merge replication  
 For merge replication, Replication Monitor displays detailed statistics for each article processed during synchronization, including the amount of time spent in each processing phase (uploading changes, downloading changes, and so on). It can help you pinpoint specific tables that cause slowdowns. It's the best place to troubleshoot performance issues with merge subscriptions. For more information about viewing detailed statistics, see [View Information and Perform Tasks using Replication Monitor](../../../relational-databases/replication/monitor/view-information-and-perform-tasks-replication-monitor.md).  
  
## View transactions and delivery time for Transactional Replication  
 For Transactional Replication, Replication Monitor displays information about the number of transactions in the distribution database that it hasn't yet distributed to a Subscriber and the estimated time for distributing these transactions. For more information, see [View Information and Perform Tasks using Replication Monitor](../../../relational-databases/replication/monitor/view-information-and-perform-tasks-replication-monitor.md).  
  
## Related content

- [Monitoring (Replication)](monitoring-replication.md)
- [Set Thresholds and Warnings in Replication Monitor](set-thresholds-and-warnings-in-replication-monitor.md)
