---
title: "disallow results from triggers (server configuration option)"
description: "Learn about the 'disallow results from triggers' option. See how it can prevent problems in applications that aren't designed to work with result sets."
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/25/2023
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "triggers [SQL Server], result sets"
  - "result sets [SQL Server], triggers"
  - "disallow results from triggers option"
---
# disallow results from triggers (server configuration option)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Use the **disallow results from triggers** option to control whether triggers return result sets. Triggers that return result sets may cause unexpected behavior in applications that aren't designed to work with them.

> [!IMPORTANT]  
> The ability to return result sets from triggers will be removed in a future version of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. Avoid returning result sets from triggers in new development work, and plan to modify applications that currently do this. To prevent triggers from returning result sets, change the disallow results from triggers option to a value of `1`. The default setting for the disallow results from triggers option will be set to `1` in a future version of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

When set to `1`, the **disallow results from triggers** option is set to `ON`. The default setting for this option is `0` (`OFF`). If this option is set to `1` (`ON`), any attempt by a trigger to return a result set fails, and the user receives the following error message:

```output
Msg 524, Level 16, State 1, Procedure <Procedure Name>, Line <Line#>

A trigger returned a resultset and the server option 'disallow_results_from_triggers' is true.
```

The **disallow results from triggers** option is applied at the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance level, and it determines behavior for all existing triggers within the instance.

The **disallow results from triggers** option is an advanced option. If you are using the `sp_configure` system stored procedure to change the setting, you can change disallow results from triggers only when **show advanced options** is set to `1`. The setting takes effect immediately without a server restart.

You can check if the option is set correctly using the following Transact-SQL code:

```sql
-- Check the current value for the option
SELECT [name], value_in_use
FROM sys.configurations
WHERE [name] LIKE 'disallow results from triggers';

-- Set the disallow results from triggers option to 1. This is an advanced option so that must be enabled first
EXEC sp_configure 'show advanced options', 1;
RECONFIGURE;
GO

-- Set the disallow results from triggers option
EXEC sp_configure 'disallow results from triggers', 1;
EXEC sp_configure 'show advanced options', 0;
RECONFIGURE
GO

-- Validate that the option is set to 1
SELECT [name], value_in_use
FROM sys.configurations
WHERE [name] LIKE 'disallow results from triggers';
GO
```

## See also

- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Server Configuration Options (SQL Server)](../../database-engine/configure-windows/server-configuration-options-sql-server.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
