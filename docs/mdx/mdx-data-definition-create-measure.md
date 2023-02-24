---
description: "MDX Data Definition - CREATE MEASURE"
title: "CREATE MEASURE statement (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# MDX Data Definition - CREATE MEASURE


  Creates a measure in a Tabular Model.  
  
## Syntax  
  
```  
  
CREATE MEASURE Table_Name[Measure_Name] = DAX_Expression  
[; CREATE MEASURE ...n]  
  
```  
  
## Arguments  
 *Table_Name*  
 A valid string literal that provides the name of the table where the measure will be created.  
  
 *Measure_Name*  
 A valid string literal that provides a measure name.  
  
 *DAX_Expression*  
 A valid DAX expression that returns a single scalar value.  
  
## Remarks  
 The *Measure_Name*  must be enclosed in square parenthesis.  
  
 The CREATE MEASURE statement can only be used inside of a MDX script definition; see [MdxScript Element &#40;ASSL&#41;](/analysis-services/assl/objects/mdxscript-element-assl).  
  
 You can also define a calculated member for use by a single query. To define a calculated member that is limited to a single query, you use the WITH clause in the SELECT statement. For more information, see [Building Measures in MDX](/analysis-services/multidimensional-models/mdx/mdx-building-measures).  
  
## See Also  
 [MDX Data Definition Statements &#40;MDX&#41;](../mdx/mdx-data-definition-statements-mdx.md)  
  
