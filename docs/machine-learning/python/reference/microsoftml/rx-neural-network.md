--- 
 
# required metadata 
title: "rx_neural_network: Neural Net" 
description: "Neural networks for regression modeling and for Binary and multi-class classification." 
keywords: "models, classification, regression, neural network, dnn" 
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

# *microsoftml.rx_neural_network*: Neural Network




## Usage



```
microsoftml.rx_neural_network(formula: str,
    data: [revoscalepy.datasource.RxDataSource.RxDataSource,
    pandas.core.frame.DataFrame], method: ['binary', 'multiClass',
    'regression'] = 'binary', num_hidden_nodes: int = 100,
    num_iterations: int = 100,
    optimizer: [<function adadelta_optimizer at 0x0000007156EAC048>,
    <function sgd_optimizer at 0x0000007156E9FB70>] = {'Name': 'SgdOptimizer',
    'Settings': {}}, net_definition: str = None,
    init_wts_diameter: float = 0.1, max_norm: float = 0,
    acceleration: [<function avx_math at 0x0000007156E9FEA0>,
    <function clr_math at 0x0000007156EAC158>,
    <function gpu_math at 0x0000007156EAC1E0>,
    <function mkl_math at 0x0000007156EAC268>,
    <function sse_math at 0x0000007156EAC2F0>] = {'Name': 'AvxMath',
    'Settings': {}}, mini_batch_size: int = 1, normalize: ['No',
    'Warn', 'Auto', 'Yes'] = 'Auto', ml_transforms: list = None,
    ml_transform_vars: list = None, row_selection: str = None,
    transforms: dict = None, transform_objects: dict = None,
    transform_function: str = None,
    transform_variables: list = None,
    transform_packages: list = None,
    transform_environment: dict = None, blocks_per_read: int = None,
    report_progress: int = None, verbose: int = 1,
    ensemble: microsoftml.modules.ensemble.EnsembleControl = None,
    compute_context: revoscalepy.computecontext.RxComputeContext.RxComputeContext = None)
```





## Description

Neural networks for regression modeling and for Binary and
multi-class classification.


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


## Arguments


### formula

The formula as described in revoscalepy.rx_formula.
Interaction terms and `F()` are not currently supported in
[microsoftml](../../ref-py-microsoftml.md).


### data

A data source object or a character string specifying a
*.xdf* file or a data frame object.


### method

A character string denoting Fast Tree type:

* `"binary"` for the default binary classification neural network. 

* `"multiClass"` for multi-class classification neural network. 

* `"regression"` for a regression neural network. 


### num_hidden_nodes

The default number of hidden nodes in the neural net.
The default value is 100.


### num_iterations

The number of iterations on the full training set.
The default value is 100.


### optimizer

A list specifying either the `sgd` or `adaptive`
optimization algorithm. This list can be created using
[`sgd_optimizer`](sgd-optimizer.md) or
[`adadelta_optimizer`](adadelta-optimizer.md).
The default value is `sgd`.


### net_definition

The Net# definition of the structure of the neural
network. For more information about the Net# language, see
[Reference Guide](/azure/machine-learning/classic/azure-ml-netsharp-reference-guide)


### init_wts_diameter

Sets the initial weights diameter that specifies
the range from which values are drawn for the initial learning weights.
The weights are initialized randomly from within this range. The default
value is 0.1.


### max_norm

Specifies an upper bound to constrain the norm of the incoming
weight vector at each hidden unit. This can be very important in max out
neural networks as well as in cases where training produces unbounded weights.


### acceleration

Specifies the type of hardware acceleration to use.
Possible values are "sse_math" and "gpu_math".
For GPU acceleration, it is recommended to use a miniBatchSize
greater than one. If you want to use the GPU acceleration, there are
additional manual setup steps are required:

* Download and install NVidia CUDA Toolkit 6.5 ([CUDA Toolkit](https://developer.nvidia.com/cuda-toolkit-65)). 

* Download and install NVidia cuDNN v2 Library ([cudnn Library](https://developer.nvidia.com/rdp/cudnn-archive)). 

* Find the libs directory of the microsoftml package by calling `import microsoftml, os`, `os.path.join(microsoftml.__path__[0], "mxLibs")`. 

* Copy *cublas64_65.dll*, *cudart64_65.dll* and *cusparse64_65.dll* from the CUDA Toolkit 6.5 into the libs directory of the microsoftml package. 

* Copy *cudnn64_65.dll* from the cuDNN v2 Library into the libs directory of the microsoftml package. 


### mini_batch_size

Sets the mini-batch size. Recommended values are between
1 and 256. This parameter is only used when the acceleration is GPU. Setting
this parameter to a higher value improves the speed of training, but it might
negatively affect the accuracy. The default value is 1.


### normalize

Specifies the type of automatic normalization used:

* `"Warn"`: if normalization is needed, it is performed automatically. This is the default choice. 

* `"No"`: no normalization is performed. 

* `"Yes"`: normalization is performed. 

* `"Auto"`: if normalization is needed, a warning message is displayed, but normalization is not performed. 

Normalization rescales disparate data ranges to a standard scale. Feature
scaling insures the distances between data points are proportional and
enables various optimization methods such as gradient descent to converge
much faster. If normalization is performed, a `MaxMin` normalizer is
used. It normalizes values in an interval [a, b] where `-1 <= a <= 0`
and `0 <= b <= 1` and `b - a = 1`. This normalizer preserves
sparsity by mapping zero to zero.


### ml_transforms

Specifies a list of MicrosoftML transforms to be
performed on the data before training or *None* if no transforms are
to be performed. See [`featurize_text`](featurize-text.md),
[`categorical`](categorical.md),
and [`categorical_hash`](categorical-hash.md), for transformations that are supported.
These transformations are performed after any specified Python transformations.
The default value is *None*.


### ml_transform_vars

Specifies a character vector of variable names
to be used in `ml_transforms` or *None* if none are to be used.
The default value is *None*.


### row_selection

NOT SUPPORTED. Specifies the rows (observations) from the data set that
are to be used by the model with the name of a logical variable from the
data set (in quotes) or with a logical expression using variables in the
data set. For example:

* `row_selection = "old"` will only use observations in which the value of the variable `old` is `True`. 

* `row_selection = (age > 20) & (age < 65) & (log(income) > 10)` only uses observations in which the value of the `age` variable is between 20 and 65 and the value of the `log` of the `income` variable is greater than 10. 

The row selection is performed after processing any data
transformations (see the arguments `transforms` or
`transform_function`). As with all expressions, `row_selection` can be
defined outside of the function call using the `expression`
function.


### transforms

NOT SUPPORTED. An expression of the form that represents the
first round of variable transformations. As with
all expressions, `transforms` (or `row_selection`) can be defined
outside of the function call using the `expression` function.


### transform_objects

NOT SUPPORTED. A named list that contains objects that can be
referenced by `transforms`, `transform_function`, and
`row_selection`.


### transform_function

The variable transformation function.


### transform_variables

A character vector of input data set variables needed for
the transformation function.


### transform_packages

NOT SUPPORTED. A character vector specifying additional Python packages
(outside of those specified in `RxOptions.get_option("transform_packages")`) to
be made available and preloaded for use in variable transformation functions.
For example, those explicitly defined in [revoscalepy](/machine-learning-server/python-reference/revoscalepy/revoscalepy-package) functions via
their `transforms` and `transform_function` arguments or those defined
implicitly via their `formula` or `row_selection` arguments.  The
`transform_packages` argument may also be *None*, indicating that
no packages outside `RxOptions.get_option("transform_packages")` are preloaded.


### transform_environment

NOT SUPPORTED. A user-defined environment to serve as a parent to all
environments developed internally and used for variable data transformation.
If `transform_environment = None`, a new "hash" environment with parent
revoscalepy.baseenvis used instead.


### blocks_per_read

Specifies the number of blocks to read for each chunk
of data read from the data source.


### report_progress

An integer value that specifies the level of reporting
on the row processing progress:

* `0`: no progress is reported. 

* `1`: the number of processed rows is printed and updated. 

* `2`: rows processed and timings are reported. 

* `3`: rows processed and all timings are reported. 


### verbose

An integer value that specifies the amount of output wanted.
If `0`, no verbose output is printed during calculations. Integer
values from `1` to `4` provide increasing amounts of information.


### compute_context

Sets the context in which computations are executed,
specified with a valid revoscalepy.RxComputeContext.
Currently local and [revoscalepy.RxInSqlServer](/machine-learning-server/python-reference/revoscalepy/RxInSqlServer) compute contexts
are supported.


### ensemble

Control parameters for ensembling.


## Returns

A [`NeuralNetwork`](learners-object.md) object with the trained model.


## Note

This algorithm is single-threaded and will not attempt to load the entire dataset into
memory.


## See also

[`adadelta_optimizer`](adadelta-optimizer.md),
[`sgd_optimizer`](sgd-optimizer.md),
[`avx_math`](avx-math.md),
[`clr_math`](clr-math.md),
[`gpu_math`](gpu-math.md),
[`mkl_math`](mkl-math.md),
[`sse_math`](sse-math.md),
[`rx_predict`](rx-predict.md).


## References

[Wikipedia: Artificial neural network](https://en.wikipedia.org/wiki/Artificial_neural_network)


## Binary classification example



```
'''
Binary Classification.
'''
import numpy
import pandas
from microsoftml import rx_neural_network, rx_predict
from revoscalepy.etl.RxDataStep import rx_data_step
from microsoftml.datasets.datasets import get_dataset

infert = get_dataset("infert")

import sklearn
if sklearn.__version__ < "0.18":
    from sklearn.cross_validation import train_test_split
else:
    from sklearn.model_selection import train_test_split

infertdf = infert.as_df()
infertdf["isCase"] = infertdf.case == 1
data_train, data_test, y_train, y_test = train_test_split(infertdf, infertdf.isCase)

forest_model = rx_neural_network(
    formula=" isCase ~ age + parity + education + spontaneous + induced ",
    data=data_train)
    
# RuntimeError: The type (RxTextData) for file is not supported.
score_ds = rx_predict(forest_model, data=data_test,
                     extra_vars_to_write=["isCase", "Score"])
                     
# Print the first five rows
print(rx_data_step(score_ds, number_rows_read=5))
```


Output:



```
Automatically adding a MinMax normalization transform, use 'norm=Warn' or 'norm=No' to turn this behavior off.
Beginning processing data.
Rows Read: 186, Read Time: 0, Transform Time: 0
Beginning processing data.
Beginning processing data.
Rows Read: 186, Read Time: 0, Transform Time: 0
Beginning processing data.
Beginning processing data.
Rows Read: 186, Read Time: 0, Transform Time: 0
Beginning processing data.
Using: AVX Math

***** Net definition *****
  input Data [5];
  hidden H [100] sigmoid { // Depth 1
    from Data all;
  }
  output Result [1] sigmoid { // Depth 0
    from H all;
  }
***** End net definition *****
Input count: 5
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
Initializing 1 Hidden Layers, 701 Weights...
Estimated Pre-training MeanError = 0.742343
Iter:1/100, MeanErr=0.680245(-8.37%), 119.87M WeightUpdates/sec
Iter:2/100, MeanErr=0.637843(-6.23%), 122.52M WeightUpdates/sec
Iter:3/100, MeanErr=0.635404(-0.38%), 122.24M WeightUpdates/sec
Iter:4/100, MeanErr=0.634980(-0.07%), 73.36M WeightUpdates/sec
Iter:5/100, MeanErr=0.635287(0.05%), 128.26M WeightUpdates/sec
Iter:6/100, MeanErr=0.634572(-0.11%), 131.05M WeightUpdates/sec
Iter:7/100, MeanErr=0.634827(0.04%), 124.27M WeightUpdates/sec
Iter:8/100, MeanErr=0.635359(0.08%), 123.69M WeightUpdates/sec
Iter:9/100, MeanErr=0.635244(-0.02%), 119.35M WeightUpdates/sec
Iter:10/100, MeanErr=0.634712(-0.08%), 127.80M WeightUpdates/sec
Iter:11/100, MeanErr=0.635105(0.06%), 122.69M WeightUpdates/sec
Iter:12/100, MeanErr=0.635226(0.02%), 98.61M WeightUpdates/sec
Iter:13/100, MeanErr=0.634977(-0.04%), 127.88M WeightUpdates/sec
Iter:14/100, MeanErr=0.634347(-0.10%), 123.25M WeightUpdates/sec
Iter:15/100, MeanErr=0.634891(0.09%), 124.27M WeightUpdates/sec
Iter:16/100, MeanErr=0.635116(0.04%), 123.06M WeightUpdates/sec
Iter:17/100, MeanErr=0.633770(-0.21%), 122.05M WeightUpdates/sec
Iter:18/100, MeanErr=0.634992(0.19%), 128.79M WeightUpdates/sec
Iter:19/100, MeanErr=0.634385(-0.10%), 122.95M WeightUpdates/sec
Iter:20/100, MeanErr=0.634752(0.06%), 127.14M WeightUpdates/sec
Iter:21/100, MeanErr=0.635043(0.05%), 123.44M WeightUpdates/sec
Iter:22/100, MeanErr=0.634845(-0.03%), 121.81M WeightUpdates/sec
Iter:23/100, MeanErr=0.634850(0.00%), 125.11M WeightUpdates/sec
Iter:24/100, MeanErr=0.634617(-0.04%), 122.18M WeightUpdates/sec
Iter:25/100, MeanErr=0.634675(0.01%), 125.69M WeightUpdates/sec
Iter:26/100, MeanErr=0.634911(0.04%), 122.44M WeightUpdates/sec
Iter:27/100, MeanErr=0.634311(-0.09%), 121.90M WeightUpdates/sec
Iter:28/100, MeanErr=0.634798(0.08%), 123.54M WeightUpdates/sec
Iter:29/100, MeanErr=0.634674(-0.02%), 127.53M WeightUpdates/sec
Iter:30/100, MeanErr=0.634546(-0.02%), 100.96M WeightUpdates/sec
Iter:31/100, MeanErr=0.634859(0.05%), 124.40M WeightUpdates/sec
Iter:32/100, MeanErr=0.634747(-0.02%), 128.21M WeightUpdates/sec
Iter:33/100, MeanErr=0.634842(0.02%), 125.82M WeightUpdates/sec
Iter:34/100, MeanErr=0.634703(-0.02%), 77.48M WeightUpdates/sec
Iter:35/100, MeanErr=0.634804(0.02%), 122.21M WeightUpdates/sec
Iter:36/100, MeanErr=0.634690(-0.02%), 112.48M WeightUpdates/sec
Iter:37/100, MeanErr=0.634654(-0.01%), 119.18M WeightUpdates/sec
Iter:38/100, MeanErr=0.634885(0.04%), 137.19M WeightUpdates/sec
Iter:39/100, MeanErr=0.634723(-0.03%), 113.80M WeightUpdates/sec
Iter:40/100, MeanErr=0.634714(0.00%), 127.50M WeightUpdates/sec
Iter:41/100, MeanErr=0.634794(0.01%), 129.54M WeightUpdates/sec
Iter:42/100, MeanErr=0.633835(-0.15%), 133.05M WeightUpdates/sec
Iter:43/100, MeanErr=0.634401(0.09%), 128.95M WeightUpdates/sec
Iter:44/100, MeanErr=0.634575(0.03%), 123.42M WeightUpdates/sec
Iter:45/100, MeanErr=0.634673(0.02%), 123.78M WeightUpdates/sec
Iter:46/100, MeanErr=0.634692(0.00%), 119.04M WeightUpdates/sec
Iter:47/100, MeanErr=0.634476(-0.03%), 122.95M WeightUpdates/sec
Iter:48/100, MeanErr=0.634583(0.02%), 97.87M WeightUpdates/sec
Iter:49/100, MeanErr=0.634706(0.02%), 121.41M WeightUpdates/sec
Iter:50/100, MeanErr=0.634564(-0.02%), 120.58M WeightUpdates/sec
Iter:51/100, MeanErr=0.634118(-0.07%), 120.17M WeightUpdates/sec
Iter:52/100, MeanErr=0.634699(0.09%), 127.27M WeightUpdates/sec
Iter:53/100, MeanErr=0.634123(-0.09%), 110.51M WeightUpdates/sec
Iter:54/100, MeanErr=0.634390(0.04%), 123.74M WeightUpdates/sec
Iter:55/100, MeanErr=0.634461(0.01%), 113.66M WeightUpdates/sec
Iter:56/100, MeanErr=0.634415(-0.01%), 118.61M WeightUpdates/sec
Iter:57/100, MeanErr=0.634453(0.01%), 114.99M WeightUpdates/sec
Iter:58/100, MeanErr=0.634478(0.00%), 104.53M WeightUpdates/sec
Iter:59/100, MeanErr=0.634010(-0.07%), 124.62M WeightUpdates/sec
Iter:60/100, MeanErr=0.633901(-0.02%), 118.93M WeightUpdates/sec
Iter:61/100, MeanErr=0.634088(0.03%), 40.46M WeightUpdates/sec
Iter:62/100, MeanErr=0.634046(-0.01%), 94.65M WeightUpdates/sec
Iter:63/100, MeanErr=0.634233(0.03%), 27.18M WeightUpdates/sec
Iter:64/100, MeanErr=0.634596(0.06%), 123.94M WeightUpdates/sec
Iter:65/100, MeanErr=0.634185(-0.06%), 125.01M WeightUpdates/sec
Iter:66/100, MeanErr=0.634469(0.04%), 119.41M WeightUpdates/sec
Iter:67/100, MeanErr=0.634333(-0.02%), 124.11M WeightUpdates/sec
Iter:68/100, MeanErr=0.634203(-0.02%), 112.68M WeightUpdates/sec
Iter:69/100, MeanErr=0.633854(-0.05%), 118.62M WeightUpdates/sec
Iter:70/100, MeanErr=0.634319(0.07%), 123.59M WeightUpdates/sec
Iter:71/100, MeanErr=0.634423(0.02%), 122.51M WeightUpdates/sec
Iter:72/100, MeanErr=0.634388(-0.01%), 126.15M WeightUpdates/sec
Iter:73/100, MeanErr=0.634230(-0.02%), 126.51M WeightUpdates/sec
Iter:74/100, MeanErr=0.634011(-0.03%), 128.32M WeightUpdates/sec
Iter:75/100, MeanErr=0.634294(0.04%), 127.48M WeightUpdates/sec
Iter:76/100, MeanErr=0.634372(0.01%), 123.51M WeightUpdates/sec
Iter:77/100, MeanErr=0.632020(-0.37%), 122.12M WeightUpdates/sec
Iter:78/100, MeanErr=0.633770(0.28%), 119.55M WeightUpdates/sec
Iter:79/100, MeanErr=0.633504(-0.04%), 124.21M WeightUpdates/sec
Iter:80/100, MeanErr=0.634154(0.10%), 125.94M WeightUpdates/sec
Iter:81/100, MeanErr=0.633491(-0.10%), 120.83M WeightUpdates/sec
Iter:82/100, MeanErr=0.634212(0.11%), 128.60M WeightUpdates/sec
Iter:83/100, MeanErr=0.634138(-0.01%), 73.58M WeightUpdates/sec
Iter:84/100, MeanErr=0.634244(0.02%), 124.08M WeightUpdates/sec
Iter:85/100, MeanErr=0.634065(-0.03%), 96.43M WeightUpdates/sec
Iter:86/100, MeanErr=0.634174(0.02%), 124.28M WeightUpdates/sec
Iter:87/100, MeanErr=0.633966(-0.03%), 125.24M WeightUpdates/sec
Iter:88/100, MeanErr=0.633989(0.00%), 130.31M WeightUpdates/sec
Iter:89/100, MeanErr=0.633767(-0.04%), 115.73M WeightUpdates/sec
Iter:90/100, MeanErr=0.633831(0.01%), 122.81M WeightUpdates/sec
Iter:91/100, MeanErr=0.633219(-0.10%), 114.91M WeightUpdates/sec
Iter:92/100, MeanErr=0.633589(0.06%), 93.29M WeightUpdates/sec
Iter:93/100, MeanErr=0.634086(0.08%), 123.31M WeightUpdates/sec
Iter:94/100, MeanErr=0.634075(0.00%), 120.99M WeightUpdates/sec
Iter:95/100, MeanErr=0.634071(0.00%), 122.49M WeightUpdates/sec
Iter:96/100, MeanErr=0.633523(-0.09%), 116.48M WeightUpdates/sec
Iter:97/100, MeanErr=0.634103(0.09%), 128.85M WeightUpdates/sec
Iter:98/100, MeanErr=0.633836(-0.04%), 123.87M WeightUpdates/sec
Iter:99/100, MeanErr=0.633772(-0.01%), 128.17M WeightUpdates/sec
Iter:100/100, MeanErr=0.633684(-0.01%), 123.65M WeightUpdates/sec
Done!
Estimated Post-training MeanError = 0.631268
___________________________________________________________________
Not training a calibrator because it is not needed.
Elapsed time: 00:00:00.2454094
Elapsed time: 00:00:00.0082325
Beginning processing data.
Rows Read: 62, Read Time: 0.001, Transform Time: 0
Beginning processing data.
Elapsed time: 00:00:00.0297006
Finished writing 62 rows.
Writing completed.
Rows Read: 5, Total Rows Processed: 5, Total Chunk Time: 0.001 seconds 
  isCase PredictedLabel     Score  Probability
0   True          False -0.689636     0.334114
1   True          False -0.710219     0.329551
2   True          False -0.712912     0.328956
3  False          False -0.700765     0.331643
4   True          False -0.689783     0.334081
```



## MultiClass classification example



```
'''
MultiClass Classification.
'''
import numpy
import pandas
from microsoftml import rx_neural_network, rx_predict
from revoscalepy.etl.RxDataStep import rx_data_step
from microsoftml.datasets.datasets import get_dataset

iris = get_dataset("iris")

import sklearn
if sklearn.__version__ < "0.18":
    from sklearn.cross_validation import train_test_split
else:
    from sklearn.model_selection import train_test_split

irisdf = iris.as_df()
irisdf["Species"] = irisdf["Species"].astype("category")
data_train, data_test, y_train, y_test = train_test_split(irisdf, irisdf.Species)

model = rx_neural_network(
    formula="  Species ~ Sepal_Length + Sepal_Width + Petal_Length + Petal_Width ",
    method="multiClass",
    data=data_train)
    
# RuntimeError: The type (RxTextData) for file is not supported.
score_ds = rx_predict(model, data=data_test,
                     extra_vars_to_write=["Species", "Score"])
                     
# Print the first five rows
print(rx_data_step(score_ds, number_rows_read=5))
```


Output:



```
Automatically adding a MinMax normalization transform, use 'norm=Warn' or 'norm=No' to turn this behavior off.
Beginning processing data.
Rows Read: 112, Read Time: 0.001, Transform Time: 0
Beginning processing data.
Beginning processing data.
Rows Read: 112, Read Time: 0, Transform Time: 0
Beginning processing data.
Beginning processing data.
Rows Read: 112, Read Time: 0, Transform Time: 0
Beginning processing data.
Using: AVX Math

***** Net definition *****
  input Data [4];
  hidden H [100] sigmoid { // Depth 1
    from Data all;
  }
  output Result [3] softmax { // Depth 0
    from H all;
  }
***** End net definition *****
Input count: 4
Output count: 3
Output Function: SoftMax
Loss Function: LogLoss
PreTrainer: NoPreTrainer
___________________________________________________________________
Starting training...
Learning rate: 0.001000
Momentum: 0.000000
InitWtsDiameter: 0.100000
___________________________________________________________________
Initializing 1 Hidden Layers, 803 Weights...
Estimated Pre-training MeanError = 1.949606
Iter:1/100, MeanErr=1.937924(-0.60%), 98.43M WeightUpdates/sec
Iter:2/100, MeanErr=1.921153(-0.87%), 96.21M WeightUpdates/sec
Iter:3/100, MeanErr=1.920000(-0.06%), 95.55M WeightUpdates/sec
Iter:4/100, MeanErr=1.917267(-0.14%), 81.25M WeightUpdates/sec
Iter:5/100, MeanErr=1.917611(0.02%), 102.44M WeightUpdates/sec
Iter:6/100, MeanErr=1.918476(0.05%), 106.16M WeightUpdates/sec
Iter:7/100, MeanErr=1.916096(-0.12%), 97.85M WeightUpdates/sec
Iter:8/100, MeanErr=1.919486(0.18%), 77.99M WeightUpdates/sec
Iter:9/100, MeanErr=1.916452(-0.16%), 95.67M WeightUpdates/sec
Iter:10/100, MeanErr=1.916024(-0.02%), 102.06M WeightUpdates/sec
Iter:11/100, MeanErr=1.917155(0.06%), 99.21M WeightUpdates/sec
Iter:12/100, MeanErr=1.918543(0.07%), 99.25M WeightUpdates/sec
Iter:13/100, MeanErr=1.919120(0.03%), 85.38M WeightUpdates/sec
Iter:14/100, MeanErr=1.917713(-0.07%), 103.00M WeightUpdates/sec
Iter:15/100, MeanErr=1.917675(0.00%), 98.70M WeightUpdates/sec
Iter:16/100, MeanErr=1.917982(0.02%), 99.10M WeightUpdates/sec
Iter:17/100, MeanErr=1.916254(-0.09%), 103.41M WeightUpdates/sec
Iter:18/100, MeanErr=1.915691(-0.03%), 102.00M WeightUpdates/sec
Iter:19/100, MeanErr=1.914844(-0.04%), 86.64M WeightUpdates/sec
Iter:20/100, MeanErr=1.919268(0.23%), 94.68M WeightUpdates/sec
Iter:21/100, MeanErr=1.918748(-0.03%), 108.11M WeightUpdates/sec
Iter:22/100, MeanErr=1.917997(-0.04%), 96.33M WeightUpdates/sec
Iter:23/100, MeanErr=1.914987(-0.16%), 82.84M WeightUpdates/sec
Iter:24/100, MeanErr=1.916550(0.08%), 99.70M WeightUpdates/sec
Iter:25/100, MeanErr=1.915401(-0.06%), 96.69M WeightUpdates/sec
Iter:26/100, MeanErr=1.916092(0.04%), 101.62M WeightUpdates/sec
Iter:27/100, MeanErr=1.916381(0.02%), 98.81M WeightUpdates/sec
Iter:28/100, MeanErr=1.917414(0.05%), 102.29M WeightUpdates/sec
Iter:29/100, MeanErr=1.917316(-0.01%), 100.17M WeightUpdates/sec
Iter:30/100, MeanErr=1.916507(-0.04%), 82.09M WeightUpdates/sec
Iter:31/100, MeanErr=1.915786(-0.04%), 98.33M WeightUpdates/sec
Iter:32/100, MeanErr=1.917581(0.09%), 101.70M WeightUpdates/sec
Iter:33/100, MeanErr=1.913680(-0.20%), 79.94M WeightUpdates/sec
Iter:34/100, MeanErr=1.917264(0.19%), 102.54M WeightUpdates/sec
Iter:35/100, MeanErr=1.917377(0.01%), 100.67M WeightUpdates/sec
Iter:36/100, MeanErr=1.912060(-0.28%), 70.37M WeightUpdates/sec
Iter:37/100, MeanErr=1.917009(0.26%), 80.80M WeightUpdates/sec
Iter:38/100, MeanErr=1.916216(-0.04%), 94.56M WeightUpdates/sec
Iter:39/100, MeanErr=1.916362(0.01%), 28.22M WeightUpdates/sec
Iter:40/100, MeanErr=1.910658(-0.30%), 100.87M WeightUpdates/sec
Iter:41/100, MeanErr=1.916375(0.30%), 85.99M WeightUpdates/sec
Iter:42/100, MeanErr=1.916257(-0.01%), 102.06M WeightUpdates/sec
Iter:43/100, MeanErr=1.914505(-0.09%), 99.86M WeightUpdates/sec
Iter:44/100, MeanErr=1.914638(0.01%), 103.11M WeightUpdates/sec
Iter:45/100, MeanErr=1.915141(0.03%), 107.62M WeightUpdates/sec
Iter:46/100, MeanErr=1.915119(0.00%), 99.65M WeightUpdates/sec
Iter:47/100, MeanErr=1.915379(0.01%), 107.03M WeightUpdates/sec
Iter:48/100, MeanErr=1.912565(-0.15%), 104.78M WeightUpdates/sec
Iter:49/100, MeanErr=1.915466(0.15%), 110.43M WeightUpdates/sec
Iter:50/100, MeanErr=1.914038(-0.07%), 98.44M WeightUpdates/sec
Iter:51/100, MeanErr=1.915015(0.05%), 96.28M WeightUpdates/sec
Iter:52/100, MeanErr=1.913771(-0.06%), 89.27M WeightUpdates/sec
Iter:53/100, MeanErr=1.911621(-0.11%), 72.67M WeightUpdates/sec
Iter:54/100, MeanErr=1.914969(0.18%), 111.17M WeightUpdates/sec
Iter:55/100, MeanErr=1.913894(-0.06%), 98.68M WeightUpdates/sec
Iter:56/100, MeanErr=1.914871(0.05%), 95.41M WeightUpdates/sec
Iter:57/100, MeanErr=1.912898(-0.10%), 80.72M WeightUpdates/sec
Iter:58/100, MeanErr=1.913334(0.02%), 103.71M WeightUpdates/sec
Iter:59/100, MeanErr=1.913362(0.00%), 99.57M WeightUpdates/sec
Iter:60/100, MeanErr=1.913915(0.03%), 106.21M WeightUpdates/sec
Iter:61/100, MeanErr=1.913310(-0.03%), 112.27M WeightUpdates/sec
Iter:62/100, MeanErr=1.913395(0.00%), 50.86M WeightUpdates/sec
Iter:63/100, MeanErr=1.912814(-0.03%), 58.91M WeightUpdates/sec
Iter:64/100, MeanErr=1.911468(-0.07%), 72.06M WeightUpdates/sec
Iter:65/100, MeanErr=1.912313(0.04%), 86.34M WeightUpdates/sec
Iter:66/100, MeanErr=1.913320(0.05%), 114.39M WeightUpdates/sec
Iter:67/100, MeanErr=1.912914(-0.02%), 105.97M WeightUpdates/sec
Iter:68/100, MeanErr=1.909881(-0.16%), 105.73M WeightUpdates/sec
Iter:69/100, MeanErr=1.911649(0.09%), 105.23M WeightUpdates/sec
Iter:70/100, MeanErr=1.911192(-0.02%), 110.24M WeightUpdates/sec
Iter:71/100, MeanErr=1.912480(0.07%), 106.86M WeightUpdates/sec
Iter:72/100, MeanErr=1.909881(-0.14%), 97.28M WeightUpdates/sec
Iter:73/100, MeanErr=1.911678(0.09%), 109.57M WeightUpdates/sec
Iter:74/100, MeanErr=1.911137(-0.03%), 91.01M WeightUpdates/sec
Iter:75/100, MeanErr=1.910706(-0.02%), 99.41M WeightUpdates/sec
Iter:76/100, MeanErr=1.910869(0.01%), 84.18M WeightUpdates/sec
Iter:77/100, MeanErr=1.911643(0.04%), 105.07M WeightUpdates/sec
Iter:78/100, MeanErr=1.911438(-0.01%), 110.12M WeightUpdates/sec
Iter:79/100, MeanErr=1.909590(-0.10%), 84.16M WeightUpdates/sec
Iter:80/100, MeanErr=1.911181(0.08%), 92.30M WeightUpdates/sec
Iter:81/100, MeanErr=1.910534(-0.03%), 110.60M WeightUpdates/sec
Iter:82/100, MeanErr=1.909340(-0.06%), 54.07M WeightUpdates/sec
Iter:83/100, MeanErr=1.908275(-0.06%), 104.08M WeightUpdates/sec
Iter:84/100, MeanErr=1.910364(0.11%), 107.19M WeightUpdates/sec
Iter:85/100, MeanErr=1.910286(0.00%), 102.55M WeightUpdates/sec
Iter:86/100, MeanErr=1.909155(-0.06%), 79.72M WeightUpdates/sec
Iter:87/100, MeanErr=1.909384(0.01%), 102.37M WeightUpdates/sec
Iter:88/100, MeanErr=1.907751(-0.09%), 105.48M WeightUpdates/sec
Iter:89/100, MeanErr=1.910164(0.13%), 102.53M WeightUpdates/sec
Iter:90/100, MeanErr=1.907935(-0.12%), 105.03M WeightUpdates/sec
Iter:91/100, MeanErr=1.909510(0.08%), 99.97M WeightUpdates/sec
Iter:92/100, MeanErr=1.907405(-0.11%), 100.03M WeightUpdates/sec
Iter:93/100, MeanErr=1.905757(-0.09%), 113.21M WeightUpdates/sec
Iter:94/100, MeanErr=1.909167(0.18%), 107.86M WeightUpdates/sec
Iter:95/100, MeanErr=1.907593(-0.08%), 106.09M WeightUpdates/sec
Iter:96/100, MeanErr=1.908358(0.04%), 111.25M WeightUpdates/sec
Iter:97/100, MeanErr=1.906484(-0.10%), 95.81M WeightUpdates/sec
Iter:98/100, MeanErr=1.908239(0.09%), 105.89M WeightUpdates/sec
Iter:99/100, MeanErr=1.908508(0.01%), 103.05M WeightUpdates/sec
Iter:100/100, MeanErr=1.904747(-0.20%), 106.81M WeightUpdates/sec
Done!
Estimated Post-training MeanError = 1.896338
___________________________________________________________________
Not training a calibrator because it is not needed.
Elapsed time: 00:00:00.1620840
Elapsed time: 00:00:00.0096627
Beginning processing data.
Rows Read: 38, Read Time: 0, Transform Time: 0
Beginning processing data.
Elapsed time: 00:00:00.0312987
Finished writing 38 rows.
Writing completed.
Rows Read: 5, Total Rows Processed: 5, Total Chunk Time: Less than .001 seconds 
      Species   Score.0   Score.1   Score.2
0  versicolor  0.350161  0.339557  0.310282
1      setosa  0.358506  0.336593  0.304901
2   virginica  0.346957  0.340573  0.312470
3   virginica  0.346685  0.340748  0.312567
4   virginica  0.348469  0.340113  0.311417
```



## Regression example



```
'''
Regression.
'''
import numpy
import pandas
from microsoftml import rx_neural_network, rx_predict
from revoscalepy.etl.RxDataStep import rx_data_step
from microsoftml.datasets.datasets import get_dataset

attitude = get_dataset("attitude")

import sklearn
if sklearn.__version__ < "0.18":
    from sklearn.cross_validation import train_test_split
else:
    from sklearn.model_selection import train_test_split

attitudedf = attitude.as_df()
data_train, data_test = train_test_split(attitudedf)

model = rx_neural_network(
    formula="rating ~ complaints + privileges + learning + raises + critical + advance",
    method="regression",
    data=data_train)
    
# RuntimeError: The type (RxTextData) for file is not supported.
score_ds = rx_predict(model, data=data_test,
                     extra_vars_to_write=["rating"])
                     
# Print the first five rows
print(rx_data_step(score_ds, number_rows_read=5))
```


Output:



```
Automatically adding a MinMax normalization transform, use 'norm=Warn' or 'norm=No' to turn this behavior off.
Beginning processing data.
Rows Read: 22, Read Time: 0, Transform Time: 0
Beginning processing data.
Beginning processing data.
Rows Read: 22, Read Time: 0.001, Transform Time: 0
Beginning processing data.
Beginning processing data.
Rows Read: 22, Read Time: 0, Transform Time: 0
Beginning processing data.
Using: AVX Math

***** Net definition *****
  input Data [6];
  hidden H [100] sigmoid { // Depth 1
    from Data all;
  }
  output Result [1] linear { // Depth 0
    from H all;
  }
***** End net definition *****
Input count: 6
Output count: 1
Output Function: Linear
Loss Function: SquaredLoss
PreTrainer: NoPreTrainer
___________________________________________________________________
Starting training...
Learning rate: 0.001000
Momentum: 0.000000
InitWtsDiameter: 0.100000
___________________________________________________________________
Initializing 1 Hidden Layers, 801 Weights...
Estimated Pre-training MeanError = 4458.793673
Iter:1/100, MeanErr=1624.747024(-63.56%), 27.30M WeightUpdates/sec
Iter:2/100, MeanErr=139.267390(-91.43%), 30.50M WeightUpdates/sec
Iter:3/100, MeanErr=116.382316(-16.43%), 29.16M WeightUpdates/sec
Iter:4/100, MeanErr=114.947244(-1.23%), 32.06M WeightUpdates/sec
Iter:5/100, MeanErr=112.886818(-1.79%), 32.96M WeightUpdates/sec
Iter:6/100, MeanErr=112.406547(-0.43%), 30.29M WeightUpdates/sec
Iter:7/100, MeanErr=110.502757(-1.69%), 30.92M WeightUpdates/sec
Iter:8/100, MeanErr=111.499645(0.90%), 31.20M WeightUpdates/sec
Iter:9/100, MeanErr=111.895816(0.36%), 32.46M WeightUpdates/sec
Iter:10/100, MeanErr=110.171443(-1.54%), 34.61M WeightUpdates/sec
Iter:11/100, MeanErr=106.975524(-2.90%), 22.14M WeightUpdates/sec
Iter:12/100, MeanErr=107.708220(0.68%), 7.73M WeightUpdates/sec
Iter:13/100, MeanErr=105.345097(-2.19%), 28.99M WeightUpdates/sec
Iter:14/100, MeanErr=109.937833(4.36%), 31.04M WeightUpdates/sec
Iter:15/100, MeanErr=106.672340(-2.97%), 30.04M WeightUpdates/sec
Iter:16/100, MeanErr=108.474555(1.69%), 32.41M WeightUpdates/sec
Iter:17/100, MeanErr=109.449054(0.90%), 31.60M WeightUpdates/sec
Iter:18/100, MeanErr=105.911830(-3.23%), 34.05M WeightUpdates/sec
Iter:19/100, MeanErr=106.045172(0.13%), 33.80M WeightUpdates/sec
Iter:20/100, MeanErr=108.360427(2.18%), 33.60M WeightUpdates/sec
Iter:21/100, MeanErr=106.506436(-1.71%), 33.77M WeightUpdates/sec
Iter:22/100, MeanErr=99.167335(-6.89%), 32.26M WeightUpdates/sec
Iter:23/100, MeanErr=108.115797(9.02%), 25.86M WeightUpdates/sec
Iter:24/100, MeanErr=106.292283(-1.69%), 31.03M WeightUpdates/sec
Iter:25/100, MeanErr=99.397875(-6.49%), 31.33M WeightUpdates/sec
Iter:26/100, MeanErr=104.805299(5.44%), 31.57M WeightUpdates/sec
Iter:27/100, MeanErr=101.385085(-3.26%), 22.92M WeightUpdates/sec
Iter:28/100, MeanErr=100.064656(-1.30%), 35.01M WeightUpdates/sec
Iter:29/100, MeanErr=100.519013(0.45%), 32.74M WeightUpdates/sec
Iter:30/100, MeanErr=99.273143(-1.24%), 35.12M WeightUpdates/sec
Iter:31/100, MeanErr=100.465649(1.20%), 33.68M WeightUpdates/sec
Iter:32/100, MeanErr=102.402320(1.93%), 33.79M WeightUpdates/sec
Iter:33/100, MeanErr=97.517196(-4.77%), 32.32M WeightUpdates/sec
Iter:34/100, MeanErr=102.597511(5.21%), 32.46M WeightUpdates/sec
Iter:35/100, MeanErr=96.187788(-6.25%), 32.32M WeightUpdates/sec
Iter:36/100, MeanErr=101.533507(5.56%), 21.44M WeightUpdates/sec
Iter:37/100, MeanErr=99.339624(-2.16%), 21.53M WeightUpdates/sec
Iter:38/100, MeanErr=98.049306(-1.30%), 15.27M WeightUpdates/sec
Iter:39/100, MeanErr=97.508282(-0.55%), 23.21M WeightUpdates/sec
Iter:40/100, MeanErr=99.894288(2.45%), 27.94M WeightUpdates/sec
Iter:41/100, MeanErr=95.190566(-4.71%), 32.47M WeightUpdates/sec
Iter:42/100, MeanErr=91.234977(-4.16%), 31.29M WeightUpdates/sec
Iter:43/100, MeanErr=98.824414(8.32%), 32.35M WeightUpdates/sec
Iter:44/100, MeanErr=96.759533(-2.09%), 22.37M WeightUpdates/sec
Iter:45/100, MeanErr=95.275106(-1.53%), 32.09M WeightUpdates/sec
Iter:46/100, MeanErr=95.749031(0.50%), 26.49M WeightUpdates/sec
Iter:47/100, MeanErr=96.267879(0.54%), 31.81M WeightUpdates/sec
Iter:48/100, MeanErr=97.383752(1.16%), 31.01M WeightUpdates/sec
Iter:49/100, MeanErr=96.605199(-0.80%), 32.05M WeightUpdates/sec
Iter:50/100, MeanErr=96.927400(0.33%), 32.42M WeightUpdates/sec
Iter:51/100, MeanErr=96.288491(-0.66%), 28.89M WeightUpdates/sec
Iter:52/100, MeanErr=92.751171(-3.67%), 33.68M WeightUpdates/sec
Iter:53/100, MeanErr=88.655001(-4.42%), 34.53M WeightUpdates/sec
Iter:54/100, MeanErr=90.923513(2.56%), 32.00M WeightUpdates/sec
Iter:55/100, MeanErr=91.627261(0.77%), 25.74M WeightUpdates/sec
Iter:56/100, MeanErr=91.132907(-0.54%), 30.00M WeightUpdates/sec
Iter:57/100, MeanErr=95.294092(4.57%), 33.13M WeightUpdates/sec
Iter:58/100, MeanErr=90.219024(-5.33%), 31.70M WeightUpdates/sec
Iter:59/100, MeanErr=92.727605(2.78%), 30.71M WeightUpdates/sec
Iter:60/100, MeanErr=86.910488(-6.27%), 33.07M WeightUpdates/sec
Iter:61/100, MeanErr=92.350984(6.26%), 32.46M WeightUpdates/sec
Iter:62/100, MeanErr=93.208298(0.93%), 31.08M WeightUpdates/sec
Iter:63/100, MeanErr=90.784723(-2.60%), 21.19M WeightUpdates/sec
Iter:64/100, MeanErr=88.685225(-2.31%), 33.17M WeightUpdates/sec
Iter:65/100, MeanErr=91.668555(3.36%), 30.65M WeightUpdates/sec
Iter:66/100, MeanErr=82.607568(-9.88%), 29.72M WeightUpdates/sec
Iter:67/100, MeanErr=88.787842(7.48%), 32.98M WeightUpdates/sec
Iter:68/100, MeanErr=88.793186(0.01%), 34.67M WeightUpdates/sec
Iter:69/100, MeanErr=88.918795(0.14%), 14.09M WeightUpdates/sec
Iter:70/100, MeanErr=87.121434(-2.02%), 33.02M WeightUpdates/sec
Iter:71/100, MeanErr=86.865602(-0.29%), 34.87M WeightUpdates/sec
Iter:72/100, MeanErr=87.261979(0.46%), 32.34M WeightUpdates/sec
Iter:73/100, MeanErr=87.812460(0.63%), 31.35M WeightUpdates/sec
Iter:74/100, MeanErr=87.818462(0.01%), 32.54M WeightUpdates/sec
Iter:75/100, MeanErr=87.085672(-0.83%), 34.80M WeightUpdates/sec
Iter:76/100, MeanErr=85.773668(-1.51%), 35.39M WeightUpdates/sec
Iter:77/100, MeanErr=85.338703(-0.51%), 34.59M WeightUpdates/sec
Iter:78/100, MeanErr=79.370105(-6.99%), 30.14M WeightUpdates/sec
Iter:79/100, MeanErr=83.026209(4.61%), 32.32M WeightUpdates/sec
Iter:80/100, MeanErr=89.776417(8.13%), 33.14M WeightUpdates/sec
Iter:81/100, MeanErr=85.447100(-4.82%), 32.32M WeightUpdates/sec
Iter:82/100, MeanErr=83.991969(-1.70%), 22.12M WeightUpdates/sec
Iter:83/100, MeanErr=85.065064(1.28%), 30.41M WeightUpdates/sec
Iter:84/100, MeanErr=83.762008(-1.53%), 31.29M WeightUpdates/sec
Iter:85/100, MeanErr=84.217726(0.54%), 34.92M WeightUpdates/sec
Iter:86/100, MeanErr=82.395181(-2.16%), 34.26M WeightUpdates/sec
Iter:87/100, MeanErr=82.979145(0.71%), 22.87M WeightUpdates/sec
Iter:88/100, MeanErr=83.656685(0.82%), 28.51M WeightUpdates/sec
Iter:89/100, MeanErr=81.132468(-3.02%), 32.43M WeightUpdates/sec
Iter:90/100, MeanErr=81.311106(0.22%), 30.91M WeightUpdates/sec
Iter:91/100, MeanErr=81.953897(0.79%), 31.98M WeightUpdates/sec
Iter:92/100, MeanErr=79.018074(-3.58%), 33.13M WeightUpdates/sec
Iter:93/100, MeanErr=78.220412(-1.01%), 31.47M WeightUpdates/sec
Iter:94/100, MeanErr=80.833884(3.34%), 25.16M WeightUpdates/sec
Iter:95/100, MeanErr=81.550135(0.89%), 32.64M WeightUpdates/sec
Iter:96/100, MeanErr=77.785628(-4.62%), 32.54M WeightUpdates/sec
Iter:97/100, MeanErr=76.438158(-1.73%), 34.34M WeightUpdates/sec
Iter:98/100, MeanErr=79.471621(3.97%), 33.12M WeightUpdates/sec
Iter:99/100, MeanErr=76.038475(-4.32%), 33.01M WeightUpdates/sec
Iter:100/100, MeanErr=75.349164(-0.91%), 32.68M WeightUpdates/sec
Done!
Estimated Post-training MeanError = 75.768932
___________________________________________________________________
Not training a calibrator because it is not needed.
Elapsed time: 00:00:00.1178557
Elapsed time: 00:00:00.0088299
Beginning processing data.
Rows Read: 8, Read Time: 0, Transform Time: 0
Beginning processing data.
Elapsed time: 00:00:00.0293893
Finished writing 8 rows.
Writing completed.
Rows Read: 5, Total Rows Processed: 5, Total Chunk Time: 0.001 seconds 
   rating      Score
0    82.0  70.120613
1    64.0  66.344688
2    68.0  68.862373
3    58.0  68.241341
4    63.0  67.196869
```



## optimizers

* [*microsoftml.adadelta_optimizer*: Adaptive learning rate method](adadelta-optimizer.md) 

* [*microsoftml.sgd_optimizer*: Stochastic gradient descent](sgd-optimizer.md) 


## math

* [*microsoftml.avx_math*: Acceleration with AVX instructions](avx-math.md) 

* [*microsoftml.clr_math*: Acceleration with .NET math](clr-math.md) 

* [*microsoftml.gpu_math*: Acceleration with NVidia CUDA](gpu-math.md) 

* [*microsoftml.mkl_math*: Acceleration with Intel MKL](mkl-math.md) 

* [*microsoftml.sse_math*: Acceleration with SSE instructions](sse-math.md) 
