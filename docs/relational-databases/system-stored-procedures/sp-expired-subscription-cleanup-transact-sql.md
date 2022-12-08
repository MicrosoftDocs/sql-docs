---
description: "sp_expired_subscription_cleanup (Transact-SQL)"
title: "sp_expired_subscription_cleanup (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_expired_subscription_cleanup"
  - "SP_EXPIRED_SUBSCRIPTION_CLEANUP_TSQL"
helpviewer_keywords: 
  - "sp_expired_subscription_cleanup"
ms.assetid: 6abc29fe-d77a-4673-9d99-ae31c688012c
author: markingmyname
ms.author: maghan
---
# sp_expired_subscription_cleanup (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Checks the status of all the subscriptions of every publication and drops those that have expired. This stored procedure is executed at the Publisher on any database or at the Distributor on the distribution database for a non- [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_expired_subscription_cleanup [ [ @publisher = ] 'publisher' ]   
```  
  
## Arguments  
`[ @publisher = ] 'publisher'`
 Is the name of a non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publisher. *publication* is **sysname**, with a default value of NULL. You should not specify this parameter for a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_expired_subscription_cleanup** is used in all types of replication.  
  
 **sp_expired_subscription_cleanup** is executed by the Expired Subscription Clean Up job to detect and remove expired subscriptions from publication databases every 24 hours. If any of the subscriptions are out-of-date, that is, have not synchronized with the Publisher within the retention period, the publication is declared expired and the traces of the subscription are cleaned up at the Publisher. For more information, see [Subscription Expiration and Deactivation](../../relational-databases/replication/subscription-expiration-and-deactivation.md).  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_expired_subscription_cleanup**.  
  
## See Also  
 [sp_mergesubscription_cleanup &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-mergesubscription-cleanup-transact-sql.md)   
 [sp_subscription_cleanup &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-subscription-cleanup-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
