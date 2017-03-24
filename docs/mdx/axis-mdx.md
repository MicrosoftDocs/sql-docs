---
title: "Axis (MDX) | Microsoft Docs"
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
  - "AXIS"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "Axis function"
ms.assetid: a3a60a1e-e266-4fa1-ae13-bae73544de33
caps.latest.revision: 31
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Axis (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the set of tuples on a specified axis.  
  
## Syntax  
  
```  
  
Axis(Axis_Number)  
```  
  
## Arguments  
 *Axis_Number*  
 A valid numeric expression that specifies the axis number.  
  
## Remarks  
 The **Axis** function uses the zero-based position of an axis to return the set of tuples on an axis. For example, `Axis(0)` returns the COLUMNS axis, `Axis(1)` returns the ROWS axis, and so on. The **Axis** function cannot be used on the filter axis. This function can be used to make calculated members aware of the context of the query that is being run. For example, you might need a calculated member that provides the sum of only those members selected on the Rows axis. It can also be used to make the definition of one axis dependent on the definition of another. For example, by ordering the contents of the Rows axis according to the value of the first item on the Columns axis.  
  
> [!NOTE]  
>  An axis can reference only a prior axis. For example, `Axis(0)` must occur after the COLUMNS axis has been evaluated, such as on a ROW or PAGE axis.  
  
## Examples  
 The following example query shows how to use the Axis function:  
  
 `WITH MEMBER MEASURES.AXISDEMO AS`  
  
 `SETTOSTR(AXIS(1))`  
  
 `SELECT MEASURES.AXISDEMO ON 0,`  
  
 `[Date].[Calendar Year].MEMBERS ON 1`  
  
 `FROM [Adventure Works]`  
  
 The following example shows the use of the Axis function inside a calculated member:  
  
 `WITH MEMBER MEASURES.AXISDEMO AS`  
  
 `SUM(AXIS(1), [Measures].[Internet Sales Amount])`  
  
 `SELECT {[Measures].[Internet Sales Amount],MEASURES.AXISDEMO} ON 0,`  
  
 `{[Date].[Calendar Year].&[2003], [Date].[Calendar Year].&[2004]} ON 1`  
  
 `FROM [Adventure Works]`  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
