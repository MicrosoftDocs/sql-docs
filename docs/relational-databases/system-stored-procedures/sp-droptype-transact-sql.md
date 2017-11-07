---
title: "sp_droptype (Transact-SQL) | Microsoft Docs"
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
  - "sp_droptype_TSQL"
  - "sp_droptype"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_droptype"
ms.assetid: e78464ac-2370-4c4e-9cc0-06aebc07cec5
caps.latest.revision: 33
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# sp_droptype (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Deletes an alias data type from **systypes**.  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [current version](http://go.microsoft.com/fwlink/p/?LinkId=299658)).|  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_droptype [ @typename = ] 'type'  
```  
  
## Arguments  
 [ **@typename=**] **'***type***'**  
 Is the name of an alias data type that you own. *type* is **sysname**, with no default.  
  
## Return Code Type  
 0 (success) or 1 (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 The **type** alias data type cannot be dropped if tables or other database objects reference it.  
  
> [!NOTE]  
>  An alias data type cannot be dropped if the alias data type is used within a table definition or if a rule or default is bound to it.  
  
## Permissions  
 Requires membership in the **db_owner** fixed database role or the **db_ddladmin** fixed database role.  
  
## Examples  
 The following example drops the alias data type `birthday`.  
  
> [!NOTE]  
>  This alias data type must already exist or this example returns an error message.  
  
```  
USE master;  
GO  
EXEC sp_droptype 'birthday';  
GO  
```  
  
## See Also  
 [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)   
 [sp_addtype &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addtype-transact-sql.md)   
 [sp_rename &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-rename-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  