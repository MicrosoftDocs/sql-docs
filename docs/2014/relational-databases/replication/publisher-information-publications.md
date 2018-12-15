---
title: "SQL Server Replication 'Publisher Information' dialog box | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql12.rep.monitor.publisherinfo.publications.f1"
ms.assetid: 0b2e3d4e-03b7-4c31-8f96-48648d750010
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# SQL Server Replication 'Publisher Information' dialog box
  The **Publications** tab provides summary information for all publications at the Publisher selected in the left pane.  
  
## Options  
 To change the way that the grid displays data, right-click the grid, and then click one of the following options:  
  
-   **Sort**: Sort on one or more columns in the **Sort Columns** dialog box.  
  
-   **Choose Columns to Show**: Select which columns to display and the order in which to display them in the **Choose Columns** dialog box.  
  
-   **Filter**: Filter rows in the grid based on column values in the **Filter Settings** dialog box.  
  
-   **Clear Filter**: Clear any filter settings for the grid.  
  
 Filter settings are specific to each grid. Column selection and sorting are applied to all grids of the same type, such as the publications grid for each Publisher.  
  
 **Status**  
 The status of each publication, which is determined by the highest priority status of its subscriptions. By default, the grid containing publication information is sorted by the **Status** column. The following list shows the possible status values and the sort order for the values (for example, errors are always shown at the top of the grid):  
  
-   Error  
  
-   Performance critical  
  
-   Retrying failed command  
  
-   OK  
  
 The status value **Performance critical** is relevant for transactional subscriptions and merge subscriptions; for transactional subscriptions, it can be displayed only if a threshold is set. For information on performance measurements and setting thresholds, see [Monitor Performance with Replication Monitor](monitor/monitor-performance-with-replication-monitor.md) and [Set Thresholds and Warnings in Replication Monitor](monitor/set-thresholds-and-warnings-in-replication-monitor.md).  
  
 **Publication**  
 The name of each publication, in the form *PublicationDatabaseName: PublicationName*.  
  
 **Subscriptions**  
 The number of subscriptions for each publication.  
  
 **Synchronizing**  
 The number of subscriptions for each publication that are currently synchronizing:  
  
-   For transactional replication, "synchronizing" means that the Distribution Agent is running, but data is not necessarily being replicated.  
  
-   For merge replication, "synchronizing" means that the Merge Agent is running and that data is currently being replicated.  
  
-   For snapshot replication, "synchronizing" means that the Distribution Agent is running and that data is currently being replicated.  
  
 **Current Average Performance** and **Current Worst Performance**  
 [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions only. The average performance rating and the worst performance rating, respectively, for all subscriptions to a publication. Ratings are based on the most recent measurements taken by Replication Monitor and do not reflect the performance of a subscription over time.  
  
 For transactional replication, Replication Monitor displays a value only for publications that have performance thresholds defined. If performance thresholds are not defined for a publication, this column displays **Not enabled**. For merge replication, Replication Monitor displays a value after five synchronizations have occurred with 50 or more changes each over the same type of connection (dial-up or LAN). If there have been less than five synchronizations with 50 or more changes or the most recent synchronization has less than 50 changes, this column is blank.  
  
 The performance rating is one of the following values:  
  
-   Excellent  
  
-   Good  
  
-   Fair  
  
-   Poor  
  
-   Critical  
  
 For more information about how performance ratings are defined and how performance thresholds are set, see [Monitor Performance with Replication Monitor](monitor/monitor-performance-with-replication-monitor.md).  
  
## See Also  
 [Start the Replication Monitor](monitor/start-the-replication-monitor.md)   
 [View Information and Perform Tasks using Replication Monitor](monitor/view-information-and-perform-tasks-replication-monitor.md)   
 [Monitoring Replication](monitoring-replication.md)  
  
  
