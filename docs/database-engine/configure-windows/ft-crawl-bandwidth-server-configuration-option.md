---
title: "ft crawl bandwidth Server Configuration Option"
description: "Learn about the ft crawl bandwidth option. See how it affects the number of buffers that SQL Server maintains in the pool of large memory buffers."
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/02/2017"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "large memory buffers"
  - "memory [SQL Server], buffers"
  - "ft crawl bandwidth option"
dev_langs:
  - "TSQL"
---
# ft crawl bandwidth Server Configuration Option
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Use the **ft crawl bandwidth** option to specify the size to which the pool of large memory buffers can grow. Large memory buffers are 4 megabytes (MB) in size. The **max** parameter value specifies the maximum number of buffers that the full-text memory manager should maintain in a large buffer pool. If the **max** value is zero, then there is no upper limit to the number of buffers that can be in a large buffer pool.  
  
 The **min** parameter specifies the minimum number of memory buffers that must be maintained in the pool of large memory buffers. Upon request from the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory manager, all extra buffer pools will be released but this minimum number of buffers will be maintained. If, however, the **min** value specified is zero, then all memory buffers are released.  
  
 Under certain circumstances, the number of buffers currently allocated is less than the value specified by the **min** parameter.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]  
  
## See Also  
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)   
 [ft notify bandwidth Server Configuration Option](../../database-engine/configure-windows/ft-notify-bandwidth-server-configuration-option.md)   
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)  
  
  
