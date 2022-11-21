---
title: "mutualInformation function (MicrosoftML)"
description: "Mutual information mode of feature selection used in the feature selection transform [selectFeatures](selectFeatures.md)."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (MicrosoftML), mutualInformation, feature, information, mutual, selection
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---


 # mutualInformation: Feature Selection Mutual Information Mode 
 

Mutual information mode of feature selection used in the feature selection
transform [selectFeatures](selectFeatures.md).


 ## Usage

```   
  mutualInformation(numFeaturesToKeep = 1000, numBins = 256, ...)

```

 ## Arguments



 ### `numFeaturesToKeep`
 If the number of features to keep is specified to be `n`, the transform picks the `n` features that have the highest mutual information with the dependent variable. The default value is 1000. 



 ### `numBins`
 Maximum number of bins for numerical values. Powers of 2 are recommended. The default value is 256. 



 ### ` ...`
 Additional arguments to be passed directly to the Microsoft Compute Engine. 



 ## Details

The mutual information of two random variables `X` and `Y` is a
measure of the mutual dependence between the variables. Formally, the
mutual information can be written as:

`I(X;Y) = E[log(p(x,y)) - log(p(x)) - log(p(y))]`

where the expectation is taken over the joint distribution of `X` and
`Y`. Here `p(x,y)` is the joint probability density function of
`X` and `Y`, `p(x)` and `p(y)` are the marginal
probability density functions of `X` and `Y` respectively. In
general, a higher mutual information between the dependent variable (or
label) and an independent variable (or feature) means that the label has
higher mutual dependence over that feature.

The mutual information feature selection mode selects the features based on
the mutual information. It keeps the top `numFeaturesToKeep` features
with the largest mutual information with the label.


 ## Value

a character string defining the mode.

 ## Author(s)

Microsoft Corporation [`Microsoft Technical Support`](https://go.microsoft.com/fwlink/?LinkID=698556&clcid=0x409)



 ## References

[`Wikipedia: Mutual Information`](https://en.wikipedia.org/wiki/Mutual_information)



 ## See also

[minCount](minCount.md) [selectFeatures](selectFeatures.md)

 ## Examples

 ```

  trainReviews <- data.frame(review = c( 
          "This is great",
          "I hate it",
          "Love it",
          "Do not like it",
          "Really like it",
          "I hate it",
          "I like it a lot",
          "I kind of hate it",
          "I do like it",
          "I really hate it",
          "It is very good",
          "I hate it a bunch",
          "I love it a bunch",
          "I hate it",
          "I like it very much",
          "I hate it very much.",
          "I really do love it",
          "I really do hate it",
          "Love it!",
          "Hate it!",
          "I love it",
          "I hate it",
          "I love it",
          "I hate it",
          "I love it"),
       like = c(TRUE, FALSE, TRUE, FALSE, TRUE,
          FALSE, TRUE, FALSE, TRUE, FALSE, TRUE, FALSE, TRUE,
          FALSE, TRUE, FALSE, TRUE, FALSE, TRUE, FALSE, TRUE, 
          FALSE, TRUE, FALSE, TRUE), stringsAsFactors = FALSE
      )

      testReviews <- data.frame(review = c(
          "This is great",
          "I hate it",
          "Love it",
          "Really like it",
          "I hate it",
          "I like it a lot",
          "I love it",
          "I do like it",
          "I really hate it",
          "I love it"), stringsAsFactors = FALSE)

  # Use a categorical hash transform which generated 128 features.
  outModel1 <- rxLogisticRegression(like~reviewCatHash, data = trainReviews, l1Weight = 0, 
      mlTransforms = list(categoricalHash(vars = c(reviewCatHash = "review"), hashBits = 7)))
  summary(outModel1)

  # Apply a categorical hash transform and a count feature selection transform
  # which selects only those hash features that has value.
  outModel2 <- rxLogisticRegression(like~reviewCatHash, data = trainReviews, l1Weight = 0, 
      mlTransforms = list(
    categoricalHash(vars = c(reviewCatHash = "review"), hashBits = 7), 
    selectFeatures("reviewCatHash", mode = minCount())))
  summary(outModel2)

  # Apply a categorical hash transform and a mutual information feature selection transform
  # which selects those features appearing with at least a count of 5.
  outModel3 <- rxLogisticRegression(like~reviewCatHash, data = trainReviews, l1Weight = 0, 
      mlTransforms = list(
    categoricalHash(vars = c(reviewCatHash = "review"), hashBits = 7), 
    selectFeatures("reviewCatHash", mode = minCount(count = 5))))
  summary(outModel3)
```






