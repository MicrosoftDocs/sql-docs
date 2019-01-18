---
title: "sp_audit_write (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_audit_write"
  - "sp_audit_write_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_audit_write"
ms.assetid: 4c523848-1ce6-49ad-92b3-e0e90f24f1c2
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_audit_write (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Adds a user-defined audit event to the **USER_DEFINED_AUDIT_GROUP**. If **USER_DEFINED_AUDIT_GROUP** is not enabled, **sp_audit_write** is ignored.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_audit_write [ @user_defined_event_id =  ] user_defined_event_id ,   
        [ @succeeded =  succeeded   
    [ , [ @user_defined_information =  ] 'user_defined_information' ]   
    [ ; ]  
```  
  
## Arguments  
 **@user_defined_event_id**  
 A parameter defined by the user and recorded in the **user_defined_event_id** column of the audit log. *@user_defined_event_id* is type **smallint**.  
  
 **@succeeded**  
 A parameter passed by user to indicate whether the event was successful or not. This appears in the succeeded column of the audit log. *@succeeded* is **bit**.  
  
 **@user_defined_information**  
 Is the text defined by the user and recorded in the new user_defined_event_id column of the audit log. *@user_defined_information* is **nvarchar(4000)**.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
 Failures are caused by incorrect input parameters or failure to write to the target audit log.  
  
## Remarks  
 When the **USER_DEFINED_AUDIT_GROUP** is added to either a server audit specification or a database audit specification, the event triggered by **sp_audit_write** will be included in the audit log.  
  
## Permissions  
 Requires membership in the **public** database role.  
  
## Examples  
  
### A. Creating a user-defined audit event with informational text  
 The following example creates an audit event with the id 27, the succeeded value of 0, and included optional informational text.  
  
```  
EXEC sp_audit_write @user_defined_event_id =  27 ,   
              @succeeded =  0   
            , @user_defined_information = N'Access to a monitored object.' ;  
```  
  
### B.  Creating a user-defined audit event without informational text  
 The following example creates an audit event with the id 27, the succeeded value of 0, and does not include optional informational text or the optional parameter names.  
  
```  
EXEC sp_audit_write 27, 0;  
  
```  
  
## See Also  
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)   
 [sys.server_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md)   
 [sp_addrole &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addrole-transact-sql.md)   
 [CREATE USER &#40;Transact-SQL&#41;](../../t-sql/statements/create-user-transact-sql.md)   
 [sp_dropuser &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropuser-transact-sql.md)   
 [sp_grantdbaccess &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-grantdbaccess-transact-sql.md)   
 [sp_grantlogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-grantlogin-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
