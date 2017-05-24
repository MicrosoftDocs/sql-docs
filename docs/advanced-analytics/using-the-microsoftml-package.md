---
title: "Using the MicrosoftML Package with SQL Server R Services | Microsoft Docs"
ms.custom: ""
ms.date: "01/27/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
dev_langs: 
  - "R"
ms.assetid: 1c377717-e281-431e-8171-3924dcce1cdd
caps.latest.revision: 12
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Using the MicrosoftML Package with SQL Server R Services
The [**MicrosoftML**](https://msdn.microsoft.com/microsoft-r/microsoftml-introduction) package that is provided with Microsoft R Server and SQL Server 2017 CTP 1.0 includes multiple machine learning algorithms developed by Microsoft that support multicore processing and fast data streaming. The package also includes transformations for text processing and featurization.

### New machine learning algorithms


-  **Fast Linear.** A linear learner based on stochastic dual coordinate ascent that can be used for binary classification or regression. The model supports L1 and L2 regularization.

- **Fast Tree.** A boosted decision tree algorithm originally known as FastRank, which was developed for use in Bing. It is one of the fastest and most popular learners. Supports binary classification and regression.

- **Fast Forest.** A logistic regression model based on the random forest method. It is similar to the `rxLogit` function in RevoScaleR, but supports L1 and L2 regularization. Supports binary classification and regression.

- **Logistic Regression.** A logistic regression model similar to the `rxLogit` function in RevoScaleR, with additional support for L1 and L2 regularization. Supports binary or multiclass classification .

- **Neural Net.** A neural network model for binary classification, multiclass classification, and regression. Supports customizable convoluted networks and GPU acceleration using a single GPU.

- **One-Class SVM.** An anomaly detection model based on the SVM method that can be used for binary classification in unbalanced data sets.

## Transformation functions

The **MicrosoftML** package also includes the following functions that can be used for transforming data and extracting features.

- `featurizeText()`
 
  Generates counts of ngrams in a given text string. 

  The function includes the options to detect the language used, perform text tokenization and normalization, remove stopwords, and generate features from text. 

- `categorical()`

  Builds a dictionary of categories and transforms each category into an indicator vector. 
 
- `categoricalHash()`

  Converts categorical values into an indicator array by hashing the value and using the hash as an index in the bag.  
​
- `selectFeatures()` 

  Selects a subset of features from the given variable, either by counting non-default values, or by computing a mutual-information score with respect to the label​. 

For detailed information about these new features, see [MicrosoftML functions](https://msdn.microsoft.com/microsoft-r/microsoftml/microsoftml).

## Support for MicrosoftML in R Services

The **MicrosoftML** package is fully integrated with the data processing pipeline used by the **RevoScaleR** package. Currently, you can use the **MicrosoftML** package in any Windows-based compute context, including SQL Server R Services.



## See Also

[Introduction to MicrosoftML](https://msdn.microsoft.com/microsoft-r/microsoftml-introduction)

[RevoScaleR Function Reference](https://msdn.microsoft.com/microsoft-r/scaler/scaler)


