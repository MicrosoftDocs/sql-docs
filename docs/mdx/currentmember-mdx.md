---
description: "CurrentMember (MDX)"
title: "CurrentMember (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# CurrentMember (MDX)


  Returns the current member along a specified hierarchy during iteration.  
  
## Syntax  
  
```  
  
Hierarchy_Expression.CurrentMember  
```  
  
## Arguments  
 *Hierarchy_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a hierarchy.  
  
## Remarks  
 When iterating through a set of hierarchy members, at each step in the iteration, the member being operated upon is the current member. The **CurrentMember** function returns that member.  
  
> [!IMPORTANT]  
>  When a dimension contains only a single visible hierarchy, the hierarchy can be referred to either by the dimension name or by the hierarchy name, because the dimension name is resolved to its only visible hierarchy. For example, `Measures.CurrentMember` is a valid MDX expression because it resolves to the only hierarchy in the Measures dimension.  
  
## Examples  
 The following query shows how **Currentmember** can be used to find the current member from hierarchies on the Columns, Rows and slice axis:  

```
WITH
  MEMBER MEASURES.CURRENTDATE AS [Date].[Calendar].CURRENTMEMBER.NAME
  MEMBER MEASURES.CURRENTPRODUCT AS [Product].[Product Categories].CURRENTMEMBER.NAME
  MEMBER MEASURES.CURRENTMEASURE AS MEASURES.CURRENTMEMBER.NAME
  MEMBER MEASURES.CURRENTCUSTOMER AS [Customer].[Customer Geography].CURRENTMEMBER.NAME
SELECT
 [Product].[Product Categories].[Category].MEMBERS *
 {MEASURES.CURRENTDATE,
    MEASURES.CURRENTPRODUCT,
    MEASURES.CURRENTMEASURE,
    MEASURES.CURRENTCUSTOMER} ON 0, 
 [Date].[Calendar].MEMBERS ON 1  
FROM [Adventure Works]
WHERE ([Customer].[Customer Geography].[Country].&[Australia])
```
  
 The current member changes on a hierarchy used on an axis in a query. Therefore, the current member on other hierarchies on the same dimension that are not used on an axis can also change; this behavior is called 'auto-exists' and more details can be found in [Key Concepts in MDX &#40;Analysis Services&#41;](/analysis-services/multidimensional-models/mdx/key-concepts-in-mdx-analysis-services). For example, the query below shows how the current member on the Calendar Year hierarchy of the Date dimension changes with the current member on the Calendar hierarchy, when the latter is displayed on the Rows axis:  

```
WITH
  MEMBER MEASURES.CURRENTYEAR AS [Date].[Calendar Year].CURRENTMEMBER.NAME
SELECT
 {MEASURES.CURRENTYEAR} ON 0,
 [Date].[Calendar].MEMBERS ON 1  
FROM [Adventure Works]
```

 **CurrentMember** is very important for making calculations aware of the context of the query they are being used in. The following example returns the order quantity of each product and the percentage of order quantities by category and model, from the **Adventure Works** cube. The **CurrentMember** function identifies the product whose order quantity is to be used during calculation.  
  
```  
WITH   
   MEMBER [Measures].[Order Percent by Category] AS  
   CoalesceEmpty  
(   
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
  
