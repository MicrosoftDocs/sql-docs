---
title: "rxFeaturize function (MicrosoftML) "
description: "Transforms data from an input data set to an output data set (MicrosoftML)."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (MicrosoftML), rxFeaturize, manip
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---


 # rxFeaturize: Data Transformation for RevoScaleR data sources 
 

Transforms data from an input data set to an output data set.


 ## Usage

```   
  rxFeaturize(data, outData = NULL, overwrite = FALSE, dataThreads = NULL,
    randomSeed = NULL, maxSlots = 5000, mlTransforms = NULL,
    mlTransformVars = NULL, rowSelection = NULL, transforms = NULL,
    transformObjects = NULL, transformFunc = NULL, transformVars = NULL,
    transformPackages = NULL, transformEnvir = NULL,
    blocksPerRead = rxGetOption("blocksPerRead"),
    reportProgress = rxGetOption("reportProgress"), verbose = 1,
    computeContext = rxGetOption("computeContext"), ...)

```

 ## Arguments



 ### `data`
 A **RevoScaleR** data source object, a data frame, or the path to a `.xdf` file. 



 ### `outData`
 Output text or xdf file name or an `RxDataSource` with write capabilities in which to store transformed data. If `NULL`, a data frame is returned. The default value is `NULL`. 



 ### `overwrite`
 If `TRUE`, an existing `outData` is overwritten; if `FALSE` an existing `outData` is not overwritten. The default  value is /codeFALSE. 



 ### `dataThreads`
 An integer specifying the desired degree of parallelism in the data pipeline. If `NULL`, the number of threads used is determined internally. The default value is `NULL`. 



 ### `randomSeed`
 Specifies the random seed. The default value is `NULL`. 



 ### `maxSlots`
 Max slots to return for vector valued columns (<=0 to return all). 



 ### `mlTransforms`
 Specifies a list of MicrosoftML transforms to be performed on the data before training or `NULL` if no transforms are  to be performed. See [featurizeText](featurizeText.md), [categorical](categorical.md), and [categoricalHash](categoricalHash.md), for transformations that are supported. These transformations are performed after any specified R transformations. The default value is `NULL`. 



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
 A user-defined environment to serve as a parent to all environments developed internally and used for variable data transformation. If `transformEnvir = NULL`, a new "hash" environment with parent `baseenv()` is used instead The default value is `NULL`. 



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
 An integer value that specifies the amount of output wanted.  If `0`, no verbose output is printed during calculations. Integer   values from `1` to `4` provide increasing amounts of information.  The default value is `1`. 



 ### `computeContext`
 Sets the context in which computations are executed, specified with a valid RxComputeContext. Currently local and RxInSqlServer compute contexts are supported. 



 ### ` ...`
 Additional arguments to be passed directly to the Microsoft Compute Engine. 



 ## Value

A data frame or an RxDataSource object
representing the created output data.

 ## Author(s)

Microsoft Corporation [`Microsoft Technical Support`](https://go.microsoft.com/fwlink/?LinkID=698556&clcid=0x409)



 ## See also

rxDataStep, rxImport,
rxTransform.

 ## Examples

 ```

  # rxFeaturize basically allows you to access data from the MicrosoftML transforms
  # In this example we'll look at getting the output of the categorical transform

  # Create the data
  categoricalData <- data.frame(
    placesVisited = c(
      "London",
      "Brunei",
      "London",
      "Paris",
      "Seria"
    ),
    stringsAsFactors = FALSE
  )

  # Invoke the categorical transform
  categorized <- rxFeaturize(
    data = categoricalData,
    mlTransforms = list(categorical(vars = c(xDataCat = "placesVisited")))
  )

  # Now let's look at the data
  categorized
```



