---
title: "sp_bindsession (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_bindsession"
  - "sp_bindsession_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_bindsession"
ms.assetid: 1436fe21-ad00-4a98-aca1-1451a5e571d2
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_bindsession (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Binds or unbinds a session to other sessions in the same instance of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)]. Binding sessions allows two or more sessions to participate in the same transaction and share locks until a ROLLBACK TRANSACTION or COMMIT TRANSACTION is issued.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use Multiple Active Results Sets (MARS) or distributed transactions instead. For more information, see [Using Multiple Active Result Sets &#40;MARS&#41;](../../relational-databases/native-client/features/using-multiple-active-result-sets-mars.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_bindsession { 'bind_token' | NULL }  
```  
  
## Arguments  
 **'** *bind_token* **'**  
 Is the token that identifies the transaction originally obtained by using **sp_getbindtoken** or the Open Data Services **srv_getbindtoken** function. *bind_token*is **varchar(255)**.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 Two sessions that are bound share only a transaction and locks. Each session retains its own isolation level, and setting a new isolation level on one session does not affect the isolation level of the other session. Each session remains identified by its security account and can only access the database resources to which the account has been granted permission.  
  
 **sp_bindsession** uses a bind token to bind two or more existing client sessions. These client sessions must be on the same instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] from which the binding token was obtained. A session is a client executing a command. Bound database sessions share a transaction and lock space.  
  
 A bind token obtained from one instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] cannot be used for a client session connected to another instance, even for DTC transactions. A bind token is valid only locally inside each instance and cannot be shared across multiple instances. To bind client sessions on another instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], you must obtain a different bind token by executing **sp_getbindtoken**.  
  
 **sp_bindsession** will fail with an error if it uses a token that is not active.  
  
 Unbind from a session either by using **sp_bindsession** without specifying *bind_token* or by passing NULL in *bind_token*.  
  
## Permissions  
 Requires membership in the **public** role.  
  
## Examples  
 The following example binds the specified bind token to the current session.  
  
> [!NOTE]  
>  The bind token shown in the example was obtained by executing **sp_getbindtoken** before executing **sp_bindsession**.  
  
```  
USE master;  
GO  
EXEC sp_bindsession 'BP9---5---->KB?-V'<>1E:H-7U-]ANZ';  
GO  
```  
  
## See Also  
 [sp_getbindtoken &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-getbindtoken-transact-sql.md)   
 [srv_getbindtoken &#40;Extended Stored Procedure API&#41;](../../relational-databases/extended-stored-procedures-reference/srv-getbindtoken-extended-stored-procedure-api.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
