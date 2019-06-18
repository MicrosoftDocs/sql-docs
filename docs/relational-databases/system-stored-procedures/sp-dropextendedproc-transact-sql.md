---
title: "sp_dropextendedproc (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_dropextendedproc"
  - "sp_dropextendedproc_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_dropextendedproc"
ms.assetid: dd93af2c-1b7d-4e39-af23-2d21d270a381
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_dropextendedproc (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Drops an extended stored procedure.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [CLR Integration](../../relational-databases/clr-integration/common-language-runtime-integration-overview.md) instead.  
  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
sp_dropextendedproc [ @functname = ] 'procedure'   
```  
  
## Arguments  
`[ @functname = ] 'procedure'`
 Is the name of the extended stored procedure to drop. *procedure* is **nvarchar(517)**, with no default.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 Executing **sp_dropextendedproc** drops the user-defined extended stored procedure name from the [sys.objects](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md) catalog view and removes the entry from the [sys.extended_procedures](../../relational-databases/system-catalog-views/sys-extended-procedures-transact-sql.md) catalog view. This stored procedure can be run only in the **master** database.  
  
**sp_dropextendedproc** does not drop system extended stored procedures. Instead, the system administrator should deny EXECUTE permission on the extended stored procedure to the **public** role.  
  
 **sp_dropextendedproc** cannot be executed inside a transaction.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_dropextendedproc**.  
  
## Examples  
 The following example drops the `xp_hello` extended stored procedure.  
  
> [!NOTE]  
>  This extended stored procedure must already exist, or the example will return an error message.  
  
```  
USE master;  
GO  
EXEC sp_dropextendedproc 'xp_hello';  
```  
  
## See Also  
 [sp_addextendedproc &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addextendedproc-transact-sql.md)   
 [sp_helpextendedproc &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpextendedproc-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
