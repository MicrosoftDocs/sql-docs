---
title: "sp_copysubscription (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_copysubscription"
  - "sp_copysubscription_TSQL"
helpviewer_keywords: 
  - "sp_copysubscription"
ms.assetid: 3c56cd62-2966-4e87-a986-44cb3fd0b760
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_copysubscription (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

    
> [!IMPORTANT]  
>  The attachable subscriptions feature is deprecated and will be removed in a future release. This feature should not be used in new development work. For merge publications that are partitioned using parameterized filters, we recommend using the new features of partitioned snapshots, which simplify the initialization of a large number of subscriptions. For more information, see [Snapshots for Merge Publications with Parameterized Filters](../../relational-databases/replication/create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md). For publications that are not partitioned, you can initialize a subscription with a backup. For more information, see [Initialize a Transactional Subscription Without a Snapshot](../../relational-databases/replication/initialize-a-transactional-subscription-without-a-snapshot.md).  
  
 Copies a subscription database that has pull subscriptions, but no push subscriptions. Only single file databases can be copied. This stored procedure is executed at the Subscriber on the subscription database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_copysubscription [ @filename = ] 'file_name'  
    [ , [ @temp_dir = ] 'temp_dir' ]  
    [ , [ @overwrite_existing_file = ] overwrite_existing_file]  
```  
  
## Arguments  
`[ @filename = ] 'file_name'`
 Is the string that specifies the complete path, including file name, to which a copy of the data file (.mdf) is saved. *file name* is **nvarchar(260)**, with no default.  
  
`[ @temp_dir = ] 'temp_dir'`
 Is the name of the directory that contains the temp files. *temp_dir* is **nvarchar(260)**, with a default of NULL. If NULL, the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] default data directory will be used. The directory should have enough space to hold a file the size of all the subscriber database files combined.  
  
`[ @overwrite_existing_file = ] 'overwrite_existing_file'`
 Is an optional Boolean flag that specifies whether or not to overwrite an existing file of the same name specified in **@filename**. *overwrite_existing_file*is **bit**, with a default of **0**. If **1**, it overwrites the file specified by **@filename**, if it exists. If **0**, the stored procedure fails if the file exists, and the file is not overwritten.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_copysubscription** is used in all types of replication to copy a subscription database to a file as an alternative to applying a snapshot at the Subscriber. The database must be configured to support only pull subscriptions. Users having appropriate permissions can make copies of the subscription database and then e-mail, copy, or transport the subscription file (.msf) to another Subscriber, where it can then be attached as a subscription.  
  
 The size of the subscription database being copied must be less than 2 gigabytes (GB).  
  
 **sp_copysubscription** is only supported for databases with client subscriptions and cannot be executed when the database has server subscriptions.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_copysubscription**.  
  
## See Also  
 [Alternate Snapshot Folder Locations](../../relational-databases/replication/snapshot-options.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
