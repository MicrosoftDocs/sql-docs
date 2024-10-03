---
title: "Server configuration: affinity I/O mask"
description: Learn about the affinity I/O mask option. Use it to enhance the performance of SQL Server threads that issue I/Os by binding disk I/O to specified CPUs.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/18/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "affinity I/O mask option"
  - "processor affinity [SQL Server]"
  - "binding processors [SQL Server]"
  - "CPU affinity mask option"
---
# Server configuration: affinity I/O mask

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

To carry out multitasking, Windows sometimes move process threads among different processors. Although efficient from an operating system point of view, this activity can reduce [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] performance under heavy system loads, as each processor cache is repeatedly reloaded with data. Assigning processors to specific threads can improve performance under these conditions by eliminating processor reloads; such an association between a thread and a processor is called processor affinity.

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supports processor affinity with two affinity mask options: `affinity mask` (also known as *CPU affinity mask*) and `affinity I/O mask`. For more information on the `affinity mask` option, see [Configure the affinity mask server configuration option](affinity-mask-server-configuration-option.md). CPU and I/O affinity support for servers with 33 to 64 processors requires that you also use the [affinity64 mask](affinity64-mask-server-configuration-option.md) and [affinity64 I/O mask](affinity64-input-output-mask-server-configuration-option.md) server configuration options respectively.

> [!NOTE]  
> Affinity support for servers with 33 to 64 processors is only available on 64-bit operating systems.

The `affinity I/O mask` option binds [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] disk I/O to a specified subset of CPUs. In high-end [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] online transactional processing (OLTP) environments, this extension can enhance the performance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] threads issuing I/Os. This enhancement doesn't support hardware affinity for individual disks or disk controllers.

The value for `affinity I/O mask` specifies which CPUs in a multiprocessor computer are eligible to process [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] disk I/O operations. The mask is a bitmap in which the rightmost bit specifies the lowest-order CPU(0), the bit to its immediate left specifies the next-lowest-order CPU(1), and so on. To configure more than 32 processors, set both the `affinity I/O mask` and the `affinity64 I/O mask`.

The values for `affinity I/O mask` are as follows:

| Bytes in mask | Number of CPUs |
| --- | --- |
| **1-byte** | Up to 8 CPUs |
| **2-byte** | Up to 16 CPUs |
| **3-byte** | Up to 24 CPUs |
| **4-byte** | Up to 32 CPUs |

To cover more than 32 CPUs, configure a 4-byte `affinity I/O mask` for the first 32 CPUs and up to a 4-byte `affinity64 I/O mask` for the remaining CPUs.

A `1` bit in the affinity I/O pattern specifies that the corresponding CPU is eligible to perform [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] disk I/O operations. A `0` bit specifies that no [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] disk I/O operations should be scheduled for the corresponding CPU. When all bits are set to `0`, or `affinity I/O mask` isn't specified, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] disk I/O is scheduled to any of the CPUs eligible to process [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] threads.

Because setting the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] `affinity I/O mask` option is a specialized operation, use it only when necessary. In most cases, the default Windows affinity provides the best performance.

When specifying the `affinity I/O mask` option, you must use it with the `affinity mask` configuration option. Don't enable the same CPU in both the `affinity I/O mask` switch and the `affinity mask` option. The bits corresponding to each CPU should be in one of the following three states:

- `0` in both the `affinity I/O mask` option and the `affinity mask` option.
- `1` in the `affinity I/O mask` option and `0` in the `affinity mask` option.
- `0` in the `affinity I/O mask` option and `1` in the `affinity mask` option.

The `affinity I/O mask` option is an advanced option. If you're using the `sp_configure` system stored procedure to change the setting, you can change `affinity I/O mask` only when `show advanced options` is set to `1`. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], reconfiguring the `affinity I/O mask` option requires a restart of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance.

> [!CAUTION]  
> Don't configure CPU affinity in the Windows operating system and also configure the `affinity mask` in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. These settings are attempting to achieve the same result, and if the configurations are inconsistent, you can have unpredictable results. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] CPU affinity is best configured using the `sp_configure` system stored procedure in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

## Related content

- [Monitor Resource Usage (Performance Monitor)](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)
- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [Configure the affinity64 I/O mask server configuration option](affinity64-input-output-mask-server-configuration-option.md)
