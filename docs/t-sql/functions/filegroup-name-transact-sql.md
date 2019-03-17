---
title: "FILEGROUP_NAME (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "FILEGROUP_NAME_TSQL"
  - "FILEGROUP_NAME"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "displaying filegroup names"
  - "identification numbers [SQL Server], filegroups"
  - "filegroups [SQL Server], IDs"
  - "IDs [SQL Server], filegroups"
  - "FILEGROUP_NAME function"
  - "filegroups [SQL Server], names"
  - "names [SQL Server], filegroups"
  - "viewing filegroup names"
ms.assetid: 26add1c0-56e5-47a8-b489-ae56784a7ee9
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# FILEGROUP_NAME (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

This function returns the filegroup name for the specified filegroup identification (ID) number.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
FILEGROUP_NAME ( filegroup_id )   
```  
  
## Arguments  
 *filegroup_id*  

The filegroup ID number whose filegroup name `FILEGROUP_NAME` will return. *filegroup_id* has a **smallint** data type.  
  
## Return Types  
**nvarchar(128)**  
  
## Remarks  
*filegroup_id* corresponds to the **data_space_id** column of the **sys.filegroups** catalog view.  
  
## Examples  
This example returns the filegroup name for filegroup ID `1` in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```  
SELECT FILEGROUP_NAME(1) AS [Filegroup Name];  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Filegroup Name   
-----------------------  
PRIMARY  
  
(1 row(s) affected)  
```  
  
## See Also  
 [Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [sys.filegroups &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-filegroups-transact-sql.md)  
  
  
