---
title: "Server configuration: user options"
description: "Learn about setting user options. See how it changes the default values of the query processing options that SQL Server establishes for user work sessions."
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/18/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "global default for all users [SQL Server]"
  - "users [SQL Server], global defaults"
  - "user options option [SQL Server]"
---
# Server configuration: user options

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to configure the `user options` server configuration option in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)]. The `user options` option specifies global defaults for all users. A list of default query processing options is established for the duration of a user's work session. The `user options` option allows you to change the default values of the `SET` options (if the server's default settings aren't appropriate).

A user can override these defaults with the `SET` statement. You can configure `user options` dynamically for new logins. After you change the setting of `user options`, new login sessions use the new setting. Current login sessions aren't affected.

## Recommendations

The following table lists and describes the configuration values for `user options`. Not all configuration values are compatible with each other. For example, `ANSI_NULL_DFLT_ON` and `ANSI_NULL_DFLT_OFF` can't be set at the same time.

| Value | Configuration | Description |
| --- | --- | --- |
| `1` | `DISABLE_DEF_CNST_CHK` | Controls interim or deferred constraint checking. |
| `2` | `IMPLICIT_TRANSACTIONS` | For dblib network library connections, controls whether a transaction is started implicitly when a statement is executed. The `IMPLICIT_TRANSACTIONS` setting has no effect on ODBC or OLEDB connections. |
| `4` | `CURSOR_CLOSE_ON_COMMIT` | Controls behavior of cursors after a commit operation has been performed. |
| `8` | `ANSI_WARNINGS` | Controls truncation and `NULL` in aggregate warnings. |
| `16` | `ANSI_PADDING` | Controls padding of fixed-length variables. |
| `32` | `ANSI_NULLS` | Controls `NULL` handling when using equality operators. |
| `64` | `ARITHABORT` | Terminates a query when an overflow or divide-by-zero error occurs during query execution. |
| `128` | `ARITHIGNORE` | Returns `NULL` when an overflow or divide-by-zero error occurs during a query. |
| `256` | `QUOTED_IDENTIFIER` | Differentiates between single and double quotation marks when evaluating an expression. |
| `512` | `NOCOUNT` | Turns off the message returned at the end of each statement that states how many rows were affected. |
| `1024` | `ANSI_NULL_DFLT_ON` | Alters the session's behavior to use ANSI compatibility for nullability. New columns defined without explicit nullability are defined to allow nulls. |
| `2048` | `ANSI_NULL_DFLT_OFF` | Alters the session's behavior not to use ANSI compatibility for nullability. New columns defined without explicit nullability don't allow nulls. |
| `4096` | `CONCAT_NULL_YIELDS_NULL` | Returns `NULL` when concatenating a `NULL` value with a string. |
| `8192` | `NUMERIC_ROUNDABORT` | Generates an error when a loss of precision occurs in an expression. |
| `16384` | `XACT_ABORT` | Rolls back a transaction if a Transact-SQL statement raises a run-time error. |

The bit positions in `user options` are identical to the bit positions in `@@OPTIONS`. Each connection has its own `@@OPTIONS` function, which represents the configuration environment. When logging in to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], a user receives a default environment that assigns the current `user options` value to `@@OPTIONS`. Executing `SET` statements for `user options` affects the corresponding value in the session's `@@OPTIONS` function. All connections created after this setting is changed receive the new value.

## Permissions

Execute permissions on `sp_configure` with no parameters or with only the first parameter are granted to all users by default. To execute `sp_configure` with both parameters to change a configuration option or to run the `RECONFIGURE` statement, a user must be granted the `ALTER SETTINGS` server-level permission. The `ALTER SETTINGS` permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.

<a id="SSMSProcedure"></a>

## Use SQL Server Management Studio

1. In Object Explorer, right-click a server and select **Properties**.

1. Select the **Connections** node.

1. In the **Default connection options** box, select one or more attributes to configure the default query-processing options for all connected users.

By default, no user options are configured.

<a id="TsqlProcedure"></a>

## Use Transact-SQL

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to configure the `user options` to change the setting for the `ANSI_WARNINGS` server option.

   ```sql
   USE master;
   GO

   EXECUTE sp_configure 'user options', 8;
   GO

   RECONFIGURE;
   GO
   ```

<a id="FollowUp"></a>

## Follow up: After you configure the user options configuration option

The setting takes effect immediately without restarting the server.

## Related content

- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [SET Statements (Transact-SQL)](../../t-sql/statements/set-statements-transact-sql.md)
