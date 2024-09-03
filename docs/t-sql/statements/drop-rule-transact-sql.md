---
title: "DROP RULE (Transact-SQL)"
description: DROP RULE removes one or more user-defined rules from the current database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 05/16/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP_RULE_TSQL"
  - "DROP RULE"
helpviewer_keywords:
  - "rules [SQL Server], removing"
  - "deleting roles"
  - "DROP RULE statement"
  - "removing roles"
  - "dropping roles"
dev_langs:
  - "TSQL"
---
# DROP RULE (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sql-asdbmi.md)]

Removes one or more user-defined rules from the current database.

> [!IMPORTANT]  
> `DROP RULE` will be removed in a future version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Don't use `DROP RULE` in new development work, and plan to modify applications that currently use them. Instead, use `CHECK` constraints that you can create by using the `CHECK` keyword of [CREATE TABLE](create-table-transact-sql.md) or [ALTER TABLE](alter-table-transact-sql.md). For more information, see [Unique Constraints and Check Constraints](../../relational-databases/tables/unique-constraints-and-check-constraints.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DROP RULE [ IF EXISTS ] { [ schema_name . ] rule_name } [ , ...n ]
[ ; ]
```

## Arguments

#### *IF EXISTS*

**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions

Conditionally drops the rule only if it already exists.

#### *schema_name*

The name of the schema to which the rule belongs.

#### *rule*

The rule to be removed. Rule names must comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md). Specifying the rule schema name is optional.

## Remarks

To drop a rule, first unbind it if the rule is currently bound to a column or to an alias data type. To unbind the rule, use `sp_unbindrule`. If the rule is bound when you try to drop it, an error message is displayed and the `DROP RULE` statement is canceled.

After a rule is dropped, new data entered into the columns previously governed by the rule is entered without the constraints of the rule. Existing data isn't affected in any way.

The `DROP RULE` statement doesn't apply to `CHECK` constraints. For more information about dropping `CHECK` constraints, see [ALTER TABLE](alter-table-transact-sql.md).

## Permissions

To execute `DROP RULE`, at a minimum, a user must have `ALTER` permission on the schema to which the rule belongs.

## Examples

The following example unbinds and then drops the rule named `VendorID_rule`.

```sql
EXEC sp_unbindrule 'Production.ProductVendor.VendorID';
DROP RULE VendorID_rule;
```

## Related content

- [CREATE RULE (Transact-SQL)](create-rule-transact-sql.md)
- [sp_bindrule (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-bindrule-transact-sql.md)
- [sp_help (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-help-transact-sql.md)
- [sp_helptext (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-helptext-transact-sql.md)
- [sp_unbindrule (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-unbindrule-transact-sql.md)
- [USE (Transact-SQL)](../language-elements/use-transact-sql.md)
