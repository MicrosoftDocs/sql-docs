---
title: "MSSQL_ENG021286 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "MSSQL_ENG021286 error"
ms.assetid: b63620b7-1c6d-46f7-90ea-3a8e99af8de4
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQL_ENG021286
    
## Message Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|21286|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|Conflict table '%s' does not exist.|  
  
## Explanation  
 This error is raised if the conflict table for an article listed in [sysmergearticles &#40;Transact-SQL&#41;](/sql/relational-databases/system-tables/sysmergearticles-transact-sql) does not actually exist. The error can occur when you attempt to add a column to or drop a column from a table published for merge replication.  
  
## User Action  
 Execute [DBCC CHECKDB &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-checkdb-transact-sql) on the database with the missing conflict table to verify there are no data consistency issues.  
  
 If the conflict table is missing on a Subscriber, drop the subscription and recreate it. If the conflict table is missing on a Publisher, drop all subscriptions, drop the publication, and then recreate the publication and all subscriptions. For more information, see [Publish Data and Database Objects](publish/publish-data-and-database-objects.md) and [Subscribe to Publications](subscribe-to-publications.md).  
  
## See Also  
 [Errors and Events Reference &#40;Replication&#41;](errors-and-events-reference-replication.md)  
  
  
