---
title: "rxFastForest function (MicrosoftML)"
description: "Machine Learning Fast Forest (MicrosoftML)."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (MicrosoftML), rxFastForest, classification, models, regression
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---


 # rxFastForest: Fast Forest 
 

Machine Learning Fast Forest


 ## Usage

```   
  rxFastForest(formula = NULL, data, type = c("binary", "regression"),
    numTrees = 100, numLeaves = 20, minSplit = 10, exampleFraction = 0.7,
    featureFraction = 0.7, splitFraction = 0.7, numBins = 255,
    firstUsePenalty = 0, gainConfLevel = 0, trainThreads = 8,
    randomSeed = NULL, mlTransforms = NULL, mlTransformVars = NULL,
    rowSelection = NULL, transforms = NULL, transformObjects = NULL,
    transformFunc = NULL, transformVars = NULL, transformPackages = NULL,
    transformEnvir = NULL, blocksPerRead = rxGetOption("blocksPerRead"),
    reportProgress = rxGetOption("reportProgress"), verbose = 2,
    computeContext = rxGetOption("computeContext"),
    ensemble = ensembleControl(), ...)

```

 ## Arguments



 ### `formula`
 The formula as described in rxFormula. Interaction terms and `F()` are not currently supported in the **MicrosoftML**. 



 ### `data`
 A data source object or a character string specifying a .xdf file or a data frame object. 



 ### `type`
 A character string denoting Fast Tree type:   
*   `"binary"` for the default Fast Tree Binary Classification or  
*   `"regression"` for Fast Tree Regression.  




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



 ### `mlTransforms`
 Specifies a list of MicrosoftML transforms to be performed on the data before training or `NULL` if no transforms are  to be performed. See [featurizeText](featurizeText.md), [categorical](categorical.md), and [categoricalHash](categoricalHash.md), for transformations that are supported. These transformations are performed after any specified R transformations. The default value is `NULL`. 



 ### `mlTransformVars`
 Specifies a character vector of variable names to be used in `mlTransforms` or `NULL` if none are to be used. The default value is `NULL`. 



 ### `rowSelection`
 Specifies the rows (observations) from the data set that are to be used by the model with the name of a logical variable from the  data set (in quotes) or with a logical expression using variables in the    data set. For example, `rowSelection = "old"` will only use observations in which the value of the variable `old` is `TRUE`. `rowSelection = (age > 20) & (age < 65) & (log(income) > 10)` only uses observations in which the value of the `age` variable is between 20 and 65 and the value of the `log` of the `income` variable is greater than 10. The row selection is performed after processing any data transformations (see the arguments `transforms` or `transformFunc`). As with all expressions, `rowSelection` can be defined outside of the function call using the expression function. 



 ### `transforms`
 An expression of the form `list(name = expression, ``...)` that represents the first round of variable transformations. As with  all expressions, `transforms` (or `rowSelection`) can be defined outside of the function call using the expression function. 



 ### `transformObjects`
 A named list that contains objects that can be referenced by `transforms`, `transformsFunc`, and `rowSelection`. 



 ### `transformFunc`
 The variable transformation function. See rxTransform for details. 



 ### `transformVars`
 A character vector of input data set variables needed for the transformation function. See rxTransform for details. 



 ### `transformPackages`
 A character vector specifying additional R packages (outside of those specified in `rxGetOption("transformPackages")`) to be made available and preloaded for use in variable transformation functions. For example, those explicitly defined in **RevoScaleR** functions via their `transforms` and `transformFunc` arguments or those defined implicitly via their `formula` or `rowSelection` arguments.  The `transformPackages` argument may also be `NULL`, indicating that no packages outside `rxGetOption("transformPackages")` are preloaded. 



 ### `transformEnvir`
 A user-defined environment to serve as a parent to all environments developed internally and used for variable data transformation. If `transformEnvir = NULL`, a new "hash" environment with parent `baseenv()` is used instead. 



 ### `blocksPerRead`
 Specifies the number of blocks to read for each chunk  of data read from the data source. 



 ### `reportProgress`
 An integer value that specifies the level of reporting on the row processing progress:   
*   `0`: no progress is reported.      
*   `1`: the number of processed rows is printed and updated.    
*   `2`: rows processed and timings are reported.   
*   `3`: rows processed and all timings are reported.  




 ### `verbose`
 An integer value that specifies the amount of output wanted. If `0`, no verbose output is printed during calculations. Integer  values from `1` to `4` provide increasing amounts of information. 



 ### `computeContext`
 Sets the context in which computations are executed, specified with a valid RxComputeContext. Currently local and RxInSqlServer compute contexts are supported. 



 ### `ensemble`
 Control parameters for ensembling. 



 ### ` ...`
 Additional arguments to be passed directly to the Microsoft Compute Engine. 



 ## Details

Decision trees are non-parametric models that perform a sequence  
of simple tests on inputs. This decision procedure maps them to outputs 
found in the training dataset whose inputs were similar to the instance 
being processed. A decision is made at each node of the binary tree data
structure based on a measure of similarity that maps each instance 
recursively through the branches of the tree until the appropriate leaf 
node is reached and the output decision returned.

Decision trees have several advantages: 


They are efficient in both computation and memory usage during 
 training and prediction.  

They can represent non-linear decision boundaries.  

They perform integrated feature selection and classification.

They are resilient in the presence of noisy features. 



Fast forest regression is a random forest and quantile regression forest
implementation using the regression tree learner in [rxFastTrees](rxFastTrees.md).
The model consists of an ensemble of decision trees. Each tree in a decision
forest outputs a Gaussian distribution by way of prediction. An aggregation
is performed over the ensemble of trees to find a Gaussian distribution
closest to the combined distribution for all trees in the model.

This decision forest classifier consists of an ensemble of decision trees.
Generally, ensemble models provide better coverage and accuracy than single
decision trees. Each tree in a decision forest outputs a Gaussian distribution
by way of prediction. An aggregation is performed over the ensemble of trees
to find a Gaussian distribution closest to the combined distribution
for all trees in the model.


 ## Value



 `rxFastForest`: A `rxFastForest` object with the trained model.

 `FastForest`: A learner specification object of class `maml`
  for the Fast Forest trainer. 



 ## Notes

This algorithm is multi-threaded and will always attempt to load the entire dataset into
memory.


 ## Author(s)

Microsoft Corporation [`Microsoft Technical Support`](https://go.microsoft.com/fwlink/?LinkID=698556&clcid=0x409)



 ## References

[`Wikipedia: Random forest`](https://en.wikipedia.org/wiki/Random_forest)


[`Quantile regression forest`](http://jmlr.org/papers/volume7/meinshausen06a/meinshausen06a.pdf)


[`From Stumps to Trees to Forests`](/archive/blogs/machinelearning/from-stumps-to-trees-to-forests)



 ## See also

[rxFastTrees](rxFastTrees.md), [rxFastLinear](rxFastLinear.md),
[rxLogisticRegression](rxLogisticRegression.md), [rxNeuralNet](rxNeuralNet.md),
[rxOneClassSvm](rxOneClassSvm.md), [featurizeText](featurizeText.md),
[categorical](categorical.md), [categoricalHash](categoricalHash.md),
[rxPredict.mlModel](rxPredict.md).

 ## Examples

 ```

  # Estimate a binary classification forest
  infert1 <- infert
  infert1$isCase = (infert1$case == 1)
  forestModel <- rxFastForest(formula = isCase ~ age + parity + education + spontaneous + induced,
          data = infert1)

  # Create text file with per-instance results using rxPredict
  txtOutFile <- tempfile(pattern = "scoreOut", fileext = ".txt")
  txtOutDS <- RxTextData(file = txtOutFile)
  scoreDS <- rxPredict(forestModel, data = infert1,
     extraVarsToWrite = c("isCase", "Score"), outData = txtOutDS)

  # Print the fist ten rows   
  rxDataStep(scoreDS, numRows = 10)

  # Clean-up
  file.remove(txtOutFile)

  ######################################################################
  # Estimate a regression fast forest

  # Use the built-in data set 'airquality' to create test and train data
  DF <- airquality[!is.na(airquality$Ozone), ]  
  DF$Ozone <- as.numeric(DF$Ozone)
  randomSplit <- rnorm(nrow(DF))
  trainAir <- DF[randomSplit >= 0,]
  testAir <- DF[randomSplit < 0,]
  airFormula <- Ozone ~ Solar.R + Wind + Temp

  # Regression Fast Forest for train data
  rxFastForestReg <- rxFastForest(airFormula, type = "regression", 
      data = trainAir)  

  # Put score and model variables in data frame
  rxFastForestScoreDF <- rxPredict(rxFastForestReg, data = testAir, 
      writeModelVars = TRUE)

  # Plot actual versus predicted values with smoothed line
  rxLinePlot(Score ~ Ozone, type = c("p", "smooth"), data = rxFastForestScoreDF)
```
