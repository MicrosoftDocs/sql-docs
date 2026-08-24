---
title: "PredictSequence (DMX)"
description: "PredictSequence (DMX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# PredictSequence (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  Predicts future sequence values for a specified set of sequence data.  
  
## Syntax  
  
```  
  
PredictSequence(<table column reference>)  
PredictSequence(\<table column reference, n>)  
PredictSequence(\<table column reference, n-start, n-end>)  
```  
  
## Return Type  
 A \<table expression>.  
  
## Remarks  
 If the *n* parameter is specified, it returns the following values:  
  
-   If *n* is greater than zero, the most likely sequence values in the next *n* steps.  
  
-   If both *n-start* and *n-end* are specified, the sequence values from *n-start* to *n-end*.  
  
## Examples  
 The following example returns a sequence of the five products that are most likely to be purchased by a customer in the [!INCLUDE[ssAWDWsp](../includes/ssawdwsp-md.md)] database based on the Sequence Clustering mining model.  
  
```  
SELECT  
  PredictSequence([Sequence Clustering].[v Assoc Seq Line Items],5)  
From  
  [Sequence Clustering]  
```  
  
## Related content

- [Data Mining Extensions (DMX) Function Reference](data-mining-extensions-dmx-function-reference.md)
- [Functions (DMX)](functions-dmx.md)
- [General Prediction Functions (DMX)](general-prediction-functions-dmx.md)
