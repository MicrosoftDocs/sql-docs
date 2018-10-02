---
title: "Creating Session-Scoped Calculated Cells | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "session-scoped calculated members [MDX]"
ms.assetid: f2d14a89-6286-4e74-9afb-091076f93f21
author: minewiskan
ms.author: owend
manager: craigg
---
# Creating Session-Scoped Calculated Cells
    
> [!IMPORTANT]  
>  This syntax has been deprecated. You should use MDX assignments should instead. For more information on assignments, see [The Basic MDX Script &#40;MDX&#41;](the-basic-mdx-script-mdx.md).  
  
 To create calculated cells that are available to all queries in the same session, you can use either the [CREATE CELL CALCULATION](/sql/mdx/mdx-data-definition-create-cell-calculation) statement or the [ALTER CUBE](/sql/mdx/mdx-data-definition-alter-cube) statement. Both statements have the same result.  
  
## CREATE CELL CALCULATION Syntax  
  
> [!IMPORTANT]  
>  This syntax has been deprecated. You should use MDX assignments should instead. For more information on assignments, see [The Basic MDX Script &#40;MDX&#41;](the-basic-mdx-script-mdx.md).  
  
 Use the following syntax to use the CREATE CELL CALCULATION statement to define a session-scoped calculated cell:  
  
```  
CREATE CELL CALCULATION Cube_Expression.<CREATE CELL CALCULATION body clause>  
  
<CREATE CELL CALCULATION body clause> ::=CellCalc_Identifier FOR String_Expression AS 'MDX_Expression'   
   [ <CREATE CELL CALCULATION property clause> [ , <CREATE CELL CALCULATION property clause> ... ] ]  
  
<CREATE CELL CALCULATION property clause> ::=  
   ( CONDITION = 'Logical_Expression' ) |   
   ( DISABLED = { TRUE | FALSE } ) |   
   ( DESCRIPTION =String_Expression ) |   
   ( CALCULATION_PASS_NUMBER = Integer_Expression ) |   
   ( CALCULATION_PASS_DEPTH = Integer_Expression ) |   
   ( SOLVE_ORDER = Integer_Expression ) |   
   ( FORMAT_STRING = String_Expression ) |   
   ( CellProperty_Identifier = Scalar_Expression )  
```  
  
## ALTER CUBE Syntax  
  
> [!IMPORTANT]  
>  This syntax has been deprecated. You should use MDX assignments should instead. For more information on assignments, see [The Basic MDX Script &#40;MDX&#41;](the-basic-mdx-script-mdx.md).  
  
 Use the following syntax to use the ALTER CUBE statement to define a session-scoped calculated cell:  
  
```  
ALTER CUBE Cube_Identifier CREATE CELL CALCULATION  
FOR String_Expression AS 'MDX_Expression'   
   [ <CREATE CELL CALCULATION property clause> [ , <CREATE CELL CALCULATION property clause> ... ] ]  
  
<CREATE CELL CALCULATION property clause> ::=  
   ( CONDITION = 'Logical_Expression' ) |   
   ( DISABLED = { TRUE | FALSE } ) |   
   ( DESCRIPTION =String_Expression ) |   
   ( CALCULATION_PASS_NUMBER = Integer_Expression ) |   
   ( CALCULATION_PASS_DEPTH = Integer_Expression ) |   
   ( SOLVE_ORDER = Integer_Expression ) |   
   ( FORMAT_STRING = String_Expression ) |   
   ( CellProperty_Identifier = Scalar_Expression )  
```  
  
 The `String_Expression` value contains a list of orthogonal, single-dimensional MDX set expressions, each of which must resolve to one of the categories of sets that are listed in the following table.  
  
|Category|Description|  
|--------------|-----------------|  
|Empty set|An MDX set expression that resolves into an empty set. In this case, the scope of the calculated cell is the whole cube.|  
|Single member set|An MDX set expression that resolves into a single member.|  
|Set of level members|An MDX set expression that resolves into the members of a single level. An example of this is the *Level_Expression*.`Members` MDX function. To include calculated members, use the *Level_Expression*.`AllMembers` MDX function.<br /><br /> For more information, see [AllMembers &#40;MDX&#41;](/sql/mdx/allmembers-mdx).|  
|Set of descendants|An MDX set expression that resolves into the descendants of a specified member. An example of this is the `Descendants`(*Member_Expression*, *Level_Expression*, *Desc_Flag*) MDX function.<br /><br /> For more information, see [Descendants &#40;MDX&#41;](/sql/mdx/descendants-mdx).|  
  
## See Also  
 [Building Cell Calculations in MDX &#40;MDX&#41;](../../multidimensional-models-olap-logical-cube-objects/calculations.md)  
  
  
