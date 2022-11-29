---
title: "rxEnsemble function (MicrosoftML)"
description: "Train an ensemble of models (MicrosoftML)."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (MicrosoftML), rxEnsemble
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---


 # rxEnsemble: Ensembles 
 

Train an ensemble of models


 ## Usage

```   
  rxEnsemble(formula = NULL, data, trainers, type = c("binary", "regression",
    "multiClass", "anomaly"), randomSeed = NULL,
    modelCount = length(trainers), replace = FALSE, sampRate = NULL,
    splitData = FALSE, combineMethod = c("median", "average", "vote"),
    maxCalibration = 1e+05, mlTransforms = NULL, mlTransformVars = NULL,
    rowSelection = NULL, transforms = NULL, transformObjects = NULL,
    transformFunc = NULL, transformVars = NULL, transformPackages = NULL,
    transformEnvir = NULL, blocksPerRead = rxGetOption("blocksPerRead"),
    reportProgress = rxGetOption("reportProgress"), verbose = 1,
    computeContext = rxGetOption("computeContext"), ...)

```

 ## Arguments



 ### `formula`
 The formula as described in rxFormula. Interaction terms and `F()` are not currently supported in the **MicrosoftML**. 



 ### `data`
 A data source object or a character string specifying a .xdffile or a data frame object. Alternatively, it can be a list of data sources indicating each model should be trained using one of the data sources in the list.  In this case, the length of the data list must be equal to `modelCount`. 



 ### `trainers`
 A list of trainers with their arguments. The trainers are created by using [fastTrees](fastTrees.md), [fastForest](fastForest.md), [fastLinear](fastLinear.md), [logisticRegression](logisticRegression.md) or [neuralNet](neuralNet.md). 



 ### `type`
 A character string that specifies the type of ensemble: `"binary"` for Binary Classification or `"regression"` for Regression. 



 ### `randomSeed`
 Specifies the random seed. The default value is `NULL`. 



 ### `modelCount`
 Specifies the number of models to train. If this number is greater  than the length of the trainers list, the trainers list is duplicated to match `modelCount`. 



 ### `replace`
 A logical value specifying if the sampling of observations should be done  with or without replacement.  The default value is /codeFALSE. 



 ### `sampRate`
 a scalar of positive value specifying the percentage of observations to sample for  each trainer. The default is 1.0 for sampling with replacement (i.e., replace=TRUE) and 0.632  for sampling without replacement (i.e., replace=FALSE). When splitData is TRUE, the default of sampRate is 1.0 (no sampling is done before splitting). 



 ### `splitData`
 A logical value specifying whether or not to train the base models on non-overlapping partitions.  The default is `FALSE`. It is available only for `RxSpark` compute context and ignored for others. 



 ### `combineMethod`
 Specifies the method used to combine the models:   
*   `median` to compute the median of the individual model outputs,   
*   `average` to compute the average of the individual model outputs and   
*   `vote` to compute (pos-neg) / the total number of models, where 'pos'   is the number of positive outputs and 'neg' is the number of negative outputs. 




 ### `maxCalibration`
 Specifies the maximum number of examples to use for calibration. This argument is ignored for all tasks other than binary classification. 



 ### `mlTransforms`
 Specifies a list of MicrosoftML transforms to be performed on the data before training or `NULL` if no transforms are  to be performed. Transforms that require an additional pass over the data  (such as [featurizeText](featurizeText.md), [categorical](categorical.md)) are not allowed. These transformations are performed after any specified R transformations. The default value is `NULL`. 



 ### `mlTransformVars`
 Specifies a character vector of variable names to be used in `mlTransforms` or `NULL` if none are to be used. The default value is `NULL`. 



 ### `rowSelection`
 Specifies the rows (observations) from the data set that are to be used by the model with the name of a logical variable from the  data set (in quotes) or with a logical expression using variables in the    data set. For example, `rowSelection = "old"` will only use observations in which the value of the variable `old` is `TRUE`. `rowSelection = (age > 20) & (age < 65) & (log(income) > 10)` only uses observations in which the value of the `age` variable is between 20 and 65 and the value of the `log` of the `income` variable is greater than 10. The row selection is performed after processing any data transformations (see the arguments `transforms` or `transformFunc`). As with all expressions, `rowSelection` can be defined outside of the function call using the expression function. 



 ### `transforms`
 An expression of the form `list(name = expression, ``...)` that represents the first round of variable transformations. As with  all expressions, `transforms` (or `rowSelection`) can be defined outside of the function call using the expression function. The default value is `NULL`. 



 ### `transformObjects`
 A named list that contains objects that can be referenced by `transforms`, `transformsFunc`, and `rowSelection`. The default value is `NULL`. 



 ### `transformFunc`
 The variable transformation function. See rxTransform for details. The default value is `NULL`. 



 ### `transformVars`
 A character vector of input data set variables needed for the transformation function. See rxTransform for details. The default value is `NULL`. 



 ### `transformPackages`
 A character vector specifying additional R packages (outside of those specified in `rxGetOption("transformPackages")`) to be made available and preloaded for use in variable transformation functions. For example, those explicitly defined in **RevoScaleR** functions via their `transforms` and `transformFunc` arguments or those defined implicitly via their `formula` or `rowSelection` arguments.  The `transformPackages` argument may also be `NULL`, indicating that no packages outside `rxGetOption("transformPackages")` are preloaded. The default value is `NULL`. 



 ### `transformEnvir`
 A user-defined environment to serve as a parent to all environments developed internally and used for variable data transformation. If `transformEnvir = NULL`, a new "hash" environment with parent `baseenv()` is used instead. The default value is `NULL`. 



 ### `blocksPerRead`
 Specifies the number of blocks to read for each chunk  of data read from the data source. 



 ### `reportProgress`
 An integer value that specifies the level of reporting on the row processing progress:   
*   `0`: no progress is reported.      
*   `1`: the number of processed rows is printed and updated.    
*   `2`: rows processed and timings are reported.   
*   `3`: rows processed and all timings are reported.  




 ### `verbose`
 An integer value that specifies the amount of output wanted. If `0`, no verbose output is printed during calculations. Integer  values from `1` to `4` provide increasing amounts of information.   The default value is `1`. 



 ### `computeContext`
 Sets the context in which computations are executed, specified with a valid RxComputeContext. Currently local and RxSpark compute contexts are supported. When RxSpark is specified, the training of the models is done in a distributed way, and the ensembling is done locally. Note that the compute context cannot be non-waiting. 



 ### ` ...`
 Additional arguments to be passed directly to the Microsoft Compute Engine. 



 ## Details

/coderxEnsemble is a function that trains a number of models
of various kinds to obtain better predictive performance than could be
obtained from a single model.


 ## Value

A `rxEnsemble` object with the trained ensemble model.

 ## Examples

 ```

  # Create an ensemble of regression rxFastTrees models

  # use xdf data source
  dataFile <- file.path(rxGetOption("sampleDataDir"), "claims4blocks.xdf")
  rxGetInfo(dataFile, getVarInfo = TRUE, getBlockSizes = TRUE)
  form <- cost ~ age + type + number

  rxSetComputeContext("localpar")
  rxGetComputeContext()

  # build an ensemble model that contains three 'rxFastTrees' models with different parameters
  ensemble <- rxEnsemble(
      formula = form,
      data = dataFile,
      type = "regression",
      trainers = list(fastTrees(), fastTrees(numTrees = 60), fastTrees(learningRate = 0.1)), #a list of trainers with their arguments.
      replace = TRUE # Indicates using a bootstrap sample for each trainer
      )

  # use text data source
  colInfo <- list(DayOfWeek = list(type = "factor", levels = c("Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday")))

  source <- system.file("SampleData/AirlineDemoSmall.csv", package = "RevoScaleR")
  data <- RxTextData(source, missingValueString = "M", colInfo = colInfo)

  # When 'distributed' is TRUE distributed data source is created
  distributed <- FALSE
  if (distributed) {
      bigDataDirRoot <- "/share"
      inputDir <- file.path(bigDataDirRoot, "AirlineDemoSmall")
      rxHadoopMakeDir(inputDir)
      rxHadoopCopyFromLocal(source, inputDir)
      hdfsFS <- RxHdfsFileSystem()
      data <- RxTextData(file = inputDir, missingValueString = "M", colInfo = colInfo, fileSystem = hdfsFS)
  }

  # When 'distributed' is TRUE training is distributed
  if (distributed) {
      cc <- rxSetComputeContext(RxSpark())
  } else {
      cc <- rxGetComputeContext()
  }

  ensemble <- rxEnsemble(
      formula = ArrDelay ~ DayOfWeek,
      data = data,
      type = "regression",
      trainers = list(fastTrees(), fastTrees(numTrees = 60), fastTrees(learningRate = 0.1)), # The ensemble will contain three 'rxFastTrees' models
      replace = TRUE # Indicates using a bootstrap sample for each trainer
      )

  # Change the compute context back to previous for scoring
  rxSetComputeContext(cc)

  # Put score and model variables in data frame
  scores <- rxPredict(ensemble, data = data, writeModelVars = TRUE)

  # Plot actual versus predicted values with smoothed line
  rxLinePlot(Score ~ ArrDelay, type = c("p", "smooth"), data = scores)
```


