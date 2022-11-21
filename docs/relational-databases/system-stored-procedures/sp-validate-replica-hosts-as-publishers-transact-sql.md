---
title: "sp_validate_replica_hosts_as_publishers (T-SQL)"
description: Describes the sp_validate_replica_hosts_as_publishers stored procedure which allows all secondary replicas to be validated. 
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_validate_replica_hosts_as_publishers_TSQL"
  - "sp_validate_replica_hosts_as_publishers"
helpviewer_keywords: 
  - "sp_validate_replica_hosts_as_publishers"
ms.assetid: 45001fc9-2dbd-463c-af1d-aa8982d8c813
author: MashaMSFT
ms.author: mathoma
---
# sp_validate_replica_hosts_as_publishers (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  **sp_validate_replica_hosts_as_publishers** is an extension of **sp_validate_redirected_publisher** that allows all secondary replicas to be validated, rather than just the current primary replica. **sp_validate_replicat_hosts_as_publisher** validates an entire Always On replication topology. **sp_validate_replica_hosts_as_publishers** must be executed directly on the distributor by using a remote desktop session to avoid a double-hop security error (21892).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_validate_replica_hosts_as_publishers   
    [ @original_publisher = ] 'original_publisher',  
    [ @publisher_db = ] 'database_name',   
    [ @redirected_publisher = ] 'new_publisher' output  
```  
  
## Arguments  
`[ @original_publisher = ] 'original_publisher'`
 The name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that originally published the database. *original_publisher* is **sysname**, with no default.  
  
`[ @publisher_db = ] 'publisher_db'`
 The name of the database being published. *publisher_db* is **sysname**, with no default.  
  
`[ @redirected_publisher = ] 'redirected_publisher'`
 The target of redirection when **sp_redirect_publisher** was called for the original publisher/published database pair. *redirected_publisher* is **sysname**, with no default.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None.  
  
## Remarks  
 If no entry exists for the publisher and the publishing database, **sp_validate_redirected_publisher** returns null for the output parameter *\@redirected_publisher*. Otherwise, the associated redirected publisher is returned, both on success and failure.  
  
 If the validation succeeds, **sp_validate_redirected_publisher** returns a success indication.  
  
 If the validation fails, appropriate errors are raised.  **sp_validate_redirected_publisher** makes a best effort to raise all issues and not just the first encountered.  
  
> [!NOTE]  
>  **sp_validate_replica_hosts_as_publishers** will fail with the following error when validating secondary replica hosts that do not allow read access, or require read intent to be specified.  
>   
>  Msg 21899, Level 11, State 1, Procedure **sp_hadr_verify_subscribers_at_publisher**, Line 109  
>   
>  The query at the redirected publisher 'MyReplicaHostName' to determine whether there were sysserver entries for the subscribers of the original publisher 'MyOriginalPublisher' failed with error '976', error message 'Error 976, Level 14, State 1, Message: The target database, 'MyPublishedDB', is participating in an availability group and is currently not accessible for queries. Either data movement is suspended or the availability replica is not enabled for read access. To allow read-only access to this and other databases in the availability group, enable read access to one or more secondary availability replicas in the group.  For more information, see the **ALTER AVAILABILITY GROUP** statement in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.'.  
>   
>  One or more publisher validation errors were encountered for replica host 'MyReplicaHostName'.  
  
## Permissions  
 Caller must either be a member of the **sysadmin** fixed server role, the **db_owner** fixed database role for the distribution database, or a member of a publication access list for a defined publication associated with the publisher database.  
  
## See Also  
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)   
 [sp_get_redirected_publisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-get-redirected-publisher-transact-sql.md)   
 [sp_redirect_publisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-redirect-publisher-transact-sql.md)   
 [sp_validate_redirected_publisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-validate-redirected-publisher-transact-sql.md)  
  
  
