---
title: "Loading Converted Database Objects into SQL Server (OracleToSQL)"
description: Learn how to load the database objects you converted from Oracle into the SQL Server instance by using SSMA for Oracle.
author: nilabjaball
ms.author: niball
ms.reviewer: randolphwest
ms.date: 06/03/2025
ms.service: sql
ms.subservice: ssma
ms.topic: how-to
ms.collection:
  - sql-migration-content
f1_keywords:
  - "ssma.oracle.synccommittarget.f1"
helpviewer_keywords:
  - "Synchronization, Securing Objects in SQL Server"
  - "Synchronization,Scripting Objects"
---

# Load converted database objects into SQL Server (OracleToSQL)

After you convert Oracle schemas to SQL Server, you can load the resulting database objects into SQL Server. Microsoft SQL Server Migration Assistant (SSMA) for Oracle can create the objects, or you can script the objects and run the scripts yourself. Also, you can use SSMA to update target metadata with the actual contents of the SQL Server database.

## Choose between synchronization and scripts

If you want to load the converted database objects into SQL Server without modification, SSMA can directly create or re-create the database objects. That method is quick and easy, but doesn't allow for customization of the [!INCLUDE [tsql](../../includes/tsql-md.md)] code that defines the SQL Server objects, other than stored procedures.

If you want to modify the [!INCLUDE [tsql](../../includes/tsql-md.md)] that is used to create objects, or if you want more control over object creation, use SSMA to create scripts. You can then modify those scripts, create each object individually, and even use SQL Server Agent to schedule the creation of those objects.

## Use SSMA to synchronize objects with SQL Server

To use SSMA to create SQL Server database objects, you select the objects in SQL Server Metadata Explorer, and then synchronize the objects with SQL Server, as shown in the following procedure. By default, if the objects already exist in SQL Server, and if the SSMA metadata is newer than the object in SQL Server, SSMA alters the object definitions in SQL Server. You can change the default behavior by editing **Project Settings**.

> [!NOTE]  
> You can select existing SQL Server database objects that weren't converted from Oracle databases. However, those objects aren't re-created or altered by SSMA.

1. In SQL Server Metadata Explorer, expand the top SQL Server node, and then expand **Databases**.

1. Select the objects to process:

   - To synchronize a complete database, select the checkbox next to the database name.

   - To synchronize or omit individual objects or categories of objects, select or clear the checkbox next to the object or folder.

1. After you select the objects to process in SQL Server Metadata Explorer, right-click **Databases**, and then select **Synchronize with Database**.

   You can also synchronize individual objects or categories of objects. Right-click the object or its parent folder, and then select **Synchronize with Database**.

   After that, SSMA will display the **Synchronize with Database** dialog, where you can see two groups of items. On the left side, SSMA shows selected database objects represented in a tree. On the right side, you can see a tree representing the same objects in SSMA metadata. You can expand the tree by selecting the right or left **+** button. The direction of the synchronization is shown in the **Action** column located between the two trees.

   An action sign can be in three states:

   - A left arrow means metadata contents are saved in the database (the default).

   - A right arrow means database contents overwrite the SSMA metadata.

   - A cross sign means no action is taken.

To change the state, select the action sign. Actual synchronization is performed when you select the **OK** button in the **Synchronize with Database** dialog.

## Script objects

To save [!INCLUDE [tsql](../../includes/tsql-md.md)] definitions of the converted database objects, or to alter the object definitions and run scripts yourself, you can save the converted database object definitions to [!INCLUDE [tsql](../../includes/tsql-md.md)] scripts. Follow these instructions:

1. After you select the objects to save to a script, right-click **Databases**, and then select **Save as Script**.

   You can also script individual objects or categories of objects. Right-click the object or its parent folder, and then select **Save as Script**.

1. In the **Save As** dialog, locate the folder where you want to save the script, enter a file name in the **File name** box, and then select **OK**. SSMA appends the .sql file name extension.

### Modify scripts

After you save the SQL Server object definitions as one or more scripts, you can use [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to view and modify the scripts. Follow these instructions:

1. On the [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] **File** menu, point to **Open**, and then select **File**.

1. In the **Open** dialog, select your script file, and then select **OK**.

1. Edit the script file by using the query editor.

1. To save the script, select **Save** on the **File** menu.

### Run scripts

You can run a script, or individual statements, in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. Follow these instructions:

1. On the [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] **File** menu, point to **Open**, and then select **File**.

1. In the **Open** dialog, select your script file, and then select **OK**.

1. To run the complete script, select the **F5** key.

1. To run a set of statements, select the statements in the query editor window, and then press the **F5** key.

For more information about how to use the query editor to run scripts, see "[!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] [!INCLUDE [tsql](../../includes/tsql-md.md)] Query" in SQL Server Books Online.

You can also run scripts from the command line by using the **sqlcmd** utility, and from the SQL Server Agent. For more information about **sqlcmd**, see "sqlcmd Utility" in SQL Server Books Online. For more information about SQL Server Agent, see "Automating Administrative Tasks (SQL Server Agent)" in SQL Server Books Online.

## Secure objects in SQL Server

After you load the converted database objects into SQL Server, you can grant and deny permissions on those objects. It's a good idea to take this action before migrating data to SQL Server. For information about how to help secure objects in SQL Server, see "Security Considerations for Databases and Database Applications" in SQL Server Books Online.

## Next step

> [!div class="nextstepaction"]
> [Migrate Oracle Data into SQL Server](migrating-oracle-data-into-sql-server-oracletosql.md)
