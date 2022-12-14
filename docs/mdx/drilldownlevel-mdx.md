---
description: "DrilldownLevel (MDX)"
title: "DrilldownLevel (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# DrilldownLevel (MDX)


  Drills down the members of a set to one level below the lowest level represented in the set.  
  
 Specifying the level at which to drill down is optional, but if you do set the level, you can use either a **level expression** or the **index level**. These arguments are mutually exclusive. Finally, if calculated members are present in the query, you can specify an argument to include them in the rowset.  
  
## Syntax  
  
```  
DrilldownLevel(Set_Expression [,[Level_Expression] ,[Index]] [,INCLUDE_CALC_MEMBERS])  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Level_Expression*  
 (Optional). An MDX expression that explicitly identifies the level at which to drill down. If you specify a level expression, skip the index argument below.  
  
 *Index*  
 (Optional). A valid numeric expression that specifies the hierarchy number to drill down into within the set. You can use the index level instead of Level_Expression to explicitly identify the level at which to drill down.  
  
 *Include_Calc_Members*  
 (Optional). A flag indicating whether to include calculated members, should they exist, at the drill down level.  
  
## Remarks  
 The **DrilldownLevel** function returns a set of child members in a hierarchical order, based on the members included in the specified set. Order is preserved among the original members in the specified set, except that all child members included in the result set of the function are included immediately under their parent member.  
  
 Given a multi-level hierarchical data structure, you can explicitly choose a level at which to drill down. There are two mutually exclusive ways to specify the level. The first approach is to set the **level_expression** argument using an MDX expression that returns the level, An alternative approach is to specify the **index** argument, using a numeric expression that specifies the level by number.  
  
 If a level expression is specified, the function constructs a set in a hierarchical order by retrieving the children of only those members that are at the specified level. If a level expression is specified and there is no member at that level, the level expression is ignored.  
  
 If an index value is specified, the function constructs a set in a hierarchical order by retrieving the children of only those members that are at the next lowest level of the hierarchy referenced in the specified set, given a zero-based index.  
  
 If neither a level expression nor an index value is specified, the function constructs a set in a hierarchical order by retrieving the children of only those members that are at the lowest level of the first dimension referenced in the specified set.  
  
 Querying the XMLA property MdpropMdxDrillFunctions enables you to verify the level of support that the server provides for the drilling functions; see [Supported XMLA Properties &#40;XMLA&#41;](/analysis-services/xmla/xml-elements-properties/propertylist-element-supported-xmla-properties) for details.  
  
## Examples  
 You can try the following examples in the MDX query window in SSMS, using the Adventure Works cube.  
  
 **Example 1 - demonstrates minimal syntax**  
  
 The first example shows the minimal syntax for **DrilldownLevel**. The only required argument is a set expression. Notice that when you run this query, you get the parent [All Categories] and members of the next level down: [Accessories], [Bikes], and so on. Although this example is simple, it demonstrates the basic purpose of the **DrilldownLevel** function, which is drilling down to the next level below.  
  
```  
SELECT DRILLDOWNLEVEL({[Product].[Product Categories]} * {[Sales Territory].[Sales Territory]}}) ON COLUMNS  
FROM [Adventure Works]  
```  
  
 Example 2 - alternate syntax using an explicit index level  
  
 This example demonstrates the alternate syntax, where the index level is specified through a numeric expression. In this case, index level is 0. For a zero-based index, this is the lowest level.  
  
```  
SELECT  
DRILLDOWNLEVEL({[Product].[Product Categories]} * {[Sales Territory].[Sales Territory]},,0) ON COLUMNS  
FROM [Adventure Works]  
```  
  
 Notice that the result set is identical to the previous query. As a general rule, setting the index level is unnecessary unless you want the drill down to start at a specific level. Re-run the previous query, setting the index value to 1, and then 2. With index value set to 1, you see the drill down starts at the second level in the hierarchy. With index value set to 2, drill down starts at the third level, the highest level in this example. The higher the numeric expression, the higher the index level.  
  
 **Example 3 - demonstrates a level expression**  
  
 The next example shows how to use a level expression. Given a set that represents a hierarchical structure, using a level expression allows you to choose a level in the hierarchy to start the drill down.  
  
 In this example, the level of drill down starts at [City], as the second argument of the **DrilldownLevel** function. When you run this query, drill down starts at the [City] level, for the Washington and Oregon states. Per the **DrilldownLevel** function, the result set also includes members at the next level down, [Postal codes].  
  
```  
SELECT [Measures].[Internet Sales Amount] ON COLUMNS,  
   NON EMPTY (  
   DRILLDOWNLEVEL(  
       {[Customer].[Customer Geography].[Country].[United States],  
           DESCENDANTS(  
             { [Customer].[Customer Geography].[State-Province].[Washington],    
               [Customer].[Customer Geography].[State-Province].[Oregon]},   
               [Customer].[Customer Geography].[City]) } ,  
[Customer].[Customer Geography].[City] ) )  ON ROWS  
FROM [Adventure Works]  
```  
  
 **Example 4 - including calculated members**  
  
 The last example shows a calculated member, which appears at the bottom of the result set when you add the **include_calculated_members** flag. Notice that the flag is specified as the fourth parameter.  
  
 This example works because the calculated member is at the same level as the non-calculated members. The calculated member [West Coast] is composed of members from [United States], plus all of the members one level below the [United States].  
  
```  
WITH MEMBER   
[Customer].[Customer Geography].[Country].&[United States].[West Coast] AS  
[Customer].[Customer Geography].[State-Province].&[OR]&[US] +  
[Customer].[Customer Geography].[State-Province].&[WA]&[US] +  
[Customer].[Customer Geography].[State-Province].&[CA]&[US]  
SELECT [Measures].[Internet Order Count] ON 0,  
DRILLDOWNLEVEL([Customer].[Customer Geography].[Country].&[United States],,,INCLUDE_CALC_MEMBERS) on 1  
FROM [Adventure Works]  
```  
  
 If you remove just the flag and re-run the query, you get the same results, minus the calculated member, [West Coast].  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
