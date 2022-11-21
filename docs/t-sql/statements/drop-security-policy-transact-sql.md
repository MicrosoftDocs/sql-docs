---
title: "DROP SECURITY POLICY (Transact-SQL)"
description: DROP SECURITY POLICY (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "05/11/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP_SECURITY_POLICY_TSQL"
  - "DROP SECURITY POLICY"
  - "DROP SECURITY"
  - "DROP_SECURITY_TSQL"
helpviewer_keywords:
  - "DROP SECURITY POLICY statement"
dev_langs:
  - "TSQL"
---
# DROP SECURITY POLICY (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

  Deletes a security policy.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DROP SECURITY POLICY [ IF EXISTS ] [schema_name. ] security_policy_name    
[;]  
```  

## Arguments
 *IF EXISTS*  
 **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] through [current version](/troubleshoot/sql/general/determine-version-edition-update-level)).  
  
 Conditionally drops the security policy only if it already exists.  
  
 *schema_name*  
 Is the name of the schema to which the security policy belongs.  
  
 *security_policy_name*  
 The name of the security policy. Security policy names must comply with the rules for identifiers and must be unique within the database and to its schema.  
  
## Remarks
  
## Permissions  
 Requires the ALTER ANY SECURITY POLICY permission and ALTER permission on the schema.  
  
## Example  
  
```sql  
DROP SECURITY POLICY secPolicy;  
```  
  
## See Also  
 [Row-Level Security](../../relational-databases/security/row-level-security.md)   
 [CREATE SECURITY POLICY &#40;Transact-SQL&#41;](../../t-sql/statements/create-security-policy-transact-sql.md)   
 [ALTER SECURITY POLICY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-security-policy-transact-sql.md)   
 [sys.security_policies &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-security-policies-transact-sql.md)   
 [sys.security_predicates &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-security-predicates-transact-sql.md)  
