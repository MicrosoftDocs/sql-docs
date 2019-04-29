---
title: "sp_ivindexhasnullcols (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_ivindexhasnullcols"
  - "sp_ivindexhasnullcols_TSQL"
helpviewer_keywords: 
  - "sp_ivindexhasnullcols"
ms.assetid: ed2cde63-37e1-43cf-b6ba-3b6114a0f797
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_ivindexhasnullcols (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Validates that the clustered index of the indexed view is unique, and does not contain any column that can be null when the indexed view is going to be used to create a transactional publication. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_ivindexhasnullcols [ @viewname = ] 'view_name'  
        , [ @fhasnullcols= ] field_has_null_columns OUTPUT  
```  
  
## Arguments  
`[ @viewname = ] 'view_name'`
 Is the name of the view to verify. *view_name* is **sysname**, with no default.  
  
`[ @fhasnullcols = ] field_has_null_columns OUTPUT`
 Is the flag indicating whether the view index has columns that allow NULL. *view_name* is **sysname**, with no default. Returns a value of **1** if the view index has columns that allow NULL. Returns a value of **0** if the view does not contain columns that allow NULLS.  
  
> [!NOTE]  
>  If the stored procedure itself returns a return code of **1**, meaning the stored procedure execution had a failure, this value is **0** and should be ignored.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_ivindexhasnullcols** is used by transactional replication.  
  
 By default, indexed view articles in a publication are created as tables at the Subscribers. However, when the indexed column allows NULL values, the indexed view is created as an indexed view at the Subscriber instead of a table. By executing this stored procedure, it can alert the user to whether or not this problem exists with the current indexed view.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute **sp_ivindexhasnullcols**.  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
