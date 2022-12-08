---
description: "sp_redirect_publisher (Transact-SQL)"
title: "sp_redirect_publisher (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_redirect_publisher_TSQL"
  - "sp_redirect_publisher"
helpviewer_keywords: 
  - "sp_redirect_publisher"
ms.assetid: af45e2b2-57fb-4bcd-a58b-e61401fb3b26
author: markingmyname
ms.author: maghan
---
# sp_redirect_publisher (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Specifies a redirected publisher for an existing publisher/database pair. If the publisher database belongs to an Always On Availability Group, the redirected publisher is the availability group listener name associated with the availability group.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_redirect_publisher   
    [ @original_publisher = ] 'original_publisher',  
    [ @publisher_db = ] 'database_name'   
    [ , [ @redirected_publisher = ] 'new_publisher' ]  
```  
  
## Arguments  
`[ @original_publisher = ] 'original_publisher'`
 The name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that originally published the database. *original_publisher* is **sysname**, with no default.  
  
`[ @publisher_db = ] 'publisher_db'`
 The name of the database being published. *publisher_db* is **sysname**, with no default.  
  
`[ @redirected_publisher = ] 'redirected_publisher'`
 The availability group listener name associated with the availability group that will be the new publisher. *redirected_publisher* is **sysname**, with no default. When the availability group listener is configured to non-default port, specify the port number along with listener name, such as `'Listenername,51433'`  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 **sp_redirect_publisher** is used to allow a replication publisher to be redirected to the current primary of an Always On availability group by associating the publisher/database pair with an availability group's listener. Execute **sp_redirect_publisher** after the AG listener has been configured for the availability group that contains the published database.  
  
 If the publication database at the original publisher is removed from an availability group at the primary replica, execute **sp_redirect_publisher** without specifying a value for the *\@redirected_publisher* parameter to remove the redirection for the publisher/database pair. For more information about redirecting the publisher when, see [Maintaining an Always On Publication Database &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/maintaining-an-always-on-publication-database-sql-server.md).  
  
## Permissions  
 Caller must either be a member of the **sysadmin** fixed server role, the **db_owner** fixed database role for the distribution database, or a member of a publication access list for a defined publication associated with the publisher database.  
  
## See Also  
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)   
 [sp_validate_redirected_publisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-validate-redirected-publisher-transact-sql.md)   
 [sp_get_redirected_publisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-get-redirected-publisher-transact-sql.md)   
 [sp_validate_replica_hosts_as_publishers &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-validate-replica-hosts-as-publishers-transact-sql.md)  
  
  
