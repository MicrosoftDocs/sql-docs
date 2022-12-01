---
description: "sp_bindrule (Transact-SQL)"
title: "sp_bindrule (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "11/25/2015"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_bindrule_TSQL"
  - "sp_bindrule"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_bindrule"
ms.assetid: 2606073e-c52f-498d-a923-5026b9d97e67
author: markingmyname
ms.author: maghan
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_bindrule (Transact-SQL)
[!INCLUDE [SQL Server SQL Database](../../includes/applies-to-version/sql-asdb.md)]

  Binds a rule to a column or to an alias data type.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use[Unique Constraints and Check Constraints](../../relational-databases/tables/unique-constraints-and-check-constraints.md) instead. CHECK constraints are created by using the CHECK keyword of the [CREATE TABLE](../../t-sql/statements/create-table-transact-sql.md) or [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md) statements.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_bindrule [ @rulename = ] 'rule' ,   
     [ @objname = ] 'object_name'   
     [ , [ @futureonly = ] 'futureonly_flag' ]   
```  
  
## Arguments  
`[ @rulename = ] 'rule'`
 Is the name of a rule created by the CREATE RULE statement. *rule* is **nvarchar(776)**, with no default.  
  
`[ @objname = ] 'object_name'`
 Is the table and column, or the alias data type to which the rule is to be bound. A rule cannot be bound to a **text**, **ntext**, **image**, **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **xml**, CLR user-defined type, or **timestamp**column. A rule cannot be bound to a computed column.  
  
 *object_name* is **nvarchar(776)** with no default. If *object_name* is a one-part name, it is resolved as an alias data type. If it is a two- or three-part name, it is first resolved as a table and column; if this resolution fails, it is resolved as an alias data type. By default, existing columns of the alias data type inherit *rule* unless a rule has been bound directly to the column.  
  
> [!NOTE]  
>  *object_name* can contain the bracket **[** and **]** characters as delimited identifier characters. For more information, see [Database Identifiers](../../relational-databases/databases/database-identifiers.md).  
  
> [!NOTE]  
>  Rules created on expressions that use alias data types can be bound to columns or alias data types, but fail to compile when they are referenced. Avoid using rules created on alias data types.  
  
`[ @futureonly = ] 'futureonly_flag'`
 Is used only when binding a rule to an alias data type. *future_only_flag* is **varchar(15)** with a default of NULL. This parameter when set to **futureonly** prevents existing columns of an alias data type from inheriting the new rule. If *futureonly_flag* is NULL, the new rule is bound to any columns of the alias data type that currently have no rule or that are using the existing rule of the alias data type.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 You can bind a new rule to a column (although using a CHECK constraint is preferred) or to an alias data type with **sp_bindrule** without unbinding an existing rule. The old rule is overridden. If a rule is bound to a column with an existing CHECK constraint, all restrictions are evaluated. You cannot bind a rule to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type.  
  
 The rule is enforced when an INSERT statement is tried, not at binding. You can bind a character rule to a column of **numeric** data type, although such an INSERT operation is not valid.  
  
 Existing columns of the alias data type inherit the new rule unless *futureonly_flag* is specified as **futureonly**. New columns defined with the alias data type always inherit the rule. However, if the ALTER COLUMN clause of an ALTER TABLE statement changes the data type of a column to an alias data type bound to a rule, the rule bound to the data type is not inherited by the column. The rule must be specifically bound to the column by using **sp_bindrule**.  
  
 When you bind a rule to a column, related information is added to the **sys.columns** table. When you bind a rule to an alias data type, related information is added to the **sys.types** table.  
  
## Permissions  
 To bind a rule to a table column, you must have ALTER permission on the table. CONTROL permission on the alias data type, or ALTER permission on the schema to which the type belongs, is required to bind a rule to an alias data type.  
  
## Examples  
  
### A. Binding a rule to a column  
 Assuming that a rule named `today` has been created in the current database by using the CREATE RULE statement, the following example binds the rule to the `HireDate` column of the `Employee` table. When a row is added to `Employee`, the data for the `HireDate` column is checked against the `today` rule.  
  
```  
USE master;  
GO  
EXEC sp_bindrule 'today', 'HumanResources.Employee.HireDate';  
```  
  
### B. Binding a rule to an alias data type  
 Assuming the existence of a rule named `rule_ssn` and an alias data type named `ssn`, the following example binds `rule_ssn` to `ssn`. In a CREATE TABLE statement, columns of type `ssn` inherit the `rule_ssn` rule. Existing columns of type `ssn` also inherit the `rule_ssn` rule, unless **futureonly** is specified for *futureonly_flag*, or `ssn` has a rule bound directly to it. Rules bound to columns always take precedence over those bound to data types.  
  
```  
USE master;  
GO  
EXEC sp_bindrule 'rule_ssn', 'ssn';  
```  
  
### C. Using the futureonly_flag  
 The following example binds the `rule_ssn` rule to the alias data type `ssn`. Because `futureonly` is specified, no existing columns of type `ssn` are affected.  
  
```  
USE master;  
GO  
EXEC sp_bindrule rule_ssn, 'ssn', 'futureonly';  
```  
  
### D. Using delimited identifiers  
 The following example shows the use of delimited identifiers in *object_name* parameter.  
  
```  
USE master;  
GO  
CREATE TABLE [t.2] (c1 int) ;  
-- Notice the period as part of the table name.  
EXEC sp_bindrule rule1, '[t.2].c1' ;  
-- The object contains two periods;   
-- the first is part of the table name   
-- and the second distinguishes the table name from the column name.  
```  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)   
 [CREATE RULE &#40;Transact-SQL&#41;](../../t-sql/statements/create-rule-transact-sql.md)   
 [DROP RULE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-rule-transact-sql.md)   
 [sp_unbindrule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-unbindrule-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
