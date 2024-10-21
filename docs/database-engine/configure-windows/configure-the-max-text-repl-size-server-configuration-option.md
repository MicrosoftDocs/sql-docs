---
title: "Server configuration: max text repl size"
description: Learn how to use the max text repl size option to limit the size of certain types of data that SQL Server adds to replicated or captured columns.
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/18/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "max text repl size option"
---
# Server configuration: max text repl size

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes how to configure the `max text repl size` server configuration option in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)]. The `max text repl size` option specifies the maximum size (in bytes) of **text**, **ntext**, **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **xml**, and **image** data that can be added to a replicated column or captured column in a single `INSERT`, `UPDATE`, `WRITETEXT`, or `UPDATETEXT` statement. The default value is 65,536 bytes. A value of `-1` indicates that there's no size limit, other than the limit imposed by the data type.

## Limitations

This option applies to transactional replication and Change Data Capture (CDC). When a server is configured for both transactional replication and CDC, the specified value applies to both features. This option is ignored by both snapshot replication and merge replication.

## Permissions

Execute permissions on `sp_configure` with no parameters or with only the first parameter are granted to all users by default. To execute `sp_configure` with both parameters to change a configuration option or to run the `RECONFIGURE` statement, a user must be granted the `ALTER SETTINGS` server-level permission. The `ALTER SETTINGS` permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.

<a id="SSMSProcedure"></a>

## Use SQL Server Management Studio

1. In Object Explorer, right-click a server and select **Properties**.

1. Select the **Advanced** node.

1. Under **Miscellaneous**, change the **Max Text Replication Size** option to the desired value.

<a id="TsqlProcedure"></a>

## Use Transact-SQL

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to configure the `max text repl size` option to `-1`.

   ```sql
   USE master;
   GO

   EXECUTE sp_configure 'show advanced options', 1;

   RECONFIGURE;
   GO

   EXECUTE sp_configure 'max text repl size', -1;
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

## Follow up: After you configure the max text repl size option

The setting takes effect immediately without restarting the server.

## Related content

- [SQL Server Replication](../../relational-databases/replication/sql-server-replication.md)
- [INSERT (Transact-SQL)](../../t-sql/statements/insert-transact-sql.md)
- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [UPDATE (Transact-SQL)](../../t-sql/queries/update-transact-sql.md)
- [UPDATETEXT (Transact-SQL)](../../t-sql/queries/updatetext-transact-sql.md)
- [WRITETEXT (Transact-SQL)](../../t-sql/queries/writetext-transact-sql.md)
