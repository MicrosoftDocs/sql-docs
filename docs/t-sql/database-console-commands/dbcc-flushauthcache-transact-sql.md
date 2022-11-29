---
title: DBCC FLUSHAUTHCACHE (Transact-SQL)
description: "DBCC FLUSHAUTHCACHE (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.date: "07/16/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: "language-reference"
f1_keywords:
  - "DBCC FLUSHAUTHCACHE"
  - "FLUSHAUTHCACHE"
  - "DBCC_FLUSHAUTHCACHE_TSQL"
  - "FLUSHAUTHCACHE_TSQL"
helpviewer_keywords:
  - "DBCC FLUSHAUTHCACHE"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current"
---

# DBCC FLUSHAUTHCACHE (Transact-SQL)

[!INCLUDE[Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Empties the database authentication cache containing information about logins  and firewall rules,  for the current user database in [!INCLUDE[ssSDS](../../includes/sssds-md.md)].<br /> This statement doesn't apply to the logical master database, because the master database contains the physical storage for the information about logins and firewall rules.<br /> The user executing the statement and other currently connected users remain connected. (`DBCC FLUSHAUTHCACHE` isn't currently supported for [!INCLUDE[ssSDW_md](../../includes/sssdw-md.md)].)
 
![Article link icon](../../database-engine/configure-windows/media/topic-link.gif "Article link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
DBCC FLUSHAUTHCACHE [ ; ]  
```

## Arguments  
None.
  
## Remarks  
The authentication cache makes a copy of logins and server firewall rules stored in master, and places them in memory in the user database.  Since information about contained database users is already stored in the user database, contained database users aren't part of the authentication cache.<br />
Continuously active connections to [!INCLUDE[ssSDS](../../includes/sssds-md.md)] require reauthorization (performed by the [!INCLUDE[ssDE](../../includes/ssde-md.md)]) at least every 10 hours. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] attempts reauthorization using the originally submitted password and no user input is required. For performance reasons, when a password is reset in [!INCLUDE[ssSDS](../../includes/sssds-md.md)], the connection won't be reauthenticated, even if the connection is reset because of connection pooling. This behavior is different from the behavior of on-premises [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If the password has changed since the connection was initially authorized, the connection must be terminated and a new connection made using the new password. <br />
A user with the **KILL DATABASE CONNECTION** permission can explicitly terminate a connection to [!INCLUDE[ssSDS](../../includes/sssds-md.md)] by using the [KILL &#40;Transact-SQL&#41;](../../t-sql/language-elements/kill-transact-sql.md) command.
  
## Permissions  
Requires the **KILL DATABASE CONNECTION**-permission [!INCLUDE[ssSDS](../../includes/sssds-md.md)] or the admin account.
  
## Example  
The following statement clears the authentication cache for the current database.
  
```sql
DBCC FLUSHAUTHCACHE;  
```  
  
## See Also  
[DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md)
  
