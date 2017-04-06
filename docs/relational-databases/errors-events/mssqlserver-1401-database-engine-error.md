---
title: "MSSQLSERVER_1401 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
helpviewer_keywords: 
  - "1401 (Database Engine error)"
ms.assetid: 02928770-aa63-4509-8713-406c73e4cedc
caps.latest.revision: 15
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_1401
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|1401|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBM_MASTERSTARTUP|  
|Message Text|Startup of the database-mirroring master thread routine failed for the following reason: %ls. Correct the cause of this error, and restart the SQL Server service.|  
  
## Explanation  
Startup of the mirroring control thread failed.  
  
## User Action  
In the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log, look for the associated error that preceded this message. Correct the cause of this error, and then restart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service (MSSQLSERVER).  
  
## See Also  
[Start, Stop, Pause, Resume, Restart the Database Engine, SQL Server Agent, or SQL Server Browser Service](~/database-engine/configure-windows/start-stop-pause-resume-restart-sql-server-services.md)  
  
