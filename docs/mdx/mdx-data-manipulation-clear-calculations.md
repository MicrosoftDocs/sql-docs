---
description: "MDX Data Manipulation - CLEAR CALCULATIONS"
title: "CLEAR CALCULATIONS Statement (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# MDX Data Manipulation - CLEAR CALCULATIONS


  Removes all calculations from the cube and returns the cube to calculation pass 0.  
  
## Syntax  
  
```  
  
CLEAR CALCULATIONS [FROMCube_Expression]  
```  
  
## Arguments  
 *Cube_Expression*  
 A valid Multidimensional Expressions (MDX) cube expression.  
  
## Remarks  
 The **FROM** clause can be omitted when the context of the cube is known, such as in an MDX script.  
  
> [!NOTE]  
>  This statement can only be executed by a server or database administrator, or a member of a role that has access to the source data in the cube (that is, ReadSourceData=true)  
  
## See Also  
 [MDX Data Manipulation Statements &#40;MDX&#41;](../mdx/mdx-data-manipulation-statements-mdx.md)  
  
  
