---
title: "Predict (MDX) | Microsoft Docs"
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
  - "PREDICT"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "Predict function"
ms.assetid: a82f3edd-249b-4559-98d3-6e10d81a095d
caps.latest.revision: 36
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Predict (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

    
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
  
  
