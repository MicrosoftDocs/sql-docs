---
title: "sys.dm_db_xtp_transactions (Transact-SQL)"
description: sys.dm_db_xtp_transactions reports the active transactions in the In-Memory OLTP database engine.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/02/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_db_xtp_transactions"
  - "sys.dm_db_xtp_transactions_TSQL"
  - "dm_db_xtp_transactions"
  - "dm_db_xtp_transactions_TSQL"
helpviewer_keywords:
  - "sys.dm_db_xtp_transactions dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_db_xtp_transactions (Transact-SQL)
[!INCLUDE[sql-asdb-asdbmi](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Reports the active transactions in the [!INCLUDE[inmemory](../../includes/inmemory-md.md)] database engine.  
  
 For more information, see [[!INCLUDE[inmemory](../../includes/inmemory-md.md)] &#40;In-Memory Optimization&#41;](../in-memory-oltp/overview-and-usage-scenarios.md).  
    
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|xtp_transaction_id|**bigint**|Internal ID for this transaction in the XTP transaction manager.|  
|transaction_id|**bigint**|The transaction ID. Joins with the `transaction_id` in other transaction-related DMVs, such as `sys.dm_tran_active_transactions`.<br /><br /> 0 for XTP-only transactions, such as transactions started by natively compiled stored procedures.|  
|session_id|**smallint**|The `session_id` of the session that is executing this transaction. Joins with `sys.dm_exec_sessions`.|  
|begin_tsn|**bigint**|Begin transaction serial number of the transaction.|  
|end_tsn|**bigint**|End transaction serial number of the transaction.|  
|state|**int**|The state of the transaction:<br /><br /> 0=ACTIVE<br /><br /> 1=COMMITTED<br /><br /> 2=ABORTED<br /><br /> 3=VALIDATING|  
|state_desc|**nvarchar**|The description of the transaction state.|  
|result|**int**|The result of this transaction. The following are the possible values.<br /><br /> 0 - IN PROGRESS<br /><br /> 1 - SUCCESS<br /><br /> 2 - ERROR<br /><br /> 3 - COMMIT DEPENDENCY<br /><br /> 4 - VALIDATION FAILED (RR)<br /><br /> 5 - VALIDATION FAILED (SR)<br /><br /> 6 - ROLLBACK|  
|result_desc|**nvarchar**|The result of this transaction. The following are the possible values.<br /><br /> IN PROGRESS<br /><br /> SUCCESS<br /><br /> ERROR<br /><br /> COMMIT DEPENDENCY<br /><br /> VALIDATION FAILED (RR)<br /><br /> VALIDATION FAILED (SR)<br /><br /> ROLLBACK|  
|last_error|**int**|Internal use only|  
|is_speculative|**bit**|Internal use only|  
|is_prepared|**bit**|Internal use only|  
|is_delayed_durability|**bit**|Internal use only|  
|memory_address|**varbinary**|Internal use only|  
|database_address|**varbinary**|Internal use only|  
|thread_id|**int**|Internal use only|  
|read_set_row_count|**int**|Internal use only|  
|write_set_row_count|**int**|Internal use only|  
|scan_set_count|**int**|Internal use only|  
|savepoint_garbage_count|**int**|Internal use only|  
|log_bytes_required|**bigint**|Internal use only|  
|count_of_allocations|**int**|Internal use only|  
|allocated_bytes|**int**|Internal use only|  
|reserved_bytes|**int**|Internal use only|  
|commit_dependency_count|**int**|Internal use only|  
|commit_dependency_total_attempt_count|**int**|Internal use only|  
|scan_area|**int**|Internal use only|  
|scan_area_desc|**nvarchar**|Internal use only|  
|scan_location|**int**|Internal use only.|  
|dependent_1_address|**varbinary(8)**|Internal use only|  
|dependent_2_address|**varbinary(8)**|Internal use only|  
|dependent_3_address|**varbinary(8)**|Internal use only|  
|dependent_4_address|**varbinary(8)**|Internal use only|  
|dependent_5_address|**varbinary(8)**|Internal use only|  
|dependent_6_address|**varbinary(8)**|Internal use only|  
|dependent_7_address|**varbinary(8)**|Internal use only|  
|dependent_8_address|**varbinary(8)**|Internal use only|  
  
## Permissions  
 Requires VIEW DATABASE STATE permission on the server.  
  
## See also

- [Introduction to Memory-Optimized Tables](../in-memory-oltp/introduction-to-memory-optimized-tables.md)
- [Memory-Optimized Table Dynamic Management Views](../../relational-databases/system-dynamic-management-views/memory-optimized-table-dynamic-management-views-transact-sql.md)

## Next steps 

- [[!INCLUDE[inmemory](../../includes/inmemory-md.md)] Overview and Usage Scenarios](../in-memory-oltp/overview-and-usage-scenarios.md)
- [Optimize performance by using in-memory technologies in Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/in-memory-oltp-overview)
