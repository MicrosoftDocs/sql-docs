---
title: "sp_dropextendedproc (Transact-SQL)"
description: "sp_dropextendedproc (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "10/04/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_dropextendedproc"
  - "sp_dropextendedproc_TSQL"
helpviewer_keywords:
  - "sp_dropextendedproc"
dev_langs:
  - "TSQL"
---
# sp_dropextendedproc (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Drops an extended stored procedure.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [CLR Integration](../../relational-databases/clr-integration/common-language-runtime-integration-overview.md) instead.  
  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
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
  
  
