---
title: "logisticRegression function (MicrosoftML)"
description: "Creates a list containing the function name and arguments to train a logistic regression model with rxEnsemble."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (MicrosoftML), logisticRegression
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
 
 
 # logisticRegression: logisticRegression 
 
 
Creates a list containing the function name and arguments to train a
 logistic regression model with [rxEnsemble](rxEnsemble.md).
 
 
 ## Usage

```   
  logisticRegression(l2Weight = 1, l1Weight = 1, optTol = 1e-07,
    memorySize = 20, initWtsScale = 0, maxIterations = 2147483647,
    showTrainingStats = FALSE, sgdInitTol = 0, trainThreads = NULL,
    denseOptimizer = FALSE, ...)
 
```
 
 ## Arguments

   
  
 ### `l2Weight`
 The L2 regularization weight. Its value must be greater than  or equal to `0` and the default value is set to `1`. 
  
  
  
 ### `l1Weight`
 The L1 regularization weight. Its value must be greater than  or equal to `0` and the default value is set to `1`. 
  
  
  
 ### `optTol`
 Threshold value for optimizer convergence. If the improvement between iterations is less than the threshold, the algorithm stops and returns the current model. Smaller values are slower, but more accurate. The default value is `1e-07`. 
  
  
  
 ### `memorySize`
 Memory size for L-BFGS, specifying the number of past positions and gradients to store for the computation of the next step. This optimization parameter limits the amount of memory that is used to compute the magnitude and direction of the next step. When you specify less memory,  training is faster but less accurate. Must be greater than or equal to  `1` and the default value is `20`. 
  
  
  
 ### `initWtsScale`
 Sets the initial weights diameter that specifies  the range from which values are drawn for the initial weights. These   weights are initialized randomly from within this range. For  example, if the diameter is specified to be `d`, then the weights  are uniformly distributed between `-d/2` and `d/2`. The default value is `0`, which specifies that all the weights are  initialized to `0`. 
  
  
  
 ### `maxIterations`
 Sets the maximum number of iterations. After this number  of steps, the algorithm stops even if it has not satisfied convergence criteria. 
  
  
  
 ### `showTrainingStats`
 Specify `TRUE` to show the statistics of  training data and the trained model; otherwise, `FALSE`. The  default value is `FALSE`. For additional information about model  statistics, see [summary.mlModel](mlModel.md). 
  
  
  
 ### `sgdInitTol`
 Set to a number greater than 0 to use Stochastic Gradient Descent (SGD) to find the initial parameters. A non-zero value  set specifies the tolerance SGD uses to determine convergence. The default value is `0` specifying that SGD is not used. 
  
  
  
 ### `trainThreads`
 The number of threads to use in training the model.  This should be set to the number of cores on the machine. Note that  L-BFGS multi-threading attempts to load dataset into memory. In case of out-of-memory issues, set `trainThreads` to `1` to turn off multi-threading. If `NULL` the number of threads to use is determined internally. The default value is `NULL`. 
  
  
  
 ### `denseOptimizer`
 If `TRUE`, forces densification of the internal optimization vectors. If `FALSE`, enables the logistic regression  optimizer use sparse or dense internal states as it finds appropriate.   Setting `denseOptimizer` to `TRUE` requires the internal  optimizer to use a dense internal state, which may help alleviate load  on the garbage collector for some varieties of larger problems. 
  
  
  
 ### ` ...`
 Additional arguments. 
  
 
 
 
