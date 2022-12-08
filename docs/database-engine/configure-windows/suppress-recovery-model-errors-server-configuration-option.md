---
title: "Suppress recovery model errors - server configuration option"
description: "Suppress recovery model errors - server configuration option"
author: MladjoA
ms.author: mlandzic
ms.date: "07/20/2020"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Suppress recovery model errors server configuration option

[!INCLUDE[tsql-appliesto-xxxxxx-asdbmi-xxxx-xxx-md.md](../../includes/tsql-appliesto-xxxxxx-asdbmi-xxxx-xxx-md.md)]

SQL Server [recovery models](../../relational-databases/backup-restore/recovery-models-sql-server.md) control transaction log maintenance. Full recovery model ensures no work is lost because of a lost or damaged data file, and supports recovery to an arbitrary point in time within backup retention policy. Full recovery model is a default and the only recovery model supported in SQL Managed Instance. Attempts to change recovery model in SQL Managed Instance will return error message.

Use the **suppress recovery model errors** advanced configuration option to specify whether commands for changing database recovery model, executed on SQL Managed Instance, will return error or warning only. When this option is set to 1 (ON) on SQL Managed Instance, executing command ALTER DATABASE SET RECOVERY will not change the recovery model of the database, still it will not return error but warning message instead. When this option is set to 0 (OFF) on SQL Managed Instance, executing command ALTER DATABASE SET RECOVERY will return error message.

**Suppress recovery model errors** option is helpful in cases where legacy or third-party applications attempt to change recovery model to Simple or Bulk logged, even though it is not a critical or mandatory requirement. When change of the recovery model is the only blocker for using SQL Managed Instance, turning on suppress recovery model errors configuration option removes that blocker. This option is especially useful if an alternative solution of changing the application code is not feasible or affordable.

## Examples

The following example enables suppression of error messages related to the change of database recovery model, and then executes command for changing database recovery model, returning warning only. Recovery model is not actually changed. Make sure to replace *my_database* with the actual database name.

```sql
-- Turn advanced configuration options on:
sp_configure 'show advanced options', 1 ;  
GO
RECONFIGURE ;  
GO

-- Enable suppression of error messages for recovery model change:
sp_configure 'suppress recovery model errors', 1 ;  
GO
RECONFIGURE ;  
GO

-- Execute command for changing recovery model to Simple:
ALTER DATABASE my_database SET RECOVERY SIMPLE;
GO
```

## See also

[Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)

[sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)

[RECONFIGURE &#40;Transact-SQL&#41;](../../t-sql/language-elements/reconfigure-transact-sql.md)