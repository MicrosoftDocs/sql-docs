---
title: "Change Tracking Tables (Transact-SQL)"
description: Change Tracking Tables (Transact-SQL)
author: JetterMcTedder
ms.author: bspendolini
ms.date: "10/20/2022"
ms.prod: sql
ms.prod_service: "database-engine"
ms.technology: system-objects
ms.topic: "reference"
dev_langs:
  - "TSQL"
ms.assetid:
---
# Change Tracking Tables (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Change tracking is a lightweight solution that provides an efficient data change tracking mechanism for applications, ETL loads, event capture, and auditing. This allows for quick and simple detection of changed data without the need for expensive and complex custom solutions traditionally involving a combination of triggers, timestamp columns, new tables to store tracking information, and cleanup processes.
  
## In This Section

 [dbo.MSchange_tracking_history](../../relational-databases/system-tables/dbo-mschange_tracking_history-transact-sql.md)  
 Returns one row for each change made to a captured column in the associated source table.  
  
## See Also

 [About Change Tracking &#40;Transact-SQL&#41;](../../relational-databases/track-changes/about-change-tracking-sql-server.md)  
 [Change Tracking Cleanup &#40;Transact-SQL&#41;](../../relational-databases/track-changes/cleanup-and-troubleshooting-change-tracking-sql-server.md)  
 [Change Tracking Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/change-tracking-functions-transact-sql.md)
 [Change Tracking Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/change-tracking-stored-procedures-transact-sql.md)  
