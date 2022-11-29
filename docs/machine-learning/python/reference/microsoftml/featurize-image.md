--- 
 
# required metadata 
title: "featurize_image: Machine Learning Image Featurization Transform" 
description: "Featurizes an image using a pre-trained deep neural network model." 
keywords: "transform, image, featurize, dnn, cnn, resnet, alexnet" 
author: WilliamDAssafMSFT
ms.author: wiassaf 
ms.date: 07/15/2019
ms.topic: "reference" 
ms.service: sql
ms.subservice: "machine-learning-services" 
ms.assetid: "" 
 
# optional metadata 
ROBOTS: "" 
audience: "" 
ms.devlang: "Python" 
ms.reviewer: "" 
ms.suite: "" 
ms.tgt_pltfrm: "" 
ms.custom: "" 
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15"
 
---

# *microsoftml.featurize_image*: Converts an image into features





## Usage



```
microsoftml.featurize_image(cols: [dict, str], dnn_model: ['Resnet18',
    'Resnet50', 'Resnet101', 'Alexnet'] = 'Resnet18', **kargs)
```





## Description

Featurizes an image using a pre-trained deep neural network model.


## Details

`featurize_image` featurizes an image using the specified
pre-trained deep neural network model. The input variables to this transform must
be extracted pixel values.


## Arguments


### cols

Input variable containing extracted pixel values. If
`dict`, the keys represent the names of new variables to be created.


### dnn_model

The pre-trained deep neural network. The possible options are:

* `"Resnet18"` 

* `"Resnet50"` 

* `"Resnet101"` 

* `"Alexnet"` 

The default value is `"Resnet18"`.
See [Deep Residual Learning for Image Recognition](http://www.cv-foundation.org/openaccess/content_cvpr_2016/html/He_Deep_Residual_Learning_CVPR_2016_paper.html)
for details about ResNet.


### kargs

Additional arguments sent to compute engine.


## Returns

An object defining the transform.


## See also

[`load_image`](load-image.md),
[`resize_image`](resize-image.md),
[`extract_pixels`](extract-pixels.md).


## Example



```
'''
Example with images.
'''
import numpy
import pandas
from microsoftml import rx_neural_network, rx_predict, rx_fast_linear
from microsoftml import load_image, resize_image, extract_pixels
from microsoftml.datasets.image import get_RevolutionAnalyticslogo

train = pandas.DataFrame(data=dict(Path=[get_RevolutionAnalyticslogo()], Label=[True]))

# Loads the images from variable Path, resizes the images to 1x1 pixels
# and trains a neural net.
model1 = rx_neural_network("Label ~ Features", data=train, 
            ml_transforms=[            
                    load_image(cols=dict(Features="Path")), 
                    resize_image(cols="Features", width=1, height=1, resizing="Aniso"), 
                    extract_pixels(cols="Features")], 
            ml_transform_vars=["Path"], 
            num_hidden_nodes=1, num_iterations=1)

# Featurizes the images from variable Path using the default model, and trains a linear model on the result.
# If dnnModel == "AlexNet", the image has to be resized to 227x227.
model2 = rx_fast_linear("Label ~ Features ", data=train, 
            ml_transforms=[            
                    load_image(cols=dict(Features="Path")), 
                    resize_image(cols="Features", width=224, height=224), 
                    extract_pixels(cols="Features")], 
            ml_transform_vars=["Path"], max_iterations=1)

# We predict even if it does not make too much sense on this single image.
print("\nrx_neural_network")
prediction1 = rx_predict(model1, data=train)
print(prediction1)

print("\nrx_fast_linear")
prediction2 = rx_predict(model2, data=train)
print(prediction2)
```


Output:



```
Automatically adding a MinMax normalization transform, use 'norm=Warn' or 'norm=No' to turn this behavior off.
Beginning processing data.
Rows Read: 1, Read Time: 0, Transform Time: 0
Beginning processing data.
Beginning processing data.
Rows Read: 1, Read Time: 0, Transform Time: 0
Beginning processing data.
Beginning processing data.
Rows Read: 1, Read Time: 0, Transform Time: 0
Beginning processing data.
Using: AVX Math

***** Net definition *****
  input Data [3];
  hidden H [1] sigmoid { // Depth 1
    from Data all;
  }
  output Result [1] sigmoid { // Depth 0
    from H all;
  }
***** End net definition *****
Input count: 3
Output count: 1
Output Function: Sigmoid
Loss Function: LogLoss
PreTrainer: NoPreTrainer
___________________________________________________________________
Starting training...
Learning rate: 0.001000
Momentum: 0.000000
InitWtsDiameter: 0.100000
___________________________________________________________________
Initializing 1 Hidden Layers, 6 Weights...
Estimated Pre-training MeanError = 0.707823
Iter:1/1, MeanErr=0.707823(0.00%), 0.01M WeightUpdates/sec
Done!
Estimated Post-training MeanError = 0.707499
___________________________________________________________________
Not training a calibrator because it is not needed.
Elapsed time: 00:00:00.0751759
Elapsed time: 00:00:00.0080433
Automatically adding a MinMax normalization transform, use 'norm=Warn' or 'norm=No' to turn this behavior off.
Beginning processing data.
Rows Read: 1, Read Time: 0, Transform Time: 0
Beginning processing data.
Beginning processing data.
Rows Read: 1, Read Time: 0.001, Transform Time: 0
Beginning processing data.
Beginning processing data.
Rows Read: 1, Read Time: 0, Transform Time: 0
Beginning processing data.
Using 2 threads to train.
Automatically choosing a check frequency of 2.
Auto-tuning parameters: L2 = 5.
Auto-tuning parameters: L1Threshold (L1/L2) = 1.
Using model from last iteration.
Not training a calibrator because it is not needed.
Elapsed time: 00:00:01.0104773
Elapsed time: 00:00:00.0106935

rx_neural_network
Beginning processing data.
Rows Read: 1, Read Time: 0, Transform Time: 0
Beginning processing data.
Elapsed time: 00:00:00.0420328
Finished writing 1 rows.
Writing completed.
  PredictedLabel     Score  Probability
0          False -0.028504     0.492875

rx_fast_linear
Beginning processing data.
Rows Read: 1, Read Time: 0, Transform Time: 0
Beginning processing data.
Elapsed time: 00:00:00.4449623
Finished writing 1 rows.
Writing completed.
  PredictedLabel  Score  Probability
0          False    0.0          0.5
```

