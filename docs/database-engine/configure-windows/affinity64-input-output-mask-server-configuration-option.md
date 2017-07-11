---
title: "affinity64 Input-Output mask Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "processor affinity [SQL Server]"
  - "binding processors [SQL Server]"
  - "affinity64 I/O mask option"
ms.assetid: d304eae7-5116-40ee-a0fa-0a3c0bc20c01
caps.latest.revision: 18
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# affinity64 Input-Output mask Server Configuration Option
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **affinity64 I/O mask** binds [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] disk I/O to a specified subset of CPUs, similar to the **affinity I/O mask** option. Use **affinity I/O mask** to bind the first 32 processors, and use **affinity64 I/O mask** to bind the remaining processors on the computer. If you reconfigure the **affinity64 I/O mask**, you must restart the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This option is only visible on the 64-bit version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## See Also  
 [affinity Input-Output mask Server Configuration Option](../../database-engine/configure-windows/affinity-input-output-mask-server-configuration-option.md)   
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)   
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)   
 [RECONFIGURE &#40;Transact-SQL&#41;](../../t-sql/language-elements/reconfigure-transact-sql.md)  
  
  
