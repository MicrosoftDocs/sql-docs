---
title: "'General Page of 'Create New Condition' or 'Open Condition' dialog box"
description: Describes the 'General Page' of the 'Create New Condition' or 'Open Condition dialog box for Policy-Based Management in SQL Server Management Studio (SSMS).
author: VanMSFT
ms.author: vanto
ms.date: 12/15/2023
ms.service: sql
ms.subservice: security
ms.topic: ui-reference
f1_keywords:
  - "sql13.swb.dmf.condition.f1"
---
# Create New Condition or Open Condition dialog box, General page

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Use this dialog box to create or change a Policy-Based Management condition. A condition is a Boolean expression that specifies a set of allowed states of a Policy-Based Management managed target with regard to facets. The properties that can be selected in the **Expression/Field** box depend upon the facet that is used. For more information about how conditions relate to facets and policies, see [Administer Servers by Using Policy-Based Management](administer-servers-by-using-policy-based-management.md).

## Options

**Name**  
For a new condition, type the new condition name. For an existing condition, the name is displayed.

**Facet**  
The facet used by this condition.

**AndOr**  
When you add multiple expressions, indicates whether the expressions should be joined by using **And** or **Or**. Remains blank when there's only one expression.

**Field**  
Each facet exposes one or more properties that can be set. In the field box, select a property from the list of available properties to create an expression for this condition.

**Operator**  
Select a comparison operator for this expression. Operators are as follows: `=`, `!=`, `>`, `>=`, `<`, `<=`, [NOT]LIKE, [NOT]IN. Not all operators are available for some properties.

**Value**  
The value setting for this expression. The allowed values depend on the facet. Values can be TRUE/FALSE, string, or numeric. String values must be enclosed in single quotation marks, for example: **'AdventureWorks'**. Not all operators are available for some properties.

## Group clauses

Clauses can be grouped to operate as a single unit separate from the rest of the query, just like putting parentheses around an expression in a mathematical equation or logic statement. Grouping clauses is useful when you're building complex queries.

**To group clauses**

- Press the SHIFT or CTRL keys, and then select two or more clauses to select a range. Right-click the selected area, and then select **Group Clauses**.

## Related content

- [Administer Servers by Using Policy-Based Management](administer-servers-by-using-policy-based-management.md)
