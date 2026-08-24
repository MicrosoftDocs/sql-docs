---
title: "Error (MDX)"
description: "Error (MDX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
---
# Error (MDX)


  Raises an error, optionally providing a specified error message.  
  
## Syntax  
  
```  
  
Error( [ Error_Text ] )  
```  
  
## Arguments  
 *Error_Text*  
 A valid string expression containing the error message to be returned.  
  
## Examples  
 The following query shows how to use the **Error** function inside a calculated measure:  
  
```  
WITH MEMBER MEASURES.ERRORDEMO AS ERROR("THIS IS AN ERROR")  
SELECT  
MEASURES.ERRORDEMO ON 0  
FROM [Adventure Works]  
```  
  
## Related content

- [MDX Function Reference (MDX)](mdx-function-reference-mdx.md)
