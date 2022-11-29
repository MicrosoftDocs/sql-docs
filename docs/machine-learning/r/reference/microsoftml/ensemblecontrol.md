---
title: "ensembleControl function (MicrosoftML)"
description: "Control the parameters used to create an ensemble."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (MicrosoftML), ensembleControl
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
 
 
 # ensembleControl: ensembleControl 
 
 
Control the parameters used to create an ensemble.
 
 
 ## Usage

```   
  ensembleControl(randomSeed = NULL, modelCount = 1, replace = FALSE,
    sampRate = ifelse(replace, 1, 0.632), splitData = FALSE,
    combineMethod = NULL, ...)
 
```
 
 ## Arguments

   
  
 ### `randomSeed`
 Specifies the random seed. The default value is `NULL`. 
  
  
  
 ### `modelCount`
 Specifies the number of models to train. The default value is `1`, meaning no ensembling occurs. 
  
  
  
 ### `replace`
 A logical value specifying if the sampling of observations should be done with  or without replacement. The default value is `FALSE`. 
  
  
  
 ### `sampRate`
 a scalar of positive value specifying the percentage of observations to sample for  each trainer. The default is 1.0 for sampling with replacement (i.e., replace=TRUE) and 0.632  for sampling without replacement (i.e., replace=FALSE). 
  
  
  
 ### `splitData`
 A logical value that specifies whether or not to train the base models  on non-overlapping partitions. The default is `FALSE`.  It is available only for `RxSpark` compute context and is ignored for others. 
  
  
  
 ### `combineMethod`
 Specifies the method used to combine the models:   
*   `median` to compute the median of the individual model outputs,   
*   `average` to compute the average of the individual model outputs and   
*   `vote` to compute (pos-neg) / the total number of models, where 'pos' is the number of positive outputs  and 'neg' is the number of negative outputs. 
 The default value is `median`. 
  
  
  
 ### ` ...`
 Not used currently. 
  
 
 
 ## Value
 
A list of ensemble parameters.
 
 
