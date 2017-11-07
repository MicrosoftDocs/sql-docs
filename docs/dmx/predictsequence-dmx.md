---
title: "PredictSequence (DMX) | Microsoft Docs"
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
f1_keywords: 
  - "PredictSequence"
dev_langs: 
  - "DMX"
helpviewer_keywords: 
  - "PredictSequence function"
ms.assetid: c2992dfc-b99d-4430-8dcd-21ad3ffd4590
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# PredictSequence (DMX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

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
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Function Reference](../dmx/data-mining-extensions-dmx-function-reference.md)   
 [Functions &#40;DMX&#41;](../dmx/functions-dmx.md)   
 [General Prediction Functions &#40;DMX&#41;](../dmx/general-prediction-functions-dmx.md)  
  
  
