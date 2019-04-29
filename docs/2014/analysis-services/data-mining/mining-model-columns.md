---
title: "Mining Model Columns | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "columns [data mining], mining model columns"
  - "columns [data mining]"
  - "REGRESSOR column"
  - "columns [data mining], modeling flags"
  - "modeling flags [data mining]"
  - "MODEL_EXISTENCE_ONLY column"
  - "usage property [data mining]"
ms.assetid: fab47643-5bfd-424e-a0f7-69e665db6bab
author: minewiskan
ms.author: owend
manager: craigg
---
# Mining Model Columns
  A data mining model applies a mining model algorithm to the data that is represented by a mining structure. Like the mining structure, the mining model contains columns. A mining model is contained within the mining structure, and inherits all the values of the properties that are defined by the mining structure. The model can use all the columns that the mining structure contains or a subset of the columns.  
  
 You can define two additional pieces of information on a mining model column: usage, and modeling flags.  
  
-   **Usage** is a property that defines how the model uses the column. Columns can be used as input columns, key columns, or predictable columns.  
  
-   **Modeling flags** provide the algorithm with additional information about the data that is defined in the case table, so that the algorithm can build a more accurate model. You can define modeling flags programmatically by using the Data Mining Extensions (DMX) language, or in **Data Mining Designer** in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
 The following list describes the modeling flags that you can define on a mining model column.  
  
 `MODEL_EXISTENCE_ONLY`  
 Indicates that the presence of the attribute is more important than the values that are in the attribute column. For example, consider a case table that contains a list of order items that are associated with a particular customer. The table data includes the product type, ID, and cost of each item. For modeling purposes, the fact that the customer purchased a particular order item may be more important than the cost of the order item itself. In this case, the cost column should be marked as `MODEL_EXISTENCE_ONLY`.  
  
 `REGRESSOR`  
 Indicates that the algorithm can use the specified column in the regression formula of regression algorithms. This flag is supported by the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Decision Trees and [!INCLUDE[msCoName](../../includes/msconame-md.md)] Time Series algorithms.  
  
 For more information about setting the usage property and defining modeling flags programmatically with DMX, see [CREATE MINING MODEL &#40;DMX&#41;](/sql/dmx/create-mining-model-dmx). For more information about setting the usage property and defining modeling flags in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], see [Moving Data Mining Objects](moving-data-mining-objects.md).  
  
## See Also  
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](data-mining-algorithms-analysis-services-data-mining.md)   
 [Mining Structures &#40;Analysis Services - Data Mining&#41;](mining-structures-analysis-services-data-mining.md)   
 [Change the Properties of a Mining Model](change-the-properties-of-a-mining-model.md)   
 [Exclude a Column from a Mining Model](exclude-a-column-from-a-mining-model.md)   
 [Mining Structure Columns](mining-structure-columns.md)  
  
  
