---
description: "Sum (MDX)"
title: "Sum (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# Sum (MDX)


  Returns the sum of a numeric expression evaluated over a specified set.  
  
## Syntax  
  
```  
  
Sum( Set_Expression [ , Numeric_Expression ] )  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) set expression.  
  
 *Numeric_Expression*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number.  
  
## Remarks  
 If a numeric expression is specified, the specified numeric expression is evaluated across the set and then summed. If a numeric expression is not specified, the specified set is evaluated in the current context of the members of the set and then summed. If the SUM function is applied to a non-numeric expression, the results are undefined.  
  
> [!NOTE]  
>  Analysis Services ignores nulls when calculating the sum of a set of numbers.  
  
## Examples  
 The following example returns the sum of Reseller Sales Amounts for all members of the Product.Category attribute hierarchy for calendar years 2001 and 2002.  
  
```  
WITH MEMBER Measures.x AS SUM  
   ( { [Date].[Calendar Year].&[2001]  
         , [Date].[Calendar Year].&[2002] }  
      , [Measures].[Reseller Sales Amount]  
    )  
SELECT Measures.x ON 0  
,[Product].[Category].Members ON 1  
FROM [Adventure Works]  
```  
  
 The following example returns the sum of the month-to-date freight costs for Internet sales for the month of July, 2002 through the 20th day of July.  
  
```  
WITH MEMBER Measures.x AS SUM   
   (  
      MTD([Date].[Calendar].[Date].[July 20, 2002])  
     , [Measures].[Internet Freight Cost]  
     )  
SELECT Measures.x ON 0  
FROM [Adventure Works]  
```  
  
 The following example uses the WITH MEMBER keyword and the **SUM** function to define a calculated member in the Measures dimension that contains the sum of the Reseller Sales Amount measure for the Canada and United States members of the Country attribute hierarchy in the Geography dimension.  
  
```  
WITH MEMBER Measures.NorthAmerica AS SUM   
      (  
         {[Geography].[Country].&[Canada]  
            , [Geography].[Country].&[United States]}  
       ,[Measures].[Reseller Sales Amount]  
      )  
SELECT {[Measures].[NorthAmerica]} ON 0,  
[Product].[Category].members ON 1  
FROM [Adventure Works]  
```  
  
 Often, the **SUM** function is used with the **CURRENTMEMBER** function or functions like **YTD** that return a set that varies depending on the currentmember of a hierarchy. For example, the following query returns the sum of the Internet Sales Amount measure for all dates from the beginning of the calendar year to the date displayed on the Rows axis:  
  
 `WITH MEMBER MEASURES.YTDSUM AS`  
  
 `SUM(YTD(), [Measures].[Internet Sales Amount])`  
  
 `SELECT {[Measures].[Internet Sales Amount], MEASURES.YTDSUM} ON 0,`  
  
 `[Date].[Calendar].MEMBERS ON 1`  
  
 `FROM [Adventure Works]`  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
