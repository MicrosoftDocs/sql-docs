---
title: "Usage (DMX) | Microsoft Docs"
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
dev_langs: 
  - "DMX"
helpviewer_keywords: 
  - "column usage [DMX]"
  - "Data Mining Extensions [Analysis Services], column usage types"
  - "DMX [Analysis Services], column usage types"
ms.assetid: 6d7ae72a-f5b5-4744-a3a2-46ccd6432c1a
caps.latest.revision: 31
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Usage (DMX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  When you use Data Mining Extensions (DMX) to define a new data mining model in [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], you must specify how the data mining algorithm that builds the model will use each column. You can specify a column as one of the following types:  
  
-   **Key**  
  
-   **Key Sequence**  
  
-   **Key Time**  
  
-   **Predict**  
  
-   **PredictOnly**  
  
 Columns that are left unspecified in DMX are treated as input columns.  
  
 To process a model correctly, the algorithm must know which column is the key column that uniquely identifies each row, which column is the target column for creating predictions if you are creating a predictable model, and which columns to use as input columns to create the relationships that predict the target column.  
  
 Columns that are specified as the **Predict** type are used as both input and output columns. Columns that are specified as **PredictOnly** are only used as output columns. Specific algorithms may treat Predict columns differently.  
  
 For more information about the column usage types that [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] supports, see [Mining Model Columns](../analysis-services/data-mining/mining-model-columns.md).  
  
## See Also  
 [Data Mining Algorithms &#40;Analysis Services - Data Mining&#41;](../analysis-services/data-mining/data-mining-algorithms-analysis-services-data-mining.md)   
 [Data Mining Extensions &#40;DMX&#41; Reference](../dmx/data-mining-extensions-dmx-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Syntax Elements](../dmx/data-mining-extensions-dmx-syntax-elements.md)   
 [Data Mining Extensions &#40;DMX&#41; Function Reference](../dmx/data-mining-extensions-dmx-function-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Operator Reference](../dmx/data-mining-extensions-dmx-operator-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)   
 [Data Mining Extensions &#40;DMX&#41; Syntax Conventions](../dmx/data-mining-extensions-dmx-syntax-conventions.md)   
 [General Prediction Functions &#40;DMX&#41;](../dmx/general-prediction-functions-dmx.md)   
 [Structure and Usage of DMX Prediction Queries](../dmx/structure-and-usage-of-dmx-prediction-queries.md)   
 [Understanding the DMX Select Statement](../dmx/understanding-the-dmx-select-statement.md)  
  
  
