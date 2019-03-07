---
title: "GetPathLocator (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "GetPathLocator"
  - "GetPathLocator_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "GetPathLocator function"
ms.assetid: 78b7e220-445b-4fdf-811b-7253f4f2b058
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# GetPathLocator (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

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
  
  
