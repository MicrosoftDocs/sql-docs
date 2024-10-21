---
title: "Server configuration: index create memory"
description: Learn how to configure the index create memory option, to set the maximum amount of memory that SQL Server initially allocates for sort operations, when creating indexes.
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/18/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "index create memory option"
---
# Server configuration: index create memory

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to configure the `index create memory` server configuration option in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)]. The `index create memory` option controls the maximum amount of memory initially allocated for sort operations when creating indexes. The default value for this option is `0` (self-configuring). If more memory is later needed for index creation and the memory is available, the server uses it. Doing so exceeds the setting of this option. If more memory isn't available, the index creation continues using the memory already allocated.

## Limitations

The setting of the [min memory per query](configure-the-min-memory-per-query-server-configuration-option.md) option has precedence over the `index create memory` option. If you change both options and the `index create memory` is less than `min memory per query`, you receive a warning message, but the value is set. During query execution, you receive a similar warning.

When you use partitioned tables and indexes, the minimum memory requirements for index creation might increase significantly if there are non-aligned partitioned indexes and a high degree of parallelism. This option controls the total initial amount of memory allocated for all index partitions in a single index creation operation. The query terminates with an error message if the amount set by this option is less than the minimum required to run the query.

The run value for this option doesn't exceed the actual amount of memory that can be used for the operating system and hardware platform on which [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is running.

## Recommendations

This option is an advanced option and should be changed only by an experienced database administrator or certified [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] professional.

The `index create memory` option is self-configuring and usually works without requiring adjustment. However, if you experience difficulties creating indexes, consider increasing the value of this option from its run value.

Creating an index on a production system is usually an infrequently performed task, often scheduled as a job to execute during off-peak time. Therefore, when creating indexes infrequently and during off-peak time, increasing the `index create memory` can improve the performance of index creation. Keep the [min memory per query](configure-the-min-memory-per-query-server-configuration-option.md) configuration option at a lower number, however, so the index creation job still starts even if all the requested memory isn't available.

## Permissions

Execute permissions on `sp_configure` with no parameters or with only the first parameter are granted to all users by default. To execute `sp_configure` with both parameters to change a configuration option or to run the `RECONFIGURE` statement, a user must be granted the `ALTER SETTINGS` server-level permission. The `ALTER SETTINGS` permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.

<a id="SSMSProcedure"></a>

## Use SQL Server Management Studio

1. In Object Explorer, right-click a server and select **Properties**.

1. Select the **Memory** node.

1. Under **Index creation memory**, type or select the desired value for the index create memory option.

   Use the `index create memory` option to control the amount of memory used by index creation sorts. The `index create memory` option is self-configuring and should work in most cases without requiring adjustment. However, if you experience difficulties creating indexes, consider increasing the value of this option from its run value. Query sorts are controlled through the `min memory per query` option.

<a id="TsqlProcedure"></a>

## Use Transact-SQL

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to set the value of the `index create memory` option to `4096`.

   ```sql
   USE master;
   GO

   EXECUTE sp_configure 'show advanced options', 1;
   GO

   RECONFIGURE;
   GO

   EXECUTE sp_configure 'index create memory', 4096;
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

## Follow up: After you configure the index create memory option

The setting takes effect immediately without restarting the server.

## Related content

- [sys.configurations (Transact-SQL)](../../relational-databases/system-catalog-views/sys-configurations-transact-sql.md)
- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Server memory configuration options](server-memory-server-configuration-options.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
