---
title: "NameToSet (MDX) | Microsoft Docs"
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
  - "NAMETOSET"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "NameToSet function"
ms.assetid: e02e17d5-4309-49cb-84c7-5b445ac2bd94
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# NameToSet (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a set that contains the member specified by a Multidimensional Expressions (MDX)–formatted string.  
  
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
  
  
