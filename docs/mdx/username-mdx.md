---
title: "UserName (MDX)"
description: "UserName (MDX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
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
  
## Related content

- [MDX Function Reference (MDX)](mdx-function-reference-mdx.md)
