---
title: "MSSQLSERVER_17887 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "17887 (Database Engine error)"
ms.assetid: ad0806e6-3296-4c32-b103-fccf0f8a8d3d
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_17887
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|17887|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SRV_IO_COMP_LISTENER_NONYIELDING|  
|Message Text|IO Completion Listener (0x%lx) Worker 0x%p appears to be non-yielding on Node %ld. Approx CPU Used: kernel %I64d ms, user %I64d ms, Interval: %I64d.|  
  
## Explanation  
 Indicates that there is a possible problem with the I/O Completion Port Listener on the specified node when executing the I/O Completion routine for a network read/write event. This error will go away when the I/O Completion Port Listener returns from executing the I/O Completion routine.  
  
## User Action  
 Contact Microsoft Customer Support Services (CSS).  
  
  
