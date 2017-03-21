---
title: "RangeMin (DMX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "RangeMin"
dev_langs: 
  - "DMX"
helpviewer_keywords: 
  - "RangeMin function"
ms.assetid: 64159bbe-7016-4f67-91d9-5c5f6ceb6c25
caps.latest.revision: 30
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# RangeMin (DMX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the lower end of the predicted bucket that is discovered for a discretized column.  
  
## Syntax  
  
```  
  
RangeMin(<scalar column reference>)  
```  
  
## Applies To  
 Scalar columns.  
  
## Return Type  
 A scalar value.  
  
## Remarks  
 The **RangeMin** function can be used in [SELECT DISTINCT FROM &#60;model &#62; &#40;DMX&#41;](../dmx/select-distinct-from-model-dmx.md) queries. When used with this type of query, the scalar column reference can contain continuous or discrete columns that are either predictable or input.  
  
 When used with [SELECT FROM &#60;model&#62; PREDICTION JOIN &#40;DMX&#41;](../dmx/select-from-model-prediction-join-dmx.md), the **RangeMin**, **RangeMid**, and **RangeMax** functions return the actual boundary values of the specified bucket. For example, if you perform a prediction on a discretized column, the query returns the predicted bucket number in the discretized column. The **RangeMin**, **RangeMid**, and **RangeMax** functions describe the bucket that the prediction specifies. When the **RangeMin** function is used with a PREDICTION JOIN statement, the scalar column reference can only contain discrete, predictable columns.  
  
## Examples  
 The following example returns the minimum, maximum, and average values for the Yearly Income continuous column in the Decision Tree mining model.  
  
```  
SELECT DISTINCT   
    RangeMin([Yearly Income]) AS [Bucket Minimum],  
    RangeMid([Yearly Income]) AS [Bucket Average],   
    RangeMax([Yearly Income]) AS [Bucket Maximum]  
FROM [TM Decision Tree]  
```  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Function Reference](../dmx/data-mining-extensions-dmx-function-reference.md)   
 [Functions &#40;DMX&#41;](../dmx/functions-dmx.md)   
 [General Prediction Functions &#40;DMX&#41;](../dmx/general-prediction-functions-dmx.md)   
 [RangeMax &#40;DMX&#41;](../dmx/rangemax-dmx.md)   
 [RangeMid &#40;DMX&#41;](../dmx/rangemid-dmx.md)  
  
  
