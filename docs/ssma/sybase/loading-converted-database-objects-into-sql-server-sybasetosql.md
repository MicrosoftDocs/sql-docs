---
description: "Loading Converted Database Objects into SQL Server (SybaseToSQL)"
title: "Loading Converted Database Objects into SQL Server (SybaseToSQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Loading Converted Database Objects"
ms.assetid: 4c59256f-99a8-4351-9559-a455813dbd06
author: cpichuka 
ms.author: cpichuka 
f1_keywords: 
    - "ssma.sybase.synchronizecommittarget.f1"
---
# Loading Converted Database Objects into SQL Server (SybaseToSQL)
After you have converted Sybase Adaptive Server Enterprise (ASE) database objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, you can load the resulting database objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure. You can either have SSMA create the objects, or you can script the objects and run the scripts yourself. Also, SSMA lets you update target metadata with the actual contents of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database.  
  
## Choosing Between Synchronization and Scripts  
If you want to load the converted database objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure without modification, you can have SSMA directly create or re-create the database objects. This method is quick and easy, but does not allow for customization of the [!INCLUDE[tsql](../../includes/tsql-md.md)] code that defines the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure objects, other than stored procedures.  
  
If you want to modify the [!INCLUDE[tsql](../../includes/tsql-md.md)] that is used to create the objects in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, or if you want more control over when and how the objects are created in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, use SSMA to create [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts. You can then modify those scripts, create each object individually, and even use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure Agent to schedule creating those objects.  
  
## Using SSMA to Load Objects into SQL Server or SQL Azure  
To use SSMA to create [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database objects, you select the objects in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure Metadata Explorer, and then synchronize the objects with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, as shown in the following procedure. By default, if the objects already exist in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, and if the SSMA metadata has some local changes or updates to the definition of those very objects, then SSMA will alter the object definitions in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure. You can change the default behavior by editing **Project Settings**.  
  
> [!NOTE]  
> You can select existing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database objects that were not converted from ASE databases. However, those objects will not be re-created or altered by SSMA.  
  
**To synchronize objects with SQL Server or SQL Azure**  
  
1.  In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure Metadata Explorer, expand the top [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure node, and then expand **Databases**.  
  
2.  Select the objects to process:  
  
    -   To synchronize a complete database, select the check box next to the database name.  
  
    -   To synchronize or omit individual objects or categories of objects, select or clear the check box next to the object or folder.  
  
3.  After you have selected the objects to process in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure Metadata Explorer, right-click **Databases**, and then click **Synchronize with Database**.  
  
    You can also synchronize individual objects or categories of objects by right-clicking the object or its parent folder, and then clicking  **Synchronize with Database**.  
  
    After that, SSMA will display the **Synchronize with Database** dialog, where you can see two groups of items. On the left side, SSMA shows selected database objects represented in a tree. On the right side, you can see a tree representing the same objects in SSMA metadata. You can expand the tree by clicking on the right or left '+' button. The direction of the synchronization is shown in the Action column placed between the two trees.  
  
    An action sign can be in three states:  
  
    -   A left arrow means the contents of metadata will be saved in the database (the default).  
  
    -   A right arrow means database contents will overwrite the SSMA metadata.  
  
    -   A cross sign means no action will be taken.  
  
Click on the action sign to change the state. Actual synchronization will be performed when you click **OK** button of the **Synchronize with Database** dialog.  
  
## Scripting Objects  
If you want to save [!INCLUDE[tsql](../../includes/tsql-md.md)] definitions of the converted database objects, or you want to alter the object definitions and run scripts yourself, you can save the converted database object definitions to [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts.  
  
**To save objects as scripts**  
  
1.  After you have selected the objects to save to a script, right-click **Databases**, and then select **Save as Script**.  
  
    You can also script individual objects or categories of objects by right-clicking the object or its containing folder, and then selecting **Save Script**.  
  
2.  In the **Save As** dialog box, locate the folder where you want to save the script, enter a file name in the **File name** box, and then click **OK**.  
  
    SSMA will append the .sql file name extension.  
  
### Modifying Scripts  
After you have saved the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure object definitions as one or more scripts, you can use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to view and modify the scripts.  
  
**To modify a script**  
  
1.  On the [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] **File** menu, point to **Open**, and then click **File**.  
  
2.  In the **Open** dialog box, navigate to and select your script file, and then click **OK**.  
  
3.  Edit and the script file by using the query editor.  
  
    For more information about the query editor, see "Editor Convenience Commands and Features" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
4.  To save the script, on the File menu, select **Save**.  
  
### Running Scripts  
You can run a script, or individual statements, in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
**To run a script**  
  
1.  On the [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] **File** menu, point to **Open**, and then click **File**.  
  
2.  In the **Open** dialog box, navigate to and select your script file, and then click **OK**.  
  
3.  To run the complete script, press the **F5** key.  
  
4.  To run a set of statements, select the statements in the query editor window, and then press the **F5** key.  
  
For more information about how to use the query editor to run scripts, see " [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] [!INCLUDE[tsql](../../includes/tsql-md.md)] Query" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
You can also run scripts from the command line by using the **sqlcmd** utility, and from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent. For more information about **sqlcmd**, see "sqlcmd Utility" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online. For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent, see "Automating Administrative Tasks ( [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent) " in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## Securing Objects in SQL Server  
After you have loaded the converted database objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can grant and deny permissions on those objects. It is a good idea to do this before migrating data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For information about how to help secure objects in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see "Security Considerations for Databases and Database Applications" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## Next Step  
The next step in the migration process is to [Migrating Sybase ASE Data into SQL Server / SQL Azure(SybaseToSQL)](./migrating-sybase-ase-data-into-sql-server-azure-sql-db-sybasetosql.md).  
  
## See Also  
[Migrating Sybase ASE Databases to SQL Server - Azure SQL Database &#40;SybaseToSQL&#41;](../../ssma/sybase/migrating-sybase-ase-databases-to-sql-server-azure-sql-db-sybasetosql.md)  
