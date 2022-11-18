---
description: "Working with Empty Values"
title: "Working with Empty Values | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# Working with Empty Values


  An empty value indicates that a specific member, tuple, or cell is empty. An empty cell value indicates either that the data for the specified cell cannot be found in the underlying fact table, or that the tuple for the specified cell represents a combination of members that is not applicable for the cube.  
  
> [!NOTE]  
>  Although an empty value is different from a value of zero, an empty value is typically treated as zero most of the time.  
  
 The following query illustrates the behavior of empty and zero values:  
  
```  
WITH  
//A calculated Product Category that always returns 0  
MEMBER [Product].[Category].[All Products].ReturnZero AS 0  
//Will return true for any null value  
MEMBER MEASURES.ISEMPTYDemo AS ISEMPTY([Measures].[Internet Tax Amount])  
//Will true for any null or zero value  
//To be clear: the expression 0=null always returns true in MDX  
MEMBER MEASURES.IsZero AS [Measures].[Internet Tax Amount]=0  
SELECT  
{[Measures].[Internet Tax Amount],MEASURES.ISEMPTYDemo,MEASURES.IsZero}  
ON COLUMNS,  
[Product].[Category].[Category].ALLMEMBERS  
ON ROWS  
FROM [Adventure Works]  
WHERE([Date].[Calendar].[Calendar Year].&[2001])  
```  
  
 The following information applies to empty values:  
  
-   The [IsEmpty](../mdx/isempty-mdx.md) function returns **TRUE** if and only if the cell identified by the tuple specified in the function is empty. Otherwise, the function returns **FALSE**.  
  
    > [!NOTE]  
    >  The **IsEmpty** function cannot determine whether a member expression returns a null value. To determine whether a null member is returned from an expression, use the [IS](../mdx/is-mdx.md)operator.  
  
-   When the empty cell value is an operand for any one of the numeric operators (+, -, *, /), the empty cell value is treated as zero if the other operand is a nonempty value. If both operands are empty, the numeric operator returns the empty cell value.  
  
-   When the empty cell value is an operand for the string concatenation operator (+), the empty cell value is treated as an empty string if the other operand is a nonempty value. If both operands are empty, the string concatenation operator returns the empty cell value.  
  
-   When the empty cell value is an operand for any one of the comparison operators (=. <>, >=, \<=, >, <), the empty cell value is treated as zero or an empty string, depending on whether the data type of the other operand is numeric or string, respectively. If both operands are empty, both operands are treated as zero.  
  
-   When collating numeric values, the empty cell value collates in the same place as zero. Between the empty cell value and zero, empty collates before zero.  
  
-   When collating string values, the empty cell value collates in the same place as the empty string. Between the empty cell value and the empty string, empty collates before an empty string.  
  
## Dealing with Empty Values in MDX Statements and Cubes  
 In Multidimensional Expressions (MDX) statements, you can look for empty values and then perform certain calculations on cells with valid (that is, not empty) data. Eliminating empty values when performing calculations can be important because certain calculations, such as an average, can be inaccurate if empty cell values are included.  
  
 If empty values are stored in your underlying fact table data, by default they will be converted to zeroes when the cube is processed. You can use the **Null Processing** option on a measure to control whether null facts are converted into 0, converted to an empty value, or even throws an error during processing. If you do not want empty cell values appearing in your query results, you should create queries, calculated members, or MDX Script statements that eliminate the empty values or replace them with some other value.  
  
 To remove empty rows or columns from a query, you can use the NON EMPTY statement before the axis set definition. For example, the following query only returns the Product Category Bikes because that is the only Category that was sold in the Calendar Year 2001:  
  
 `SELECT`  
  
 `{[Measures].[Internet Tax Amount]}`  
  
 `ON COLUMNS,`  
  
 `//Comment out the following line to display all the empty rows for other Categories`  
  
 `NON EMPTY`  
  
 `[Product].[Category].[Category].MEMBERS`  
  
 `ON ROWS`  
  
 `FROM [Adventure Works]`  
  
 `WHERE([Date].[Calendar].[Calendar Year].&[2001])`  
  
 More generally, to remove empty tuples from a set you can use the NonEmpty function. The following query shows two calculated measures, one of which counts the number of Product Categories and the second shows the number of Product Categories which have values for the measure [Internet Tax Amount] and the Calendar Year 2001:  
  
 `WITH`  
  
 `MEMBER MEASURES.CategoryCount AS`  
  
 `COUNT([Product].[Category].[Category].MEMBERS)`  
  
 `MEMBER MEASURES.NonEmptyCategoryCountFor2001 AS`  
  
 `COUNT(`  
  
 `NONEMPTY(`  
  
 `[Product].[Category].[Category].MEMBERS`  
  
 `,([Date].[Calendar].[Calendar Year].&[2001], [Measures].[Internet Tax Amount])`  
  
 `))`  
  
 `SELECT`  
  
 `{MEASURES.CategoryCount,MEASURES.NonEmptyCategoryCountFor2001 }`  
  
 `ON COLUMNS`  
  
 `FROM [Adventure Works]`  
  
 For more information, see [NonEmpty &#40;MDX&#41;](../mdx/nonempty-mdx.md).  
  
## Empty Values and Comparison Operators  
 When empty values are present in data, logical and comparison operators can potentially return a third result of EMPTY instead of just TRUE or FALSE. This need for three-valued logic is a source of many application errors. These tables outline the effect of introducing empty value comparisons.  
  
 This table shows the results of applying an AND operator to two Boolean operands.  
  
|AND|TRUE|EMPTY|FALSE|  
|---------|----------|-----------|-----------|  
|**TRUE**|TRUE|FALSE|FALSE|  
|**EMPTY**|FALSE|EMPTY|FALSE|  
|**FALSE**|FALSE|FALSE|FALSE|  
  
 This table shows the results of applying an OR operator to two Boolean operands.  
  
|OR|TRUE|FALSE|  
|--------|----------|-----------|  
|**TRUE**|TRUE|TRUE|  
|**EMPTY**|TRUE|TRUE|  
|**FALSE**|TRUE|FALSE|  
  
 This table shows how the NOT operator negates, or reverses, the result of a Boolean operator.  
  
|Boolean expression to which the NOT operator is applied|Evaluates to|  
|-------------------------------------------------------------|------------------|  
|TRUE|FALSE|  
|EMPTY|EMPTY|  
|FALSE|TRUE|  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)   
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)   
 [Expressions &#40;MDX&#41;](../mdx/expressions-mdx.md)  
  
  
