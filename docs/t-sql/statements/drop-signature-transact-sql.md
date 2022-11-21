---
title: "DROP SIGNATURE (Transact-SQL)"
description: DROP SIGNATURE (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP SIGNATURE"
  - "DROP_SIGNATURE_TSQL"
helpviewer_keywords:
  - "deleting signatures"
  - "dropping signatures"
  - "DROP SIGNATURE statement"
  - "removing signatures"
  - "signatures [SQL Server]"
  - "digital signatures [SQL Server]"
dev_langs:
  - "TSQL"
---
# DROP SIGNATURE (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Drops a digital signature from a stored procedure, function, trigger, or assembly.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DROP [ COUNTER ] SIGNATURE FROM module_name   
    BY <crypto_list> [ ,...n ]  
  
<crypto_list> ::=  
    CERTIFICATE cert_name  
    | ASYMMETRIC KEY Asym_key_name  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *module_name*  
 Is the name of a stored procedure, function, assembly, or trigger.  
  
 CERTIFICATE *cert_name*  
 Is the name of a certificate with which the stored procedure, function, assembly, or trigger is signed.  
  
 ASYMMETRIC KEY *Asym_key_name*  
 Is the name of an asymmetric key with which the stored procedure, function, assembly, or trigger is signed.  
  
## Remarks  
 Information about signatures is visible in the sys.crypt_properties catalog view.  
  
## Permissions  
 Requires ALTER permission on the object and CONTROL permission on the certificate or asymmetric key. If an associated private key is protected by a password, the user also must have the password.  
  
## Examples  
 The following example removes the signature of certificate `HumanResourcesDP` from the stored procedure `HumanResources.uspUpdateEmployeeLogin`.  
  
```sql  
USE AdventureWorks2012;  
DROP SIGNATURE FROM HumanResources.uspUpdateEmployeeLogin   
    BY CERTIFICATE HumanResourcesDP;  
GO  
```  
  
## See Also  
 [sys.crypt_properties &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-crypt-properties-transact-sql.md)   
 [ADD SIGNATURE &#40;Transact-SQL&#41;](../../t-sql/statements/add-signature-transact-sql.md)  
  
  
