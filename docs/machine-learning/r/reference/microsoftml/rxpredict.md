---
title: "rxPredict.mlModel function (MicrosoftML)"
description: "Reports per-instance scoring results in a data frame or RevoScaleR data source using a trained Microsoft R Machine Learning model with a RevoScaleR data source."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (MicrosoftML), rxPredict.mlModel, manip
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---


 # rxPredict.mlModel: Score using a Microsoft R Machine Learning model 
 

Reports per-instance scoring results in a data frame or RevoScaleR data source
using a trained Microsoft R Machine Learning model with a RevoScaleR data
source.


 ## Usage

```   
 ## S3 method for class `mlModel':
rxPredict  (modelObject, data, outData = NULL,
    writeModelVars = FALSE, extraVarsToWrite = NULL, suffix = NULL,
    overwrite = FALSE, dataThreads = NULL,
    blocksPerRead = rxGetOption("blocksPerRead"),
    reportProgress = rxGetOption("reportProgress"), verbose = 1,
    computeContext = rxGetOption("computeContext"), ...)

```

 ## Arguments



 ### `modelObject`
 A model information object returned from a MicrosoftML model. For example, an object returned from [rxFastTrees](rxFastTrees.md) or [rxLogisticRegression](rxLogisticRegression.md). 



 ### `data`
 A **RevoScaleR** data source object, a data frame, or the path to a `.xdf` file. 



 ### `outData`
 Output text or xdf file name or an `RxDataSource` with write capabilities in which to store predictions. If `NULL`, a data frame is returned. The default value is `NULL`. 



 ### `writeModelVars`
 If `TRUE`, variables in the model are written to the output data set in addition to the scoring variables. If variables from the input data set are transformed in the model, the transformed variables are also included. The default value is `FALSE`. 



 ### `extraVarsToWrite`
 `NULL` or character vector of additional variables names from the input data to include in the `outData`. If `writeModelVars` is `TRUE`, model variables are included as well. The default value is `NULL`. 



 ### `suffix`
 A character string specifying suffix to append to the created  scoring variable(s) or `NULL` in there is no suffix. The default  value is `NULL`. 



 ### `overwrite`
 If `TRUE`, an existing `outData` is overwritten; if `FALSE` an existing `outData` is not overwritten. The default  value is `FALSE`. 



 ### `dataThreads`
 An integer specifying the desired degree of parallelism in the data pipeline. If `NULL`, the number of threads used is determined internally. The default value is `NULL`. 



 ### `blocksPerRead`
 Specifies the number of blocks to read for each chunk  of data read from the data source. 



 ### `reportProgress`
 An integer value that specifies the level of reporting  on the row processing progress:   
*   `0`: no progress is reported.     
*   `1`: the number of processed rows is printed and updated.   
*   `2`: rows processed and timings are reported.  
*   `3`: rows processed and all timings are reported.   
 The default value is `1`. 



 ### `verbose`
 An integer value that specifies the amount of output wanted. If `0`, no verbose output is printed during calculations. Integer  values from `1` to `4` provide increasing amounts of information.  The default value is `1`. 



 ### `computeContext`
 Sets the context in which computations are executed, specified with a valid RxComputeContext. Currently local and RxInSqlServer compute contexts are supported. 



 ### ` ...`
 Additional arguments to be passed directly to the Microsoft Compute Engine. 



 ## Details

The following items are reported in the output by default: scoring on three 
variables for the binary classifiers: PredictedLabel, Score, and Probability;
the Score for oneClassSvm and regression classifiers; PredictedLabel for 
Multi-class classifiers, plus a variable for each category prepended by the
Score.


 ## Value

A data frame or an RxDataSource object
representing the created output data. By default, output from scoring binary
classifiers include three variables: `PredictedLabel`,
`Score`, and `Probability`; `rxOneClassSvm` and regression 
include one variable: `Score`; and multi-class classifiers include
`PredictedLabel` plus a variable for each category prepended by
`Score`. If a `suffix` is provided, it is added to the end
of these output variable names.

 ## Author(s)

Microsoft Corporation [`Microsoft Technical Support`](https://go.microsoft.com/fwlink/?LinkID=698556&clcid=0x409)



 ## See also

[rxFastTrees](rxFastTrees.md), [rxFastForest](rxFastForest.md),
[rxLogisticRegression](rxLogisticRegression.md), [rxNeuralNet](rxNeuralNet.md),
[rxOneClassSvm](rxOneClassSvm.md).

 ## Examples

 ```


  # Estimate a logistic regression model
  infert1 <- infert
  infert1$isCase <- (infert1$case == 1)
  myModelInfo <- rxLogisticRegression(formula = isCase ~ age + parity + education + spontaneous + induced,
                         data = infert1)

  # Create an xdf file with per-instance results using rxPredict
  xdfOut <- tempfile(pattern = "scoreOut", fileext = ".xdf")
  scoreDS <- rxPredict(myModelInfo, data = infert1,
      outData = xdfOut, overwrite = TRUE,
      extraVarsToWrite = c("isCase", "Probability"))

  # Summarize results with an ROC curve
  rxRocCurve(actualVarName = "isCase", predVarNames = "Probability", data = scoreDS)

  # Use the built-in data set 'airquality' to create test and train data
  DF <- airquality[!is.na(airquality$Ozone), ]  
  DF$Ozone <- as.numeric(DF$Ozone)
  set.seed(12)
  randomSplit <- rnorm(nrow(DF))
  trainAir <- DF[randomSplit >= 0,]
  testAir <- DF[randomSplit < 0,]
  airFormula <- Ozone ~ Solar.R + Wind + Temp

  # Regression Fast Tree for train data
  fastTreeReg <- rxFastTrees(airFormula, type = "regression", 
      data = trainAir)  

  # Put score and model variables in data frame, including the model variables
  # Add the suffix "Pred" to the new variable
  fastTreeScoreDF <- rxPredict(fastTreeReg, data = testAir, 
      writeModelVars = TRUE, suffix = "Pred")

  rxGetVarInfo(fastTreeScoreDF)

  # Clean-up
  file.remove(xdfOut)
```



