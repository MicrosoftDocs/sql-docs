---
title: "Establishing Cube Context in a Query (MDX) | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: mdx
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Establishing Cube Context in a Query (MDX)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Every MDX query runs within a specified cube context. This context defines the members that are evaluated by the expressions within the query.  
  
 In the SELECT statement, the FROM clause determines the cube context. This context can be the whole cube or just a subcube from that cube. Having specified cube context through the FROM clause, you can use additional functions to expand or restrict that context.  
  
> [!NOTE]  
>  The SCOPE and CALCULATE statements also let you manage cube context from within an MDX script. For more information, see [MDX Scripting Fundamentals &#40;Analysis Services&#41;](../../../analysis-services/multidimensional-models/mdx/mdx-scripting-fundamentals-analysis-services.md).  
  
## FROM Clause Syntax  
 The following syntax describes the FROM clause:  
  
```  
<SELECT subcube clause> ::=  
   Cube_Identifier |   
   (SELECT [  
      * |   
      ( <SELECT query axis clause> [ , <SELECT query axis clause> ... ] ) ]   
   FROM <SELECT subcube clause> <SELECT slicer axis clause> )  
```  
  
 In this syntax, notice that it is the `<SELECT subcube clause>` clause that describes the cube or subcube on which the SELECT statement is executed.  
  
 A simple example of a FROM clause would be one that runs against the entire Adventure Works sample cube. Such a FROM clause would have the following format:  
  
```  
FROM [Adventure Works]  
```  
  
 For more information about the FROM clause in the MDX SELECT statement, see [SELECT Statement &#40;MDX&#41;](../../../mdx/mdx-data-manipulation-select.md).  
  
## Refining the Context  
 Although the FROM clause specifies the cube context as within a single cube, this does not have to limit you from working with data from more than one cube at a time.  
  
 You can use the MDX [LookupCube](../../../mdx/lookupcube-mdx.md) function to retrieve data from cubes outside the cube context. Additionally, functions such as the [Filter](../../../mdx/filter-mdx.md) function, are available that allow temporary restriction of the context while evaluating the query.  
  
## See Also  
 [MDX Query Fundamentals &#40;Analysis Services&#41;](../../../analysis-services/multidimensional-models/mdx/mdx-query-fundamentals-analysis-services.md)  
  
  
