---
description: "DrilldownMemberBottom (MDX)"
title: "DrilldownMemberBottom (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# DrilldownMemberBottom (MDX)


  Drills down the members in a specified set that are present in a second specified set, limiting the result set to a specified number of members. Alternatively, this function also drills down on a set of tuples by using the first tuple hierarchy or the optionally specified hierarchy.  
  
## Syntax  
  
```  
  
DrillDownMemberBottom(<Set_Expression1>, <Set_Expression2>, <Count> [,[<Numeric_Expresion>] [,[<Hierarchy>]] [,[RECURSIVE][,INCLUDE_CALC_MEMBERS]]])  
```  
  
## Arguments  
 *Set_Expression1*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Set_Expression2*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Count*  
 A valid numeric expression that specifies the number of tuples to be returned.  
  
 *Numeric_Expression*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number.  
  
 *Hierarchy*  
 A valid Multidimensional Expressions (MDX) expression that returns a hierarchy.  
  
 *Recursive*  
 A keyword that indicates recursive comparison of sets.  
  
 *Include_Calc_Members*  
 A keyword to enable calculated members to be included in drilldown results.  
  
## Remarks  
 If a numeric expression is specified, the **DrilldownMemberBottom** function sorts, in ascending order, the children of each member in the first set, according to the value of the numeric expression, as evaluated over the set of child members. If a numeric expression is not specified, the function sorts, in ascending order, the children of each member in the first set according to the values of the cells represented by the set of child members, as determined by the query context. This behavior is similar to the BottomCount and Tail (MDX) functions which return a set of members in natural order, without any sorting.  
  
 After sorting, the **DrilldownMemberBottom** function returns a set that contains the parent members and the number of child members, specified in *Count,* with the lowest value and are contained by both sets.  
  
 If **RECURSIVE** is specified, the function sorts the first set as described previously, then recursively compares the members of the first set, as organized in a hierarchy, against the second set. The function retrieves the bottommost number of children for each member in the first set that is also present in the second set.  
  
 The first set can contain tuples instead of members. Tuple drilldown is an extension of OLE DB, and returns a set of tuples instead of members.  
  
 The **DrilldownMemberBottom** function is similar to the [DrilldownMember](../mdx/drilldownmember-mdx.md) function, but instead of including all children for each member in the first set that is also present in the second set, the **DrilldownMemberBottom** function returns the bottommost number of child members for each member.  
  
 Querying the XMLA property MdpropMdxDrillFunctions enables you to verify the level of support that the server provides for the drilling functions; see [Supported XMLA Properties &#40;XMLA&#41;](/analysis-services/xmla/xml-elements-properties/propertylist-element-supported-xmla-properties) for details.  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
