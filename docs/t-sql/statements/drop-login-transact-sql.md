---
title: "DROP LOGIN (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/11/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DROP LOGIN"
  - "DROP_LOGIN_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "deleting login accounts"
  - "logins [SQL Server], removing"
  - "DROP LOGIN statement"
  - "removing login accounts"
  - "dropping login accounts"
ms.assetid: acb5c3dc-7aa2-49f6-9330-573227ba9b1a
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DROP LOGIN (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Removes a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login account.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
DROP LOGIN login_name  
```  
  
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
  
```  
DROP LOGIN WilliJo;  
GO 
```  
 
  
## See Also  
 [CREATE LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/create-login-transact-sql.md)   
 [ALTER LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/alter-login-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  

