--- 
 
# required metadata 
title: "concat: Machine Learning Concat Transform" 
description: "Combines several columns into a single vector-valued column." 
keywords: "transform, schema" 
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

# *microsoftml.concat*: Concatenates multiple columns into a single vector





## Usage



```
microsoftml.concat(cols: [dict, list], **kargs)
```





## Description

Combines several columns into a single vector-valued column.


## Details

`concat` creates a single vector-valued column from multiple
columns. It can be performed on data before training a model. The concatenation
can significantly speed up the processing of data when the number of columns
is as large as hundreds to thousands.


## Arguments


### cols

A character dict or list of variable names to transform. If
`dict`, the keys represent the names of new variables to be created.
Note that all the input variables must
be of the same type. It is possible to produce multiple output columns
with the concatenation transform. In this case, you need to use a list of
vectors to define a one-to-one mapping between input and output variables.
For example, to concatenate columns InNameA and InNameB into column OutName1
and also columns InNameC and InNameD into column OutName2, use the dict:
dict(OutName1 = [InNameA, InNameB], outName2 = [InNameC, InNameD])


### kargs

Additional arguments sent to the compute engine.


## Returns

An object defining the concatenation transform.


## See also

[`drop_columns`](drop-columns.md),
[`select_columns`](select-columns.md).


## Example



```
'''
Example on logistic regression and concat.
'''
import numpy
import pandas
import sklearn
from microsoftml import rx_logistic_regression, concat, rx_predict
from microsoftml.datasets.datasets import get_dataset

iris = get_dataset("iris")

if sklearn.__version__ < "0.18":
    from sklearn.cross_validation import train_test_split
else:
    from sklearn.model_selection import train_test_split

# We use iris dataset.
irisdf = iris.as_df()

# The training features.
features = ["Sepal_Length", "Sepal_Width", "Petal_Length", "Petal_Width"]

# The label.
label = "Label"

# microsoftml needs a single dataframe with features and label.
cols = features + [label]

# We split into train/test. y_train, y_test are not used.
data_train, data_test, y_train, y_test = train_test_split(irisdf[cols], irisdf[label])

# We train a logistic regression.
# A concat transform is added to group features in a single vector column.
multi_logit_out = rx_logistic_regression(
                        formula="Label ~ Features",
                        method="multiClass",
                        data=data_train,
                        ml_transforms=[concat(cols={'Features': features})])
                        
# We show the coefficients.
print(multi_logit_out.coef_)

# We predict.
prediction = rx_predict(multi_logit_out, data=data_test)

print(prediction.head())
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
Beginning processing data.
Rows Read: 112, Read Time: 0.001, Transform Time: 0
Beginning processing data.
LBFGS multi-threading will attempt to load dataset into memory. In case of out-of-memory issues, turn off multi-threading by setting trainThreads to 1.
Beginning optimization
num vars: 15
improvement criterion: Mean Improvement
L1 regularization selected 9 of 15 weights.
Not training a calibrator because it is not needed.
Elapsed time: 00:00:00.2348578
Elapsed time: 00:00:00.0197433
OrderedDict([('0+(Bias)', 1.943994402885437), ('1+(Bias)', 0.6346845030784607), ('2+(Bias)', -2.57867693901062), ('0+Petal_Width', -2.7277402877807617), ('0+Petal_Length', -2.5394322872161865), ('0+Sepal_Width', 0.4810805320739746), ('1+Sepal_Width', -0.5790582299232483), ('2+Petal_Width', 2.547518491744995), ('2+Petal_Length', 1.6753791570663452)])
Beginning processing data.
Rows Read: 38, Read Time: 0, Transform Time: 0
Beginning processing data.
Elapsed time: 00:00:00.0662932
Finished writing 38 rows.
Writing completed.
    Score.0   Score.1   Score.2
0  0.320061  0.504115  0.175825
1  0.761624  0.216213  0.022163
2  0.754765  0.215548  0.029687
3  0.182810  0.517855  0.299335
4  0.018770  0.290014  0.691216
```

