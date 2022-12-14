---
title: "sys.dm_pdw_nodes_database_encryption_keys (Transact-SQL)"
description: sys.dm_pdw_nodes_database_encryption_keys (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/07/2017"
ms.service: sql
ms.subservice: data-warehouse
ms.topic: "reference"
ms.custom: seo-dt-2019
dev_langs:
  - "TSQL"
ms.assetid: e7fd02b2-5d7e-4816-a0af-b58ae2ac3f7a
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest"
---
# sys.dm_pdw_nodes_database_encryption_keys (Transact-SQL)
[!INCLUDE[applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

  Returns information about the encryption state of a database and its associated database encryption keys. **sys.dm_pdw_nodes_database_encryption_keys** provides this information for each node. For more information about database encryption, see [Transparent Data Encryption (SQL Server PDW)](../../analytics-platform-system/transparent-data-encryption.md).

> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]  
  
|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------|  
|database_id|**int**|ID of the physical database on each node.|  
|encryption_state|**int**|Indicates whether the database on this node is encrypted or not encrypted.<br /><br /> 0 = No database encryption key present, no encryption<br /><br /> 1 = Unencrypted<br /><br /> 2 = Encryption in progress<br /><br /> 3 = Encrypted<br /><br /> 4 = Key change in progress<br /><br /> 5 = Decryption in progress<br /><br /> 6 = Protection change in progress (The certificate that is encrypting the database encryption key is being changed.)|  
|create_date|**datetime**|Displays the date the encryption key was created.|  
|regenerate_date|**datetime**|Displays the date the encryption key was regenerated.|  
|modify_date|**datetime**|Displays the date the encryption key was modified.|  
|set_date|**datetime**|Displays the date the encryption key was applied to the database.|  
|opened_date|**datetime**|Shows when the database key was last opened.|  
|key_algorithm|**varchar(?)**|Displays the algorithm that is used for the key.|  
|key_length|**int**|Displays the length of the key.|  
|encryptor_thumbprint|**varbin**|Shows the thumbprint of the encryptor.|  
|percent_complete|**real**|Percent complete of the database encryption state change. This will be 0 if there is no state change.|  
|node_id|**int**|Unique numeric ID associated with the node.|  
  
## Permissions  
 Requires the VIEW SERVER STATE permission on the server.  

## Examples 

The following example joins `sys.dm_pdw_nodes_database_encryption_keys` to other system tables to indicate the encryption state for each node of the TDE protected databases.  

[!INCLUDE[ssPDW](../../includes/sspdw-md.md)]

```sql  
SELECT D.database_id AS DBIDinMaster, D.name AS UserDatabaseName,   
PD.pdw_node_id AS NodeID, DM.physical_name AS PhysDBName,   
keys.encryption_state  
FROM sys.dm_pdw_nodes_database_encryption_keys AS keys  
JOIN sys.pdw_nodes_pdw_physical_databases AS PD  
    ON keys.database_id = PD.database_id AND keys.pdw_node_id = PD.pdw_node_id  
JOIN sys.pdw_database_mappings AS DM  
    ON DM.physical_name = PD.physical_name  
JOIN sys.databases AS D  
    ON D.database_id = DM.database_id  
ORDER BY D.database_id, PD.pdw_node_ID;  
```  

[!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] 

```sql
--Query provides underlying distribution encryption status
SELECT keys.database_id AS DBIDinPhysicalDatabases,   
PD.pdw_node_id AS NodeID, PD.physical_name AS PhysDBName,   
keys.encryption_state  
FROM sys.dm_pdw_nodes_database_encryption_keys AS keys  
JOIN sys.pdw_nodes_pdw_physical_databases AS PD  
    ON keys.database_id = PD.database_id AND keys.pdw_node_id = PD.pdw_node_id  
ORDER BY keys.database_id, PD.pdw_node_ID;
```

```sql
--Query provides the DW encryption status
SELECT D.database_id AS DBIDinMaster, D.name AS UserDatabaseName,   
PD.pdw_node_id AS NodeID, PD.physical_name AS PhysDBName,   
keys.encryption_state  
FROM sys.dm_pdw_nodes_database_encryption_keys AS keys  
JOIN sys.pdw_nodes_pdw_physical_databases AS PD  
    ON keys.database_id = PD.database_id AND keys.pdw_node_id = PD.pdw_node_id  
JOIN sys.databases AS D  
    ON D.database_id = PD.database_id  
ORDER BY D.database_id, PD.pdw_node_ID;
```
  
## See Also  
 [Azure Synapse Analytics and Parallel Data Warehouse Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-and-parallel-data-warehouse-dynamic-management-views.md)   
 [CREATE DATABASE ENCRYPTION KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-database-encryption-key-transact-sql.md)   
 [ALTER DATABASE ENCRYPTION KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-encryption-key-transact-sql.md)   
 [DROP DATABASE ENCRYPTION KEY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-database-encryption-key-transact-sql.md)  
  
  

