---
title: "hardware offload enable Server Configuration Option | Microsoft Docs"
description: 'Learn about the "hardware offload enabled" option. '
ms.custom: ""
ms.date: "08/16/2022"
ms.prod: sql
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "hardware offload enable"
  - "HARDWARE_OFFLOAD"
ms.reviewer: dplessMSFT 
ms.technology: configuration
ms.topic: conceptual
author: MikeRayMSFT
ms.author: mikeray

---
# Hardware offload enabled configuration option

[!INCLUDE [sqlserver2022](../../includes/applies-to-version/sqlserver2022.md)]

The `hardware offload enabled` configuration option allows integrated offloading and acceleration with validated solutions from partners. For more information, see [Integrated offloading and acceleration](../../relational-databases/integrated-acceleration/overview.md).

This is an advanced option.

This option requires a restart.

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

[sys.dm_server_accelerator_status](../../relational-databases/system-dynamic-management-views/sys-dm-server-accelerator-status-transact-sql.md)

[Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)

[ALTER SERVER CONFIGURATION (Transact-SQL)](../../t-sql/statements/alter-server-configuration-transact-sql.md)
