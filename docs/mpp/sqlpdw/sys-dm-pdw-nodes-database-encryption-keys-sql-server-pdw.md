---
title: "sys.dm_pdw_nodes_database_encryption_keys (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 10e42af5-504b-4e04-9580-7855ec43288a
caps.latest.revision: 8
author: BarbKess
---
# sys.dm_pdw_nodes_database_encryption_keys (SQL Server PDW)
Returns information about the encryption state of a database and its associated database encryption keys. **sys.dm_pdw_nodes_database_encryption_keys** provides this information for each node. For more information about database encryption, see [Transparent Data Encryption &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/transparent-data-encryption-sql-server-pdw.md).  
  
|Column Name|Data Type|Description|  
|---------------|-------------|---------------|  
|database_id|**int**|ID of the physical database on each node.|  
|encryption_state|**int**|Indicates whether the database on this node is encrypted or not encrypted.<br /><br />0 = No database encryption key present, no encryption<br /><br />1 = Unencrypted<br /><br />2 = Encryption in progress<br /><br />3 = Encrypted<br /><br />4 = Key change in progress<br /><br />5 = Decryption in progress<br /><br />6 = Protection change in progress (The certificate that is encrypting the database encryption key is being changed.)|  
|create_date|**datetime**|Displays the date the encryption key was created.|  
|regenerate_date|**datetime**|Displays the date the encryption key was regenerated.|  
|modify_date|**datetime**|Displays the date the encryption key was modified.|  
|set_date|**datetime**|Displays the date the encryption key was applied to the database.|  
|opened_date|**datetime**|Shows when the database key was last opened.|  
|key_algorithm|**varchar(?)**|Displays the algorithm that is used for the key.|  
|key_length|**int**|Displays the length of the key.|  
|encryptor_thumbprint|**varbin**|Shows the thumbprint of the encryptor.|  
|percent_complete|**real**|Percent complete of the database encryption state change. This will be 0 if there is no state change.|  
|node_id|**int**|Unique numeric id associated with the node.|  
  
## Permissions  
Requires the VIEW SERVER STATE permission on the server.  
  
The following example joins `sys.dm_pdw_nodes_database_encryption_keys` to other system tables to indicate the encryption state for each node of the TDE protected databases.  
  
```  
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
  
## See Also  
[Transparent Data Encryption &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/transparent-data-encryption-sql-server-pdw.md)  
[CREATE DATABASE ENCRYPTION KEY &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-database-encryption-key-sql-server-pdw.md)  
[ALTER DATABASE ENCRYPTION KEY &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/alter-database-encryption-key-sql-server-pdw.md)  
[DROP DATABASE ENCRYPTION KEY &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/drop-database-encryption-key-sql-server-pdw.md)  
[sp_pdw_database_encryption_regenerate_system_keys &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sp-pdw-database-encryption-regenerate-system-keys-sql-server-pdw.md)  
  
