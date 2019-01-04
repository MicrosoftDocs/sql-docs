---
title: "affinity64 mask Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "processor affinity [SQL Server]"
  - "affinity64 mask option"
  - "binding processors [SQL Server]"
ms.assetid: 75ed08c7-f85c-4e15-9ee1-e7bc545d3293
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# affinity64 mask Server Configuration Option
  The affinity64 mask binds processors to specific threads, similar to the affinity mask option. Use affinity mask to bind the first 32 processors, and use affinity64 mask to bind the remaining processors on the computer. This option is only visible on the 64-bit version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!NOTE]  
>  [!INCLUDE[ssNoteDepNextAvoid](../../includes/ssnotedepnextavoid-md.md)] Use [ALTER SERVER CONFIGURATION &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-server-configuration-transact-sql) instead.  
  
## See Also  
 [affinity mask Server Configuration Option](affinity-mask-server-configuration-option.md)   
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)   
 [Server Configuration Options &#40;SQL Server&#41;](server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-configure-transact-sql)   
 [RECONFIGURE &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/reconfigure-transact-sql)  
  
  
