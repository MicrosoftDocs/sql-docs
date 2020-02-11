---
title: "lightweight pooling Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "default lightweight pooling"
  - "decreasing overhead"
  - "lightweight pooling option"
  - "system overhead [SQL Server]"
  - "performance [SQL Server], lightweight pooling"
  - "context switching [SQL Server], lightweight pooling option"
  - "excessive context switching [SQL Server]"
  - "reducing overhead"
  - "overhead [SQL Server]"
ms.assetid: 2dc11b61-d065-4126-8e00-acf40390f9fb
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# lightweight pooling Server Configuration Option
  Use the **lightweight pooling** option to provide a means of reducing the system overhead associated with the excessive context switching sometimes seen in symmetric multiprocessing (SMP) environments. When excessive context switching is present, lightweight pooling can provide better throughput by performing the context switching inline, thus helping to reduce user/kernel ring transitions.  
  
 Fiber mode is intended for certain situations in which the context switching of the UMS workers are the critical bottleneck in performance. Because this is rare, fiber mode rarely enhances performance or scalability on the typical system. Improved context switching in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[winxpsvr](../../includes/winxpsvr-md.md)] has also reduced the need for fiber mode. We do not recommend that you use fiber mode scheduling for routine operation. This is because it can decrease performance by inhibiting the regular benefits of context switching, and because some components of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that use Thread Local Storage (TLS) or thread-owned objects, such as mutexes (a type of Win32 kernel object), cannot function correctly in fiber mode.  
  
 Setting **lightweight pooling** to 1 causes [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to switch to fiber mode scheduling. The default value for this option is 0.  
  
 The **lightweight pooling** option is an advanced option. If you are using the **sp_configure** system stored procedure to change the setting, you can change **lightweight pooling** only when **show advanced options** is set to 1. The setting takes effect after the server is restarted.  
  
> [!NOTE]  
>  Lightweight pooling is not supported for [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows 2000 and [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows XP. [!INCLUDE[winxpsvr](../../includes/winxpsvr-md.md)] provides full support for lightweight pooling.  
  
> [!NOTE]  
>  Common language runtime (CLR) execution is not supported under lightweight pooling. Disable one of two options: "clr enabled" or "lightweight pooling". Features that rely upon CLR and that do not work properly in fiber mode include the hierarchy data type, replication, and Policy-Based Management.  
  
## See Also  
 [clr enabled Server Configuration Option](clr-enabled-server-configuration-option.md)   
 [Server Configuration Options &#40;SQL Server&#41;](server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-configure-transact-sql)   
 [clr enabled Server Configuration Option](clr-enabled-server-configuration-option.md)  
  
  
