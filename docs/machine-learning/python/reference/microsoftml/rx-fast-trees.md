--- 
 
# required metadata 
title: "rx_fast_trees: Fast Tree" 
description: "Machine Learning Fast Tree" 
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

# *microsoftml.rx_fast_trees*: Boosted Trees





## Usage



```
microsoftml.rx_fast_trees(formula: str,
    data: [revoscalepy.datasource.RxDataSource.RxDataSource,
    pandas.core.frame.DataFrame], method: ['binary',
    'regression'] = 'binary', num_trees: int = 100,
    num_leaves: int = 20, learning_rate: float = 0.2,
    min_split: int = 10, example_fraction: float = 0.7,
    feature_fraction: float = 1, split_fraction: float = 1,
    num_bins: int = 255, first_use_penalty: float = 0,
    gain_conf_level: float = 0, unbalanced_sets: bool = False,
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

Machine Learning Fast Tree


## Details

rx_fast_trees is an implementation of FastRank. FastRank is an efficient
implementation of the MART gradient boosting algorithm. Gradient boosting is
a machine learning technique for regression problems. It builds each
regression tree in a step-wise fashion, using a predefined loss function to
measure the error for each step and corrects for it in the next. So this
prediction model is actually an ensemble of weaker prediction models. In
regression problems, boosting builds a series of such trees in a step-wise
fashion and then selects the optimal tree using an arbitrary differentiable
loss function.

MART learns an ensemble of regression trees, which is a decision tree with
scalar values in its leaves. A decision (or regression) tree is a
binary tree-like flow chart, where at each interior node one decides which
of the two child nodes to continue to based on one of the feature values
from the input. At each leaf node, a value is returned. In the
interior nodes, the decision is based on the test `"x <= v"`, where
`x` is the value of the feature in the input sample and `v` is one
of the possible values of this feature. The functions that can be produced
by a regression tree are all the piece-wise constant functions.

The ensemble of trees is produced by computing, in each step, a regression
tree that approximates the gradient of the loss function, and adding it to
the previous tree with coefficients that minimize the loss of the new tree.
The output of the ensemble produced by MART on a given instance is the sum
of the tree outputs.

* In case of a binary classification problem, the output is converted to a probability by using some form of calibration. 

* In case of a regression problem, the output is the predicted value of the function. 

* In case of a ranking problem, the instances are ordered by the output value of the ensemble. 

If `method` is set to `"regression"`, a regression version of
FastTree is used. If set to `"ranking"`, a ranking version of FastTree
is used. In the ranking case, the instances should be ordered by the output
of the tree ensemble. The only difference in the settings of these
versions is in the calibration settings, which are needed only for
classification.


## Arguments


### formula

The formula as described in revoscalepy.rx_formula.
Interaction terms and `F()` are not currently supported in
[microsoftml](../../ref-py-microsoftml.md).


### data

A data source object or a character string specifying a
*.xdf* file or a data frame object.


### method

A character string that specifies the type of Fast Tree:
`"binary"` for the default Fast Tree Binary Classification or
`"regression"` for Fast Tree Regression.


### num_trees

Specifies the total number of decision trees to create in
the ensemble.By creating more decision trees, you can potentially get
better coverage, but the training time increases. The default value is 100.


### num_leaves

The maximum number of leaves (terminal nodes) that can be created
in any tree. Higher values potentially increase the size of the tree and get
better precision, but risk overfitting and requiring longer training times.
The default value is 20.


### learning_rate

Determines the size of the step taken in the direction
of the gradient in each step of the learning process.  This determines how
fast or slow the learner converges on the optimal solution.  If the step
size is too big, you might overshoot the optimal solution.  If the step size
is too small, training takes longer to converge to the best solution.


### min_split

Minimum number of training instances required to form a
leaf. That is, the minimal number of documents allowed in a leaf of a
regression tree, out of the sub-sampled data. A 'split' means that features
in each level of the tree (node) are randomly divided. The default value is
10. Only the number of instances is counted even if instances are weighted.


### example_fraction

The fraction of randomly chosen instances to use
for each tree. The default value is 0.7.


### feature_fraction

The fraction of randomly chosen features to use for
each tree. The default value is 1.


### split_fraction

The fraction of randomly chosen features to use on
each split. The default value is 1.


### num_bins

Maximum number of distinct values (bins) per feature. If the
feature has fewer values than the number indicated, each value is placed
in its own bin.  If there are more values, the algorithm creates
`numBins` bins.


### first_use_penalty

The feature first use penalty coefficient. This is a
form of regularization that incurs a penalty for using a new feature when
creating the tree. Increase this value to create trees that don't use many
features. The default value is 0.


### gain_conf_level

Tree fitting gain confidence requirement (should be in
the range [0,1)). The default value is 0.


### unbalanced_sets

If `True`, derivatives optimized for unbalanced
sets are used. Only applicable when `type` equal to `"binary"`.
The default value is `False`.


### train_threads

The number of threads to use in training. The default
value is 8.


### random_seed

Specifies the random seed. The default value is *None*.


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

NOT SUPPORTED. An expression of the form  that represents the
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

A [`FastTrees`](learners-object.md) object with the trained model.


## Note

This algorithm is multi-threaded and will always attempt to load the entire dataset into
memory.


## See also

[`rx_fast_forest`](rx-fast-forest.md),
[`rx_predict`](rx-predict.md)


## References

[Wikipedia: Gradient boosting (Gradient tree boosting)](https://en.wikipedia.org/wiki/Gradient_boosting)

[Greedy function approximation: A gradient boosting machine.](http://projecteuclid.org/DPubS?service=UI&version=1.0&verb=Display&handle=euclid.aos/1013203451)


## Binary Classification example



```
'''
Binary Classification.
'''
import numpy
import pandas
from microsoftml import rx_fast_trees, rx_predict
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

trees_model = rx_fast_trees(
    formula=" isCase ~ age + parity + education + spontaneous + induced ",
    data=data_train)
    
# RuntimeError: The type (RxTextData) for file is not supported.
score_ds = rx_predict(trees_model, data=data_test,
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
Reserved memory for tree learner: 7020 bytes
Starting to train ...
Not training a calibrator because it is not needed.
Elapsed time: 00:00:00.0949161
Elapsed time: 00:00:00.0112103
Beginning processing data.
Rows Read: 62, Read Time: 0.001, Transform Time: 0
Beginning processing data.
Elapsed time: 00:00:00.0230457
Finished writing 62 rows.
Writing completed.
Rows Read: 5, Total Rows Processed: 5, Total Chunk Time: 0.001 seconds 
  isCase PredictedLabel      Score  Probability
0  False          False  -4.722279     0.131369
1  False          False -11.550012     0.009757
2  False          False  -7.312314     0.050935
3   True           True   3.889991     0.825778
4  False          False  -6.361800     0.072782
```



## Regression example



```
'''
Regression.
'''
import numpy
import pandas
from microsoftml import rx_fast_trees, rx_predict
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
ff_reg = rx_fast_trees(airFormula, method="regression", data=data_train)

# Put score and model variables in data frame
score_df = rx_predict(ff_reg, data=data_test, write_model_vars=True)
print(score_df.head())

# Plot actual versus predicted values with smoothed line
# Supported in the next version.
# rx_line_plot(" Score ~ Ozone ", type=["p", "smooth"], data=score_df)
```


Output:



```
'unbalanced_sets' ignored for method 'regression'
Not adding a normalizer.
Making per-feature arrays
Changing data from row-wise to column-wise
Beginning processing data.
Rows Read: 87, Read Time: 0.001, Transform Time: 0
Beginning processing data.
Warning: Skipped 4 instances with missing features during training
Processed 83 instances
Binning and forming Feature objects
Reserved memory for tree learner: 21528 bytes
Starting to train ...
Not training a calibrator because it is not needed.
Elapsed time: 00:00:00.0512720
Elapsed time: 00:00:00.0094435
Beginning processing data.
Rows Read: 29, Read Time: 0, Transform Time: 0
Beginning processing data.
Elapsed time: 00:00:00.0229873
Finished writing 29 rows.
Writing completed.
   Solar_R  Wind  Temp      Score
0    115.0   7.4  76.0  26.003876
1    307.0  12.0  66.0  18.057747
2    230.0  10.9  75.0  10.896211
3    259.0   9.7  73.0  13.726607
4     92.0  15.5  84.0  37.972855
```

