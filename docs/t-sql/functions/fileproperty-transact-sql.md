---
title: "FILEPROPERTY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "FILEPROPERTY_TSQL"
  - "FILEPROPERTY"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "viewing file properties"
  - "names [SQL Server], files"
  - "displaying file properties"
  - "file properties [SQL Server]"
  - "FILEPROPERTY function"
  - "file names [SQL Server], FILEPROPERTY"
ms.assetid: b82244ed-d623-431f-aa06-8017349d847f
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# FILEPROPERTY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the specified file name property value when a file name in the current database and a property name are specified. Returns NULL for files that are not in the current database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
FILEPROPERTY ( file_name , property )  
```  
  
## Arguments  
 *file_name*  
 Is an expression that contains the name of the file associated with the current database for which to return property information. *file_name* is **nchar(128)**.  
  
 *property*  
 Is an expression that contains the name of the file property to return. *property* is **varchar(128)**, and can be one of the following values.  
  
|Value|Description|Value returned|  
|-----------|-----------------|--------------------|  
|**IsReadOnly**|Filegroup is read-only.|1 = True<br /><br /> 0 = False<br /><br /> NULL = Input is not valid.|  
|**IsPrimaryFile**|File is the primary file.|1 = True<br /><br /> 0 = False<br /><br /> NULL = Input is not valid.|  
|**IsLogFile**|File is a log file.|1 = True<br /><br /> 0 = False<br /><br /> NULL = Input is not valid.|  
|**SpaceUsed**|Amount of space that is used by the specified file.|Number of pages allocated in the file|  
  
## Return Types  
 **int**  
  
## Remarks  
 *file_name* corresponds to the **name** column in the **sys.master_files** or **sys.database_files** catalog view.  
  
## Examples  
 The following example returns the setting for the `IsPrimaryFile` property for the `AdventureWorks_Data` file name in [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] the database.  
  
```  
  
SELECT FILEPROPERTY('AdventureWorks2012_Data', 'IsPrimaryFile')AS [Primary File];  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Primary File   
-------------  
1  
(1 row(s) affected)  
```  
  
## See Also  
 [FILEGROUPPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/filegroupproperty-transact-sql.md)   
 [Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)   
 [sp_spaceused &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-spaceused-transact-sql.md)   
 [sys.database_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)   
 [sys.master_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md)  
  
  
