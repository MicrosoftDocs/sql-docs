--- 
 
# required metadata 
title: "get_sentiment: Machine Learning Sentiment Analyzer Transform" 
description: "Scores natural language text and assesses the probability the sentiments are positive." 
keywords: "transform, text, sentiment, nlp" 
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

# *microsoftml.get_sentiment*: Sentiment analysis





## Usage



```
microsoftml.get_sentiment(cols: [str, dict, list], **kargs)
```





## Description

Scores natural language text and assesses
the probability the sentiments are positive.


## Details

The `get_sentiment` transform returns the probability
that the sentiment of a natural text is positive. Currently supports
only the English language.


## Arguments


### cols

A character string or list of variable names to transform. If
`dict`, the names represent the names of new variables to be created.


### kargs

Additional arguments sent to compute engine.


## Returns

An object defining the transform.


## See also

[`featurize_text`](featurize-text.md).


## Example



```
'''
Example with get_sentiment and rx_logistic_regression.
'''
import numpy
import pandas
from microsoftml import rx_logistic_regression, rx_featurize, rx_predict, get_sentiment

# Create the data
customer_reviews = pandas.DataFrame(data=dict(review=[
            "I really did not like the taste of it",
            "It was surprisingly quite good!",
            "I will never ever ever go to that place again!!"]))
            
# Get the sentiment scores
sentiment_scores = rx_featurize(
    data=customer_reviews,
    ml_transforms=[get_sentiment(cols=dict(scores="review"))])
    
# Let's translate the score to something more meaningful
sentiment_scores["eval"] = sentiment_scores.scores.apply(
            lambda score: "AWESOMENESS" if score > 0.6 else "BLAH")
print(sentiment_scores)
```


Output:



```
Beginning processing data.
Rows Read: 3, Read Time: 0, Transform Time: 0
Beginning processing data.
Elapsed time: 00:00:02.4327924
Finished writing 3 rows.
Writing completed.
                                            review    scores         eval
0            I really did not like the taste of it  0.461790         BLAH
1                  It was surprisingly quite good!  0.960192  AWESOMENESS
2  I will never ever ever go to that place again!!  0.310344         BLAH
```

