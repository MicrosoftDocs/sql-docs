---
title: "CASE Statement (MDX) | Microsoft Docs"
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
# CASE Statement (MDX)


  Lets you conditionally return specific values from multiple comparisons. There are two types of case statements:  
  
-   A simple case statement that compares an expression to a set of simple expressions to return specific values.  
  
-   A searched case statement that evaluates a set of Boolean expressions to return specific values.  
  
## Syntax  
  
```  
  
Simple Case Statement  
CASE [input_expression]  
WHEN when_expression THEN when_true_result_expression  
[...n]  
[ELSE else_result_expression]  
END  
  
Search Case Statement  
CASE   
WHEN Boolean_expression THEN when_true_result_expression  
[...n]  
[ELSE else_result_expression]  
END  
```  
  
## Arguments  
 *input_expression*  
 A Multidimensional Expressions (MDX) expression that resolves to a scalar value.  
  
 *when_expression*  
 A specified scalar value against which the *input_expression* is evaluated, which when evaluated to true, returns the scalar value of the *else_result_expression*.  
  
 *when_true_result_expression*  
 The scalar value returned when the WHEN clause evaluates to true.  
  
 *else_result_expression*  
 The scalar value returned when none of the WHEN clauses evaluate to true.  
  
 *Boolean_expression*  
 An MDX expression that evaluates to a scalar value.  
  
## Remarks  
 If there is no ELSE clause, and all WHEN clauses evaluate to false, the result is an empty cell.  
  
## Simple Case Expression  
 MDX evaluates a simple case expression by resolving the *input_expression* to a scalar value. This scalar value is then compared to the scalar value of the *when_expression*. If the two scalar values match, the CASE statement returns the value of the *when_true_expression*. If the two scalar values do not match, the next WHEN clause is evaluated. If all of the WHEN clauses evaluate to false, the value of *else_result_expression* from the ELSE clause, if any, is returned.  
  
 In the following example, the Reseller Order Count measure is evaluated against several WHEN clauses and returns a result based on the value of the Reseller Order Count measure for each year. For Reseller Order Count values that do not match a scalar value specified in a *when_expression* in a WHEN clause, the scalar value of the *else_result_expression* is returned.  
  
```  
WITH MEMBER [Measures].x AS   
CASE [Measures].[Reseller Order Count]  
   WHEN 0 THEN 'NONE'  
   WHEN 1 THEN 'SMALL'  
   WHEN 2 THEN 'SMALL'  
   WHEN 3 THEN 'MEDIUM'  
   WHEN 4 THEN 'MEDIUM'  
   WHEN 5 THEN 'LARGE'  
   WHEN 6 THEN 'LARGE'  
      ELSE 'VERY LARGE'  
END  
SELECT Calendar.[Calendar Year] on 0  
, NON EMPTY [Geography].[Postal Code].Members ON 1  
FROM [Adventure Works]  
WHERE [Measures].x  
```  
  
## Searched Case Expression  
 To use the case expression to perform more complex evaluations, use the searched case expression. This variation of the search expression allows you to evaluate whether an input expression is within a range of values. MDX evaluates the WHEN clauses in the order that these clauses appear in the CASE statement.  
  
 In the following example, the Reseller Order Count measure is evaluated against the specified *Boolean_expression* for each of several WHEN clauses. A result is returned based on the value of the Reseller Order Count measure for each year. Because WHEN clauses are evaluated in the order they appear, all values larger than 6 can easily be assigned the value of "VERY LARGE" without having to specify each value explicitly. For Reseller Order Count values that are not specified in a WHEN clause, the scalar value of the *else_result_expression* is returned.  
  
```  
WITH MEMBER [Measures].x AS   
CASE   
   WHEN [Measures].[Reseller Order Count] > 6 THEN 'VERY LARGE'  
   WHEN [Measures].[Reseller Order Count] > 4 THEN 'LARGE'  
   WHEN [Measures].[Reseller Order Count] > 2 THEN 'MEDIUM'  
   WHEN [Measures].[Reseller Order Count] > 0 THEN 'SMALL'  
   ELSE "NONE"  
END  
SELECT Calendar.[Calendar Year] on 0,  
NON EMPTY [Geography].[Postal Code].Members on 1  
FROM [Adventure Works]  
WHERE [Measures].x  
```  
  
## See Also  
 [MDX Scripting Statements &#40;MDX&#41;](../mdx/mdx-scripting-statements-mdx.md)  
  
  
