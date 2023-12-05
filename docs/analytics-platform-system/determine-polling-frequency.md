---
title: Determine polling frequency
description: This article explains how to determine the polling frequency for Analytics Platform System appliance alerts.
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 12/04/2023
ms.service: sql
ms.subservice: data-warehouse
ms.topic: conceptual
---

# Determine polling frequency
This article explains how to determine the polling frequency for Analytics Platform System appliance alerts.  
  
## <a id="to-determine-the-polling-frequency"></a> Determine the Polling Frequency
Since PDW doesn't currently support proactive notifications when alerts occur, the monitoring solution needs to continuously poll the appliance DLLs.  Internally, PDW polls the components at different intervals:  
  
-   Cluster - 60 seconds  
  
-   Heartbeat - 60 seconds  
  
-   All other components - five minutes  
  
-   Performance counters - three seconds  
  
A common interval to poll for alerts, which is also used by System Center Operations Manager, is **every 15 minutes**.  Obviously, you could query more or less frequently, but it isn't recommended to poll less than every six hours.  
  
Polling more frequently is acceptable, but polling too frequently can clutter the [sys.dm_pdw_nodes_exec_requests](../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md) DMV. Polling too frequently can make it difficult for users to diagnose query performance issues when they quickly roll out of view.  
  
## Related content

- [Monitor the appliance with system views - Analytics Platform System](monitor-the-appliance-by-using-system-views.md)
- [Appliance monitoring for Analytics Platform System](appliance-monitoring.md)