---
title: "Publication Information, Tracer Tokens (SQL Server 2005 and Later) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.monitor.publicationinfo.tracertokens.f1"
ms.assetid: a115ba95-17ae-45df-91bd-5a1a35f3745f
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Publication Information, Tracer Tokens (SQL Server 2005 and Later)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The **Tracer Tokens** tab allows you to validate connections and to measure the latency of a system that uses transactional replication. A token (a small amount of data) is written to the transaction log of the publication database, marked as though it were a typical replicated transaction, and sent through the system, allowing a calculation of:  
  
-   How much time elapses between a transaction being committed at the Publisher and the corresponding command being inserted in the distribution database at the Distributor.  
  
-   How much time elapses between a command being inserted in the distribution database and the corresponding transaction being committed at a Subscriber.  
  
 From these calculations, you can answer a number of questions, including:  
  
-   Which Subscribers take the longest to receive a change from the Publisher?  
  
-   Of the Subscribers expected to receive the tracer token, which, if any, have not received it?  
  
## Options  
 To change the way that the grid displays data, right-click the grid, and then click one of the following options:  
  
-   **Sort**: Sort on one or more columns in the **Sort Columns** dialog box.  
  
-   **Choose Columns to Show**: Select which columns to display and the order in which to display them in the **Choose Columns** dialog box.  
  
-   **Filter**: Filter rows in the grid based on column values in the **Filter Settings** dialog box.  
  
-   **Clear Filter**: Clear any filter settings for the grid.  
  
 Filter settings are specific to each grid. Column selection and sorting are applied to all grids of the same type, such as the publications grid for each Publisher.  
  
 **Insert Tracer**  
 Click to insert a tracer token in the transaction log at the Publisher.  
  
 **Time inserted**  
 Select a time at which a tracer token was inserted to display latency information from that time. By default, information from the most recent time is displayed.  
  
> [!NOTE]  
>  Tracer token information is retained for the same time period as other historical data, which is governed by the history retention period of the distribution database. For information about changing distribution database properties, see [View and Modify Distributor and Publisher Properties](../../relational-databases/replication/view-and-modify-distributor-and-publisher-properties.md).  
  
 **Subscription**  
 The name of each subscription to the publication.  
  
 **Publisher to Distributor**  
 The elapsed time between a transaction being committed at the Publisher and the corresponding command being inserted in the distribution database at the Distributor. A value of **Pending** indicates that the token has not yet reached the Distributor. If the pending state persists, ensure that the Log Reader Agent is running.  
  
 **Distributor to Subscriber**  
 The elapsed time between a command being inserted in the distribution database and the corresponding transaction being committed at a Subscriber. A value of **Pending** indicates that the token has not yet reached the Subscriber. If the pending state persists, ensure that the Distribution Agent is running.  
  
 **Total Latency**  
 The elapsed time between a transaction being committed at the Publisher and the corresponding transaction being committed at the Subscriber. This represents the end-to-end latency of the replication system for this Subscriber at this time. A value of **Pending** indicates that the token has not yet reached the Subscriber.  
  
## See Also  
 [Start and Stop a Replication Agent &#40;SQL Server Management Studio&#41;](../../relational-databases/replication/agents/start-and-stop-a-replication-agent-sql-server-management-studio.md)   
 [Start the Replication Monitor](../../relational-databases/replication/monitor/start-the-replication-monitor.md)   
 [Measure Latency and Validate Connections for Transactional Replication](../../relational-databases/replication/monitor/measure-latency-and-validate-connections-for-transactional-replication.md)   
 [Monitor Performance with Replication Monitor](../../relational-databases/replication/monitor/monitor-performance-with-replication-monitor.md)   
 [Monitoring Replication](../../relational-databases/replication/monitor/monitoring-replication.md)   
 [Replication Agents Overview](../../relational-databases/replication/agents/replication-agents-overview.md)  
  
  
