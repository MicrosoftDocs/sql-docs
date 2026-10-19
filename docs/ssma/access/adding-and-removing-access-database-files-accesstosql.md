---
title: "Adding and Removing Access Database Files (AccessToSQL)"
description: Learn how to add or remove Access databases to or from the SSMA project to migrate Access data to SQL Server or Azure SQL.
author: nilabjaball
ms.author: niball
ms.reviewer: vanto
ms.date: 12/30/2025
ms.service: sql
ms.subservice: ssma
ms.topic: how-to
ms.collection:
  - sql-migration-content
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
---
# Add and remove Access database files (AccessToSQL)

To migrate Access data to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL, you must add one or more Access databases to the SSMA project. These databases must be Access 97 or later versions. If you have databases from an earlier version of Access, you must convert the databases to a newer version. You do this by opening and saving the databases in Access 97 or a later version before you add them to SSMA.

## What happens when you add Access database files?

When you add an Access database to an SSMA project, SSMA reads database metadata, and then adds this metadata to the project file. This metadata describes the database and its objects. SSMA uses the metadata when it converts objects to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL syntax, and when it migrates data to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL. You can browse this metadata in Access Metadata Explorer and review properties of individual database objects.

> [!NOTE]  
> An Access database can be split into multiple files: a back-end database that contains tables, and front-end databases that contain queries, forms, reports, macros, modules, and shortcuts. If you want to migrate a split database to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL, add the front-end database to SSMA.

## Permissions that are required by SSMA

To migrate an Access database to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL, the Users group and the Admin user must have Administer permissions. For information about how to migrate databases with workgroup protection, see [Prepare Access databases for migration](preparing-access-databases-for-migration-accesstosql.md).

## Select databases to add

If you want to add one or more databases to an SSMA project, and the files are all in one known location, you can add the files by using the following procedure.

1. On the **File** menu, select **Add Databases**.

1. In the **Open** dialog box, locate the folder that contains the database file or files.

1. Select the files that you want to add, and then select **Open**.

## Find databases to add

If you want to add multiple Access databases from different folders to an SSMA project, or you want to add a single file but have to find the file, you can follow these steps to locate one of more files and add them to the project.

1. On the **File** menu, select **Find Databases**.

1. In the Find Databases Wizard, enter the name of the drive, file path, or the UNC path that you want to search. Alternatively, select **Browse** to locate the drive or network folder.

1. Select **Add** to add the location to the list.

   Repeat the previous two steps to add more search locations.

1. Optionally, add search criteria to refine the list of databases that are returned.

   > [!IMPORTANT]  
   > The **All or part of the file name** text box doesn't support wildcard characters.

1. Select **Scan**.

   The Scan page appears. This shows the databases that have been found and the progress of the search. To stop the search, select **Stop**.

1. On the Select Files page, select the databases that you want to add to the project.

   You can use the **Select All** and **Clear All** buttons at the top of the list to select or clear all databases. You can hold the CTRL key down to select multiple databases, or hold the SHIFT key down to select a range of databases.

1. Select **Next**.

1. On the Verify page, select **Finish**.

## Browse Access metadata

After you add an Access database to a project, the project metadata appears in Access Metadata Explorer. You can browse the hierarchy of databases and database objects in the explorer.

1. In Access Metadata Explorer, expand **Access-metabase**, and then expand **Databases**.

1. Expand the database that you want to review, and then expand **Queries**.

   Notice the list of queries. If you select a query, a **SQL** tab and a **Properties** tab appear in the right pane.

1. Expand **Tables** and then select a table.

   Four tabs appear: **Table**, **Type Mapping**, **Properties**, and **Data**.

1. Expand a table, expand **Keys**, and then select a key.

   The key properties appear in the right pane.

1. Expand **Indexes**, and then select an index.

   The index properties appear in the right pane.

## Refresh databases

If an Access database changes after you add its file, you can update metadata from the Access database.

In Access Metadata Explorer, right-click the database, and then select **Refresh from Database**.

## Remove databases

You can remove an Access database from a project by following these steps.

1. In Access Metadata Explorer, expand **Access-metabase**, and then expand **Databases**.

1. Right-click the database, and then select **Remove Database**.

## Related content

- [Migrate Access databases to SQL Server and Azure SQL](migrating-access-databases-to-sql-server-azure-sql-db-accesstosql.md)
- [Create and manage projects](creating-and-managing-projects-accesstosql.md)
- [Connect to SQL Server](connecting-to-sql-server-accesstosql.md)
