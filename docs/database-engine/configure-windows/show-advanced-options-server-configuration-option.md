---
title: "Server Configuration: show advanced options"
description: "Learn about the show advanced options option. See how to use it to list advanced options when you run the SQL Server system stored procedure sp_configure."
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/26/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "sp_configure"
  - "show advanced options option"
---
# Server configuration: show advanced options

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Use the `show advanced options` option to display the `sp_configure` system stored procedure advanced options. When you set `show advanced options` to `1`, you can list the advanced options by using `sp_configure`. The default is `0`.

The setting takes effect immediately without a server restart.

## Related content

- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [sys.sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
