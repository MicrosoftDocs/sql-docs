---
title: "Keep the Locks Configuration Option Default Value"
description: "Keep the Locks Configuration Option Default Value."
author: VanMSFT
ms.author: vanto
ms.date: 12/15/2023
ms.service: sql
ms.subservice: security
ms.topic: reference
helpviewer_keywords:
  - "Best Practices [Database Engine]"
---
# Keep the locks configuration option default value

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This rule checks the value of the locks configuration option. This option determines the maximum number of available locks. This limits how much memory the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] uses for locks. The default setting of 0 enables the [!INCLUDE [ssDE](../../includes/ssde-md.md)] to allocate and deallocate lock structures dynamically based on changing system requirements.

If locks are nonzero, batch jobs stop and an `out of locks` error message will be generated if the value specified is exceeded.

## Best practices recommendations

Use the `sp_configure` system stored procedure to change the value of locks to its default setting by using the following statement:

```
EXEC sp_configure 'locks', 0;
```

## For more information

[Configure the locks (server configuration option)](../../database-engine/configure-windows/configure-the-locks-server-configuration-option.md)

[sys.dm_tran_locks (Transact-SQL)](../system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md)

[sys.dm_os_wait_stats (Transact-SQL)](../system-dynamic-management-views/sys-dm-os-wait-stats-transact-sql.md)

[Microsoft Knowledge Base article 271509](/troubleshoot/sql/performance/understand-resolve-blocking)

## Related content

- [Monitor and Enforce Best Practices by Using Policy-Based Management](monitor-and-enforce-best-practices-by-using-policy-based-management.md)
