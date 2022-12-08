---
title: "sys.dm_tran_session_transactions (Transact-SQL)"
description: sys.dm_tran_session_transactions (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/30/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_tran_session_transactions"
  - "sys.dm_tran_session_transactions"
  - "sys.dm_tran_session_transactions_TSQL"
  - "dm_tran_session_transactions_TSQL"
helpviewer_keywords:
  - "sys.dm_tran_session_transactions dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: c7157491-58c2-49fe-87d7-0c9723113adf
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_tran_session_transactions (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns correlation information for associated transactions and sessions.  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_tran_session_transactions**. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)] 
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|session_id|**int**|ID of the session under which the transaction is running.|  
|transaction_id|**bigint**|ID of the transaction.|  
|transaction_descriptor|**binary(8)**|Transaction identifier used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] when communicating with the client driver.|  
|enlist_count|**int**|Number of active requests in the session working on the transaction.|  
|is_user_transaction|**bit**|1 = The transaction was initiated by a user request.<br /><br /> 0 = System transaction.|  
|is_local|**bit**|1 = Local transaction.<br /><br /> 0 = Distributed transaction or an enlisted bound session transaction.|  
|is_enlisted|**bit**|1 = Enlisted distributed transaction.<br /><br /> 0 = Not an enlisted distributed transaction.|  
|is_bound|**bit**|1 = The transaction is active on the session via bound sessions.<br /><br /> 0 = The transaction is not active on the session via bound sessions.|  
|open_transaction_count||The number of open transactions for each session.|  
|pdw_node_id|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   

## Remarks  
 Through bound sessions and distributed transactions, it is possible for a transaction to be running under more than one session. In such cases, sys.dm_tran_session_transactions will show multiple rows for the same transaction_id, one for each session under which the transaction is running.  
  
 By executing multiple requests in autocommit mode using multiple active result sets (MARS), it is possible to have more than one active transaction on a single session. In such cases, sys.dm_tran_session_transactions will show multiple rows for the same session_id, one for each transaction running under that session.  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Transaction Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/transaction-related-dynamic-management-views-and-functions-transact-sql.md)  
  
