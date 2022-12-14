---
title: "FILE_NAME (Transact-SQL)"
description: "FILE_NAME (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "FILE_NAME_TSQL"
  - "FILE_NAME"
helpviewer_keywords:
  - "viewing file names"
  - "file names [SQL Server], FILE_NAME"
  - "IDs [SQL Server], files"
  - "file IDs [SQL Server]"
  - "names [SQL Server], files"
  - "displaying file names"
  - "identification numbers [SQL Server], files"
  - "FILE_NAME function"
  - "logical file names [SQL Server]"
dev_langs:
  - "TSQL"
---
# FILE_NAME (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This function returns the logical file name for a given file identification (ID) number.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
FILE_NAME ( file_id )   
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*file_id*  
The file identification number whose file name `FILE_NAME` will return. *file_id* has an **int** data type.  
  
## Return Types  
**nvarchar(128)**  
  
## Remarks  
*file_ID* corresponds to the file_id column in the sys.master_files catalog view or the sys.database_files catalog view.  
  
## Examples  
This example returns the file names for `file_ID 1` and `file_ID` in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```sql  
SELECT FILE_NAME(1) AS 'File Name 1', FILE_NAME(2) AS 'File Name 2';  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```
File Name 1                File Name 2  
-------------------------  ------------------------  
AdventureWorks2012_Data    AdventureWorks2012_Log  

(1 row(s) affected)
``` 
  
## See Also  
 [FILE_IDEX &#40;Transact-SQL&#41;](../../t-sql/functions/file-idex-transact-sql.md)   
 [Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)   
 [sys.database_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)   
 [sys.master_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md)  
  
