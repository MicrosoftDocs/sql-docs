---
description: "sp_setnetname (Transact-SQL)"
title: "sp_setnetname (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_setnetname"
  - "sp_setnetname_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_setnetname"
ms.assetid: f416ba81-3835-4588-b0a3-2fe75589490e
author: markingmyname
ms.author: maghan
---
# sp_setnetname (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Sets the network names in **sys.servers** to their actual network computer names for remote instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This procedure can be used to enable execution of remote stored procedure calls to computers that have network names containing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] identifiers that are not valid.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_setnetname  
@server = 'server',   
     @netname = 'network_name'  
```  
  
## Arguments  
 **@server = '** *server* **'**  
 Is the name of the remote server as referenced in user-coded remote stored procedure call syntax. Exactly one row in **sys.servers** must already exist to use this *server*. *server* is **sysname**, with no default.  
  
 **@netname ='** *network_name* **'**  
 Is the network name of the computer to which remote stored procedure calls are made. *network_name* is **sysname**, with no default.  
  
 This name must match the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows computer name, and the name can include characters that are not allowed in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] identifiers.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 Some remote stored procedure calls to Windows computers can encounter problems if the computer name contains identifiers that are not valid.  
  
 Because linked servers and remote servers reside in the same namespace, they cannot have the same name. However, you can define both a linked server and a remote server against a specified server by assigning different names and by using **sp_setnetname** to set the network name of one of them to the network name of the underlying server.  
  
```  
--Assume sqlserv2 is actual name of SQL Server   
--database server  
EXEC sp_addlinkedserver 'sqlserv2';  
GO  
EXEC sp_addserver 'rpcserv2';  
GO  
EXEC sp_setnetname 'rpcserv2', 'sqlserv2';  
```  
  
> [!NOTE]  
>  Using **sp_setnetname** to point a linked server back to the local server is not supported. Servers that are referenced in this manner cannot participate in a distributed transaction.  
  
## Permissions  
 Requires membership in the **sysadmin** and **setupadmin** fixed server roles.  
  
## Examples  
 The following example shows a typical administrative sequence used on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to issue the remote stored procedure call.  
  
```  
USE master;  
GO  
EXEC sp_addserver 'Win_1';  
EXEC sp_setnetname 'Win_1','Win-1';  
EXEC Win_1.master.dbo.sp_who;  
```  
  
## See Also  
 [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)   
 [sp_addlinkedserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md)   
 [sp_addserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addserver-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
