---
title: "ALTER DATABASE (Parallel Data Warehouse) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 5751656b-7aae-4152-a314-4c631bea4fc4
caps.latest.revision: 10
author: "barbkess"
ms.author: "barbkess"
manager: "jhubbard"
---
# ALTER DATABASE (Parallel Data Warehouse)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-xxxx-pdw_md](../../includes/tsql-appliesto-xxxxxx-xxxx-xxxx-pdw-md.md)]

  Modifies the maximum database size options for replicated tables, distributed tables, and the transaction log in [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]. Use this statement to manage disk space allocations for a database as it grows or shrinks in size.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Parallel Data Warehouse  
ALTER DATABASE database_name    
    SET ( <set_database_options>   | <db_encryption_option> )  
[;]  
  
<set_database_options> ::=   
{  
    AUTOGROW = { ON | OFF }  
    | REPLICATED_SIZE = size [GB]  
    | DISTRIBUTED_SIZE = size [GB]  
    | LOG_SIZE = size [GB]  
}  
  
<db_encryption_option> ::=  
    ENCRYPTION { ON | OFF }  
```  
  
## Arguments  
 *database_name*  
 The name of the database to be modified. To display a list of databases on the appliance, use [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md).  
  
 AUTOGROW = { ON | OFF }  
 Updates the AUTOGROW option. When AUTOGROW is ON, [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] automatically increases the allocated space for replicated tables, distributed tables, and the transaction log as necessary to accommodate growth in storage requirements. When AUTOGROW is OFF, [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] returns an error if replicated tables, distributed tables, or the transaction log exceeds the maximum size setting.  
  
 REPLICATED_SIZE = *size* [GB]  
 Specifies the new maximum gigabytes per Compute node for storing all of the replicated tables in the database being altered. If you are planning for appliance storage space, you will need to multiply REPLICATED_SIZE by the number of Compute nodes in the appliance.  
  
 DISTRIBUTED_SIZE = *size* [GB]  
 Specifies the new maximum gigabytes per database for storing all of the distributed tables in the database being altered. The size is distributed across all of the Compute nodes in the appliance.  
  
 LOG_SIZE = *size* [GB]  
 Specifies the new maximum gigabytes per database for storing all of the transaction logs in the database being altered. The size is distributed across all of the Compute nodes in the appliance.  
  
 ENCRYPTION { ON | OFF }  
 Sets the database to be encrypted (ON) or not encrypted (OFF). Encryption can only be configured for [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] when [sp_pdw_database_encryption](http://msdn.microsoft.com/en-us/5011bb7b-1793-4b2b-bd9c-d4a8c8626b6e) has been set to **1**. A database encryption key must be created before transparent data encryption can be configured. For more information about database encryption, see [Transparent Data Encryption &#40;TDE&#41;](../../relational-databases/security/encryption/transparent-data-encryption.md).  
  
## Permissions  
 Requires the ALTER permission on the database.  
  
## General Remarks  
 The values for REPLICATED_SIZE, DISTRIBUTED_SIZE, and LOG_SIZE can be greater than, equal to, or less than the current values for the database.  
  
## Limitations and Restrictions  
 Grow and shrink operations are approximate. The resulting actual sizes can vary from the size parameters.  
  
 [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] does not perform the ALTER DATABASE statement as an atomic operation. If the statement is aborted during execution, changes that have already occurred will remain.  
  
## Locking Behavior  
 Takes a shared lock on the DATABASE object. You cannot alter a database that is in use by another user for reading or writing. This includes sessions that have issued a [USE](http://msdn.microsoft.com/en-us/158ec56b-b822-410f-a7c4-1a196d4f0e15) statement on the database.  
  
## Performance  
 Shrinking a database can take a large amount of time and system resources, depending on the size of the actual data within the database, and the amount of fragmentation on disk. For example, shrinking a database could take serveral hours or more.  
  
## Determining Encryption Progress  
 Use the following query to determine progress of database transparent data encryption as a percent:  
  
```  
WITH  
database_dek AS (  
    SELECT ISNULL(db_map.database_id, dek.database_id) AS database_id,  
        dek.encryption_state, dek.percent_complete,  
        dek.key_algorithm, dek.key_length, dek.encryptor_thumbprint,  
        type  
    FROM sys.dm_pdw_nodes_database_encryption_keys AS dek  
    INNER JOIN sys.pdw_nodes_pdw_physical_databases AS node_db_map  
        ON dek.database_id = node_db_map.database_id   
        AND dek.pdw_node_id = node_db_map.pdw_node_id  
    LEFT JOIN sys.pdw_database_mappings AS db_map  
        ON node_db_map .physical_name = db_map.physical_name  
    INNER JOIN sys.dm_pdw_nodes nodes  
        ON nodes.pdw_node_id = dek.pdw_node_id  
    WHERE dek.encryptor_thumbprint <> 0x  
),  
dek_percent_complete AS (  
    SELECT database_dek.database_id, AVG(database_dek.percent_complete) AS percent_complete  
    FROM database_dek  
    WHERE type = 'COMPUTE'  
    GROUP BY database_dek.database_id  
)  
SELECT DB_NAME( database_dek.database_id ) AS name,  
    database_dek.database_id,  
    ISNULL(  
       (SELECT TOP 1 dek_encryption_state.encryption_state  
        FROM database_dek AS dek_encryption_state  
        WHERE dek_encryption_state.database_id = database_dek.database_id  
        ORDER BY (CASE encryption_state  
            WHEN 3 THEN -1  
            ELSE encryption_state  
            END) DESC), 0)  
        AS encryption_state,  
dek_percent_complete.percent_complete,  
database_dek.key_algorithm, database_dek.key_length, database_dek.encryptor_thumbprint  
FROM database_dek  
INNER JOIN dek_percent_complete   
    ON dek_percent_complete.database_id = database_dek.database_id  
WHERE type = 'CONTROL';  
```  
  
 For a comprehensive example demonstrating all the steps in implementing TDE, see [Transparent Data Encryption &#40;TDE&#41;](../../relational-databases/security/encryption/transparent-data-encryption.md).  
  
## Examples: [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### A. Altering the AUTOGROW setting  
 Set AUTOGROW to ON for database `CustomerSales`.  
  
```  
ALTER DATABASE CustomerSales  
    SET ( AUTOGROW = ON );  
```  
  
### B. Altering the maximum storage for replicated tables  
 The following example sets the replicated table storage limit to 1 GB for the database `CustomerSales`. This is the storage limit per Compute node.  
  
```  
ALTER DATABASE CustomerSales  
    SET ( REPLICATED_SIZE = 1 GB );  
```  
  
### C. Altering the maximum storage for distributed tables  
 The following example sets the distributed table storage limit to 1000 GB (one terabyte) for the database `CustomerSales`. This is the combined storage limit across the appliance for all of the Compute nodes, not the storage limit per Compute node.  
  
```  
ALTER DATABASE CustomerSales  
    SET ( DISTRIBUTED_SIZE = 1000 GB );  
```  
  
### D. Altering the maximum storage for the transaction log  
 The following example updates the database `CustomerSales` to have a maximum [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] transaction log size of 10 GB for the appliance.  
  
```  
ALTER DATABASE CustomerSales  
    SET ( LOG_SIZE = 10 GB );  
```  
  
## See Also  
 [CREATE DATABASE &#40;Parallel Data Warehouse&#41;](../../t-sql/statements/create-database-parallel-data-warehouse.md)   
 [DROP DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-database-transact-sql.md)  
  
  