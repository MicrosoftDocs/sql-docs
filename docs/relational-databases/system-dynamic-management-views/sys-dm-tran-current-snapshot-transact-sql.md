---
title: "sys.dm_tran_current_snapshot (Transact-SQL)"
description: sys.dm_tran_current_snapshot (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_tran_current_snapshot_TSQL"
  - "dm_tran_current_snapshot"
  - "dm_tran_current_snapshot_TSQL"
  - "sys.dm_tran_current_snapshot"
helpviewer_keywords:
  - "sys.dm_tran_current_snapshot dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: 7509d595-c0e1-4237-a5ac-b41ad934544c
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_tran_current_snapshot (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns a virtual table that displays all active transactions at the time when the current snapshot transaction starts. If the current transaction is not a snapshot transaction, this function returns no rows. **sys.dm_tran_current_snapshot** is similar to **sys.dm_tran_transactions_snapshot**, except that **sys.dm_tran_current_snapshot** returns only the active transactions for the current snapshot transaction.  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_tran_current_snapshot**. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)] 
  
## Syntax  
  
```  
  
sys.dm_tran_current_snapshot  
```  
  
## Table Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**transaction_sequence_num**|**bigint**|Transaction sequence number of the active transaction.|  
|pdw_node_id|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   

## Examples  
 The following example uses a test scenario in which four concurrent transactions, each identified by a transaction sequence number (XSN), are running in a database that has the ALLOW_SNAPSHOT_ISOLATION and READ_COMMITTED_SNAPSHOT options set to ON. The following transactions are running:  
  
-   XSN-57 is an update operation under serializable isolation.  
  
-   XSN-58 is the same as XSN-57.  
  
-   XSN-59 is a select operation under snapshot isolation.  
  
-   XSN-60 is the same as XSN-59.  
  
 The following query is executed within the scope of XSN-59.  
  
```  
SELECT   
    transaction_sequence_num  
  FROM sys.dm_tran_current_snapshot;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
transaction_sequence_num  
------------------------  
57  
58  
```  
  
 The results show that XSN-57 and XSN-58 were active at the time that the snapshot transaction XSN-59 started. This same result persists, even after XSN-57 and XSN-58 commit or roll back, until the snapshot transaction finishes.  
  
 The same query is executed within the scope of XSN-60.  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
transaction_sequence_num  
------------------------  
57  
58  
59  
```  
  
 The output for XSN-60 includes the same transactions that appear for XSN-59, but also includes XSN-59, which was active when XSN-60 started.  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Transaction Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/transaction-related-dynamic-management-views-and-functions-transact-sql.md)  
  
