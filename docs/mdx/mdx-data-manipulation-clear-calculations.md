---
title: "CLEAR CALCULATIONS Statement (MDX)"
description: "MDX Data Manipulation - CLEAR CALCULATIONS"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
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
  
## Related content

- [MDX Data Manipulation Statements (MDX)](mdx-data-manipulation-statements-mdx.md)
