---
title: "Level (MDX) | Microsoft Docs"
ms.date: 06/04/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Level (MDX)


  Returns the level of a member.  
  
## Syntax  
  
```  
  
Member_Expression.Level  
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expression (MDX) that returns a member.  
  
### Examples  
 The following example uses the **Level** function to return all months in the Adventure Works cube.  
  
```  
SELECT[Date].[Fiscal].[Month].[February 2002].Level.Members ON 0,  
[Measures].[Internet Sales Amount] ON 1  
FROM [Adventure Works]  
```  
  
 The following example uses the **Level** function to return the name of the level for the All-Purpose Bike Stand in the Model Name attribute hierarchy in the Adventure Works cube.  
  
```  
WITH MEMBER Measures.x AS   
   [Product].[Model Name].[All-Purpose Bike Stand].Level.Name  
SELECT Measures.x ON 0  
FROM [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
