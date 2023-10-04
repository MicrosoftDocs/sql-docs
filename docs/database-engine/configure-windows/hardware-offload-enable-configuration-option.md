---
title: "Hardware offload enabled server configuration option"
description: "Learn about the hardware offload enabled option."
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: david.pless, wiassaf
ms.date: 08/17/2022
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "hardware offload enable"
  - "HARDWARE_OFFLOAD"
dev_langs:
  - "TSQL"
---
# Hardware offload enabled configuration option

[!INCLUDE [sqlserver2022](../../includes/applies-to-version/sqlserver2022.md)]

The `hardware offload enabled` configuration option allows integrated acceleration and offloading with validated solutions from partners. For more information, see [Integrated acceleration and offloading](../../relational-databases/integrated-acceleration/overview.md).

Changing option requires a restart.

## Example

Set the server configuration option `hardware offload enabled` to `1`. By default, this setting is `0`. This setting is an advanced configuration option. To set this setting, run the following commands:

```sql
sp_configure 'show advanced options', 1
GO
RECONFIGURE
GO
sp_configure 'hardware offload enabled', 1
GO
RECONFIGURE
GO
```

> [!NOTE]
> If `hardware offload enabled` is disabled (`0`), all offloading and acceleration solutions are disabled.
  
## Next steps

 - [sys.dm_server_accelerator_status](../../relational-databases/system-dynamic-management-views/sys-dm-server-accelerator-status-transact-sql.md)
 - [Server configuration options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)
 - [ALTER SERVER CONFIGURATION (Transact-SQL)](../../t-sql/statements/alter-server-configuration-transact-sql.md)
