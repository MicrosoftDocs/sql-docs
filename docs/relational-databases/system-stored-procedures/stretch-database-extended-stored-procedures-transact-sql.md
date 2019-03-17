---
title: "Stretch Database Extended Stored Procedures (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: stored-procedures
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Stretch Database, stored procedures"
ms.assetid: bda29952-4b8b-4295-ab78-f24dcb0b03c6
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Stretch Database Extended Stored Procedures (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

 This section describes the extended stored procedures that are related to Stretch Database.  
  
## In This Section  
[sys.sp_rda_deauthorize_db](../../relational-databases/system-stored-procedures/sys-sp-rda-deauthorize-db-transact-sql.md) 
Removes the authenticated connection between a local Stretch-enabled database and the remote Azure database.

[sys.sp_rda_get_rpo_duration](../../relational-databases/system-stored-procedures/sys-sp-rda-get-rpo-duration-transact-sql.md)
  Gets the number of hours of migrated data that SQL Server retains in a staging table to help ensure a full restore of the remote Azure database, if a restore is necessary.
  
 [sys.sp_rda_reauthorize_db](../../relational-databases/system-stored-procedures/sys-sp-rda-reauthorize-db-transact-sql.md) 
 Restores the authenticated connection between a local database enabled for Stretch and the remote database.
  
 [sys.sp_rda_reconcile_batch](../../relational-databases/system-stored-procedures/sys-sp-rda-reconcile-batch-transact-sql.md)  
 Reconciles the batch ID stored in the Stretch-enabled SQL Server table for the most recently migrated data with the batch ID stored in the remote Azure table. 
 
[sys.sp_rda_reconcile_columns](../../relational-databases/system-stored-procedures/sys-sp-rda-reconcile-columns-transact-sql.md) 
 Reconciles the columns in the remote Azure table to the columns in the Stretch-enabled SQL Server table.
 
 [sys.sp_rda_reconcile_indexes](../../relational-databases/system-stored-procedures/sys-sp-rda-reconcile-indexes-transact-sql.md) 
 Queues a schema task to reconcile indexes on the remote table.
 
 [sys.sp_rda_set_query_mode](../../relational-databases/system-stored-procedures/sys-sp-rda-set-query-mode-transact-sql.md) 
 Specifies whether queries against the current Stretch-enabled database and its tables return both local and remote data (the default), or local data only.
 
 [sys.sp_rda_set_rpo_duration](../../relational-databases/system-stored-procedures/sys-sp-rda-set-rpo-duration-transact-sql.md)
 Sets the number of hours of migrated data that SQL Server retains in a staging table to help ensure a full restore of the remote Azure database, if a restore is necessary.
 
 [sys.sp_rda_test_connection](../../relational-databases/system-stored-procedures/sys-sp-rda-test-connection-transact-sql.md) 
 Tests the connection from SQL Server to the remote Azure server and reports problems that may prevent data migration.
 
## See Also  
 [Stretch Database](../../sql-server/stretch-database/stretch-database.md)  
  
  
