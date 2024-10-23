---
title: "Load converted database objects into SQL Server (Db2ToSQL)"
description: Learn how to load converted database objects into SQL Server with SSMA for Db2
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 09/24/2024
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
---
# Load converted database objects into SQL Server (Db2ToSQL)

After you have converted Db2 schemas to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you can load the resulting database objects into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. You can either have SQL Server Migration Assistant (SSMA) create the objects, or you can script the objects and run the scripts yourself. Also, SSMA lets you update target metadata with the actual contents of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database.

## Choose between synchronization and scripts

If you want to load the converted database objects into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] without modification, you can have SSMA directly create or recreate the database objects. That method is quick and easy, but doesn't allow for customization of the [!INCLUDE [tsql](../../includes/tsql-md.md)] code that defines the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] objects, other than stored procedures.

If you want to modify the [!INCLUDE [tsql](../../includes/tsql-md.md)] that is used to create objects, or if you want more control over objects creation, use SSMA to create scripts. You can then modify those scripts, create each object individually, and even use [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent to schedule creating those objects.

## Use SSMA to synchronize objects with SQL Server

To use SSMA to create [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database objects, you select the objects in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Metadata Explorer, and then synchronize the objects with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], as shown in the following procedure. By default, if the objects already exist in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], and if the SSMA metadata is newer than the object in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], SSMA alters the object definitions in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. You can change the default behavior by editing **Project Settings**.

> [!NOTE]  
> You can select existing [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database objects that weren't converted from Db2 databases. However, those objects will not be recreated or altered by SSMA.

1. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Metadata Explorer, expand the top [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] node, and then expand **Databases**.

1. Select the objects to process:

   - To synchronize a complete database, select the check box next to the database name.

   - To synchronize or omit individual objects or categories of objects, select or clear the check box next to the object or folder.

1. After you have selected the objects to process in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Metadata Explorer, right-click **Databases**, and then select **Synchronize with Database**.

   You can also synchronize individual objects or categories of objects by right-clicking the object or its parent folder, and then selecting **Synchronize with Database**.

   After that, SSMA will display the **Synchronize with Database** dialog, where you can see two groups of items. On the left side, SSMA shows selected database objects represented in a tree. On the right side, you can see a tree representing the same objects in SSMA metadata. You can expand the tree by selecting on the right or left '+' button. The direction of the synchronization is shown in the Action column placed between the two trees.

   An action sign can be in three states:

   - A left arrow means the contents of metadata are saved in the database (the default).

   - A right arrow means database contents overwrite the SSMA metadata.

   - A cross sign means no action is taken.

Select the action sign to change the state. Actual synchronization is performed when you select **OK** button of the **Synchronize with Database** dialog.

## Script objects

To save [!INCLUDE [tsql](../../includes/tsql-md.md)] definitions of the converted database objects, or to alter the object definitions and run scripts yourself, you can save the converted database object definitions to [!INCLUDE [tsql](../../includes/tsql-md.md)] scripts.

1. After you have selected the objects to save to a script, right-click **Databases**, and then select **Save as Script**.

   You can also script individual objects or categories of objects by right-clicking the object or its parent folder, and then selecting **Save as Script**.

1. In the **Save As** dialog box, locate the folder where you want to save the script, enter a file name in the **File name** box, and then select **OK**. SSMA appends the .sql file name extension.

### Modify scripts

After you save the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] object definitions as one or more scripts, you can use [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to view and modify the scripts.

1. In [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], navigate to **File** > **Open** > **File**.

1. In the **Open** dialog box, select your script file, and then select **OK**.

1. Edit the script file by using the query editor.

   For more information about the query editor, see "Editor Convenience Commands and Features" in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.

1. To save the script, select **File** > **Save**.

### Run scripts

You can run a script, or individual statements, in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)].

1. In [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], navigate to **File** > **Open** > **File**.

1. In the **Open** dialog box, select your script file, and then select **OK**.

1. To run the complete script, press the **F5** key.

1. To run a set of statements, select the statements in the query editor window, and then press the **F5** key.

For more information about how to use the query editor to run scripts, see " [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] [!INCLUDE [tsql](../../includes/tsql-md.md)] Query" in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.

You can also run scripts from the command line by using the **sqlcmd** utility, and from the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent. For more information about **sqlcmd**, see "sqlcmd Utility" in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Books Online. For more information about [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent, see "Automating Administrative Tasks ([!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent)" in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.

## Secure objects in SQL Server

After you load the converted database objects into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you can grant and deny permissions on those objects. It's a good idea to do this step before migrating data to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For information about how to help secure objects in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see "Security Considerations for Databases and Database Applications" in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.

## Related content

- [Migrate Db2 Data into SQL Server (Db2ToSQL)](migrating-db2-data-into-sql-server-db2tosql.md)
