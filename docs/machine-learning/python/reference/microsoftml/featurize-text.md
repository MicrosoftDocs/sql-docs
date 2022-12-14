--- 
 
# required metadata 
title: "featurize_text: Machine Learning Text Transform" 
description: "Text transforms that can be performed on data before training a model." 
keywords: "transform, featurizer, text" 
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

# *microsoftml.featurize_text*: Converts text columns into numerical features





## Usage



```
microsoftml.featurize_text(cols: [str, dict, list], language: ['AutoDetect',
    'English', 'French', 'German', 'Dutch', 'Italian', 'Spanish',
    'Japanese'] = 'English', stopwords_remover=None, case: ['Lower',
    'Upper', 'None'] = 'Lower', keep_diacritics: bool = False,
    keep_punctuations: bool = True, keep_numbers: bool = True,
    dictionary: dict = None, word_feature_extractor={'Name': 'NGram',
    'Settings': {'Weighting': 'Tf', 'MaxNumTerms': [10000000],
    'NgramLength': 1, 'AllLengths': True, 'SkipLength': 0}},
    char_feature_extractor=None, vector_normalizer: ['None', 'L1', 'L2',
    'LInf'] = 'L2', **kargs)
```





## Description

Text transforms that can be performed on data before training
a model.


## Details

The `featurize_text` transform produces a bag of counts of
sequences of consecutive words, called n-grams, from a given corpus of text.
There are two ways it can do this:

* build a dictionary of n-grams and use the ID in the dictionary as the index in the bag; 

* hash each n-gram and use the hash value as the index in the bag. 

The purpose of hashing is to convert variable-length text documents into
equal-length numeric feature vectors, to support dimensionality reduction
and to make the lookup of feature weights faster.

The text transform is applied to text input columns. It offers language
detection, tokenization, stopwords removing, text normalization and feature
generation. It supports the following languages by default: English, French,
German, Dutch, Italian, Spanish and Japanese.

The n-grams are represented as count vectors, with vector slots
corresponding either to n-grams (created using `n_gram`) or to
their hashes (created using `n_gram_hash`). Embedding ngrams in a vector
space allows their contents to be compared in an efficient manner.
The slot values in the vector can be weighted by the following factors:

* *term frequency* - The number of occurrences of the slot in the text 

* *inverse document frequency* - A ratio (the logarithm of inverse relative slot frequency) that measures the information a slot provides by determining how common or rare it is across the entire text. 

* *term frequency-inverse document frequency* - the product term frequency and the inverse document frequency. 


## Arguments


### cols

A character string or list of variable names to transform. If
`dict`, the keys represent the names of new variables to be created.


### language

Specifies the language used in the data set. The following
values are supported:

* `"AutoDetect"`: for automatic language detection. 

* `"English"` 

* `"French"` 

* `"German"` 

* `"Dutch"` 

* `"Italian"` 

* `"Spanish"` 

* `"Japanese"` 


### stopwords_remover

Specifies the stopwords remover to use. There are
three options supported:

* *None*: No stopwords remover is used. 

* `predefined`: A precompiled language-specific list of stop words is used that includes the most common words from Microsoft Office. 

* `custom`: A user-defined list of stopwords. It accepts the following option: `stopword`. 

The default value is *None*.


### case

Text casing using the rules of the invariant culture. Takes the
following values:

* `"Lower"` 

* `"Upper"` 

* `"None"` 

The default value is `"Lower"`.


### keep_diacritics

`False` to remove diacritical marks; `True` to
retain diacritical marks. The default value is `False`.


### keep_punctuations

`False` to remove punctuation; `True` to
retain punctuation. The default value is `True`.


### keep_numbers

`False` to remove numbers; `True` to retain
numbers. The default value is `True`.


### dictionary

A dictionary of allowlisted terms which accepts
the following options:

* `term`: An optional character vector of terms or categories. 

* `dropUnknowns`: Drop items. 

* `sort`: Specifies how to order items when vectorized. Two orderings are supported:
    * `"occurrence"`: items appear in the order encountered. 
    * `"value"`: items are sorted according to their default comparison. For example, text sorting will be case sensitive (e.g., 'A' then 'Z' then 'a'). 

The default value is *None*.
Note that the stopwords list takes precedence over the dictionary allowlist
as the stopwords are removed before the dictionary terms are allowlisted.


### word_feature_extractor

Specifies the word feature extraction arguments. There
are two different feature extraction mechanisms:

* `n_gram()`: Count-based feature extraction (equivalent to WordBag). It accepts the following options: `max_num_terms` and `weighting`. 

* `n_gram_hash()`: Hashing-based feature extraction (equivalent to WordHashBag). It accepts the following options: `hash_bits`, `seed`, `ordered` and `invert_hash`. 

The default value is `n_gram`.


### char_feature_extractor

Specifies the char feature extraction arguments. There
are two different feature extraction mechanisms:

* `n_gram()`: Count-based feature extraction (equivalent to WordBag). It accepts the following options: `max_num_terms` and `weighting`. 

* `n_gram_hash()`: Hashing-based feature extraction (equivalent to WordHashBag). It accepts the following options: `hash_bits`, `seed`, `ordered` and `invert_hash`. 

The default value is *None*.


### vector_normalizer

Normalize vectors (rows) individually by rescaling
them to unit norm. Takes one of the following values:

* `"None"` 

* `"L2"` 

* `"L1"` 

* `"LInf"` 

The default value is `"L2"`.


### kargs

Additional arguments sent to compute engine.


## Returns

An object defining the transform.


## See also

[`n_gram`](n-gram.md),
[`n_gram_hash`](n-gram-hash.md),
[`n_gram`](custom.md),
[`n_gram_hash`](predefined.md),
[`get_sentiment`](get-sentiment.md).


## Example



```
'''
Example with featurize_text and rx_logistic_regression.
'''
import numpy
import pandas
from microsoftml import rx_logistic_regression, featurize_text, rx_predict
from microsoftml.entrypoints._stopwordsremover_predefined import predefined


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

out_model = rx_logistic_regression("like ~ review_tran",
                    data=train_reviews,
                    ml_transforms=[
                        featurize_text(cols=dict(review_tran="review"),
                            stopwords_remover=predefined(),
                            keep_punctuations=False)])
                            
# Use the model to score.
score_df = rx_predict(out_model, data=test_reviews, extra_vars_to_write=["review"])
print(score_df.head())
```


Output:



```
Beginning processing data.
Rows Read: 25, Read Time: 0, Transform Time: 0
Beginning processing data.
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
num vars: 11
improvement criterion: Mean Improvement
L1 regularization selected 3 of 11 weights.
Not training a calibrator because it is not needed.
Elapsed time: 00:00:00.3725934
Elapsed time: 00:00:00.0131199
Beginning processing data.
Rows Read: 10, Read Time: 0, Transform Time: 0
Beginning processing data.
Elapsed time: 00:00:00.0635453
Finished writing 10 rows.
Writing completed.
           review PredictedLabel     Score  Probability
0   This is great           True  0.443986     0.609208
1       I hate it          False -0.668449     0.338844
2         Love it           True  0.994339     0.729944
3  Really like it           True  0.443986     0.609208
4       I hate it          False -0.668449     0.338844
```



## N-grams extractors

* [*microsoftml.n_gram*: Converts text into features using n-grams](n-gram.md) 

* [*microsoftml.n_gram_hash*: Converts text into features using hashed n-grams](n-gram-hash.md) 


## Stopwords removers

* [*microsoftml.custom*: Removes custom stopwords](custom.md) 

* [*microsoftml.predefined*: Removes predefined stopwords](predefined.md) 
