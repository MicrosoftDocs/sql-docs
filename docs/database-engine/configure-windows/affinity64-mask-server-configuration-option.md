---
title: "affinity64 mask Server Configuration Option"
description: Find out about the affinity64 mask option. See when to use it in SQL Server to bind processors to specific threads.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/02/2017"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "processor affinity [SQL Server]"
  - "affinity64 mask option"
  - "binding processors [SQL Server]"
---
# affinity64 mask Server Configuration Option
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The affinity64 mask binds processors to specific threads, similar to the affinity mask option. Use affinity mask to bind the first 32 processors, and use affinity64 mask to bind the remaining processors on the computer. This option is only visible on the 64-bit version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!NOTE]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [ALTER SERVER CONFIGURATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-configuration-transact-sql.md) instead.  
  
## See Also  
 [affinity mask Server Configuration Option](../../database-engine/configure-windows/affinity-mask-server-configuration-option.md)   
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)   
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)   
 [RECONFIGURE &#40;Transact-SQL&#41;](../../t-sql/language-elements/reconfigure-transact-sql.md)  
  
  
