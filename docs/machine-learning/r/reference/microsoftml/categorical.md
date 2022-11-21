---
title: "categorical function (MicrosoftML)"
description: "Categorical transform that can be performed on data before  training a model."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (MicrosoftML), categorical, transform
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---


 # categorical: Machine Learning Categorical Data Transform 
 

Categorical transform that can be performed on data before 
training a model.


 ## Usage

```   
  categorical(vars, outputKind = "ind", maxNumTerms = 1e+06, terms = "",
    ...)

```

 ## Arguments



 ### `vars`
 A character vector or list of variable names to transform. If named, the names represent the names of new variables to be created. 



 ### `outputKind`
 A character string that specifies the kind of output kind.   
*   `"ind"`: Outputs an indicator vector. The input column is a vector   of categories, and the output contains one indicator vector per slot in   the input column.    
*   `"bag"`: Outputs a multi-set vector. If the input column is a  vector of categories, the output contains one vector, where the value in   each slot is the number of occurrences of the category in the input  vector. If the input column contains a single category, the indicator  vector and the bag vector are equivalent   
*   `"key"`: Outputs an index. The output is an integer ID (between 1 and the number of categories in the dictionary) of the category.   
 The default value is `"ind"`. 



 ### `maxNumTerms`
 An integer that specifies the maximum number of  categories to include in the dictionary. The default value is 1000000. 



 ### `terms`
 Optional character vector of terms or categories. 



 ### ` ...`
 Additional arguments sent to compute engine. 



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


 ## Value

A `maml` object defining the transform.

 ## Author(s)

Microsoft Corporation [`Microsoft Technical Support`](https://go.microsoft.com/fwlink/?LinkID=698556&clcid=0x409)



 ## See also

[rxFastTrees](rxFastTrees.md), [rxFastForest](rxFastForest.md),
[rxNeuralNet](rxNeuralNet.md), [rxOneClassSvm](rxOneClassSvm.md),
[rxLogisticRegression](rxLogisticRegression.md).

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


  # Use a categorical transform: the entire string is treated as a category
  outModel1 <- rxLogisticRegression(like~reviewCat, data = trainReviews, 
      mlTransforms = list(categorical(vars = c(reviewCat = "review"))))
  # Note that 'I hate it' and 'I love it' (the only strings appearing more than once)
  # have non-zero weights
  summary(outModel1)

  # Use the model to score
  scoreOutDF1 <- rxPredict(outModel1, data = testReviews, 
      extraVarsToWrite = "review")
  scoreOutDF1
```



