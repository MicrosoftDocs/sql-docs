---
title: "MSSQLSERVER_1406 | Microsoft Docs"
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
  - "1406 (Database Engine error)"
ms.assetid: 915f97de-9b74-41f8-8bd5-b2d061416718
caps.latest.revision: 16
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_1406
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|1406|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBM_BADSTATEFORFORCESERVICE|  
|Message Text|Unable to force service safely. Remove database mirroring and recover database "%.*ls" to gain access.|  
  
## Explanation  
The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] cannot force service because the mirroring state cannot guarantee that the force service process would work correctly.  
  
## User Action  
Remove database mirroring. You can then recover the mirror database to gain access to it.  
  
## See Also  
[Force Service in a Database Mirroring Session &#40;Transact-SQL&#41;](../Topic/Force%20Service%20in%20a%20Database%20Mirroring%20Session%20(Transact-SQL).md)  
[Removing Database Mirroring &#40;SQL Server&#41;](../Topic/Removing%20Database%20Mirroring%20(SQL%20Server).md)  
  
