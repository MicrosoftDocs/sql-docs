--- 
 
# required metadata 
title: "rx_ensemble: Ensembles" 
description: "Train an ensemble of models" 
keywords: "ensemble" 
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

# *microsoftml.rx_ensemble*: Combine models into a single one


 


## Usage



```
microsoftml.rx_ensemble(formula: str,
    data: [<class 'revoscalepy.datasource.RxDataSource.RxDataSource'>,
    <class 'pandas.core.frame.DataFrame'>, <class 'list'>],
    trainers: typing.List[microsoftml.modules.base_learner.BaseLearner],
    method: str = None, model_count: int = None,
    random_seed: int = None, replace: bool = False,
    samp_rate: float = None, combine_method: ['Average', 'Median',
    'Vote'] = 'Median', max_calibration: int = 100000,
    split_data: bool = False, ml_transforms: list = None,
    ml_transform_vars: list = None, row_selection: str = None,
    transforms: dict = None, transform_objects: dict = None,
    transform_function: str = None,
    transform_variables: list = None,
    transform_packages: list = None,
    transform_environment: dict = None, blocks_per_read: int = None,
    report_progress: int = None, verbose: int = 1,
    compute_context: revoscalepy.computecontext.RxComputeContext.RxComputeContext = None)
```





## Description

Train an ensemble of models.


## Details

`rx_ensemble` is a function that trains a number of models
of various kinds to obtain better predictive performance than could be
obtained from a single model.


## Arguments


### formula

A symbolic or mathematical formula in valid Python syntax,
enclosed in double quotes. A symbolic formula might reference objects in the
data source, such as `"creditScore ~ yearsEmploy"`.
Interaction terms (`creditScore * yearsEmploy`) and
expressions (`creditScore == 1`) are not currently supported.


### data

A data source object or a character string specifying a *.xdf*
file or a data frame object. Alternatively, it can be a list of data sources
indicating each model should be trained using one of the data sources in the list.
In this case, the length of the data list must be equal to *model_count*.


### trainers

A list of trainers with their arguments. The trainers are
created by using `FastTrees`, `FastForest`, `FastLinear`,
`LogisticRegression`, `NeuralNetwork`, or `OneClassSvm`.


### method

A character string that specifies the type of ensemble:
`"anomaly"` for Anomaly Detection, `"binary"` for Binary Classification,
`multiClass` for Multiclass Classification, or `"regression"` for Regression.


### random_seed

Specifies the random seed. The default value is `None`.


### model_count

Specifies the number of models to train. If this number is greater
than the length of the trainers list, the trainers list is duplicated to match `model_count`.


### replace

A logical value specifying if the sampling of observations should be done
with or without replacement.  The default value is `False`.


### samp_rate

A scalar of positive value specifying the percentage of observations to sample for
each trainer. The default is `1.0` for sampling with replacement (i.e., `replace=True`) and `0.632`
for sampling without replacement (i.e., `replace=False`). When `split_data` is `True`, the default of
`samp_rate` is `1.0` (no sampling is done before splitting).


### split_data

A logical value specifying whether or not to train the base models on non-overlapping partitions.
The default is `False`. It is available only for `RxSpark` compute context and ignored for others.


### combine_method

Specifies the method used to combine the models:

* `"Median"`: to compute the median of the individual model outputs, 

* `"Average"`: to compute the average of the individual model outputs and 

* `"Vote"`: to compute (pos-neg) / the total number of models, where 'pos' is the number of positive outputs and 'neg' is the number of negative outputs.


### max_calibration

Specifies the maximum number of examples to use for calibration. This
argument is ignored for all tasks other than binary classification.


### ml_transforms

Specifies a list of MicrosoftML transforms to be
performed on the data before training or *None* if no transforms are
to be performed. Transforms that require an additional pass over the data
(such as `featurize_text`, `categorical` are not allowed.
These transformations are performed after any specified R transformations.
The default value is *None*.


### ml_transform_vars

Specifies a character vector of variable names
to be used in *ml_transforms* or *None* if none are to be used.
The default value is *None*.


### row_selection

NOT SUPPORTED. Specifies the rows (observations) from the data set that
are to be used by the model with the name of a logical variable from the
data set (in quotes) or with a logical expression using variables in the
data set. For example:

* `rowSelection = "old"` will only use observations in which the value of the variable `old` is `True`. 

* `rowSelection = (age > 20) & (age < 65) & (log(income) > 10)` only uses observations in which the value of the `age` variable is between 20 and 65 and the value of the `log` of the `income` variable is greater than 10. 

The row selection is performed after processing any data
transformations (see the arguments `transforms` or
`transform_func`). As with all expressions, `row_selection` can be
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
`revoscalepy.baseenv` is used instead.


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
specified with a valid `revoscalepy.RxComputeContext`.
Currently local and [revoscalepy.RxSpark](/machine-learning-server/python-reference/revoscalepy/RxSpark) compute contexts
are supported. When [revoscalepy.RxSpark](/machine-learning-server/python-reference/revoscalepy/RxSpark) is specified,
the training of the models is done in a distributed way, and the ensembling
is done locally. Note that the compute context cannot be non-waiting.


## Returns

A `rx_ensemble` object with the trained ensemble model.
