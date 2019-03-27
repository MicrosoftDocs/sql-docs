---
title: "Load Files into FileTables | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: filestream
ms.topic: conceptual
helpviewer_keywords: 
  - "FileTables [SQL Server], migrating files"
  - "FileTables [SQL Server], bulk loading"
  - "FileTables [SQL Server], loading files"
ms.assetid: dc842a10-0586-4b0f-9775-5ca0ecc761d9
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Load Files into FileTables
  Describes how to load or migrate files into FileTables.  
  
##  <a name="BasicsLoadNew"></a> Loading or Migrating Files into a FileTable  
 The method that you choose for loading or migrating files into a FileTable depends on where the files are currently stored.  
  
|Current location of files|Options for migration|  
|-------------------------------|---------------------------|  
|Files are currently stored in the file system.<br /><br /> [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has no knowledge of the files.|Since a FileTable appears as a folder in the Windows file system, you can easily load files into a new FileTable by using any of the available methods for moving or copying files. These methods include Windows Explorer, command line options including xcopy and robocopy, and custom scripts or applications.<br /><br /> You cannot convert an existing folder to a FileTable.|  
|Files are currently stored in the file system.<br /><br /> [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] contains a table of metadata that contains pointers to the files.|The first step is to move or copy the files by using one of the methods mentioned above.<br /><br /> The second step is to update the existing table of metadata to point to the new location of the files.<br /><br /> For more information, see [Example: Migrating Files from the File System into a FileTable](#HowToMigrateFiles) in this topic.|  
  
###  <a name="HowToLoadNew"></a> How To: Load Files into a FileTable  
 The methods that you can use to load files into a FileTable include the following:  
  
-   Drag and drop files from the source folders to the new FileTable folder in Windows Explorer.  
  
-   Use command line options such as MOVE, COPY, XCOPY, or ROBOCOPY from the command prompt or in a batch file or script.  
  
-   Write a custom application in C# or Visual Basic.NET that uses methods from the **System.IO** namespace to move or copy the files.  
  
###  <a name="HowToMigrateFiles"></a> Example: Migrating Files from the File System into a FileTable  
 In this scenario, your files are stored in the file system, and you have a table of metadata in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that contains pointers to the files. You want to move the files into a FileTable, and then replace the original UNC path for each file in the metadata with the FileTable UNC path. The [GetPathLocator &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/getpathlocator-transact-sql) function helps you to achieve this goal.  
  
 For this example, assume that there is an existing database table, `PhotoMetadata`, which contains data about photographs. This table has a column `UNCPath` of type `varchar`(512) which contains the actual UNC path to a .jpg file.  
  
 To migrate the image files from the file system into a FileTable, you have to do the following:  
  
1.  Create a new FileTable to hold the files. This example uses the table name, `dbo.PhotoTable`, but does not show the code to create the table.  
  
2.  Use xcopy or a similar tool to copy the .jpg files, with their directory structure, into the root directory of the FileTable.  
  
3.  Fix the metadata in the `PhotoMetadata` table, by using code similar to the following:  
  
```sql  
--  Add a path locator column to the PhotoMetadata table.  
ALTER TABLE PhotoMetadata ADD pathlocator hierarchyid;  
  
-- Get the root path of the Photo directory on the File Server.  
DECLARE @UNCPathRoot varchar(100) = '\\RemoteShare\Photographs';  
  
-- Get the root path of the FileTable.  
DECLARE @FileTableRoot varchar(1000);  
SELECT @FileTableRoot = FileTableRootPath('dbo.PhotoTable');  
  
-- Update the PhotoMetadata table.  
  
-- Replace the File Server UNC path with the FileTable path.  
UPDATE PhotoMetadata  
    SET UNCPath = REPLACE(UNCPath, @UNCPathRoot, @FileTableRoot);  
  
-- Update the pathlocator column to contain the pathlocator IDs from the FileTable.  
UPDATE PhotoMetadata  
    SET pathlocator = GetPathLocator(UNCPath);  
```  
  
##  <a name="BasicsBulkLoad"></a> Bulk Loading Files into a FileTable  
 A FileTable behaves like a normal table for bulk operations, with the following qualifications.  
  
 A FileTable has system-defined constraints which ensure that the integrity of the file and directory namespace is maintained. These constraints have to be verified on the data bulk loaded into the FileTable. Since some bulk insert operations allow table constraints to be ignored, the following requirements are enforced.  
  
-   Bulk loading operations that enforce constraints can be run against a FileTable as against any other table. This category includes the following operations:  
  
    -   bcp with CHECK_CONSTRAINTS clause.  
  
    -   BULK INSERT with CHECK_CONSTRAINTS clause.  
  
    -   INSERT INTO ... SELECT * FROM OPENROWSET(BULK ...) without IGNORE_CONSTRAINTS clause.  
  
-   Bulk loading operations that do not enforce constraints fail unless the FileTable system-defined constraints have been disabled. This category includes the following operations:  
  
    -   bcp without CHECK_CONSTRAINTS clause.  
  
    -   BULK INSERT without CHECK_CONSTRAINTS clause.  
  
    -   INSERT INTO ... SELECT * FROM OPENROWSET(BULK ...) with IGNORE_CONSTRAINTS clause.  
  
###  <a name="HowToBulkLoad"></a> How To: Bulk Load Files into a FileTable  
 You can use various methods to bulk load files into a FileTable:  
  
-   **bcp**  
  
    -   Call with the **CHECK_CONSTRAINTS** clause.  
  
    -   Disable the FileTable namespace and call without the **CHECK_CONSTRAINTS** clause. Then re-enable the FileTable namespace.  
  
-   **BULK INSERT**  
  
    -   Call with the **CHECK_CONSTRAINTS** clause.  
  
    -   Disable the FileTable namespace and call without the **CHECK_CONSTRAINTS** clause. Then re-enable the FileTable namespace.  
  
-   **INSERT INTO ... SELECT \* FROM OPENROWSET(BULK ...)**  
  
    -   Call with the **IGNORE_CONSTRAINTS** clause.  
  
    -   Disable the FileTable namespace and call without the **IGNORE_CONSTRAINTS** clause. Then re-enable the FileTable namespace.  
  
 For information about disabling the FileTable constraints, see [Manage FileTables](manage-filetables.md).  
  
###  <a name="disabling"></a> How To: Disable FileTable Constraints for Bulk Loading  
 To bulk load files into a FileTable without the overhead of enforcing the system-defined constraints, you can temporarily disable the constraints. For more information, see [Manage FileTables](manage-filetables.md).  
  
## See Also  
 [Access FileTables with Transact-SQL](access-filetables-with-transact-sql.md)   
 [Access FileTables with File Input-Output APIs](access-filetables-with-file-input-output-apis.md)  
  
  
