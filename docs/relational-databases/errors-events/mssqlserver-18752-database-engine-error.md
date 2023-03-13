---
title: "MSSQLSERVER_18752"
description: "MSSQLSERVER_18752"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "18752 (Database Engine error)"
---
# MSSQLSERVER_18752
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
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
  
