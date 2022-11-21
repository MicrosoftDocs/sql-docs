---
description: "MDX Data Manipulation - UPDATE CUBE"
title: "UPDATE CUBE Statement (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# MDX Data Manipulation - UPDATE CUBE


  The UPDATE CUBE statement is used to write back data to any cell in a cube that aggregates to its parent using the SUM aggregation. For more explanation and an example, see "Understanding Allocations" in this blog post: [Building a Writeback Application with Analysis Services (blog)](/archive/blogs/data_otaku/building-a-writeback-application-with-analysis-services).  
  
## Syntax  
  
```  
  
UPDATE [ CUBE ] Cube_Name   
   SET   
            <update clause>   
           [, <update clause> ...n ]  
  
<update clause> ::=   
      Tuple_Expression[.VALUE]= New_Value  
      [   
        USE_EQUAL_ALLOCATION   
        | USE_EQUAL_INCREMENT   
        | USE_WEIGHTED_ALLOCATION [ BY Weight_Expression]   
        | USE_WEIGHTED_INCREMENT [ BY Weight_Expression]  
      ]  
```  
  
## Arguments  
 *Cube_Name*  
 A valid string that provides the name of a cube.  
  
 *Tuple_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a tuple.  
  
 *New_Value*  
 A valid numeric expression.  
  
 *Weight_Expression*  
 A valid Multidimensional Expressions (MDX) numeric expression that returns a decimal value between 0 and 1.  
  
## Remarks  
 You can update the value of a specified leaf or nonleaf cell in a cube, optionally allocating the value for a specified non-leaf cell across dependent leaf cells. The cell specified by the tuple expression can be any valid cell in the multidimensional space (that is, the cell does not have to be a leaf cell). However, the cell must be aggregated with the [Sum](../mdx/sum-mdx.md) aggregate function and must not include a calculated member in the tuple that is used to identify the cell.  
  
 It may be helpful to think of the **UPDATE CUBE** statement as a subroutine that will automatically generate a series of individual cell writeback operations to leaf and non-leaf cells that will roll up into a specified sum.  
  
 The following is a  description of the methods of allocation.  
  
 **USE_EQUAL_ALLOCATION:** Every leaf cell that contributes to the updated cell will be assigned an equal value based on the following expression.  
  
```  
<leaf cell value> =   
<New Value> / Count(leaf cells that are contained in <tuple>)  
```  
  
 **USE_EQUAL_INCREMENT:** Every leaf cell that contributes to the updated cell will be changed according to the following expression.  
  
```  
<leaf cell value> = <leaf cell value> +   
(<New Value > - <existing value>) /  
Count(leaf cells contained in <tuple>)  
```  
  
 **USE_WEIGHTED_ALLOCATION:** Every leaf cell that contributes to the updated cell will be assigned an equal value that is based on the following expression.  
  
```  
<leaf cell value> = < New Value> * Weight_Expression  
```  
  
 **USE_WEIGHTED_INCREMENT:** Every leaf cell that contributes to the updated cell will be changed according to the following expression.  
  
```  
<leaf cell value> = <leaf cell value> +   
(<New Value> - <existing value>)  * Weight_Expression  
```  
  
 If a weight expression is not specified, the **UPDATE CUBE** statement implicitly uses the following expression.  
  
```  
Weight_Expression = <leaf cell value> / <existing value>  
```  
  
 A weight expression should be expressed as a decimal value between zero (0) and 1. This value specifies the ratio of the allocated value that you want to assign to the leaf cells that are affected by the allocation. The client application programmer's has the responsibility of creating expressions whose rollup aggregate values will equal the allocated value of the expression.  
  
> [!CAUTION]  
>  The client application must consider the allocation of all dimensions concurrently to avoid possible unexpected results, including incorrect rollup values or inconsistent data.  
  
 Each **UPDATE CUBE** allocation should be considered to be atomic for transactional purposes. This means, that if any one of the allocation operations fails for any reason, such as an error in a formula or a security violation, the whole UPDATE CUBE operation will fail. Before the calculations of the individual allocation operations are processed, a snapshot of the data is taken to ensure that the resulting calculations are correct.  
  
> [!CAUTION]  
>  When used on a measure that contains integers, the USE_WEIGHTED_ALLOCATION method can return imprecise results caused by incremental rounding changes.  
  
> [!IMPORTANT]  
>  When updated cells do not overlap, the **Update Isolation Level** connection string property can be used to enhance performance for UPDATE CUBE.  
  
## See Also  
 <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.ConnectionString%2A>   
 [MDX Data Manipulation Statements &#40;MDX&#41;](../mdx/mdx-data-manipulation-statements-mdx.md)  
  
