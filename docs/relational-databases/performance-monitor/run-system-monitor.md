---
title: "Run Performance Monitor"
description: Performance Monitor in Windows uses remote procedure calls to collect information from SQL Server. 
ms.custom: ""
ms.date: "07/12/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Performance Monitor [SQL Server], running"
  - "Windows Performance Monitor [SQL Server], running"
  - "remote procedure calls [SQL Server]"
  - "starting Windows NT Performance Monitor"
  - "RPC"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Run Performance Monitor
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sql-windows-only.md)]

  Performance Monitor uses remote procedure calls (RPCs) to collect information from Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Any user who has Microsoft Windows permissions to run Performance Monitor can use Performance Monitor to monitor [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 As with all performance monitoring tools, expect some performance overhead when you use Performance Monitor to monitor [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The actual overhead in any specific instance depends on the hardware platform, the number of counters, and the selected update interval. However, the integration of Performance Monitor with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is designed to minimize any reduction in performance.  
  
> [!NOTE]  
>  If you have selected [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] performance counters to monitor in the Performance Monitor snap-in, you will see the counters even if [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is not running.  
  
 For information about starting Performance Monitor, see [Start Performance Monitor &#40;Windows&#41;](../../relational-databases/performance/start-system-monitor-windows.md).  
  
  
