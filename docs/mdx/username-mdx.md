---
description: "UserName (MDX)"
title: "UserName (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# UserName (MDX)


  Returns the domain name and user name of the current connection.  
  
## Syntax  
  
```  
  
UserName [ ( ) ]  
```  
  
## Remarks  
 The returned value is a string with the following format:  
  
 *domain-name\user-name*  
  
## Example  
 The following example returns the user name of the user that is executing the query.  
  
```  
WITH MEMBER Measures.x AS UserName  
SELECT Measures.x ON COLUMNS  
FROM [Adventure Works]  
  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
