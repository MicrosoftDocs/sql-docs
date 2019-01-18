---
title: "sys.dm_tran_version_store_space_usage (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/24/2018"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_tran_version_store_space_usage_TSQL"
  - "sys.dm_tran_version_store_space_usage"
  - "dm_tran_version_store_space_usage"
  - "dm_tran_version_store_space_usage_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_tran_version_store_space_usage dynamic management view"
ms.assetid: 7ab44517-0351-4f91-bdd9-7cf940f03c51
author: "savjani"
ms.author: "pariks"
manager: "ajayj"
monikerRange: ">=sql-server-2017||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_tran_version_store_space_usage (Transact-SQL)
[!INCLUDE[tsql-appliesto-2016sp2-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-2016sp2-asdb-xxxx-xxx-md.md)]

Returns a table that displays total space in tempdb used by version store records for each database. **sys.dm_tran_version_store_space_usage** is efficient and not expensive to run, as it does not navigate through individual version store records, and returns aggregated version store space consumed in tempdb per database.
  
Each versioned record is stored as binary data, together with some tracking or status information. Similar to records in database tables, version-store records are stored in 8192-byte pages. If a record exceeds 8192 bytes, the record will be split across two different records.  
  
Because the versioned record is stored as binary, there are no problems with different collations from different databases. Use **sys.dm_tran_version_store_space_usage** to monitor and plan tempdb size based on the version store space usage of databases in a SQL Server instance.
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**database_id**|**int**|Database ID of the database.|  
|**reserved_page_count**|**bigint**|Total count of the pages reserved in tempdb for version store records of the database.|  
|**reserved_space_kb**|**bigint**|Total space used in kilobytes in tempdb for version store records of the database.|  
  
## Permissions  
On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   

## Examples  
The following query can be used to determine space consumed in tempdb, by version store of each database in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. 
  
```sql  
SELECT 
  DB_NAME(database_id) as 'Database Name',
  reserved_page_count,
  reserved_space_kb 
FROM sys.dm_tran_version_store_space_usage;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Database Name            reserved_page_count reserved_space_kb  
------------------------ -------------------- -----------  
msdb                      0                    0             
AdventureWorks2016        10                   80             
AdventureWorks2016DW      0                    0             
WideWorldImporters        20                   160             
```
 
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Transaction Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/transaction-related-dynamic-management-views-and-functions-transact-sql.md)  
  
