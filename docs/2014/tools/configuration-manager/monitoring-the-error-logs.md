---
title: "Monitoring the Error Logs | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "logs [SQL Server]"
  - "database performance [SQL Server], errors"
  - "Windows application logs [SQL Server]"
  - "monitoring performance [SQL Server], errors"
  - "server performance [SQL Server], errors"
  - "comparing error and application log output"
  - "errors [SQL Server], logs"
  - "tuning databases [SQL Server], errors"
  - "database monitoring [SQL Server], errors"
  - "SQL Server error log"
  - "logs [SQL Server], SQL Server error logs"
  - "error logs [SQL Server]"
  - "logs [SQL Server], Windows application logs"
ms.assetid: e250336b-0695-44f6-a42f-23222f94e377
author: stevestein
ms.author: sstein
manager: craigg
---
# Monitoring the Error Logs
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logs certain system events and user-defined events to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log and the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows application log. Both logs automatically timestamp all recorded events. Use the information in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log to troubleshoot problems related to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 The Windows application log provides an overall picture of events that occur on the Windows operating system, as well as events in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent. Use the Windows Event Viewer to view the Windows application log and to filter the information. For example, you can filter events, such as information, warning, error, success audit, and failure audit.  
  
## Comparing Error and Application Log Output  
 You can use both the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log and the Windows application log to identify the cause of problems. For example, while monitoring the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log, you may encounter error messages that do not contain cause information. By comparing the dates and times for events between these logs, you can narrow the list of probable causes. The [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Log File Viewer lets you integrate [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent, and the Windows logs into a single list, making it easy to understand related server events and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] events. For more information, see the topic "Log File Viewer" in SQL Server Books Online.  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[Viewing the SQL Server Error Log](../../../2014/tools/configuration-manager/viewing-the-sql-server-error-log.md)|Contains information about the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log and how to view it.|  
|[Viewing the Windows Application Log](viewing-the-windows-application-log.md)|Contains information about the Windows application log and how to view it.|  
  
  
