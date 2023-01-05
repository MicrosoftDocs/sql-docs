---
title: "rxNeuralNet function (MicrosoftML)"
description: "Neural networks for regression modeling and for Binary and multi-class classification (MicrosoftML)."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (MicrosoftML), rxNeuralNet, classification, dnn, models, network, neural, regression
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---


 # rxNeuralNet: Neural Net 
 

Neural networks for regression modeling and for Binary and
multi-class classification.


 ## Usage

```   
  rxNeuralNet(formula = NULL, data, type = c("binary", "multiClass",
    "regression"), numHiddenNodes = 100, numIterations = 100,
    optimizer = sgd(), netDefinition = NULL, initWtsDiameter = 0.1,
    maxNorm = 0, acceleration = c("sse", "gpu"), miniBatchSize = 1,
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
 A character string denoting Fast Tree type:   
*   `"binary"` for the default binary classification neural network.  
*   `"multiClass"` for multi-class classification neural network.  
*   `"regression"` for a regression neural network.  




 ### `numHiddenNodes`
 The default number of hidden nodes in the neural net. The default value is 100. 



 ### `numIterations`
 The number of iterations on the full training set. The default value is 100. 



 ### `optimizer`
 A list specifying either the `sgd` or `adaptive` optimization algorithm. This list can be created using [sgd](optimizer.md) or [adaDeltaSgd](optimizer.md). The default value is `sgd`. 



 ### `netDefinition`
 The Net# definition of the structure of the neural network. For more information about the Net# language, see   [`Reference Guide`](/azure/machine-learning/classic/azure-ml-netsharp-reference-guide)  



 ### `initWtsDiameter`
 Sets the initial weights diameter that specifies  the range from which values are drawn for the initial learning weights.    The weights are initialized randomly from within this range. The default value is 0.1. 



 ### `maxNorm`
 Specifies an upper bound to constrain the norm of the incoming  weight vector at each hidden unit. This can be very important in maxout  neural networks as well as in cases where training produces unbounded weights. 



 ### `acceleration`
 Specifies the type of hardware acceleration to use.  Possible values are "sse" and "gpu".  For GPU acceleration, it is recommended to use a miniBatchSize greater than one.  If you want to use the GPU acceleration, there are additional manual setup steps are required:    
*  Download and install NVidia CUDA Toolkit 6.5  ([`CUDA Toolkit`](https://developer.nvidia.com/cuda-toolkit-65) ).  
*  Download and install NVidia cuDNN v2 Library  ([`cudnn Library`](https://developer.nvidia.com/rdp/cudnn-archive) ).  
*  Find the libs directory of the MicrosoftRML package by calling `system.file("mxLibs/x64", package = "MicrosoftML")`.  
*  Copy cublas64_65.dll, cudart64_65.dll and cusparse64_65.dll from the CUDA Toolkit 6.5 into the libs directory of the MicrosoftML package. 
*  Copy cudnn64_65.dll from the cuDNN v2 Library into the libs directory of the MicrosoftML package. 




 ### `miniBatchSize`
 Sets the mini-batch size. Recommended values are between  1 and 256. This parameter is only used when the acceleration is GPU. Setting  this parameter to a higher value improves the speed of training, but it might negatively affect the accuracy. The default value is 1. 



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

A neural network is a class of prediction models inspired by the human
brain. A neural network can be represented as a weighted directed graph.
Each node in the graph is called a neuron. The neurons in the graph are
arranged in layers, where neurons in one layer are connected by a weighted
edge (weights can be 0 or positive numbers) to neurons in the next layer.
The first layer is called the input layer, and each neuron in the input
layer corresponds to one of the features. The last layer of the function is
called the output layer. So in the case of binary neural networks it
contains two output neurons, one for each class, whose values are the
probabilities of belonging to each class. The remaining layers are called
hidden layers. The values of the neurons in the hidden layers and in the
output layer are set by calculating the weighted sum of the values of the
neurons in the previous layer and applying an activation function to that
weighted sum. A neural network model is defined by the structure of its
graph (namely, the number of hidden layers and the number of neurons in each
hidden layer), the choice of activation function, and the weights on the
graph edges. The neural network algorithm tries to learn the optimal weights
on the edges based on the training data.

Although neural networks are widely known for use in deep learning and
modeling complex problems such as image recognition, they are also easily 
adapted to regression problems. Any class of statistical models can be considered 
a neural network if they use adaptive weights and can approximate non-linear
functions of their inputs. Neural network regression is especially suited to
problems where a more traditional regression model cannot fit a solution.


 ## Value


 `rxNeuralNet`: an `rxNeuralNet` object with the
  trained model.  
 `NeuralNet`: a learner specification object of
  class `maml` for the Neural Net trainer.  


 ## Notes

This algorithm is single-threaded and will not attempt to load the entire dataset into
memory.


 ## Author(s)

Microsoft Corporation [`Microsoft Technical Support`](https://go.microsoft.com/fwlink/?LinkID=698556&clcid=0x409)



 ## References

[`Wikipedia: Artificial neural network`](http://en.wikipedia.org/wiki/Artificial_neural_network)



 ## See also

[rxFastTrees](rxFastTrees.md), [rxFastForest](rxFastForest.md),
[rxFastLinear](rxFastLinear.md), [rxLogisticRegression](rxLogisticRegression.md),
[rxOneClassSvm](rxOneClassSvm.md), [featurizeText](featurizeText.md),
[categorical](categorical.md), [categoricalHash](categoricalHash.md),
[rxPredict.mlModel](rxPredict.md).

 ## Examples

 ```

  # Estimate a binary neural net
  rxNeuralNet1 <- rxNeuralNet(isCase ~ age + parity + education + spontaneous + induced,
                    transforms = list(isCase = case == 1),
                    data = infert)

  # Score to a data frame
  scoreDF <- rxPredict(rxNeuralNet1, data = infert, 
      extraVarsToWrite = "isCase",
      outData = NULL) # return a data frame

  # Compute and plot the Radio Operator Curve and AUC
  roc1 <- rxRoc(actualVarName = "isCase", predVarNames = "Probability", data = scoreDF) 
  plot(roc1)
  rxAuc(roc1)

  #########################################################################
  # Regression neural net

  # Create an xdf file with the attitude data
  myXdf <- tempfile(pattern = "tempAttitude", fileext = ".xdf")
  rxDataStep(attitude, myXdf, rowsPerRead = 50, overwrite = TRUE)
  myXdfDS <- RxXdfData(file = myXdf)

  attitudeForm <- rating ~ complaints + privileges + learning + 
      raises + critical + advance

  # Estimate a regression neural net 
  res2 <- rxNeuralNet(formula = attitudeForm,  data = myXdfDS, 
      type = "regression")

  # Score to data frame
  scoreOut2 <- rxPredict(res2, data = myXdfDS, 
      extraVarsToWrite = "rating")

  # Plot the rating versus the score with a regression line
  rxLinePlot(rating~Score, type = c("p","r"), data = scoreOut2)

  # Clean up   
  file.remove(myXdf)    

  #############################################################################
  # Multi-class neural net
  multiNN <- rxNeuralNet(
      formula = Species~Sepal.Length + Sepal.Width + Petal.Length + Petal.Width,
      type = "multiClass", data = iris)
  scoreMultiDF <- rxPredict(multiNN, data = iris, 
      extraVarsToWrite = "Species", outData = NULL)    
  # Print the first rows of the data frame with scores
  head(scoreMultiDF)
  # Compute % of incorrect predictions
  badPrediction = scoreMultiDF$Species != scoreMultiDF$PredictedLabel
  sum(badPrediction)*100/nrow(scoreMultiDF)
  # Look at the observations with incorrect predictions
  scoreMultiDF[badPrediction,]
```
