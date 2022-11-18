---
description: "sp_addmergepartition (Transact-SQL)"
title: "sp_addmergepartition (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_addmergepartition"
  - "sp_addmergepartition_TSQL"
helpviewer_keywords: 
  - "sp_addmergepartition"
ms.assetid: 02a5f46b-e5ff-4932-a3ff-7f0fd82d0981
author: markingmyname
ms.author: maghan
---
# sp_addmergepartition (Transact-SQL)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  Creates a dynamically filtered partition for a subscription that is filtered by the values of [HOST_NAME](../../t-sql/functions/host-name-transact-sql.md) or [SUSER_SNAME](../../t-sql/functions/suser-sname-transact-sql.md) at the Subscriber. This stored procedure is executed at the Publisher on the database that is being published, and is used to manually generate partitions.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_addmergepartition [ @publication = ] 'publication'  
        , [ @suser_sname = ] 'suser_sname'  
        , [ @host_name = ] 'host_name'  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the merge publication on which the partition is being created. *publication* is **sysname**, with no default. If *suser_sname* is specified, the value of *hostname* must be NULL.  
  
`[ @suser_sname = ] 'suser_sname'`
 Is the value used when creating the partition for a subscription that is filtered by the value of the [SUSER_SNAME](../../t-sql/functions/suser-sname-transact-sql.md) function at the Subscriber. *suser_sname* is **sysname**, with no default.  
  
`[ @host_name = ] 'host_name'`
 Is the value used when creating the partition for a subscription that is filtered by the value of the [HOST_NAME](../../t-sql/functions/host-name-transact-sql.md) function at the Subscriber. *host_name* is **sysname**, with no default.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_addmergepartition** is used in merge replication.  
  
## Example  
 [!code-sql[HowTo#sp_MergeDynamicPubPlusPartition](../../relational-databases/replication/codesnippet/tsql/sp-addmergepartition-tra_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_addmergepartition**.  
  
## See Also  
 [Create a Snapshot for a Merge Publication with Parameterized Filters](../../relational-databases/replication/create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md)   
 [Parameterized Row Filters](../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md)  
  
  
