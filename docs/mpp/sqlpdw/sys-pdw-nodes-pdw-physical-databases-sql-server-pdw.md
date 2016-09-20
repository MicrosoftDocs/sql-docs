---
title: "sys.pdw_nodes_pdw_physical_databases (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: ac8f49e9-73d4-42c1-a19c-d7386a892fe5
caps.latest.revision: 5
author: BarbKess
---
# sys.pdw_nodes_pdw_physical_databases (SQL Server PDW)
Contains a row for each physical database on a compute node. Aggregate physical database information to get detailed information about databases. To combine information, join the `sys.pdw_nodes_pdw_physical_databases` to the `sys.pdw_database_mappings` and `sys.databases` tables.  
  
|Column Name|Data Type|Description|  
|---------------|-------------|---------------|  
|database_id|**int**|The object ID for the database. Note that this value is not same as a database_id in the sys.databases (SQL Server PDW) view.|  
|physical_name|**sysname**|The physical name for the database on the Shell/Compute nodes. This value is same as a value in the physical_name column in the sys.pdw_database_mappings (SQL Server PDW) view.|  
|pdw_node_id|**int**|Unique numeric id associated with the node.|  
  
## Examples  
  
### A. Returning  
The following query returns the name and ID of each database in master, and the corresponding database name on each compute node.  
  
```  
SELECT D.database_id AS DBID_in_master, D.name AS UserDatabaseName,   
PD.pdw_node_id AS NodeID, DM.physical_name AS PhysDBName   
FROM sys.databases AS D  
JOIN sys.pdw_database_mappings AS DM  
    ON D.database_id = DM.database_id  
JOIN sys.pdw_nodes_pdw_physical_databases AS PD  
    ON DM.physical_name = PD.physical_name  
ORDER BY D.database_id, PD.pdw_node_ID;  
```  
  
### B. Using sys.pdw_nodes_pdw_physical_databases to gather detailed object information  
The following query shows information about indexes and includes useful information about the database the objects belong to objects in the database.  
  
```  
SELECT D.name AS UserDatabaseName, D.database_id AS DBIDinMaster,  
DM.physical_name AS PhysDBName, PD.pdw_node_id AS NodeID,   
IU.object_id, IU.index_id, IU.user_seeks, IU.user_scans, IU.user_lookups, IU.user_updates  
FROM sys.databases AS D  
JOIN sys.pdw_database_mappings AS DM  
    ON D.database_id = DM.database_id  
JOIN sys.pdw_nodes_pdw_physical_databases AS PD  
    ON DM.physical_name = PD.physical_name  
JOIN sys.dm_pdw_nodes_db_index_usage_stats AS IU  
    ON PD.database_id = IU.database_id  
ORDER BY D.database_id, IU.object_id, IU.index_id, PD.pdw_node_ID;  
```  
  
### C. Using sys.pdw_nodes_pdw_physical_databases to determine the encryption state  
The following query provides encryption state of the AdventureWorksPDW2012 database.  
  
```  
WITH dek_encryption_state AS   
(  
    SELECT ISNULL(db_map.database_id, dek.database_id) AS database_id, encryption_state  
    FROM sys.dm_pdw_nodes_database_encryption_keys AS dek  
        INNER JOIN sys.pdw_nodes_pdw_physical_databases AS node_db_map  
            ON dek.database_id = node_db_map.database_id AND dek.pdw_node_id = node_db_map.pdw_node_id  
        LEFT JOIN sys.pdw_database_mappings AS db_map  
            ON node_db_map .physical_name = db_map.physical_name  
        INNER JOIN sys.dm_pdw_nodes AS nodes  
            ON nodes.pdw_node_id = dek.pdw_node_id  
    WHERE dek.encryptor_thumbprint <> 0x  
)  
SELECT TOP 1 encryption_state  
       FROM  dek_encryption_state  
       WHERE dek_encryption_state.database_id = DB_ID('AdventureWorksPDW2012 ')  
       ORDER BY (CASE encryption_state WHEN 3 THEN -1 ELSE encryption_state END) DESC;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/system-views-sql-server-pdw.md)  
[sys.databases &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-databases-sql-server-pdw.md)  
[sys.pdw_database_mappings &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-pdw-database-mappings-sql-server-pdw.md)  
  
