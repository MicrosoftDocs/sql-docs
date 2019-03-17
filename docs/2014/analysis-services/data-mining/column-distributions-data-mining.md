---
title: "Column Distributions (Data Mining) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "normal distribution type [data mining]"
  - "uniform distribution type [data mining]"
  - "columns [data mining], distributions"
  - "log normal distribution type [data mining]"
  - "continuous columns"
  - "distributions [data mining]"
ms.assetid: 87e700de-32be-4bc8-b01d-ba4ee1ab48de
author: minewiskan
ms.author: owend
manager: craigg
---
# Column Distributions (Data Mining)
  In [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], you can define column distributions in a mining structure, to affect how algorithms process the data in those columns when you create mining models. For some algorithms, it is useful to define the distribution of any continuous columns before you process the model, if the columns are known to contain common distributions of values. If you do not define the distributions, the resulting mining models may produce less accurate predictions than if the distributions were defined, because the algorithms will have less information from which to interpret the data.  
  
 The algorithms that are available in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] support the following distribution types:  
  
 `Normal`  
 The values for the continuous column form a histogram with a normal distribution.  
  
 ![Histogram with normal distribution](../media/normal-distribution.gif "Histogram with normal distribution")  
  
 `Log Normal`  
 The values for the continuous column form a histogram, where the curve is elongated at the upper end and is skewed toward the lower end.  
  
 ![Histogram with log normal distribution](../media/log-normal-distribution.gif "Histogram with log normal distribution")  
  
 `Uniform`  
 The values for the continuous column form a flat curve, in which all values are equally likely.  
  
 ![Histogram with uniform distribution](../media/uniform-distribution.gif "Histogram with uniform distribution")  
  
 For more information about the algorithms that [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] provides, see [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](data-mining-algorithms-analysis-services-data-mining.md).  
  
## See Also  
 [Content Types &#40;Data Mining&#41;](content-types-data-mining.md)   
 [Mining Structures &#40;Analysis Services - Data Mining&#41;](mining-structures-analysis-services-data-mining.md)   
 [Discretization Methods &#40;Data Mining&#41;](discretization-methods-data-mining.md)   
 [Distributions &#40;DMX&#41;](/sql/dmx/distributions-dmx)   
 [Mining Structure Columns](mining-structure-columns.md)  
  
  
