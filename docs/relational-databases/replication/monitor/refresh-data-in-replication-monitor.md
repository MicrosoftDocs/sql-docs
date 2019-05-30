---
title: "Refresh Data in Replication Monitor | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "refreshing data"
ms.assetid: e9582244-7d00-45f4-be16-020a65c76a5e
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Refresh Data in Replication Monitor
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  In Replication Monitor, the main window and detail windows (those windows launched from the main window) can be refreshed automatically or manually. To refresh a window manually, press F5. By default, the main window is refreshed automatically every five seconds; the rate can be customized for each Publisher.  
  
 The data displayed in Replication Monitor is queried from a cache; for information about the relationship between the cache and refreshing Replication Monitor, see [Caching, Refresh, and Replication Monitor Performance](../../../relational-databases/replication/monitor/caching-refresh-and-replication-monitor-performance.md). For information about starting Replication Monitor, see [Start the Replication Monitor](../../../relational-databases/replication/monitor/start-the-replication-monitor.md).  
  
### To set refresh options for Replication Monitor  
  
1.  Right-click a Publisher in the left pane of Replication Monitor, and then click **Publisher Settings**.  
  
2.  In the **Publisher Settings** dialog box, set the **Auto refresh** and **Refresh rate** options. The **Auto refresh** setting affects the main window in Replication Monitor. The **Refresh rate** setting also affects any detail windows that are set to refresh automatically (changes to the setting only affect the detail windows opened after the change).  
  
3.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
### To specify that a detail window should automatically refresh  
  
1.  Open a detail window in Replication Monitor. For example:  
  
    1.  Expand a Publisher group in the left pane, expand a Publisher, and then click a publication.  
  
    2.  Click the **All Subscriptions** tab.  
  
    3.  Right-click a subscription, and then click **View Details**.  
  
2.  In the **Subscription \<SubscriptionName>** detail window, click **Action**, and then click **Auto Refresh**. The refresh rate is determined by the **Refresh rate** setting in the **Publisher Settings** dialog box.  
  
## See Also  
 [Monitoring Replication](../../../relational-databases/replication/monitor/monitoring-replication.md)  
  
  
