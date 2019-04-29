---
title: "Understanding Pass Order and Solve Order (MDX) | Microsoft Docs"
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
# MDX Data Manipulation - Understanding Pass Order and Solve Order
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  When a cube is calculated as the result of an MDX script, it can go through many stages of computation depending on the use of various calculation-related features. Each of these stages is referred to as a calculation pass.  
  
 A calculation pass can be referred to by an ordinal position, called the calculation pass number. The count of calculation passes that are required to fully compute all the cells of a cube is referred to as the calculation pass depth of the cube.  
  
 Fact table and writeback data only impact pass 0. Scripts populate data after pass 0; each assignment and calculate statement in a script creates a new pass. Outside the MDX script, references to absolute pass 0 refer to the last pass created by the script for the cube.  
  
 Calculated members are created at all passes, but the expression is applied at the current pass. Prior passes contain the calculated measure, but with a null value.  
  
## Solve Order  
 Solve order determines the priority of calculation in the event of competing expressions. Within a single pass, solve order determines two things:  
  
-   The order in which [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] evaluates dimensions, members, calculated members, custom rollups, and calculated cells.  
  
-   The order in which [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] calculates custom members, calculated members, custom rollup, and calculated cells.  
  
 The member with the highest solve order takes precedence.  
  
> [!NOTE]  
>  The exception to this precedence is the Aggregate function. Calculated members with the Aggregate function have a lower solve order than any intersecting calculated measure.  
  
## Solve Order Values and Precedence  
 Solve order values can range from -8181 to 65535. In this range, some solve order values correspond to specific kinds of calculations, as shown in the following table.  
  
|Calculation|Solve Order|  
|-----------------|-----------------|  
|Custom member formulas|-5119|  
|Unary operators|-5119|  
|Visual totals calculation|-4096|  
|All other calculations (if not otherwise specified)|0|  
  
 It is highly recommended that you use only positive integers when setting solve order values. If you assign values that are lower than the solve order values shown in the previous table, the calculation pass can become unpredictable. For example, the calculation for a calculated member receives a solve order value that is lower than the default custom rollup formula value of -5119. Such a low solve order value causes the calculated members to be calculated before the custom rollup formulas, and can produce incorrect results.  
  
### Creating and Changing Solve Order  
 In Cube Designer, on the **Calculations Pane**, you can change the solve order for calculated members and calculated cells by changing the order of the calculations.  
  
 In MDX, you can use the **SOLVE_ORDER** keyword to create or change calculated members and calculated cells.  
  
## Solve Order Examples  
 To illustrate the potential complexities of solve order, the following series of MDX queries starts with two queries that each individually have no solve order issues. These two queries are then combined into a query that requires solve order.  
  
> [!NOTE]  
>  You can run these MDX queries against the Adventure Works sample multidimensional database. You can download the [AdventureWorks Multidimensional Models SQL Server 2012](http://msftdbprodsamples.codeplex.com/releases/view/55330) sample from the codeplex site.  
  
### Query 1-Differences in Income and Expenses  
 For the first MDX query, calculate the difference in sales and costs for each year by constructing a simple MDX query similar to the following example:  
  
```  
WITH   
MEMBER  
[Date].[Calendar].[Year Difference] AS ([Date].[Calendar].[Calendar Year].&[2008] - [Date].[Calendar].[Calendar Year].&[2007] )  
SELECT   
{[Measures].[Internet Sales Amount], [Measures].[Internet Total Product Cost] }  
ON COLUMNS ,  
{ [Date].[Calendar].[Calendar Year].&[2007], [Date].[Calendar].[Calendar Year].&[2008], [Date].[Year Difference] }  
ON ROWS  
FROM [Adventure Works]  
```  
  
 In this query, there is only one calculated member, `Year Difference`. Because there is only one calculated member, solve order is not an issue, as long as the cube does not use any calculated members.  
  
 This MDX query produces a result set similar to the following table.  
  
||Internet Sales Amount|Internet Total Product Cost|  
|-|---------------------------|---------------------------------|  
|**CY 2007**|$9,791,060.30|$5,718,327.17|  
|**CY 2008**|$9,770,899.74|$5,721,205.24|  
|**Year Difference**|($20,160.56)|$2,878.06|  
  
### Query 2-Percentage of Income after Expenses  
 For the second query, calculate the percentage of income after expenses for each year using the following MDX query:  
  
```  
WITH   
MEMBER  
[Measures].[Profit Margin] AS   
([Measures].[Internet Sales Amount] - [Measures].[Internet Total Product Cost] ) / [Measures].[Internet Sales Amount] ,  
FORMAT_STRING="Percent"  
SELECT   
{[Measures].[Internet Sales Amount], [Measures].[Internet Total Product Cost], [Measures].[Profit Margin] }  
ON COLUMNS ,  
{ [Date].[Calendar].[Calendar Year].&[2007], [Date].[Calendar].[Calendar Year].&[2008] }  
ON ROWS  
FROM [Adventure Works]  
```  
  
 This MDX query, like the previous one, has only a single calculated member, `Profit Margin`, and therefore does not have any solve order complications.  
  
 This MDX query produces a slightly different result set, similar to the following table.  
  
||Internet Sales Amount|Internet Total Product Cost|Profit Margin|  
|-|---------------------------|---------------------------------|-------------------|  
|**CY 2007**|$9,791,060.30|$5,718,327.17|41.60%|  
|**CY 2008**|$9,770,899.74|$5,721,205.24|41.45%|  
  
 The difference in result sets between the first query and the second query comes from the difference in placement of the calculated member. In the first query, the calculated member is part of the ROWS axis, not the COLUMNS axis shown in the second query. This difference in placement becomes important in the next query, which combines the two calculated members in a single MDX query.  
  
### Query 3-Combined Year Difference and Net Income Calculations  
 In this final query combining both of the previous examples into a single MDX query, solve order becomes important because of the calculations on both columns and rows. To make sure that the calculations occur in the correct sequence, define the sequence in which the calculations occur by using the **SOLVE_ORDER** keyword.  
  
 The **SOLVE_ORDER** keyword specifies the solve order of calculated members in an MDX query or a **CREATE MEMBER** command. The integer values used with the **SOLVE_ORDER** keyword are relative, do not need to start at zero, and do not need to be consecutive. The value simply tells MDX to calculate a member based on values derived from calculating members with a higher value. If a calculated member is defined without the **SOLVE_ORDER** keyword, the default value of that calculated member is zero.  
  
 For example, if you combine the calculations used in the first two example queries, the two calculated members, `Year Difference` and `Profit Margin`, intersect at a single cell in the result dataset of the MDX query example. The only way to determine how [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] will evaluate this cell is by the solve order. The formulas that are used to construct this cell will produce different results depending upon the solve order of the two calculated members.  
  
 First, try combining the calculations used in the first two queries in the following MDX query:  
  
```  
WITH   
MEMBER  
[Date].[Calendar].[Year Difference] AS ([Date].[Calendar].[Calendar Year].&[2008] - [Date].[Calendar].[Calendar Year].&[2007] ) ,  
SOLVE_ORDER = 1  
MEMBER  
[Measures].[Profit Margin] AS   
( [Measures].[Internet Sales Amount] - [Measures].[Internet Total Product Cost] ) / [Measures].[Internet Sales Amount] ,  
FORMAT_STRING="Percent" ,  
SOLVE_ORDER = 2  
SELECT   
{[Measures].[Internet Sales Amount], [Measures].[Internet Total Product Cost], [Measures].[Profit Margin] }  
ON COLUMNS ,  
{ [Date].[Calendar].[Calendar Year].&[2007], [Date].[Calendar].[Calendar Year].&[2008], [Date].[Year Difference] }  
ON ROWS  
FROM [Adventure Works]  
```  
  
 In this combined MDX query example, `Profit Margin` has the highest solve order, so it takes precedence when the two expressions interact. [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] evaluates the cell in question by using the `Profit Margin` formula. The results of this nested calculation, as shown in the following table.  
  
||Internet Sales Amount|Internet Total Product Cost|Profit Margin|  
|-|---------------------------|---------------------------------|-------------------|  
|**CY 2007**|$9,791,060.30|$5,718,327.17|41.60%|  
|**CY 2008**|$9,770,899.74|$5,721,205.24|41.45%|  
|**Year Difference**|($20,160.56)|$2,878.06|114.28%|  
  
 The result in the shared cell is based on the formula for `Profit Margin`. That is, [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] calculates the result in the shared cell with the `Year Difference` data, producing the following formula (the result is rounded for clarity):  
  
```  
((9,770,899.74 - 9,791,060.30) - (5,721,205.24 - 5,718,327.17)) / (9,770,899.74 - 9,791,060.30) = 1.14275744   
```  
  
 or  
  
```  
(23,038.63) / (20,160.56) = 114.28%  
```  
  
 This is clearly incorrect. However, [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] calculates the result in the shared cell differently if you switch the solve orders for the calculated members in the MDX query. The following combined MDX query reverses the solve order for the calculated members:  
  
```  
WITH   
MEMBER  
[Date].[Calendar].[Year Difference] AS ([Date].[Calendar].[Calendar Year].&[2008] - [Date].[Calendar].[Calendar Year].&[2007] ) ,  
SOLVE_ORDER = 2  
MEMBER  
[Measures].[Profit Margin] AS   
( [Measures].[Internet Sales Amount] - [Measures].[Internet Total Product Cost] ) / [Measures].[Internet Sales Amount] ,  
FORMAT_STRING="Percent" ,  
SOLVE_ORDER = 1  
SELECT   
{[Measures].[Internet Sales Amount], [Measures].[Internet Total Product Cost], [Measures].[Profit Margin] }  
ON COLUMNS ,  
{ [Date].[Calendar].[Calendar Year].&[2007], [Date].[Calendar].[Calendar Year].&[2008], [Date].[Year Difference] }  
ON ROWS  
FROM [Adventure Works]  
```  
  
 As the order of the calculated members has been switched, [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] uses the `Year Difference` formula to evaluate the cell, as shown in the following table.  
  
||Internet Sales Amount|Internet Total Product Cost|Profit Margin|  
|-|---------------------------|---------------------------------|-------------------|  
|**CY 2007**|$9,791,060.30|$5,718,327.17|41.60%|  
|**CY 2008**|$9,770,899.74|$5,721,205.24|41.45%|  
|**Year Difference**|($20,160.56)|$2,878.06|(0.15%)|  
  
 Because this query uses the `Year Difference` formula with the `Profit Margin` data, the formula for the shared cell resembles the following calculation:  
  
```  
(($9,770,899.74 - 5,721,205.24) / $9,770,899.74) - ((9,791,060.30 - 5,718,327.17) / 9,791,060.30) = -0.15   
```  
  
 Or  
  
```  
0.4145 - 0.4160= -0.15  
```  
  
## Additional Considerations  
 Solve order can be a very complex issue to deal with, especially in cubes with a high number of dimensions involving calculated member, custom rollup formulas, or calculated cells. When [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] evaluates an MDX query, [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] takes into account the solve order values for everything involved within a given pass, including the dimensions of the cube specified in the MDX query.  
  
## See Also  
 [CalculationCurrentPass &#40;MDX&#41;](../../../mdx/calculationcurrentpass-mdx.md)   
 [CalculationPassValue &#40;MDX&#41;](../../../mdx/calculationpassvalue-mdx.md)   
 [CREATE MEMBER Statement &#40;MDX&#41;](../../../mdx/mdx-data-definition-create-member.md)   
 [Manipulating Data &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/mdx-data-manipulation-manipulating-data.md)  
  
  
