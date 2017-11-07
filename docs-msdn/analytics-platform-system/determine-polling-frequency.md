---
title: "Determine Polling Frequency (Analytics Platform System)"
ms.custom: na
ms.date: 01/05/2017
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 062c0e3d-f7d0-44f1-aeab-a9bd17dc6fdd
caps.latest.revision: 7
author: BarbKess
---
# Determine Polling Frequency
This topic explains how to determine the polling frequency for SQL Server PDW appliance alerts.  
  
## To Determine the Polling Frequency  
Since PDW does not currently support proactive notifications when alerts occur, the monitoring solution needs to continuously poll the appliance DLLs.  Internally, PDW polls the components at different intervals:  
  
-   Cluster – 60 seconds  
  
-   Heartbeat – 60 seconds  
  
-   All other components – 5 minutes  
  
-   Performance counters – 3 seconds  
  
A common interval to poll for alerts, which is also used by System Center, is **every 15 minutes**.  Obviously, you could query more or less frequently, but it is not recommended to poll less than every 6 hours.  
  
Polling more frequently is acceptable, but polling too frequently can clutter the [sys.dm_pdw_nodes_exec_requests](http://msdn.microsoft.com/en-us/library/ms177648(v=sql11).aspx) DMV.  This can make it difficult for users to diagnose query performance issues if there query quickly rolls out of view.  
  
## See Also  
<!-- MISSING LINKS [Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  -->  
[Appliance Monitoring &#40;Analytics Platform System&#41;](appliance-monitoring.md)  
  
