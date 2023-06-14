---
title: "lightweight pooling (server configuration option)"
description: "Learn about the lightweight pooling option. See how it can provide better throughput in symmetric multiprocessing environments with excessive context switching."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: jopilov
ms.date: 12/16/2022
ms.service: sql
ms.subservice: configuration
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
---
# lightweight pooling (server configuration option)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Use the **lightweight pooling** option (also called "fiber mode") to provide a means of reducing the system overhead associated with the excessive context switching sometimes seen in symmetric multiprocessing (SMP) environments. When excessive context switching is present, lightweight pooling can provide better throughput by performing the context switching inline, thus helping to reduce user/kernel ring transitions.

 Fiber mode, which is based on [Windows fibers](/windows/win32/procthread/fibers), is intended for situations in which the context switching of [worker threads](../../relational-databases/thread-and-task-architecture-guide.md#workers) are the critical bottleneck in performance. Because this is rare, fiber mode rarely enhances performance or scalability on the typical system. Improved context switching in [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Server has also reduced the need for fiber mode. We do not recommend that you use fiber mode scheduling for routine operation. That's because fiber mode can decrease performance by inhibiting the regular benefits of context switching, and because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components that use Thread Local Storage (TLS) or thread-owned objects, such as [Windows mutexes](/windows/win32/sync/mutex-objects), cannot function correctly in fiber mode.

 Setting **lightweight pooling** to 1 causes [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to switch to fiber mode scheduling. The default value for this option is 0.

 The **lightweight pooling** option is an advanced option. If you are using the **sp_configure** system stored procedure to change the setting, you can change **lightweight pooling** only when **show advanced options** is set to 1. The setting takes effect after the server is restarted.

> [!NOTE]  
> Lightweight pooling is not supported for [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows 2000 and [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows XP. [!INCLUDE[winserver2003](../../includes/winserver2003-md.md)] provides full support for lightweight pooling.

> [!NOTE]  
> Common language runtime (CLR) execution is not supported under lightweight pooling. Disable one of two options: "clr enabled" or "lightweight pooling". Features that rely upon CLR and that do not work properly in fiber mode include the hierarchy data type, replication, and Policy-Based Management.

## See also

- [clr enabled Server Configuration Option](../../database-engine/configure-windows/clr-enabled-server-configuration-option.md)
- [Server Configuration Options (SQL Server)](../../database-engine/configure-windows/server-configuration-options-sql-server.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [clr enabled Server Configuration Option](../../database-engine/configure-windows/clr-enabled-server-configuration-option.md)
