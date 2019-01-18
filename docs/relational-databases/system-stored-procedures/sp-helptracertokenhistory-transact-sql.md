---
title: "sp_helptracertokenhistory (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helptracertokenhistory_TSQL"
  - "sp_helptracertokenhistory"
helpviewer_keywords: 
  - "sp_helptracertokenhistory"
ms.assetid: 96910d1c-be76-43eb-9c93-4477e6761749
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helptracertokenhistory (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns detailed latency information for specified tracer tokens, with one row being returned for each Subscriber. This stored procedure is executed at the Publisher on the publication database or at the Distributor on the distribution database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helptracertokenhistory [ @publication = ] 'publication'   
        , [ @tracer_id = ] tracer_id  
    [ , [ @publisher = ] 'publisher' ]  
    [ , [ @publisher_db = ] 'publisher_db' ]  
```  
  
## Arguments  
 [ **@publication=** ] **'***publication***'**  
 Is the name of the publication in which the tracer token was inserted. *publication* is **sysname**, with no default.  
  
 [ **@tracer_id=** ] *tracer_id*  
 Is the ID of the tracer token in the [MStracer_tokens &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mstracer-tokens-transact-sql.md) table for which history information is returned. *tracer_id* is **int**, with no default.  
  
 [ **@publisher=** ] **'***publisher***'**  
 The name of the Publisher. *publisher* is **sysname**, with a default of NULL.  
  
> [!NOTE]
>  This parameter should only be specified for non- [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publishers.  
  
 [ **@publisher_db=** ] **'***publisher_db***'**  
 The name of the publication database. *publisher_db* is **sysname**, with a default value of NULL. This parameter is ignored if the stored procedure is executed at the Publisher.  
  
## Result Set  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**distributor_latency**|**bigint**|Number of seconds between the tracer token record being committed at the Publisher and the record being committed at the Distributor.|  
|**subscriber**|**sysname**|Name of the Subscriber that received the tracer token.|  
|**subscriber_db**|**sysname**|Name of the subscription database into which the tracer token record was inserted.|  
|**subscriber_latency**|**bigint**|Number of seconds between the tracer token record being committed at the Distributor and the record being committed at the Subscriber.|  
|**overall_latency**|**bigint**|Number of seconds between the tracer token record being committed at the Publisher and token record being committed at the Subscriber.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_helptracertokenhistory** is used in transactional replication.  
  
 Execute [sp_helptracertokens &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helptracertokens-transact-sql.md) to obtain a list of tracer tokens for the publication.  
  
 A value of NULL in the result set means that latency statistics cannot be calculated. This is because the tracer token has not been received at the Distributor or one of the Subscribers.  
  
## Example  
 [!code-sql[HowTo#sp_tracertokens](../../relational-databases/replication/codesnippet/tsql/sp-helptracertokenhistor_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role, the **db_owner** fixed database role in the publication database, or **db_owner** fixed database or **replmonitor** roles in the distribution database can execute **sp_helptracertokenhistory**.  
  
## See Also  
 [Measure Latency and Validate Connections for Transactional Replication](../../relational-databases/replication/monitor/measure-latency-and-validate-connections-for-transactional-replication.md)   
 [sp_deletetracertokenhistory &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-deletetracertokenhistory-transact-sql.md)  
  
  
