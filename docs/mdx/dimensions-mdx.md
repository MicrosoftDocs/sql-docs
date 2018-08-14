---
title: "Dimensions (MDX) | Microsoft Docs"
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
# Dimensions (MDX)


  Returns a hierarchy specified by a numeric or string expression.  
  
## Syntax  
  
```  
  
Numeric expression syntax  
Dimensions(Hierarchy_Number)  
  
String expression syntax  
Dimensions(Hierarchy_Name)  
```  
  
## Arguments  
 *Hierarchy_Number*  
 A valid numeric expression that specifies a hierarchy number.  
  
 *Hierarchy_Name*  
 A valid string expression that specifies a hierarchy name  
  
## Remarks  
 If a hierarchy number is specified, the **Dimensions** function returns a hierarchy whose zero-based position within the cube is specified hierarchy number.  
  
 If a hierarchy name is specified, the **Dimensions** function returns the specified hierarchy. Typically, you use this string version of the **Dimensions** function with user-defined functions.  
  
> [!NOTE]  
>  The **Measures** dimension is always represented by `Dimensions(0)`.  
  
## Examples  
 The following examples use the **Dimensions** function to return the name, count of levels, and count of members of a specified hierarchy, using both a numeric expression and a string expression.  
  
```  
WITH MEMBER Measures.x AS Dimensions  
   ('[Product].[Product Model Lines]').Name  
SELECT Measures.x on 0  
FROM [Adventure Works]  
  
WITH MEMBER Measures.x AS Dimensions  
   ('[Product].[Product Model Lines]').Levels.Count  
SELECT Measures.x on 0  
FROM [Adventure Works]  
  
WITH MEMBER Measures.x AS Dimensions  
   ('[Product].[Product Model Lines]').Members.Count  
SELECT Measures.x on 0  
FROM [Adventure Works]  
  
WITH MEMBER Measures.x AS Dimensions(0).Name  
SELECT Measures.x on 0  
FROM [Adventure Works]  
  
WITH MEMBER Measures.x AS Dimensions(0).Levels.Count  
SELECT measures.x on 0  
FROM [Adventure Works]  
  
WITH MEMBER Measures.x AS Dimensions(0).Members.Count  
SELECT measures.x on 0  
FROM [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
