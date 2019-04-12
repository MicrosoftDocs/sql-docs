---
title: "sp_addlinkedsrvlogin (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_addlinkedsrvlogin_TSQL"
  - "sp_addlinkedsrvlogin"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_addlinkedsrvlogin"
ms.assetid: eb69f303-1adf-4602-b6ab-f62e028ed9f6
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# sp_addlinkedsrvlogin (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Creates or updates a mapping between a login on the local instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and a security account on a remote server.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
sp_addlinkedsrvlogin [ @rmtsrvname = ] 'rmtsrvname'   
     [ , [ @useself = ] { 'TRUE' | 'FALSE' | NULL } ]   
     [ , [ @locallogin = ] 'locallogin' ]   
     [ , [ @rmtuser = ] 'rmtuser' ]   
     [ , [ @rmtpassword = ] 'rmtpassword' ]   
```  
  
## Arguments  
 `[ @rmtsrvname = ] 'rmtsrvname'`  
 Is the name of a linked server that the login mapping applies to. *rmtsrvname* is **sysname**, with no default.  
  
 `[ @useself = ] { 'TRUE' | 'FALSE' | NULL }'
 Determines whether to connect to *rmtsrvname* by impersonating local logins or explicitly submitting a login and password. The data type is **varchar(**8**)**, with a default of TRUE.  
  
 A value of TRUE specifies that logins use their own credentials to connect to *rmtsrvname*, with the *rmtuser* and *rmtpassword* arguments being ignored. FALSE specifies that the *rmtuser* and *rmtpassword* arguments are used to connect to *rmtsrvname* for the specified *locallogin*. If *rmtuser* and *rmtpassword* are also set to NULL, no login or password is used to connect to the linked server.  
  
 `[ @locallogin = ] 'locallogin'`  
 Is a login on the local server. *locallogin* is **sysname**, with a default of NULL. NULL specifies that this entry applies to all local logins that connect to *rmtsrvname*. If not NULL, *locallogin* can be a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login or a Windows login. The Windows login must have been granted access to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] either directly, or through its membership in a Windows group granted access.  
  
 `[ @rmtuser = ] 'rmtuser'`  
 Is the remote login used to connect to *rmtsrvname* when @useself is FALSE. When the remote server is an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that does not use Windows Authentication, *rmtuser* is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login. *rmtuser* is **sysname**, with a default of NULL.  
  
 `[ @rmtpassword = ] 'rmtpassword'`  
 Is the password associated with *rmtuser*. *rmtpassword* is **sysname**, with a default of NULL.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 When a user logs on to the local server and executes a distributed query that accesses a table on the linked server, the local server must log on to the linked server on behalf of the user to access that table. Use sp_addlinkedsrvlogin to specify the login credentials that the local server uses to log on to the linked server.  
  
> [!NOTE]  
>  To create the best query plans when you are using a table on a linked server, the query processor must have data distribution statistics from the linked server. Users that have limited permissions on any columns of the table might not have sufficient permissions to obtain all the useful statistics, and might receive a less efficient query plan and experience poor performance. If the linked server is an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], to obtain all available statistics, the user must own the table or be a member of the sysadmin fixed server role, the db_owner fixed database role, or the db_ddladmin fixed database role on the linked server. SQL Server 2012 SP1 modifies the permission restrictions for obtaining statistics and allows users with SELECT permission to access statistics available through DBCC SHOW_STATISTICS. For more information, see the Permissions section of [DBCC SHOW_STATISTICS &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-show-statistics-transact-sql.md).  
  
 A default mapping between all logins on the local server and remote logins on the linked server is automatically created by executing sp_addlinkedserver. The default mapping states that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses the user credentials of the local login when connecting to the linked server on behalf of the login. This is equivalent to executing sp_addlinkedsrvlogin with @useself set to **true** for the linked server, without specifying a local user name. Use sp_addlinkedsrvlogin only to change the default mapping or to add new mappings for specific local logins. To delete the default mapping or any other mapping, use sp_droplinkedsrvlogin.  
  
 Instead of having to use sp_addlinkedsrvlogin to create a predetermined login mapping, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can automatically use the Windows security credentials (Windows login name and password) of a user issuing the query to connect to a linked server when all the following conditions exist:  
  
-   A user is connected to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using Windows Authentication Mode.  
  
-   Security account delegation is available on the client and sending server.  
  
-   The provider supports Windows Authentication Mode; for example, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] running on Windows.  
  
> [!NOTE]  
>  Delegation does not have to be enabled for single-hop scenarios, but it is required for multiple-hop scenarios.  
  
 After the authentication has been performed by the linked server by using the mappings that are defined by executing sp_addlinkedsrvlogin on the local instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the permissions on individual objects in the remote database are determined by the linked server, not the local server.  
  
 sp_addlinkedsrvlogin cannot be executed from within a user-defined transaction.  
  
## Permissions  
 Requires ALTER ANY LOGIN permission on the server.  
  
## Examples  
  
### A. Connecting all local logins to the linked server by using their own user credentials  
 The following example creates a mapping to make sure that all logins to the local server connect through to the linked server `Accounts` by using their own user credentials.  
  
```  
EXEC sp_addlinkedsrvlogin 'Accounts';  
```  
  
 Or  
  
```  
EXEC sp_addlinkedsrvlogin 'Accounts', 'true';  
```  
  
> [!NOTE]  
>  If there are explicit mappings created for individual logins, they take precedence over any global mappings that may exist for that linked server.  
  
### B. Connecting a specific login to the linked server by using different user credentials  
 The following example creates a mapping to make sure that the Windows user `Domain\Mary` connects through to the linked server `Accounts` by using the login `MaryP` and password `d89q3w4u`.  
  
```  
EXEC sp_addlinkedsrvlogin 'Accounts', 'false', 'Domain\Mary', 'MaryP', 'd89q3w4u';  
```  
  
> [!IMPORTANT]  
>  This example does not use Windows Authentication. Passwords will be transmitted unencrypted. Passwords may be visible in data source definitions and scripts that are saved to disk, in backups, and in log files. Never use an administrator password in this kind of connection. Consult your network administrator for security guidance specific to your environment.  
  
## See Also  
 [Linked Servers Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/linked-servers-catalog-views-transact-sql.md)   
 [sp_addlinkedserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md)   
 [sp_droplinkedsrvlogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-droplinkedsrvlogin-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
