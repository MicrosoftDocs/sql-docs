---
title: "MSSQLSERVER_8443 | Microsoft Docs"
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
  - "8443 (Database Engine error)"
ms.assetid: a3541b9c-b1a8-4280-add1-275f08696b62
caps.latest.revision: 16
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_8443
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|8443|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SB_DIALOG_WO_CONV_GROUP|  
|Message Text|The conversation with ID '%.*ls' and initiator %d references a missing conversation group '%.\*ls'. Run DBCC CHECKDB to analyze and repair the database.|  
  
## Explanation  
The metadata layer returned NULL for the conversation group. The database has been corrupted in some way. One possible source of corruption is a disk error.  
  
## User Action  
Run DBCC CHECKDB in repair mode to bring the database back into a consistent state. It may delete messages if necessary to restore consistency. Investigate system error logs to see if this error was caused by another failure in the system.  
  
