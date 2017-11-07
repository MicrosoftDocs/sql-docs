---
title: "Security Audit Event Category | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "Security Audit event category [SQL Server]"
  - "event classes [Analysis Services], security audit"
  - "security events [Analysis Services]"
ms.assetid: 9686a495-68d7-4137-8e30-2655aa519f6c
caps.latest.revision: 25
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Security Audit Event Category
  The Security Audit event category has the event classes described in the following table.  
  
|Event Class|Event Id|Description|  
|-----------------|--------------|-----------------|  
|Audit Login|1|Records all new connection events since the trace started, such as when a client requested a connection to a server running an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].|  
|Audit Logout|2|Records all new disconnect events since the trace started, such as when a client issues a disconnect command.|  
|Audit Server Starts and Stops|4|Records shutdown, start, and pause activities for services.|  
|Audit Object Permission Event|18|Records all object permission changes.|  
|Audit Admin Operations Event|19|Records server operations for backup, restore, synchronize, attach, detach, image load, and image save.|  
  
 For information about the columns associated with each of the Security Audit event classes, see [Security Audit Data Columns](../../analysis-services/trace-events/security-audit-data-columns.md).  
  
## See Also  
 [Analysis Services Trace Events](../../analysis-services/trace-events/analysis-services-trace-events.md)  
  
  