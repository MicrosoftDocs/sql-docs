---
title: "Children (MDX) | Microsoft Docs"
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
  - "CHILDREN"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "Children function"
ms.assetid: ce2c3069-914c-44a3-8a4c-5cbd4fb71e4c
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Children (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the set of children of a specified member.  
  
## Syntax  
  
```  
  
Member_Expression.Children  
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
## Remarks  
 The **Children** function returns a naturally ordered set that contains the children of a specified member. If the specified member has no children, this function returns an empty set.  
  
## Example  
 The following example returns the children of the United States member of the Geography hierarchy in the Geography dimension.  
  
```  
SELECT [Geography].[Geography].[Country].&[United States].Children ON 0  
FROM [Adventure Works]  
```  
  
 The following example returns all members in the **Measures** dimension on the column axis, this includes all calculated members, and the set of all children of the `[Product].[Model Name]` attribute hierarchy on the row axis from the **Adventure Works** cube.  
  
```  
SELECT  
    Measures.AllMembers ON COLUMNS,  
    [Product].[Model Name].Children ON ROWS  
FROM  
    [Adventure Works]  
  
```  
  
|Release|History|  
|-------------|-------------|  
|[!INCLUDE[ssBOL2005_R03](../includes/ssbol2005-r03-md.md)]|**Changed content:**<br /> — Updated syntax and arguments to improve clarity.<br /><br /> — Added updated examples.|  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
