---
title: "MSSQLSERVER_1458 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "1458 (Database Engine error)"
ms.assetid: adc78c59-a6f2-432b-9a07-fdd1dc2b9026
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_1458
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|1458|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBM_FAILREDO_ON_PRIMARY|  
|Message Text|The principal copy of the '%.*ls' database encountered error %d, status %d, severity %d. Database mirroring has been suspended. Try to resolve the error condition, and resume mirroring.|  
  
## Explanation  
 This messages indicates that the principal database encountered an error that caused database mirroring to be suspended.  
  
## User Action  
 Most cases of this error are self correcting. If the problem persists, restarting the database or server instance typically corrects the problem. For more information, look in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log on each partner for the associated error that preceded this message.  
  
## See Also  
 [Monitoring Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-sql-server.md)  
  
  
