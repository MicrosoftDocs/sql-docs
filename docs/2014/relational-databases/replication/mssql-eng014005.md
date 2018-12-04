---
title: "MSSQL_ENG014005 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "MSSQL_ENG014005 error"
ms.assetid: f168f0d6-cb11-45d4-9781-c374d7f388ee
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQL_ENG014005
    
## Message Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|14005|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|Could not drop publication. A subscription exists to it.|  
  
## Explanation  
 You have tried to drop a publication which has one or more associated subscriptions. A publication can only be dropped if there are no associated subscriptions.  
  
## User Action  
 Drop the subscriptions before dropping the publication. If you use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to drop the publication, it will give you the option to automatically drop all associated subscriptions before dropping the publication. If you use stored procedures, you must explicitly drop the subscriptions first. For more information, see [Delete a Push Subscription](delete-a-push-subscription.md) and [Delete a Pull Subscription](delete-a-pull-subscription.md).  
  
 If no subscriptions appear to exist for the publication or if you see this error when you create a publication, you might have a previous subscription that was not completely cleaned up when it was removed. Execute [sp_removedbreplication &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-removedbreplication-transact-sql) on the database to remove all objects and settings related to replication.  
  
## See Also  
 [Errors and Events Reference &#40;Replication&#41;](errors-and-events-reference-replication.md)  
  
  
