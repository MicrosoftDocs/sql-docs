---
title: "Creating Query-Scoped Cell Calculations (MDX) | Microsoft Docs"
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
# MDX Cell Calculations - Query-Scoped Cell Calculations
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  You use the **WITH** keyword in Multidimensional Expressions (MDX) to describe calculated cells within the context of a query. The **WITH** keyword has the following syntax:  
  
```  
WITH CELL CALCULATION Cube_Name.CellCalc_Identifier  String_Expression  
```  
  
 The `CellCalc_Identifier` value is the name of the calculated cells. The `String_Expression` value contains a list of orthogonal, single-dimensional MDX set expressions. Each of these set expressions must resolve to one of the categories listed in the following table.  
  
|Category|Description|  
|--------------|-----------------|  
|Empty set|An MDX set expression that resolves into an empty set. In this case, the scope of the calculated cell is the whole cube.|  
|Single member set|An MDX set expression that resolves into a single member.|  
|Set of level members|An MDX set expression that resolves into the members of a single level. An example of such a set expression is the *Level_Expression*.**Members** MDX function. To include calculated members, use the *Level_Expression*.**AllMembers** MDX function. For more information, see [AllMembers &#40;MDX&#41;](../../../mdx/allmembers-mdx.md).|  
|Set of descendants|An MDX set expression that resolves into the descendants of a specified member. An example of such a set expression is the **Descendants**(*Member_Expression*, *Level_Expresion*, *Desc_Flag*) MDX function. For more information, see [Descendants &#40;MDX&#41;](../../../mdx/descendants-mdx.md).|  
  
 If the `String_Expression` argument does not describe a dimension, MDX assumes that all members are included for the purposes of constructing the calculation subcube. Therefore, if the `String_Expression` argument is NULL, the calculated cells definition applies to the whole cube.  
  
 The `MDX_Expression` argument contains an MDX expression that evaluates to a cell value for all the cells defined in the `String_Expression` argument.  
  
## Additional Considerations  
 MDX processes the calculation condition, specified by the **CONDITION** property, only once. This single processing provides increased performance for the evaluation of multiple calculated cells definitions, especially with overlapping calculated cells across cube passes.  
  
 When this single processing occurs depends on the creation scope of the calculated cells definition:  
  
-   If created at global scope, as part of a cube, MDX process the calculation condition when the cube is processed. If cells are modified in the cube in any way, and the cells are included in the calculation subcube of a calculated cells definition, the calculation condition may not be accurate until the cube is reprocessed. Cell modification can occur from writebacks, for example. The calculation condition is reprocessed when the cube is reprocessed.  
  
-   If created at session scope, MDX process the calculation condition when the statement is issued during the session. As with calculated cells definitions created globally, if the cells are modified, the calculation condition may not be accurate for the calculated cells definition.  
  
-   If created at query scope, MDX processes the calculation condition when the query runs. The cell modification issue applies here, also, although data latency issues are minimal because of the low processing time of MDX query execution.  
  
 On the other hand, MDX processes the calculation formula whenever an MDX query is issued against the cube involving cells included in the calculated cells definition. This processing occurs regardless of the creation scope.  
  
## See Also  
 [CREATE CELL CALCULATION Statement &#40;MDX&#41;](../../../mdx/mdx-data-definition-create-cell-calculation.md)  
  
  
