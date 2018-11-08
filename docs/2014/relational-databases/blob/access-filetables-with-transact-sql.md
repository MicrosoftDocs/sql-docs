---
title: "Access FileTables with Transact-SQL | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: filestream
ms.topic: conceptual
helpviewer_keywords: 
  - "FileTables [SQL Server], accessing files with T-SQL"
ms.assetid: 3c4a5ffb-c521-4696-99cb-2b03cffc9c02
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Access FileTables with Transact-SQL
  Describes how [!INCLUDE[tsql](../../includes/tsql-md.md)] data manipulation language (DML) commands work with FileTables.  
  
##  <a name="BasicsInsert"></a> INSERT Operations on FileTables  
 The following considerations apply to **INSERT** Operations on FileTables:  
  
-   All the file attribute columns have NOT NULL constraints. If values are not explicitly set, then appropriate default values are supplied.  
  
-   System-defined constraints are enforced if the INSERT statement sets the **name**, **path_locator**, **parent_path_locator**, or file attributes.  
  
-   The application can obtain the **path_locator** for a file or directory by providing the file system path to the [GetPathLocator &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/getpathlocator-transact-sql) function.  
  
##  <a name="BasicsUpdate"></a> UPDATE Operations on FileTables  
 The following considerations apply to **UPDATE** operations on FileTables:  
  
-   Updates to any user-defined data are allowed.  
  
-   System-defined constraints are enforced if the INSERT statement sets the **name**, **path_locator**, **parent_path_locator**, or file attributes.  
  
-   Updates can be made to the FILESTREAM data in the **file_stream** column without affecting any of the other columns, including the timestamps.  
  
##  <a name="BasicsDelete"></a> DELETE Operations on FileTables  
 The following considerations apply to **DELETE** operations on FileTables:  
  
-   Deleting a row also removes the corresponding file or directory from the file system.  
  
-   Deleting a row fails if the row corresponds to a directory that contains other files or directories.  
  
##  <a name="BasicsConstraints"></a> Constraints That Are Enforced for DML Operations on FileTables  
 System-defined constraints ensure that DML actions do not compromise the integrity of the file namespace hierarchy. The constraints that are enforced include the following:  
  
-   When you set or change the **name** of the file or directory:  
  
    -   Windows file and directory naming conventions are enforced.  
  
    -   The uniqueness of the name in the parent directory is enforced.  
  
-   When you set or change the location of a file or directory by setting or changing the **path_locator** or **parent_path_locator**:  
  
    -   Uniqueness is enforced.  
  
    -   The consistency of the hierarchical tree of directories and files is enforced, including the consistency of **path_locator** and **parent_path_locator** values.  
  
-   The value of **is_directory** cannot be set to true when the **file_stream** column is not null. Data in the **file_stream** column indicates that the row represents a file and not a directory.  
  
-   File attribute columns cannot be null. NOT NULL constraints are enforced with default values.  
  
-   The value of **last_access_time** cannot be earlier than **last_write_time** and **creation_time**.  
  
## See Also  
 [Load Files into FileTables](load-files-into-filetables.md)   
 [Work with Directories and Paths in FileTables](work-with-directories-and-paths-in-filetables.md)   
 [Access FileTables with File Input-Output APIs](access-filetables-with-file-input-output-apis.md)   
 [FileTable DDL, Functions, Stored Procedures, and Views](../views/views.md)  
  
  
