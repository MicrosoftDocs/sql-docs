---
title: "sp_deletetracertokenhistory (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
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
 [ **@publication=** ] **'***publication***'**  
 Is the name of the publication in which the tracer token was inserted. *publication* is **sysname**, with no default.  
  
 [ **@tracer_id=** ] *tracer_id*  
 Is the ID of the tracer token to delete. *tracer_id* is **int**, with a default value of NULL. If **null**, then all tracer tokens belonging to the publication are deleted.  
  
 [ **@cutoff_date=** ] *cutoff_date*  
 Specifies a cutoff date such that all tracer tokens inserted into the publication before that date are removed. *cutoff_date* is datetime, with a default value of NULL.  
  
 [ **@publisher=** ] **'***publisher***'**  
 The name of the Publisher. *publisher* is **sysname**, with a default of NULL.  
  
> [!NOTE]
>  This parameter should only be specified for non- [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publishers or when executing the stored procedure from distributor.  
  
 [ **@publisher_db=** ] **'***publisher_db***'**  
 The name of the publication database. *publisher_db* is **sysname**, with a default value of NULL. This parameter is ignored if the stored procedure is executed at the Publisher.  
  
> [!NOTE]
>  This parameter should be specified when executing the stored procedure from distributor.  

## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_deletetracertokenhistory** is used in transactional replication.  
  
 When executing **sp_deletetracertokenhistory**, you can only specify one of *tracer_id* or *cutoff_date*. An error occurs when you specify both parameters.  
  
 If you do not execute **sp_deletetracertokenhistory** to remove tracer token metadata, the information will be removed when the regularly scheduled history cleanup occurs.  
  
 Tracer token IDs can be determined by executing [sp_helptracertokens &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helptracertokens-transact-sql.md) or by querying the [MStracer_tokens &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mstracer-tokens-transact-sql.md) system table.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role, the **db_owner** fixed database role in the publication database, or **db_owner** fixed database or **replmonitor** roles in the distribution database can execute **sp_deletetracertokenhistory**.  
  
## See Also  
 [Measure Latency and Validate Connections for Transactional Replication](../../relational-databases/replication/monitor/measure-latency-and-validate-connections-for-transactional-replication.md)   
 [sp_helptracertokenhistory &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helptracertokenhistory-transact-sql.md)  
  
  
