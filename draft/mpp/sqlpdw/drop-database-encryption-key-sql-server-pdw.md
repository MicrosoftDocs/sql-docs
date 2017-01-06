---
title: "DROP DATABASE ENCRYPTION KEY (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: db91c67e-7135-4dcd-a623-7b1fd4dcc8b3
caps.latest.revision: 10
author: BarbKess
---
# DROP DATABASE ENCRYPTION KEY (SQL Server PDW)
Drops a SQL Server PDW database encryption key that is used in transparent database encryption. For more information about transparent database encryption, see [Transparent Data Encryption &#40;SQL Server PDW&#41;](../sqlpdw/transparent-data-encryption-sql-server-pdw.md).  
  
> [!IMPORTANT]  
> The backup of the certificate that was protecting the database encryption key should be retained even if the encryption is no longer enabled on a database. Even though the database is not encrypted anymore, parts of the transaction log may still remain protected, and the certificate may be needed for some operations until the full backup of the database is performed.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DROP DATABASE ENCRYPTION KEY  
```  
  
## Remarks  
If the database is encrypted, you must first remove encryption from the database by using the **ALTER DATABASE** statement. Wait for decryption to complete before removing the database encryption key. For more information about the **ALTER DATABASE** statement, see [ALTER DATABASE &#40;SQL Server PDW&#41;](../sqlpdw/alter-database-sql-server-pdw.md). To view the state of the database, use the [sys.dm_pdw_nodes_database_encryption_keys &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-nodes-database-encryption-keys-sql-server-pdw.md) dynamic management view.  
  
## Permissions  
Requires **CONTROL** permission on the database.  
  
## Examples  
The following example removes the TDE encryption and then drops the database encryption key.  
  
```  
ALTER DATABASE AdventureWorksPDW2012  
    SET ENCRYPTION OFF;  
GO  
/* Wait for decryption operation to complete, look for a   
value of  1 in the query below. */  
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
GO  
USE AdventureWorksPDW2012;  
GO  
DROP DATABASE ENCRYPTION KEY;  
GO  
```  
  
## See Also  
[Transparent Data Encryption &#40;SQL Server PDW&#41;](../sqlpdw/transparent-data-encryption-sql-server-pdw.md)  
[CREATE DATABASE ENCRYPTION KEY &#40;SQL Server PDW&#41;](../sqlpdw/create-database-encryption-key-sql-server-pdw.md)  
[ALTER DATABASE ENCRYPTION KEY &#40;SQL Server PDW&#41;](../sqlpdw/alter-database-encryption-key-sql-server-pdw.md)  
[sys.dm_pdw_nodes_database_encryption_keys &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-nodes-database-encryption-keys-sql-server-pdw.md)  
[sp_pdw_database_encryption_regenerate_system_keys &#40;SQL Server PDW&#41;](../sqlpdw/sp-pdw-database-encryption-regenerate-system-keys-sql-server-pdw.md)  
  
