---
title: "sp_helpremotelogin (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helpremotelogin_TSQL"
  - "sp_helpremotelogin"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_helpremotelogin"
ms.assetid: 93f50869-2627-4642-899f-8f626f8833f4
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helpremotelogin (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Reports information about remote logins for a particular remote server, or for all remote servers, defined on the local server.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepNextDontUse](../../includes/ssnotedepnextdontuse-md.md)] Use linked servers and linked server stored procedures instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helpremotelogin [ [ @remoteserver = ] 'remoteserver' ]   
     [ , [ @remotename = ] 'remote_name' ]  
```  
  
## Arguments  
 [ @remoteserver **=** ] **'***remoteserver***'**  
 Is the remote server about which the remote login information is returned. *remoteserver* is **sysname**, with a default of NULL. If *remoteserver* is not specified, information about all remote servers defined on the local server is returned.  
  
 [ @remotename **=** ] **'***remote_name***'**  
 Is a specific remote login on the remote server. *remote_name* is **sysname**, with a default of NULL. If *remote_name* is not specified, information about all remote users defined for *remoteserver* is returned.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|server|**sysname**|Name of a remote server defined on the local server.|  
|local_user_name|**sysname**|Login on the local server that remote logins from server map to.|  
|remote_user_name|**sysname**|Login on the remote server that maps to local_user_name.|  
|options|**sysname**|Trusted = The remote login does not need to supply a password when connecting to the local server from the remote server.<br /><br /> Untrusted (or blank) = The remote login is prompted for a password when connecting to the local server from the remote server.|  
  
## Remarks  
 Use sp_helpserver to list the names of remote servers defined on the local server.  
  
## Permissions  
 No permissions are checked.  
  
## Examples  
  
### A. Reporting help on a single server  
 The following example displays information about all remote users on the remote server `Accounts`.  
  
```  
EXEC sp_helpremotelogin 'Accounts';  
```  
  
### B. Reporting help on all remote users  
 The following example displays information about all remote users on all remote servers known to the local server.  
  
```  
EXEC sp_helpremotelogin;  
```  
  
## See Also  
 [sp_addremotelogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addremotelogin-transact-sql.md)   
 [sp_dropremotelogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropremotelogin-transact-sql.md)   
 [sp_helpserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpserver-transact-sql.md)   
 [sp_remoteoption &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-remoteoption-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
