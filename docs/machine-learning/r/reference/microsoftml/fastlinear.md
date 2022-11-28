---
title: "fastLinear function (MicrosoftML) "
description: "Creates a list containing the function name and arguments to train a Fast Linear model with rxEnsemble."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (MicrosoftML), fastLinear
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
 
 
 # fastLinear: fastLinear 
 
 
Creates a list containing the function name and arguments to train a
 Fast Linear model with [rxEnsemble](rxEnsemble.md).
 
 
 ## Usage

```   
  fastLinear(lossFunction = NULL, l2Weight = NULL, l1Weight = NULL,
    trainThreads = NULL, convergenceTolerance = 0.1, maxIterations = NULL,
    shuffle = TRUE, checkFrequency = NULL, ...)
 
```
 
 ## Arguments

   
  
 ### `lossFunction`
 Specifies the empirical loss function to optimize. For binary classification, the following choices are available:  
*   [logLoss](loss.md): The log-loss. This is the default.  
*   [hingeLoss](loss.md): The SVM hinge loss. Its parameter represents the margin size.    
*   [smoothHingeLoss](loss.md): The smoothed hinge loss. Its parameter represents the smoothing constant.  
For linear regression, squared loss [squaredLoss](loss.md) is currently supported. When this parameter is set to `NULL`, its default value depends on the type of learning:  
*   [logLoss](loss.md) for binary classification. 
*   [squaredLoss](loss.md) for linear regression. 
 
  
  
  
 ### `l2Weight`
 Specifies the L2 regularization weight. The value must be either non-negative or `NULL`. If `NULL` is specified, the  actual value is automatically computed based on data set. `NULL` is the default value. 
  
  
  
 ### `l1Weight`
 Specifies the L1 regularization weight. The value must be either non-negative or `NULL`. If `NULL` is specified, the  actual value is automatically computed based on data set. `NULL` is the default value. 
  
  
  
 ### `trainThreads`
 Specifies how many concurrent threads can be used to run the algorithm. When this parameter is set to `NULL`, the number of threads used is determined based on the number of logical processors available to the process as well as the sparsity of data. Set it to `1` to run the algorithm in a single thread. 
  
  
  
 ### `convergenceTolerance`
 Specifies the tolerance threshold used as a  convergence criterion. It must be between 0 and 1. The default value is `0.1`. The algorithm is considered to have converged if the relative  duality gap, which is the ratio between the duality gap and the primal loss,  falls below the specified convergence tolerance. 
  
  
  
 ### `maxIterations`
 Specifies an upper bound on the number of training iterations. This parameter must be positive or `NULL`. If `NULL` is specified, the actual value is automatically computed based on data set.  Each iteration requires a complete pass over the training data. Training  terminates after the total number of iterations reaches the specified   upper bound or when the loss function converges, whichever happens earlier. 
  
  
  
 ### `shuffle`
 Specifies whether to shuffle the training data. Set `TRUE` to shuffle the data; `FALSE` not to shuffle. The default value is `TRUE`. SDCA is a stochastic optimization algorithm.  If shuffling is turned on, the training data is shuffled on each iteration. 
  
  
  
 ### `checkFrequency`
 The number of iterations after which the loss function is computed and checked to determine whether it has converged. The value  specified must be a positive integer or `NULL`. If `NULL`, the actual value is automatically computed based on data set. Otherwise,  for example, if `checkFrequency = 5` is specified, then the loss function is computed and convergence is checked every 5 iterations. The computation of the loss function requires a separate complete pass over the training data. 
  
  
  
 ### ` ...`
 Additional arguments. 
  
 
 
 
