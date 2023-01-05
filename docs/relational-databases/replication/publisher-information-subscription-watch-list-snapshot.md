---
title: "Subscription Watch List (Replication Monitor - Snapshot)"
description: Describes the 'Subscription Watch List' tab of Replication Monitor for a Snapshot Publication in SQL Server Management Studio (SSMS).
ms.custom: seo-lt-2019
ms.date: "03/07/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.monitor.publisherinfo.subscriptionssummary.snapshot.f1"
ms.assetid: 2ebeee62-7f54-4c77-9d37-15708bc5cc23
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Publisher Information, Subscription Watch List (Snapshot)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  The **Subscription Watch List** tab is available for Distributors running [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions; it is intended to display information on subscriptions from all publications available at the selected Publisher. You can filter the list of subscriptions to see errors, warnings, and any poorly performing subscriptions. This tab provides a single location for an administrator to monitor all replication activity at a Publisher: Replication Monitor displays all subscriptions that require attention, based on the selected replication type and on the option chosen in the **Show** drop-down list box. Because the items displayed on this tab are based on current status and performance, subscriptions are displayed on this page only if they match the option in the **Show** list box at the current time.  
  
## Options  
 For more detailed information and tasks related to a subscription, right-click the row for that subscription, and then click an option on the shortcut menu. To change the way that the grid displays data, right-click the grid, and then click one of the following options:  
  
-   **Sort**: Sort on one or more columns in the **Sort Columns** dialog box.  
  
-   **Choose Columns to Show**: Select which columns to display and the order in which to display them in the **Choose Columns** dialog box.  
  
-   **Filter**: Filter rows in the grid based on column values in the **Filter Settings** dialog box.  
  
-   **Clear Filter**: Clear any filter settings for the grid.  
  
 Filter settings are specific to each grid. Column selection and sorting are applied to all grids of the same type, such as the publications grid for each Publisher.  
  
 **Show Snapshot Subscriptions**  
 Select the type of subscriptions (transactional, snapshot, or merge) to display for the selected Publisher.  
  
 **Show**  
 Select the subscription states to display for the selected subscription type. For example, you can select to display only those subscriptions that have an error.  
  
 **Status**  
 The status of each subscription, which is determined by the status of the Snapshot Agent or the Distribution Agent (the higher priority status is displayed).  
  
 By default, the grid containing subscription information is sorted by the **Status** column. The following list shows the possible status values and the sort order for the values (for example, errors are always shown at the top of the grid).  
  
-   Error  
  
-   Expiring soon/Expired  
  
-   Uninitialized subscription  
  
-   Retrying failed command  
  
-   Synchronizing  
  
-   Not synchronizing  
  
 The sort order also determines which value is displayed if a given subscription is in more than one state. For example, if a subscription has an error and is expiring soon, the **Status** column displays **Error**.  
  
 The status values **Expiring soon/Expired** and **Uninitialized subscription** are warnings. When a warning is displayed, the **Status** column also displays if an agent is running. For example, the status could be **Running, Expiring soon/Expired**.  
  
 The status value **Expiring soon/Expired** is displayed only if a threshold is set. For information on setting thresholds, see [Set Thresholds and Warnings in Replication Monitor](../../relational-databases/replication/monitor/set-thresholds-and-warnings-in-replication-monitor.md).  
  
 **Subscription**  
 The name of each subscription, in the form: *SubscriberName: SubscriptionDatabaseName*.  
  
 **Publication**  
 The name of the publication with which a subscription synchronizes, in the form: *PublicationDatabaseName: PublicationName*.  
  
 **Last Synchronization**  
 The time at which the Distribution Agent last ran. If synchronization is in progress, **In progress** is displayed.  
  
## See Also  
 [Start the Replication Monitor](../../relational-databases/replication/monitor/start-the-replication-monitor.md)   
 [View Information and Perform Tasks for a Publisher &#40;Replication Monitor&#41;](../../relational-databases/replication/monitor/view-information-and-perform-tasks-replication-monitor.md)   
 [Monitoring Replication](../../relational-databases/replication/monitor/monitoring-replication.md)  
  
  
