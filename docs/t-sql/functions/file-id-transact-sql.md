---
title: "FILE_ID (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
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
caps.latest.revision: 34
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# FILE_ID (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the file identification (ID) number for the given logical file name in the current database.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [FILE_IDEX](../../t-sql/functions/file-idex-transact-sql.md) instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
FILE_ID ( file_name )  
```  
  
## Arguments  
 *file_name*  
 Is an expression of type **sysname** that represents the name of the file for which to return the file ID.  
  
## Return Types  
 **smallint**  
  
## Remarks  
 *file_name* corresponds to the logical file name displayed in the name column in the sys.master_files or sys.database_files catalog views.  
  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the file identification number assigned to full-text catalogs is greater than 32767. Because the return type of the FILE_ID function is **smallint**, this function cannot be used for full-text files. Use [FILE_IDEX](../../t-sql/functions/file-idex-transact-sql.md) instead.  
  
## Examples  
 The following example returns the file ID for the `AdventureWorks_Data` file.  
  
```tsql  
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
  
  