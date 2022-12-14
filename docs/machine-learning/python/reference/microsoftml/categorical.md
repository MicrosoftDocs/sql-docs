--- 
 
# required metadata 
title: "categorical: Machine Learning Categorical Data Transform" 
description: "Categorical transform that can be performed on data before training a model." 
keywords: "transform, category" 
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

# *microsoftml.categorical*: Converts a text column into categories





## Usage



```
microsoftml.categorical(cols: [str, dict, list], output_kind: ['Bag', 'Ind',
    'Key', 'Bin'] = 'Ind', max_num_terms: int = 1000000,
    terms: int = None, sort: ['Occurrence', 'Value'] = 'Occurrence',
    text_key_values: bool = False, **kargs)
```





## Description

Categorical transform that can be performed on data before
training a model.


## Details

The `categorical` transform passes through a data set, operating
on text columns, to build a dictionary of categories. For each row,
the entire text string appearing in the input column is defined as a
category. The output of the categorical transform is an indicator vector.
Each slot in this vector corresponds to a category in the dictionary, so
its length is the size of the built dictionary. The categorical transform
can be applied to one or more columns, in which case it builds a separate
dictionary for each column that it is applied to.

`categorical` is not currently supported to handle factor data.


## Arguments


### cols

A character string or list of variable names to transform. If
`dict`, the keys represent the names of new variables to be created.


### output_kind

A character string that specifies the kind of output kind.

* `"Bag"`: Outputs a multi-set vector. If the input column is a vector of categories, the output contains one vector, where the value in each slot is the number of occurrences of the category in the input vector. If the input column contains a single category, the indicator vector and the bag vector are equivalent 

* `"Ind"`: Outputs an indicator vector. The input column is a vector of categories, and the output contains one indicator vector per slot in the input column. 

* `"Key"`: Outputs an index. The output is an integer ID (between 1 and the number of categories in the dictionary) of the category. 

* `"Bin"`: Outputs a vector which is the binary representation of the category. 

The default value is `"Ind"`.


### max_num_terms

An integer that specifies the maximum number of
categories to include in the dictionary. The default value is 1000000.


### terms

Optional character vector of terms or categories.


### sort

A character string that specifies the sorting criteria.

* `"Occurrence"`: Sort categories by occurrences. Most frequent is first. 

* `"Value"`: Sort categories by values. 


### text_key_values

Whether key value metadata should be text, regardless of the actual input type.


### kargs

Additional arguments sent to compute engine.


## Returns

An object defining the transform.


## See also

[`categorical_hash`](categorical-hash.md)


## Example



```
'''
Example on rx_logistic_regression and categorical.
'''
import numpy
import pandas
from microsoftml import rx_logistic_regression, categorical, rx_predict

train_reviews = pandas.DataFrame(data=dict(
    review=[
        "This is great", "I hate it", "Love it", "Do not like it", "Really like it",
        "I hate it", "I like it a lot", "I kind of hate it", "I do like it",
        "I really hate it", "It is very good", "I hate it a bunch", "I love it a bunch",
        "I hate it", "I like it very much", "I hate it very much.",
        "I really do love it", "I really do hate it", "Love it!", "Hate it!",
        "I love it", "I hate it", "I love it", "I hate it", "I love it"],
    like=[True, False, True, False, True, False, True, False, True, False,
        True, False, True, False, True, False, True, False, True, False, True,
        False, True, False, True]))
        
test_reviews = pandas.DataFrame(data=dict(
    review=[
        "This is great", "I hate it", "Love it", "Really like it", "I hate it",
        "I like it a lot", "I love it", "I do like it", "I really hate it", "I love it"]))

# Use a categorical transform: the entire string is treated as a category
out_model = rx_logistic_regression("like ~ reviewCat",
                data=train_reviews,
                ml_transforms=[categorical(cols=dict(reviewCat="review"))])
                
# Note that 'I hate it' and 'I love it' (the only strings appearing more than once)
# have non-zero weights.
print(out_model.coef_)

# Use the model to score.
source_out_df = rx_predict(out_model, data=test_reviews, extra_vars_to_write=["review"])
print(source_out_df.head())
```


Output:



```
Beginning processing data.
Rows Read: 25, Read Time: 0, Transform Time: 0
Beginning processing data.
Not adding a normalizer.
Beginning processing data.
Rows Read: 25, Read Time: 0, Transform Time: 0
Beginning processing data.
Beginning processing data.
Rows Read: 25, Read Time: 0, Transform Time: 0
Beginning processing data.
LBFGS multi-threading will attempt to load dataset into memory. In case of out-of-memory issues, turn off multi-threading by setting trainThreads to 1.
Warning: Too few instances to use 4 threads, decreasing to 1 thread(s)
Beginning optimization
num vars: 20
improvement criterion: Mean Improvement
L1 regularization selected 3 of 20 weights.
Not training a calibrator because it is not needed.
Elapsed time: 00:00:01.6550695
Elapsed time: 00:00:00.2259981
OrderedDict([('(Bias)', 0.21317288279533386), ('I hate it', -0.7937591671943665), ('I love it', 0.19668534398078918)])
Beginning processing data.
Rows Read: 10, Read Time: 0, Transform Time: 0
Beginning processing data.
Elapsed time: 00:00:00.1385248
Finished writing 10 rows.
Writing completed.
           review PredictedLabel     Score  Probability
0   This is great           True  0.213173     0.553092
1       I hate it          False -0.580586     0.358798
2         Love it           True  0.213173     0.553092
3  Really like it           True  0.213173     0.553092
4       I hate it          False -0.580586     0.358798
```

