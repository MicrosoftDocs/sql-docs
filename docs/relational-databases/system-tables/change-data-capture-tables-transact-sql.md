---
title: "Change Data Capture Tables (Transact-SQL)"
description: Change Data Capture Tables (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
dev_langs:
  - "TSQL"
ms.assetid: a4372d0b-50ca-4e58-80f6-2ed3cb52a84a
---
# Change Data Capture Tables (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Change data capture enables change tracking on tables so that data manipulation language (DML) and data definition language (DDL) changes made to the tables can be incrementally loaded into a data warehouse. The topics in this section describe the system tables that store information used by change data capture operations.  
  
## In This Section  
 [cdc.<capture_instance>_CT](../../relational-databases/system-tables/cdc-capture-instance-ct-transact-sql.md)  
 Returns one row for each change made to a captured column in the associated source table.  
  
 [cdc.captured_columns](../../relational-databases/system-tables/cdc-captured-columns-transact-sql.md)  
 Returns one row for each column tracked in a capture instance.  
  
 [cdc.change_tables](../../relational-databases/system-tables/cdc-change-tables-transact-sql.md)  
 Returns one row for each change table in the database.  
  
 [cdc.ddl_history](../../relational-databases/system-tables/cdc-ddl-history-transact-sql.md)  
 Returns one row for each data definition language (DDL) change made to tables that are enabled for change data capture.  
  
 [cdc.lsn_time_mapping](../../relational-databases/system-tables/cdc-lsn-time-mapping-transact-sql.md)  
 Returns one row for each transaction having rows in a change table. This table is used to map between log sequence number (LSN) commit values and the time the transaction committed.  
  
 [cdc.index_columns](../../relational-databases/system-tables/cdc-index-columns-transact-sql.md)  
 Returns one row for each index column associated with a change table.  
  
 [dbo.cdc_jobs &#40;Transact-SQL&#41;](../../relational-databases/system-tables/dbo-cdc-jobs-transact-sql.md)  
 Returns the configuration parameters for change data capture agent jobs.  
  
## See Also  
 [Change Data Capture Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/change-data-capture-stored-procedures-transact-sql.md)   
 [Change Data Capture Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/change-data-capture-functions-transact-sql.md)  
  
  
