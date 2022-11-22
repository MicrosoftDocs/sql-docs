---
title: "Monitor CPU Usage"
description: Monitor a SQL Server instance to determine if CPU usage rates are in a normal range. Use System Monitor to see how much time the CPU spends running a thread.
ms.custom: ""
ms.date: "07/12/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "monitoring performance [SQL Server], CPU usage"
  - "tuning databases [SQL Server], CPU usage"
  - "processors [SQL Server], monitoring usage"
  - "database performance [SQL Server], CPU usage"
  - "monitoring CPU usage [SQL Server]"
  - "server performance [SQL Server], CPU usage"
  - "database monitoring [SQL Server], CPU usage"
  - "monitoring [SQL Server], CPU usage"
  - "processors [SQL Server]"
  - "CPU [SQL Server], monitoring"
  - "monitoring server performance [SQL Server], CPU usage"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Monitor CPU Usage
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Monitor an instance of Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] periodically to determine whether CPU usage rates are within normal ranges. A continually high rate of CPU usage may indicate the need to upgrade the CPU or add multiple processors. Alternatively, a high CPU usage rate may indicate a poorly tuned or designed application. Optimizing the application can lower CPU utilization.  
  
 An efficient way to determine CPU usage is to use the **Processor:% Processor Time** counter in System Monitor. This counter monitors the amount of time the CPU spends executing a thread that is not idle. A consistent state of 80 percent to 90 percent may indicate the need to upgrade your CPU or add more processors. For multiprocessor systems, monitor a separate instance of this counter for each processor. This value represents the sum of processor time on a specific processor. To determine the average for all processors, use the **System: %Total Processor Time** counter instead.  
  
 Optionally, you can also monitor the following counters to monitor processor usage:  
  
-   **Processor: % Privileged Time**  
  
     Corresponds to the percentage of time the processor spends on execution of Microsoft Windows kernel commands, such as processing of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] I/O requests. If this counter is consistently high when the **Physical Disk** counters are high, consider installing a faster or more efficient disk subsystem.  
  
    > [!NOTE]  
    >  Different disk controllers and drivers use different amounts of kernel processing time. Efficient controllers and drivers use less privileged time, leaving more processing time available for user applications, increasing overall throughput.  
  
-   **Processor: %User Time**  
  
     Corresponds to the percentage of time that the processor spends on executing user processes such as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   **System: Processor Queue Length**  
  
     Corresponds to the number of threads waiting for processor time. A processor bottleneck develops when threads of a process require more processor cycles than are available. If more than a few processes attempt to utilize the processor's time, you might need to install a faster processor. Or, if you have a multiprocessor system, you could add a processor.  
  
 When you examine processor usage, consider the type of work that the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] performs. If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] performs many calculations, such as queries involving aggregates or memory-bound queries that require no disk I/O, 100 percent of the processor's time can be used. If this causes the performance of other applications to suffer, try changing the workload. For example, dedicate the computer to running the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 Usage rates around 100 percent, where many client requests are being processed, may indicate that processes are queuing up, waiting for processor time, and causing a bottleneck. Resolve the problem by adding faster processors.  
  
  
