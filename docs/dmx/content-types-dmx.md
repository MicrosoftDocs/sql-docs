---
title: "Content Types (DMX)"
description: "Content Types (DMX)"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# Content Types (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  Data mining algorithms require additional information beyond the data type to function correctly, such as the content type. The content type helps the algorithm determine how to work with the data in the column.  
  
 Each algorithm supports specific content types. For example, the [!INCLUDE[msCoName](../includes/msconame-md.md)] Naive Bayes algorithm cannot use continuous columns. To use a continuous column in a [!INCLUDE[msCoName](../includes/msconame-md.md)] Naive Bayes model, you must discretize the data in the column. Some algorithms require certain content types in order to function correctly. For example, the [!INCLUDE[msCoName](../includes/msconame-md.md)] Time Series algorithm requires a key time column to identify the time over which the data was collected.  
  
 For a complete description of the content types that [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] supports, see [Content Types &#40;Data Mining&#41;](/analysis-services/data-mining/content-types-data-mining).  
  
## See Also  
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](/analysis-services/data-mining/data-mining-algorithms-analysis-services-data-mining)   
 [Data Mining Extensions &#40;DMX&#41; Reference](../dmx/data-mining-extensions-dmx-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Syntax Elements](../dmx/data-mining-extensions-dmx-syntax-elements.md)   
 [Data Mining Extensions &#40;DMX&#41; Function Reference](../dmx/data-mining-extensions-dmx-function-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Operator Reference](../dmx/data-mining-extensions-dmx-operator-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)   
 [Data Mining Extensions &#40;DMX&#41; Syntax Conventions](../dmx/data-mining-extensions-dmx-syntax-conventions.md)   
 [General Prediction Functions &#40;DMX&#41;](../dmx/general-prediction-functions-dmx.md)   
 [Structure and Usage of DMX Prediction Queries](../dmx/structure-and-usage-of-dmx-prediction-queries.md)   
 [Understanding the DMX Select Statement](../dmx/understanding-the-dmx-select-statement.md)  
  
