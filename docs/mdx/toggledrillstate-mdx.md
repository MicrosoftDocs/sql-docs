---
title: "ToggleDrillState (MDX) | Microsoft Docs"
ms.date: 06/04/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# ToggleDrillState (MDX)


  Toggles the drill state of members between drilldown and drillllup modes.  
  
## Syntax  
  
```  
  
ToggleDrillState(Set_Expression1,Set_Expression2 [, [RECURSIVE] [,INCLUDE_CALC_MEMBERS] ] )  
```  
  
## Arguments  
 *Set_Expression1*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Set_Expression2*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Recursive*  
 (Optional). A keyword that indicates recursive comparison of sets. The **ToggleDrillState** function is a combination of the **DrillupMember** and **DrilldownMember** functions. Recursion only applies when the member is in the **DrilldownMember** state.  
  
 *Include_calc_members*  
 (Optional). A flag indicating whether to include calculated members, should they exist, at the drill down level.  
  
## Remarks  
 The **ToggleDrillState** function toggles the drill state of each member of the second set that is present in the first set. The first set can contain tuples with any dimensionality, but the second set must contain members of a single dimension. The **ToggleDrillState** function is a combination of the **DrillupMember** and **DrilldownMember** functions. If the member, *m*, of the second set is present in the first set, and that member is drilled down (that is, has a descendant immediately following it), then `DrillupMember(Set_Expression1, {m})` is applied to the member or tuple in the first set. If that *m* member is drilled up (that is, there is no descendant of *m* that immediately follows *m*), `DrilldownMember(Set_Expression1, {m}[, RECURSIVE])` is applied to the first set.  
  
 If the optional **RECURSIVE** flag is used, drill up and drill down are applied recursively. For more information about the recursive flag, see the [DrillupMember](../mdx/drillupmember-mdx.md) and [DrilldownMember](../mdx/drilldownmember-mdx.md) functions.  
  
 Querying the XMLA property MdpropMdxDrillFunctions enables you to verify the level of support that the server provides for the drilling functions; see [Supported XMLA Properties &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/propertylist-element-supported-xmla-properties) for details.  
  
 See [Database Journal: MDX Set Functions: The ToggleDrillState() Function](https://go.microsoft.com/fwlink/?LinkId=517759) for scenarios and examples involving this function.  
  
## Example  
 The following example drills down on the Australia member of the first set, and drills up on the United States member of the first set.  
  
```  
SELECT ToggleDrillState  
   ({[Geography].[Geography].[Country].Members, [Geography].[Geography].[Country].&[United States].Children},  
      {[Geography].[Geography].[Country].[Australia]  
      , [Geography].[Geography].[Country].&[United States]}  
      --, recursive  
      --, include_calc_members  
   ) ON 0  
   FROM [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
