---
title: "SQL Server Properties (Startup Parameters Tab)"
description: Use the Startup Parameters tab of the SQL Server Properties dialog box to add or remove startup parameters, which can affect Database Engine performance.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/15/2025
ms.service: sql
ms.subservice: tools-other
ms.topic: ui-reference
ms.collection:
  - data-tools
monikerRange: ">=sql-server-2017"
---
# SQL Server Properties (Startup Parameters tab)

[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

Use this dialog box to add or remove startup parameters for the [!INCLUDE [ssDE](../../includes/ssde-md.md)]. Startup parameters can have a large effect on the [!INCLUDE [ssDE](../../includes/ssde-md.md)] performance. Before adding or changing startup parameters, see [Database Engine Service startup options](../../database-engine/configure-windows/database-engine-service-startup-options.md).

## Options

#### Specify a startup parameter

To add a parameter, type the parameter, and then select **Add**.

To modify one of the required parameters, select the parameter in the **Existing parameters** box, change the values in the **Specify a startup parameter** box, and then select **Update**.

#### Existing parameters

To remove a parameter, select a parameter, and then select **Remove**.

## Parameter format

Don't enter a separator between parameters. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager automatically adds the separator. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager enforces the following parameter requirements.

- Leading and trailing spaces are trimmed from any startup parameter.

- All startup parameters start with a hyphen (`-`), and the second value is a letter.

## Required parameters

The following parameters are required. They can be changed but not removed.

- `-d` is the path of the `master.mdf` file (the `master` database data file).
- `-l` is the path of the `master.ldf` file (the `master` database log file).
- `-e` is the path of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] error log files.

If the file path parameters are incorrect, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] might not start.

For more information about how to move the `master` database, see [Move system databases](../../relational-databases/databases/move-system-databases.md).

## Optional parameters

All of the supported startup parameters are described in [Database Engine Service startup options](../../database-engine/configure-windows/database-engine-service-startup-options.md). A startup parameter of `-T<trace#>` indicates that an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] should be started with a specified trace flag (`<trace#>`) in effect. Trace flags are used to start the server with nonstandard behavior. For more information about trace flags, see [Set trace flags with DBCC TRACEON](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).

> [!CAUTION]  
> You might see additional undocumented startup parameters and trace flags described online. Undocumented startup parameters and trace flags are created to address uncommon problems or to force certain conditions required for testing. Using undocumented startup parameters can have unexpected results. Don't use undocumented parameters unless directed by Microsoft Customer Support Services.

The following list describes some common optional parameters.

| Parameter | Short description | More information |
| --- | --- | --- |
| `-m` | Starts an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode. | [Single-user mode for SQL Server](../../database-engine/configure-windows/start-sql-server-in-single-user-mode.md) |
| `-T1204` | Returns the resources and types of locks participating in a deadlock and also the current command affected. | [Trace flag 1204](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf1204) |
| `-T1224` | Disables lock escalation based on the number of locks.<br /><br />[!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] introduces [Optimized locking](../../relational-databases/performance/optimized-locking.md) instead. | [Trace flag 1224](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf1224) |
| `-T3608` | Prevents [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] from automatically starting and recovering any database except the `master` database. | [Trace flag 3608](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf3608) |
| `-T7806` | Enables a dedicated administrator connection (DAC) on [!INCLUDE [ssExpress](../../includes/ssexpress-md.md)]. | [Trace flag 7806](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf7806) |

> [!CAUTION]  
> Some optional parameters can change server behavior and might affect performance.

## Permissions

Use of this page is restricted to users who can change the related entries in the registry. This includes the following users:

- Members of the local administrators group.

- The domain account that is used by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], if the [!INCLUDE [ssDE](../../includes/ssde-md.md)] is configured to run under a domain account.

## Related content

- [Database Engine Service startup options](../../database-engine/configure-windows/database-engine-service-startup-options.md)
- [Set trace flags with DBCC TRACEON](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md)
- [SQL Server Configuration Manager](sql-server-configuration-manager.md)
