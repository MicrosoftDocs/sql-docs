---
title: "DROP SECURITY POLICY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/11/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DROP_SECURITY_POLICY_TSQL"
  - "DROP SECURITY POLICY"
  - "DROP SECURITY"
  - "DROP_SECURITY_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "DROP SECURITY POLICY statement"
ms.assetid: 5bd3393d-2fa5-4db0-a69a-a1a72d638e9d
author: VanMSFT
ms.author: vanto
manager: craigg
---
# DROP SECURITY POLICY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  Deletes a security policy.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
DROP SECURITY POLICY [ IF EXISTS ] [schema_name. ] security_policy_name    
[;]  
```  
  
## Arguments  
 *IF EXISTS*  
 **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [current version](https://go.microsoft.com/fwlink/p/?LinkId=299658)).  
  
 Conditionally drops the security policy only if it already exists.  
  
 *schema_name*  
 Is the name of the schema to which the security policy belongs.  
  
 *security_policy_name*  
 The name of the security policy. Security policy names must comply with the rules for identifiers and must be unique within the database and to its schema.  
  
## Remarks  
  
## Permissions  
 Requires the ALTER ANY SECURITY POLICY permission and ALTER permission on the schema.  
  
## Example  
  
```  
DROP SECURITY POLICY secPolicy;  
```  
  
## See Also  
 [Row-Level Security](../../relational-databases/security/row-level-security.md)   
 [CREATE SECURITY POLICY &#40;Transact-SQL&#41;](../../t-sql/statements/create-security-policy-transact-sql.md)   
 [ALTER SECURITY POLICY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-security-policy-transact-sql.md)   
 [sys.security_policies &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-security-policies-transact-sql.md)   
 [sys.security_predicates &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-security-predicates-transact-sql.md)  
  
  
