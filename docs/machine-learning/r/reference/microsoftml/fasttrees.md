---
title: "fastTrees function (MicrosoftML)"
description: "Creates a list containing the function name and arguments to train a  FastTree model with rxEnsemble."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (MicrosoftML), fastTrees
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
 
 
 # fastTrees: fastTrees 
 
 
Creates a list containing the function name and arguments to train a
 FastTree model with [rxEnsemble](rxEnsemble.md).
 
 
 ## Usage

```   
  fastTrees(numTrees = 100, numLeaves = 20, learningRate = 0.2,
    minSplit = 10, exampleFraction = 0.7, featureFraction = 1,
    splitFraction = 1, numBins = 255, firstUsePenalty = 0,
    gainConfLevel = 0, unbalancedSets = FALSE, trainThreads = 8,
    randomSeed = NULL, ...)
 
```
 
 ## Arguments

   
  
 ### `numTrees`
 Specifies the total number of decision trees to create in  the ensemble. By creating more decision trees, you can potentially get  better coverage, but the training time increases. The default value is 100. 
  
  
  
 ### `numLeaves`
 The maximum number of leaves (terminal nodes) that can be created in any tree. Higher values potentially increase the size of the tree and get better precision, but risk overfitting and requiring longer training times. The default value is 20. 
  
  
  
 ### `learningRate`
 Determines the size of the step taken in the direction of the gradient in each step of the learning process.  This determines how fast or slow the learner converges on the optimal solution.  If the step size is too big, you might overshoot the optimal solution.  If the step size is too small, training takes longer to converge to the best solution. 
  
  
  
 ### `minSplit`
 Minimum number of training instances required to form a leaf. That is, the minimal number of documents allowed in a leaf of a regression tree, out of the sub-sampled data. A 'split' means that features in each level of the tree (node) are randomly divided. The default value is  10. Only the number of instances is counted even if instances are weighted. 
  
  
  
 ### `exampleFraction`
 The fraction of randomly chosen instances to use for each tree. The default value is 0.7. 
  
  
  
 ### `featureFraction`
 The fraction of randomly chosen features to use for each tree. The default value is 1. 
  
  
  
 ### `splitFraction`
 The fraction of randomly chosen features to use on each split. The default value is 1. 
  
  
  
 ### `numBins`
 Maximum number of distinct values (bins) per feature. If the feature has fewer values than the number indicated, each value is placed  in its own bin.  If there are more values, the algorithm creates  `numBins` bins. 
  
  
  
 ### `firstUsePenalty`
 The feature first use penalty coefficient.  This is a form of regularization that incurs a penalty for using a new feature when creating the tree. Increase this value to create trees that don't use many features. The default value is 0. 
  
  
  
 ### `gainConfLevel`
 Tree fitting gain confidence requirement (should be in the range [0,1)). The default value is 0. 
  
  
  
 ### `unbalancedSets`
 If `TRUE`, derivatives optimized for unbalanced sets are used. Only applicable when `type` equal to `"binary"`. The default value is `FALSE`. 
  
  
  
 ### `trainThreads`
 The number of threads to use in training. The default  value is 8. 
  
  
  
 ### `randomSeed`
 Specifies the random seed. The default value is `NULL`. 
  
  
  
 ### ` ...`
 Additional arguments. 
  
 
 
 
