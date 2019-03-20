---
title: "sys.dm_database_encryption_keys (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date:03/25/2019"03/20/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_database_encryption_keys"
  - "sys.dm_database_encryption_keys_TSQL"
  - "dm_database_encryption_keys"
  - "dm_database_encryption_keys_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_database_encryption_keys dynamic management view"
ms.assetid: 56fee8f3-06eb-4fff-969e-abeaa0c4b8e4
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_database_encryption_keys (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns information about the encryption state of a database and its associated database encryption keys. For more information about database encryption, see [Transparent Data Encryption &#40;TDE&#41;](../../relational-databases/security/encryption/transparent-data-encryption.md).  
 
|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------|  
|database_id|**int**|ID of the database.|  
|encryption_state|**int**|Indicates whether the database is encrypted or not encrypted.<br /><br /> 0 = No database encryption key present, no encryption<br /><br /> 1 = Unencrypted<br /><br /> 2 = Encryption in progress<br /><br /> 3 = Encrypted<br /><br /> 4 = Key change in progress<br /><br /> 5 = Decryption in progress<br /><br /> 6 = Protection change in progress (The certificate or asymmetric key that is encrypting the database encryption key is being changed.)|  
|create_date|**datetime**|Displays the date (in UTC) the encryption key was created.|  
|regenerate_date|**datetime**|Displays the date (in UTC) the encryption key was regenerated.|  
|modify_date|**datetime**|Displays the date (in UTC) the encryption key was modified.|  
|set_date|**datetime**|Displays the date (in UTC) the encryption key was applied to the database.|  
|opened_date|**datetime**|Shows when (in UTC) the database key was last opened.|  
|key_algorithm|**nvarchar(32)**|Displays the algorithm that is used for the key.|  
|key_length|**int**|Displays the length of the key.|  
|encryptor_thumbprint|**varbinary(20)**|Shows the thumbprint of the encryptor.|  
|encryptor_type|**nvarchar(32)**|**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [current version](https://go.microsoft.com/fwlink/p/?LinkId=299658)).<br /><br /> Describes the encryptor.|  
|percent_complete|**real**|Percent complete of the database encryption state change. This will be 0 if there is no state change.|
|encryption_state_desc|**nvarchar(32)**|**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)] and later.<br><br> String that indicates whether the database is encrypted or not encrypted.<br><br>NONE<br><br>UNENCRYPTED<br><br>ENCRYPTED<br><br>DECRYPTION_IN_PROGRESS<br><br>ENCRYPTION_IN_PROGRESS<br><br>KEY_CHANGE_IN_PROGRESS<br><br>PROTECTION_CHANGE_IN_PROGRESS|
|encryption_scan_state|**int**|**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)] and later.<br><br>Indicates the current state of the encryption scan. <br><br>0 = No scan has been initiated, TDE is not enabled<br><br>1 = Scan is in progress.<br><br>2 = Scan is in progress but has been suspended, user can resume.<br><br>3 = Scan has been successfully completed, TDE is enabled and encryption is complete.<br><br>4 = Scan was aborted for some reason, manual intervention is required. Contact Microsoft Support for more assistance.|
|encryption_scan_state_desc|**nvarchar(32)**|**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)] and later.<br><br>String that indicates the current state of the encryption scan.<br><br> NONE<br><br>RUNNING<br><br>SUSPENDED<br><br>COMPLETE<br><br>ABORTED|
|encryption_scan_modify_date|**datetime**|**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)] and later.<br><br> Displays the date (in UTC) the encryption scan state was last modified.|
  
## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)], requires the `VIEW DATABASE STATE` permission in the database.   

## See Also  

 [Security-Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/security-related-dynamic-management-views-and-functions-transact-sql.md)   
 [Transparent Data Encryption &#40;TDE&#41;](../../relational-databases/security/encryption/transparent-data-encryption.md)   
 [SQL Server Encryption](../../relational-databases/security/encryption/sql-server-encryption.md)   
 [SQL Server and Database Encryption Keys &#40;Database Engine&#41;](../../relational-databases/security/encryption/sql-server-and-database-encryption-keys-database-engine.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)   
 [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md)   
 [CREATE DATABASE ENCRYPTION KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-database-encryption-key-transact-sql.md)   
 [ALTER DATABASE ENCRYPTION KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-encryption-key-transact-sql.md)   
 [DROP DATABASE ENCRYPTION KEY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-database-encryption-key-transact-sql.md)  
  
  
