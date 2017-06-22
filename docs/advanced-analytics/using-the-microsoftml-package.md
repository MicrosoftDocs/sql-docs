---
title: "Using the MicrosoftML Package with SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "06/22/2017"
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
caps.latest.revision: 132
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Using the MicrosoftML Package with SQL Server

The [**MicrosoftML**](https://msdn.microsoft.com/microsoft-r/microsoftml-introduction) package that is provided with Microsoft R Server and SQL Server 2017 includes multiple machine learning algorithms. These APIs were developed by Microsoft for internal machine learning applications, and have been refined over the years to support big data and high performance, with multicore processing and fast data streaming. MicrosoftML also includes numerous transformations for text processing and featurization.

In SQL Server 2017 CTP 2.0, support was added for the Python language. Python functions equivalent to those in the MicrosoftML package for R have been included in this release.

## What's in MicrosoftML

**Machine learning algorithms**

-  Linear models: `rxFastLinear` is a linear learner based on stochastic dual coordinate ascent that can be used for binary classification or regression. The model supports L1 and L2 regularization.

- Decision tree and decision forest models: `rxFastTree` is a boosted decision tree algorithm originally known as FastRank, which was developed for use in Bing. It is one of the fastest and most popular learners. Supports binary classification and regression.

  `rxFastForest` is a logistic regression model based on the random forest method. It is similar to the `rxLogit` function in RevoScaleR, but supports L1 and L2 regularization. Supports binary classification and regression.

- Logistic regression: `rxLogisticRegression` is a logistic regression model similar to the `rxLogit` function in RevoScaleR, with additional support for L1 and L2 regularization. Supports binary or multiclass classification .

- Neural networks: The `rxNeuralNet` function supports binary classification, multiclass classification, and regression using neural networks. Customizable and supports convoluted networks with GPU acceleration, using a single GPU.

- Anomaly detection.  The `rxOneClassSvm` function is based on the SVM method but is optimized for binary classification in unbalanced data sets.

**Transformation functions**

MicrosoftML includes numerous specialized functions that are useful for transforming data and extracting features.

- Text processing capabilities include the `featurizeText` and `getSentiment` functions. You can count ngrams, detect the language used, or perform text normalization. You can also perform common text cleaning operations such as stopword removal, or generate hashed or count-based features from text.

- Feature selection and feature transformation functions, such as  `selectFeatures` or `getSentiment`, analyze data and create features that are most useful for modeling.

- Work with categorical variables by using such as `categorical` or `categoricalHash`, which convert categorical values into indexed arrays for better performance.
​
- Functions specific to image processing and analytics, such as `extractPixels` or `featurizeImage`, let you get the most information about of images and process images faster.

For more information, see [MicrosoftML functions](https://msdn.microsoft.com/microsoft-r/microsoftml/microsoftml).

## How to use MicrosoftML in R Services

The MicrosoftML package is fully integrated with the data processing pipeline used by the RevoScaleR package. Currently, you can use the MicrosoftML package in any Windows-based compute context, including SQL Server R Services.

In your R code, load the MicrosoftML package and call its functions, like you would with any other package.

```R
library(microsoftml);
library(RevoScaleR);
logisticRegression(args);
```

## How to use MicrosoftML in Python Services

In SQL Server 2017 CTP 2.0, most MicrosoftML functions have been ported to Python and are available as Python35-compatible modules. The functions are integrated with the compute contexts and data sources that are supported by **revoscalepy**. What this means is that you can define and train a MicrosoftML model in Python and use it for prediction, running either locally, or in any Windows-based compute context (including SQL Server).

In your Python code, import the MicrosoftML module, as well as the revoscalepy module, and then reference the individual functions you need.

```Python
from microsoftml.modules.logistic_regression.rx_logistic_regression import rx_logistic_regression
from revoscalepy.functions.RxSummary import rx_summary
from revoscalepy.etl.RxImport import rx_import_datasource
```
