---
title: "EXISTING Keyword (MDX) | Microsoft Docs"
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
# MDX Query - EXISTING Keyword
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Forces a specified set to be evaluated within the current context.  
  
## Syntax  
  
```  
  
Existing Set_Expression  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) set expression.  
  
## Remarks  
 By default, sets are evaluated within the context of the cube that contains the members of the set. The **Existing** keyword forces a specified set to be evaluated within the current context instead.  
  
## Example  
 The following example returns the count of the resellers whose sales have declined over the previous time period, based on user-selected State-Province member values evaluated using the **Aggregate** function. The [Hierarchize &#40;MDX&#41;](../../../mdx/hierarchize-mdx.md) and [DrilldownLevel (MDX)](../../../mdx/drilldownlevel-mdx.md) functions are used to return values for declining sales for product categories in the Product dimension. The **Existing** keyword forces the set in the **Filter** function to be evaluated in the current context - that is, for the Washington and Oregon members of the State-Province attribute hierarchy.  
  
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
 [Count &#40;Set&#41; &#40;MDX&#41;](../../../mdx/count-set-mdx.md)   
 [AddCalculatedMembers &#40;MDX&#41;](../../../mdx/addcalculatedmembers-mdx.md)   
 [Aggregate &#40;MDX&#41;](../../../mdx/aggregate-mdx.md)   
 [Filter &#40;MDX&#41;](../../../mdx/filter-mdx.md)   
 [Properties &#40;MDX&#41;](../../../mdx/properties-mdx.md)   
 [DrilldownLevel &#40;MDX&#41;](../../../mdx/drilldownlevel-mdx.md)   
 [Hierarchize &#40;MDX&#41;](../../../mdx/hierarchize-mdx.md)   
 [MDX Function Reference &#40;MDX&#41;](../../../mdx/mdx-function-reference-mdx.md)  
  
  
