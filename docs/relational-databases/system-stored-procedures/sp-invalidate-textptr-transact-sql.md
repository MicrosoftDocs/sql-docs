---
title: "sp_invalidate_textptr (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_invalidate_textptr_TSQL"
  - "sp_invalidate_textptr"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_invalidate_textptr"
ms.assetid: dd9920e1-7064-4c05-93d8-9303103fa1d6
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_invalidate_textptr (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Invalidates the specified in-row text pointer, or all in-row text pointers, in the transaction. **sp_invalidate_textptr** can be used only on in-row text pointers. These pointers are from tables that have the **text in row** option enabled.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_invalidate_textptr [ [ @TextPtrValue = ] textptr_value ]  
```  
  
## Arguments  
`[ @TextPtrValue = ] textptr_value`
 Is the in-row text pointer that to be invalidated. *textptr_value* is **varbinary(**16**)**, with a default of NULL. If NULL, **sp_invalidate_textptr** invalidates all in-row text pointers in the transaction.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] allows for a maximum of 1,024 active valid in-row text pointers per transaction per database; however, a transaction spanning more than one database can have 1,024 in-row text pointers in each database. **sp_invalidate_textptr** can be used to invalidate in-row text pointers and, therefore, free space for additional in-row text pointers.  
  
 For more information about the text in row option, see [sp_tableoption &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-tableoption-transact-sql.md).  
  
## Permissions  
 Requires membership in the **public** role.  
  
## See Also  
 [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [sp_tableoption &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-tableoption-transact-sql.md)   
 [TEXTPTR &#40;Transact-SQL&#41;](../../t-sql/functions/text-and-image-functions-textptr-transact-sql.md)   
 [TEXTVALID &#40;Transact-SQL&#41;](../../t-sql/functions/text-and-image-functions-textvalid-transact-sql.md)  
  
  
