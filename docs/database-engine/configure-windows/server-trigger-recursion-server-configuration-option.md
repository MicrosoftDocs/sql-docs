---
title: "Server Configuration: server trigger recursion"
description: "Learn how the server trigger recursion option affects recursion in SQL Server server-level triggers. See how to turn direct and indirect recursion on and off."
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/26/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "recursive triggers [SQL Server]"
  - "triggers [SQL Server], recursive"
  - "server trigger recursion option"
---
# Server configuration: server trigger recursion

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Use the `server trigger recursion` option to specify whether to allow server-level triggers to fire recursively. When this option is set to `1` (enabled), server-level triggers will be allowed to fire recursively. When set to `0` (disabled), server-level triggers can't be fired recursively.

Only direct recursion is prevented when the server trigger recursion option is set to `0` (disabled). (To disable indirect recursion, set the `nested triggers` option to `0`.) The default value for this option is `1` (enabled). The setting takes effect immediately (without a server restart).

For more information, see [Create Nested Triggers](../../relational-databases/triggers/create-nested-triggers.md).

## Related content

- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [sys.sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
