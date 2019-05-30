---
title: "sp_publication_validation (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_publication_validation"
  - "sp_publication_validation_TSQL"
helpviewer_keywords: 
  - "sp_publication_validation"
ms.assetid: 06be2363-00c0-4936-97c1-7347f294a936
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_publication_validation (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Initiates an article validation request for each article in the specified publication. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_publication_validation [ @publication = ] 'publication'  
    [ , [ @rowcount_only = ] type_of_check_requested ]  
    [ , [ @full_or_fast = ] full_or_fast ]  
    [ , [ @shutdown_agent = ] shutdown_agent ]  
    [ , [ @publisher = ] 'publisher' ]  
```  
  
## Arguments  
 [**@publication=**] **'**_publication'_  
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
 [**@rowcount_only=**] *rowcount_only*  
 Is whether to return only the rowcount for the table. *rowcount_only* is **smallint** and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**0**|Perform a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 7.0 compatible checksum.<br /><br /> Note: When an article is horizontally filtered, a rowcount operation is performed instead of a checksum operation.|  
|**1** (default)|Perform a rowcount check only.|  
|**2**|Perform a rowcount and binary checksum.<br /><br /> Note: For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version 7.0 Subscribers, only a rowcount validation is performed.|  
  
 [**@full_or_fast=**] *full_or_fast*  
 Is the method used to calculate the rowcount. *full_or_fast* is **tinyint** and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**0**|Does full count using COUNT(*).|  
|**1**|Does fast count from **sysindexes.rows**. Counting rows in [sys.sysindexes](../../relational-databases/system-compatibility-views/sys-sysindexes-transact-sql.md) is much faster than counting rows in the actual table. However, because [sys.sysindexes](../../relational-databases/system-compatibility-views/sys-sysindexes-transact-sql.md) is lazily updated, the rowcount may not be accurate.|  
|**2** (default)|Does conditional fast counting by first trying the fast method. If fast method shows differences, reverts to full method. If *expected_rowcount* is NULL and the stored procedure is being used to get the value, a full COUNT(*) is always used.|  
  
`[ @shutdown_agent = ] shutdown_agent`
 Is whether the Distribution Agent should shut down immediately upon completion of the validation. *shutdown_agent* is **bit**, with a default of **0**. If **0**, the replication agent does not shut down. If **1**, the replication agent shuts down after the last article is validated.  
  
`[ @publisher = ] 'publisher'`
 Specifies a non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *publisher* is **sysname**, with a default of NULL.  
  
> [!NOTE]  
>  *publisher* should not be used when requesting validation on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_publication_validation** is used in transactional replication.  
  
 **sp_publication_validation** can be called at any time after the articles associated with the publication have been activated. The procedure can be run manually (one time) or as part of a regularly scheduled job that validates the data.  
  
 If your application has immediate-updating Subscribers, **sp_publication_validation** may detect spurious errors. **sp_publication_validation** first calculates the rowcount or checksum at the Publisher and then at the Subscriber. Because the immediate-updating trigger could propagate an update from the Subscriber to the Publisher after the rowcount or checksum is completed at the Publisher, but before the rowcount or checksum is completed at the Subscriber, the values could change. To ensure that the values at the Subscriber and Publisher do not change while validating a publication, stop the Microsoft Distributed Transaction Coordinator (MS DTC) service at the Publisher during validation.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute **sp_publication_validation**.  
  
## See Also  
 [Validate Data at the Subscriber](../../relational-databases/replication/validate-data-at-the-subscriber.md)   
 [sp_article_validation &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-article-validation-transact-sql.md)   
 [sp_table_validation &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-table-validation-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
