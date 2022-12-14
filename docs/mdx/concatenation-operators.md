---
description: "Concatenation Operators"
title: "Concatenation Operators | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# Concatenation Operators


  The concatenation operator is the plus sign (+). You can combine, or concatenate, two or more character strings into a single character string. You can also concatenate binary strings.  
  
 The following code is an example of concatenation operator that combines the product name with the product's unique name:  
  
```  
WITH MEMBER Measures.ProductName AS   
   Product.Product.CurrentMember.Name + " (" + Product.Product.CurrentMember.UniqueName + ")"  
SELECT   
   {Measures.ProductName} ON COLUMNS,  
   Product.Product.Members ON ROWS  
FROM [Adventure Works]  
```  
  
## Language Considerations  
 When the strings used in a concatenation both have the same collation, the resulting concatenated string has the same collation as the inputs. When the strings used in a concatenation have different collations, the rules of collation precedence determine the collation of the resulting concatenated string. For more information, see [Languages and Collations &#40;Analysis Services&#41;](/analysis-services/languages-and-collations-analysis-services).  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)   
 [Operators &#40;MDX Syntax&#41;](../mdx/operators-mdx-syntax.md)  
  
