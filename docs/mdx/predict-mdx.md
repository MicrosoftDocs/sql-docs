---
title: "Predict (MDX) | Microsoft Docs"
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
# Predict (MDX)


    
> [!CAUTION]  
>  This function is in the process of being removed due to internal inconsistencies.  
>   
>  Review the example section for a workaround using a DMX expression.  
  
 Returns a value of a numeric expression evaluated over a data mining model.  
  
## Syntax  
  
```  
  
Predict(Mining_Model_Name,String_Expression)   
```  
  
## Arguments  
 *Mining_Model_Name*  
 A valid string expression that represents the name of a mining model.  
  
 *String_Expression*  
 A valid string expression that evaluates to a valid DMX expression for the specified mining model.  
  
## Remarks  
 The **Predict** function evaluates the specified string expression within the context of the specified mining model.  
  
 Data mining syntax and functions are documented in Data Mining Expressions (DMX) reference.  
  
## Example  
 The following example predicts name of the cluster and the distance from it of a particular customer using the Customer Clusters mining model:  
  
```  
WITH MEMBER MEASURES.CLNAME AS   
PREDICT("Customer Clusters", "Cluster()")  
MEMBER MEASURES.CLDISTANCE AS   
PREDICT("Customer Clusters", "ClusterDistance(Cluster())")  
SELECT {MEASURES.CLNAME, MEASURES.CLDISTANCE} ON 0   
FROM [Adventure Works]  
WHERE([Customer].[Customer Geography].[Customer].&[12012])  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
