---
title: "StrToValue (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "STRTOVALUE"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "StrToValue function"
ms.assetid: 118a9c4f-74a3-48d5-a4f4-318664bc51bc
caps.latest.revision: 30
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# StrToValue (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the numeric value specified by a Multidimensional Expressions (MDX)–formatted string.  
  
## Syntax  
  
```  
  
StrToValue(MDX_Expression [,CONSTRAINED] )   
```  
  
## Arguments  
 *MDX_Expression*  
 A valid string expression that resolves, directly or indirectly, to a single cell.  
  
## Remarks  
 The **StrToValue** function returns the numeric value specified by the MDX expression. The **StrToValue** function is typically used with user-defined functions to return an MDX expression from an external function back to an MDX statement that can be resolved to a single cell.  
  
-   When the CONSTRAINED flag is used, the MDX expression must contain only a scalar value. The CONSTRAINED flag is used to reduce the risk of injection attacks via the specified string. If a MDX expression is provided that is not directly resolvable to a scalar value, the following error appears: "The restrictions imposed by the CONSTRAINED flag in the STRTOVALUE function were violated."  
  
-   When the CONSTRAINED flag is not used, the specified MDX expression can be as complex as desired as long as it resolves to a valid Multidimensional Expressions (MDX) expression that returns a single cell.  
  
> [!NOTE]  
>  Returning the result of an MDX expression as a numeric value can be useful if the value is stored as text and you want to perform arithmetic operations on the returned values.  
  
## Example  
 The following example uses the **StrToValue** function to return the weight of each bicycle as a value.  
  
```  
WITH MEMBER Measures.x AS   
StrToValue   
   ([Product].[Product].CurrentMember.Properties ('Weight')  
   ,CONSTRAINED  
   )  
SELECT Measures.x ON 0  
,[Product].[Product].[Product].Members ON 1  
FROM [Adventure Works]  
WHERE [Product].[Product Categories].[Bikes]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
