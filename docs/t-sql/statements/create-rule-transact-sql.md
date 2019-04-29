---
title: "CREATE RULE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "RULE_TSQL"
  - "CREATE RULE"
  - "CREATE_RULE_TSQL"
  - "RULE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "list rules [SQL Server]"
  - "unbinding rules"
  - "pattern rules [SQL Server]"
  - "range rules [SQL Server]"
  - "alias data types [SQL Server], rules"
  - "CREATE RULE statement"
  - "reports [SQL Server], rules"
  - "status information [SQL Server], rules"
  - "rules [SQL Server], precedence"
  - "binding rules [SQL Server]"
  - "rules [SQL Server], creating"
ms.assetid: b016a289-3a74-46b1-befc-a13183be51e4
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# CREATE RULE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Creates an object called a rule. When bound to a column or an alias data type, a rule specifies the acceptable values that can be inserted into that column.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] We recommend that you use check constraints instead. Check constraints are created by using the CHECK keyword of CREATE TABLE or ALTER TABLE. For more information, see [Unique Constraints and Check Constraints](../../relational-databases/tables/unique-constraints-and-check-constraints.md).  
  
 A column or alias data type can have only one rule bound to it. However, a column can have both a rule and one or more check constraints associated with it. When this is true, all restrictions are evaluated.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
CREATE RULE [ schema_name . ] rule_name   
AS condition_expression  
[ ; ]  
```  
  
## Arguments  
 *schema_name*  
 Is the name of the schema to which the rule belongs.  
  
 *rule_name*  
 Is the name of the new rule. Rule names must comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md). Specifying the rule owner name is optional.  
  
 *condition_expression*  
 Is the condition or conditions that define the rule. A rule can be any expression valid in a WHERE clause and can include elements such as arithmetic operators, relational operators, and predicates (for example, IN, LIKE, BETWEEN). A rule cannot reference columns or other database objects. Built-in functions that do not reference database objects can be included. User-defined functions cannot be used.  
  
 *condition_expression* includes one variable. The at sign (**@**) precedes each local variable. The expression refers to the value entered with the UPDATE or INSERT statement. Any name or symbol can be used to represent the value when creating the rule, but the first character must be the at sign (**@**).  
  
> [!NOTE]  
>  Avoid creating rules on expressions that use alias data types. Although rules can be created on expressions that use alias data types, after binding the rules to columns or alias data types, the expressions fail to compile when referenced.  
  
## Remarks  
 CREATE RULE cannot be combined with other [!INCLUDE[tsql](../../includes/tsql-md.md)] statements in a single batch. Rules do not apply to data already existing in the database at the time the rules are created, and rules cannot be bound to system data types.  
  
 A rule can be created only in the current database. After you create a rule, execute **sp_bindrule** to bind the rule to a column or to alias data type. A rule must be compatible with the column data type. For example, "\@value LIKE A%" cannot be used as a rule for a numeric column. A rule cannot be bound to a **text**, **ntext**, **image**, **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **xml**, CLR user-defined type, or **timestamp**column. A rule cannot be bound to a computed column.  
  
 Enclose character and date constants with single quotation marks (') and precede binary constants with 0x. If the rule is not compatible with the column to which it is bound, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] returns an error message when a value is inserted, but not when the rule is bound.  
  
 A rule bound to an alias data type is activated only when you try to insert a value into, or to update, a database column of the alias data type. Because rules do not test variables, do not assign a value to an alias data type variable that would be rejected by a rule that is bound to a column of the same data type.  
  
 To get a report on a rule, use **sp_help**. To display the text of a rule, execute **sp_helptext** with the rule name as the parameter. To rename a rule, use **sp_rename**.  
  
 A rule must be dropped by using DROP RULE before a new one with the same name is created, and the rule must be unbound by using **sp_unbindrule** before it is dropped. To unbind a rule from a column, use **sp_unbindrule**.  
  
 You can bind a new rule to a column or data type without unbinding the previous one; the new rule overrides the previous one. Rules bound to columns always take precedence over rules bound to alias data types. Binding a rule to a column replaces a rule already bound to the alias data type of that column. But binding a rule to a data type does not replace a rule bound to a column of that alias data type. The following table shows the precedence in effect when rules are bound to columns and to alias data types on which rules already exist.  
  
|New rule bound to|Old rule bound to<br /><br /> alias data type|Old rule bound to<br /><br /> Column|  
|-----------------------|-------------------------------------------|----------------------------------|  
|Alias data type|Old rule replaced|No change|  
|Column|Old rule replaced|Old rule replaced|  
  
 If a column has both a default and a rule associated with it, the default must fall within the domain defined by the rule. A default that conflicts with a rule is never inserted. The SQL Server Database Engine generates an error message each time it tries to insert such a default.  
  
## Permissions  
 To execute CREATE RULE, at a minimum, a user must have CREATE RULE permission in the current database and ALTER permission on the schema in which the rule is being created.  
  
## Examples  
  
### A. Creating a rule with a range  
 The following example creates a rule that restricts the range of integers inserted into the column or columns to which this rule is bound.  
  
```  
CREATE RULE range_rule  
AS   
@range>= $1000 AND @range <$20000;  
```  
  
### B. Creating a rule with a list  
 The following example creates a rule that restricts the actual values entered into the column or columns (to which this rule is bound) to only those listed in the rule.  
  
```  
CREATE RULE list_rule  
AS   
@list IN ('1389', '0736', '0877');  
```  
  
### C. Creating a rule with a pattern  
 The following example creates a rule to follow a pattern of any two characters followed by a hyphen (`-`), any number of characters or no characters, and ending with an integer from `0` through `9`.  
  
```  
CREATE RULE pattern_rule   
AS  
@value LIKE '__-%[0-9]'  
```  
  
## See Also  
 [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)   
 [CREATE DEFAULT &#40;Transact-SQL&#41;](../../t-sql/statements/create-default-transact-sql.md)   
 [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)   
 [DROP DEFAULT &#40;Transact-SQL&#41;](../../t-sql/statements/drop-default-transact-sql.md)   
 [DROP RULE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-rule-transact-sql.md)   
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [sp_bindrule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-bindrule-transact-sql.md)   
 [sp_help &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-transact-sql.md)   
 [sp_helptext &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helptext-transact-sql.md)   
 [sp_rename &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-rename-transact-sql.md)   
 [sp_unbindrule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-unbindrule-transact-sql.md)   
 [WHERE &#40;Transact-SQL&#41;](../../t-sql/queries/where-transact-sql.md)  
  
  
