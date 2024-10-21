---
title: "Server configuration: lightweight pooling"
description: "Learn about the lightweight pooling option. See how it can provide better throughput in symmetric multiprocessing environments with excessive context switching."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: jopilov
ms.date: 10/18/2024
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
# Server configuration: lightweight pooling

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Use the `lightweight pooling` option (also called *fiber mode*) to provide a means of reducing the system overhead associated with the excessive context switching sometimes seen in symmetric multiprocessing (SMP) environments. When excessive context switching is present, lightweight pooling might provide better throughput by performing the context switching inline, thus helping to reduce user/kernel ring transitions.

## Limitations

Common language runtime (CLR) execution isn't supported under lightweight pooling. Disable one of two options: `clr enabled` or `lightweight pooling`. Features that rely upon CLR and that don't work properly in fiber mode include the [hierarchyid data type](../../t-sql/data-types/hierarchyid-data-type-method-reference.md), [replication](../../relational-databases/replication/sql-server-replication.md), and [Monitor and Enforce Best Practices by Using Policy-Based Management](../../relational-databases/policy-based-management/monitor-and-enforce-best-practices-by-using-policy-based-management.md).

Lightweight pooling isn't supported on [!INCLUDE [ssexpress-md](../../includes/ssexpress-md.md)] edition.

## Remarks

Fiber mode, which is based on [Windows fibers](/windows/win32/procthread/fibers), is intended for situations in which the context switching of [worker threads](../../relational-databases/thread-and-task-architecture-guide.md#workers) are the critical bottleneck in performance. Because this scenario is rare, fiber mode rarely enhances performance or scalability on a typical system. Improved context switching in Windows Server reduces the need for fiber mode.

We don't recommend that you use fiber mode scheduling for routine operation. Fiber mode can *decrease* performance by inhibiting the regular benefits of context switching, and because [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] components that use Thread Local Storage (TLS) or thread-owned objects, such as [Windows mutexes](/windows/win32/sync/mutex-objects), can't function correctly in fiber mode.

Setting `lightweight pooling` to `1` causes [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to switch to fiber mode scheduling. The default value for this option is `0`.

The `lightweight pooling` option is an advanced option. If you use the `sp_configure` system stored procedure to change the setting, you can change `lightweight pooling` only when `show advanced options` is set to `1`. The setting takes effect after the server is restarted.

## Related content

- [clr enabled (server configuration option)](clr-enabled-server-configuration-option.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
