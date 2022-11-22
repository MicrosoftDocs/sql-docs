---
title: "sys.dm_tran_top_version_generators (Transact-SQL)"
description: sys.dm_tran_top_version_generators (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_tran_top_version_generators"
  - "sys.dm_tran_top_version_generators"
  - "dm_tran_top_version_generators_TSQL"
  - "sys.dm_tran_top_version_generators_TSQL"
helpviewer_keywords:
  - "sys.dm_tran_top_version_generators dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: cec7809b-ba8a-4df9-b5bb-d4f651ff1a86
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_tran_top_version_generators (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns a virtual table for the objects that are producing the most versions in the version store. **sys.dm_tran_top_version_generators** returns the top 256 aggregated record lengths that are grouped by the **database_id** and **rowset_id**. **sys.dm_tran_top_version_generators** retrieves data by querying the **dm_tran_version_store** virtual table. **sys.dm_tran_top_version_generators** is an inefficient view to run because this view queries the version store, and the version store can be very large. We recommend that you use this function to find the largest consumers of the version store.  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_tran_top_version_generators**. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]
  
## Syntax  
  
```  
  
sys.dm_tran_top_version_generators  
```  
  
## Table Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**database_id**|**int**|Database ID.|  
|**rowset_id**|**bigint**|Rowset ID.|  
|**aggregated_record_length_in_bytes**|**int**|Sum of the record lengths for each **database_id** and **rowset_id pair** in the version store.|  
|**pdw_node_id**|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   

## Remarks  
 Because **sys.dm_tran_top_version_generators** might have to read many pages as it scans the entire version store, running **sys.dm_tran_top_version_generators** can interfere with system performance.  
  
## Examples  
 The following example uses a test scenario in which four concurrent transactions, each identified by a transaction sequence number (XSN), are running in a database that has the ALLOW_SNAPSHOT_ISOLATION and READ_COMMITTED_SNAPSHOT options set to ON. The following transactions are running:  
  
-   XSN-57 is an update operation under serializable isolation.  
  
-   XSN-58 is the same as XSN-57.  
  
-   XSN-59 is a select operation under snapshot isolation.  
  
-   XSN-60 is the same as XSN-59.  
  
 The following query is executed.  
  
```  
SELECT  
    database_id,  
    rowset_id,  
    aggregated_record_length_in_bytes  
  FROM sys.dm_tran_top_version_generators;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
database_id rowset_id            aggregated_record_length_in_bytes  
----------- -------------------- ---------------------------------  
9           72057594038321152    87  
9           72057594038386688    33  
```  
  
 The output shows that all versions are created by `database_id``9` and that the versions generate from two tables.  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Transaction Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/transaction-related-dynamic-management-views-and-functions-transact-sql.md)  
  
