---
title: "DROP SIGNATURE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DROP SIGNATURE"
  - "DROP_SIGNATURE_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "deleting signatures"
  - "dropping signatures"
  - "DROP SIGNATURE statement"
  - "removing signatures"
  - "signatures [SQL Server]"
  - "digital signatures [SQL Server]"
ms.assetid: 8a1fd8c5-0e75-4b2f-9d3c-c296bed56cc7
author: VanMSFT
ms.author: vanto
anager: craigg
---
# DROP SIGNATURE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Drops a digital signature from a stored procedure, function, trigger, or assembly.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
DROP [ COUNTER ] SIGNATURE FROM module_name   
    BY <crypto_list> [ ,...n ]  
  
<crypto_list> ::=  
    CERTIFICATE cert_name  
    | ASYMMETRIC KEY Asym_key_name  
```  
  
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
  
```  
USE AdventureWorks2012;  
DROP SIGNATURE FROM HumanResources.uspUpdateEmployeeLogin   
    BY CERTIFICATE HumanResourcesDP;  
GO  
```  
  
## See Also  
 [sys.crypt_properties &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-crypt-properties-transact-sql.md)   
 [ADD SIGNATURE &#40;Transact-SQL&#41;](../../t-sql/statements/add-signature-transact-sql.md)  
  
  
