---
title: "CREATE CELL CALCULATION Statement (MDX) | Microsoft Docs"
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
# MDX Data Definition - CREATE CELL CALCULATION


  Creates a calculation that evaluates a Multidimensional Expressions (MDX) expression over a specified set of tuples within a cube.  
  
## Syntax  
  
```  
  
[WITH <CELL CALCULATION clause> Calculation_Name  
   [,WITH <CELL CALCULATION clause> Calculation_Name...n]  
CREATE CELL CALCULATION CURRENTCUBE | Cube_Name.Calculation_Name   
  
<CELL CALCULATION clause> ::=  
   FOR Set_Expression AS 'MDX_Expression'   
      [ [ CONDITION = 'Logical_Expression' ]   
    | [ DISABLED = { TRUE | FALSE } ]   
    | [ DESCRIPTION =String ]   
    | [ CALCULATION_PASS_NUMBER = Integer]   
    | [ CALCULATION_PASS_DEPTH = Integer]   
    | [ SOLVE_ORDER = Integer]   
    | [ Calculation_Name= Scalar_Expression ], ...n]  
```  
  
## Arguments  
 *Cube_Name*  
 A valid string that provides a cube name.  
  
 *Calculation_Name*  
 A valid string that provides a cell calculation name.  
  
 *Set_Expression*  
 A valid MDX expression that returns a set.  
  
 *String*  
 A valid string value.  
  
 *MDX_Expression*  
 A valid MDX expression.  
  
 *Logical_Expression*  
 A valid MDX logical expression.  
  
 *Integer*  
 A valid integer value.  
  
 *Calculation_Name*  
 A valid string that provides the name of a cell calculation property.  
  
 *Scalar_Expression*  
 A valid MDX scalar expression.  
  
## Remarks  
 By using calculated cells, the client application can specify a rollup value for a particular set of cells, instead of for an entire set of cells as in the case of a custom rollup formula or a calculated member. For example, it is possible to specify that any cell in the set defined by `{[Canada],[Time].[2000]}` can contain a value that is defined by a formula. Any other cells that are not contained within this set are computed normally.  
  
> [!NOTE]  
>  The Backus-Naur Form (BNF) of `{*(<comment> | <whitespace> | <newline>)}` will be parsed as `{*}` for backwards compatibility.  
  
## See Also  
 [Creating Session-Scoped Calculated Cells](../analysis-services/multidimensional-models/mdx/mdx-cell-calculations-session-scoped-calculated-cells.md)   
 [Creating Query-Scoped Cell Calculations &#40;MDX&#41;](../analysis-services/multidimensional-models/mdx/mdx-cell-calculations-query-scoped-cell-calculations.md)   
 [Building Cell Calculations in MDX &#40;MDX&#41;](../analysis-services/multidimensional-models/mdx/mdx-cell-calculations-build-cell-calculations.md)   
 [Using Cell Properties &#40;MDX&#41;](../analysis-services/multidimensional-models/mdx/mdx-cell-properties-using-cell-properties.md)   
 [FORMAT_STRING Contents &#40;MDX&#41;](../analysis-services/multidimensional-models/mdx/mdx-cell-properties-format-string-contents.md)   
 [FORE_COLOR and BACK_COLOR Contents &#40;MDX&#41;](../analysis-services/multidimensional-models/mdx/mdx-cell-properties-fore-color-and-back-color-contents.md)   
 [MDX Data Definition Statements &#40;MDX&#41;](../mdx/mdx-data-definition-statements-mdx.md)  
  
  
