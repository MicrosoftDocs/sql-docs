---
title: "NameToSet (MDX) | Microsoft Docs"
ms.date: 06/04/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# NameToSet (MDX)


  Returns a set that contains the member specified by a Multidimensional Expressions (MDX)-formatted string.  
  
## Syntax  
  
```  
  
NameToSet(Member_Name)   
```  
  
## Arguments  
 *Member_Name*  
 A valid string expression that represents the name of a member.  
  
## Remarks  
 If the specified member name exists, the **NameToSet** function returns a set containing that member. Otherwise, the function returns an empty set.  
  
> [!NOTE]  
>  The specified member name must only be a member name; it cannot be a member expression. To use a member expression, see [StrToSet &#40;MDX&#41;](../mdx/strtoset-mdx.md).  
  
## Example  
 The following returns the default measure value for the specified member name.  
  
```  
SELECT NameToSet('[Date].[Calendar].[July 2001]') ON 0  
FROM [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
