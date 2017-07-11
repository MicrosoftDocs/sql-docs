---
title: "Ordinal (MDX) | Microsoft Docs"
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
  - "Ordinal"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "Ordinal function"
ms.assetid: 9d416c39-da4a-4f0d-8d85-a76af5df0a87
caps.latest.revision: 28
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Ordinal (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the zero-based ordinal value associated with a level.  
  
## Syntax  
  
```  
  
Level_Expression.Ordinal   
```  
  
## Arguments  
 *Level_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a level.  
  
## Remarks  
 The **Ordinal** function is frequently used in conjunction with the **IIF** and **CurrentMember** functions to conditionally display different values at different hierarchy levels, based on the ordinal position of each specific cell in the query result. For example, you can use the **Ordinal** function to perform calculations at certain levels and display a default value of "N/A" at other levels.  
  
## Example  
 The following example returns the ordinal number for the Calendar Quarter level in the Calendar hierarchy.  
  
```  
WITH MEMBER Measures.x AS [Date].[Calendar].[Calendar Quarter].Ordinal  
SELECT Measures.x on 0  
FROM [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
