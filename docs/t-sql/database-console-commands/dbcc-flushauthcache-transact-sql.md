---
title: "DBCC FLUSHAUTHCACHE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/16/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""

ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DBCC FLUSHAUTHCACHE"
  - "FLUSHAUTHCACHE"
  - "DBCC_FLUSHAUTHCACHE_TSQL"
  - "FLUSHAUTHCACHE_TSQL"
helpviewer_keywords: 
  - "DBCC FLUSHAUTHCACHE"
ms.assetid: 681ef31d-ceb9-4da5-86bf-bf1240df950f
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: "= azuresqldb-current || = sqlallproducts-allversions"
---
# DBCC FLUSHAUTHCACHE (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md)]

Empties the database authentication cache containing information about logins  and firewall rules,  for the current user database in [!INCLUDE[ssSDS](../../includes/sssds-md.md)]. This statement does not apply to the logical master database, because the master database contains the physical storage for the information about logins and firewall rules. The user executing the statement and other currently connected users remain connected. (DBCC FLUSHAUTHCACHE is not currently supported for [!INCLUDE[ssSDW_md](../../includes/sssdw-md.md)].)
 
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
DBCC FLUSHAUTHCACHE [ ; ]  
```  
  
## Arguments  
None.
  
## Remarks  
The authentication cache makes a copy of logins and server firewall rules that are stored in master and places them in memory in the user database.  Since information about contained database users are already stored in the user database, contained database users are not part of the authentication cache.
Continuously active connections to [!INCLUDE[ssSDS](../../includes/sssds-md.md)] require reauthorization (performed by the [!INCLUDE[ssDE](../../includes/ssde-md.md)]) at least every 10 hours. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] attempts reauthorization using the originally submitted password and no user input is required. For performance reasons, when a password is reset in [!INCLUDE[ssSDS](../../includes/sssds-md.md)], the connection will not be re-authenticated, even if the connection is reset due to connection pooling. This is different from the behavior of on-premises [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If the password has been changed since the connection was initially authorized, the connection must be terminated and a new connection made using the new password. A user with the KILL DATABASE CONNECTION permission can explicitly terminate a connection to [!INCLUDE[ssSDS](../../includes/sssds-md.md)] by using the [KILL &#40;Transact-SQL&#41;](../../t-sql/language-elements/kill-transact-sql.md) command.
  
## Permissions  
Requires the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] admin account.
  
## Example  
The following statement clears the authentication cache for the current database.
  
```sql
DBCC FLUSHAUTHCACHE;  
```  
  
## See Also  
[DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md)
  
  
