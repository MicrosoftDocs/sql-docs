---
description: "CoalesceEmpty (MDX)"
title: "CoalesceEmpty (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# CoalesceEmpty (MDX)


  Converts an empty cell value to a specified nonempty cell value, which can be either a number or string.  
  
## Syntax  
  
```  
  
Numeric syntax  
CoalesceEmpty( Numeric_Expression1 [ ,Numeric_Expression2,...n] )  
  
String syntax  
CoalesceEmpty(String_Expression1 [ ,String_Expression2,...n] )  
```  
  
## Arguments  
 *Numeric_Expression1*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number.  
  
 *Numeric_Expression2*  
 A valid numeric expression that is typically a specified numeric value.  
  
 *String_Expression1*  
 A valid string expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that returns a string.  
  
 *String_Expression2*  
 A valid string expression that is typically a specified string value that is substituted for a NULL returned by the first string expression.  
  
## Remarks  
 If one or more numeric expressions are specified, the **CoalesceEmpty** function returns the numeric value of the first numeric expression (from left to right) that can be resolved to a nonempty value. If none of the specified numeric expressions can be resolved to a nonempty value, the function returns the empty cell value. Typically, the value for the second numeric expression is the numeric value that is substituted for a NULL returned by the first numeric expression.  
  
 If one or more string expressions are specified, the function returns the string value of the first string expression (from left to right) that can be resolved to a nonempty value. If none of the specified string expressions can be resolved to a nonempty value, the function returns the empty cell value. Typically, the value for the second string expression value is the string value that is substituted for a NULL returned by the first string expression.  
  
 The **CoalesceEmpty** function can only take values of the same type. In other words, all specified value expressions must evaluate to only numeric data types or an empty cell value, or all specified value expressions must evaluate to string data types or to an empty cell value. A single call to this function cannot include both numeric and string expressions.  
  
 For more information about empty cells, see the OLE DB documentation.  
  
## Example  
 The following example queries the **Adventure Works** cube. This example returns the order quantity of each product and the percentage of order quantities by category. The **CoalesceEmpty** function ensures that null values are represented as zero (0) when formatting the calculated members.  
  
```  
WITH   
   MEMBER [Measures].[Order Percent by Category] AS  
   CoalesceEmpty(   
      ([Product].[Product Categories].CurrentMember,  
        Measures.[Order Quantity]) /   
          (  
           Ancestor  
           ( [Product].[Product Categories].CurrentMember,   
             [Product].[Product Categories].[Category]  
           ), Measures.[Order Quantity]  
       ), 0  
   ), FORMAT_STRING='Percent'  
SELECT   
   {Measures.[Order Quantity],  
      [Measures].[Order Percent by Category]} ON COLUMNS,  
{[Product].[Product].Members} ON ROWS  
FROM [Adventure Works]  
WHERE {[Date].[Calendar Year].[Calendar Year].&[2003]}  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
