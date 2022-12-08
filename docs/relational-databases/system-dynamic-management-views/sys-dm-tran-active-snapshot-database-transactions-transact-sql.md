---
title: "sys.dm_tran_active_snapshot_database_transactions (Transact-SQL)"
description: sys.dm_tran_active_snapshot_database_transactions (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_tran_active_snapshot_database_transactions_TSQL"
  - "dm_tran_active_snapshot_database_transactions"
  - "sys.dm_tran_active_snapshot_database_transactions"
  - "dm_tran_active_snapshot_database_transactions_TSQL"
helpviewer_keywords:
  - "sys.dm_tran_active_snapshot_database_transactions dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: 55b83f9c-da10-4e65-9846-f4ef3c0c0f36
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_tran_active_snapshot_database_transactions (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  In a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance, this dynamic management view returns a virtual table for all active transactions that generate or potentially access row versions. Transactions are included for one or more of the following conditions:  
  
-   When either or both ALLOW_SNAPSHOT_ISOLATION and READ_COMMITTED_SNAPSHOT database options are set to ON:  
  
    -   There is one row for each transaction that is running under snapshot isolation level, or read-committed isolation level that is using row versioning.  
  
    -   There is one row for each transaction that causes a row version to be created in the current database. For example, the transaction generates a row version by updating or deleting a row in the current database.  
  
-   When a trigger is fired, there is one row for the transaction under which the trigger is executing.  
  
-   When an online indexing procedure is running, there is one row for the transaction that is creating the index.  
  
-   When Multiple Active Results Sets (MARS) session is enabled, there is one row for each transaction that is accessing row versions.  
  
 This dynamic management view does not include system transactions.  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_tran_active_snapshot_database_transactions**. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]  
  
## Syntax  
  
```  
  
sys.dm_tran_active_snapshot_database_transactions  
```  
  
## Table Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**transaction_id**|**bigint**|Unique identification number assigned for the transaction. The transaction ID is primarily used to identify the transaction in locking operations.|  
|**transaction_sequence_num**|**bigint**|Transaction sequence number. This is a unique sequence number that is assigned to a transaction when it starts. Transactions that do not generate version records and do not use snapshot scans will not receive a transaction sequence number.|  
|**commit_sequence_num**|**bigint**|Sequence number that indicates when the transaction finishes (commits or stops). For active transactions, the value is NULL.|  
|**is_snapshot**|**int**|0 = Is not a snapshot isolation transaction.<br /><br /> 1 = Is a snapshot isolation transaction.|  
|**session_id**|**int**|ID of the session that started the transaction.|  
|**first_snapshot_sequence_num**|**bigint**|Lowest transaction sequence number of the transactions that were active when a snapshot was taken. On execution, a snapshot transaction takes a snapshot of all of the active transactions at that time. For nonsnapshot transactions, this column shows 0.|  
|**max_version_chain_traversed**|**int**|Maximum length of the version chain that is traversed to find the transactionally consistent version.|  
|**average_version_chain_traversed**|**real**|Average number of row versions in the version chains that are traversed.|  
|**elapsed_time_seconds**|**bigint**|Elapsed time since the transaction obtained its transaction sequence number.|  
|**pdw_node_id**|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   

## Remarks  
 **sys.dm_tran_active_snapshot_database_transactions** reports transactions that are assigned a transaction sequence number (XSN). The XSN is assigned when the transaction first accesses the version store. In a database that is enabled for snapshot isolation or read committed isolation using row versioning, the examples show when an XSN is assigned to a transaction:  
  
-   If a transaction is running under serializable isolation level, an XSN is assigned when the transaction first executes a statement, such as an UPDATE operation, that causes a row version to be created.  
  
-   If a transaction is running under snapshot isolation, an XSN is assigned when any data manipulation language (DML) statement, including a SELECT operation, is executed.  
  
 Transaction sequence numbers are serially incremented for each transaction that is started in an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
## Examples  
 The following example uses a test scenario in which four concurrent transactions, each identified by a transaction sequence number (XSN), are running in a database that has the ALLOW_SNAPSHOT_ISOLATION and READ_COMMITTED_SNAPSHOT options set to ON. The following transactions are running:  
  
-   XSN-57 is an update operation under serializable isolation.  
  
-   XSN-58 is the same as XSN-57.  
  
-   XSN-59 is a select operation under snapshot isolation  
  
-   XSN-60 is same as XSN-59.  
  
 The following query is executed.  
  
```  
SELECT   
    transaction_id,  
    transaction_sequence_num,  
    commit_sequence_num,  
    is_snapshot session_id,  
    first_snapshot_sequence_num,  
    max_version_chain_traversed,  
    average_version_chain_traversed,  
    elapsed_time_seconds  
  FROM sys.dm_tran_active_snapshot_database_transactions;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
transaction_id  transaction_sequence_num  commit_sequence_num  
--------------  ------------------------  -------------------  
9295            57                        NULL  
9324            58                        NULL  
9387            59                        NULL  
9400            60                        NULL  
  
is_snapshot  session_id   first_snapshot_sequence_num  
-----------  -----------  ---------------------------  
0            54           0  
0            53           0  
1            52           57  
1            51           57  
  
max_version_chain_traversed  average_version_chain_traversed  
---------------------------  -------------------------------  
0                            0  
0                            0  
1                            1  
1                            1  
  
elapsed_time_seconds  
--------------------  
419  
397  
359  
333  
```  
  
 The following information evaluates the results from **sys.dm_tran_active_snapshot_database_transactions**:  
  
-   XSN-57: Because this transaction is not running under snapshot isolation, the `is_snapshot` value and `first_snapshot_sequence_num` are `0`. `transaction_sequence_num` shows that a transaction sequence number has been assigned to this transaction, because one or both ALLOW_SNAPSHOT_ISOLATION or READ_COMMITTED_SNAPSHOT database options are ON.  
  
-   XSN-58: This transaction is not running under snapshot isolation and the same information for XSN-57 applies.  
  
-   XSN-59: This is the first active transaction that is running under snapshot isolation. This transaction reads data that is committed before XSN-57, as indicated by `first_snapshot_sequence_num`. The output for this transaction also shows the maximum version chain that is traversed for a row is `1` and has traversed an average of `1` version for each row that is accessed. This means that transactions XSN-57, XSN-58, and XSN-60 have not modified rows and committed.  
  
-   XSN-60: This is the second transaction running under snapshot isolation. The output shows the same information as XSN-59.  
  
## See Also  
 [SET TRANSACTION ISOLATION LEVEL &#40;Transact-SQL&#41;](../../t-sql/statements/set-transaction-isolation-level-transact-sql.md)   
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Transaction Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/transaction-related-dynamic-management-views-and-functions-transact-sql.md)  
  
