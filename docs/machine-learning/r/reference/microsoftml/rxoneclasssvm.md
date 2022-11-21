---
title: "rxOneClassSvm function (MicrosoftML)"
description: "Machine Learning One Class Support Vector Machines (MicrosoftML)."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (MicrosoftML), rxOneClassSvm, anomaly, detection, models
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---


 # rxOneClassSvm: OneClass SVM 
 

Machine Learning One Class Support Vector Machines


 ## Usage

```   
  rxOneClassSvm(formula = NULL, data, cacheSize = 100, kernel = rbfKernel(),
    epsilon = 0.001, nu = 0.1, shrink = TRUE, normalize = "auto",
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
 The formula as described in rxFormula. Interaction terms and `F()` are not currently supported in the **MicrosoftML**. 



 ### `data`
 A data source object or a character string specifying a .xdf file or a data frame object. 



 ### `cacheSize`
 The maximal size in MB of the cache that stores the training data. Increase this for large training sets. The default value is 100 MB. 



 ### `kernel`
 A character string representing the kernel used for computing inner products. For more information, see [maKernel](kernel.md). The following choices are available:   
*   `rbfKernel()`: Radial basis function kernel. Its parameter  represents`gamma` in the term `exp(-gamma|x-y|^2`. If not  specified, it defaults to `1` divided by the number of features used. For example, `rbfKernel(gamma = .1)`. This is the default value. 
*   `linearKernel()`: Linear kernel.   
*   `polynomialKernel()`: Polynomial kernel with parameter names `a`,  `bias`, and `deg` in the term `(a*<x,y> + bias)^deg`. The  `bias`, defaults to `0`. The degree, `deg`, defaults to  `3`. If `a` is not specified, it is set to `1` divided by the number of features. For example, `maKernelPoynomial(bias = 0, deg = ``  3)`.   
*   `sigmoidKernel()`: Sigmoid kernel with parameter names  `gamma` and `coef0` in the term `tanh(gamma*<x,y> + coef0)`.  `gamma`, defaults to `1` divided by the number of features. The  parameter `coef0` defaults to `0`.  For example,  `sigmoidKernel(gamma = .1, coef0 = 0)`.   




 ### `epsilon`
 The threshold for optimizer convergence. If the  improvement between iterations is less than the threshold, the algorithm  stops and returns the current model. The value must be greater than or equal to `.Machine$double.eps`. The default value is 0.001. 



 ### `nu`
 The trade-off between the fraction of outliers and the number of support vectors (represented by the Greek letter nu). Must be between 0 and 1, typically between 0.1 and 0.5. The default value is 0.1. 



 ### `shrink`
 Uses the shrinking heuristic if `TRUE`. In this case, some samples will be "shrunk" during the training procedure, which may speed up training. The default value is `TRUE`. 



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

detection is to identify outliers that do not belong to some target class.
This type of SVM is one-class because the training set contains only
examples from the target class. It infers what properties are normal for
the objects in the target class and from these properties predicts
which examples are unlike the normal examples. This is useful for anomaly
detection because the scarcity of training examples is the defining 
character of anomalies: typically there are very few examples of network
intrusion, fraud, or other types of anomalous behavior.


 ## Value



 `rxOneClassSvm`: A `rxOneClassSvm` object with the trained model.  

 `OneClassSvm`: A learner specification object of class `maml` 
  for the OneClass Svm trainer.  



 ## Notes

This algorithm is single-threaded and will always attempt to load the entire dataset into
memory.


 ## Author(s)

Microsoft Corporation [`Microsoft Technical Support`](https://go.microsoft.com/fwlink/?LinkID=698556&clcid=0x409)



 ## References

[`Anomaly detection`](https://en.wikipedia.org/wiki/Anomaly_detection)


[`Azure Machine Learning Studio (classic): One-Class Support Vector Machine`](/azure/machine-learning/studio-module-reference/one-class-support-vector-machine)


[`Support of a High-Dimensional Distribution`](https://research.microsoft.com/pubs/69731/tr-99-87.pdf)


[`Support Vector Algorithms`](http://www.stat.purdue.edu/~yuzhu/stat598m3/Papers/NewSVM.pdf)


[`for Support Vector Machines`](https://www.csie.ntu.edu.tw/~cjlin/papers/libsvm.pdf)



 ## See also

[rbfKernel](kernel.md), [linearKernel](kernel.md),
[polynomialKernel](kernel.md), [sigmoidKernel](kernel.md)
[rxFastTrees](rxFastTrees.md), [rxFastForest](rxFastForest.md), [rxFastLinear](rxFastLinear.md),
[rxLogisticRegression](rxLogisticRegression.md), [rxNeuralNet](rxNeuralNet.md),
[featurizeText](featurizeText.md), [categorical](categorical.md),
[categoricalHash](categoricalHash.md), [rxPredict.mlModel](rxPredict.md).

 ## Examples

 ```

  # Estimate a One-Class SVM model
  trainRows <- c(1:30, 51:80, 101:130)
  testRows = !(1:150 %in% trainRows)
  trainIris <- iris[trainRows,]
  testIris <- iris[testRows,]

  svmModel <- rxOneClassSvm(
      formula = ~Sepal.Length + Sepal.Width + Petal.Length + Petal.Width,
      data = trainIris)

  # Add additional non-iris data to the test data set
  testIris$isIris <- 1
  notIris <- data.frame(
      Sepal.Length = c(2.5, 2.6),
      Sepal.Width = c(.75, .9),
      Petal.Length = c(2.5, 2.5),
      Petal.Width = c(.8, .7),
      Species = c("not iris", "not iris"),
      isIris = 0)
  testIris <- rbind(testIris, notIris)  

  scoreDF <- rxPredict(svmModel, 
       data = testIris, extraVarsToWrite = "isIris")

  # Look at the last few observations
  tail(scoreDF)
  # Look at average scores conditioned by 'isIris'
  rxCube(Score ~ F(isIris), data = scoreDF)
```
