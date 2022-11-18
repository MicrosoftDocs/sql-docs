---
description: "MDX Data Definition - DROP MEMBER"
title: "DROP MEMBER Statement (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# MDX Data Definition - DROP MEMBER


  Removes a calculated member.  
  
## Syntax  
  
```  
  
DROP MEMBER   
   CURRENTCUBE | Cube_Name  
      .Member_Name   
               [,CURRENTCUBE | Cube_Name.Member_Name ...n]  
```  
  
## Arguments  
 *Cube_Name*  
 A valid string expression that provides a cube name.  
  
 *Member_Identifier*  
 A valid string expression that provides a member name or member key.  
  
## See Also  
 [CREATE MEMBER Statement &#40;MDX&#41;](../mdx/mdx-data-definition-create-member.md)   
 [MDX Data Definition Statements &#40;MDX&#41;](../mdx/mdx-data-definition-statements-mdx.md)  
  
  
