---
title: "concat function (MicrosoftML)"
description: "Combines several columns into a single vector-valued column (MicrosoftML)."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (MicrosoftML), concat, transform
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---


 # concat: Machine Learning Concat Transform 
 

Combines several columns into a single vector-valued column.


 ## Usage

```   
  concat(vars, ...)

```

 ## Arguments



 ### `vars`
 A named list of character vectors of input variable names and the name of the output variable. Note that all the input variables must be of the same type. It is possible to produce multiple output columns  with the concatenation transform. In this case, you need to use a list of  vectors to define a one-to-one mapping between input and output variables. For example, to concatenate columns InNameA and InNameB into column OutName1 and also columns InNameC and InNameD into column OutName2, use the list:  (list(OutName1 = c(InNameA, InNameB), outName2 = c(InNameC, InNameD))) 



 ### ` ...`
 Additional arguments sent to the compute engine 



 ## Details

`concat` creates a single vector-valued column from multiple  
columns. It can be performed on data before training a model. The concatenation  
can significantly speed up the processing of data when the number of columns 
is as large as hundreds to thousands.


 ## Value

A `maml` object defining the concatenation transform.

 ## Author(s)

Microsoft Corporation [`Microsoft Technical Support`](https://go.microsoft.com/fwlink/?LinkID=698556&clcid=0x409)



 ## See also

[featurizeText](featurizeText.md), [categorical](categorical.md),
[categoricalHash](categoricalHash.md), [rxFastTrees](rxFastTrees.md),
[rxFastForest](rxFastForest.md), [rxNeuralNet](rxNeuralNet.md),
[rxOneClassSvm](rxOneClassSvm.md), [rxLogisticRegression](rxLogisticRegression.md).

 ## Examples

 ```

  testObs <- rnorm(nrow(iris)) > 0
  testIris <- iris[testObs,]
  trainIris <- iris[!testObs,]

  multiLogitOut <- rxLogisticRegression(
          formula = Species~Features, type = "multiClass", data = trainIris,
          mlTransforms = list(concat(vars = list(
              Features = c("Sepal.Length", "Sepal.Width", "Petal.Length", "Petal.Width")
            ))))
  summary(multiLogitOut)
```



