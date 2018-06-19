---
title: "LinRegPoint (MDX) | Microsoft Docs"
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
# LinRegPoint (MDX)


  Calculates the linear regression of a set, and returns the value of the *y-intercept* in the regression line, y = ax + b for a particular value of x.  
  
## Syntax  
  
```  
  
LinRegPoint(Slice_Expression_x, Set_Expression, Numeric_Expression_y [ ,Numeric_Expression_x ] )  
```  
  
## Arguments  
 *Slice_Expression_x*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number that represents values for the slicer axis.  
  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Numeric_Expression_y*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number that represents values for the y-axis.  
  
 *Numeric_Expression_x*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number that represents values for the x-axis.  
  
## Remarks  
 Linear regression, that uses the least-squares method, calculates the equation of a regression line (that is, the best-fit line for a series of points). The regression line has the following equation, where a is the slope and b is the intercept:  
  
 y = ax+b  
  
 The **LinRegPoint** function evaluates the specified set against the second numeric expression to obtain the values for the y-axis. The function then evaluates the specified set against the third numeric expression, if specified, to get the values for the x-axis. If the third numeric expression is not specified, the function uses the current context of the cells in the specified set as the values for the x-axis. Not specifying the x-axis argument is frequently used with the Time dimension.  
  
 Once the linear regression line has been calculated, the value of the equation is calculated for the first numeric expression and then returned.  
  
> [!NOTE]  
>  The **LinRegPoint** function ignores empty cells or cells that contain text. However, the function includes cells with values of zero.  
  
## Example  
 The following example returns the predicted value of Unit Sales over the past ten periods based on the statistical relationship between Unit Sales and Store Sales.  
  
```  
LinRegPoint([Measures].[Unit Sales],LastPeriods(10),[Measures].[Unit Sales],[Measures].[Store Sales])  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
