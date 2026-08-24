---
title: "Server Configuration: PolyBase Network Encryption"
description: Set the configuration option for PolyBase network encryption in SQL Server settings.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: hudequei, randolphwest
ms.date: 08/26/2025
ms.service: sql
ms.subservice: polybase
ms.topic: how-to
---

# Server configuration: polybase network encryption

[!INCLUDE [sql-pdw](../../includes/applies-to-version/sql-pdw.md)]

Display or changes the global configuration settings for PolyBase network encryption. This configuration option controls whether PolyBase encrypts the communication channels between the SQL Server instance and the PolyBase Engine, which runs in the same server.

The possible values are described in the following table:

| Value | Meaning |
| --- | --- |
| `0` | Disabled |
| `1` (default) | Enabled |

Enabled is the default setting in SQL Server versions for security compliance.

When enabled, the communication between SQL Server and PolyBase components is encrypted.

When disabled:

- Communication isn't encrypted
- No certificate or extra checks are required.

Disabled encryption configuration can be suitable for environments that are fully trusted, isolated or when no certificate can be provided.

This change takes effect immediately.

## Enable network encryption

The following example enables this setting.

```sql
EXECUTE sp_configure 'show advanced options', 1;
GO

RECONFIGURE;
GO

EXECUTE sp_configure 'polybase network encryption', 1;
GO

RECONFIGURE;
GO
```

## Disable network encryption

The following example disables this setting.

```sql
EXECUTE sp_configure 'show advanced options', 1;
GO

RECONFIGURE;
GO

EXECUTE sp_configure 'polybase network encryption', 0;
GO

RECONFIGURE;
GO
```

## Permissions

All users can execute `sp_configure` with no parameters or the `@configname` parameter.

Requires `ALTER SETTINGS` server-level permission or membership in the **sysadmin** fixed server role to change a configuration value or to run `RECONFIGURE`.

## Related content

- [Exporting data](../../relational-databases/polybase/polybase-configure-hadoop.md#exporting-data)
- [PolyBase overview](../../relational-databases/polybase/overview.md)
- [CREATE EXTERNAL TABLE AS SELECT (CETAS) (Transact-SQL)](../../t-sql/statements/create-external-table-as-select-transact-sql.md)
- [PolyBase errors and possible solutions](../../relational-databases/polybase/polybase-errors-and-possible-solutions.md)
