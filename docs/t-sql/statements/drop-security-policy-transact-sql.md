---
title: "DROP SECURITY POLICY (Transact-SQL)"
description: DROP SECURITY POLICY a security policy for use with row-level security.
author: VanMSFT
ms.author: vanto
ms.date: 10/04/2023
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
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current||=fabric"
---
# DROP SECURITY POLICY (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-fabricse-fabricdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-fabricse-fabricdw.md)]

  Deletes a security policy.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md) 
  
## Syntax
  
```syntaxsql
DROP SECURITY POLICY [ IF EXISTS ] [schema_name. ] security_policy_name    
[;]  
```  

## Arguments

#### IF EXISTS
 **Applies to**: [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] through [current version](/troubleshoot/sql/general/determine-version-edition-update-level)).  
  
 Conditionally drops the security policy only if it already exists.  
  
#### *schema_name*


 Is the name of the schema to which the security policy belongs.  
  
#### *security_policy_name*


 The name of the security policy. Security policy names must comply with the rules for identifiers and must be unique within the database and to its schema.  
  
## Remarks
  
## Permissions


 Requires the ALTER ANY SECURITY POLICY permission and ALTER permission on the schema.  
  
## Examples
  
```sql  
DROP SECURITY POLICY secPolicy;  
```  
  
## Related content

- [Row-level security](../../relational-databases/security/row-level-security.md)
- [CREATE SECURITY POLICY (Transact-SQL)](../../t-sql/statements/create-security-policy-transact-sql.md)   
- [ALTER SECURITY POLICY (Transact-SQL)](../../t-sql/statements/alter-security-policy-transact-sql.md)   
- [sys.security_policies (Transact-SQL)](../../relational-databases/system-catalog-views/sys-security-policies-transact-sql.md)   
- [sys.security_predicates (Transact-SQL)](../../relational-databases/system-catalog-views/sys-security-predicates-transact-sql.md)  
