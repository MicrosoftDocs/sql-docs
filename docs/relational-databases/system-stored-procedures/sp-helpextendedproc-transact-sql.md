---
title: "sp_helpextendedproc (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helpextendedproc"
  - "sp_helpextendedproc_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_helpextendedproc"
ms.assetid: 7e1f017e-c898-4225-b375-6a73ef9aac7b
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helpextendedproc (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Reports the currently defined extended stored procedures and the name of the dynamic-link library (DLL) to which the procedure (function) belongs.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [CLR Integration](../../relational-databases/clr-integration/common-language-runtime-integration-overview.md) instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helpextendedproc [ [@funcname = ] 'procedure' ]  
```  
  
## Arguments  
 [ **@funcname =**] **'***procedure***'**  
 Is the name of the extended stored procedure for which information is reported. *procedure* is **sysname**, with a default of NULL.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
  
|Column name|Data Type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Name of the extended stored procedure.|  
|**dll**|**nvarchar(255)**|Name of the DLL.|  
  
## Remarks  
 When *procedure* is specified, **sp_helpextendedproc** reports on the specified extended stored procedure. When this parameter is not supplied, **sp_helpextendedproc** returns all extended stored procedure names and the DLL names to which each extended stored procedure belongs.  
  
## Permissions  
 Permission to execute **sp_helpextendedproc** is granted to **public**.  
  
## Examples  
  
### A. Reporting help on all extended stored procedures  
 The following example reports on all extended stored procedures.  
  
```  
USE master;  
GO  
EXEC sp_helpextendedproc;  
GO  
```  
  
### B. Reporting help on a single extended stored procedure  
 The following example reports on the `xp_cmdshell` extended stored procedure.  
  
```  
USE master;  
GO  
EXEC sp_helpextendedproc xp_cmdshell;  
GO  
```  
  
## See Also  
 [sp_addextendedproc &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addextendedproc-transact-sql.md)   
 [sp_dropextendedproc &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropextendedproc-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
