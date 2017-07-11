---
title: "Log File Viewer | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Log File Viewer"
ms.assetid: a4ea7fc8-1cb2-4c98-bc86-8991c5e748b2
caps.latest.revision: 26
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Log File Viewer
  Log File Viewer in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] is used to access information about errors and events that are captured in log files.  
  
## Benefits of using Log File Viewer  
 You can view [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] log files from a local or remote instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] when the target instance is offline or cannot start. You can access the offline log files from Registered Servers, or programmatically through WMI and WQL (WMI Query Language) queries. For more information, see [View Offline Log Files](../../relational-databases/logs/view-offline-log-files.md). Following are the types of log files you can access using Log File Viewer:  
  
-   Audit Collection  
  
-   Data Collection  
  
-   Database Mail  
  
-   Job History  
  
-   Maintenance Plans  
  
-   Remote Maintenance Plans  
  
-   SQL Server  
  
-   SQL Server Agent  
  
-   Windows NT (These are Windows events that can also be accessed from Event Viewer.)  
  
## Log File Viewer Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Describes how to open Log File Viewer depending on the information that you want to view.|[Open Log File Viewer](../../relational-databases/logs/open-log-file-viewer.md)|  
|Describes how to view offline log files through registered servers and how to verify WMI permissions.|[View Offline Log Files](../../relational-databases/logs/view-offline-log-files.md)|  
|Provides Log File Viewer F1 Help.|[Log File Viewer F1 Help](../../relational-databases/logs/log-file-viewer-f1-help.md)|  
  
## See Also  
 [SQL Server Audit &#40;Database Engine&#41;](../../relational-databases/security/auditing/sql-server-audit-database-engine.md)   
 [SQL Server Agent Error Log](http://msdn.microsoft.com/library/0b2d6e6e-cd2d-4b8b-9fa2-2bbd2fc0da41)  
  
  