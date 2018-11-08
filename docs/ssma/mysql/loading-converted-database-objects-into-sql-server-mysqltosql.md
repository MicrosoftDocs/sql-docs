---
title: "Loading Converted Database Objects into SQL Server (MySQLToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: ac993a6d-0283-4823-8793-6b217677dfa3
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Loading Converted Database Objects into SQL Server (MySQLToSQL)
After you have converted MySQL Databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, you can load the resulting database objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure. You can either have SSMA create the objects, or you can script the objects and run the scripts yourself. Also, SSMA lets you update target metadata with the actual contents of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure database.  
  
## Choosing Between Synchronization and Scripts  
If you want to load the converted database objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure without modification, you can have SSMA directly create or recreate the database objects. That method is quick and easy, but does not allow for customization of the Transact-SQL code that defines the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure objects.  
  
If you want to modify the Transact-SQL that is used to create objects, or if you want more control over objects creation, use SSMA to create scripts. You can then modify these scripts, create each object individually, and even use SQL Server Agent to schedule creating these objects.  
  
## Using SSMA to Synchronize Objects with SQL Server  
To use SSMA to create SQL Server or SQL Azure database objects, you select the objects in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure Metadata Explorer, and then synchronize the objects with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, as shown in the following procedure. By default, if the objects already exist in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, and if the SSMA metadata has some local changes or updates to the definition of those very objects, then SSMA will alter the object definitions in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure. You can change the default behavior by editing **Project Settings**.  
  
> [!NOTE]  
> You can select existing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure database objects that were not converted from MySQL databases. However, these objects will not be recreated or altered by SSMA.  
  
##### To synchronize objects with SQL Server or SQL Azure  
  
1.  In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure Metadata Explorer, expand the top [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure node, and then expand **Databases**.  
  
2.  Select the objects to process:  
  
    -   To synchronize a complete database, select the check box next to the database name.  
  
    -   To synchronize or omit individual objects or categories of objects, select or clear the check box next to the object or folder.  
  
3.  After you have selected the objects to process in SQL Server or SQL Azure Metadata Explorer, right-click **Databases**, and then click **Synchronize with Database**.  
  
    You can also synchronize individual objects or categories of objects by right-clicking the object or its parent folder, and then clicking **Synchronize with Database**.  
  
    After that, SSMA will display the **Synchronize with Database** dialog, where you can see two groups of items. On the left side, SSMA shows selected database objects represented in a tree. On the right side, you can see a tree representing the same objects in SSMA metadata. You can expand the tree by clicking on the right or left '+' button. The direction of the synchronization is shown in the Action column placed between the two trees.  
  
    An action sign can be in the following three states:  
  
    -   A left arrow means the contents of metadata will be saved in the database (the default).  
  
    -   A right arrow means database contents will overwrite the SSMA metadata.  
  
    -   A cross sign means no action will be taken.  
  
    -   Click on the action sign to change the state. Actual synchronization will be performed when you click **OK** button of the **Synchronize with Database** dialog.  
  
## Scripting Objects  
To save [!INCLUDE[tsql](../../includes/tsql-md.md)] definitions of the converted database objects, or to alter the object definitions and run scripts yourself, you can save the converted database object definitions to [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts.  
  
**To save objects as scripts**  
  
1.  After you have selected the objects to save to a script, right-click **Databases**, and then click **Save as Script**.  
  
    You can also script individual objects or categories of objects by right-clicking the object or its parent folder, and then clicking **Save as Script**.  
  
2.  In the **Save As** dialog box, locate the folder where you want to save the script, enter a file name in the **File name** box, and then [!INCLUDE[clickOK](../../includes/clickok-md.md)] SSMA will append the .sql file name extension.  
  
### Modifying Scripts  
After you have saved the SQL Server or SQL Azure object definitions as a script, you can use SQL Server Management Studio to modify the script.  
  
**To modify a script**  
  
1.  On the Management Studio **File** menu, point to **Open**, and then click **File**.  
  
2.  In the Open dialog box, locate and select your script file, and then click **OK**.  
  
3.  Edit the script file by using the query editor.For more information about the query editor, see "Editor Convenience Commands and Features" in SQL Server Books Online.  
  
4.  To save the script, on the File menu, select **Save**.  
  
### Running Scripts  
You can run a script, or individual statements, in SQL Server Management Studio.  
  
**To run a script**  
  
1.  On the SQL Server Management Studio **File** menu, point to **Open** and then click **File**.  
  
2.  In the Open dialog box, locate and select your script file, and then click **OK**.  
  
3.  To run the complete script, press the **F5** key.  
  
4.  To run a set of statements, select the statements in the query editor window, and then press the **F5** key.  
  
5.  For more information about how to use the query editor to run scripts, see "SQL Server Management Studio Transact-SQL Query" in SQL Server Books Online.  
  
6.  You can also run scripts from the command line by using the **sqlcmd** utility, and from SQL Server Agent. For more information about **sqlcmd**, see "sqlcmd Utility" in SQL Server Books Online. For more information about SQL Server Agent, see "Automating Administrative Tasks (SQL Server Agent)" in SQL Server Books Online.  
  
## Securing Objects in SQL Server  
After you have loaded the converted database objects into SQL Server, you can grant and deny permissions on these objects. It is a good idea to do this before migrating data to SQL Server. For information about how to help secure objects in SQL Server, see "Security Considerations for Databases and Database Applications" in SQL Server Books Online.  
  
## Next Step  
The next step in the migration process is [Migrating MySQL Data into SQL Server - Azure SQL DB &#40;MySQLToSQL&#41;](../../ssma/mysql/migrating-mysql-data-into-sql-server-azure-sql-db-mysqltosql.md)  
  
## See Also  
[Migrating MySQL Databases to SQL Server - Azure SQL DB &#40;MySQLToSql&#41;](../../ssma/mysql/migrating-mysql-databases-to-sql-server-azure-sql-db-mysqltosql.md)  
  
