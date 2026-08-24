---
title: "Server Configuration: hardware offload enabled"
description: "Learn about the hardware offload enabled option."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: dpless, wiassaf, randolphwest
ms.date: 08/26/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "hardware offload enable"
  - "HARDWARE_OFFLOAD"
dev_langs:
  - "TSQL"
---
# Server configuration: hardware offload enabled

[!INCLUDE [sqlserver2022](../../includes/applies-to-version/sqlserver2022-and-later.md)]

The `hardware offload enabled` configuration option allows integrated acceleration and offloading with validated solutions from partners. For more information, see [Integrated acceleration and offloading](../../relational-databases/integrated-acceleration/overview.md).

Changing option requires a restart.

## Examples

Set the server configuration option `hardware offload enabled` to `1`. By default, this setting is `0`. This setting is an advanced configuration option. To set this setting, run the following commands:

```sql
EXECUTE sp_configure 'show advanced options', 1;
GO

RECONFIGURE;
GO

EXECUTE sp_configure 'hardware offload enabled', 1;
GO

RECONFIGURE;
GO
```

> [!NOTE]  
> If `hardware offload enabled` is disabled (`0`), all offloading and acceleration solutions are disabled.

## Related content

- [sys.dm_server_accelerator_status (Transact-SQL)](../../relational-databases/system-dynamic-management-objects/sys-dm-server-accelerator-status-transact-sql.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [ALTER SERVER CONFIGURATION (Transact-SQL)](../../t-sql/statements/alter-server-configuration-transact-sql.md)
