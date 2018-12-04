---
title: Determine polling frequency - Analytics Platform System | Microsoft Docs
description: This article explains how to determine the polling frequency for Analytics Platform System appliance alerts.
author: mzaman1 
manager: craigg
ms.prod: sql
ms.technology: data-warehouse
ms.topic: conceptual
ms.date: 04/17/2018
ms.author: murshedz
ms.reviewer: martinle
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
  
Polling more frequently is acceptable, but polling too frequently can clutter the [sys.dm_pdw_nodes_exec_requests](https://msdn.microsoft.com/library/ms177648(v=sql11).aspx) DMV.  Polling too frequently can make it difficult for users to diagnose query performance issues when their quickly rolls out of view.  
  
## See Also  
<!-- MISSING LINKS [Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  -->  
[Appliance Monitoring &#40;Analytics Platform System&#41;](appliance-monitoring.md)  
  
