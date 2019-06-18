---
title: "sp_resyncmergesubscription (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_resyncmergesubscription_TSQL"
  - "sp_resyncmergesubscription"
helpviewer_keywords: 
  - "sp_resyncmergesubscription"
ms.assetid: e04d464a-60ab-4b39-a710-c066025708e6
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_resyncmergesubscription (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Resynchronizes a merge subscription to a known validation state that you specify. This allows you to force convergence or synchronize the subscription database to a specific point in time, such as the last time there was a successful validation, or to a specified date. The snapshot is not reapplied when resynchronizing a subscription using this method. This stored procedure is not used for snapshot replication subscriptions or transactional replication subscriptions. This stored procedure is executed at the Publisher, on the publication database, or at the Subscriber, on the subscription database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_resyncmergesubscription [ [ @publisher = ] 'publisher' ]  
    [ , [ @publisher_db = ] 'publisher_db' ]  
        , [ @publication = ] 'publication'   
    [ , [ @subscriber = ] 'subscriber' ]  
    [ , [ @subscriber_db = ] 'subscriber_db' ]  
    [ , [ @resync_type = ] resync_type ]  
    [ , [ @resync_date_str = ] resync_date_string ]  
```  
  
## Arguments  
`[ @publisher = ] 'publisher'`
 Is the name of the Publisher. *publisher* is **sysname**, with a default of NULL. A value of NULL is valid if the stored procedure is run at the Publisher. If the stored procedure is run at the Subscriber, a Publisher must be specified.  
  
`[ @publisher_db = ] 'publisher_db'`
 Is the name of the publication database. *publisher_db* is **sysname**, with a default of NULL. A value of NULL is valid if the stored procedure is run at the Publisher in the publication database. If the stored procedure is run at the Subscriber, a Publisher must be specified.  
  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication*is **sysname**, with no default.  
  
`[ @subscriber = ] 'subscriber'`
 Is the name of the Subscriber. *subscriber* is **sysname**, with a default of NULL. A value of NULL is valid if the stored procedure is run at the Subscriber. If the stored procedure is run at the Publisher, a Subscriber must be specified.  
  
`[ @subscriber_db = ] 'subscriber_db'`
 Is the name of the subscription database. *subscription_db* is **sysname**, with a default of NULL. A value of NULL is valid if the stored procedure is run at the Subscriber in the subscription database. If the stored procedure is run at the Publisher, a Subscriber must be specified.  
  
`[ @resync_type = ] resync_type`
 Defines when the resynchronization should start at. *resync_type* is **int**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**0**|Synchronization starts from after the initial snapshot. This is the most resource-intensive option, since all changes since the initial snapshot are reapplied to the Subscriber.|  
|**1**|Synchronization starts since last successful validation. All new or incomplete generations originating since the last successful validation are reapplied to the Subscriber.|  
|**2**|Synchronization starts from the date given in *resync_date_str*. All new or incomplete generations originating after the date are reapplied to the Subscriber|  
  
`[ @resync_date_str = ] resync_date_string`
 Defines the date when the resynchronization should start at. *resync_date_string* is **nvarchar(30)**, with a default of NULL. This parameter is used when the *resync_type* is a value of **2**. The date given is converted to its equivalent **datetime** value.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_resyncmergesubscription** is used in merge replication.  
  
 A value of **0** for the *resync_type* parameter, which reapplies all changes since the initial snapshot, may be resource-intensive, but possibly a lot less than a full reinitialization. For example, if the initial snapshot was delivered one month ago, this value would cause data from the past month to be reapplied. If the initial snapshot contained 1 gigabyte (GB) of data, but the amount of changes from the past month consisted of 2 megabytes (MB) of changed data, it would be more efficient to reapply the data than to reapply the full 1 GB snapshot.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute **sp_resyncmergesubscription**.  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
