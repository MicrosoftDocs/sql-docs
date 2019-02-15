---
title: "Run System Monitor | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "System Monitor [SQL Server], running"
  - "Windows System Monitor [SQL Server], running"
  - "remote procedure calls [SQL Server]"
  - "starting Windows NT System Monitor"
  - "RPC"
ms.assetid: 05297984-bc8d-43df-829c-032387f7ea61
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Run System Monitor
  System Monitor uses remote procedure calls (RPCs) to collect information from Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Any user who has Microsoft Windows permissions to run System Monitor can use System Monitor to monitor [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!NOTE]  
>  When using System Monitor or Performance Monitor, you cannot connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is running on Windows 98.  
  
 As with all performance monitoring tools, expect some performance overhead when you use System Monitor to monitor [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The actual overhead in any specific instance depends on the hardware platform, the number of counters, and the selected update interval. However, the integration of System Monitor with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is designed to minimize any reduction in performance.  
  
> [!NOTE]  
>  If you have selected [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] performance counters to monitor in the System Monitor snap-in, you will see the counters even if [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is not running.  
  
 For information about starting System Monitor, see [Start System Monitor &#40;Windows&#41;](../performance/start-system-monitor-windows.md).  
  
  
