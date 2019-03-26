---
title: "sp_deletetracertokenhistory (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_deletetracertokenhistory"
  - "sp_deletetracertokenhistory_TSQL"
helpviewer_keywords: 
  - "sp_deletetracertokenhistory"
ms.assetid: 9ae1be14-0d2f-40b1-9d6e-22d79726abf4
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_deletetracertokenhistory (Transact-SQL)

[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

Removes tracer token records from the [MStracer_tokens &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mstracer-tokens-transact-sql.md) and [MStracer_history &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mstracer-history-transact-sql.md) system tables. This stored procedure is executed at the Publisher on the publication database or at the Distributor on the distribution database.

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```
sp_deletetracertokenhistory [ @publication = ] 'publication'
    [ , [ @tracer_id = ] tracer_id ]
    [ , [ @cutoff_date = ] cutoff_date ]
    [ , [ @publisher = ] 'publisher' ]
    [ , [ @publisher_db = ] 'publisher_db' ]
```

## Arguments

`@publication= 'publication'`  
Is the name of the publication in which the tracer token was inserted. The data type is **sysname**. This parameter is required.

`[ @tracer_id= ] tracer_id`  
Is the ID of the tracer token to delete. The data type is **int**. The default value is *null*. If *null*, all tracer tokens belonging to the publication are deleted.

`[ @cutoff_date= ] cutoff_date`  
Tracer tokens inserted into the publication before this date are deleted. The data type is **datetime**. The default value is *null*.

`[ @publisher= ] 'publisher'`  
Is the name of the Publisher. The data type is **sysname**. The default value is *null*.

> [!NOTE]
> This parameter should only be specified for non- [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publishers or when executing the stored procedure from distributor.

`[ @publisher_db= ] 'publisher_db'`  
Is the name of the publication database. The data type is **sysname**. The default value is NULL. This parameter is ignored if the stored procedure is executed at the Publisher.

> [!NOTE]
> This parameter should be specified when executing the stored procedure from distributor.

## Return Code Values

**0** (success) or **1** (failure)

## Remarks

**sp_deletetracertokenhistory** is used in transactional replication.  

An error occurs if you specify both parameters *tracer_id* and *cutoff_date*.

If you do not execute **sp_deletetracertokenhistory** to delete tracer token metadata, the information will be deleted when the regularly scheduled history cleanup occurs.

Tracer token IDs can be determined by executing [sp_helptracertokens &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helptracertokens-transact-sql.md) or by querying the [MStracer_tokens &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mstracer-tokens-transact-sql.md) system table.

## Permissions

Only the following personnel have the authority to execute **sp_deletetracertokenhistory**:

- Members of the **replmonitor** roles, in the distribution database
- Members of the **sysadmin** fixed server role.
- Members of the **db_owner** fixed database role, in the publication database.
- The **db_owner** of the fixed database.

## See Also

[Measure Latency and Validate Connections for Transactional Replication](../../relational-databases/replication/monitor/measure-latency-and-validate-connections-for-transactional-replication.md)

[sp_helptracertokenhistory &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helptracertokenhistory-transact-sql.md)
