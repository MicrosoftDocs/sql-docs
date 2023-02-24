---
title: "fastForest function (MicrosoftML)"
description: "Creates a list containing the function name and arguments to train a  FastForest model with rxEnsemble."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (MicrosoftML), fastForest
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
 
 
 # fastForest: fastForest 
 
 
Creates a list containing the function name and arguments to train a
 FastForest model with [rxEnsemble](rxEnsemble.md).
 
 
 ## Usage

```   
  fastForest(numTrees = 100, numLeaves = 20, minSplit = 10,
    exampleFraction = 0.7, featureFraction = 0.7, splitFraction = 0.7,
    numBins = 255, firstUsePenalty = 0, gainConfLevel = 0,
    trainThreads = 8, randomSeed = NULL, ...)
 
```
 
 ## Arguments

   
  
 ### `numTrees`
 Specifies the total number of decision trees to create in  the ensemble. By creating more decision trees, you can potentially get  better coverage, but the training time increases. The default value is 100. 
  
  
  
 ### `numLeaves`
 The maximum number of leaves (terminal nodes) that can be created in any tree. Higher values potentially increase the size of the tree and get better precision, but risk overfitting and requiring longer training times. The default value is 20. 
  
  
  
 ### `minSplit`
 Minimum number of training instances required to form a leaf. That is, the minimal number of documents allowed in a leaf of a regression tree, out of the sub-sampled data. A 'split' means that features in each level of the tree (node) are randomly divided. The default value is 10. 
  
  
  
 ### `exampleFraction`
 The fraction of randomly chosen instances to use for each tree. The default value is 0.7. 
  
  
  
 ### `featureFraction`
 The fraction of randomly chosen features to use for each tree. The default value is 0.7. 
  
  
  
 ### `splitFraction`
 The fraction of randomly chosen features to use on each split. The default value is 0.7. 
  
  
  
 ### `numBins`
 Maximum number of distinct values (bins) per feature. The default value is 255. 
  
  
  
 ### `firstUsePenalty`
 The feature first use penalty coefficient. The default  value is 0. 
  
  
  
 ### `gainConfLevel`
 Tree fitting gain confidence requirement (should be in the range [0,1)). The default value is 0. 
  
  
  
 ### `trainThreads`
 The number of threads to use in training. If `NULL`is specified, the number of threads to use is determined internally.  The default value is `NULL`. 
  
  
  
 ### `randomSeed`
 Specifies the random seed. The default value is `NULL`. 
  
  
  
 ### ` ...`
 Additional arguments. 
  
 
 
 
