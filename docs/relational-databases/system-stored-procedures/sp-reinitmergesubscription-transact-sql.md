---
description: "sp_reinitmergesubscription (Transact-SQL)"
title: "sp_reinitmergesubscription (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_reinitmergesubscription_TSQL"
  - "sp_reinitmergesubscription"
helpviewer_keywords: 
  - "sp_reinitmergesubscription"
ms.assetid: 249a4048-e885-48e0-a92a-6577f59de751
author: markingmyname
ms.author: maghan
---
# sp_reinitmergesubscription (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Marks a merge subscription for reinitialization the next time the Merge Agent runs. This stored procedure is executed at the Publisher in the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_reinitmergesubscription [ [ @publication = ] 'publication'  
    [ , [ @subscriber = ] 'subscriber'  
    [ , [ @subscriber_db = ] 'subscriber_db'  
    [ , [ @upload_first = ] 'upload_first'  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with a default of **all**.  
  
`[ @subscriber = ] 'subscriber'`
 Is the name of the Subscriber. *subscriber* is **sysname**, with a default of **all**.  
  
`[ @subscriber_db = ] 'subscriber_db'`
 Is the name of the Subscriber database. *subscriber_db* is **sysname**, with a default of **all**.  
  
`[ @upload_first = ] 'upload_first'`
 Is whether changes at the Subscriber are uploaded before the subscription is reinitialized. *upload_first* is **nvarchar(5)**, with a default of FALSE. If **true**, changes are uploaded before the subscription is reinitialized. If **false**, changes are not uploaded.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_reinitmergesubscription** is used in merge replication.  
  
 **sp_reinitmergesubscription** can be called from the Publisher to reinitialize merge subscriptions. We recommend re-running the Snapshot Agent as well.  
  
 If you add, drop, or change a parameterized filter, pending changes at the Subscriber cannot be uploaded to the Publisher during reinitialization. If you want to upload pending changes, synchronize all subscriptions before changing the filter.  
  
## Examples  

### A. Reinitialize the push subscription and lose pending changes

 [!code-sql[HowTo#sp_reinitmergepushsub](../../relational-databases/replication/codesnippet/tsql/sp-reinitmergesubscripti_1.sql)]  
  
### B. Reinitialize the push subscription and upload pending changes
 [!code-sql[HowTo#sp_reinitmergepushsubwithupload](../../relational-databases/replication/codesnippet/tsql/sp-reinitmergesubscripti_2.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute **sp_reinitmergesubscription**.  
  
## See Also  
 [Reinitialize Subscriptions](../../relational-databases/replication/reinitialize-subscriptions.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
