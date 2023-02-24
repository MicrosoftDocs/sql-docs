--- 
 
# required metadata 
title: "rx_oneclass_svm: OneClass SVM" 
description: "Machine Learning One Class Support Vector Machines" 
keywords: "models, anomaly, detection" 
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

# *microsoftml.rx_oneclass_svm*: Anomaly Detection





## Usage



```
microsoftml.rx_oneclass_svm(formula: str,
    data: [revoscalepy.datasource.RxDataSource.RxDataSource,
    pandas.core.frame.DataFrame], cache_size: float = 100,
    kernel: [<function linear_kernel at 0x0000007156EAC8C8>,
    <function polynomial_kernel at 0x0000007156EAC950>,
    <function rbf_kernel at 0x0000007156EAC7B8>,
    <function sigmoid_kernel at 0x0000007156EACA60>] = {'Name': 'RbfKernel',
    'Settings': {}}, epsilon: float = 0.001, nu: float = 0.1,
    shrink: bool = True, normalize: ['No', 'Warn', 'Auto',
    'Yes'] = 'Auto', ml_transforms: list = None,
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

Machine Learning One Class Support Vector Machines


## Details

One-class SVM is an algorithm for anomaly detection. The goal of anomaly
detection is to identify outliers that do not belong to some target class.
This type of SVM is one-class because the training set contains only
examples from the target class. It infers what properties are normal for
the objects in the target class and from these properties predicts
which examples are unlike the normal examples. This is useful for anomaly
detection because the scarcity of training examples is the defining
character of anomalies: typically there are very few examples of network
intrusion, fraud, or other types of anomalous behavior.


## Arguments


### formula

The formula as described in revoscalepy.rx_formula.
Interaction terms and `F()` are not currently supported in
[microsoftml](../../ref-py-microsoftml.md).


### data

A data source object or a character string specifying a
*.xdf* file or a data frame object.


### cache_size

The maximal size in MB of the cache that stores the training
data. Increase this for large training sets. The default value is 100 MB.


### kernel

A character string representing the kernel used for computing
inner products. For more information, see `ma_kernel()`. The
following choices are available:

* `rbf_kernel`: Radial basis function kernel. Its parameter represents``gamma`` in the term `exp(-gamma|x-y|^2`. If not specified, it defaults to `1` divided by the number of features used. For example, `rbf_kernel(gamma = .1)`. This is the default value. 

* `linear_kernel`: Linear kernel. 

* `polynomial_kernel`: Polynomial kernel with parameter names `a`, `bias`, and `deg` in the term `(a*<x,y> + bias)^deg`. The `bias`, defaults to `0`. The degree, `deg`, defaults to `3`. If `a` is not specified, it is set to `1` divided by the number of features. 

* `sigmoid_kernel`: Sigmoid kernel with parameter names `gamma` and `coef0` in the term `tanh(gamma*<x,y> + coef0)`. `gamma`, defaults to `1` divided by the number of features. The parameter `coef0` defaults to `0`.  For example, `sigmoid_kernel(gamma = .1, coef0 = 0)`. 


### epsilon

The threshold for optimizer convergence. If the
improvement between iterations is less than the threshold, the algorithm
stops and returns the current model. The value must be greater than or equal
to `numpy.finfo(double).eps`. The default value is 0.001.


### nu

The trade-off between the fraction of outliers and the number of
support vectors (represented by the Greek letter nu). Must be between 0 and
1, typically between 0.1 and 0.5. The default value is 0.1.


### shrink

Uses the shrinking heuristic if `True`. In this case,
some samples will be "shrunk" during the training procedure, which may speed
up training. The default value is `True`.


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

A [`OneClassSvm`](learners-object.md) object with the trained model.


## Note

This algorithm is single-threaded and will always attempt to load the entire dataset into memory.


## See also

`linear_kernel`,
`polynomial_kernel`,
`rbf_kernel`,
`sigmoid_kernel`,
[`rx_predict`](rx-predict.md).


## References

[Wikipedia: Anomaly detection](https://en.wikipedia.org/wiki/Anomaly_detection)

[Microsoft Azure Machine Learning Studio (classic): One-Class Support Vector Machine](/azure/machine-learning/studio-module-reference/one-class-support-vector-machine)

[Estimating the Support of a High-Dimensional Distribution](https://research.microsoft.com/pubs/69731/tr-99-87.pdf)

[New Support Vector Algorithms](http://www.stat.purdue.edu/~yuzhu/stat598m3/Papers/NewSVM.pdf)

[LIBSVM: A Library for Support Vector Machines](https://www.csie.ntu.edu.tw/~cjlin/papers/libsvm.pdf)


## Example



```
'''
Anomaly Detection.
'''
import numpy
import pandas
from microsoftml import rx_oneclass_svm, rx_predict
from revoscalepy.etl.RxDataStep import rx_data_step
from microsoftml.datasets.datasets import get_dataset

iris = get_dataset("iris")

import sklearn
if sklearn.__version__ < "0.18":
    from sklearn.cross_validation import train_test_split
else:
    from sklearn.model_selection import train_test_split

irisdf = iris.as_df()
data_train, data_test = train_test_split(irisdf)

# Estimate a One-Class SVM model
model = rx_oneclass_svm(
            formula= "~ Sepal_Length + Sepal_Width + Petal_Length + Petal_Width",
            data=data_train)

# Add additional non-iris data to the test data set
data_test["isIris"] = 1.0
not_iris = pandas.DataFrame(data=dict(Sepal_Length=[2.5, 2.6], 
        Sepal_Width=[.75, .9], Petal_Length=[2.5, 2.5], 
        Petal_Width=[.8, .7], Species=["not iris", "not iris"], 
        isIris=[0., 0.]))

merged_test = pandas.concat([data_test, not_iris])

scoresdf = rx_predict(model, data=merged_test, extra_vars_to_write=["isIris"])

# Look at the last few observations
print(scoresdf.tail())
```


Output:



```
Automatically adding a MinMax normalization transform, use 'norm=Warn' or 'norm=No' to turn this behavior off.
Beginning processing data.
Rows Read: 112, Read Time: 0, Transform Time: 0
Beginning processing data.
Beginning processing data.
Rows Read: 112, Read Time: 0, Transform Time: 0
Beginning processing data.
Using these libsvm parameters: svm_type=2, nu=0.1, cache_size=100, eps=0.001, shrinking=1, kernel_type=2, gamma=0.25, degree=0, coef0=0
Reconstructed gradient.
optimization finished, #iter = 15
obj = 52.905421, rho = 9.506052
nSV = 12, nBSV = 9
Not training a calibrator because it is not needed.
Elapsed time: 00:00:00.0555122
Elapsed time: 00:00:00.0212389
Beginning processing data.
Rows Read: 40, Read Time: 0, Transform Time: 0
Beginning processing data.
Elapsed time: 00:00:00.0349974
Finished writing 40 rows.
Writing completed.
    isIris     Score
35     1.0 -0.142141
36     1.0 -0.531449
37     1.0 -0.189874
38     0.0  0.635845
39     0.0  0.555602
```