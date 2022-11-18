---
title: "Predict (DMX)"
description: "Predict (DMX)"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# Predict (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  The **Predict** function returns a predicted value, or set of values, for a specified column.  
  
## Syntax  
  
```  
  
Predict(<scalar column reference>, [option1], [option2], [option n], [INCLUDE_NODE_ID], n)  
Predict(<table column reference>, [option1], [option2], [option n], [INCLUDE_NODE_ID], n)  
```  
  
## Applies To  
 Either a scalar column reference or a table column reference.  
  
## Return Type  
 \<scalar column reference>  
  
 or  
  
 \<table column reference>  
  
 The return type depends on the type of column to which this function is applied.  
  
> [!NOTE]  
>  INCLUSIVE, EXCLUSIVE, INPUT_ONLY, and INCLUDE_STATISTICS apply only for a table column reference, and EXCLUDE_NULL and INCLUDE_NULL apply only for a scalar column reference.  
  
## Remarks  
 Options include EXCLUDE_NULL (default), INCLUDE_NULL, INCLUSIVE, EXCLUSIVE (default), INPUT_ONLY, and INCLUDE_STATISTICS.  
  
> [!NOTE]  
>  For time series models, the Predict function does not support INCLUDE_STATISTICS.  
  
 The INCLUDE_NODE_ID parameter returns the $NODEID column in the result. NODE_ID is the content node on which the prediction is executed for a particular case. This parameter is optional when using Predict on table columns.  
  
 The *n* parameter applies to table columns. It sets the number of rows that are returned based on the type of prediction. If the underlying column is sequence, it calls the **PredictSequence** function. If the underlying column is time series, it calls the **PredictTimeSeries** function. For associative types of prediction, it calls the **PredictAssociation** function.  
  
 The **Predict** function supports polymorphism.  
  
 The following alternative abbreviated forms are frequently used:  
  
-   [Gender] is an alternative for **Predict**([Gender], EXCLUDE_NULL).  
  
-   [Products Purchases] is an alternative for **Predict**([Products Purchases], EXCLUDE_NULL, EXCLUSIVE).  
  
    > [!NOTE]  
    >  The return type of this function is itself regarded as a column reference. This means that the **Predict** function can be used as an argument in other functions that take a column reference as an argument (except for the **Predict** function itself).  
  
 Passing INCLUDE_STATISTICS to a prediction on a table-valued column adds the columns **$Probability** and **$Support** to the resulting table. These columns describe the probability of existence for the associated nested table record.  
  
## Examples  
 The following example uses the Predict function to return the four products in the Adventure Works database that are most likely to be sold together. Because the function is predicting against an association rules mining model, it automatically uses the **PredictAssociation** function as described earlier.  
  
```  
SELECT  
    Predict([Association].[v Assoc Seq Line Items],INCLUDE_STATISTICS,4)  
FROM     [Association]  
```  
  
 Sample results:  
  
 This query returns a single row of data with one column, `Expression`, but that column contains the following nested table.  
  
|Model|$SUPPORT|$PROBABILITY|$ADJUSTEDPROBABILITY|  
|-----------|--------------|------------------|--------------------------|  
|Sport-100|4334|0.291283016331743|0.252695851192499|  
|Water Bottle|2866|0.192620471805901|0.175205052318795|  
|Patch Kit|2113|0.142012232004839|0.132389356196586|  
|Mountain Tire Tube|1992|0.133879965051415|0.125304947722259|  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Function Reference](../dmx/data-mining-extensions-dmx-function-reference.md)   
 [Functions &#40;DMX&#41;](../dmx/functions-dmx.md)   
 [General Prediction Functions &#40;DMX&#41;](../dmx/general-prediction-functions-dmx.md)  
  
  
