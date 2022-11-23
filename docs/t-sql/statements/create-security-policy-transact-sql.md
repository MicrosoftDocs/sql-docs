---
title: "CREATE SECURITY POLICY (Transact-SQL)"
description: CREATE SECURITY POLICY (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "08/10/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "SECURITY_POLICY_TSQL"
  - "CREATE SECURITY"
  - "SECURITY"
  - "CREATE_SECURITY_POLICY_TSQL"
  - "CREATE_SECURITY_TSQL"
  - "SECURITY POLICY"
  - "SECURITY_TSQL"
  - "CREATE SECURITY POLICY"
helpviewer_keywords:
  - "RLS"
  - "CREATE SECURITY POLICY statement"
  - "Row-Level Security"
dev_langs:
  - "TSQL"
---
# CREATE SECURITY POLICY (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

  Creates a security policy for row level security.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
CREATE SECURITY POLICY [schema_name. ] security_policy_name    
    { ADD [ FILTER | BLOCK ] } PREDICATE tvf_schema_name.security_predicate_function_name   
      ( { column_name | expression } [ , ...n] ) ON table_schema_name. table_name    
      [ <block_dml_operation> ] , [ , ...n] 
    [ WITH ( STATE = { ON | OFF }  [,] [ SCHEMABINDING = { ON | OFF } ] ) ]  
    [ NOT FOR REPLICATION ] 
[;]  
  
<block_dml_operation>  
    [ { AFTER { INSERT | UPDATE } }   
    | { BEFORE { UPDATE | DELETE } } ]  
```  

## Arguments
 *security_policy_name*  
 The name of the security policy. Security policy names must comply with the rules for identifiers and must be unique within the database and to its schema.  
  
 *schema_name*  
 Is the name of the schema to which the security policy belongs. *schema_name* is required because of schema binding.  
  
 [ FILTER | BLOCK ]  
 The type of security predicate for the function being bound to the target table. FILTER predicates silently filter the rows that are available to read operations. BLOCK predicates explicitly block write operations that violate the predicate function.  
  
 *tvf_schema_name.security_predicate_function_name*  
 Is the inline table value function that will be used as a predicate and that will be enforced upon queries against a target table. At most one security predicate can be defined for a particular DML operation against a particular table. The inline table value function must have been created using the SCHEMABINDING option.  
  
 { *column_name* | *expression* }  
 A column name or expression used as a parameter for the security predicate function. Any column on the target table can be used. An [Expression](../../t-sql/language-elements/expressions-transact-sql.md) can only include constants, built in scalar functions, operators and columns from the target table. A column name or expression needs to be specified for each parameter of the function.  
  
 *table_schema_name.table_name*  
 Is the target table to which the security predicate will be applied. Multiple disabled security policies can target a single table for a particular DML operation, but only one can be enabled at any given time.  
  
 *\<block_dml_operation>* 
 The particular DML operation for which the block predicate will be applied. AFTER specifies that the predicate will be evaluated on the values of the rows after the DML operation was performed (INSERT or UPDATE). BEFORE specifies that the predicate will be evaluated on the values of the rows before the DML operation is performed (UPDATE or DELETE). If no operation is specified, the predicate will apply to all operations.  
  
 [ STATE = { **ON** | OFF } ]  
 Enables or disables the security policy from enforcing its security predicates against the target tables. If not specified the security policy being created is enabled.  
  
 [ SCHEMABINDING = { **ON** | OFF } ]  
 Indicates whether all predicate functions in the policy must be created with the SCHEMABINDING option. By default this setting is **ON** and all functions must be created with SCHEMABINDING.  
  
 NOT FOR REPLICATION  
 Indicates that the security policy should not be executed when a replication agent modifies the target object. For more information, see [Control the Behavior of Triggers and Constraints During Synchronization &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/control-behavior-of-triggers-and-constraints-in-synchronization.md).  
  
 [*table_schema_name*.] *table_name*  
 Is the target table to which the security predicate will be applied. Multiple disabled security policies can target a single table, but only one can be enabled at any given time.  
  

## Remarks
 When using predicate functions with memory-optimized tables, you must include **SCHEMABINDING** and use the **WITH NATIVE_COMPILATION** compilation hint.  
  
 Block predicates are evaluated after the corresponding DML operation is executed. Therefore, a READ UNCOMMITTED query can see transient values that will be rolled back.  
  
## Permissions  
 Requires the ALTER ANY SECURITY POLICY permission and ALTER permission on the schema.  
  
 Additionally the following permissions are required for each predicate that is added:  
  
-   SELECT and REFERENCES permissions on the function being used as a predicate.  
  
-   REFERENCES permission on the target table being bound to the policy.  
  
-   REFERENCES permission on every column from the target table used as arguments.  
  
## Examples  
 The following examples demonstrate the use of the **CREATE SECURITY POLICY** syntax. For an example of a complete security policy scenario, see [Row-Level Security](../../relational-databases/security/row-level-security.md).  
  
### A. Creating a security policy  
 The following syntax creates a security policy with a filter predicate for the Customer table, and leaves the security policy disabled.  
  
```  
CREATE SECURITY POLICY [FederatedSecurityPolicy]   
ADD FILTER PREDICATE [rls].[fn_securitypredicate]([CustomerId])   
ON [dbo].[Customer];  
```  
  
### B. Creating a policy that affects multiple tables  
 The following syntax creates a security policy with three filter predicates on three different tables, and enables the security policy.  
  
```sql  
CREATE SECURITY POLICY [FederatedSecurityPolicy]   
ADD FILTER PREDICATE [rls].[fn_securitypredicate1]([CustomerId])   
    ON [dbo].[Customer],  
ADD FILTER PREDICATE [rls].[fn_securitypredicate1]([VendorId])   
    ON [dbo].[ Vendor],  
ADD FILTER PREDICATE [rls].[fn_securitypredicate2]([WingId])   
    ON [dbo].[Patient]  
WITH (STATE = ON);  
```  
  
### C. Creating a policy with multiple types of security predicates  
 Adding both a filter predicate and a block predicate to the Sales table.  
  
```sql  
CREATE SECURITY POLICY rls.SecPol  
    ADD FILTER PREDICATE rls.tenantAccessPredicate(TenantId) ON dbo.Sales,  
    ADD BLOCK PREDICATE rls.tenantAccessPredicate(TenantId) ON dbo.Sales AFTER INSERT;  
```  
  
## See Also  
 [Row-Level Security](../../relational-databases/security/row-level-security.md)   
 [ALTER SECURITY POLICY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-security-policy-transact-sql.md)   
 [DROP SECURITY POLICY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-security-policy-transact-sql.md)   
 [sys.security_policies &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-security-policies-transact-sql.md)   
 [sys.security_predicates &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-security-predicates-transact-sql.md)  
  
  

