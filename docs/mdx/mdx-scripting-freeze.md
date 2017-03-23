---
title: "FREEZE Statement (MDX) | Microsoft Docs"
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
  - "FREEZE"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "FREEZE statement"
  - "locking cell values [MDX]"
ms.assetid: 59f1e860-6f37-41af-97d6-7708bdaac933
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# MDX Scripting - FREEZE
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Locks the cell values of a specified subcube to their current values. When the cell values are locked, changes to other cells have no effect on the cells that are locked.  
  
## Syntax  
  
```  
  
FREEZE Subcube_Expression   
```  
  
## Arguments  
 *Subcube_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a subcube.  
  
## Remarks  
 The **FREEZE** statement locks the values of cells in a specified subcube, preventing subsequent statements in an MDX script from changing their values in subsequent calculation passes.  
  
 In the following example, A and B represent subcubes in an MDX calculation script:  
  
```  
B = 2;  
A = B;  
B = 3  
```  
  
 At this point, both A and B are equal to 3.  
  
 We now insert the **Freeze** function to lock the cells in the A subcube:  
  
```  
B = 2;  
A = B;  
FREEZE(A);  
B = 3  
```  
  
 A is now equal to 2, and B is equal to 3.  
  
## See Also  
 [MDX Scripting Statements &#40;MDX&#41;](../mdx/mdx-scripting-statements-mdx.md)  
  
  
