---
title: "GetFileNamespacePath (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "GetFileNamespacePath"
  - "GetFileNamespacePath_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "GetFileNamespacePath function"
ms.assetid: b393ecef-baa8-4d05-a268-b2f309fce89a
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# GetFileNamespacePath (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Returns the UNC path for a file or directory in a FileTable.  
  
## Syntax  
  
```  
  
<column-name>.GetFileNamespacePath(is_full_path, @option)  
```  
  
## Arguments  
 *column-name*  
 The column name of the VARBINARY(MAX) **file_stream** column in a FileTable.  
  
 The *column-name* value must be a valid column name. It cannot be an expression, or a value converted or cast from a column of another data type.  
  
 *is_full_path*  
 An integer expression that specifies whether to return a relative or an absolute path. *is_full_path* can have one of the following values:  
  
|Value|Description|  
|-----------|-----------------|  
|**0**|Returns the relative path within the database-level directory.<br /><br /> This is the default value|  
|**1**|Returns the full UNC path, starting with the `\\computer_name`.|  
  
 *@option*  
 An integer expression that defines how the server component of the path should be formatted. *@option* can have one of the following values:  
  
|Value|Description|  
|-----------|-----------------|  
|**0**|Returns the server name converted to NetBIOS format, for example:<br /><br /> `\\SERVERNAME\MSSQLSERVER\MyDocumentDatabase`<br /><br /> This is the default value.|  
|**1**|Returns the server name without conversion, for example:<br /><br /> `\\ServerName\MSSQLSERVER\MyDocumentDatabase`|  
|**2**|Returns the complete server path, for example:<br /><br /> `\\ServerName.MyDomain.com\MSSQLSERVER\MyDocumentDatabase`|  
  
## Return Type  
 **nvarchar(max)**  
  
 If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance is clustered in a failover cluster, then the machine name that is returned as part of this path is the virtual hostname for the clustered instance.  
  
 When the database belongs to an Always On availability group, then the **FileTableRootPath** function returns the virtual network name (VNN) instead of the computer name.  
  
## General Remarks  
 The path that the **GetFileNamespacePath** function returns is a logical directory or file path in the following format:  
  
 `\\<machine>\<instance-level FILESTREAM share>\<database-level directory>\<FileTable directory>\...`  
  
 This logical path does not directly correspond to a physical NTFS path. It is translated to the physical path by FILESTREAM's file system filter driver and the FILESTREAM agent. This separation between the logical path and physical path lets [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] reorganize data internally without affecting the validity of the path.  
  
## Best Practices  
 To keep code and applications independent of the current computer and database, avoid writing code that relies on absolute file paths. Instead, get the complete path for a file at run time by using the **FileTableRootPath** and **GetFileNamespacePath** functions together, as shown in the following example. By default, the **GetFileNamespacePath** function returns the relative path of the file under the root path for the database.  
  
```sql  
USE MyDocumentDatabase;  
@root varchar(100)  
SELECT @root = FileTableRootPath();  
  
@fullPath = varchar(1000);  
SELECT @fullPath = @root + file_stream.GetFileNamespacePath() FROM DocumentStore  
WHERE Name = N'document.docx';  
```  
  
## Remarks  
  
## Examples  
 The following examples show how to call the **GetFileNamespacePath** function to get the UNC path for a file or directory in a FileTable.  
  
```  
-- returns the relative path of the form "\MyFileTable\MyDocDirectory\document.docx"  
SELECT file_stream.GetFileNamespacePath() AS FilePath FROM DocumentStore  
WHERE Name = N'document.docx';  
  
-- returns "\\MyServer\MSSQLSERVER\MyDocumentDatabase\MyFileTable\MyDocDirectory\document.docx"  
SELECT file_stream.GetFileNamespacePath(1, Null) AS FilePath FROM DocumentStore  
WHERE Name = N'document.docx';  
```  
  
## See Also  
 [Work with Directories and Paths in FileTables](../../relational-databases/blob/work-with-directories-and-paths-in-filetables.md)  
  
  
