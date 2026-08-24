---
title: "DROP MEMBER Statement (MDX)"
description: "MDX Data Definition - DROP MEMBER"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
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
  
## Related content

- [MDX Data Definition - CREATE MEMBER](mdx-data-definition-create-member.md)
- [MDX Data Definition Statements (MDX)](mdx-data-definition-statements-mdx.md)
