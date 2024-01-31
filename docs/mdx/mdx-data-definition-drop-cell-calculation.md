---
title: "DROP CELL CALCULATION Statement (MDX)"
description: "MDX Data Definition - DROP CELL CALCULATION"
author: kfollis
ms.author: kfollis
ms.reviewer: kfollis
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
---
# MDX Data Definition - DROP CELL CALCULATION


  Removes the specified cell calculation.  
  
## Syntax  
  
```  
  
DROP [ SESSION ] CELL CALCULATION CURRENTCUBE | Cube_Name.CellCalc_Name  
```  
  
## Arguments  
 *Cube_Name*  
 A valid string expression that provides the name of a cube expression.  
  
 *CellCalc_Name*  
 A valid string expression that provides the name of the cell calculation to be dropped.  
  
## See Also  
 [CREATE CELL CALCULATION Statement &#40;MDX&#41;](../mdx/mdx-data-definition-create-cell-calculation.md)   
 [MDX Data Definition Statements &#40;MDX&#41;](../mdx/mdx-data-definition-statements-mdx.md)  
  
  
