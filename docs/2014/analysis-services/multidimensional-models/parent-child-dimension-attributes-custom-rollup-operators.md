---
title: "Custom Rollup Operators in Parent-Child Dimensions | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "child rollup operations"
  - "UnaryOperatorColumn property"
  - "custom rollup operators [Analysis Services]"
  - "unary operators"
  - "parent-child dimensions [Analysis Services]"
ms.assetid: a3ddd9fc-5fa3-4227-9322-8c45a5b5c2c3
author: minewiskan
ms.author: owend
manager: craigg
---
# Custom Rollup Operators in Parent-Child Dimensions
  Custom rollup operators provide a simple way to control how member values are rolled up into parent values in a parent-child hierarchy. In a dimension containing a parent-child relationship, you specify a column that contains unary operators that specify rollup for all noncalculated members of the parent attribute. The unary operator is applied to members whenever the values of the parent members are evaluated.  
  
 The unary operators are stored in column defined by the `UnaryOperatorColumn` property of the parent attribute, and they are applied to each member of the attribute. The column specified by this property can reside either in the dimension table or in a table related to the dimension table by a foreign key in the dimension table.  
  
 Custom rollup operators provide functionality similar to, but more simplified than, custom member formulas. A custom member formula uses Multidimensional Expressions (MDX) expressions to determine how the members are rolled up. In contrast, a custom rollup operator uses a simple unary operator to determine how the value of a member affects the parent. Custom member formulas of the preceding level in a dimension override a level's custom rollup operator.  
  
## Custom Rollup Precedence  
 In terms of precedence, the custom rollup operators of the source attribute for a level in a hierarchy override the custom member formulas of the previous level. However, the custom member formulas of the preceding level override the custom rollup operators of a level.  
  
## See Also  
 [Define Custom Member Formulas](attribute-properties-define-custom-member-formulas.md)   
 [Unary Operators in Parent-Child Dimensions](parent-child-dimension-attributes-unary-operators.md)  
  
  
