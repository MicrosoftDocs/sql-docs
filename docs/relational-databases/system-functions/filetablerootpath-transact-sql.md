---
title: "FileTableRootPath (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "FileTableRootPath_TSQL"
  - "FileTableRootPath"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "FileTableRootPath function"
ms.assetid: 0cba908a-c85c-4b09-b16a-df1cb333c629
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# FileTableRootPath (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Returns the root-level UNC path for a specific FileTable or for the current database.  
  
## Syntax  
  
```  
  
FileTableRootPath ( [ '[schema_name.]FileTable_name' ], @option )  
```  
  
## Arguments  
 *FileTable_name*  
 The name of the FileTable. *FileTable_name* is of type **nvarchar**. This is an optional parameter. The default value is the current database. Specifying *schema_name* is also optional. You can pass NULL for *FileTable_name* to use the default parameter value  
  
 *@option*  
 An integer expression that defines how the server component of the path should be formatted. *@option* can have one of the following values:  
  
|Value|Description|  
|-----------|-----------------|  
|**0**|Returns the server name converted to NetBIOS format, for example:<br /><br /> `\\SERVERNAME\MSSQLSERVER\MyDocumentDatabase`<br /><br /> This is the default value.|  
|**1**|Returns the server name without conversion, for example:<br /><br /> `\\ServerName\MSSQLSERVER\MyDocumentDatabase`|  
|**2**|Returns the complete server path, for example:<br /><br /> `\\ServerName.MyDomain.com\MSSQLSERVER\MyDocumentDatabase`|  
  
## Return Type  
 **nvarchar(4000)**  
  
 When the database belongs to an Always On availability group, then the **FileTableRootPath** function returns the virtual network name (VNN) instead of the computer name.  
  
## General Remarks  
 The **FileTableRootPath** function returns NULL when one of the following conditions is true:  
  
-   The value of *FileTable_name* is not valid.  
  
-   The caller does not have sufficient permission to reference the specified table or the current database.  
  
-   The FILESTREAM option of *database_directory* is not set for the current database.  
  
 For more information, see [Work with Directories and Paths in FileTables](../../relational-databases/blob/work-with-directories-and-paths-in-filetables.md).  
  
## Best Practices  
 To keep code and applications independent of the current computer and database, avoid writing code that relies on absolute file paths. Instead, get the complete path for a file at run time by using the **FileTableRootPath** and **GetFileNamespacePath** functions together, as shown in the following example. By default, the **GetFileNamespacePath** function returns the relative path of the file under the root path for the database.  
  
```sql  
USE MyDocumentDatabase;  
  
@root varchar(100)  
SELECT @root = FileTableRootPath();  
@fullPath = varchar(1000);  
  
SELECT @fullPath = @root + file_stream.GetFileNamespacePath()  
FROM DocumentStore  
WHERE Name = N'document.docx';  
```  
  
## Security  
  
### Permissions  
 The **FileTableRootPath** function requires:  
  
-   SELECT permission on the FileTable to get the root path of a specific FileTable.  
  
-   **db_datareader** or higher permission to get the root path for the current database.  
  
## Examples  
 The following examples show how to call the **FileTableRootPath** function.  
  
```  
USE MyDocumentDatabase;  
-- returns "\\MYSERVER\MSSQLSERVER\MyDocumentDatabase"  
SELECT FileTableRootPath();  
  
-- returns "\\MYSERVER\MSSQLSERVER\MyDocumentDatabase\MyFileTable"  
SELECT FileTableRootPath(N'dbo.MyFileTable');  
  
-- returns "\\MYSERVER\MSSQLSERVER\MyDocumentDatabase\MyFileTable"  
SELECT FileTableRootPath(N'MyFileTable');  
```  
  
## See Also  
 [Work with Directories and Paths in FileTables](../../relational-databases/blob/work-with-directories-and-paths-in-filetables.md)  
  
  
