---
description: "ValidMeasure (MDX)"
title: "ValidMeasure (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# ValidMeasure (MDX)


  Returns the value of a measure in a cube by forcing inapplicable dimensions to their All level (or default member if not aggregatable) when returning the result for a specified tuple.  
  
## Syntax  
  
```  
  
ValidMeasure(Tuple_Expression)   
```  
  
## Arguments  
 *Tuple_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a tuple.  
  
## Remarks  
 The **ValidMeasure** function returns the value of a tuple, ignoring attributes that have no relationship with the measure group of the Measure whose value the tuple returns. An attribute can be unrelated to a measure for two reasons:  
  
-   The attribute's dimension has no relationship with the measure group of the measure in the tuple.  
  
-   The attribute's dimension does not have a relationship with the measure group of the measure, but the granularity attribute is not the key attribute, and the granularity attribute does not have a direct relationship with the attribute in the tuple.  
  
 The behavior specified by this function is the default server-side behavior and is controlled by the **IgnoreUnrelatedDimensions** property on the measure group object.  
  
 For each attribute in the specified tuple with granularity (that is to say, where the member in the tuple is not the All member), the current coordinate for each such attribute is moved as follows:  
  
-   Related attributes to the specified attribute member are moved to the member that exists with the current member.  
  
-   Relating attributes to the specified attribute member are moved to the All member (or the default member if the hierarchy is not aggregatable).  
  
-   Unrelated attributes are moved to the All member (based on measure).  
  
## Example  
 The following query shows how the ValidMeasure function can be used to override the behavior of the IgnoreUnrelatedDimensions property. In the Adventure Works cube, the Sales Targets measure group has IgnoreUnrelatedDimensions set to False; since the Date dimension joins to this measure group at the Calendar Quarter granularity, this means that the Sales Quota measure will , by default, return null below Calendar Quarter (although there is also a calculation in the MDX Script which allocates values down to the Month level too). Using the ValidMeasure function in a calculated measure can be used to make the Sales Quota measure behave as if IgnoreUnrelatedDimensions was set to True and force Sales Quota to display the value of the current Calendar Quarter.  
  
```  
WITH MEMBER MEASURES.VTEST AS VALIDMEASURE([Measures].[Sales Amount Quota])  
SELECT {[Measures].[Sales Amount Quota], MEASURES.VTEST} ON 0,  
[Date].[Calendar].MEMBERS ON 1  
FROM [Adventure Works]  
```  
  
 Similarly, the Sales Targets measure group has no relationship at all with the Promotion dimension, so below the All Member of any hierarchy on Promotion it will return null. Again, this behavior can be changed by using ValidMeasure:  
  
 `WITH MEMBER MEASURES.VTEST AS VALIDMEASURE([Measures].[Sales Amount Quota])`  
  
 `SELECT {[Measures].[Sales Amount Quota], MEASURES.VTEST} ON 0,`  
  
 `[Promotion].[Promotions].members ON 1`  
  
 `FROM [Adventure Works]`  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
