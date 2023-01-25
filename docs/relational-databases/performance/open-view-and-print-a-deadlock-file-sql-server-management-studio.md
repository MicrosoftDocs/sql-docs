---
title: "Open, view, print deadlock file (SSMS)"
description: Learn how to capture deadlock information that SQL Server Profiler generates and view it in SQL Server Management Studio.
ms.custom: seo-dt-2019
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "deadlocks [SQL Server], printing files"
  - "deadlocks [SQL Server], opening files"
  - "opening deadlock files"
  - "printing deadlock files"
ms.assetid: 5061b13f-2cb7-457a-b8d0-fbd437b510ab
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Open, view, and print a deadlock file in SQL Server Management Studio (SSMS)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  When [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] generates a deadlock, you can capture and save the deadlock information to a file. After you've saved the deadlock file, you can open it in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to view it or print it.  
  
## Open and view a deadlock file  
  
1. On the **File** menu in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], point to **Open**, and then select **File**.  
  
2. In the **Open File** dialog box, select the .xdl file type in the **Files of type** box. You now have a filtered list of only deadlock files.  
  
## Print a deadlock file  
  
1. On the **File** menu in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], point to **Open**, and then select **File**.  
  
2. In the **Open File** dialog box, select the .xdl file type in the **Files of type** box. You now have a filtered list of only deadlock files.  
  
3. Select the deadlock file you want to print, and select **Open**.  
  
4. On the **File** menu, select **Print**.  
  
## See also  
 [Save deadlock graphs &#40;SQL Server Profiler&#41;](../../relational-databases/performance/save-deadlock-graphs-sql-server-profiler.md)  
  
  
