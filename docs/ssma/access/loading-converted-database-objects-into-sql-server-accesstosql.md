---
title: "Loading Converted Database Objects into SQL Server (AccessToSQL)"
description: "Loading Converted Database Objects into SQL Server (AccessToSQL)"
author: nilabjaball
ms.author: niball
ms.reviewer: vanto
ms.date: 12/30/2025
ms.service: sql
ms.subservice: ssma
ms.topic: concept-article
ms.collection:
  - sql-migration-content
f1_keywords:
  - "ssma.access.synchronizecommittarget.f1"
helpviewer_keywords:
  - "Access databases, loading converted objects into Azure SQL"
  - "Access databases, loading converted objects into SQL Server"
  - "data, securing"
  - "loading objects into Azure SQL"
  - "loading objects into SQL Server"
  - "migrating objects into Azure SQL"
  - "migrating objects into SQL Server"
  - "moving objects into Azure SQL"
  - "moving objects into SQL Server"
  - "schemas, loading into Azure SQL"
  - "schemas, loading into SQL Server"
  - "scripting converted objects"
  - "securing data"
  - "Azure SQL, loading objects into"
  - "SQL Server, loading objects into"
  - "synchronizing metadata with Azure SQL"
  - "synchronizing metadata with SQL Server"
  - "uploading objects into Azure SQL"
  - "uploading objects into SQL Server"
---
# Load converted database objects into SQL Server (AccessToSQL)

After you use SQL Server Migration Assistant (SSMA) to convert Access database objects to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL, you can load the resulting database objects into the target. You can either have SSMA create the objects, or you can script the objects and run the scripts yourself. Also, SSMA lets you update target metadata with the actual contents of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database.

## Choose between synchronization and scripts

If you want to load the converted database objects into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL without modification, you can have SSMA directly create or recreate the database objects. This method is quick and easy, but it doesn't allow for customization of the [!INCLUDE [tsql](../../includes/tsql-md.md)] code that defines the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL objects, other than stored procedures.

If you want to modify the [!INCLUDE [tsql](../../includes/tsql-md.md)] that is used to create objects, or if you want more control over object creation, use SSMA to create scripts. You can then modify those scripts, create each object individually, and even use [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent to schedule creating those objects.

## Use SSMA to synchronize objects with SQL Server

To use SSMA to create [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database objects, select the objects in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Metadata Explorer, and then synchronize the objects with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL, as shown in the following procedure. By default, if the objects already exist in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL, and if the SSMA metadata has some local changes or updates to the definition of those objects, then SSMA alters the object definitions in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL. You can change the default behavior by editing **Project Settings**.

> [!NOTE]  
> You can select existing [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database objects that weren't converted from Access databases. However, SSMA doesn't re-create or alter those objects.

1. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Metadata Explorer, expand the top [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL node, and then expand **Databases**.

1. Select the objects to process:

   - To synchronize a complete database, select the check box next to the database name.

   - To synchronize or omit individual objects or categories of objects, select or clear the check box next to the object or folder.

1. After you select the objects to process in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Metadata Explorer, right-click **Databases**, and then select **Synchronize with Database**.

   You can also synchronize individual objects or categories of objects by right-clicking the object or its parent folder, and then selecting **Synchronize with Database**.

   After that, SSMA displays the **Synchronize with Database** dialog, where you can see two groups of items. On the left side, SSMA shows selected database objects represented in a tree. On the right side, you can see a tree representing the same objects in SSMA metadata. You can expand the tree by selecting the right or left '+' button. The direction of the synchronization is shown in the Action column placed between the two trees.

   An action sign can be in three states:

   - A left arrow means the contents of metadata are saved in the database (the default).

   - A right arrow means database contents overwrite the SSMA metadata.

   - A cross sign means no action is taken.

   Select the action sign to change the state. Actual synchronization is performed when you select **OK** button of the **Synchronize with Database** dialog.

## Script objects

If you want to save [!INCLUDE [tsql](../../includes/tsql-md.md)] definitions of the converted database objects, or if you want to alter the object definitions and run scripts yourself, save the converted database object definitions to [!INCLUDE [tsql](../../includes/tsql-md.md)] scripts.

1. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Metadata Explorer, expand the top node (the server name) and then expand **Databases**.

1. Perform one or more of the following actions:

   - To script a complete database, select the check box next to the database name.

   - To script or omit individual views, expand the database, expand **Views**, and then select or clear the check box next to the view.

   - To script or omit individual tables, expand the database, expand **Tables**, and then select or clear the check box next to the table.

   - To script or omit individual indexes for a table, expand the table, expand **Indexes**, and then select or clear the index.

1. Right-click **Databases** and select **Save as Script**.

   You can also script individual objects. To script an object, right-click the object and select **Save as Script**.

1. In the **Save As** dialog box, locate the folder where you want to save the script, enter a file name in the **File name** box, and then select **OK**.

   SSMA appends the `.sql` file name extension.

### Modify scripts

After you save the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL object definitions as a script, use [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to modify the script.

1. On the [!INCLUDE [ssManStudio](../../includes/ssmanstudio-md.md)] **File** menu, point to **Open**, and then select **File**.

1. In the **Open** dialog box, locate and select your script file, and then select **OK**.

1. Edit the script file by using the query editor.

   For more information about the query editor, see [Configure Editors (SQL Server Management Studio)](/ssms/scripting/configure-editors-sql-server-management-studio).

1. To save the script, on the File menu, select **Save**.

### Run scripts

You can run a script, or individual statements, in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)].

1. On the [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] **File** menu, point to **Open** and then select **File**.

1. In the **Open** dialog box, locate and select your script file, and then select **OK**.

1. To run the complete script, press the **F5** key.

1. To run a set of statements, select the statements in the query editor window, and then press the **F5** key.

For more information about how to use the query editor to run scripts, see [Quickstart: Connect and query a SQL Server instance using SQL Server Management Studio (SSMS)](/ssms/quickstarts/ssms-connect-query-sql-server).

You can also run scripts from the command line by using the **sqlcmd** utility, and from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent. For more information, see [sqlcmd utility](../../tools/sqlcmd/sqlcmd-utility.md). For more information about [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent, see [Automated Administration Tasks (SQL Server Agent)](/ssms/agent/automated-administration-tasks-sql-server-agent).

## Secure objects in SQL Server

After you load the converted database objects into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], grant or deny permissions on those objects. It's a good idea to set permissions before migrating data to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For information about how to help secure objects in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [SQL Server security best practices](../../relational-databases/security/sql-server-security-best-practices.md).

## Related content

- [Migrate Access databases to SQL Server and Azure SQL](migrating-access-databases-to-sql-server-azure-sql-db-accesstosql.md)
- [Migrate Access data into SQL Server and Azure SQL](migrating-access-data-into-sql-server-azure-sql-db-accesstosql.md)
