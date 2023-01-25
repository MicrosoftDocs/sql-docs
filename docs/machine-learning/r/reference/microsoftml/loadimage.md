---
title: "loadImage function (MicrosoftML)"
description: "Loads image data (MicrosoftML)."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (MicrosoftML), loadImage, image, transform
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---


 # loadImage: Machine Learning Load Image Transform 
 

Loads image data.


 ## Usage

```   
  loadImage(vars)

```

 ## Arguments



 ### `vars`
 A named list of character vectors of input variable names and the name of the output variable. Note that the input variables must be of the same type. For one-to-one mappings between input and output variables, a named character vector can be used. 



 ## Details

`loadImage` loads images from paths.


 ## Value

A `maml` object defining the transform.

 ## Author(s)

Microsoft Corporation [`Microsoft Technical Support`](https://go.microsoft.com/fwlink/?LinkID=698556&clcid=0x409)



 ## Examples

 ```

  train <- data.frame(Path = c(system.file("help/figures/RevolutionAnalyticslogo.png", package = "MicrosoftML")), Label = c(TRUE), stringsAsFactors = FALSE)

  # Loads the images from variable Path, resizes the images to 1x1 pixels and trains a neural net.
  model <- rxNeuralNet(
      Label ~ Features,
      data = train,
      mlTransforms = list(
          loadImage(vars = list(Features = "Path")),
          resizeImage(vars = "Features", width = 1, height = 1, resizing = "Aniso"),
          extractPixels(vars = "Features")
          ),
      mlTransformVars = "Path",
      numHiddenNodes = 1,
      numIterations = 1)

  # Featurizes the images from variable Path using the default model, and trains a linear model on the result.
  model <- rxFastLinear(
      Label ~ Features,
      data = train,
      mlTransforms = list(
          loadImage(vars = list(Features = "Path")),
          resizeImage(vars = "Features", width = 224, height = 224), # If dnnModel == "AlexNet", the image has to be resized to 227x227.
          extractPixels(vars = "Features"),
          featurizeImage(var = "Features")
          ),
      mlTransformVars = "Path")
```




