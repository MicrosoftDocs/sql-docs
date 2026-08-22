---
title: "Server Configuration: suppress recovery model errors"
description: "Suppress recovery model errors"
author: MladjoA
ms.author: mlandzic
ms.reviewer: randolphwest
ms.date: 08/26/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
---

# Server configuration: suppress recovery model errors

[!INCLUDE [_asmi](../../includes/applies-to-version/asmi.md)]

SQL Server [recovery models](../../relational-databases/backup-restore/recovery-models-sql-server.md) control transaction log maintenance. The Full recovery model ensures no work is lost because of a lost or damaged data file, and supports recovery to an arbitrary point in time within the backup retention policy. The Full recovery model is the default and only recovery model supported in [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)]. Attempts to change the recovery model in SQL Managed Instance return an error message.

In [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], you can use the `suppress recovery model errors` advanced configuration option to specify whether commands for changing the database recovery model return errors, or warnings only. When this option is set to `1` (enabled), executing the command `ALTER DATABASE SET RECOVERY` doesn't change the recovery model of the database, and it returns a warning message instead of an error message. When this option is set to `0` (disabled), executing the command `ALTER DATABASE SET RECOVERY` returns an error message.

The `suppress recovery model errors` option is helpful in cases where legacy or third-party applications attempt to change recovery model to Simple or Bulk logged, even though it's not a critical or mandatory requirement. When a change of the recovery model is the only blocker for using SQL Managed Instance, turning on the `suppress recovery model errors` configuration option removes that blocker. This option is especially useful if an alternative solution of changing the application code isn't feasible or affordable.

## Examples

The following example enables suppression of error messages related to the change of database recovery model, and then executes command for changing database recovery model, returning a warning only. The recovery model isn't actually changed. Replace `<database>` with the actual database name.

```sql
-- Turn advanced configuration options on:
EXECUTE sp_configure 'show advanced options', 1;
GO

RECONFIGURE;
GO

-- Enable suppression of error messages for recovery model change:
EXECUTE sp_configure 'suppress recovery model errors', 1;
GO

RECONFIGURE;
GO

-- Execute command for changing recovery model to Simple:
ALTER DATABASE <database> SET RECOVERY SIMPLE;
GO
```

## Related content

- [Server configuration options](server-configuration-options-sql-server.md)
- [sys.sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
