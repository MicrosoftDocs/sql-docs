---
title: "sp_addserver (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_addserver"
  - "sp_addserver_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_addserver"
  - "renaming servers"
  - "machine names [SQL Server]"
  - "computer names"
ms.assetid: 160a6b29-5e80-44ab-80ec-77d4280f627c
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_addserver (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Defines the name of the local instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. When the computer hosting [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is renamed, use **sp_addserver** to inform the instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] of the new computer name. This procedure must be executed on all instances of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] hosted on the computer. The instance name of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] cannot be changed. To change the instance name of a named instance, install a new instance with the desired name, detach the database files from old instance, attach the databases to the new instance and drop the old instance. Alternatively, you can create a client alias name on the client computer, redirecting the connection to different server and instance name or **server:port** combination without changing the name of the instance on the server computer.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_addserver [ @server = ] 'server' ,  
     [ @local = ] 'local'   
     [ , [ @duplicate_ok = ] 'duplicate_OK' ]  
```  
  
## Arguments  
 [ **@server =** ] **'***server***'**  
 Is the name of the server. Server names must be unique and follow the rules for [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows computer names, although spaces are not allowed. *server* is **sysname**, with no default.  
  
 When multiple instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are installed on a computer, an instance operates as if it is on a separate server. Specify a named instance by referring to *server* as *servername\instancename*.  
  
 [ **@local =** ] **'LOCAL'**  
 Specifies that the server that is being added as a local server. **@local** is **varchar(10)**, with a default of NULL. Specifying **@local** as **LOCAL** defines **@server** as the name of the local server and causes the @@SERVERNAME function to return the value of *server*.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup sets this variable to the computer name during installation. By default, the computer name is the way users connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] without requiring additional configuration.  
  
 The local definition takes effect only after the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is restarted. Only one local server can be defined in each instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
 [ **@duplicate_ok =** ] **'duplicate_OK'**  
 Specifies whether a duplicate server name is allowed. **@duplicate_OK** is **varchar(13)**, with a default of NULL. **@duplicate_OK** can only have the value **duplicate_OK** or NULL. If **duplicate_OK** is specified and the server name that is being added already exists, no error is raised. If named parameters are not used, **@local** must be specified.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 To set or clear server options, use **sp_serveroption**.  
  
 **sp_addserver** cannot be used inside a user-defined transaction.  
  
 Using **sp_addserver** to add a remote server is discontinued. Use [sp_addlinkedserver](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md) instead.  
  
## Permissions  
 Requires membership in the **setupadmin** fixed server role.  
  
## Examples  
 The following example changes the [!INCLUDE[ssDE](../../includes/ssde-md.md)] entry for the name of the computer hosting [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to `ACCOUNTS`.  
  
```  
sp_addserver 'ACCOUNTS', 'local';  
```  
  
## See Also  
 [Rename a Computer that Hosts a Stand-Alone Instance of SQL Server](../../database-engine/install-windows/rename-a-computer-that-hosts-a-stand-alone-instance-of-sql-server.md)   
 [sp_addlinkedserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md)   
 [sp_dropserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropserver-transact-sql.md)   
 [sp_helpserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpserver-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)  
  
  
