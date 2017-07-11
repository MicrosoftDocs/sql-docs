---
title: "Level (MDX) | Microsoft Docs"
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
  - "LEVEL"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "Level function"
ms.assetid: 10bbe4ac-44bc-45c7-81a1-85423fbeaab1
caps.latest.revision: 31
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Level (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the level of a member.  
  
## Syntax  
  
```  
  
Member_Expression.Level  
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expression (MDX) that returns a member.  
  
### Examples  
 The following example uses the **Level** function to return all months in the Adventure Works cube.  
  
```  
SELECT[Date].[Fiscal].[Month].[February 2002].Level.Members ON 0,  
[Measures].[Internet Sales Amount] ON 1  
FROM [Adventure Works]  
```  
  
 The following example uses the **Level** function to return the name of the level for the All-Purpose Bike Stand in the Model Name attribute hierarchy in the Adventure Works cube.  
  
```  
WITH MEMBER Measures.x AS   
   [Product].[Model Name].[All-Purpose Bike Stand].Level.Name  
SELECT Measures.x ON 0  
FROM [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
