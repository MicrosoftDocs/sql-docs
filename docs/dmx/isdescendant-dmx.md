---
title: "IsDescendant (DMX) | Microsoft Docs"
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
  - "IsDescendant"
dev_langs: 
  - "DMX"
helpviewer_keywords: 
  - "IsDescendant function"
ms.assetid: d9fe7446-edb5-487b-8ea6-c9efaccf6d90
caps.latest.revision: 29
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# IsDescendant (DMX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Indicates whether the current node descends from the specified node.  
  
## Syntax  
  
```  
  
IsDescendant(<NodeID>)  
```  
  
## Return Type  
 A Boolean type.  
  
## Remarks  
 **IsDescendant** is only used in [SELECT FROM &#60;model&#62;.CONTENT &#40;DMX&#41;](../dmx/select-from-model-content-dmx.md) and [SELECT FROM &#60;model&#62;.DIMENSION_CONTENT &#40;DMX&#41;](../dmx/select-from-model-dimension-content-dmx.md) queries.  
  
## Examples  
 The following example returns all the cases that are descendents of the node that is specified in the IsDescendant function.  
  
```  
SELECT * FROM [TM Decision Tree].CONTENT  
WHERE IsDescendant('00000000100')  
```  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Function Reference](../dmx/data-mining-extensions-dmx-function-reference.md)   
 [Functions &#40;DMX&#41;](../dmx/functions-dmx.md)   
 [General Prediction Functions &#40;DMX&#41;](../dmx/general-prediction-functions-dmx.md)  
  
  
