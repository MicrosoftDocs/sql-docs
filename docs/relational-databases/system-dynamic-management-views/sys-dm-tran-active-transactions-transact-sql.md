---
title: "sys.dm_tran_active_transactions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/30/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_tran_active_transactions"
  - "sys.dm_tran_active_transactions_TSQL"
  - "dm_tran_active_transactions_TSQL"
  - "dm_tran_active_transactions"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_tran_active_transactions dynamic management view"
ms.assetid: 154ad6ae-5455-4ed2-b014-e443abe2c6ee
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_tran_active_transactions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns information about transactions for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_tran_active_transactions**.  
  
|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------|  
|transaction_id|**bigint**|ID of the transaction at the instance level, not the database level. It is only unique across all databases within an instance but not unique across all server instances.|  
|name|**nvarchar(32)**|Transaction name. This is overwritten if the transaction is marked and the marked name replaces the transaction name.|  
|transaction_begin_time|**datetime**|Time that the transaction started.|  
|transaction_type|**int**|Type of transaction.<br /><br /> 1 = Read/write transaction<br /><br /> 2 = Read-only transaction<br /><br /> 3 = System transaction<br /><br /> 4 = Distributed transaction|  
|transaction_uow|**uniqueidentifier**|Transaction unit of work (UOW) identifier for distributed transactions. MS DTC uses the UOW identifier to work with the distributed transaction.|  
|transaction_state|**int**|0 = The transaction has not been completely initialized yet.<br /><br /> 1 = The transaction has been initialized but has not started.<br /><br /> 2 = The transaction is active.<br /><br /> 3 = The transaction has ended. This is used for read-only transactions.<br /><br /> 4 = The commit process has been initiated on the distributed transaction. This is for distributed transactions only. The distributed transaction is still active but further processing cannot take place.<br /><br /> 5 = The transaction is in a prepared state and waiting resolution.<br /><br /> 6 = The transaction has been committed.<br /><br /> 7 = The transaction is being rolled back.<br /><br /> 8 = The transaction has been rolled back.|  
|transaction_status|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|transaction_status2|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|dtc_state|**int**|**Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] (Initial release through [current release](https://go.microsoft.com/fwlink/p/?LinkId=299659)).<br /><br /> 1 = ACTIVE<br /><br /> 2 = PREPARED<br /><br /> 3 = COMMITTED<br /><br /> 4 = ABORTED<br /><br /> 5 = RECOVERED|  
|dtc_status|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|dtc_isolation_level|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|filestream_transaction_id|**varbinary(128)**|**Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] (Initial release through [current release](https://go.microsoft.com/fwlink/p/?LinkId=299659)).<br /><br /> [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|pdw_node_id|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)], requires the `VIEW DATABASE STATE` permission in the database.   
  
## See Also  
 [sys.dm_tran_session_transactions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-session-transactions-transact-sql.md)   
 [sys.dm_tran_database_transactions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-database-transactions-transact-sql.md)   
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Transaction Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/transaction-related-dynamic-management-views-and-functions-transact-sql.md)  
  
  


