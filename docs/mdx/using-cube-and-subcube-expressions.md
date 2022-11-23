---
description: "Using Cube and Subcube Expressions"
title: "Using Cube and Subcube Expressions | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# Using Cube and Subcube Expressions


  You use cube and subcube expressions in Multidimensional Expressions (MDX) statements to define, manipulate, or retrieve data from a cube or subcube.  
  
## Cube Expressions  
 A cube expression contains either a cube identifier or the CURRENTCUBE keyword, and therefore can only be simple expressions. Many MDX statements use the CURRENTCUBE keyword to identify the current cube context instead of requiring a cube identifier.  
  
 A cube identifier appears as *Cube_Name* in BNF notation descriptions of MDX statements.  
  
 Cube expressions may appear in several places. In an MDX SELECT statement they specify the cube from which data is to be retrieved. In the following example query, the expression [Adventure Works] refers to the cube of that name:  
  
 `SELECT [Measures].[Internet Sales Amount] ON COLUMNS`  
  
 `FROM [Adventure Works]`  
  
 In the CREATE MEMBER statement, the cube expression specifies which cube the calculated member you are creating is to appear on. In the following example, the statement creates a calculated measure on the Measures dimension of the Adventure Works cube:  
  
 `CREATE MEMBER [Adventure Works].[Measures].[Test] AS 1`  
  
 When you use the CREATE MEMBER statement inside an MDX Script, the name of the cube can be replaced with the CURRENTCUBE keyword, since the cube where the calculated member is to be created must be the same cube that the MDX Script belongs to, as shown in the followingexample:  
  
 `CREATE MEMBER CURRENTCUBE.[Measures].[Test] AS 1;`  
  
 Doing this makes it easier to copy and paste calculated member definitions from one cube to another since the name of the cube is no longer hard-coded.  
  
## SubCube Expressions  
 A subcube expression can contain a subcube identifier or an MDX statement that returns a subcube. If the subcube expression contains a subcube identifier, it will be a simple expression. If it contains an MDX statement that returns a subcube, it is a complex statement. The MDX SELECT statement, for example, returns a subcube and can be used where subcube expressions are allowed, as shown in the following example:  
  
 `SELECT [Measures].MEMBERS ON COLUMNS,`  
  
 `[Date].[Calendar Year].MEMBERS ON ROWS`  
  
 `FROM`  
  
 `(SELECT [Measures].[Internet Sales Amount] ON COLUMNS,`  
  
 `[Date].[Calendar Year].&[2004] ON ROWS`  
  
 `FROM [Adventure Works])`  
  
 This use of a SELECT statement in the FROM clause is also referred to as a subselect.  
  
 Another common scenario where subcube expressions are encountered is when making scoped assignments in an MDX Script. In the following example, the SCOPE statement is used to limit an assignment to a subcube consisting of [Measures].[Internet Sales Amount]:  
  
 `SCOPE([Measures].[Internet Sales Amount]);`  
  
 `This=1;`  
  
 `END SCOPE;`  
  
 A subcube identifier appears as *Subcube_Name*. in BNF notation descriptions of MDX statements.  
  
## See Also  
 [The Basic MDX Query &#40;MDX&#41;](/analysis-services/multidimensional-models/mdx/mdx-query-the-basic-query)   
 [Building Subcubes in MDX &#40;MDX&#41;](/analysis-services/multidimensional-models/mdx/building-subcubes-in-mdx-mdx)   
 [CREATE SUBCUBE Statement &#40;MDX&#41;](../mdx/mdx-data-definition-create-subcube.md)   
 [Expressions &#40;MDX&#41;](../mdx/expressions-mdx.md)   
 [SCOPE Statement &#40;MDX&#41;](../mdx/mdx-scripting-scope.md)  
  
