---
title: "FILE_IDEX (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "FILE_IDEX"
  - "FILE_IDEX_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "FILE_IDEX function"
  - "IDs [SQL Server], files"
  - "file IDs [SQL Server]"
  - "names [SQL Server], files"
  - "identification numbers [SQL Server], files"
  - "file names [SQL Server], FILE_IDEX"
ms.assetid: 7532fea5-ee5e-4edd-b98b-111a7ba56c8e
caps.latest.revision: 35
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# FILE_IDEX (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the file identification (ID) number for the specified logical file name of the data, log, or full-text file in the current database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
FILE_IDEX ( file_name )  
```  
  
## Arguments  
 *file_name*  
 Is an expression of type **sysname** that represents the name of the file for which to return the file ID.  
  
## Return Types  
 **int**  
  
 **NULL** on error  
  
## Remarks  
 *file_name* corresponds to the logical file name displayed in the **name** column in the [sys.master_files](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md) or [sys.database_files](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md) catalog views.  
  
 FILE_IDEX can be used in a select list, a WHERE clause, or anywhere an expression is allowed. For more information, see [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md).  
  
## Examples  
  
### A. Retrieving the file id of a specified file  
 The following example returns the file ID for the `AdventureWorks_Data` file.  
  
```tsql  
USE AdventureWorks2012;  
GO  
SELECT FILE_IDEX('AdventureWorks2012_Data')AS 'File ID';  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
File ID   
-------   
1  
(1 row(s) affected)  
```  
  
### B. Retrieving the file id when the file name is not known  
 The following example returns the file ID of the `AdventureWorks` log file by selecting the logical file name from the `sys.database`_`files` catalog view where the file type is equal to `1` (log).  
  
```tsql  
USE AdventureWorks2012;  
GO  
SELECT FILE_IDEX((SELECT TOP(1)name FROM sys.database_files   
WHERE type = 1))AS 'File ID';  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
File ID   
-------   
2  
```  
  
### C. Retrieving the file id of a full-text catalog file  
 The following example returns the file ID of a full-text file by selecting the logical file name from the `sys.database`_`files` catalog view where the file type is equal to `4` (full-text). This example will return NULL if a full-text catalog does not exist.  
  
```tsql  
SELECT FILE_IDEX((SELECT name FROM sys.master_files WHERE type = 4))  
AS 'File_ID';  
```  
  
## See Also  
 [Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)   
 [sys.database_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)   
 [sys.master_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md)  
  
  