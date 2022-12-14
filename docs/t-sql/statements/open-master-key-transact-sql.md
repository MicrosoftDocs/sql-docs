---
title: "OPEN MASTER KEY (Transact-SQL)"
description: OPEN MASTER KEY (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "OPEN MASTER KEY DECRYPTION BY PASSWORD"
  - "OPEN_MASTER_KEY_DECRYPTION_BY_PASSWORD_TSQL"
  - "MASTER_KEY_TSQL"
  - "MASTER KEY"
  - "OPEN_MASTER_KEY_TSQL"
  - "MASTER KEY DECRYPTION"
  - "OPEN MASTER KEY"
  - "MASTER_KEY_DECRYPTION_TSQL"
helpviewer_keywords:
  - "opening Database Master Keys"
  - "encryption [SQL Server], Database Master Key"
  - "cryptography [SQL Server], Database Master Key"
  - "master key decryption"
  - "OPEN MASTER KEY statement"
  - "database master key [SQL Server], opening"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# OPEN MASTER KEY (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Opens the Database Master Key of the current database.  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
OPEN MASTER KEY DECRYPTION BY PASSWORD = 'password'   
```

> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

'*password*'  
The password with which the Database Master Key was encrypted.  
  
## Remarks

If the database master key was encrypted with the service master key, it will be automatically opened when it is needed for decryption or encryption. In this case, it is not necessary to use the **OPEN MASTER KEY** statement.  
  
When a database is first attached or restored to a new instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a copy of the database master key (encrypted by the service master key) is not yet stored in the server. You must use the **OPEN MASTER KEY** statement to decrypt the database master key (DMK). Once the DMK has been decrypted, you have the option of enabling automatic decryption in the future by using the **ALTER MASTER KEY REGENERATE** statement to provision the server with a copy of the DMK, encrypted with the service master key (SMK). When a database has been upgraded from an earlier version, the DMK should be regenerated to use the newer AES algorithm. For more information about regenerating the DMK, see [ALTER MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-master-key-transact-sql.md). The time required to regenerate the DMK key to upgrade to AES depends upon the number of objects protected by the DMK. Regenerating the DMK key to upgrade to AES is only necessary once, and has no impact on future regenerations as part of a key rotation strategy.  
  
You can exclude the Database Master Key of a specific database from automatic key management by using the ALTER MASTER KEY statement with the DROP ENCRYPTION BY SERVICE MASTER KEY option. Afterward, you must explicitly open the Database Master Key with a password.  
  
If a transaction in which the Database Master Key was explicitly opened is rolled back, the key will remain open.  
  
## Permissions

Requires CONTROL permission on the database.  
  
## Examples

The following example opens the Database Master Key of the `AdventureWorks2012` database, which has been encrypted with a password.  
  
```sql  
USE AdventureWorks2012;  
OPEN MASTER KEY DECRYPTION BY PASSWORD = '43987hkhj4325tsku7';  
GO  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]

The following example opens the database master, which has been encrypted with a password.  
  
```sql  
USE master;  
OPEN MASTER KEY DECRYPTION BY PASSWORD = '43987hkhj4325tsku7';  
GO  
CLOSE MASTER KEY;  
GO  
```  
  
## See Also

- [CREATE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-master-key-transact-sql.md)
- [CLOSE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/close-master-key-transact-sql.md)
- [BACKUP MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/backup-master-key-transact-sql.md)
- [RESTORE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-master-key-transact-sql.md)
- [ALTER MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-master-key-transact-sql.md)
- [DROP MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-master-key-transact-sql.md)
- [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)
