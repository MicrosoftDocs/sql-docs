---
title: "Distributions (DMX)"
description: "Distributions (DMX)"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# Distributions (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  In [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], you can define the content of columns in a mining structure, to affect how algorithms process the data in those columns when you create mining models. For some algorithms, it is useful to define the distribution of any continuous columns before you process the model, if the columns are known to contain common distributions of values. If you do not define the distributions, the resulting mining models may produce less accurate predictions than if the distributions were defined, because the algorithms will have less information from which to interpret the data.  
  
 [!INCLUDE[msCoName](../includes/msconame-md.md)] data mining algorithms support the following distribution types:  
  
 **NORMAL**  
 The values for the continuous column form a histogram with a normal Gaussian distribution.  
  
 **Log Normal**  
 The values for the continuous column form a histogram, where the logarithm of the values is normally distributed.  
  
 **UNIFORM**  
 The values for the continuous column form a flat curve, in which all values are equally likely.  
  
 For more information about [!INCLUDE[msCoName](../includes/msconame-md.md)] data mining algorithms, see [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](/analysis-services/data-mining/data-mining-algorithms-analysis-services-data-mining). Third-party algorithm providers may support additional distribution types. To determine which distribution types an algorithm supports, use the **SUPPORTED_DISTRIBUTION_FLAGS** schema rowset.  
  
 For more information about distribution types, see [Column Distributions &#40;Data Mining&#41;](/analysis-services/data-mining/column-distributions-data-mining).  
  
## See Also  
 [Content Types &#40;Data Mining&#41;](/analysis-services/data-mining/content-types-data-mining)   
 [Data Mining Extensions &#40;DMX&#41; Reference](../dmx/data-mining-extensions-dmx-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Syntax Elements](../dmx/data-mining-extensions-dmx-syntax-elements.md)   
 [Data Mining Extensions &#40;DMX&#41; Function Reference](../dmx/data-mining-extensions-dmx-function-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Operator Reference](../dmx/data-mining-extensions-dmx-operator-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)   
 [Data Mining Extensions &#40;DMX&#41; Syntax Conventions](../dmx/data-mining-extensions-dmx-syntax-conventions.md)   
 [General Prediction Functions &#40;DMX&#41;](../dmx/general-prediction-functions-dmx.md)   
 [Structure and Usage of DMX Prediction Queries](../dmx/structure-and-usage-of-dmx-prediction-queries.md)   
 [Understanding the DMX Select Statement](../dmx/understanding-the-dmx-select-statement.md)  
  
