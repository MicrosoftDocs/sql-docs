---
description: "DrilldownLevelTop (MDX)"
title: "DrilldownLevelTop (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# DrilldownLevelTop (MDX)


  Drills down the topmost members of a set, at a specified level, to one level below.  
  
## Syntax  
  
```  
  
DrilldownLevelTop(<Set_Expression>, <Count> [,[<Level_Expression>] [,[<Numeric_Expression>][,INCLUDE_CALC_MEMBERS]]])  
  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Count*  
 A valid numeric expression that specifies the number of tuples to be returned.  
  
 *Level_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a level.  
  
 *Numeric_Expression*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number.  
  
 *Include_Calc_Members*  
 A keyword for adding calculated members to drilldown results.  
  
## Remarks  
 If a numeric expression is specified, the **DrilldownLevelTop** function sorts, in descending order, the children of each member in the specified set according to the value of the numeric expression, as evaluated over the set of child members. If a numeric expression is not specified, the function sorts, in descending order, the children of each member in the specified set according to the values of the cells represented by the set of child members, as determined by the query context.  
  
 After sorting, the **DrilldownLevelTop** function returns a set that contains the parent members and the number of child members, specified in *Count,* with the highest value.  
  
 The **DrilldownLevelTop** function is similar to the [DrilldownLevel](../mdx/drilldownlevel-mdx.md) function, but instead of including all children for each member at the specified level, the **DrilldownLevelTop** function returns the topmost number of child members.  
  
 Querying the XMLA property MdpropMdxDrillFunctions enables you to verify the level of support that the server provides for the drilling functions; see [Supported XMLA Properties &#40;XMLA&#41;](/analysis-services/xmla/xml-elements-properties/propertylist-element-supported-xmla-properties) for details.  
  
## Examples  
 The following example returns the top three children of the Product Category level, based on the default measure. In the Adventure Works sample cube, the top three children for Accessories are Bike Racks, Bike Stands, and Bottles and Cages. In Management Studio, in the MDX query window, you can navigate to Products | Product Categories | Members | All Products | Accessories to view the complete list. You can increase the Count argument to return more members.  
  
```  
SELECT DrilldownLevelTop   
   ([Product].[Product Categories].children,  
   3,  
   [Product].[Product Categories].[Category])  
   ON 0  
   FROM [Adventure Works]  
```  
  
 The next example illustrates using the **include_calc_members** flag, used to include calculated members in the drill down level. The measure [Reseller Order Count] is included in the **DrilldownLevelTop** statement to ensure that the return values are sorted by that measure.  
  
```  
WITH MEMBER   
[Product].[Product Categories].[Category].&[3].[Premium Clothes] AS  
[Product].[Product Categories].[Subcategory].&[18] +  
[Product].[Product Categories].[Subcategory].&[21]  
SELECT [Measures].[Reseller Order Count] ON 0,  
DRILLDOWNLEVELTOP(  
  [Product].[Product Categories].children ,  
  2,  
  [Product].[Product Categories].[Category] ,  
  [Measures].[Reseller Order Count],  
  INCLUDE_CALC_MEMBERS ) ON 1  
FROM [Adventure Works]  
```  
  
## See Also  
 [DrilldownLevel &#40;MDX&#41;](../mdx/drilldownlevel-mdx.md)   
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
