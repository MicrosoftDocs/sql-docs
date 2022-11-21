---
title: Determine polling frequency
description: This article explains how to determine the polling frequency for Analytics Platform System appliance alerts.
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 04/17/2018
ms.service: sql
ms.subservice: data-warehouse
ms.topic: conceptual
ms.custom: seo-dt-2019
---

# Determine Polling Frequency
This article explains how to determine the polling frequency for Analytics Platform System appliance alerts.  
  
## To Determine the Polling Frequency  
Since PDW does not currently support proactive notifications when alerts occur, the monitoring solution needs to continuously poll the appliance DLLs.  Internally, PDW polls the components at different intervals:  
  
-   Cluster - 60 seconds  
  
-   Heartbeat - 60 seconds  
  
-   All other components - five minutes  
  
-   Performance counters - three seconds  
  
A common interval to poll for alerts, which is also used by System Center, is **every 15 minutes**.  Obviously, you could query more or less frequently, but it is not recommended to poll less than every six hours.  
  
Polling more frequently is acceptable, but polling too frequently can clutter the [sys.dm_pdw_nodes_exec_requests](../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md) DMV.  Polling too frequently can make it difficult for users to diagnose query performance issues when their quickly rolls out of view.  
  
## See Also  
<!-- MISSING LINKS [Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  -->  
[Appliance Monitoring &#40;Analytics Platform System&#41;](appliance-monitoring.md)  
