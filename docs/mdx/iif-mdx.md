---
title: "IIf (MDX) | Microsoft Docs"
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
# IIf (MDX)


  Evaluates different branch expressions depending on whether a Boolean condition is true or false.  
  
## Syntax  
  
```  
  
IIf(Logical_Expression, Expression1 [HINT <hints>], Expression2 [HINT <hints>])  
```  
  
## Arguments  
 The IIf function takes three arguments: iif(\<condition>, \<then branch>, \<else branch>).  
  
 *Logical_Expression*  
 A condition that evaluates to **true** (1) or **false** (0). It must be a valid Multidimensional Expressions (MDX) logical expression.  
  
 *Expression1 Hint [Eager|Strict|Lazy]]*  
 Used when the logical expression evaluates to **true**. Expression1 must be a valid Multidimensional Expressions (MDX) expression.  
  
 *Expression2 Hint [Eager|Strict|Lazy]]*  
 Used when the logical expression evaluates to **false**. Expression2 must be valid Multidimensional Expressions (MDX) expression.  
  
## Remarks  
 The condition specified by the logical expression evaluates to **false** when the value of this expression is zero. Any other value evaluates to **true**.  
  
 When the condition is **true**, the **IIf** function returns the first expression. Otherwise, the function returns the second expression.  
  
 The specified expressions can return values or MDX objects. Furthermore, the specified expressions need not match in type.  
  
 The **IIf** function is not recommended for creating a set of members based on search criteria. Instead, use the [Filter](../mdx/filter-mdx.md) function to evaluate each member in a specified set against a logical expression and return a subset of members.  
  
> [!NOTE]  
>  If either expression evaluates to NULL, the result set will be NULL when that condition is met.  
  
 Hint is an optional modifier that determines how and when the expression is evaluated. It allows you to override the default query plan by specifying how the expression is evaluated.  
  
-   EAGER evaluates the expression over the original IIF subspace.  
  
-   STRICT evaluates the expression only in the restricted subspace that is created by the logical condition expression.  
  
-   LAZY evaluates the expression in cell-by-cell mode.  
  
 While EAGER and STRICT only apply to the then-else branches of IIF, LAZY applies to all MDX expressions. Any MDX expression can be followed by HINT LAZY which will evaluate that expression in cell-by-cell mode.  
  
 EAGER and STRICT are mutually exclusive in the hint; they can be used in the same IIF(,,) over different expressions.  
  
 For more information, see [IIF Function Query Hints in SQL Server Analysis Services 2008](https://go.microsoft.com/fwlink/?LinkId=269540) and [Execution Plans and Plan Hints for MDX IIF Function and CASE Statement](https://go.microsoft.com/fwlink/?LinkId=269565).  
  
## Examples  
 The following query shows a simple use of **IIF** inside a calculated measure to return one of two different string values when the measure Internet Sales Amount is greater or less than $10000:  
  
 `WITH MEMBER MEASURES.IIFDEMO AS`  
  
 `IIF([Measures].[Internet Sales Amount]>10000`  
  
 `, "Sales Are High", "Sales Are Low")`  
  
 `SELECT {[Measures].[Internet Sales Amount],MEASURES.IIFDEMO} ON 0,`  
  
 `[Date].[Date].[Date].MEMBERS ON 1`  
  
 `FROM [Adventure Works]`  
  
 A very common use of IIF is to handle 'division by zero'  errors within calculated measures, as in the following example:  
  
 `WITH`  
  
 `//Returns 1.#INF when the previous period contains no value`  
  
 `//but the current period does`  
  
 `MEMBER MEASURES.[Previous Period Growth With Errors] AS`  
  
 `([Measures].[Internet Sales Amount]-([Measures].[Internet Sales Amount], [Date].[Date].CURRENTMEMBER.PREVMEMBER))`  
  
 `/`  
  
 `([Measures].[Internet Sales Amount], [Date].[Date].CURRENTMEMBER.PREVMEMBER)`  
  
 `,FORMAT_STRING='PERCENT'`  
  
 `//Traps division by zero and returns null when the previous period contains`  
  
 `//no value but the current period does`  
  
 `MEMBER MEASURES.[Previous Period Growth] AS`  
  
 `IIF(([Measures].[Internet Sales Amount], [Date].[Date].CURRENTMEMBER.PREVMEMBER)=0,`  
  
 `NULL,`  
  
 `([Measures].[Internet Sales Amount]-([Measures].[Internet Sales Amount], [Date].[Date].CURRENTMEMBER.PREVMEMBER))`  
  
 `/`  
  
 `([Measures].[Internet Sales Amount], [Date].[Date].CURRENTMEMBER.PREVMEMBER)`  
  
 `),FORMAT_STRING='PERCENT'`  
  
 `SELECT {[Measures].[Internet Sales Amount],MEASURES.[Previous Period Growth With Errors], MEASURES.[Previous Period Growth]} ON 0,`  
  
 `DESCENDANTS(`  
  
 `[Date].[Calendar].[Calendar Year].&[2004],`  
  
 `[Date].[Calendar].[Date])`  
  
 `ON 1`  
  
 `FROM [Adventure Works]`  
  
 `WHERE([Product].[Product Categories].[Subcategory].&[26])`  
  
 The following is an example of **IIF** returning one of two sets inside the Generate function to create a complex set of tuples on Rows:  
  
 `SELECT {[Measures].[Internet Sales Amount]} ON 0,`  
  
 `//If Internet Sales Amount is zero or null`  
  
 `//returns the current year and the All Customers member`  
  
 `//else returns the current year broken down by Country`  
  
 `GENERATE(`  
  
 `[Date].[Calendar Year].[Calendar Year].MEMBERS`  
  
 `, IIF([Measures].[Internet Sales Amount]=0,`  
  
 `{([Date].[Calendar Year].CURRENTMEMBER, [Customer].[Country].[All Customers])}`  
  
 `, {{[Date].[Calendar Year].CURRENTMEMBER} * [Customer].[Country].[Country].MEMBERS}`  
  
 `))`  
  
 `ON 1`  
  
 `FROM [Adventure Works]`  
  
 `WHERE([Product].[Product Categories].[Subcategory].&[26])`  
  
 Lastly, this example shows how to use Plan Hints:  
  
 `WITH MEMBER MEASURES.X AS`  
  
 `IIF(`  
  
 `[Measures].[Internet Sales Amount]=0`  
  
 `, NULL`  
  
 `, (1/[Measures].[Internet Sales Amount]) HINT EAGER)`  
  
 `SELECT {[Measures].x} ON 0,`  
  
 `[Customer].[Customer Geography].[Country].MEMBERS ON 1`  
  
 `FROM [Adventure Works]`  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
