--- 
 
# required metadata 
title: "resize_image: Machine Learning Resize Image Transform" 
description: "Resizes an image to a specified dimension using a specified resizing method." 
keywords: "transform, image" 
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

# *microsoftml.resize_image*: Resizes an Image





## Usage



```
microsoftml.resize_image(cols: [str, dict, list], width: int = 224,
    height: int = 224, resizing_option: ['IsoPad', 'IsoCrop',
    'Aniso'] = 'IsoCrop', **kargs)
```





## Description

Resizes an image to a specified dimension using a specified
resizing method.


## Details

`resize_image` resizes an image to the specified height and width
using a specified resizing method. The input variables to this transform must
be images, typically the result of the `load_image` transform.


## Arguments


### cols

A character string or list of variable names to transform. If
`dict`, the keys represent the names of new variables to be created.


### width

Specifies the width of the scaled image in pixels. The default value is 224.


### height

Specifies the height of the scaled image in pixels. The default value is 224.


### resizing_option

Specified the resizing method to use. Note that all methods
are using bilinear interpolation. The options are:

* `"IsoPad"`: The image is resized such that the aspect ratio is preserved. If needed, the image is padded with black to fit the new width or height. 

* `"IsoCrop"`: The image is resized such that the aspect ratio is preserved. If needed, the image is cropped to fit the new width or height. 

* `"Aniso"`: The image is stretched to the new width and height, without preserving the aspect ratio. 

The default value is `"IsoPad"`.


### kargs

Additional arguments sent to compute engine.


## Returns

An object defining the transform.


## See also

[`load_image`](load-image.md),
[`extract_pixels`](extract-pixels.md),
[`featurize_image`](featurize-image.md).


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
Rows Read: 1, Read Time: 0.001, Transform Time: 0
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
Elapsed time: 00:00:00.0820600
Elapsed time: 00:00:00.0090292
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
Using 2 threads to train.
Automatically choosing a check frequency of 2.
Auto-tuning parameters: L2 = 5.
Auto-tuning parameters: L1Threshold (L1/L2) = 1.
Using model from last iteration.
Not training a calibrator because it is not needed.
Elapsed time: 00:00:01.0852660
Elapsed time: 00:00:00.0132126

rx_neural_network
Beginning processing data.
Rows Read: 1, Read Time: 0, Transform Time: 0
Beginning processing data.
Elapsed time: 00:00:00.0441601
Finished writing 1 rows.
Writing completed.
  PredictedLabel     Score  Probability
0          False -0.028504     0.492875

rx_fast_linear
Beginning processing data.
Rows Read: 1, Read Time: 0.001, Transform Time: 0
Beginning processing data.
Elapsed time: 00:00:00.5196788
Finished writing 1 rows.
Writing completed.
  PredictedLabel  Score  Probability
0          False    0.0          0.5
```

