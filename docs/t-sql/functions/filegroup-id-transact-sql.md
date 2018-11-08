---
title: "FILEGROUP_ID (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "FILEGROUP_ID_TSQL"
  - "FILEGROUP_ID"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "FILEGROUP_ID function"
  - "identification numbers [SQL Server], filegroups"
  - "filegroups [SQL Server], IDs"
  - "IDs [SQL Server], filegroups"
  - "names [SQL Server], filegroups"
ms.assetid: 852a76d8-9e61-4a31-84ee-c7edb84a061c
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# FILEGROUP_ID (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

This function returns the filegroup identification (ID) number for a specified filegroup name.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
FILEGROUP_ID ( 'filegroup_name' )   
```  
  
## Arguments  
*filegroup_name*
An expression of type **sysname**, representing the filegroup name whose filegroup ID `FILEGROUP_ID` will return.  
  
## Return Types  
**int**  
  
## Remarks  
*filegroup_name* corresponds to the **name** column in the **sys.filegroups** catalog view.  
  
## Examples  
This example returns the filegroup ID for the filegroup named `PRIMARY` in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```  
SELECT FILEGROUP_ID('PRIMARY') AS [Filegroup ID];  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```
Filegroup ID  
------------  
1  

(1 row(s) affected)
 ```  
  
## See Also  
 [FILEGROUP_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/filegroup-name-transact-sql.md)   
 [Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)   
 [sys.filegroups &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-filegroups-transact-sql.md)  
  
  
