---
title: "getNetDefinition function (MicrosoftML)"
description: "Returns the Net# definition from a trained neural network model."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (MicrosoftML), getNetDefinition, transform
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---


 # getNetDefinition: Get the Net# definition from a trained neural network model 
 

Returns the Net# definition from a trained neural network model.


 ## Usage

```   
  getNetDefinition(model, getWeights = TRUE)

```

 ## Arguments



 ### `model`
 The previously trained neural network model. 



 ### `getWeights`
 If `TRUE`, the weights are included in the returned Net# definition. 



 ## Details

Returns the Net# definition from a trained neural network model. It is
useful for implementing a form of continued training, where the initial weights
of the model are obtained from a previously trained model. Because only the
weights are initialized from the trained model (but not gradients, momentum
etc.), the training is not resumed where it was left at the end of
training of the first model.


 ## Value

A character string containing the Net# definition.

 ## Author(s)

Microsoft Corporation [`Microsoft Technical Support`](https://go.microsoft.com/fwlink/?LinkID=698556&clcid=0x409)



 ## Examples

 ```

  # Train a neural network on the iris dataset for 10 iterations.
  model1 <- rxNeuralNet(
      formula = Species~Sepal.Length + Sepal.Width + Petal.Length + Petal.Width, 
      data = iris, 
      numHiddenNodes=10, 
      type="multi", 
      numIterations=10, 
      optimizer=adaDeltaSgd())

  # Train another neural network on the iris dataset, initializing the topology and weights
  # from the previously trained model.
  model2 <- rxNeuralNet(
      formula = Species~Sepal.Length + Sepal.Width + Petal.Length + Petal.Width, 
      data = iris, 
      netDefinition=getNetDefinition(model1), 
      type="multi", 
      numIterations=10, 
      optimizer = adaDeltaSgd())
```



