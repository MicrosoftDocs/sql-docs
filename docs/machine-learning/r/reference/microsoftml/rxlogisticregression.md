---
title: "rxLogisticRegression function (MicrosoftML)"
description: "Machine Learning Logistic Regression (MicrosoftML)."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (MicrosoftML), rxLogisticRegression, classification, models
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---


 # rxLogisticRegression: Logistic Regression 
 

Machine Learning Logistic Regression


 ## Usage

```   
  rxLogisticRegression(formula = NULL, data, type = c("binary", "multiClass"),
    l2Weight = 1, l1Weight = 1, optTol = 1e-07, memorySize = 20,
    initWtsScale = 0, maxIterations = 2147483647, showTrainingStats = FALSE,
    sgdInitTol = 0, trainThreads = NULL, denseOptimizer = FALSE,
    normalize = "auto", mlTransforms = NULL, mlTransformVars = NULL,
    rowSelection = NULL, transforms = NULL, transformObjects = NULL,
    transformFunc = NULL, transformVars = NULL, transformPackages = NULL,
    transformEnvir = NULL, blocksPerRead = rxGetOption("blocksPerRead"),
    reportProgress = rxGetOption("reportProgress"), verbose = 1,
    computeContext = rxGetOption("computeContext"),
    ensemble = ensembleControl(), ...)

```

 ## Arguments



 ### `formula`
 The formula as described in rxFormula. Interaction terms and `F()` are not currently supported in the **MicrosoftML**. 



 ### `data`
 A data source object or a character string specifying a .xdf file or a data frame object. 



 ### `type`
 A character string that specifies the type of Logistic Regression:  `"binary"` for the default binary classification logistic regression or `"multi"` for multinomial logistic regression. 



 ### `l2Weight`
 The L2 regularization weight. Its value must be greater than  or equal to `0` and the default value is set to `1`. 



 ### `l1Weight`
 The L1 regularization weight. Its value must be greater than  or equal to `0` and the default value is set to `1`. 



 ### `optTol`
 Threshold value for optimizer convergence. If the improvement between iterations is less than the threshold, the algorithm stops and returns the current model. Smaller values are slower, but more accurate. The default value is `1e-07`. 



 ### `memorySize`
 Memory size for L-BFGS, specifying the number of past positions and gradients to store for the computation of the next step. This optimization parameter limits the amount of memory that is used to compute the magnitude and direction of the next step. When you specify less memory,  training is faster but less accurate. Must be greater than or equal to  `1` and the default value is `20`. 



 ### `initWtsScale`
 Sets the initial weights diameter that specifies  the range from which values are drawn for the initial weights. These   weights are initialized randomly from within this range. For  example, if the diameter is specified to be `d`, then the weights  are uniformly distributed between `-d/2` and `d/2`. The default value is `0`, which specifies that all the  weights are  initialized to `0`. 



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



 ### `normalize`
 Specifies the type of automatic normalization used:  
*   `"auto"`: if normalization is needed, it is performed  automatically. This is the default choice.    
*   `"no"`: no normalization is performed.  
*   `"yes"`: normalization is performed.    
*   `"warn"`: if normalization is needed, a warning  message is displayed, but normalization is not performed.  
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



 ## Details

Logistic Regression is a classification method used to predict 
the value of a categorical dependent variable from its relationship to one
or more independent variables assumed to have a logistic distribution. If
the dependent variable has only two possible values (success/failure), 
then the logistic regression is binary. If the dependent variable has 
more than two possible values (blood type given diagnostic test results),
then the logistic regression is multinomial. 

The optimization technique used for `rxLogisticRegression` is the 
limited memory Broyden-Fletcher-Goldfarb-Shanno (L-BFGS). Both the L-BFGS
and regular BFGS algorithms use quasi-Newtonian methods to estimate the 
computationally intensive Hessian matrix in the equation used by Newton's
method to calculate steps. But the L-BFGS approximation uses only a limited
amount of memory to compute the next step direction, so that it is especially 
suited for problems with a large number of variables. The `memorySize`
parameter specifies the number of past positions and gradients to store for
use in the computation of the next step. 

This learner can use elastic net regularization: a linear combination of L1
(lasso) and L2 (ridge) regularizations. Regularization is a method that 
can render an ill-posed problem more tractable by imposing constraints that
provide information to supplement the data and that prevents overfitting by
penalizing models with extreme coefficient values. This can improve the 
generalization of the model learned by selecting the optimal complexity 
in the bias-variance tradeoff. Regularization works by
adding the penalty that is associated with coefficient values to the error
of the hypothesis. An accurate model with extreme coefficient values would
be penalized more, but a less accurate model with more conservative values
would be penalized less. L1 and L2 regularization have different effects
and uses that are complementary in certain respects.  


`l1Weight`: can be applied to sparse models, when working
 with high-dimensional data. It pulls small weights associated features
 that are relatively unimportant towards 0.

`l2Weight`: is preferable for data that is not sparse. It pulls
 large weights towards zero. 


Adding the ridge penalty to the regularization overcomes some of lasso's  
limitations. It can improve its predictive accuracy, for example, when 
the number of predictors is greater than the sample size.
If `x = l1Weight` and `y = l2Weight`, `ax + by = c`
defines the linear span of the regularization terms. The default values 
of x and y are both `1`. An aggressive regularization can harm predictive
capacity by excluding important variables out of the model. So choosing the 
optimal values for the regularization parameters is important for the 
performance of the logistic regression model.


 ## Value



 `rxLogisticRegression`: A `rxLogisticRegression` object
  with the trained model.

 `LogisticReg`: A learner specification object of class `maml`
  for the Logistic Reg trainer. 



 ## Notes

This algorithm will attempt to load the entire dataset into memory
when `trainThreads > 1` (multi-threading).


 ## Author(s)

Microsoft Corporation [`Microsoft Technical Support`](https://go.microsoft.com/fwlink/?LinkID=698556&clcid=0x409)



 ## References

[` Wikipedia: L-BFGS`](https://en.wikipedia.org/wiki/L-BFGS)


[`regression`](https://en.wikipedia.org/wiki/Logistic_regression)


[`Training of L1-Regularized Log-Linear Models`](https://research.microsoft.com/apps/pubs/default.aspx?id=78900)


[`and L2 Regularization for Machine Learning`](/archive/msdn-magazine/2015/february/test-run-l1-and-l2-regularization-for-machine-learning)



 ## See also

[rxFastTrees](rxFastTrees.md), [rxFastForest](rxFastForest.md),
[rxFastLinear](rxFastLinear.md), [rxNeuralNet](rxNeuralNet.md),
[rxOneClassSvm](rxOneClassSvm.md), [featurizeText](featurizeText.md),
[categorical](categorical.md), [categoricalHash](categoricalHash.md),
[rxPredict.mlModel](rxPredict.md).

 ## Examples

 ```

  # Estimate a logistic regression model
  logitModel <- rxLogisticRegression(isCase ~ age + parity + education + spontaneous + induced,
                    transforms = list(isCase = case == 1),
                    data = infert)
  # Print a summary of the model
  summary(logitModel)

  # Score to a data frame
  scoreDF <- rxPredict(logitModel, data = infert, 
      extraVarsToWrite = "isCase")

  # Compute and plot the Radio Operator Curve and AUC
  roc1 <- rxRoc(actualVarName = "isCase", predVarNames = "Probability", data = scoreDF) 
  plot(roc1)
  rxAuc(roc1)

  #######################################################################################
  # Multi-class logistic regression  
  testObs <- rnorm(nrow(iris)) > 0
  testIris <- iris[testObs,]
  trainIris <- iris[!testObs,]
  multiLogit <- rxLogisticRegression(
      formula = Species~Sepal.Length + Sepal.Width + Petal.Length + Petal.Width,
      type = "multiClass", data = trainIris)

  # Score the model
  scoreMultiDF <- rxPredict(multiLogit, data = testIris, 
      extraVarsToWrite = "Species")    
  # Print the first rows of the data frame with scores
  head(scoreMultiDF)
  # Look at confusion matrix
  table(scoreMultiDF$Species, scoreMultiDF$PredictedLabel)

  # Look at the observations with incorrect predictions
  badPrediction = scoreMultiDF$Species != scoreMultiDF$PredictedLabel
  scoreMultiDF[badPrediction,]
```
