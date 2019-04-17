---
title: "sp_addremotelogin (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_addremotelogin_TSQL"
  - "sp_addremotelogin"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_addremotelogin"
ms.assetid: 71b7cd36-a17d-4b12-b102-10aeb0f9268b
author: VanMSFT
ms.author: vanto
manager: craigg
---
# sp_addremotelogin (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Adds a new remote login ID on the local server. This enables remote servers to connect and execute remote procedure calls.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepNextDontUse](../../includes/ssnotedepnextdontuse-md.md)] Use linked servers and linked server stored procedures instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_addremotelogin [ @remoteserver = ] 'remoteserver'   
     [ , [ @loginame = ] 'login' ]   
     [ , [ @remotename = ] 'remote_name' ]  
```  
  
## Arguments  
 [ @remoteserver **=** ] **'**_remoteserver_**'**  
 Is the name of the remote server that the remote login applies to. *remoteserver* is **sysname**, with no default. If only *remoteserver* is specified, all users on *remoteserver* are mapped to existing logins of the same name on the local server. The server must be known to the local server. This is added by using sp_addserver. When users on *remoteserver* connect to the local server that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to execute a remote stored procedure, they connect as the local login that matches their own login on *remoteserver*. *remoteserver* is the server that initiates the remote procedure call.  
  
 [ @loginame **=** ] **'**_login_**'**  
 Is the login ID of the user on the local instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. *login* is **sysname**, with a default of NULL. *login*must already exist on the local instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If *login* is specified, all users on *remoteserver* are mapped to that specific local login. When users on *remoteserver* connect to the local instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to execute a remote stored procedure, they connect as *login*.  
  
 [ @remotename **=** ] **'**_remote_name_**'**  
 Is the login ID of the user on the remote server. *remote_name* is **sysname**, with a default of NULL. *remote_name* must exist on *remoteserver*. If *remote_name* is specified, the specific user *remote_name* is mapped to *login* on the local server. When *remote_name* on *remoteserver* connects to the local instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to execute a remote stored procedure, it connects as *login*. The login ID of *remote_name* can be different from the login ID on the remote server, *login*.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 To execute distributed queries, use sp_addlinkedsrvlogin.  
  
 sp_addremotelogin cannot be used inside a user-defined transaction.  
  
## Permissions  
 Only members of the sysadmin and securityadmin fixed server roles can execute sp_addremotelogin.  
  
## Examples  
  
### A. Mapping one to one  
 The following example maps remote names to local names when the remote server `ACCOUNTS` and local server have the same user logins.  
  
```  
EXEC sp_addremotelogin 'ACCOUNTS';  
```  
  
### B. Mapping many to one  
 The following example creates an entry that maps all users from the remote server `ACCOUNTS` to the local login ID `Albert`.  
  
```  
EXEC sp_addremotelogin 'ACCOUNTS', 'Albert';  
```  
  
### C. Using explicit one-to-one mapping  
 The following example maps a remote login from the remote user `Chris` on the remote server `ACCOUNTS` to the local user `salesmgr`.  
  
```  
EXEC sp_addremotelogin 'ACCOUNTS', 'salesmgr', 'Chris';  
```  
  
## See Also  
 [sp_addlinkedsrvlogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addlinkedsrvlogin-transact-sql.md)   
 [sp_addlogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addlogin-transact-sql.md)   
 [sp_addserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addserver-transact-sql.md)   
 [sp_dropremotelogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropremotelogin-transact-sql.md)   
 [sp_grantlogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-grantlogin-transact-sql.md)   
 [sp_helpremotelogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpremotelogin-transact-sql.md)   
 [sp_helpserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpserver-transact-sql.md)   
 [sp_remoteoption &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-remoteoption-transact-sql.md)   
 [sp_revokelogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-revokelogin-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
