---
title: "PredictAssociation (DMX) | Microsoft Docs"
ms.date: 06/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: dmx
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# PredictAssociation (DMX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Predicts associative membership.  
  
For example, you can use the PredictAssociation function to obtain the set of recommendations given the current state of the shopping basket for a customer. 
  
## Syntax  
  
```  
  
PredictAssociation(<table column reference>, option1, option2, n ...)  
```  
  
## Applies To  
 Algorithms that contain predictable nested tables, including association and some classification algorithms. Classification algorithms that support nested tables include the [!INCLUDE[msCoName](../includes/msconame-md.md)] Decision Trees, [!INCLUDE[msCoName](../includes/msconame-md.md)] Naive Bayes, and [!INCLUDE[msCoName](../includes/msconame-md.md)] Neural Network algorithms.  
  
## Return Type  
 \<table expression>  
  
## Remarks  
 The options for the **PredictAssociation** function include EXCLUDE_NULL, INCLUDE_NULL, INCLUSIVE, EXCLUSIVE (default), INPUT_ONLY, INCLUDE_STATISTICS, and INCLUDE_NODE_ID.  
  
> [!NOTE]  
>  INCLUSIVE, EXCLUSIVE, INPUT_ONLY, and INCLUDE_STATISTICS apply only for a table column reference, and EXCLUDE_NULL and INCLUDE_NULL apply only for a scalar column reference.  
  
 INCLUDE_STATISTICS only returns **$Probability** and **$AdjustedProbability**.  
  
 If the numeric parameter *n* is specified, the **PredictAssociation** function returns the top n most likely values based on the probability:  
  
```  
PredictAssociation(colref, [$AdjustedProbability], n)  
```  
  
 If you include **$AdjustedProbability**, the statement returns the top *n* values based on the **$AdjustedProbability**.  
  
## Examples  
 The following example uses the **PredictAssociation** function to return the four products in the Adventure Works database that are most likely to be sold together.  
  
```  
SELECT  
  PredictAssociation([Association].[v Assoc Seq Line Items],4)  
From  
  [Association]  
```  
The following example demonstrates how you can use a nested table as input to the prediction function, useing the SHAPE clause. The SHAPE query creates a rowset with customerId as one column and a nested table as a second column, which contains the list of products a customer has already brought. 

~~~~
SELECT T.[CustomerId], PredictAssociation(MyNestedTable, 5) // returns top 5 associated items
FROM My Model
PREDICTION JOIN
SHAPE {
    OPENQUERY([Adventure Works DW],'SELECT CustomerID, OrderNumber
    FROM vAssocSeqOrders ORDER BY OrderNumber')
} APPEND (
    {OPENQUERY([Adventure Works DW],'SELECT OrderNumber, model FROM 
    dbo.vAssocSeqLineItems ORDER BY OrderNumber, Model')}
  RELATE OrderNumber to OrderNumber) AS T
~~~~  

  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Function Reference](../dmx/data-mining-extensions-dmx-function-reference.md)   
 [Functions &#40;DMX&#41;](../dmx/functions-dmx.md)   
 [General Prediction Functions &#40;DMX&#41;](../dmx/general-prediction-functions-dmx.md)  
  
  
