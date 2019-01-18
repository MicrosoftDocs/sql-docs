---
title: "MSSQLSERVER_9955 | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "9955 (Database Engine error)"
ms.assetid: 77f30570-7790-4747-b372-eac71c036e19
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_9955
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|9955|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|FTXT2_MSSEARCHACCESSDENY|  
|Message Text|SQL Server failed to create named pipe '%ls' to communicate with the full-text filter daemon (Windows error: %d). Either a named pipe already exists for a filter daemon host process, the system is low on resources, or the security identification number (SID) lookup for the filter daemon account group failed. To resolve this error, terminate any running full-text filter daemon processes, and if necessary reconfigure the full-text daemon launcher service account.|  
  
## Explanation  
 This message occurs because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failed to create a named pipe to communicate with the full-text filter daemon. Either a named pipe already exists for a filter daemon host process, the system is low on resources, or the security identification number (SID) lookup for the filter daemon account group failed.  
  
## User Action  
 To resolve this error, terminate any running full-text filter daemon processes, and if necessary reconfigure the full-text daemon host account by using the SQL Server Configuration Manager.  
  
## See Also  
 [SQL Server Configuration Manager](../sql-server-configuration-manager.md)   
 [Set the Service Account for the Full-text Filter Daemon Launcher](../search/set-the-service-account-for-the-full-text-filter-daemon-launcher.md)   
 [Full-Text Search](../search/full-text-search.md)  
  
  
