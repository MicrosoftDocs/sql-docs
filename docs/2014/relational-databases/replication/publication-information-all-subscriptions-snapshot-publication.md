---
title: "Publication Information, All Subscriptions (Snapshot Publication) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql12.rep.monitor.publicationinfo.allsubscriptions.snapshot.f1"
ms.assetid: 7ce656c2-6e60-4625-8955-1daff641070c
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Publication Information, All Subscriptions (Snapshot Publication)
  The **All Subscriptions** tab displays information on all subscriptions to the selected snapshot publication.  
  
## Options  
 For more detailed information and tasks related to a subscription, right-click the row for that subscription, and then click an option on the shortcut menu. To change the way that the grid displays data, right-click the grid, and then click one of the following options:  
  
-   **Sort**: Sort on one or more columns in the **Sort Columns** dialog box.  
  
-   **Choose Columns to Show**: Select which columns to display and the order in which to display them in the **Choose Columns** dialog box.  
  
-   **Filter**: Filter rows in the grid based on column values in the **Filter Settings** dialog box.  
  
-   **Clear Filter**: Clear any filter settings for the grid.  
  
 Filter settings are specific to each grid. Column selection and sorting are applied to all grids of the same type, such as the publications grid for each Publisher.  
  
 **Show**  
 [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions only. Select the subscription states to display for the selected subscription type. For example, you can select to display only those subscriptions that have an error.  
  
 **Status**  
 The status of each subscription, which is determined by the status of the Snapshot Agent or the Distribution Agent (the higher priority status is displayed).  
  
 By default, the grid containing subscription information is sorted by the **Status** column. The following list shows the possible status values and the sort order for the values (for example, errors are always shown at the top of the grid).  
  
-   Error  
  
-   Expiring soon/Expired ([!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions only)  
  
-   Uninitialized subscription ([!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions only)  
  
-   Retrying failed command  
  
-   Synchronizing  
  
-   Not synchronizing  
  
 The sort order also determines which value is displayed if a given subscription is in more than one state. For example, if a subscription has an error and is expiring soon, the **Status** column displays **Error**.  
  
 The status values **Expiring soon/Expired** and **Uninitialized subscription** are warnings. When a warning is displayed, the **Status** column also displays if an agent is running. For example, the status could be **Running, Expiring soon/Expired**.  
  
 The status value **Expiring soon/Expired** is displayed only if a threshold is set. For information on setting thresholds, see [Set Thresholds and Warnings in Replication Monitor](monitor/set-thresholds-and-warnings-in-replication-monitor.md).  
  
 **Subscription**  
 The name of each subscription, in the form: *SubscriberName: SubscriptionDatabaseName*.  
  
 **Last Synchronization**  
 The time at which the Distribution Agent last ran. If synchronization is in progress, **In progress** is displayed.  
  
## See Also  
 [Start the Replication Monitor](monitor/start-the-replication-monitor.md)   
 [View Information and Perform Tasks using Replication Monitor](monitor/view-information-and-perform-tasks-replication-monitor.md)   
 [Monitoring Replication](monitoring-replication.md)  
  
  
