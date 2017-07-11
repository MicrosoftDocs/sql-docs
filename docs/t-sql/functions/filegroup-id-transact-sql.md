---
title: "FILEGROUP_ID (Transact-SQL) | Microsoft Docs"
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
caps.latest.revision: 21
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# FILEGROUP_ID (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the filegroup identification (ID) number for a specified filegroup name.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
FILEGROUP_ID ( 'filegroup_name' )   
```  
  
## Arguments  
 **'** *filegroup_name* **'**  
 Is an expression of type **sysname** that represents the filegroup name for which to return the filegroup ID.  
  
## Return Types  
 **int**  
  
## Remarks  
 *filegroup_name* corresponds to the **name** column in the **sys.filegroups** catalog view.  
  
## Examples  
 The following example returns the filegroup ID for the filegroup named `PRIMARY` in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```  
  
SELECT FILEGROUP_ID('PRIMARY') AS [Filegroup ID];  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `Filegroup ID`  
  
 `------------`  
  
 `1`  
  
 `(1 row(s) affected)`  
  
## See Also  
 [FILEGROUP_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/filegroup-name-transact-sql.md)   
 [Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)   
 [sys.filegroups &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-filegroups-transact-sql.md)  
  
  