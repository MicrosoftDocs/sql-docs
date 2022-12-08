---
description: "DrilldownMember (MDX)"
title: "DrilldownMember (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# DrilldownMember (MDX)


  Drills down the members in a specified set that are present in a second specified set.  
  
 Alternatively, the function drills down on a set of tuples by using the first tuple hierarchy or the optionally specified hierarchy.  
  
## Syntax  
  
```  
  
DrillDownMember(<Set_Expression1>, <Set_Expression2> [,[<Target_Hierarchy>]] [,[RECURSIVE][,INCLUDE_CALC_MEMBERS]])  
```  
  
## Arguments  
 *Set_Expression1*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Set_Expression2*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Target_Hierarchy*  
 A valid Multidimensional Expressions (MDX) expression that returns a hierarchy.  
  
 *Recursive*  
 A keyword that indicates recursive comparison of sets.  
  
 *Include_Calc_Members*  
 A keyword to enable calculated members to be included in drilldown results.  
  
## Remarks  
 This function returns a set of child members that are ordered by hierarchy, and includes members specified in the first set that are also present in the second set. Parent members will not be drilled down if the first set contains the parent member and one or more children. The first set can have any dimensionality, but the second set must contain a one-dimensional set. Order is preserved among the original members in the first set, except that all child members included in the result set of the function are included immediately under their parent member. The function constructs the result set by retrieving the children for each member in the first set that is also present in the second set. If **RECURSIVE** is specified, the function continues to recursively compare the members of the result set against the second set, retrieving the children for each member in the result set that is also present in the second set until no more members from the result set can be found in the second set.  
  
 Querying the XMLA property **MdpropMdxDrillFunctions** enables you to verify the level of support that the server provides for the drilling functions; see [Supported XMLA Properties &#40;XMLA&#41;](/analysis-services/xmla/xml-elements-properties/propertylist-element-supported-xmla-properties) for details.  
  
 The first set can contain tuples instead of members. Tuple drilldown is an extension of OLE DB, and it returns a set of tuples instead of members.  
  
> [!IMPORTANT]  
>  A member will not get drilled down into if it is immediately followed by one of its children. The order of members in the set matters for both the Drilldown* and Drillup\* families of functions.  
  
## Examples  
 The following example drills down into Australia, which is the member of the first set which is also present in the second set.  
  
```  
SELECT DrilldownMember   
   ( [Geography].[Geography].Children,  
      {[Geography].[Geography].[Country].[Australia],  
        [Geography].[Geography].[State-Province].[New South Wales]}  
   )  
   ON 0  
   FROM [Adventure Works]  
```  
  
 The following example drills down into Australia, which is the member of the first set which is also present in the second set. However, because the RECURSIVE argument is present, the function continues to recursively compare the members of the result set (members of the State-Province level) against the second set, retrieving the children for each member in the result set (members of the City level) that is also present in the second set until no more members from the result set can be found in the second set.  
  
```  
SELECT DrilldownMember   
   ( [Geography].[Geography].Children,  
      {[Geography].[Geography].[Country].[Australia],  
        [Geography].[Geography].[State-Province].[New South Wales]}  
   ,RECURSIVE)  
   ON 0  
   FROM [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
