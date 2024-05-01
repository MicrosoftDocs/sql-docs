---
title: "Enable coordinated backups (Transactional)"
description: Learn to enable coordinated backups on the distribution database so that the transaction log for the Transactional Replication publication database is not truncated until transactions that have been propagated to the Distributor have been backed up.
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: "03/07/2017"
ms.service: sql
ms.subservice: replication
ms.topic: how-to
ms.custom: updatefrequency5
helpviewer_keywords:
  - "transactional replication, backup and restore"
  - "sp_replicationdboption"
  - "sync with backup [SQL Server replication]"
  - "coordinated backups [SQL Server replication]"
  - "backups [SQL Server replication], transactional replication"
dev_langs:
  - "TSQL"
---
# Enable Coordinated Backups for Transactional Replication
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  When enabling a database for transactional replication, you can specify that all transactions must be backed up before being delivered to the distribution database. You can also enable coordinated backup on the distribution database so that the transaction log for the publication database is not truncated until transactions that have been propagated to the Distributor have been backed up. For more information, see [Strategies for Backing Up and Restoring Snapshot and Transactional Replication](../../../relational-databases/replication/administration/strategies-for-backing-up-and-restoring-snapshot-and-transactional-replication.md).  
  
 > [!NOTE] 
 > Using the **sync with backup option** on the distribution database is not compatible when the publisher database is part of an availability group and could lead to the following error: `The process could not execute 'sp_repldone/sp_replcounters' on 'machinename\instance',  Possible inconsistent state in the distribution database, Get help: http://help/MSSQL_REPL20011 (Source: MSSQLServer, Error number: 18846)`

  
### To enable coordinated backups for a database published with transactional replication  
  
1.  At the Publisher, use the `SELECT DATABASEPROPERTYEX(DB_NAME(),'IsSyncWithBackup')` [DATABASEPROPERTYEX &#40;Transact-SQL&#41;](../../../t-sql/functions/databasepropertyex-transact-sql.md) function to return the **IsSyncWithBackup** property of the publication database. If the function returns **1**, coordinated backups are already enabled for the published database.  
  
2.  If the function in step 1 returns **0**, execute [sp_replicationdboption &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-replicationdboption-transact-sql.md) at the Publisher on the publication database. Specify a value of **sync with backup** for **\@optname**, and **true** for **\@value**.  
  
    > [!NOTE]  
    >  If you change the **sync with backup** option to **false**, the truncation point of the publication database will be updated after the Log Reader Agent runs, or after an interval if the Log Reader Agent is running continuously. The maximum interval is controlled by the **–MessageInterval** agent parameter (which has a default of 30 seconds).  
  
### To enable coordinated backups for a distribution database  
  
1.  At the Distributor, use the [DATABASEPROPERTYEX &#40;Transact-SQL&#41;](../../../t-sql/functions/databasepropertyex-transact-sql.md) function to return the **IsSyncWithBackup** property of the distribution database. If the function returns **1**, coordinated backups are already enabled for the distribution database.  
  
2.  If the function in step 1 returns **0**, execute [sp_replicationdboption &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-replicationdboption-transact-sql.md) at the Distributor on the distribution database. Specify a value of **sync with backup** for **\@optname** and **true** for **\@value**.  
  
### To disable coordinated backups  
  
1.  At either the Publisher on the publication database or at the Distributor on the distribution database, execute [sp_replicationdboption &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-replicationdboption-transact-sql.md). Specify a value of **sync with backup** for **\@optname** and **false** for **\@value**.  
  
## Examples  
  
### A. Retrieve the `IsSyncWithBackup` property for the current database

This example returns the `IsSyncWithBackup` property for the current database:
  
```sql
SELECT DATABASEPROPERTYEX(DB_NAME(),'IsSyncWithBackup')`
```

### B. Retrieve the `IsSyncWithBackup` property for a specific database

This example returns the `IsSyncWithBackup` property for the database `NameOfDatabaseToCheck`:
  
```sql
SELECT DATABASEPROPERTYEX('NameOfDatabaseToCheck','IsSyncWithBackup')`
```
