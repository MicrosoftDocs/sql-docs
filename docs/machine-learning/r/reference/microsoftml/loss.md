---
title: "loss functions function (MicrosoftML)"
description: "The loss functions for classification and regression."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (MicrosoftML), loss functions, expLoss, hingeLoss, logLoss, poissonLoss, smoothHingeLoss, squaredLoss, loss
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---








 # loss functions: Classification and Regression Loss functions 
 

The loss functions for classification and regression.


 ## Usage

```   
  expLoss(beta = 1, ...)

  hingeLoss(margin = 1, ...)

  logLoss(...)

  smoothHingeLoss(smoothingConst = 1, ...)

  poissonLoss(...)

  squaredLoss(...)

```

 ## Arguments



 ### `beta`
 Specifies the numeric value of beta (dilation). The default value  is 1. 



 ### `margin`
 Specifies the numeric margin value. The default value is 1. 



 ### `smoothingConst`
 Specifies the numeric value of the smoothing constant. The default value is 1. 



 ### ` ...`
 hidden argument. 



 ## Details

A loss function measures the discrepancy between the prediction
of a machine learning algorithm and the supervised output and represents the
cost of being wrong. 

The classification loss functions supported are:


`logLoss` 

`expLoss` 

`hingeLoss` 

`smoothHingeLoss`


The regression loss functions supported are:


`poissonLoss` 

`squaredLoss`.




 ## Value

A character string defining the loss function.

 ## Author(s)

Microsoft Corporation [`Microsoft Technical Support`](https://go.microsoft.com/fwlink/?LinkID=698556&clcid=0x409)



 ## See also

[rxFastLinear](rxFastLinear.md), [rxNeuralNet](rxNeuralNet.md)

 ## Examples

 ```

  train <- function(lossFunction) {

      result <- rxFastLinear(isCase ~ age + parity + education + spontaneous + induced,
                    transforms = list(isCase = case == 1), lossFunction = lossFunction,
                    data = infert,
                    type = "binary")
      coef(result)[["age"]]
  }

  age <- list()
  age$LogLoss <- train(logLoss())
  age$LogLossHinge <- train(hingeLoss())
  age$LogLossSmoothHinge <- train(smoothHingeLoss())
  age
```



