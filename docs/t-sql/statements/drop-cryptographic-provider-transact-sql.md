---
title: "DROP CRYPTOGRAPHIC PROVIDER (Transact-SQL)"
description: DROP CRYPTOGRAPHIC PROVIDER (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP CRYPTOGRAPHIC PROVIDER"
  - "DROP_CRYPTOGRAPHIC_PROVIDER_TSQL"
helpviewer_keywords:
  - "DROP CRYPTOGRAPHIC PROVIDER statement"
dev_langs:
  - "TSQL"
---
# DROP CRYPTOGRAPHIC PROVIDER (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Drops a cryptographic provider within [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DROP CRYPTOGRAPHIC PROVIDER provider_name   
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *provider_name*  
 Is the name of the Extensible Key Management provider.  
  
## Remarks  
 To delete an Extensible Key Management (EKM) provider, all sessions that use the provider must be stopped.  
  
 An EKM provider can only be dropped if there are no credentials mapped to it.  
  
 If there are keys mapped to an EKM provider when it is dropped the GUIDs for the keys remain stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If a provider is created later with the same key GUIDs, the keys will be reused.  
  
## Permissions  
 Requires CONTROL permission on the symmetric key.  
  
## Examples  
 The following example drops a cryptographic provider called `SecurityProvider`.  
  
```sql  
/* First, disable provider to perform the upgrade.  
This will terminate all open cryptographic sessions. */  
ALTER CRYPTOGRAPHIC PROVIDER SecurityProvider   
SET ENABLED = OFF;  
GO  
/* Drop the provider. */  
DROP CRYPTOGRAPHIC PROVIDER SecurityProvider;  
GO  
```  
  
## See Also  
 [Extensible Key Management &#40;EKM&#41;](../../relational-databases/security/encryption/extensible-key-management-ekm.md)   
 [CREATE CRYPTOGRAPHIC PROVIDER &#40;Transact-SQL&#41;](../../t-sql/statements/create-cryptographic-provider-transact-sql.md)   
 [ALTER CRYPTOGRAPHIC PROVIDER &#40;Transact-SQL&#41;](../../t-sql/statements/alter-cryptographic-provider-transact-sql.md)   
 [CREATE SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-symmetric-key-transact-sql.md)  
  
  
