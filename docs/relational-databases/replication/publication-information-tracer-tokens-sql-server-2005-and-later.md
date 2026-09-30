---
title: "Tracer Tokens (Publication Information)"
description: A description of the 'Tracer Tokens' tab of the 'Publication Information' page found in Replication Monitor within SQL Server Management Studio (SSMS).
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: ui-reference
ms.custom:
  - updatefrequency5
f1_keywords:
  - "sql13.rep.monitor.publicationinfo.tracertokens.f1"
monikerRange: "=azuresqldb-mi-current || >=sql-server-2017"
---
# Publication information, tracer tokens (SQL Server 2005 and later)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  Use the **Tracer Tokens** tab to validate connections and measure the latency of a system that uses transactional replication. When you create a token (a small amount of data), the system writes it to the transaction log of the publication database, marks it as though it were a typical replicated transaction, and sends it through the system. This process makes it possible to calculate:  
  
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
  
 Filter settings are specific to each grid. Column selection and sorting applies to all grids of the same type, such as the publications grid for each Publisher.  
  
 **Insert Tracer**  
 Select to insert a tracer token in the transaction log at the Publisher.  
  
 **Time inserted**  
 Select a time when a tracer token was inserted to display latency information from that time. By default, the grid displays information from the most recent time.  
  
> [!NOTE]  
>  Tracer token information is retained for the same time period as other historical data, which the history retention period of the distribution database governs. For information about changing distribution database properties, see [View and Modify Distributor and Publisher Properties](../../relational-databases/replication/view-and-modify-distributor-and-publisher-properties.md).  
  
 **Subscription**  
 The name of each subscription to the publication.  
  
 **Publisher to Distributor**  
 The elapsed time between a transaction being committed at the Publisher and the corresponding command being inserted in the distribution database at the Distributor. A value of **Pending** indicates that the token hasn't reached the Distributor. If the pending state persists, ensure that the Log Reader Agent is running.  
  
 **Distributor to Subscriber**  
 The elapsed time between a command being inserted in the distribution database and the corresponding transaction being committed at a Subscriber. A value of **Pending** indicates that the token hasn't reached the Subscriber. If the pending state persists, ensure that the Distribution Agent is running.  
  
 **Total Latency**  
 The elapsed time between a transaction being committed at the Publisher and the corresponding transaction being committed at the Subscriber. This value represents the end-to-end latency of the replication system for this Subscriber at this time. A value of **Pending** indicates that the token hasn't reached the Subscriber.  
  
## Related content

- [Start and Stop a Replication Agent (SQL Server Management Studio)](../../relational-databases/replication/agents/start-and-stop-a-replication-agent-sql-server-management-studio.md)
- [Start the Replication Monitor](../../relational-databases/replication/monitor/start-the-replication-monitor.md)
- [Measure Latency and Validate Connections for Transactional Replication](../../relational-databases/replication/monitor/measure-latency-and-validate-connections-for-transactional-replication.md)
- [Monitor Performance with Replication Monitor](../../relational-databases/replication/monitor/monitor-performance-with-replication-monitor.md)
- [Monitoring Replication](../../relational-databases/replication/monitor/monitoring-replication.md)
- [Replication Agents Overview](../../relational-databases/replication/agents/replication-agents-overview.md)
