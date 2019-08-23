---
title: "MSSQLSERVER_17884 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "17884 (Database Engine error)"
ms.assetid: 8d05ba05-3f71-4dc3-bd81-2ea5ac9fe843
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_17884
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|17884|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SRV_SCHEDULER_DEADLOCK|  
|Message Text|New queries assigned to process on Node %d have not been picked  up by a worker thread in the last %d seconds. Blocking or long-running queries can contribute to this condition, and may degrade client response time. Use the "max worker threads" configuration option to increase number  of allowable threads, or optimize current running queries.  SQL Process Utilization: %d%%. System Idle: %d%%.|  
  
## Explanation  
 There is no sign of progress in each of the schedulers and could be caused by deadlocks where none of the threads can advance and/or no new work can be picked up and processed. If process utilization is low then other processes on the machine may be causing the server process CPU starvation.  
  
## User Action  
 Determine why there is blocking and no progress being made and resolve situation accordingly. If process utilization is low check the load on the system caused by other processes.  
  
  
