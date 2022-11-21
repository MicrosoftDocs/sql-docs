---
title: "Structure and Usage of DMX Prediction Queries"
description: "Structure and Usage of DMX Prediction Queries"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# Structure and Usage of DMX Prediction Queries
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  In [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], you can use the prediction query in Data Mining Extensions (DMX) to predict unknown column values in a new dataset, based on the results of a mining model.  
  
 The type of query you use depends on what information you want to obtain from a model. If you want to create simple predictions in real time, for example to know if a potential customer on a Web site fits the persona of a bike buyer, you would use a singleton query. If you want to create a batch of predictions from a set of cases that are contained within a data source, you would use a regular prediction query.  
  
## Prediction Types  
 You can use DMX to create the following types of predictions:  
  
 Prediction join  
 Use to create predictions on input data based on the patterns that exist in the mining model. This query statement must be followed by an **ON** clause that supplies the join conditions between the mining model columns and the input columns.  
  
 Natural prediction join  
 Use to create predictions that are based on column names in the mining model that exactly match the column names in the table on which you are performing the query. This query statement does not require an **ON** clause, because the join condition is automatically generated based on the matching names between the mining model columns and the input columns.  
  
 Empty prediction join  
 Use to discover the most likely prediction, without having to supply input data. This returns a prediction that is based only on the content of the mining model.  
  
 Singleton query  
 Use to create a prediction by feeding the data to the query. This statement is useful because you can feed a single case to the query, to get a result back quickly. For example, you can use the query to predict whether someone who is female, age 35, and married would be likely to purchase a bicycle. This query does not require an external data source.  
  
## Query Structure  
 To build a prediction query in DMX, you use a combination of the following elements:  
  
-   **SELECT [FLATTENED]**  
  
-   **TOP**  
  
-   **FROM**  *\<model>*  **PREDICTION JOIN**  
  
-   **ON**  
  
-   **WHERE**  
  
-   **ORDER BY**  
  
 The **SELECT** element of a prediction query defines the columns and expressions that will appear in the result set, and can include the following data:  
  
-   **Predict** or **PredictOnly** columns from the mining model.  
  
-   Any column from the input data that is used to create the predictions.  
  
-   Functions that return a column of data.  
  
 The **FROM** *\<model>* **PREDICTION JOIN** element defines the source data to be used to create the prediction. For a singleton query, this is a series of values that are assigned to columns. For an empty prediction join, this is left empty.  
  
 The **ON** element maps the columns that are defined in the mining model to columns in an external dataset. You do not have to include this element if you are creating an empty prediction join query or a natural prediction join.  
  
 You can use the **WHERE** clause to filter the results of a prediction query. You can use a **TOP** or **ORDER BY** clause to select most likely predictions. For more information about using these clauses, see [SELECT &#40;DMX&#41;](../dmx/select-dmx.md).  
  
 For more information about the syntax of a prediction statement, see [SELECT FROM &#60;model&#62; PREDICTION JOIN &#40;DMX&#41;](../dmx/select-from-model-prediction-join-dmx.md) and [SELECT FROM &#60;model&#62; &#40;DMX&#41;](../dmx/select-from-model-dmx.md).  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Reference](../dmx/data-mining-extensions-dmx-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Function Reference](../dmx/data-mining-extensions-dmx-function-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Operator Reference](../dmx/data-mining-extensions-dmx-operator-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)   
 [Data Mining Extensions &#40;DMX&#41; Syntax Conventions](../dmx/data-mining-extensions-dmx-syntax-conventions.md)   
 [Data Mining Extensions &#40;DMX&#41; Syntax Elements](../dmx/data-mining-extensions-dmx-syntax-elements.md)   
 [General Prediction Functions &#40;DMX&#41;](../dmx/general-prediction-functions-dmx.md)   
 [Understanding the DMX Select Statement](../dmx/understanding-the-dmx-select-statement.md)  
  
  
