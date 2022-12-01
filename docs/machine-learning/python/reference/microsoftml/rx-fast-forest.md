--- 
 
# required metadata 
title: "rx_fast_forest: Fast Forest" 
description: "Machine Learning Fast Forest" 
keywords: "models, classification, regression" 
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

# *microsoftml.rx_fast_forest*: Random Forest





## Usage



```
microsoftml.rx_fast_forest(formula: str,
    data: [revoscalepy.datasource.RxDataSource.RxDataSource,
    pandas.core.frame.DataFrame], method: ['binary',
    'regression'] = 'binary', num_trees: int = 100,
    num_leaves: int = 20, min_split: int = 10,
    example_fraction: float = 0.7, feature_fraction: float = 1,
    split_fraction: float = 1, num_bins: int = 255,
    first_use_penalty: float = 0, gain_conf_level: float = 0,
    train_threads: int = 8, random_seed: int = None,
    ml_transforms: list = None, ml_transform_vars: list = None,
    row_selection: str = None, transforms: dict = None,
    transform_objects: dict = None, transform_function: str = None,
    transform_variables: list = None,
    transform_packages: list = None,
    transform_environment: dict = None, blocks_per_read: int = None,
    report_progress: int = None, verbose: int = 1,
    ensemble: microsoftml.modules.ensemble.EnsembleControl = None,
    compute_context: revoscalepy.computecontext.RxComputeContext.RxComputeContext = None)
```





## Description

Machine Learning Fast Forest


## Details

Decision trees are non-parametric models that perform a sequence
of simple tests on inputs. This decision procedure maps them to outputs
found in the training dataset whose inputs were similar to the instance
being processed. A decision is made at each node of the binary tree data
structure based on a measure of similarity that maps each instance
recursively through the branches of the tree until the appropriate leaf
node is reached and the output decision returned.

Decision trees have several advantages:

* They are efficient in both computation and memory usage during training and prediction. 

* They can represent non-linear decision boundaries. 

* They perform integrated feature selection and classification. 

* They are resilient in the presence of noisy features. 

Fast forest regression is a random forest and quantile regression forest
implementation using the regression tree learner in
[`rx_fast_trees`](rx-fast-trees.md).
The model consists of an ensemble of decision trees. Each tree in a decision
forest outputs a Gaussian distribution by way of prediction. An aggregation
is performed over the ensemble of trees to find a Gaussian distribution
closest to the combined distribution for all trees in the model.

This decision forest classifier consists of an ensemble of decision trees.
Generally, ensemble models provide better coverage and accuracy than single
decision trees. Each tree in a decision forest outputs a Gaussian distribution.


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

* `"binary"` for the default Fast Tree Binary Classification or 

* `"regression"` for Fast Tree Regression. 


### num_trees

Specifies the total number of decision trees to create in
the ensemble.By creating more decision trees, you can potentially get
better coverage, but the training time increases. The default value is 100.


### num_leaves

The maximum number of leaves (terminal nodes) that can be created
in any tree. Higher values potentially increase the size of the tree and get
better precision, but risk overfitting and requiring longer training times.
The default value is 20.


### min_split

Minimum number of training instances required to form a
leaf. That is, the minimal number of documents allowed in a leaf of a
regression tree, out of the sub-sampled data. A 'split' means that features
in each level of the tree (node) are randomly divided. The default value is 10.


### example_fraction

The fraction of randomly chosen instances to use
for each tree. The default value is 0.7.


### feature_fraction

The fraction of randomly chosen features to use for
each tree. The default value is 0.7.


### split_fraction

The fraction of randomly chosen features to use on
each split. The default value is 0.7.


### num_bins

Maximum number of distinct values (bins) per feature.
The default value is 255.


### first_use_penalty

The feature first use penalty coefficient. The default
value is 0.


### gain_conf_level

Tree fitting gain confidence requirement (should be in
the range [0,1) ). The default value is 0.


### train_threads

The number of threads to use in training. If *None*
is specified, the number of threads to use is determined internally.
The default value is *None*.


### random_seed

Specifies the random seed. The default value is *None*.


### ml_transforms

Specifies a list of MicrosoftML transforms to be
performed on the data before training or *None* if no transforms are
to be performed. See [`featurize_text`](featurize-text.md),
[`categorical`](categorical.md),
and [`categorical_hash`](categorical-hash.md),
for transformations that are supported.
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

NOT SUPPORTED. An expression of the form  that represents the first round
of variable transformations. As with
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
specified with a valid `RxComputeContext`.
Currently local and `RxInSqlServer` compute contexts
are supported.


### ensemble

Control parameters for ensembling.


## Returns

A [`FastForest`](learners-object.md) object with the trained model.


## Note

This algorithm is multi-threaded and will always attempt to load the entire dataset into
memory.


## See also

[`rx_fast_trees`](rx-fast-trees.md),
[`rx_predict`](rx-predict.md)


## References

[Wikipedia: Random forest](https://en.wikipedia.org/wiki/Random_forest)

[Quantile regression forest](http://jmlr.org/papers/volume7/meinshausen06a/meinshausen06a.pdf)

[From Stumps to Trees to Forests](/archive/blogs/machinelearning/from-stumps-to-trees-to-forests)


## Binary classification example



```
'''
Binary Classification.
'''
import numpy
import pandas
from microsoftml import rx_fast_forest, rx_predict
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

forest_model = rx_fast_forest(
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
Not adding a normalizer.
Making per-feature arrays
Changing data from row-wise to column-wise
Beginning processing data.
Rows Read: 186, Read Time: 0, Transform Time: 0
Beginning processing data.
Processed 186 instances
Binning and forming Feature objects
Reserved memory for tree learner: 7176 bytes
Starting to train ...
Not training a calibrator because a valid calibrator trainer was not provided.
Elapsed time: 00:00:00.2704185
Elapsed time: 00:00:00.0443884
Beginning processing data.
Rows Read: 62, Read Time: 0, Transform Time: 0
Beginning processing data.
Elapsed time: 00:00:00.0253862
Finished writing 62 rows.
Writing completed.
Rows Read: 5, Total Rows Processed: 5, Total Chunk Time: Less than .001 seconds 
  isCase PredictedLabel      Score
0  False          False -36.205067
1   True          False -40.396084
2  False          False -33.242531
3  False          False -87.212494
4   True          False -13.100666
```



## Regression example



```
'''
Regression.
'''
import numpy
import pandas
from microsoftml import rx_fast_forest, rx_predict
from revoscalepy.etl.RxDataStep import rx_data_step
from microsoftml.datasets.datasets import get_dataset

airquality = get_dataset("airquality")

import sklearn
if sklearn.__version__ < "0.18":
    from sklearn.cross_validation import train_test_split
else:
    from sklearn.model_selection import train_test_split

airquality = airquality.as_df()


######################################################################
# Estimate a regression fast forest
# Use the built-in data set 'airquality' to create test and train data

df = airquality[airquality.Ozone.notnull()]
df["Ozone"] = df.Ozone.astype(float)

data_train, data_test, y_train, y_test = train_test_split(df, df.Ozone)

airFormula = " Ozone ~ Solar_R + Wind + Temp "

# Regression Fast Forest for train data
ff_reg = rx_fast_forest(airFormula, method="regression", data=data_train)

# Put score and model variables in data frame
score_df = rx_predict(ff_reg, data=data_test, write_model_vars=True)
print(score_df.head())

# Plot actual versus predicted values with smoothed line
# Supported in the next version.
# rx_line_plot(" Score ~ Ozone ", type=["p", "smooth"], data=score_df)
```


Output:



```
Not adding a normalizer.
Making per-feature arrays
Changing data from row-wise to column-wise
Beginning processing data.
Rows Read: 87, Read Time: 0, Transform Time: 0
Beginning processing data.
Warning: Skipped 4 instances with missing features during training
Processed 83 instances
Binning and forming Feature objects
Reserved memory for tree learner: 21372 bytes
Starting to train ...
Not training a calibrator because it is not needed.
Elapsed time: 00:00:00.0644269
Elapsed time: 00:00:00.0109290
Beginning processing data.
Rows Read: 29, Read Time: 0.001, Transform Time: 0
Beginning processing data.
Elapsed time: 00:00:00.0314390
Finished writing 29 rows.
Writing completed.
   Solar_R  Wind  Temp      Score
0    190.0   7.4  67.0  26.296144
1     20.0  16.6  63.0  14.274153
2    320.0  16.6  73.0  23.421144
3    187.0   5.1  87.0  80.662109
4    175.0   7.4  89.0  67.570549
```