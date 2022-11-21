--- 
 
# required metadata 
title: "rx_featurize: Data Transformation for revoscalepy data sources" 
description: "Transforms data from an input data set to an output data set." 
keywords: "transform, featurizer" 
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

# *microsoftml.rx_featurize*: Data transformation for data sources





## Usage



```
microsoftml.rx_featurize(data: typing.Union[revoscalepy.datasource.RxDataSource.RxDataSource,
    pandas.core.frame.DataFrame],
    output_data: typing.Union[revoscalepy.datasource.RxDataSource.RxDataSource,
    str] = None, overwrite: bool = False,
    data_threads: int = None, random_seed: int = None,
    max_slots: int = 5000, ml_transforms: list = None,
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

Transforms data from an input data set to an output data set.


## Arguments


### data

A [revoscalepy](/machine-learning-server/python-reference/revoscalepy/revoscalepy-package) data source object, a data frame, or the path
to a `.xdf` file.


### output_data

Output text or xdf file name or an `RxDataSource` with
write capabilities in which to store transformed data. If *None*, a data
frame is returned. The default value is *None*.


### overwrite

If `True`, an existing `output_data` is overwritten;
if `False` an existing `output_data` is not overwritten. The default
value is `False`.


### data_threads

An integer specifying the desired degree of parallelism in
the data pipeline. If *None*, the number of threads used is determined
internally. The default value is *None*.


### random_seed

Specifies the random seed. The default value is *None*.


### max_slots

Max slots to return for vector valued columns (<=0 to return all).


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
The default value is *None*.


### transform_objects

NOT SUPPORTED. A named list that contains objects that can be
referenced by `transforms`, `transform_function`, and
`row_selection`. The default value is *None*.


### transform_function

The variable transformation function.
The default value is *None*.


### transform_variables

A character vector of input data set variables needed for
the transformation function.
The default value is *None*.


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
revoscalepy.baseenv is used instead The default value is *None*.


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

The default value is `1`.


### verbose

An integer value that specifies the amount of output wanted.
If `0`, no verbose output is printed during calculations. Integer
values from `1` to `4` provide increasing amounts of information.
The default value is `1`.


### compute_context

Sets the context in which computations are executed,
specified with a valid revoscalepy.RxComputeContext.
Currently local and [revoscalepy.RxInSqlServer](/machine-learning-server/python-reference/revoscalepy/RxInSqlServer) compute contexts
are supported.


## Returns

A data frame or an [revoscalepy.RxDataSource](/machine-learning-server/python-reference/revoscalepy/RxDataSource) object
representing the created output data.


## See also

[`rx_predict`](rx-predict.md),
[revoscalepy.rx_data_step](/machine-learning-server/python-reference/revoscalepy/rx-data-step),
[revoscalepy.rx_import](/machine-learning-server/python-reference/revoscalepy/rx-import).


## Example



```
'''
Example with rx_featurize.
'''
import numpy
import pandas
from microsoftml import rx_featurize, categorical

# rx_featurize basically allows you to access data from the MicrosoftML transforms
# In this example we'll look at getting the output of the categorical transform
# Create the data
categorical_data = pandas.DataFrame(data=dict(places_visited=[
                "London", "Brunei", "London", "Paris", "Seria"]),
                dtype="category")
                
print(categorical_data)

# Invoke the categorical transform
categorized = rx_featurize(data=categorical_data,
                           ml_transforms=[categorical(cols=dict(xdatacat="places_visited"))])

# Now let's look at the data
print(categorized)
```


Output:



```
  places_visited
0         London
1         Brunei
2         London
3          Paris
4          Seria
Beginning processing data.
Rows Read: 5, Read Time: 0, Transform Time: 0
Beginning processing data.
Beginning processing data.
Rows Read: 5, Read Time: 0, Transform Time: 0
Beginning processing data.
Elapsed time: 00:00:00.0521300
Finished writing 5 rows.
Writing completed.
  places_visited  xdatacat.London  xdatacat.Brunei  xdatacat.Paris  \
0         London              1.0              0.0             0.0   
1         Brunei              0.0              1.0             0.0   
2         London              1.0              0.0             0.0   
3          Paris              0.0              0.0             1.0   
4          Seria              0.0              0.0             0.0   

   xdatacat.Seria  
0             0.0  
1             0.0  
2             0.0  
3             0.0  
4             1.0  
```

