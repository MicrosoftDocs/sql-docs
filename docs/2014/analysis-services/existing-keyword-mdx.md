---
title: "EXISTING Keyword (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "EXISTING"
helpviewer_keywords: 
  - "Existing keyword"
ms.assetid: 651ee9ac-04ef-4316-87c9-a3df5ac27d22
caps.latest.revision: 38
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
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
 The following example returns the count of the resellers whose sales have declined over the previous time period, based on user-selected State-Province member values evaluated using the `Aggregate` function. The [Hierarchize &#40;MDX&#41;](../Topic/Hierarchize%20\(MDX\).md) and [DrilldownLevel (MDX)](../Topic/DrilldownLevel%20\(MDX\).md) functions are used to return values for declining sales for product categories in the Product dimension. The `Existing` keyword forces the the set in the `Filter` function to be evaluated in the current context - that is, for the Washington and Oregon members of the State-Province attribute hierarchy.  
  
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
 [Count &#40;Set&#41; &#40;MDX&#41;](../Topic/Count%20\(Set\)%20\(MDX\).md)   
 [AddCalculatedMembers &#40;MDX&#41;](../Topic/AddCalculatedMembers%20\(MDX\).md)   
 [Aggregate &#40;MDX&#41;](../Topic/Aggregate%20\(MDX\).md)   
 [Filter &#40;MDX&#41;](../Topic/Filter%20\(MDX\).md)   
 [Properties &#40;MDX&#41;](../Topic/Properties%20\(MDX\).md)   
 [DrilldownLevel &#40;MDX&#41;](../Topic/DrilldownLevel%20\(MDX\).md)   
 [Hierarchize &#40;MDX&#41;](../Topic/Hierarchize%20\(MDX\).md)   
 [MDX Function Reference &#40;MDX&#41;](../Topic/MDX%20Function%20Reference%20\(MDX\).md)  
  
  