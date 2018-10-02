---
title: "MSSQLSERVER_9001 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "9001 (Database Engine error)"
ms.assetid: a54de936-90c6-4845-aa96-29d32f154601
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_9001
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|9001|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|LOG_NOT_AVAIL|  
|Message Text|The log for database '%.*ls' is not available. Check the event log for related error messages. Resolve any errors and restart the database.|  
  
## Explanation  
 The database log was taken offline. Usually this signifies a catastrophic failure that requires the database to restart.  
  
## User Action  
 Diagnose other errors and restart the instance of SQL Server if it has not already restarted itself.  
  
  
