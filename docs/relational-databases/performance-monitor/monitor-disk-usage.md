---
title: "Monitor Disk Usage"
description: Monitoring disk activity for SQL Server involves monitoring disk I/O and detecting excess paging, and isolating disk activity that SQL Server creates.
ms.custom: ""
ms.date: 07/12/2021
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "database monitoring [SQL Server], disk usage"
  - "disks [SQL Server]"
  - "monitoring performance [SQL Server], disk usage"
  - "server performance [SQL Server], disk usage"
  - "monitoring [SQL Server], disk activity"
  - "excess paging [SQL Server]"
  - "tuning databases [SQL Server], disk usage"
  - "I/O [SQL Server], monitoring"
  - "disks [SQL Server], monitoring activity"
  - "isolating disk activity [SQL Server]"
  - "database performance [SQL Server], disk usage"
  - "monitoring server performance [SQL Server], disk usage"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Monitor Disk Usage
  [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses Microsoft Windows operating system input/output (I/O) calls to perform read and write operations on your disk. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] manages when and how disk I/O is performed, but the Windows operating system performs the underlying I/O operations. The I/O subsystem includes the system bus, disk controller cards, disks, tape drives, CD-ROM drive, and many other I/O devices. Disk I/O is frequently the cause of bottlenecks in a system.  
  
 Monitoring disk activity involves two areas of focus:  
  
-   Monitoring Disk I/O and Detecting Excess Paging  
  
-   Isolating Disk Activity That [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Creates  
  
 For more information, see [Monitoring Disk Usage](https://social.technet.microsoft.com/wiki/contents/articles/monitoring-disk-usage.aspx). 
 
 For more information on how to troubleshoot I/O issues in SQL Server, see [Slow I/O - SQL Server and disk I/O performance](https://techcommunity.microsoft.com/t5/SQL-Server-Support/Slow-I-O-SQL-Server-and-disk-I-O-performance/ba-p/333983).
  
  
