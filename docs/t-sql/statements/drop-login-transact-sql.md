---
title: "DROP LOGIN (Transact-SQL)"
description: DROP LOGIN (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "05/11/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP LOGIN"
  - "DROP_LOGIN_TSQL"
helpviewer_keywords:
  - "deleting login accounts"
  - "logins [SQL Server], removing"
  - "DROP LOGIN statement"
  - "removing login accounts"
  - "dropping login accounts"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DROP LOGIN (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Removes a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login account.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
DROP LOGIN login_name  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *login_name*  
 Specifies the name of the login to be dropped.  
  
## Remarks  
 A login cannot be dropped while it is logged in. A login that owns any securable, server-level object, or SQL Server Agent job cannot be dropped.  
  
 You can drop a login to which database users are mapped; however, this will create orphaned users. For more information, see [Troubleshoot Orphaned Users &#40;SQL Server&#41;](../../sql-server/failover-clusters/troubleshoot-orphaned-users-sql-server.md).  
  
 In [!INCLUDE[ssSDS](../../includes/sssds-md.md)], login data required to authenticate a connection and server-level firewall rules are temporarily cached in each database. This cache is periodically refreshed. To force a refresh of the authentication cache and make sure that a database has the latest version of the logins table, execute [DBCC FLUSHAUTHCACHE &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-flushauthcache-transact-sql.md).  
  
## Permissions  
 Requires ALTER ANY LOGIN permission on the server.  
  
## Examples  
  
### A. Dropping a login  
 The following example drops the login `WilliJo`.  
  
```sql  
DROP LOGIN WilliJo;  
GO 
```  
 
  
## See Also  
 [CREATE LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/create-login-transact-sql.md)   
 [ALTER LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/alter-login-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  

