---
title: "GetPathLocator (Transact-SQL)"
description: "GetPathLocator (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "GetPathLocator"
  - "GetPathLocator_TSQL"
helpviewer_keywords:
  - "GetPathLocator function"
dev_langs:
  - "TSQL"
---
# GetPathLocator (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns the path locator ID value for the specified file or directory in a FileTable.  
  
## Syntax  
  
```  
  
GetPathLocator(filenamespace_path)  
```  
  
## Arguments  
 *filenamespace_path*  
 A namespace path in the FileTable. The namespace path is of type **nvarchar(max)**.  
  
 When the database belongs to an Always On availability group, then the **GetPathLocator** function accepts the virtual network name (VNN) or the computer name.  
  
## Return Type  
 **hierarchyid**  
  
## General Remarks  
 For more information, see [Work with Directories and Paths in FileTables](../../relational-databases/blob/work-with-directories-and-paths-in-filetables.md).  
  
## Examples  
 You can use the **GetPathLocator** function when you are migrating files from a file server to a FileTable. In this scenario, you want to move the files into the FileTable, and then replace the original UNC path for each file with the FileTable UNC path. For a complete example, see [Load Files into FileTables](../../relational-databases/blob/load-files-into-filetables.md).  
  
## See Also  
 [Work with Directories and Paths in FileTables](../../relational-databases/blob/work-with-directories-and-paths-in-filetables.md)  
  
  
