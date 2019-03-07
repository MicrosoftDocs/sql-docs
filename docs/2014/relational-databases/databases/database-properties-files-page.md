---
title: "Database Properties (Files Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.databaseproperties.files.f1"
ms.assetid: 3c030e51-db82-4b43-b1e5-8547ddd3de87
author: stevestein
ms.author: sstein
manager: craigg
---
# Database Properties (Files Page)
  Use this page to create a new database, or view or modify properties for the selected database. This topic applies to the **Database Properties (Files Page)** for existing databases, and to the **New Database (General Page)**.  
  
## UIElement List  
 **Database name**  
 Add or display the name of the database.  
  
 **Owner**  
 Specify the owner of the database by selecting from the list.  
  
 **Use full-text indexing**  
 This check box is checked and disabled because full-text indexing is always enabled in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. For more information, see [Full-Text Search](../search/full-text-search.md).  
  
 **Database Files**  
 Add, view, modify, or remove database files for the associated database. Database files have the following properties:  
  
 **Logical Name**  
 Enter or modify the name of the file.  
  
 **File Type**  
 Select the file type from the list. The file type can be **Data**, **Log**, or **Filestream Data**. You cannot modify the file type of an existing file.  
  
 Select **Filestream Data** if you are adding files (containers) to a memory-optimized filegroup.  
  
 To add files (containers) to a Filestream data filegroup, FILESTREAM must be enabled. You can enable FILESTREAM by using the [Server Properties (Advanced Page)](../../database-engine/configure-windows/server-properties-advanced-page.md) dialog box.  
  
 **Filegroup**  
 Select the filegroup for the file from the list. By default, the filegroup is PRIMARY. You can create a new filegroup by selecting **\<new filegroup>** and entering information about the filegroup in the **New Filegroup** dialog box. A new filegroup can also be created on the **Filegroup** page. You cannot modify the filegroup of an existing file.  
  
 When adding files (containers) to a memory-optimized filegroup, the **Filegroup** field will be populated with the name of the database's memory-optimized filegroup.  
  
 **Initial Size**  
 Enter or modify the initial size for the file in megabytes. By default, this is the value of the **model** database.  
  
 This field is not valid for FILESTREAM files.  
  
 For files in memory-optimized filegroups, this field cannot be modified.  
  
 **Autogrowth**  
 Select or display the autogrowth properties for the file. These properties control how the file expands when its maximum file size is reached. To edit autogrowth values, click the edit button next to the autogrowth properties for the file that you want, and change the values in the **Change Autogrowth** dialog box. By default, these are the values of the **model** database.  
  
 This field is not valid for FILESTREAM files.  
  
 For files in memory-optimized filegroups, this field should be **Unlimited**.  
  
 **Path**  
 Displays the path of the selected file. To specify a path for a new file, click the edit button next to the path for the file, and navigate to the destination folder. You cannot modify the path of an existing file.  
  
 For FILESTREAM files, the path is a folder. The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] will create the underlying files in this folder.  
  
 **File Name**  
 Displays the name of the file.  
  
 This field is not valid for FILESTREAM files, including files in memory-optimized filegroups.  
  
 **Add**  
 Add a new file to the database.  
  
 **Remove**  
 Delete the selected file from the database. A file cannot be removed unless it is empty. The primary data file and log file cannot be removed.  
  
 For information about files, see [Database Files and Filegroups](database-files-and-filegroups.md).  
  
## See Also  
 [ALTER DATABASE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql)   
 [sys.databases &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-databases-transact-sql)  
  
  
