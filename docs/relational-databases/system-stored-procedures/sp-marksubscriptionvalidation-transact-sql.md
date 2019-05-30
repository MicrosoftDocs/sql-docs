---
title: "sp_marksubscriptionvalidation (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_marksubscriptionvalidation"
  - "sp_marksubscriptionvalidation_TSQL"
helpviewer_keywords: 
  - "sp_marksubscriptionvalidation"
ms.assetid: e68fe0b9-5993-4880-917a-b0f661f8459b
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_marksubscriptionvalidation (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Marks the current open transaction to be a subscription-level validation transaction for the specified subscriber. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_marksubscriptionvalidation [ @publication = ] 'publication'  
        , [ @subscriber = ] 'subscriber'  
        , [ @destination_db = ] 'destination_db'  
    [ , [ @publisher = ] 'publisher' ]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
`[ @subscriber = ] 'subscriber'`
 Is the name of the Subscriber. *subscriber* is sysname, with no default.  
  
`[ @destination_db = ] 'destination_db'`
 Is the name of the destination database. *destination_db* is **sysname**, with no default.  
  
`[ @publisher = ] 'publisher'`
 Specifies a non- [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *publisher* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  *publisher* should not be used for a publication that belongs to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_marksubscriptionvalidation** is used in transactional replication.  
  
 **sp_marksubscriptionvalidation** does not support non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers.  
  
 For non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publishers, you cannot execute **sp_marksubscriptionvalidation** from within an explicit transaction. This is because explicit transactions are not supported over the linked server connection used to access the Publisher.  
  
 **sp_marksubscriptionvalidation** must be used together with [sp_article_validation &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-article-validation-transact-sql.md), specifying a value of **1** for *subscription_level*, and can be used with other calls to **sp_marksubscriptionvalidation** to mark the current open transaction for other subscribers.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_marksubscriptionvalidation**.  
  
## Example  
 The following query can be applied to the publishing database to post subscription-level validation commands. These commands are picked up by the Distribution Agents of specified Subscribers. Note that the first transaction validates article '**art1**', while the second transaction validates '**art2**'. Also note that the calls to **sp_marksubscriptionvalidation** and [sp_article_validation &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-article-validation-transact-sql.md) have been encapsulated in a transaction. We recommend only one call to [sp_article_validation &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-article-validation-transact-sql.md) per transaction. This is because [sp_article_validation &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-article-validation-transact-sql.md) holds a shared table lock on the source table for the duration of the transaction. You should keep the transaction short to maximize concurrency.  
  
```  
begin tran  
  
exec sp_marksubscriptionvalidation @publication = 'pub1',  
 @subscriber = 'Sub', @destination_db = 'SubDB'  
  
exec sp_marksubscriptionvalidation @publication = 'pub1',  
 @subscriber = 'Sub2', @destination_db = 'SubDB'  
  
exec sp_article_validation @publication = 'pub1', @article = 'art1',  
 @rowcount_only = 0, @full_or_fast = 0, @shutdown_agent = 0,  
 @subscription_level = 1  
  
commit tran  
  
begin tran  
  
exec sp_marksubscriptionvalidation @publication = 'pub1',  
 @subscriber = 'Sub', @destination_db = 'SubDB'  
  
exec sp_marksubscriptionvalidation @publication = 'pub1',  
 @subscriber = 'Sub2', @destination_db = 'SubDB'  
  
exec sp_article_validation @publication = 'pub1', @article = 'art2',  
 @rowcount_only = 0, @full_or_fast = 0, @shutdown_agent = 0,  
 @subscription_level = 1  
  
commit tran  
```  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Validate Replicated Data](../../relational-databases/replication/validate-data-at-the-subscriber.md)  
  
  
