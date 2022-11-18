---
title: "Viewing the Windows Application Log"
description: Find out how to view and manage the Windows application log. You can configure SQL Server to write event information to this log.
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: tools-other
ms.topic: conceptual
helpviewer_keywords: 
  - "application logs [SQL Server]"
  - "Windows application logs [SQL Server]"
  - "viewing Windows application logs"
  - "errors [SQL Server], logs"
  - "system logs [SQL Server]"
  - "security logs [SQL Server]"
  - "displaying Windows application logs"
  - "logs [SQL Server], Windows application logs"
ms.assetid: f9853b74-7db7-47cc-b957-e49ed5bc0a1a
author: markingmyname
ms.author: maghan
monikerRange: ">=sql-server-2016"
---
# Viewing the Windows Application Log
[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]
  When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is configured to use the Microsoft Windows application log, each [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] session writes new events to that log. Unlike the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log, a new application log is not created each time you start an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 View and manage the Windows application log by using Windows Event Viewer or the Log Viewer in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 There are three logs that can be viewed with Event Viewer.  
  
|Windows log type|Description|  
|----------------------|-----------------|  
|System log|Records events logged by the Windows operating system components. For example, the failure of a driver or other system component to load during startup is recorded in the system log.|  
|Security log|Records security events, such as failed login attempts. This helps track changes to the security system and identify possible breaches to security. For example, attempts to log on to the system may be recorded in the security log, depending on the audit settings in the User Manager.<br /><br /> Only members of the **sysadmin** fixed server role can view the security log.|  
|Application log|Records events that are logged by applications. For example, a database application might record a file error in the application log.|  
  
 For more information about using Event Viewer, managing the application log, and understanding the information it presents, see the Windows documentation.  
  
 **To view the Windows application log**  
  
 [View the Windows Application Log &#40;Windows&#41;](../../relational-databases/performance/view-the-windows-application-log-windows-10.md)  
  
  
