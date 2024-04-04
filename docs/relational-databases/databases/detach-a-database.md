---
title: "Detach a database"
description: Learn how to detach a database in SQL Server by using SQL Server Management Studio or Transact-SQL. Files can be reattached or attached to another server.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 03/30/2024
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.detachdatabase.f1"
helpviewer_keywords:
  - "database detaching [SQL Server]"
  - "detaching databases [SQL Server]"
---
# Detach a database

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to detach a database in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] with [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)]. The detached files aren't deleted and remain in the file system. The files can be reattached by using `CREATE DATABASE ... FOR ATTACH` or `FOR ATTACH_REBUILD_LOG` option. The files can also be moved to another server and attached to an instance with the same or newer version.

## Limitations

For a list of limitations and restrictions, see [Database detach and attach (SQL Server)](database-detach-and-attach-sql-server.md).

## Permissions

Requires membership in the **db_owner** fixed database role.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

### Before moving a database

If you're moving a database, before you detach it from its existing SQL Server instance, use the **Database properties** page to review the files associated with the database and their current locations.

1. In [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Object Explorer, connect to the instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] and then expand the instance.

1. Expand **Databases**, and select the name of the user database you want to detach.

1. Right-click the database name, select **Properties**. Select the **Files** page and review the entries in the **Database files:** table.

Be sure to account for all files associated with the database before you detach, move, and attach. Then, proceed with the detach steps in the next section. For more information on attaching the database in its new location, see [Attach a Database](attach-a-database.md).

### Detach a database

1. In [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Object Explorer, connect to the instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] and then expand the instance.

1. Expand **Databases**, and select the name of the user database you want to detach.

1. Right-click the database name, point to **Tasks**, and then select **Detach**. The **Detach Database** dialog box appears.

   - **Databases to detach**: Lists the databases to detach.

   - **Database Name**: Displays the name of the database to be detached.

   - **Drop Connections**: Disconnect connections to the specified database.

     > [!NOTE]  
     > You can't detach a database with active connections.

   - **Update Statistics**: By default, the detach operation retains any out-of-date optimization statistics when detaching the database; to update the existing optimization statistics, select this check box.

   - **Keep Full-Text Catalogs**: By default, the detach operation keeps any full-text catalogs that are associated with the database. To remove them, clear the **Keep Full-Text Catalogs** check box. This option appears only when you're upgrading a database from [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)].

   - **Status**: Displays one of the following states: **Ready** or **Not ready**.

   - **Message**: The **Message** column might display information about the database, as follows:

     - When a database is involved with replication, the **Status** is **Not ready** and the **Message** column displays **Database replicated**.

     - When a database has one or more active connections, the **Status** is **Not ready** and the **Message** column displays _<number_of_active_connections>_**Active connection(s)**. For example: **1 Active connection(s)**. Before you can detach the database, you must disconnect any active connections by selecting **Drop Connections**.

     To obtain more information about a message, select the hyperlinked text to open Activity Monitor.

1. When you're ready to detach the database, select **OK**.

> [!NOTE]  
> The newly detached database will remain visible in the **Databases** node of Object Explorer until the view is refreshed. You can refresh the view at any time: Select in the Object Explorer pane, and from the menu bar select **View** and then **Refresh**.

## <a id="TsqlProcedure"></a> Use Transact-SQL

### Before moving a database

If you're moving a database, before detaching it from its existing SQL Server instance, use the `sys.database_files` system catalog view to review the files associated with the database and their current locations. For more information, see [sys.database_files (Transact-SQL)](../system-catalog-views/sys-database-files-transact-sql.md).

1. In [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], select **New Query** to open the Query Editor.

1. Copy the following [!INCLUDE [tsql](../../includes/tsql-md.md)] script into the Query Editor, and then select **Execute**. This script displays the location of the physical database files. Be sure to account for all files when moving the database via detach/attach.

   ```sql
   USE [database_name]
   GO

   SELECT type_desc, name, physical_name
   FROM sys.database_files;
   ```

Be sure to account for all files associated with the database before you detach, move, and attach. Then, proceed with the detach steps in the next section. For more information on attaching the database in its new location, see [Attach a Database](attach-a-database.md).

### Detach a database

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example detaches the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database with the `skipchecks` option set to `true`. For more information, see [sp_detach_db](../system-stored-procedures/sp-detach-db-transact-sql.md).

   ```sql
   EXEC sp_detach_db 'AdventureWorks2022', 'true';
   ```

## Related content

- [Database detach and attach (SQL Server)](database-detach-and-attach-sql-server.md)
- [sp_detach_db (Transact-SQL)](../system-stored-procedures/sp-detach-db-transact-sql.md)
- [Attach a Database](attach-a-database.md)
