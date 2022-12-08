---
title: "getSentiment function (MicrosoftML)"
description: "Scores natural language text and creates a column that  contains probabilities that the sentiments in the text are positive."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (MicrosoftML), getSentiment, nlp, sentiment, text, transform
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---


 # getSentiment: Machine Learning Sentiment Analyzer Transform 
 

Scores natural language text and creates a column that 
contains probabilities that the sentiments in the text are positive.


 ## Usage

```   
  getSentiment(vars, ...)

```

 ## Arguments



 ### `vars`
 A character vector or list of variable names to transform. If named, the names represent the names of new variables to be created. 



 ### ` ...`
 Additional arguments sent to compute engine. 



 ## Details

The `getSentiment` transform returns the probability 
that the sentiment of a natural text is positive. Currently supports  
only the English language.


 ## Value

A `maml` object defining the transform.

 ## Author(s)

Microsoft Corporation [`Microsoft Technical Support`](https://go.microsoft.com/fwlink/?LinkID=698556&clcid=0x409)



 ## See also

[rxFastTrees](rxFastTrees.md), [rxFastForest](rxFastForest.md),
[rxNeuralNet](rxNeuralNet.md), [rxOneClassSvm](rxOneClassSvm.md),
[rxLogisticRegression](rxLogisticRegression.md), [rxFastLinear](rxFastLinear.md).

 ## Examples

 ```

  # Create the data
  CustomerReviews <- data.frame(Review = c(
    "I really did not like the taste of it",
    "It was surprisingly quite good!",
    "I will never ever ever go to that place again!!"),
    stringsAsFactors = FALSE)

  # Get the sentiment scores
  sentimentScores <- rxFeaturize(data = CustomerReviews, 
                                 mlTransforms = getSentiment(vars = list(SentimentScore = "Review")))

  # Let's translate the score to something more meaningful
  sentimentScores$PredictedRating <- ifelse(sentimentScores$SentimentScore > 0.6, 
                                            "AWESOMENESS", "BLAH")

  # Let's look at the results
  sentimentScores
```






