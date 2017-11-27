---
title: "CREATE MEASURE statement (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "analysis-services"
ms.prod_service: "analysis-services"
ms.service: ""
ms.component: ""
ms.reviewer: ""
ms.suite: "pro-bi"
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
ms.assetid: f264ba96-cbbe-488b-8ac9-b3056a6e997b
caps.latest.revision: 5
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
ms.workload: "Inactive"
---
# MDX Data Definition - CREATE MEASURE
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

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
  
 The CREATE MEASURE statement can only be used inside of a MDX script definition; see [MdxScript Element &#40;ASSL&#41;](../analysis-services/scripting/objects/mdxscript-element-assl.md).  
  
 You can also define a calculated member for use by a single query. To define a calculated member that is limited to a single query, you use the WITH clause in the SELECT statement. For more information, see [Building Measures in MDX](../analysis-services/multidimensional-models/mdx/mdx-building-measures.md).  
  
## See Also  
 [MDX Data Definition Statements &#40;MDX&#41;](../mdx/mdx-data-definition-statements-mdx.md)  
  
  
