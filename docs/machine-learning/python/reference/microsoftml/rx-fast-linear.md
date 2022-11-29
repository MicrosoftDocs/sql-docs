--- 
 
# required metadata 
title: "rx_fast_linear: Fast Linear Model - Stochastic Dual Coordinate Ascent" 
description: "A Stochastic Dual Coordinate Ascent (SDCA) optimization trainer for linear binary classification and regression." 
keywords: "models, linear, SDCA, stochastic, classification, regression" 
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

# *microsoftml.rx_fast_linear*: Linear Model with Stochastic Dual Coordinate Ascent





## Usage



```
microsoftml.rx_fast_linear()
```





## Description

A Stochastic Dual Coordinate Ascent (SDCA) optimization trainer
for linear binary classification and regression.


## Details

`rx_fast_linear` is a trainer based on the Stochastic Dual Coordinate Ascent
(SDCA) method, a state-of-the-art optimization technique for convex objective
functions. The algorithm can be scaled for use on large out-of-memory data
sets due to a semi-asynchronized implementation that supports multi-threading.
Convergence is underwritten by periodically enforcing synchronization between
primal and dual updates in a separate thread. Several choices of loss
functions are also provided. The SDCA method combines several of the
best properties and capabilities of logistic regression and SVM algorithms.
For more information on SDCA, see the citations in the reference section.

Traditional optimization algorithms, such as stochastic gradient descent
(SGD), optimize the empirical loss function directly. The SDCA chooses a
different approach that optimizes the dual problem instead. The dual loss
function is parametrized by per-example weights. In each iteration, when a
training example from the training data set is read, the corresponding
example weight is adjusted so that the dual loss function is optimized with
respect to the current example. No learning rate is needed by SDCA to
determine step size as is required by various gradient descent methods.

`rx_fast_linear` supports binary classification with three types of
loss functions currently: Log loss, hinge loss, and smoothed hinge loss.
Linear regression also supports with squared loss function. Elastic net
regularization can be specified by the `l2_weight` and `l1_weight`
parameters. Note that the `l2_weight` has an effect on the rate of
convergence. In general, the larger the `l2_weight`, the faster
SDCA converges.

Note that `rx_fast_linear` is a stochastic and streaming optimization
algorithm. The results depend on the order of the training data. For
reproducible results, it is recommended that one sets `shuffle` to
`False` and `train_threads` to `1`.


## Arguments


### formula

The formula described in revoscalepy.rx_formula.
Interaction terms and `F()` are not currently supported in
[microsoftml](../../ref-py-microsoftml.md).


### data

A data source object or a character string specifying a
*.xdf* file or a data frame object.


### method

Specifies the model type with a character string: `"binary"`
for the default binary classification or `"regression"` for linear
regression.


### loss_function

Specifies the empirical loss function to optimize.
For binary classification, the following choices are available:

* [`log_loss`](log-loss.md): The log-loss. This is the default. 

* [`hinge_loss`](hinge-loss.md): The SVM hinge loss. Its parameter represents the margin size. 

* `smooth_hinge_loss`: The smoothed hinge loss. Its parameter represents the smoothing constant. 

For linear regression, squared loss [`squared_loss`](squared-loss.md) is
currently supported. When this parameter is set to *None*, its
default value depends on the type of learning:

* [`log_loss`](log-loss.md) for binary classification. 

* [`squared_loss`](squared-loss.md) for linear regression. 

The following example changes the loss_function to
[`hinge_loss`](hinge-loss.md):
`rx_fast_linear(..., loss_function=hinge_loss())`.


### l1_weight

Specifies the L1 regularization weight. The value must be
either non-negative or *None*. If *None* is specified, the
actual value is automatically computed based on data set. *None*
is the default value.


### l2_weight

Specifies the L2 regularization weight. The value must be
either non-negative or *None*. If *None* is specified, the
actual value is automatically computed based on data set. *None*
is the default value.


### train_threads

Specifies how many concurrent threads can be used to run
the algorithm. When this parameter is set to *None*, the number of
threads used is determined based on the number of logical processors
available to the process as well as the sparsity of data. Set it to `1`
to run the algorithm in a single thread.


### convergence_tolerance

Specifies the tolerance threshold used as a
convergence criterion. It must be between 0 and 1. The default value is
`0.1`. The algorithm is considered to have converged if the relative
duality gap, which is the ratio between the duality gap and the primal loss,
falls below the specified convergence tolerance.


### max_iterations

Specifies an upper bound on the number of training
iterations. This parameter must be positive or *None*. If *None*
is specified, the actual value is automatically computed based on data set.
Each iteration requires a complete pass over the training data. Training
terminates after the total number of iterations reaches the specified
upper bound or when the loss function converges, whichever happens earlier.


### shuffle

Specifies whether to shuffle the training data. Set `True`
to shuffle the data; `False` not to shuffle. The default
value is `True`. SDCA is a stochastic optimization algorithm.  If
shuffling is turned on, the training data is shuffled on each iteration.


### check_frequency

The number of iterations after which the loss function
is computed and checked to determine whether it has converged. The value
specified must be a positive integer or *None*. If *None*,
the actual value is automatically computed based on data set. Otherwise,
for example, if `checkFrequency = 5` is specified, then the loss
function is computed and convergence is checked every 5 iterations. The
computation of the loss function requires a separate complete pass over
the training data.


### normalize

Specifies the type of automatic normalization used:

* `"Auto"`: if normalization is needed, it is performed automatically. This is the default choice. 

* `"No"`: no normalization is performed. 

* `"Yes"`: normalization is performed. 

* `"Warn"`: if normalization is needed, a warning message is displayed, but normalization is not performed. 

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

NOT SUPPORTED. An expression of the form that represents
the first round of variable transformations. As with
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
revoscalepy.baseenv is used instead.


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

A [`FastLinear`](learners-object.md) object with the trained model.


## Note

This algorithm is multi-threaded and will not attempt to load the entire dataset into memory.


## See also

[`hinge_loss`](hinge-loss.md),
[`log_loss`](log-loss.md),
[`smoothed_hinge_loss`](smoothed-hinge-loss.md),
[`squared_loss`](squared-loss.md),
[`rx_predict`](rx-predict.md)


## References

[Scaling Up Stochastic Dual Coordinate Ascent](https://research.microsoft.com/en-us/um/people/mbilenko/papers/15-sasdca.pdf)

[Stochastic Dual Coordinate Ascent Methods for Regularized Loss Minimization](https://jmlr.csail.mit.edu/papers/volume14/shalev-shwartz13a/shalev-shwartz13a.pdf)


## Binary classification example



```
'''
Binary Classification.
'''
import numpy
import pandas
from microsoftml import rx_fast_linear, rx_predict
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

forest_model = rx_fast_linear(
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
Using 2 threads to train.
Automatically choosing a check frequency of 2.
Auto-tuning parameters: maxIterations = 8064.
Auto-tuning parameters: L2 = 2.666837E-05.
Auto-tuning parameters: L1Threshold (L1/L2) = 0.
Using best model from iteration 568.
Not training a calibrator because it is not needed.
Elapsed time: 00:00:00.5810985
Elapsed time: 00:00:00.0084876
Beginning processing data.
Rows Read: 62, Read Time: 0, Transform Time: 0
Beginning processing data.
Elapsed time: 00:00:00.0292334
Finished writing 62 rows.
Writing completed.
Rows Read: 5, Total Rows Processed: 5, Total Chunk Time: Less than .001 seconds 
  isCase PredictedLabel     Score  Probability
0   True           True  0.990544     0.729195
1  False          False -2.307120     0.090535
2  False          False -0.608565     0.352387
3   True           True  1.028217     0.736570
4   True          False -3.913066     0.019588
```



## Regression example



```
'''
Regression.
'''
import numpy
import pandas
from microsoftml import rx_fast_linear, rx_predict
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

model = rx_fast_linear(
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
Rows Read: 22, Read Time: 0.001, Transform Time: 0
Beginning processing data.
Beginning processing data.
Rows Read: 22, Read Time: 0.001, Transform Time: 0
Beginning processing data.
Beginning processing data.
Rows Read: 22, Read Time: 0, Transform Time: 0
Beginning processing data.
Using 2 threads to train.
Automatically choosing a check frequency of 2.
Auto-tuning parameters: maxIterations = 68180.
Auto-tuning parameters: L2 = 0.01.
Auto-tuning parameters: L1Threshold (L1/L2) = 0.
Using best model from iteration 54.
Not training a calibrator because it is not needed.
Elapsed time: 00:00:00.1114324
Elapsed time: 00:00:00.0090901
Beginning processing data.
Rows Read: 8, Read Time: 0, Transform Time: 0
Beginning processing data.
Elapsed time: 00:00:00.0330772
Finished writing 8 rows.
Writing completed.
Rows Read: 5, Total Rows Processed: 5, Total Chunk Time: Less than .001 seconds 
   rating      Score
0    71.0  72.630440
1    67.0  56.995350
2    67.0  52.958641
3    72.0  80.894539
4    50.0  38.375427
```



## loss functions

* [*microsoftml.hinge_loss*: Hinge loss function](hinge-loss.md) 

* [*microsoftml.log_loss*: Log loss function](log-loss.md) 

* [*microsoftml.smoothed_hinge_loss*: Smoothed hinge loss function](smoothed-hinge-loss.md) 

* [*microsoftml.squared_loss*: Squared loss function](squared-loss.md) 
