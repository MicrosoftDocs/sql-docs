---
title: "ft notify bandwidth Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ft notify bandwidth opion"
  - "small memory buffers"
  - "memory [SQL Server], buffers"
ms.assetid: 9ca284c5-f3e0-4a67-a132-fff376ff0ffe
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# ft notify bandwidth Server Configuration Option
  Use the **ft notify bandwidth** option to specify the size to which the pool of small memory buffers can grow. Small memory buffers are 64 kilobytes (KB) in size. The *max* parameter value specifies the maximum number of buffers that the full-text memory manager should maintain in a small buffer pool. If the `max` value is zero, then there is no upper limit to the number of buffers that can be in a small buffer pool.  
  
 The **min** parameter specifies the minimum number of memory buffers that must be maintained in the pool of small memory buffers. Upon request from the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory manager, all extra buffer pools will be released but this minimum number of buffers will be maintained. If, however, the **min** value specified is zero, then all memory buffers are released.  
  
 Under certain circumstances the number of buffers currently allocated is less than the value specified by the **min** parameter.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]  
  
## See Also  
 [Server Configuration Options &#40;SQL Server&#41;](server-configuration-options-sql-server.md)   
 [ft crawl bandwidth Server Configuration Option](ft-crawl-bandwidth-server-configuration-option.md)   
 [sp_configure &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-configure-transact-sql)  
  
  
