---
title: "sp_droplogin (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_droplogin"
  - "sp_droplogin_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_droplogin"
ms.assetid: e58684d1-c394-48de-906e-da6ee91100c3
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_droplogin (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Removes a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login. This prevents access to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] under that login name.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [DROP LOGIN](../../t-sql/statements/drop-login-transact-sql.md) instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_droplogin [ @loginame = ] 'login'  
```  
  
## Arguments  
`[ @loginame = ] 'login'`
 Is the login to be removed. *login* is **sysname**, with no default. *login* must already exist in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 **sp_droplogin** calls DROP LOGIN.  
  
 **sp_droplogin** cannot be executed within a user-defined transaction.  
  
## Permissions  
 Requires ALTER ANY LOGIN permission on the server.  
  
## Examples  
 The following example uses `DROP LOGIN` to remove the login `Victoria` from an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This is the preferred method.  
  
```  
DROP LOGIN Victoria;  
GO  
```  
  
## See Also  
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)   
 [DROP LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/drop-login-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
