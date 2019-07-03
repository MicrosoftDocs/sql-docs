---
title: "sp_replicationdboption (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_replicationdboption_TSQL"
  - "sp_replicationdboption"
helpviewer_keywords: 
  - "sp_replicationdboption"
ms.assetid: d021864e-3f21-4d1a-89df-6c1086f753bf
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# sp_replicationdboption (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Sets a replication database option for the specified database. This stored procedure is executed at the Publisher or Subscriber on any database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_replicationdboption [ @dbname= ] 'db_name'   
        , [ @optname= ] 'optname'   
        , [ @value= ] 'value'   
    [ , [ @ignore_distributor= ] ignore_distributor ]  
    [ , [ @from_scripting = ] from_scripting ]  
```  
  
## Arguments  
 [**@dbname=**] **'***dbname***'**  
 Is the database for which the replication database option is being set. *db_name* is **sysname**, with no default.  
  
 [**@optname=**] **'***optname***'**  
 Is the replication database option to enable or disable. *optname* is **sysname**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**merge publish**|Database can be used for merge publications.|  
|**publish**|Database can be used for other types of publications.|  
|**subscribe**|Database is a subscription database.|  
|**sync with backup**|Database is enabled for coordinated backup. For more information, see [Enable Coordinated Backups for Transactional Replication &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/administration/enable-coordinated-backups-for-transactional-replication.md).|  
  
`[ @value = ] 'value'`
 Is whether to enable or disable the given replication database option. *value* is **sysname**, and can be **true** or **false**. When this value is **false** and *optname* is **merge publish**, subscriptions to the merge published database are also dropped.  
  
`[ @ignore_distributor = ] ignore_distributor`
 Indicates whether this stored procedure is executed without connecting to the Distributor. *ignore_distributor* is **bit**, with a default of **0**, meaning the Distributor should be connected to and updated with the new status of the publishing database. The value **1** should be specified only if the Distributor is inaccessible and **sp_replicationdboption** is being used to disable publishing.  
  
`[ @from_scripting = ] from_scripting`
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_replicationdboption** is used in snapshot replication, transactional replication, and merge replication.  
  
 This procedure creates or drops specific replication system tables, security accounts, and so on, depending on the options given. Sets the corresponding category bit in the **master.sysdatabases** system table and creates the necessary system tables.  
  
 To disable publishing, the publication database must be online. If a database snapshot exists for the publication database, it must be dropped before disabling publishing. A database snapshot is a read-only offline copy of a database, and is not related to a replication snapshot. For more information, see [Database Snapshots &#40;SQL Server&#41;](../../relational-databases/databases/database-snapshots-sql-server.md).  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_replicationdboption**.  
  
## See Also  
 [Configure Publishing and Distribution](../../relational-databases/replication/configure-publishing-and-distribution.md)   
 [Create a Publication](../../relational-databases/replication/publish/create-a-publication.md)   
 [Delete a Publication](../../relational-databases/replication/publish/delete-a-publication.md)   
 [Disable Publishing and Distribution](../../relational-databases/replication/disable-publishing-and-distribution.md)   
 [sys.sysdatabases &#40;Transact-SQL&#41;](../../relational-databases/system-compatibility-views/sys-sysdatabases-transact-sql.md)   
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
