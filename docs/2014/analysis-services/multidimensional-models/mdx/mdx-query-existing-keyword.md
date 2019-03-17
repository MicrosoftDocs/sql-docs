---
title: "EXISTING Keyword (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "EXISTING"
helpviewer_keywords: 
  - "Existing keyword"
ms.assetid: 651ee9ac-04ef-4316-87c9-a3df5ac27d22
author: minewiskan
ms.author: owend
manager: craigg
---
# EXISTING Keyword (MDX)
  Forces a specified set to be evaluated within the current context.  
  
## Syntax  
  
```  
  
Existing Set_Expression  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) set expression.  
  
## Remarks  
 By default, sets are evaluated within the context of the cube that contains the members of the set. The `Existing` keyword forces a specified set to be evaluated within the current context instead.  
  
## Example  
 The following example returns the count of the resellers whose sales have declined over the previous time period, based on user-selected State-Province member values evaluated using the `Aggregate` function. The [Hierarchize &#40;MDX&#41;](/sql/mdx/hierarchize-mdx) and [DrilldownLevel (MDX)](/sql/mdx/drilldownlevel-mdx) functions are used to return values for declining sales for product categories in the Product dimension. The `Existing` keyword forces the set in the `Filter` function to be evaluated in the current context - that is, for the Washington and Oregon members of the State-Province attribute hierarchy.  
  
```  
WITH MEMBER Measures.[Declining Reseller Sales] AS  
   Count  
      (Filter  
         (Existing  
            (Reseller.Reseller.Reseller)  
         , [Measures].[Reseller Sales Amount] <   
            ([Measures].[Reseller Sales Amount]  
               ,[Date].Calendar.PrevMember  
            )  
        )  
      )  
MEMBER [Geography].[State-Province].x AS   
   Aggregate   
      ( {[Geography].[State-Province].&[WA]&[US]  
         , [Geography].[State-Province].&[OR]&[US] }   
      )  
SELECT NON EMPTY HIERARCHIZE   
      (AddCalculatedMembers   
         (   
            {DrillDownLevel  
               ({[Product].[All Products]}  
               )  
            }   
         )   
      ) DIMENSION PROPERTIES PARENT_UNIQUE_NAME ON COLUMNS   
FROM [Adventure Works]  
WHERE   
      ( [Geography].[State-Province].x  
        , [Date].[Calendar].[Calendar Quarter].&[2003]&[4]  
        ,[Measures].[Declining Reseller Sales]  
      )  
  
```  
  
## See Also  
 [Count &#40;Set&#41; &#40;MDX&#41;](/sql/mdx/count-set-mdx)   
 [AddCalculatedMembers &#40;MDX&#41;](/sql/mdx/addcalculatedmembers-mdx)   
 [Aggregate &#40;MDX&#41;](/sql/mdx/aggregate-mdx)   
 [Filter &#40;MDX&#41;](/sql/mdx/filter-mdx)   
 [Properties &#40;MDX&#41;](/sql/mdx/properties-mdx)   
 [DrilldownLevel &#40;MDX&#41;](/sql/mdx/drilldownlevel-mdx)   
 [Hierarchize &#40;MDX&#41;](/sql/mdx/hierarchize-mdx)   
 [MDX Function Reference &#40;MDX&#41;](/sql/mdx/mdx-function-reference-mdx)  
  
  
