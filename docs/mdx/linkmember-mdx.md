---
description: "LinkMember (MDX)"
title: "LinkMember (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# LinkMember (MDX)


  Returns the member equivalent to a specified member in a specified hierarchy.  
  
## Syntax  
  
```  
  
LinkMember(Member_Expression, Hierarchy_Expression)   
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
 *Hierarchy_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a hierarchy.  
  
## Remarks  
 The **LinkMember** function returns the member from the specified hierarchy that matches the key values at each level of the specified member in a related hierarchy. Attributes at each level must have the same key cardinality and data type. In unnatural hierarchies, if there is more than one match for an attribute's key value, the result will be an error or indeterminate.  
  
## Examples  
 The following example uses the **LinkMember** function to return the default measure in the Adventure Works cube for the ascendants of the July 1, 2002 member of the Date.Date attribute hierarchy in the Calendar hierarchy.  
  
```  
SELECT  Hierarchize  
   (Ascendants   
      (LinkMember   
         ([Date].[Date].[July 1, 2002], [Date].[Calendar]  
         )  
       )  
    ) ON 0  
FROM [Adventure Works]  
```  
  
## See Also  
 [Hierarchize &#40;MDX&#41;](../mdx/hierarchize-mdx.md)   
 [Ascendants &#40;MDX&#41;](../mdx/ascendants-mdx.md)   
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
