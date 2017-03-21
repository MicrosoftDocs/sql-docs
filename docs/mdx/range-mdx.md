---
title: ": (Range) (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - ":"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - ": (range operator)"
  - "range operator (:)"
ms.assetid: f9b36aca-4efd-49b4-9e4f-12914c1b24a6
caps.latest.revision: 37
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# : (Range) (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Performs a set operation that returns a naturally ordered set, with the two specified members as endpoints, and all members between the two specified members included as members of the set.  
  
## Syntax  
  
```  
  
Member_Expression : Member_Expression      
```  
  
#### Parameters  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
## Return Value  
 A set that contains the specified members and all members between the specified members.  
  
## Remarks  
 Both parameters must specify members within the same level and hierarchy of a given dimension. If both parameters specify the same member, the **: (Range)** operator returns a set that contains just the specified member. If the first parameter is Null, then the set contains all members from the beginning of the level of the member specified in the second parameter, up to and including that member. If the second parameter is Null, then the set contains all members from the member specified in the first parameter, up to and including the last member on the same level.  
  
 This set operator has no functional equivalent in MDX.  
  
## Examples  
 The following example demonstrates the use of this operator.  
  
```  
-- This query returns the freight cost per user  
-- for products, averaged by month, for the first quarter.  
With Member [Measures].[Freight Per Customer] as  
 (  
     [Measures].[Internet Freight Cost]  
     /   
     [Measures].[Customer Count]  
)  
  
SELECT   
    {[Ship Date].[Calendar].[Month].&[2004]&[1] : [Ship Date].[Calendar].[Month].&[2004]&[3]} ON 0,  
    [Product].[Category].[Category].Members ON 1  
FROM  
    [Adventure Works]  
WHERE  
    ([Measures].[Freight Per Customer])  
```  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
