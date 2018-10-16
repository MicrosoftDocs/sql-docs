---
title: "Data Types (Data Mining) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "data types [data mining]"
  - "columns [data mining], data types"
  - "data mining [Analysis Services], data types"
ms.assetid: 4af5b7db-790b-459c-b2b4-00f0cf6b5ce4
author: minewiskan
ms.author: owend
manager: craigg
---
# Data Types (Data Mining)
  When you create a mining model or a mining structure in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], you must define the data types for each of the columns in the mining structure. The data type tells the data mining engine whether the data in the data source is numerical or text, and how the data should be processed. For example, if your source data contains numerical data, you can specify whether the numbers be treated as integers or by using decimal places.  
  
 Each data type supports one or more content types. By setting the content type, you can customize the way that data in the column is processed or calculated in the mining model.  
  
 For example, if you have numeric data in a column, you can choose to handle it either as a numeric or text data type. If you choose the numeric data type, you can set several different content types: you can discretize the numbers, or handle them as continuous values. For a list of all the content types, see [Content Types &#40;Data Mining&#41;](content-types-data-mining.md).  
  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] supports the following data types for mining structure columns:  
  
|Data Type|Supported Content Types|  
|---------------|-----------------------------|  
|`Text`|Cyclical, Discrete, Discretized, Key Sequence, Ordered, Sequence|  
|`Long`|Continuous, Cyclical, Discrete, Discretized, Key, Key Sequence, Key Time, Ordered, Sequence, Time<br /><br /> Classified|  
|`Boolean`|Cyclical, Discrete, Ordered|  
|`Double`|Continuous, Cyclical, Discrete, Discretized, Key, Key Sequence, Key Time, Ordered, Sequence, Time<br /><br /> Classified|  
|`Date`|Continuous, Cyclical, Discrete, Discretized, Key, Key Sequence, Key Time, Ordered|  
  
> [!NOTE]  
>  The Time and Sequence content types are only supported by third-party algorithms. The Cyclical and Ordered content types are supported, but most algorithms treat them as discrete values and do not perform special processing.  
  
## Specifying a Data Type  
 If you create the mining model directly by using Data Mining Extensions (DMX), you can define the data type for each column as you define the model, and Analysis Services will create the corresponding mining structure with the specified data types at the same time. If you create the mining model or mining structure by using a wizard, Analysis Services will suggest a data type, or you can choose a data type from a list.  
  
## Changing a Data Type  
 If you change the data type of a column, you must always reprocess the mining structure and any mining models that are based on that structure. Sometimes if you change the data type, that column can no longer be used in a particular model. In that case, Analysis Services will either raise an error when you reprocess the model, or will process the model but leave out that particular column.  
  
## See Also  
 [Content Types &#40;Data Mining&#41;](content-types-data-mining.md)   
 [Content Types &#40;DMX&#41;](/sql/dmx/content-types-dmx)   
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](data-mining-algorithms-analysis-services-data-mining.md)   
 [Mining Structures &#40;Analysis Services - Data Mining&#41;](mining-structures-analysis-services-data-mining.md)   
 [Data Types &#40;DMX&#41;](/sql/dmx/data-types-dmx)   
 [Mining Model Columns](mining-model-columns.md)   
 [Mining Structure Columns](mining-structure-columns.md)  
  
  
