---
title: "MSSQLSERVER_18752 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "18752 (Database Engine error)"
ms.assetid: 234c58d8-7a1e-4b07-a64b-32a311527980
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_18752
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|18752|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|REPL_INUSE|  
|Message Text|Only one Log Reader Agent or log-related procedure (sp_repldone, sp_replcmds, and sp_replshowcmds) can connect to a database at a time. If you executed a log-related procedure, drop the connection over which the procedure was executed or execute sp_replflush over that connection before starting the Log Reader Agent or executing another log-related procedure.|  
  
## Explanation  
 Only one Log Reader agent or log-related procedure can connect to a database at a time.  
  
## User Action  
 Make sure no other logreader is running for the same publishing database, and no other active connection had previously run sp_replcmds/sp_repltrans/sp_repldone without running sp_replflush afterwards or disconnecting.  
  
  
