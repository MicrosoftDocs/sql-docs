---
title: "sp_helptracertokens (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helptracertokens"
  - "sp_helptracertokens_TSQL"
helpviewer_keywords: 
  - "sp_helptracertokens"
ms.assetid: 61f27234-531d-4b37-8fa3-fe4c32e6f521
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helptracertokens (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns one row for each tracer token that has been inserted into a publication to determine latency. This stored procedure is executed at the Publisher on the publication database or at the Distributor on the distribution database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helptracertokens [ @publication = ] 'publication'   
    [ , [ @publisher = ] 'publisher' ]   
    [ , [ @publisher_db = ] 'publisher_db' ]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication in which tracer tokens were inserted. *publication* is **sysname**, with no default.  
  
`[ @publisher = ] 'publisher'`
 The name of the Publisher. *publisher* is **sysname**, with a default of NULL.  
  
> [!NOTE]
>  This parameter should only be specified for non- [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publishers.  
  
`[ @publisher_db = ] 'publisher_db'`
 The name of the publication database. *publisher_db* is **sysname**, with a default value of NULL. This parameter is ignored if the stored procedure is executed at the Publisher.  
  
## Result Set  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**tracer_id**|**int**|Identifies a tracer token record.|  
|**publisher_commit**|**datetime**|The date and time that the token record was committed at the Publisher in the publication database.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_helptracertokens** is used in transactional replication.  
  
 **sp_helptracertokens** is used to obtain tracer token IDs when executing [sp_helptracertokenhistory &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helptracertokenhistory-transact-sql.md).  
  
## Example  
 [!code-sql[HowTo#sp_tracertokens](../../relational-databases/replication/codesnippet/tsql/sp-helptracertokens-tran_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role, the **db_owner** fixed database role in the publication database, or **db_owner** fixed database or **replmonitor** roles in the distribution database can execute **sp_helptracertokenhistory**.  
  
## See Also  
 [Measure Latency and Validate Connections for Transactional Replication](../../relational-databases/replication/monitor/measure-latency-and-validate-connections-for-transactional-replication.md)   
 [sp_deletetracertokenhistory &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-deletetracertokenhistory-transact-sql.md)  
  
  
