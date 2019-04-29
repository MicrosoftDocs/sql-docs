---
title: "FILE_ID (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "FILE_ID"
  - "FILE_ID_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "IDs [SQL Server], files"
  - "file IDs [SQL Server]"
  - "FILE_ID function"
  - "names [SQL Server], files"
  - "identification numbers [SQL Server], files"
  - "file names [SQL Server], FILE_ID"
ms.assetid: 6a7382cf-a360-4d62-b9d2-5d747f56f076
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# FILE_ID (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

For the given logical name for a component file of the current database, this function returns the file identification (ID) number.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [FILE_IDEX](../../t-sql/functions/file-idex-transact-sql.md) instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
FILE_ID ( file_name )  
```  
  
## Arguments  
*file_name*  
An expression of type **sysname**, representing the logical name of the file whose file ID value `FILE_ID` will return.  
  
## Return Types  
**smallint**  
  
## Remarks  
*file_name* corresponds to the logical file name displayed in the name column of the sys.master_files or sys.database_files catalog views.  

`FILE_ID` returns `NULL` if *file_name* does not correspond to the logical name of a component file of the current database.
  
In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the file identification number assigned to full-text catalogs exceeds 32767. Because the `FILE_ID` function has a **smallint** return type, `FILE_ID` will not support full-text files. Use [FILE_IDEX](../../t-sql/functions/file-idex-transact-sql.md) instead.  
  
## Examples  
This example returns the file ID value for the `AdventureWorks_Data` file, a component file of the `ADVENTUREWORKS2012` database.  

```sql  
USE AdventureWorks2012;  
GO  
SELECT FILE_ID('AdventureWorks2012_Data')AS 'File ID';  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
File ID   
-------   
1  
(1 row(s) affected)  
```  
  
## See Also  
 [Deprecated Database Engine Features in SQL Server 2016](../../database-engine/deprecated-database-engine-features-in-sql-server-2016.md)   
 [FILE_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/file-name-transact-sql.md)   
 [Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)   
 [sys.database_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)   
 [sys.master_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md)  
  
  
