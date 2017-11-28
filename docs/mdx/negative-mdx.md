---
title: "- (Negative) (MDX) | Microsoft Docs"
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
f1_keywords: 
  - "-"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "- (negative)"
  - "negative operator (-)"
ms.assetid: 7fbe83ed-aaa5-41f6-a17c-bfc2e1bffa77
caps.latest.revision: 39
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
ms.workload: "Inactive"
---
# - (Negative) (MDX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Performs a unary operation that returns the negative value of a numeric expression.  
  
## Syntax  
  
```  
  
- Numeric_Expression  
```  
  
#### Parameters  
 *Numeric_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a numeric value.  
  
## Return Value  
 A negative value that has the data type of the specified parameter.  
  
## Examples  
 The following example demonstrates the use of this operator.  
  
```  
-- This member creates a negative version of the  
-- Reseller Freight Cost.  
WITH MEMBER   
   Measures.[Resell Cost as Negative]   
   AS -Measures.[Reseller Freight Cost]  
SELECT   
   [Date].[Calendar Month of Year].Children ON COLUMNS,  
   [Product].[Product Categories].Children ON ROWS  
FROM  
    [Adventure Works]  
WHERE  
    {[Measures].[Resell Cost as Negative]}  
```  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
