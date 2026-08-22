---
title: "DROP SET Statement (MDX)"
description: "MDX Data Definition - DROP SET"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
---
# MDX Data Definition - DROP SET


  Removes a named set.  
  
## Syntax  
  
```  
  
DROP [SESSION] SET   
   <Cube_Reference>.Set_Name   
               [,<Cube_Reference>.Set_Name ...n]  
  
<Cube_Reference> ::= {CURRENTCUBE | Cube_Name}  
  
```  
  
## Arguments  
 *Cube_Name*  
 A valid string expression that provides the name of the cube.  
  
 *Set_Name*  
 A valid string expression that provides that name of the named set to be dropped.  
  
## Remarks  
 For more information about named sets, see [Building Named Sets in MDX &#40;MDX&#41;](/analysis-services/multidimensional-models/mdx/mdx-named-sets-building-named-sets).  
  
## Related content

- [MDX Data Definition Statements (MDX)](mdx-data-definition-statements-mdx.md)
