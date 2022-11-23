---
title: "rxFastLinear function (MicrosoftML) "
description: "A Stochastic Dual Coordinate Ascent (SDCA) optimization trainer  for linear binary classification and regression (MicrosoftML)."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (MicrosoftML), rxFastLinear, classification, fast, linear, regression, sdca, stochastic
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---


 # rxFastLinear: Fast Linear Model -- Stochastic Dual Coordinate Ascent 
 

A Stochastic Dual Coordinate Ascent (SDCA) optimization trainer 
for linear binary classification and regression.  

`rxFastLinear` is a trainer based on the Stochastic Dual Coordinate Ascent
(SDCA) method, a state-of-the-art optimization technique for convex objective
functions. The algorithm can be scaled for use on large out-of-memory data 
sets due to a semi-asynchronized implementation that supports multi-threading.
primal and dual updates in a separate thread. Several choices of loss
functions are also provided. The SDCA method combines several of the 
best properties and capabilities of logistic regression and SVM algorithms. 
For more information on SDCA, see the citations in the reference section.

Traditional optimization algorithms, such as stochastic gradient descent 
(SGD), optimize the empirical loss function directly. The SDCA chooses a
different approach that optimizes the dual problem instead. The dual loss
function is parametrized by per-example weights. In each iteration, when a
training example from the training data set is read, the corresponding
example weight is adjusted so that the dual loss function is optimized with
respect to the current example. No learning rate is needed by SDCA to 
determine step size as is required by various gradient descent methods.

`rxFastLinear` supports binary classification with three types of
loss functions currently: Log loss, hinge loss, and smoothed hinge loss. 
Linear regression also supports with squared loss function. Elastic net
regularization can be specified by the `l2Weight` and `l1Weight`
parameters. Note that the `l2Weight` has an effect on the rate of
convergence. In general, the larger the `l2Weight`, the faster 
SDCA converges.

Note that `rxFastLinear` is a stochastic and streaming optimization
algorithm. The result depends on the order of the training data. For
reproducible results, it is recommended that one sets `shuffle` to
`FALSE` and `trainThreads` to `1`.


 ## Usage

```   
  rxFastLinear(formula = NULL, data, type = c("binary", "regression"),
    lossFunction = NULL, l2Weight = NULL, l1Weight = NULL,
    trainThreads = NULL, convergenceTolerance = 0.1, maxIterations = NULL,
    shuffle = TRUE, checkFrequency = NULL, normalize = "auto",
    mlTransforms = NULL, mlTransformVars = NULL, rowSelection = NULL,
    transforms = NULL, transformObjects = NULL, transformFunc = NULL,
    transformVars = NULL, transformPackages = NULL, transformEnvir = NULL,
    blocksPerRead = rxGetOption("blocksPerRead"),
    reportProgress = rxGetOption("reportProgress"), verbose = 1,
    computeContext = rxGetOption("computeContext"),
    ensemble = ensembleControl(), ...)

```

 ## Arguments



 ### `formula`
 The formula described in rxFormula. Interaction terms and `F()` are currently not supported in **MicrosoftML**. 



 ### `data`
 A data source object or a character string specifying a .xdf file or a data frame object. 



 ### `type`
 Specifies the model type with a character string: `"binary"` for the default binary classification or `"regression"` for linear  regression. 



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



 ### `normalize`
 Specifies the type of automatic normalization used:  
*   `"auto"`: if normalization is needed, it is automatically  performed. This is the default value.    
*   `"no"`: no normalization is performed.   
*   `"yes"`: normalization is performed.  
*   `"warn"`: if normalization is needed, a warning message is   displayed, but normalization is not performed.  
Normalization rescales disparate data ranges to a standard scale. Feature scaling insures the distances between data points are proportional and  enables various optimization methods such as gradient descent to converge  much faster. If normalization is performed, a `MaxMin` normalizer is  used. It normalizes values in an interval [a, b] where `-1 <= a <= 0`and `0 <= b <= 1` and `b - a = 1`. This normalizer preserves  sparsity by mapping zero to zero. 



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



 ## Value



 `rxFastLinear`: A `rxFastLinear` object with the trained model.   

 `FastLinear`: A learner specification object of class `maml`
  for the Fast Linear trainer. 



 ## Notes

This algorithm is multi-threaded and will not attempt to load the entire dataset into
memory.


 ## Author(s)

Microsoft Corporation [`Microsoft Technical Support`](https://go.microsoft.com/fwlink/?LinkID=698556&clcid=0x409)



 ## References

[`Scaling Up Stochastic Dual Coordinate Ascent`](https://research.microsoft.com/um/people/mbilenko/papers/15-sasdca.pdf)


[`Stochastic Dual Coordinate Ascent Methods for Regularized Loss Minimization`](https://jmlr.csail.mit.edu/papers/volume14/shalev-shwartz13a/shalev-shwartz13a.pdf)



 ## See also

[logLoss](loss.md), [hingeLoss](loss.md),
[smoothHingeLoss](loss.md), [squaredLoss](loss.md),
[rxFastTrees](rxFastTrees.md), [rxFastForest](rxFastForest.md),
[rxLogisticRegression](rxLogisticRegression.md), [rxNeuralNet](rxNeuralNet.md),
[rxOneClassSvm](rxOneClassSvm.md), [featurizeText](featurizeText.md),
[categorical](categorical.md), [categoricalHash](categoricalHash.md),
[rxPredict.mlModel](rxPredict.md).

 ## Examples

 ```

  # Train a binary classiication model with rxFastLinear
  res1 <- rxFastLinear(isCase ~ age + parity + education + spontaneous + induced,
                    transforms = list(isCase = case == 1),
                    data = infert,
                    type = "binary")
  # Print a summary of the model
  summary(res1)

  # Score to a data frame
  scoreDF <- rxPredict(res1, data = infert, 
      extraVarsToWrite = "isCase")

  # Compute and plot the Radio Operator Curve and AUC
  roc1 <- rxRoc(actualVarName = "isCase", predVarNames = "Probability", data = scoreDF) 
  plot(roc1)
  rxAuc(roc1)

  #########################################################################
  # rxFastLinear Regression

  # Create an xdf file with the attitude data
  myXdf <- tempfile(pattern = "tempAttitude", fileext = ".xdf")
  rxDataStep(attitude, myXdf, rowsPerRead = 50, overwrite = TRUE)
  myXdfDS <- RxXdfData(file = myXdf)

  attitudeForm <- rating ~ complaints + privileges + learning + 
      raises + critical + advance

  # Estimate a regression model with rxFastLinear 
  res2 <- rxFastLinear(formula = attitudeForm,  data = myXdfDS, 
      type = "regression")

  # Score to data frame
  scoreOut2 <- rxPredict(res2, data = myXdfDS, 
      extraVarsToWrite = "rating")

  # Plot the rating versus the score with a regression line
  rxLinePlot(rating~Score, type = c("p","r"), data = scoreOut2)

  # Clean up   
  file.remove(myXdf)
```








