---
title: "DROP RULE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/11/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DROP_RULE_TSQL"
  - "DROP RULE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "rules [SQL Server], removing"
  - "deleting roles"
  - "DROP RULE statement"
  - "removing roles"
  - "dropping roles"
ms.assetid: 8370b730-7fd5-43fe-a7f6-8300b3caa16d
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# DROP RULE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Removes one or more user-defined rules from the current database.  
  
> [!IMPORTANT]
>  DROP RULE will be removed in the next version of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Do not use DROP RULE in new development work, and plan to modify applications that currently use them. Instead, use CHECK constraints that you can create by using the CHECK keyword of [CREATE TABLE](../../t-sql/statements/create-table-transact-sql.md) or [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md). For more information, see [Unique Constraints and Check Constraints](../../relational-databases/tables/unique-constraints-and-check-constraints.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
DROP RULE [ IF EXISTS ] { [ schema_name . ] rule_name } [ ,...n ] [ ; ]  
```  
  
## Arguments  
 *IF EXISTS*  
 **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [current version](https://go.microsoft.com/fwlink/p/?LinkId=299658)).  
  
 Conditionally drops the rule only if it already exists.  
  
 *schema_name*  
 Is the name of the schema to which the rule belongs.  
  
 *rule*  
 Is the rule to be removed. Rule names must comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md). Specifying the rule schema name is optional.  
  
## Remarks  
 To drop a rule, first unbind it if the rule is currently bound to a column or to an alias data type. To unbind the rule, use **sp_unbindrule**. If the rule is bound when you try to drop it, an error message is displayed and the DROP RULE statement is canceled.  
  
 After a rule is dropped, new data entered into the columns previously governed by the rule is entered without the constraints of the rule. Existing data is not affected in any way.  
  
 The DROP RULE statement does not apply to CHECK constraints. For more information about dropping CHECK constraints, see [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md).  
  
## Permissions  
 To execute DROP RULE, at a minimum, a user must have ALTER permission on the schema to which the rule belongs.  
  
## Examples  
 The following example unbinds and then drops the rule named `VendorID_rule`. 
  
```  
sp_unbindrule 'Production.ProductVendor.VendorID'  
DROP RULE VendorID_rule  
GO  
```  
  
## See Also  
 [CREATE RULE &#40;Transact-SQL&#41;](../../t-sql/statements/create-rule-transact-sql.md)   
 [sp_bindrule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-bindrule-transact-sql.md)   
 [sp_help &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-transact-sql.md)   
 [sp_helptext &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helptext-transact-sql.md)   
 [sp_unbindrule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-unbindrule-transact-sql.md)   
 [USE &#40;Transact-SQL&#41;](../../t-sql/language-elements/use-transact-sql.md)  

