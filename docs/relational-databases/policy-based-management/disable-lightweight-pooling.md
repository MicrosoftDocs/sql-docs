---
title: "Disable Lightweight Pooling"
description: "Disable Lightweight Pooling"
author: VanMSFT
ms.author: vanto
ms.date: 12/15/2023
ms.service: sql
ms.subservice: security
ms.topic: reference
helpviewer_keywords:
  - "Best Practices [Database Engine]"
---
# Disable lightweight pooling

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This rule checks that lightweight pooling is disabled on the server. Setting `lightweightpooling` to `1` causes [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to switch to fiber mode scheduling. Fiber mode is intended for certain situations in which the context switching of the UMS workers is the important bottleneck in performance. Because this is rare, fiber mode seldom improves performance or scalability on the typical system.

## Best practices recommendations

The `lightweightpooling` option should only be enabled after thorough testing, after all other performance tuning opportunities are evaluated, and when context switching is a known issue in your environment.

We recommend that you don't use fiber mode scheduling for routine operation because it can decrease performance by preventing the regular benefits of context switching, and because some components of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that use Thread Local Storage (TLS) or thread-owned objects, such as mutexes (a kind of Win32 kernel object), can't function correctly in fiber mode.

To remove lightweight pooling, execute the following statement, and then restart the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)].

```sql
sp_configure 'show advanced options', 1;
GO
sp_configure 'lightweight pooling', 0;
GO
RECONFIGURE;
GO
```

## For more information

[lightweight pooling Server Configuration Option](../../database-engine/configure-windows/lightweight-pooling-server-configuration-option.md)

## Related content

- [Monitor and Enforce Best Practices by Using Policy-Based Management](monitor-and-enforce-best-practices-by-using-policy-based-management.md)
