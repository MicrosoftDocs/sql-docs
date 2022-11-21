---
title: "kernel function (MicrosoftML)"
description: "Kernels supported for use in computing inner products."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (MicrosoftML), kernel, linearKernel, maKernel, polynomialKernel, rbfKernel, sigmoidKernel
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---







 # kernel: Kernel 
 

Kernels supported for use in computing inner products.


 ## Usage

```   
  linearKernel(...)

  polynomialKernel(a = NULL, bias = 0, deg = 3, ...)

  rbfKernel(gamma = NULL, ...)

  sigmoidKernel(gamma = NULL, coef0 = 0, ...)

```

 ## Arguments



 ### `a`
 The numeric value for a in the term (a*<x,y> + b)^d. If not specified, `(1/(number of features)` is used. 



 ### `bias`
 The numeric value for b in the term `(a*<x,y> + b)^d`. 



 ### `deg`
 The integer value for d in the term `(a*<x,y> + b)^d`. 



 ### `gamma`
 The numeric value for gamma in the expression `tanh(gamma*<x,y> + c`). If not specified, `1/(number of features)` is used. 



 ### `coef0`
 The numeric value for c in the expression `tanh(gamma*<x,y> + c`). 



 ### ` ...`
 Additional arguments passed to the Microsoft ML compute engine. 



 ## Details

These helper functions specify the kernel that is used for training in
relevant algorithms. The kernels that are supported: 


`linearKernel`: linear kernel.

`rbfKernel`: radial basis function kernel. 

`polynomialKernel`: polynomial kernel. 

`sigmoidKernel`: sigmoid kernel. 




 ## Value

A character string defining the kernel.

 ## Author(s)

Microsoft Corporation [`Microsoft Technical Support`](https://go.microsoft.com/fwlink/?LinkID=698556&clcid=0x409)



 ## References

[`  Estimating the Support of a High-Dimensional Distribution`](https://research.microsoft.com/pubs/69731/tr-99-87.pdf)


[`  New Support Vector Algorithms`](http://www.stat.purdue.edu/~yuzhu/stat598m3/Papers/NewSVM.pdf)



 ## See also

[rxOneClassSvm](rxOneClassSvm.md)

 ## Examples

 ```

  # Simulate some simple data
  set.seed(7)
  numRows <- 200
  normalData <- data.frame(day = 1:numRows)
  normalData$pageViews = runif(numRows, min = 10, max = 1000) + .5 * normalData$day
  testData <- data.frame(day = 1:numRows)
  # The test data has outliers above 1000
  testData$pageViews = runif(numRows, min = 10, max = 1400) + .5 * testData$day

  train <- function(kernelFunction, args=NULL) {
      model <- rxOneClassSvm(formula = ~pageViews + day, data = normalData,
      kernel = kernelFunction(args))
      scores <- rxPredict(model, data = testData, writeModelVars = TRUE)
      scores$groups = scores$Score > 0
      scores
  }
  display <- function(scores) {
      print(sum(scores$groups))
      rxLinePlot(pageViews ~ day, data = scores, groups = groups, type = "p",
       symbolColors = c("red", "blue"))
  }
  scores <- list()
  scores$rbfKernel <- train(rbfKernel)
  scores$linearKernel <- train(linearKernel)
  scores$polynomialKernel <- train(polynomialKernel, (a = .2))
  scores$sigmoidKernel <- train(sigmoidKernel)
  display(scores$rbfKernel)
  display(scores$linearKernel)
  display(scores$polynomialKernel)
  display(scores$sigmoidKernel)
```



