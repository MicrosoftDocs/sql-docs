---
title: "sp_article_validation (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_article_validation_TSQL"
  - "sp_article_validation"
helpviewer_keywords: 
  - "sp_article_validation"
ms.assetid: 44e7abcd-778c-4728-a03e-7e7e78d3ce22
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_article_validation (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Initiates a data validation request for the specified article. This stored procedure is executed at the Publisher on the publication database and at the Subscriber on the subscription database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_article_validation [ @publication = ] 'publication'  
    [ , [ @article = ] 'article' ]  
    [ , [ @rowcount_only = ] type_of_check_requested ]  
    [ , [ @full_or_fast = ] full_or_fast ]  
    [ , [ @shutdown_agent = ] shutdown_agent ]  
    [ , [ @subscription_level = ] subscription_level ]  
    [ , [ @reserved = ] reserved ]  
    [ , [ @publisher = ] 'publisher' ]  
```  
  
## Arguments  
 [ **@publication=**] **'**_publication_**'**  
 Is the name of the publication in which the article exists. *publication* is **sysname**, with no default.  
  
 [ **@article=**] **'**_article_**'**  
 Is the name of the article to validate. *article* is **sysname**, with no default.  
  
 [ **@rowcount_only=**] *type_of_check_requested*  
 Specifies if only the rowcount for the table is returned. *type_of_check_requested* is **smallint**, with a default of **1**.  
  
 If **0**, perform a rowcount and a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 7.0 compatible checksum.  
  
 If **1**, perform a rowcount check only.  
  
 If **2**, perform a rowcount and binary checksum.  
  
 [ **@full_or_fast=**] *full_or_fast*  
 Is the method used to calculate the rowcount. *full_or_fast* is **tinyint**, and can be one of these values.  
  
|**Value**|**Description**|  
|---------------|---------------------|  
|**0**|Performs full count using COUNT(*).|  
|**1**|Performs fast count from **sysindexes.rows**. Counting rows in **sysindexes** is faster than counting rows in the actual table. However, **sysindexes** is updated lazily, and the rowcount may not be accurate.|  
|**2** (default)|Performs conditional fast counting by first trying the fast method. If fast method shows differences, reverts to full method. If *expected_rowcount* is NULL and the stored procedure is being used to get the value, a full COUNT(*) is always used.|  
  
 [ **@shutdown_agent=**] *shutdown_agent*  
 Specifies if the Distribution agent should shut down immediately upon completion of the validation. *shutdown_agent* is **bit**, with a default of **0**. If **0**, the Distribution Agent does not shut down. If **1**, the Distribution Agent shuts down after the article is validated.  
  
 [ **@subscription_level=**] *subscription_level*  
 Specifies whether or not the validation is picked up by a set of subscribers. *subscription_level* is **bit**, with a default of **0**. If **0**, validation is applied to all Subscribers. If **1**, validation is only applied to a subset of the Subscribers specified by calls to **sp_marksubscriptionvalidation** in the current open transaction.  
  
 [ **@reserved=**] *reserved*  
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  
  
 [ **@publisher**= ] **'**_publisher_**'**  
 Specifies a non- [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *publisher* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  *publisher* should not be used when requesting validation on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_article_validation** is used in transactional replication.  
  
 **sp_article_validation** causes validation information to be gathered on the specified article and posts a validation request to the transaction log. When the Distribution Agent receives this request, the Distribution Agent compares the validation information in the request to the Subscriber table. The results of the validation are displayed in the Replication Monitor and in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent alerts.  
  
## Permissions  
 Only users with SELECT ALL permissions on the source table for the article being validated can execute **sp_article_validation**.  
  
## See Also  
 [Validate Replicated Data](../../relational-databases/replication/validate-data-at-the-subscriber.md)   
 [sp_marksubscriptionvalidation &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-marksubscriptionvalidation-transact-sql.md)   
 [sp_publication_validation &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-publication-validation-transact-sql.md)   
 [sp_table_validation &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-table-validation-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
