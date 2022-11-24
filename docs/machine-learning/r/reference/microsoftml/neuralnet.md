---
title: "neuralNet function (MicrosoftML)"
description: "Creates a list containing the function name and arguments to train a NeuralNet model with rxEnsemble."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (MicrosoftML), neuralNet
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
 
 
 # neuralNet: neuralNet 
 
 
Creates a list containing the function name and arguments to train a
 NeuralNet model with [rxEnsemble](rxEnsemble.md).
 
 
 ## Usage

```   
  neuralNet(numHiddenNodes = 100, numIterations = 100, optimizer = sgd(),
    netDefinition = NULL, initWtsDiameter = 0.1, maxNorm = 0,
    acceleration = c("sse", "gpu"), miniBatchSize = 1, ...)
 
```
 
 ## Arguments

   
  
 ### `numHiddenNodes`
 The default number of hidden nodes in the neural net. The default value is 100. 
  
  
  
 ### `numIterations`
 The number of iterations on the full training set. The default value is 100. 
  
  
  
 ### `optimizer`
 A list specifying either the `sgd` or `adaptive` optimization algorithm. This list can be created using [sgd](optimizer.md) or [adaDeltaSgd](optimizer.md). The default value is `sgd`. 
  
  
  
 ### `netDefinition`
 The Net# definition of the structure of the neural network. For more information about the Net# language, see   [`Reference Guide`](/azure/machine-learning/classic/azure-ml-netsharp-reference-guide)  
  
  
  
 ### `initWtsDiameter`
 Sets the initial weights diameter that specifies  the range from which values are drawn for the initial learning weights.    The weights are initialized randomly from within this range. The default value is 0.1. 
  
  
  
 ### `maxNorm`
 Specifies an upper bound to constrain the norm of the incoming  weight vector at each hidden unit. This can be important in maxout neural networks and in cases where training produces unbounded weights. 
  
  
  
 ### `acceleration`
 Specifies the type of hardware acceleration to use.  Possible values are "sse" and "gpu".  For GPU acceleration, it is recommended to use a miniBatchSize greater than one.  If you want to use the GPU acceleration, there are additional manual setup steps are required:    
*  Download and install NVidia CUDA Toolkit 6.5  ([`CUDA Toolkit`](https://developer.nvidia.com/cuda-toolkit-65) ).  
*  Download and install NVidia cuDNN v2 Library  ([`cudnn Library`](https://developer.nvidia.com/rdp/cudnn-archive) ).  
*  Find the libs directory of the MicrosoftRML package by calling `system.file("mxLibs/x64", package = "MicrosoftML")`.  
*  Copy cublas64_65.dll, cudart64_65.dll and cusparse64_65.dll from the CUDA Toolkit 6.5 into the libs directory of the MicrosoftML package. 
*  Copy cudnn64_65.dll from the cuDNN v2 Library into the libs directory of the MicrosoftML package. 
 
  
  
  
 ### `miniBatchSize`
 Sets the mini-batch size. Recommended values are between  1 and 256. This parameter is only used when the acceleration is GPU. Setting  this parameter to a higher value improves the speed of training, but it might negatively affect the accuracy. The default value is 1. 
  
  
  
 ### ` ...`
 Additional arguments. 
  
 
 
 
