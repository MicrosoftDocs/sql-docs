---
title: "Data Types (Data Mining) | Microsoft Docs"
ms.date: 05/01/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Data Types (Data Mining)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  When you create a mining model or a mining structure in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], you must define the data types for each of the columns in the mining structure. The data type tells the analysis engine whether the data in the data source is numerical or text, and how the data should be processed. For example, if your source data contains numerical data, you can specify whether the numbers be treated as integers or by using decimal places.  
  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] supports the following data types for mining structure columns:  
  
|Data Type|Supported Content Types|  
|---------------|-----------------------------|  
|**Text**|Cyclical, Discrete, Discretized, Key Sequence, Ordered, Sequence|  
|**Long**|Continuous, Cyclical, Discrete, Discretized, Key, Key Sequence, Key Time, Ordered, Sequence, Time<br /><br /> Classified|  
|**Boolean**|Cyclical, Discrete, Ordered|  
|**Double**|Continuous, Cyclical, Discrete, Discretized, Key, Key Sequence, Key Time, Ordered, Sequence, Time<br /><br /> Classified|  
|**Date**|Continuous, Cyclical, Discrete, Discretized, Key, Key Sequence, Key Time, Ordered|  
  
> [!NOTE]  
>  The Time and Sequence content types are only supported by third-party algorithms. The Cyclical and Ordered content types are supported, but most algorithms treat them as discrete values and do not perform special processing.  
  
 The table also shows the *content types* supported for each data type.  
  
 The content type is specific to data mining and lets you customize the way that data is processed or calculated in the mining model. For example, even if your column contains numbers, you might need to model them as discrete values. If the column contains numbers, you can also specify that they be binned, or discretized, or specify that the model handle them as continuous values. Thus, the content type can have a huge effect on the model.. For a list of all the content types, see [Content Types &#40;Data Mining&#41;](../../analysis-services/data-mining/content-types-data-mining.md).  
  
> [!NOTE]  
>  In other machine learning systems, you might encounter the terms *nominal data*, *factors* or *categories*, *ordinal data*, or *sequence data*. In general, these correspond to content types. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the data type specifies only the value type for storage, not its usage in the model.  
  
## Specifying a Data Type  
 If you create the mining model directly by using Data Mining Extensions (DMX), you can define the data type for each column as you define the model, and Analysis Services will create the corresponding mining structure with the specified data types at the same time. If you create the mining model or mining structure by using a wizard, Analysis Services will suggest a data type, or you can choose a data type from a list.  
  
## Changing a Data Type  
 If you change the data type of a column, you must always reprocess the mining structure and any mining models that are based on that structure. Sometimes if you change the data type, that column can no longer be used in a particular model. In that case, Analysis Services will either raise an error when you reprocess the model, or will process the model but leave out that particular column.  
  
## See Also  
 [Content Types &#40;Data Mining&#41;](../../analysis-services/data-mining/content-types-data-mining.md)   
 [Content Types &#40;DMX&#41;](../../dmx/content-types-dmx.md)   
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/data-mining-algorithms-analysis-services-data-mining.md)   
 [Mining Structures &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-structures-analysis-services-data-mining.md)   
 [Data Types &#40;DMX&#41;](../../dmx/data-types-dmx.md)   
 [Mining Model Columns](../../analysis-services/data-mining/mining-model-columns.md)   
 [Mining Structure Columns](../../analysis-services/data-mining/mining-structure-columns.md)  
  
  
