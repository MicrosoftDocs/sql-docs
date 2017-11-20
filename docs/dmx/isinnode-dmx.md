---
title: "IsInNode (DMX) | Microsoft Docs"
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
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "IsInNode"
dev_langs: 
  - "DMX"
helpviewer_keywords: 
  - "IsInNode function"
ms.assetid: 47b2114f-9ad6-45cc-9498-193ad6fa5288
caps.latest.revision: 29
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
ms.workload: "Inactive"
---
# IsInNode (DMX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Indicates whether the specified node contains the current case.  
  
## Syntax  
  
```  
  
IsInNode(<NodeID>)  
```  
  
## Return Type  
 A Boolean type.  
  
## Remarks  
 **IsInNode** is only used in [SELECT FROM &#60;model&#62;.CASES &#40;DMX&#41;](../dmx/select-from-model-cases-dmx.md) and [SELECT FROM &#60;model&#62;.SAMPLE_CASES &#40;DMX&#41;](../dmx/select-from-model-sample-cases-dmx.md) queries.  
  
## Examples  
 The following example returns all the cases that were used to create the model that is associated with the node that is specified in the IsInNode function.  
  
```  
Select * from [TM Decision Tree].Cases  
WHERE IsInNode('0')  
```  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Function Reference](../dmx/data-mining-extensions-dmx-function-reference.md)   
 [Functions &#40;DMX&#41;](../dmx/functions-dmx.md)   
 [General Prediction Functions &#40;DMX&#41;](../dmx/general-prediction-functions-dmx.md)  
  
  
