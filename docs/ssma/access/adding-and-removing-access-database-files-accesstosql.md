---
title: "Adding and Removing Access Database Files (AccessToSQL) | Microsoft Docs"
description: Learn how to add or remove Access databases to or from the SSMA project to migrate Access data to SQL Server or Azure SQL Database.
ms.service: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Access databases"
  - "adding Access files"
  - "adding and removing Access databases"
  - "browsing Access metadata"
  - "browsing metadata, Access databases"
  - "connecting, Access databases"
  - "databases"
  - "files, adding and removing"
  - "finding Access databases"
  - "finding database files"
  - "locating database files"
  - "metadata"
  - "metadata, browsing"
  - "metadata, refreshing"
  - "refreshing metadata"
  - "removing Access files"
  - "scanning for database files"
  - "searching for database files"
ms.assetid: e944c740-4c8a-4bc1-b0ed-be57bc06dced
author: cpichuka 
ms.author: cpichuka 
---
# Adding and Removing Access Database Files (AccessToSQL)
To migrate Access data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, you must add one or more Access databases to the SSMA project. These databases must be Access 97 or later versions. If you have databases from an earlier version of Access, you must convert the databases to a newer version. You do this by opening and saving the databases in Access 97 or a later version before you add them to SSMA.  
  
## What Happens When You Add Access Database Files?  
When you add an Access database to an SSMA project, SSMA reads database metadata, and then adds this metadata to the project file. This metadata describes the database and its objects. SSMA uses the metadata when it converts objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure syntax, and when it migrates data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure. You can browse this metadata in Access Metadata Explorer and review properties of individual database objects.  
  
> [!NOTE]  
> An Access database can be split into multiple files: a back-end database that contains tables, and front-end databases that contain queries, forms, reports, macros, modules, and shortcuts. If you want to migrate a split database to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, add the front-end database to SSMA.  
  
## Permissions that are required by SSMA  
To migrate an Access database to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, the Users group and the Admin user must have Administer permissions. For information about how to migrate databases with workgroup protection, see [Preparing Access Databases for Migration](preparing-access-databases-for-migration-accesstosql.md).  
  
## Selecting Databases to Add  
If you want to add one or more databases to an SSMA project, and the files are all in one known location, you can add the files by using the following procedure.  
  
**To add individual database files**  
  
1.  On the **File** menu, click **Add Databases**.  
  
2.  In the **Open** dialog box, locate the folder that contains the database file or files.  
  
3.  Select the files that you want to add, and then click **Open**.  
  
## Finding Databases to Add  
If you want to add multiple Access databases from different folders to an SSMA project, or you want to add a single file but have to find the file, you can follow these steps to locate one of more files and add them to the project.  
  
**To find and add databases**  
  
1.  On the **File** menu, click **Find Databases**.  
  
2.  In the Find Databases Wizard, enter the name of the drive, file path, or the UNC path that you want to search. Alternatively, click **Browse** to locate the drive or network folder.  
  
3.  Click **Add** to add the location to the list.  
  
    Repeat the previous two steps to add more search locations.  
  
4.  Optionally, add search criteria to refine the list of databases that are returned.  
  
    > [!IMPORTANT]  
    > The **All or part of the file name** text box does not support wildcard characters.  
  
5.  Click **Scan**.  
  
    The Scan page appears. This shows the databases that have been found and the progress of the search. To stop the search, click **Stop**.  
  
6.  On the Select Files page, select the databases that you want to add to the project.  
  
    You can use the **Select All** and **Clear All** buttons at the top of the list to select or clear all databases. You can hold the CTRL key down to select multiple databases, or hold the SHIFT key down to select a range of databases.  
  
7.  Click **Next**.  
  
8.  On the Verify page, click **Finish**.  
  
## Browsing Access Metadata  
After you add an Access database to a project, the project metadata appears in Access Metadata Explorer. You can browse the hierarchy of databases and database objects in the explorer.  
  
**To browse metadata**  
  
1.  In Access Metadata Explorer, expand **access-metabase**, and then expand **Databases**.  
  
2.  Expand the database that you want to review, and then expand **Queries**.  
  
    Notice the list of queries. If you select a query, an **SQL** tab and a **Properties** tab appear in the right pane.  
  
3.  Expand **Tables** and then select a table.  
  
    Notice that four tabs appear: **Table**, **Type Mapping**, **Properties**, and **Data**.  
  
4.  Expand a table, expand **Keys**, and then select a key.  
  
    The key properties appear in the right pane.  
  
5.  Expand **Indexes**, and then select an index.  
  
    The index properties appear in the right pane.  
  
## Refreshing Databases  
If an Access database changes after you add its file, you can update metadata from the Access database.  
  
**To update Access metadata**  
  
-   In Access Metadata Explorer, right-click the database, and then select **Refresh from Database**.  
  
## Removing Databases  
You can remove an Access database from a project by following these steps.  
  
**To remove a database from a project**  
  
1.  In Access Metadata Explorer, expand **access-metabase**, and then expand **Databases**.  
  
2.  Right-click the database, and then select **Remove Database**.  
  
## Next Step  
The next step in the migration process is to [connect to SQL Server](../sybase/connecting-to-sql-server-sybasetosql.md).  
  
## See Also  
[Migrating Access Databases to SQL Server](migrating-access-databases-to-sql-server-azure-sql-db-accesstosql.md)  
[Creating and Managing Projects](creating-and-managing-projects-accesstosql.md)  
