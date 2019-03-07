---
title: "DROP DATABASE ENCRYPTION KEY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/20/2017"
ms.prod: sql
ms.prod_service: "sql-data-warehouse, pdw, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DROP DATABASE ENCRYPTION KEY"
  - "DROP_DATABASE_ENCRYPTION_KEY_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "database encryption key, drop"
  - "DROP DATABASE ENCRYPTION KEY statement"
ms.assetid: 9231bd89-75e1-45c4-b4c8-13f08695af68
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DROP DATABASE ENCRYPTION KEY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-ss2008-xxxx-asdw-pdw-md.md)]

  Drops a database encryption key that is used in transparent database encryption. For more information about transparent database encryption, see [Transparent Data Encryption &#40;TDE&#41;](../../relational-databases/security/encryption/transparent-data-encryption.md).  
  
> [!IMPORTANT]  
>  The backup of the certificate that was protecting the database encryption key should be retained even if the encryption is no longer enabled on a database. Even though the database is not encrypted anymore, parts of the transaction log may still remain protected, and the certificate may be needed for some operations until the full backup of the database is performed.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
DROP DATABASE ENCRYPTION KEY  
```  
  
## Remarks  
 If the database is encrypted, you must first remove encryption from the database by using the ALTER DATABASE statement. Wait for decryption to complete before removing the database encryption key. For more information about the ALTER DATABASE statement, see [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md). To view the state of the database, use the [sys.dm_database_encryption_keys](../../relational-databases/system-dynamic-management-views/sys-dm-database-encryption-keys-transact-sql.md) dynamic management view.  
  
## Permissions  
 Requires CONTROL permission on the database.  
  
## Examples  
 The following example removes the database encryption and drops the database encryption key.  
  
```  
ALTER DATABASE AdventureWorks2012  
SET ENCRYPTION OFF;  
GO  
/* Wait for decryption operation to complete, look for a   
value of  1 in the query below. */  
SELECT encryption_state  
FROM sys.dm_database_encryption_keys;  
GO  
USE AdventureWorks2012;  
GO  
DROP DATABASE ENCRYPTION KEY;  
GO  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
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
 [Transparent Data Encryption &#40;TDE&#41;](../../relational-databases/security/encryption/transparent-data-encryption.md)   
 [SQL Server Encryption](../../relational-databases/security/encryption/sql-server-encryption.md)   
 [SQL Server and Database Encryption Keys &#40;Database Engine&#41;](../../relational-databases/security/encryption/sql-server-and-database-encryption-keys-database-engine.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)   
 [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md)   
 [CREATE DATABASE ENCRYPTION KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-database-encryption-key-transact-sql.md)   
 [ALTER DATABASE ENCRYPTION KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-encryption-key-transact-sql.md)   
 [sys.dm_database_encryption_keys &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-database-encryption-keys-transact-sql.md)  
  
  

