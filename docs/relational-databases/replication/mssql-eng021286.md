---
description: "MSSQL_ENG021286"
title: "MSSQL_ENG021286 | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: reference
helpviewer_keywords: 
  - "MSSQL_ENG021286 error"
ms.assetid: b63620b7-1c6d-46f7-90ea-3a8e99af8de4
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# MSSQL_ENG021286
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
    
## Message Details  
  
|Attribute|Value|  
|-|-|  
|Product Name|SQL Server|  
|Event ID|21286|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|Conflict table '%s' does not exist.|  
  
## Explanation  
 This error is raised if the conflict table for an article listed in [sysmergearticles &#40;Transact-SQL&#41;](../../relational-databases/system-tables/sysmergearticles-transact-sql.md) does not actually exist. The error can occur when you attempt to add a column to or drop a column from a table published for merge replication.  
  
## User Action  
 Execute [DBCC CHECKDB &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) on the database with the missing conflict table to verify there are no data consistency issues.  
  
 If the conflict table is missing on a Subscriber, drop the subscription and recreate it. If the conflict table is missing on a Publisher, drop all subscriptions, drop the publication, and then recreate the publication and all subscriptions. For more information, see [Publish Data and Database Objects](../../relational-databases/replication/publish/publish-data-and-database-objects.md) and [Subscribe to Publications](../../relational-databases/replication/subscribe-to-publications.md).  
  
## See Also  
 [Errors and Events Reference &#40;Replication&#41;](../../relational-databases/replication/errors-and-events-reference-replication.md)  
  
  
