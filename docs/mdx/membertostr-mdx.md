---
title: "MemberToStr (MDX) | Microsoft Docs"
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
  - "MEMBERTOSTR"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "MemberToStr function"
ms.assetid: 2076b24a-603a-4d74-91bd-a3d347739bcd
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# MemberToStr (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a Multidimensional Expressions (MDX)–formatted string that corresponds to a specified member.  
  
## Syntax  
  
```  
  
MemberToStr(Member_Expression)   
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
## Remarks  
 This function returns a string containing the uniquename of a member. It is usually used to pass a member’s uniquename to an external function.  
  
## Example  
 The following example returns the string [Geography].[Geography].[Country].&[United States] :  
  
 `WITH MEMBER Measures.x AS MemberToStr`  
  
 `([Geography].[Geography].[Country].[United States])`  
  
 `SELECT Measures.x ON 0`  
  
 `FROM [Adventure Works]`  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
