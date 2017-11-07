---
title: "LookupCube (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "LOOKUPCUBE"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "LookupCube function"
ms.assetid: 243fa101-328a-4016-86e0-d8b5977e15a9
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# LookupCube (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the value of a Multidimensional Expressions (MDX) expression evaluated over another specified cube in the same database.  
  
## Syntax  
  
```  
  
Numeric expression syntax  
LookupCube(Cube_Name, Numeric_Expression )  
  
String expression syntax  
LookupCube(Cube_Name, String_Expression )  
```  
  
## Arguments  
 *Cube_Name*  
 A valid string expression that specifies the name of a cube.  
  
 *Numeric_Expression*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number.  
  
 *String_Expression*  
 A valid string expression that is typically a valid Multidimensional Expressions (MDX) expression of cell coordinates that returns a string.  
  
## Remarks  
 If a numeric expression is specified, the **LookupCube** function evaluates the specified numeric expression in the specified cube and returns the resulting numeric value.  
  
 If a string expression is specified, the **LookupCube** function evaluates the specified string expression in the specified cube and returns the resulting string value.  
  
 The **LookupCube** function works on cubes within the same database as the source cube on which the MDX query that contains the **LookupCube** function is running.  
  
> [!IMPORTANT]  
>  You must provide any necessary current members in the numeric or string expression because the context of the current query does not carry over to the cube being queried.  
  
 Any calculation using the **LookupCube** function is likely to suffer from poor performance. Instead of using this function, consider redesigning your solution so that all of the data you need is present in one cube.  
  
## Examples  
 The following query demonstrates the use of LookupCube:  
  
 `WITH MEMBER MEASURES.LOOKUPCUBEDEMO AS`  
  
 `LOOKUPCUBE("Adventure Works", "[Measures].[In" + "ternet Sales Amount]")`  
  
 `SELECT MEASURES.LOOKUPCUBEDEMO  ON 0`  
  
 `FROM [Adventure Works]`  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
