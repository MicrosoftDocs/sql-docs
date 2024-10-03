---
title: "Server configuration: affinity64 mask"
description: Find out about the affinity64 mask option. See when to use it in SQL Server to bind processors to specific threads.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/18/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "processor affinity [SQL Server]"
  - "affinity64 mask option"
  - "binding processors [SQL Server]"
---
# Server configuration: affinity64 mask

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The `affinity64 mask` binds processors to specific threads, similar to the [affinity mask](affinity-mask-server-configuration-option.md) option. Use `affinity mask` to bind the first 32 processors, and use `affinity64 mask` to bind the remaining processors on the computer. This option is only visible on the 64-bit version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

> [!NOTE]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [ALTER SERVER CONFIGURATION](../../t-sql/statements/alter-server-configuration-transact-sql.md) instead.

## Related content

- [Configure the affinity mask server configuration option](affinity-mask-server-configuration-option.md)
- [Monitor Resource Usage (Performance Monitor)](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
