---
title: "Publisher Information, Subscription Watch List (Merge Publication) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.monitor.publisherinfo.subscriptionssummary.merge.f1"
ms.assetid: 4ec956bf-5cef-4377-a1d1-8c7f0107a6cb
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Publisher Information, Subscription Watch List (Merge Publication)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The **Subscription Watch List** tab is available for Distributors running [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions; it is intended to display information on subscriptions from all publications available at the selected Publisher. You can filter the list of subscriptions to see errors, warnings, and any poorly performing subscriptions. This tab provides a single location for an administrator to monitor all replication activity at a Publisher: Replication Monitor displays all subscriptions that require attention, based on the selected replication type and on the option chosen in the **Show** drop-down list box. Because the items displayed on this tab are based on current status and performance, subscriptions are displayed on this page only if they match the option in the **Show** list box at the current time.  
  
## Options  
 For more detailed information and tasks related to a subscription, right-click the row for that subscription, and then click an option on the shortcut menu. To change the way that the grid displays data, right-click the grid, and then click one of the following options:  
  
-   **Sort**: Sort on one or more columns in the **Sort Columns** dialog box.  
  
-   **Choose Columns to Show**: Select which columns to display and the order in which to display them in the **Choose Columns** dialog box.  
  
-   **Filter**: Filter rows in the grid based on column values in the **Filter Settings** dialog box.  
  
-   **Clear Filter**: Clear any filter settings for the grid.  
  
 Filter settings are specific to each grid. Column selection and sorting are applied to all grids of the same type, such as the publications grid for each Publisher.  
  
 **Show Merge Subscriptions**  
 Select the type of subscriptions (transactional, snapshot, or merge) to display for the selected Publisher.  
  
 **Show**  
 Select the subscription states to display for the selected subscription type. For example, you can select to display only those subscriptions that have an error.  
  
 **Status**  
 The status of each subscription, which is determined by the status of the Merge Agent.  
  
 By default, the grid containing subscription information is sorted by the **Status** column (and then sorted by the **Performance** column for those subscriptions with the same status). The following list shows the possible status values and the sort order for the values (for example, errors are always shown at the top of the grid).  
  
-   Error  
  
-   Performance critical  
  
-   Long-running merge  
  
-   Expiring soon/Expired  
  
-   Uninitialized subscription  
  
-   Retrying failed command  
  
-   Synchronizing  
  
-   Not synchronizing  
  
 The sort order also determines which value is displayed if a given subscription is in more than one state. For example, if a subscription has an error and is expiring soon, the **Status** column displays **Error**.  
  
 The status values **Performance critical**, **Long-running merge**, **Expiring soon/Expired**, and **Uninitialized subscription** are warnings. When a warning is displayed, the **Status** column also displays if an agent is synchronizing. For example, the status could be **Synchronizing, Performance critical**.  
  
 The status values **Expiring soon/Expired** and **Long-running merge** can be displayed only if thresholds are set. The status value **Performance critical** can be displayed only after five synchronizations of subscriptions with the same connection type (dial-up or LAN). For information on performance measurements and setting thresholds, see [Monitor Performance with Replication Monitor](../../relational-databases/replication/monitor/monitor-performance-with-replication-monitor.md) and [Set Thresholds and Warnings in Replication Monitor](../../relational-databases/replication/monitor/set-thresholds-and-warnings-in-replication-monitor.md).  
  
 **Subscription**  
 The name of each subscription, in the form:*SubscriberName: SubscriptionDatabaseName*.  
  
 **Friendly Name**  
 The description of each subscription. The description is entered in the **Subscription Properties** dialog box or specified with the **@description** parameter of [sp_addmergesubscription](../../relational-databases/system-stored-procedures/sp-addmergesubscription-transact-sql.md) or [sp_addmergepullsubscription](../../relational-databases/system-stored-procedures/sp-addmergepullsubscription-transact-sql.md). Users often use the description as a "friendly name" or nickname for the subscription.  
  
 **Publication**  
 The name of the publication with which a subscription synchronizes, in the form: *PublicationDatabaseName: PublicationName*.  
  
 **Performance**  
 The performance rating for each subscription, based on the most recent measurements of delivery rate taken by Replication Monitor. The rating is determined by comparing individual subscription performance to the average historical performance of subscriptions to the publication that have the same connection type (dial-up or LAN). Replication Monitor displays a value after five synchronizations have occurred with 50 or more changes each over the same type of connection. If there have been less than five synchronizations with 50 or more changes or the most recent synchronization has less than 50 changes, this column is blank.  
  
> [!NOTE]  
>  Performance is based on the connection type displayed in the **Connection** column; therefore it is possible for a subscription with a lower delivery rate to display a better performance rating than another subscription if the first subscription is synchronized over a slower connection.  
  
 The performance rating is one of the following values:  
  
-   Excellent  
  
-   Good  
  
-   Fair  
  
-   Poor  
  
 For more information about how performance ratings are defined and how performance thresholds are set, see [Monitor Performance with Replication Monitor](../../relational-databases/replication/monitor/monitor-performance-with-replication-monitor.md).  
  
 **Delivery Rate**  
 The number of rows per second processed by the Merge Agent.  
  
 **Last Synchronization**  
 The time at which the Merge Agent last ran. Changes may or may not have been processed during this synchronization. If synchronization is in progress, a percentage complete value is displayed.  
  
 **Duration**  
 The amount of time the Merge Agent has run during the last synchronization. The time represents elapsed time if the Merge Agent is currently synchronizing and total time if the Merge Agent has synchronized previously.  
  
 **Connection**  
 The type of connection between the Subscriber and the Publisher. The possible values are **LAN**, **Dialup**, and **Internet**. The **Internet** value is displayed if the subscription uses Web synchronization.  
  
## See Also  
 [Start the Replication Monitor](../../relational-databases/replication/monitor/start-the-replication-monitor.md)   
 [View Information and Perform Tasks for a Publisher &#40;Replication Monitor&#41;](../../relational-databases/replication/monitor/view-information-and-perform-tasks-replication-monitor.md)   
 [Monitoring Replication](../../relational-databases/replication/monitor/monitoring-replication.md)   
 [Web Synchronization for Merge Replication](../../relational-databases/replication/web-synchronization-for-merge-replication.md)  
  
  
