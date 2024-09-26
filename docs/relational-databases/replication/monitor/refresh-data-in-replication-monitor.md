---
title: "Refresh Data in Replication Monitor"
description: "Refresh Data in Replication Monitor"
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: how-to
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "refreshing data"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Refresh Data in Replication Monitor
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]
  In Replication Monitor, the main window and detail windows (those windows launched from the main window) can be refreshed automatically or manually. To refresh a window manually, press F5. By default, the main window is refreshed automatically every five seconds; the rate can be customized for each Publisher.  
  
 The data displayed in Replication Monitor is queried from a cache; for information about the relationship between the cache and refreshing Replication Monitor, see [Caching, Refresh, and Replication Monitor Performance](../../../relational-databases/replication/monitor/caching-refresh-and-replication-monitor-performance.md). For information about starting Replication Monitor, see [Start the Replication Monitor](../../../relational-databases/replication/monitor/start-the-replication-monitor.md).  
  
### To set refresh options for Replication Monitor
  
1.  Right-click a Publisher in the left pane of Replication Monitor, and then click **Publisher Settings**.  
  
2.  In the **Publisher Settings** dialog box, set the **Auto refresh** and **Refresh rate** options. The **Auto refresh** setting affects the main window in Replication Monitor. The **Refresh rate** setting also affects any detail windows that are set to refresh automatically (changes to the setting only affect the detail windows opened after the change).  
  
3.  Select **OK**.

### To specify that a detail window should automatically refresh  
  
1.  Open a detail window in Replication Monitor. For example:  
  
    1.  Expand a Publisher group in the left pane, expand a Publisher, and then click a publication.  
  
    2.  Click the **All Subscriptions** tab.  
  
    3.  Right-click a subscription, and then click **View Details**.  
  
2.  In the **Subscription \<SubscriptionName>** detail window, click **Action**, and then click **Auto Refresh**. The refresh rate is determined by the **Refresh rate** setting in the **Publisher Settings** dialog box.  
  
## Related content

- [Monitoring Replication](../../../relational-databases/replication/monitor/monitoring-replication.md)
