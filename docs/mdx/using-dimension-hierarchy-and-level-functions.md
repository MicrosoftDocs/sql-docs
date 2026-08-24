---
title: "Using Dimension, Hierarchy, and Level Functions"
description: "Using Dimension, Hierarchy, and Level Functions"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
---
# Using Dimension, Hierarchy, and Level Functions


  Dimension, hierarchy, and level functions are useful for traversing the multidimensional structures found in Analysis Services. Typically, you use such functions in conjunction with other functions to obtain information about the members of a dimension, hierarchy, or level.  
  
 The following example shows how to use the **.Dimension**, **.Hierarchy**, and **.Level** functions:  
  
```  
WITH  
MEMBER MEASURES.DIMENSIONNAME AS [Date].[Calendar].CURRENTMEMBER.DIMENSION.NAME  
MEMBER MEASURES.HIERARCHYNAME AS [Date].[Calendar].CURRENTMEMBER.HIERARCHY.NAME  
MEMBER MEASURES.LEVELNAME AS [Date].[Calendar].LEVEL.NAME  
SELECT  
{MEASURES.DIMENSIONNAME, MEASURES.HIERARCHYNAME, MEASURES.LEVELNAME}  
ON Columns,  
[Date].[Calendar].MEMBERS  
ON Rows  
FROM [Adventure Works]  
```  
  
## Related content

- [Dimension (MDX)](dimension-mdx.md)
- [Functions (MDX Syntax)](functions-mdx-syntax.md)
- [Hierarchy (MDX)](hierarchy-mdx.md)
- [Level (MDX)](level-mdx.md)
