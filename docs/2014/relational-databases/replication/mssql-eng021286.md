---
title: "MSSQL_ENG021286 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "MSSQL_ENG021286 error"
ms.assetid: b63620b7-1c6d-46f7-90ea-3a8e99af8de4
caps.latest.revision: 11
author: "craigg-msft"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQL_ENG021286
    
## Message Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|21286|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|Conflict table '%s' does not exist.|  
  
## Explanation  
 This error is raised if the conflict table for an article listed in [sysmergearticles &#40;Transact-SQL&#41;](../Topic/sysmergearticles%20\(Transact-SQL\).md) does not actually exist. The error can occur when you attempt to add a column to or drop a column from a table published for merge replication.  
  
## User Action  
 Execute [DBCC CHECKDB &#40;Transact-SQL&#41;](../Topic/DBCC%20CHECKDB%20\(Transact-SQL\).md) on the database with the missing conflict table to verify there are no data consistency issues.  
  
 If the conflict table is missing on a Subscriber, drop the subscription and recreate it. If the conflict table is missing on a Publisher, drop all subscriptions, drop the publication, and then recreate the publication and all subscriptions. For more information, see [Publish Data and Database Objects](../../../2014/relational-databases/replication/publish-data-and-database-objects.md) and [Subscribe to Publications](../../../2014/relational-databases/replication/subscribe-to-publications.md).  
  
## See Also  
 [Errors and Events Reference &#40;Replication&#41;](../../../2014/relational-databases/replication/errors-and-events-reference-replication.md)  
  
  