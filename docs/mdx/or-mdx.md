---
description: "OR (MDX)"
title: "OR (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# OR (MDX)


  Performs a logical disjunction on two numeric expressions.  
  
## Syntax  
  
```  
  
Expression1 OR Expression2   
```  
  
#### Parameters  
 Expression1  
 A valid Multidimensional Expressions (MDX) expression that returns a numeric value.  
  
 Expression2  
 A valid MDX expression that returns a numeric value.  
  
## Return Value  
 A Boolean value that returns **true** if either or both arguments evaluate to **true**; otherwise, **false**.  
  
## Remarks  
 The **OR** operator treats both arguments as Boolean values (zero, 0, as **false**; otherwise, **true**) before the operator performs the logical disjunction. The following table illustrates how the **OR** operator performs the logical disjunction.  
  
|*Expression1*|*Expression2*|Return Value|  
|-------------------|-------------------|------------------|  
|**true**|**true**|**true**|  
|**true**|**false**|**true**|  
|**false**|**true**|**true**|  
|**false**|**false**|**false**|  
  
## Example  
 The following query contains a calculated measure that returns the string "MARRIED OR MALE" if the current member on the Gender hierarchy of the Customer dimension is Male or the current member on the Marital Status hierarchy of the Customer dimension is Married; otherwise it returns the string "UNMARRIED OR FEMALE".  
  
```  
WITH  
MEMBER MEASURES.ORDEMO AS  
IIF(  
([Customer].[Gender].CURRENTMEMBER IS [Customer].[Gender].&[M])  
OR  
([Customer].[Marital Status].CURRENTMEMBER IS [Customer].[Marital Status].&[M]),  
"MARRIED OR MALE",  
"UNMARRIED OR FEMALE")  
SELECT [Customer].[Gender].[Gender].MEMBERS ON 0,  
[Customer].[Marital Status].[Marital Status].MEMBERS ON 1  
FROM [Adventure Works]  
WHERE(MEASURES.ORDEMO)  
```  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
