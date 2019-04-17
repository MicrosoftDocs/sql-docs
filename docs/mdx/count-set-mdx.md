---
title: "Count (Set) (MDX) | Microsoft Docs"
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
# Count (Set) (MDX)


  Returns the number of cells in a set.  
  
## Syntax  
  
```  
  
Standard syntax  
Count(Set_Expression [ , ( EXCLUDEEMPTY | INCLUDEEMPTY ) ] )  
  
Alternate syntax  
Set_Expression.Count  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
## Remarks  
 The **Count (Set)** function includes or excludes empty cells, depending on the syntax used. If the standard syntax is used, empty cells can be excluded or included by using the **EXCLUDEEMPTY** or **INCLUDEEMPTY** flags, respectively. If the alternate syntax is used, the function always includes empty cells.  
  
 To exclude empty cells in the count of a set, use the standard syntax and the optional **EXCLUDEEMPTY** flag.  
  
> [!NOTE]  
>  The **Count (Set)** function counts empty cells by default. In contrast, the **Count** function in OLE DB that counts a set excludes empty cells by default.  
  
## Examples  
 The following example counts the number of cells in the set of members that consist of the children of the Model Name attribute hierarchy in the Product dimension.  
  
```  
WITH MEMBER measures.X AS  
   [Product].[Model Name].children.count   
SELECT Measures.X ON 0  
FROM [Adventure Works]  
```  
  
 The following example counts the number of products in the Product dimension by using the **DrilldownLevel** function in conjunction with the **Count** function.  
  
```  
Count(DrilldownLevel (   
   [Product].[Product].[Product]))  
```  
  
 The following example returns those resellers with declining sales compared to the previous calendar quarter, by using the **Count** function in conjunction with the **Filter** function and a number of other functions. This query uses the **Aggregate** function to support the selection of multiple geography members, such as for selection from within a drop-down list in a client application.  
  
```  
WITH MEMBER Measures.[Declining Reseller Sales] AS  
   Count  
   (Filter  
      (Existing(Reseller.Reseller.Reseller),  
         [Measures].[Reseller Sales Amount]   
         < ([Measures].[Reseller Sales Amount],  
            [Date].Calendar.PrevMember)  
      )  
   )  
MEMBER [Geography].[State-Province].x AS   
   Aggregate  
   ( {[Geography].[State-Province].&[WA]&[US],   
      [Geography].[State-Province].&[OR]&[US] }   
   )  
SELECT NON EMPTY HIERARCHIZE   
   (AddCalculatedMembers   
      ({DrillDownLevel  
         ({[Product].[All Products]})  
      })  
   ) DIMENSION PROPERTIES PARENT_UNIQUE_NAME ON COLUMNS   
FROM [Adventure Works]  
WHERE ([Geography].[State-Province].x,  
   [Date].[Calendar].[Calendar Quarter].&[2003]&[4]  
   ,[Measures].[Declining Reseller Sales])  
  
```  
  
## See Also  
 [Count &#40;Dimension&#41; &#40;MDX&#41;](../mdx/count-dimension-mdx.md)   
 [Count &#40;Hierarchy Levels&#41; &#40;MDX&#41;](../mdx/count-hierarchy-levels-mdx.md)   
 [Count &#40;Tuple&#41; &#40;MDX&#41;](../mdx/count-tuple-mdx.md)   
 [DrilldownLevel &#40;MDX&#41;](../mdx/drilldownlevel-mdx.md)   
 [AddCalculatedMembers &#40;MDX&#41;](../mdx/addcalculatedmembers-mdx.md)   
 [Hierarchize &#40;MDX&#41;](../mdx/hierarchize-mdx.md)   
 [Properties &#40;MDX&#41;](../mdx/properties-mdx.md)   
 [Aggregate &#40;MDX&#41;](../mdx/aggregate-mdx.md)   
 [Filter &#40;MDX&#41;](../mdx/filter-mdx.md)   
 [PrevMember &#40;MDX&#41;](../mdx/prevmember-mdx.md)   
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
