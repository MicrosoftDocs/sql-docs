---
title: "SQL Server Agent Properties (Job System Page) | Microsoft Docs"
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
  - "sql13.ag.agent.job.f1"
ms.assetid: e171d13e-1302-4f0e-88be-67d656aec8d3
caps.latest.revision: 4
author: "stevestein"
ms.author: "sstein"
manager: "jhubbard"
---
# SQL Server Agent Properties (Job System Page)
Use this page to view and modify how the [!INCLUDE[msCoName](../../includes/msconame_md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent Service manages jobs.  
  
## Options  
**Shutdown time-out interval (in seconds)**  
Specifies the number of seconds that [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent waits for jobs to complete before shutting down. If the job is still running after the interval specified, [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent forcefully stops the job.  
  
**Use a non-administrator proxy account**  
Sets a non-administrator proxy account for [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent. [!INCLUDE[msCoName](../../includes/msconame_md.md)] [!INCLUDE[ssKatmai](../../includes/sskatmai_md.md)] and later versions support multiple proxies, therefore this option is only applicable when managing [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent versions prior to [!INCLUDE[ssKatmai](../../includes/sskatmai_md.md)].  
  
**User name**  
Type the name of the user for the non-administrator proxy account. [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] supports multiple proxies, therefore this option is only applicable when managing [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent versions prior to [!INCLUDE[ssKatmai](../../includes/sskatmai_md.md)].  
  
**Password**  
Type the password of the user for the non-administrator proxy account. [!INCLUDE[ssVersion2005](../../includes/ssversion2005_md.md)] and later versions support multiple proxies, therefore this option is only applicable when managing [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent versions prior to [!INCLUDE[ssVersion2005](../../includes/ssversion2005_md.md)].  
  
**Domain**  
Type the domain of the user for the non-administrative proxy account. [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] supports multiple proxies, therefore this option is only applicable when managing [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent versions prior to [!INCLUDE[ssKatmai](../../includes/sskatmai_md.md)].  
  
## See Also  
[Implement Jobs](../../ssms/agent/implement-jobs.md)  
  
