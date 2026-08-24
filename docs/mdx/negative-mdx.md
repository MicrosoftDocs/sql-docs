---
title: "- (Negative) (MDX)"
description: "- (Negative) (MDX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
---
# - (Negative) (MDX)


  Performs a unary operation that returns the negative value of a numeric expression.  
  
## Syntax  
  
```  
  
- Numeric_Expression  
```  
  
#### Parameters  
 *Numeric_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a numeric value.  
  
## Return Value  
 A negative value that has the data type of the specified parameter.  
  
## Examples  
 The following example demonstrates the use of this operator.  
  
```  
-- This member creates a negative version of the  
-- Reseller Freight Cost.  
WITH MEMBER   
   Measures.[Resell Cost as Negative]   
   AS -Measures.[Reseller Freight Cost]  
SELECT   
   [Date].[Calendar Month of Year].Children ON COLUMNS,  
   [Product].[Product Categories].Children ON ROWS  
FROM  
    [Adventure Works]  
WHERE  
    {[Measures].[Resell Cost as Negative]}  
```  
  
## Related content

- [MDX Operator Reference (MDX)](mdx-operator-reference-mdx.md)
