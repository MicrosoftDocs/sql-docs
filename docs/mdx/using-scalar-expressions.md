---
title: "Using Scalar Expressions | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "scalar expressions"
  - "expressions [MDX], scalar"
ms.assetid: 4678b675-8fbd-4e5b-a519-d4cd1bb8c46a
caps.latest.revision: 25
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Using Scalar Expressions
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  In Multidimensional Expressions (MDX), a scalar expression is an element of MDX syntax that, when evaluated, returns a single value within the context of evaluation.  
  
 Scalar expressions include string, numeric, and date expressions in MDX.  
  
 Scalar expressions are typically used in calculated member definitions, as calculated members must return a scalar value. The following query shows examples of calculated members on the Measures dimension that use different types of scalar expression:  
  
 `WITH`  
  
 `MEMBER MEASURES.NumericValue AS 10`  
  
 `MEMBER MEASURES.NumericExpression AS 10 + 10`  
  
 `MEMBER MEASURES.NumericExpressionBasedOnMeasure AS [Measures].[Internet Sales Amount] + 10`  
  
 `MEMBER MEASURES.StringValue AS "10"`  
  
 `MEMBER MEASURES.ConcatenatedString AS "10" + "10"`  
  
 `MEMBER MEASURES.StringFunction AS MEASURES.CURRENTMEMBER.NAME`  
  
 `MEMBER MEASURES.TodaysDate AS NOW()`  
  
 `SELECT`  
  
 `{MEASURES.NumericValue,MEASURES.NumericExpression,MEASURES.NumericExpressionBasedOnMeasure,`  
  
 `MEASURES.StringValue, MEASURES.ConcatenatedString, MEASURES.StringFunction, MEASURES.TodaysDate}`  
  
 `ON COLUMNS`  
  
 `FROM [Adventure Works]`  
  
 The only data type that a measure, calculated or otherwise, can return is the OLE Variant type. Therefore, sometimes you might need to cast a measure value to a particular type to receive the behavior you expect. The following query shows an example of this:  
  
```  
WITH  
//Two calculated measures that return strings  
MEMBER MEASURES.NumericString1 AS "10"  
MEMBER MEASURES.NumericString2 AS "10"  
//In this case, the + operator acts to concatenate the strings  
MEMBER MEASURES.Concatenation AS MEASURES.NumericString1 + MEASURES.NumericString2  
//Casting one value to an integer with the CINT function causes the second measure  
//to be treated as an integer too, so that the + operator now acts to add the values  
MEMBER MEASURES.Addition AS CINT(MEASURES.NumericString1) + MEASURES.NumericString2  
SELECT  
{MEASURES.NumericString1,MEASURES.NumericString2,MEASURES.Concatenation,MEASURES.Addition }  
ON COLUMNS  
FROM [Adventure Works]  
```  
  
## See Also  
 [Expressions &#40;MDX&#41;](../mdx/expressions-mdx.md)  
  
  
