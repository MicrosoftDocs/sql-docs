---
title: "DROP SYMMETRIC KEY (Transact-SQL)"
description: DROP SYMMETRIC KEY (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP SYMMETRIC KEY"
  - "DROP_SYMMETRIC_KEY_TSQL"
helpviewer_keywords:
  - "symmetric keys [SQL Server], removing"
  - "deleting symmetric keys"
  - "encryption [SQL Server], symmetric keys"
  - "removing symmetric keys"
  - "dropping symmetric keys"
  - "cryptography [SQL Server], symmetric keys"
  - "DROP SYMMETRIC KEY statement"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||=azuresqldb-mi-current||>=sql-server-2016||>=sql-server-linux-2017||=azure-sqldw-latest"
---
# DROP SYMMETRIC KEY (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

  Removes a symmetric key from the current database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)] 
  
## Syntax  
  
```syntaxsql  
DROP SYMMETRIC KEY symmetric_key_name [REMOVE PROVIDER KEY]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *symmetric_key_name*  
 Is the name of the symmetric key to be dropped.  
  
 REMOVE PROVIDER KEY  
 Removes an Extensible Key Management (EKM) key from an EKM device. For more information about Extensible Key Management, see [Extensible Key Management &#40;EKM&#41;](../../relational-databases/security/encryption/extensible-key-management-ekm.md).  
  
## Remarks  
  
If the asymmetric key is mapped to an Extensible Key Management (EKM) key on an EKM device and the **REMOVE PROVIDER KEY** option is not specified, the key will be dropped from the database but not the device, and a warning will be issued.  
  
## Permissions  
 Requires CONTROL permission on the symmetric key.  
  
## Examples  
 The following example removes a symmetric key named `GailSammamishKey6` from the current database.  
  
```sql  
CLOSE SYMMETRIC KEY GailSammamishKey6;  
DROP SYMMETRIC KEY GailSammamishKey6;  
GO  
```  
  
## See also     
 [CREATE SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-symmetric-key-transact-sql.md)   
 [ALTER SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-symmetric-key-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)   
 [CLOSE SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/close-symmetric-key-transact-sql.md)   
 [Extensible Key Management &#40;EKM&#41;](../../relational-databases/security/encryption/extensible-key-management-ekm.md)  
  
  
