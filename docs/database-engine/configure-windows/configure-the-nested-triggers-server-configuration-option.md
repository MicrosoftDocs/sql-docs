---
title: "Server configuration: nested triggers"
description: Learn about the nested triggers option. See how to use it to set the number of levels of AFTER triggers that can cascade in SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/18/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "nested triggers option"
---
# Server configuration: nested triggers

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to configure the `nested triggers` server configuration option in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)]. The `nested triggers` option controls whether an `AFTER` trigger can cascade. That is, perform an action that initiates another trigger, which initiates another trigger, and so on. When `nested triggers` is set to `0`, `AFTER` triggers can't cascade. When `nested triggers` is set to `1` (the default), `AFTER` triggers can cascade to as many as 32 levels. `INSTEAD OF` triggers can be nested regardless of the setting of this option.

## Permissions

Execute permissions on `sp_configure` with no parameters or with only the first parameter are granted to all users by default. To execute `sp_configure` with both parameters to change a configuration option or to run the `RECONFIGURE` statement, a user must be granted the `ALTER SETTINGS` server-level permission. The `ALTER SETTINGS` permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.

<a id="SSMSProcedure"></a>

## Use SQL Server Management Studio

1. In **Object Explorer**, right-click a server, and then select **Properties**.

1. On the **Advanced** page, set the **Allow Triggers to Fire Others** option to **True** (the default) or **False**.

<a id="TsqlProcedure"></a>

## Use Transact-SQL

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to set the value of the `nested triggers` option to `0`.

   ```sql
   USE master;
   GO

   EXECUTE sp_configure 'show advanced options', 1;
   GO

   RECONFIGURE;
   GO

   EXECUTE sp_configure 'nested triggers', 0;
   GO

   RECONFIGURE;
   GO

   EXECUTE sp_configure 'show advanced options', 0;
   GO

   RECONFIGURE;
   GO
   ```

For more information, see [Server configuration options](server-configuration-options-sql-server.md).

<a id="FollowUp"></a>

## Follow up: After you configure the nested triggers option

The setting takes effect immediately without restarting the server.

## Related content

- [Create Nested Triggers](../../relational-databases/triggers/create-nested-triggers.md)
- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
