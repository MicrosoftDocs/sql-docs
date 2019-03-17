---
title: "sp_unbindrule (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_unbindrule_TSQL"
  - "sp_unbindrule"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_unbindrule"
ms.assetid: f54ee155-c3c9-4f1a-952e-632a8339f0cc
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_unbindrule (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Unbinds a rule from a column or an alias data type in the current database.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepNextDontUse](../../includes/ssnotedepnextdontuse-md.md)] We recommend that you create default definitions by using the DEFAULT keyword in the [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md) or [CREATE TABLE](../../t-sql/statements/create-table-transact-sql.md) statements instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_unbindrule [ @objname = ] 'object_name'   
     [ , [ @futureonly = ] 'futureonly_flag' ]  
```  
  
## Arguments  
 [ **@objname=** ] **'***object_name***'**  
 Is the name of the table and column or the alias data type from which the rule is unbound. *object_name* is **nvarchar(776)**, with no default. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] attempts to resolve two-part identifiers to column names first, then to alias data types. When unbinding a rule from an alias data type, any columns of the data type that have the same rule are also unbound. Columns of that data type with rules bound directly to them are unaffected.  
  
> [!NOTE]  
>  *object_name* can contain brackets **[]** as delimited identifier characters. For more information, see [Database Identifiers](../../relational-databases/databases/database-identifiers.md).  
  
 [ **@futureonly=** ] **'***futureonly_flag***'**  
 Is used only when unbinding a rule from an alias data type. *futureonly_flag* is **varchar(15)**, with a default of NULL. When *futureonly_flag* is **futureonly**, existing columns of that data type do not lose the specified rule.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 To display the text of a rule, execute **sp_helptext** with the rule name as the parameter.  
  
 When a rule is unbound, the information about the binding is removed from the **sys.columns** table if the rule was bound to a column, and from the **sys.types** table if the rule was bound to an alias data type.  
  
 When a rule is unbound from an alias data type, it is also unbound from any columns having that alias data type. The rule may also still be bound to columns whose data types were later changed by the ALTER COLUMN clause of an ALTER TABLE statement, you must specifically unbind the rule from these columns by using **sp_unbindrule** and specifying the column name.  
  
## Permissions  
 To unbind a rule from a table column requires ALTER permission on the table. To unbind a rule from an alias data type requires CONTROL permission on the type or ALTER permission on the schema to which the type belongs.  
  
## Examples  
  
### A. Unbinding a rule from a column  
 The following example unbinds the rule from the `startdate` column of an `employees` table.  
  
```  
EXEC sp_unbindrule 'employees.startdate';  
```  
  
### B. Unbinding a rule from an alias data type  
 The following example unbinds the rule from the alias data type `ssn`. It unbinds the rule from existing and future columns of that type.  
  
```  
EXEC sp_unbindrule ssn;  
```  
  
### C. Using futureonly_flag  
 The following example unbinds the rule from the alias data type `ssn` without affecting existing `ssn` columns.  
  
```  
EXEC sp_unbindrule 'ssn', 'futureonly';  
```  
  
### D. Using delimited identifiers  
 The following example shows using delimited identifiers in the *object_name* parameter.  
  
```  
CREATE TABLE [t.4] (c1 int); -- Notice the period as part of the table   
-- name.  
GO  
CREATE RULE rule2 AS @value > 100;  
GO  
EXEC sp_bindrule rule2, '[t.4].c1' -- The object contains two   
-- periods; the first is part of the table name and the second   
-- distinguishes the table name from the column name.  
GO  
EXEC sp_unbindrule '[t.4].c1';  
```  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)   
 [CREATE RULE &#40;Transact-SQL&#41;](../../t-sql/statements/create-rule-transact-sql.md)   
 [DROP RULE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-rule-transact-sql.md)   
 [sp_bindrule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-bindrule-transact-sql.md)   
 [sp_helptext &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helptext-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
