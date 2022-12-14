---
title: sp_dropserver (Transact-SQL)
description: "sp_dropserver (Transact-SQL)"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_dropserver_TSQL"
  - "sp_dropserver"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_dropserver"
author: markingmyname
ms.author: maghan
ms.custom: ""
ms.date: "09/07/2018"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016||>=sql-server-linux-2017"
---

# sp_dropserver (Transact-SQL)

[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

Removes a server from the list of known remote and linked servers on the local instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
![link icon](../../database-engine/configure-windows/media/topic-link.gif "link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql  
sp_dropserver [ @server = ] 'server'   
     [ , [ @droplogins = ] { 'droplogins' | NULL} ]  
```  
  
## Arguments  
 *server*  
 Is the server to be removed. *server* is **sysname**, with no default. *server* must exist.  
  
 *droplogins*  
 Indicates that related remote and linked server logins for *server* must also be removed if **droplogins** is specified. **`@droplogins`** is **char(10)**, with a default of NULL.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 If you run **sp_dropserver** on a server that has associated remote and linked server login entries, or is configured as a replication publisher, an error message is returned. To remove all remote and linked server logins for a server when you remove the server, use the **droplogins** argument.  
  
 **sp_dropserver** cannot be executed inside a user-defined transaction.  
  
 **sp_dropserver** to change the local server name may cause unintended effects or unsupported configurations.
  
## Permissions  
 Requires ALTER ANY LINKED SERVER permission on the server.  
  
## Examples  
 The following example removes the remote server `ACCOUNTS` and all associated remote logins from the local instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
```  
sp_dropserver 'ACCOUNTS', 'droplogins';  
```  
  
## See Also  
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)   
 [sp_addserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addserver-transact-sql.md)   
 [sp_dropremotelogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropremotelogin-transact-sql.md)   
 [sp_helpremotelogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpremotelogin-transact-sql.md)   
 [sp_helpserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpserver-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
