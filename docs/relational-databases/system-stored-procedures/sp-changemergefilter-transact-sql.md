---
title: "sp_changemergefilter (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_changemergefilter_TSQL"
  - "sp_changemergefilter"
helpviewer_keywords: 
  - "sp_changemergefilter"
ms.assetid: e08fdfdd-d242-4e85-817b-9f7a224fe567
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_changemergefilter (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Changes some merge filter properties. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_changemergefilter [ @publication= ] 'publication'  
        , [ @article= ] 'article'  
        , [ @filtername= ] 'filtername'  
        , [ @property= ] 'property'  
        , [ @value= ] 'value'  
    [ , [ @force_invalidate_snapshot = ] force_invalidate_snapshot ]  
    [ , [ @force_reinit_subscription = ] force_reinit_subscription ]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
`[ @article = ] 'article'`
 Is the name of the article. *article* is **sysname**, with no default.  
  
`[ @filtername = ] 'filtername'`
 Is the current name of the filter. *filtername* is **sysname**, with no default.  
  
`[ @property = ] 'property'`
 Is the name of the property to change. *property* is **sysname**, with no default.  
  
`[ @value = ] 'value'`
 Is the new value for the specified property. *value*is **nvarchar(1000)**, with no default.  
  
 This table describes the properties of articles and the values for those properties.  
  
|Property|Value|Description|  
|--------------|-----------|-----------------|  
|**filter_type**|**1**|Join filter.<br /><br /> This option is required to support [!INCLUDE[ssEW](../../includes/ssew-md.md)] Subscribers.|  
||**2**|Logical record relationship.|  
||**3**|Join filter is also a logical record relationship.|  
|**filtername**||Name of the filter.|  
|**join_articlename**||Name of the join article.|  
|**join_filterclause**||Filter clause.|  
|**join_unique_key**|**true**|Join is on a unique key|  
||**false**|Join is not on a unique key.|  
  
`[ @force_invalidate_snapshot = ] force_invalidate_snapshot`
 Acknowledges that the action taken by this stored procedure may invalidate an existing snapshot. *force_invalidate_snapshot* is a **bit**, with a default **0**.  
  
 **0** specifies that changes to the merge article do not cause the snapshot to be invalid. If the stored procedure detects that the change does require a new snapshot, an error occurs and no changes are made.  
  
 **1** means that changes to the merge article may cause the snapshot to be invalid, and if there are existing subscriptions that would require a new snapshot, gives permission for the existing snapshot to be marked as obsolete and a new snapshot generated.  
  
`[ @force_reinit_subscription = ] force_reinit_subscription`
 Acknowledges that the action taken by this stored procedure may require existing subscriptions to be reinitialized. *force_reinit_subscription* is a **bit** with a default of **0**.  
  
 **0** specifies that changes to the merge article do not cause the subscription to be reinitialized. If the stored procedure detects that the change would require existing subscriptions to be reinitialized, an error occurs and no changes are made.  
  
 **1** means that changes to the merge article will cause existing subscriptions to be reinitialized, and gives permission for the subscription reinitialization to occur.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_changemergefilter** is used in merge replication.  
  
 Changing the filter on a merge article requires the snapshot, if one exists, to be recreated. This is performed by setting the **@force_invalidate_snapshot** to **1**. Also, if there are subscriptions to this article, the subscriptions need to be reinitialized. This is done by setting the **@force_reinit_subscription** to **1**.  
  
 To use logical records, the publication and articles must meet a number of requirements. For more information, see [Group Changes to Related Rows with Logical Records](../../relational-databases/replication/merge/group-changes-to-related-rows-with-logical-records.md).  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_changemergefilter**.  
  
## See Also  
 [Change Publication and Article Properties](../../relational-databases/replication/publish/change-publication-and-article-properties.md)   
 [sp_addmergefilter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addmergefilter-transact-sql.md)   
 [sp_dropmergefilter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropmergefilter-transact-sql.md)   
 [sp_helpmergefilter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpmergefilter-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
