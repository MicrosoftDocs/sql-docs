---
title: "CREATE DEFAULT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "11/25/2015"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CREATE_DEFAULT_TSQL"
  - "CREATE DEFAULT"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "objects [SQL Server], default"
  - "default objects"
  - "CREATE DEFAULT statement"
  - "objects [SQL Server], creating"
  - "DEFAULT definition"
ms.assetid: 08475db4-7d90-486a-814c-01a99d783d41
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# CREATE DEFAULT (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Creates an object called a default. When bound to a column or an alias data type, a default specifies a value to be inserted into the column to which the object is bound (or into all columns, if of an alias data type), when no value is explicitly supplied during an insert.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Instead, use default definitions created using the DEFAULT keyword of ALTER TABLE or CREATE TABLE.  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
CREATE DEFAULT [ schema_name . ] default_name   
AS constant_expression [ ; ]  
```  
  
## Arguments  
*schema_name*  
 The name of the schema to which the default belongs.  
  
*default_name*  
 The name of the default. Default names must conform to the rules for [identifiers](../../relational-databases/databases/database-identifiers.md). Specifying the default owner name is optional.  
  
*constant_expression*  
An [expression](../../t-sql/language-elements/expressions-transact-sql.md) that contains only constant values (it can't include the names of any columns or other database objects). You can use any constant, built-in function, or mathematical expression, except ones that contain alias data types. User-defined functions can't be used. Enclose character and date constants in single quotation marks (**'**); monetary, integer, and floating-point constants don't require quotation marks. Binary data must be preceded by 0x, and monetary data must be preceded by a dollar sign ($). The default value must be compatible with the column data type.  
  
## Remarks  
 You can only create a default name in the current database. Within a database, default names must be unique by schema. When you create a default, use **sp_bindefault** to bind it to a column or to an alias data type.  
  
 If the default isn't compatible with the column to which it's bound, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generates an error message when trying to insert the default value. For example, N/A can't be used as a default for a **numeric** column.  
  
 If the default value is too long for the column to which it's bound, the value is truncated.  
  
 CREATE DEFAULT statements can't be combined with other [!INCLUDE[tsql](../../includes/tsql-md.md)] statements in a single batch.  
  
 A default must be dropped before creating a new one of the same name. And, the default must be unbound by executing **sp_unbindefault** before it's dropped.  
  
 If a column has both a default and a rule associated with it, the default value must not violate the rule. A default that conflicts with a rule is never inserted, and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generates an error message each time it attempts to insert the default.  
  
 When bound to a column, a default value is inserted when:  
  
-   A value isn't explicitly inserted.  
  
-   Either the DEFAULT VALUES or DEFAULT keywords are used with INSERT to insert default values.  
  
 If you specify NOT NULL when creating a column and you don't create a default for it, an error message generates when a user fails to make an entry in that column. The following table illustrates the relationship between the existence of a default and the definition of a column as NULL or NOT NULL. The entries in the table show the result.  
  
|Column definition|No entry, no default|No entry, default|Enter NULL, no default|Enter NULL, default|  
|-----------------------|--------------------------|-----------------------|----------------------------|-------------------------|  
|**NULL**|NULL|default|NULL|NULL|  
|**NOT NULL**|Error|default|error|error|  
  
 To rename a default, use **sp_rename**. For a report on a default, use **sp_help**.  
  
## Permissions  
 To use CREATE DEFAULT, at a minimum, a user must have CREATE DEFAULT permission in the current database and ALTER permission on the schema in which the default is being created.  
  
## Examples  
  
### A. Creating a simple character default  
 The following example creates a character default called `unknown`.  
  
```sql  
USE AdventureWorks2012;  
GO  
CREATE DEFAULT phonedflt AS 'unknown';  
```  
  
### B. Binding a default  
 The following example binds the default created in example A. The default takes effect only if no entry is specified for the `Phone` column of the `Contact` table. 
 
 > [!Note] 
 >  Omitting any entry is different from explicitly stating NULL in an INSERT statement.  
  
 Because a default named `phonedflt` doesn't exist, the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement fails. This example is for illustration only.  
  
```sql  
USE AdventureWorks2012;  
GO  
sp_bindefault 'phonedflt', 'Person.PersonPhone.PhoneNumber';  
```  
  
## See Also  
 [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)   
 [CREATE RULE &#40;Transact-SQL&#41;](../../t-sql/statements/create-rule-transact-sql.md)   
 [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)   
 [DROP DEFAULT &#40;Transact-SQL&#41;](../../t-sql/statements/drop-default-transact-sql.md)   
 [DROP RULE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-rule-transact-sql.md)   
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/insert-transact-sql.md)   
 [sp_bindefault &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-bindefault-transact-sql.md)   
 [sp_help &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-transact-sql.md)   
 [sp_helptext &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helptext-transact-sql.md)   
 [sp_rename &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-rename-transact-sql.md)   
 [sp_unbindefault &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-unbindefault-transact-sql.md)  
  
  
