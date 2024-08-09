---
title: "Server configuration: affinity64 I/O mask"
description: Learn about the affinity64 I/O mask option. See when to use it to bind SQL Server disk I/O to a specified subset of CPUs.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/18/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "processor affinity [SQL Server]"
  - "binding processors [SQL Server]"
  - "affinity64 I/O mask option"
---
# Server configuration: affinity64 I/O mask

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The `affinity64 I/O mask` binds [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] disk I/O to a specified subset of CPUs, similar to the `affinity I/O mask` option. Use `affinity I/O mask` to bind the first 32 processors, and use `affinity64 I/O mask` to bind the remaining processors on the computer. If you reconfigure the `affinity64 I/O mask`, you must restart the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. This option is only visible on the 64-bit version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

## Related content

- [Configure the affinity I/O mask server configuration option](affinity-input-output-mask-server-configuration-option.md)
- [Monitor Resource Usage (Performance Monitor)](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
