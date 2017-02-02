---
title: "Recycle SQL Server Agent Error Logs | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "tools-ssms"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.ag.errorlog.recyclesqlagenterrorlogs.f1"
ms.assetid: 10bc2dd1-0505-4527-8ec7-d3b4e5d6352b
caps.latest.revision: 3
author: "stevestein"
ms.author: "sstein"
manager: "jhubbard"
---
# Recycle SQL Server Agent Error Logs
Use this page to recycle the [!INCLUDE[msCoName](../../includes/msconame_md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent Error Logs. Recycling the log closes the current [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent error log and starts a new error log without restarting the [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent service. Notice that [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent maintains the nine most recent error logs. If there are already nine error logs present, recycling the error log causes [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent to remove the oldest error log.  
  
## See Also  
[SQL Server Agent Error Log](../../ssms/agent/sql-server-agent-error-log.md)  
  
