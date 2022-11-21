---
description: "MSSQLSERVER_17883"
title: "MSSQLSERVER_17883 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "17883 (Database Engine error)"
ms.assetid: adaf1c04-e397-4a69-90b8-9353a37277ea
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_17883
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|17883|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SRV_SCHEDULER_NONYIELDING|  
|Message Text|Process %ld:%ld:%ld (0x%lx) Worker 0x%p appears to be non-yielding on Scheduler %ld. Thread creation time: %I64d. Approx Thread CPU Used: kernel %I64d ms, user %I64d ms. Process Utilization %d%%. System Idle %d%%. Interval: %I64d ms.|  
  
## Explanation  
Indicates that there is a possible problem with a thread not yielding on a scheduler.  This could be caused by a bug in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or if [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is not getting enough cycles to execute.  This error could go away if the thread eventually yields.  
  
## User Action  
If excessive load on system reduce load otherwise contact Microsoft Customer Support Services.  
  
