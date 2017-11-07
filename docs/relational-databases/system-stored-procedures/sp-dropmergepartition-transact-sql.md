---
title: "sp_dropmergepartition (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
applies_to: 
  - "SQL Server"
f1_keywords: 
  - "sp_dropmergepartition_TSQL"
  - "sp_dropmergepartition"
helpviewer_keywords: 
  - "sp_dropmergepartition"
ms.assetid: 1be511c1-79ff-4947-9379-78d83b7b8945
caps.latest.revision: 30
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# sp_dropmergepartition (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Removes a partition for a parameterized row filter from a publication. This stored procedure is executed at the Publisher on the publication database. This stored procedure also removes the corresponding snapshot job and snapshot files for the partition.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_dropmergepartition [ @publication = ] 'publication'  
        , [ @suser_sname = ] 'suser_sname'  
        , [ @host_name = ] 'host_name'  
```  
  
## Arguments  
 [ **@publication**] = **'***publication***'**  
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
 [ **@suser_sname**= ] **'***suser_sname***'**  
 Is the value of the [SUSER_SNAME](../../t-sql/functions/suser-sname-transact-sql.md) function at the Subscriber used to define the partition. *suser_sname* is **sysname**, with no default.  
  
 [ **@host_name** = ] **'***host_name***'**  
 Is the value of the [HOST_NAME](../../t-sql/functions/host-name-transact-sql.md) function at the Subscriber used to define the partition. *host_name* is **sysname**, with no default.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_dropmergepartition** is used in merge replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_dropmergepartition**.  
  
## See Also  
 [Manage Partitions for a Merge Publication with Parameterized Filters](../../relational-databases/replication/publish/manage-partitions-for-a-merge-publication-with-parameterized-filters.md)  
  
  