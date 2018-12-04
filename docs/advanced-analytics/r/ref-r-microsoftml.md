---
title: MicrosoftML R function library in SQL Server Machine Learning | Microsoft Docs
description: Introduction to the MicrosoftML function library in SQL Server 2016 R Services and SQL Server 2017 Machine Learning Services with R.
ms.prod: sql
ms.technology: machine-learning

ms.date: 12/04/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# MicrosoftML R library in SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

The **MicrosoftML** package is an R function library from Microsoft that provides machine learning algorithms. It is included in several products, including SQL Server 2016 R Services, SQL Server 2017 Machine Learning Services (with R), Microsoft Machine Learning Server, Microsoft R Client, and several virtual machine images on Azure. 

The machine learning APIs were developed by Microsoft for internal machine learning applications, and have been refined over the years to support high performance on big data, using multicore processing and fast data streaming. MicrosoftML also includes numerous transformations for text and image processing.

In SQL Server 2017, support was added for the Python language. The [microsoftml package for Python](../python/ref-py-microsoftml.md) contains functions equivalent to those in the MicrosoftML package for R. 

## Machine learning algorithms

-  Linear models: `rxFastLinear` is a linear learner based on stochastic dual coordinate ascent that can be used for binary classification or regression. The model supports L1 and L2 regularization.

- Decision tree and decision forest models: `rxFastTree` is a boosted decision tree algorithm originally known as FastRank, which was developed for use in Bing. It is one of the fastest and most popular learners. Supports binary classification and regression.

  `rxFastForest` is a logistic regression model based on the random forest method. It is similar to the `rxLogit` function in RevoScaleR, but supports L1 and L2 regularization. Supports binary classification and regression.

- Logistic regression: `rxLogisticRegression` is a logistic regression model similar to the `rxLogit` function in RevoScaleR, with additional support for L1 and L2 regularization. Supports binary or multiclass classification.

- Neural networks: The `rxNeuralNet` function supports binary classification, multiclass classification, and regression using neural networks. Customizable and supports convoluted networks with GPU acceleration, using a single GPU.

- Anomaly detection.  The `rxOneClassSvm` function is based on the SVM method but is optimized for binary classification in unbalanced data sets.

## Transformation functions

MicrosoftML includes numerous specialized functions that are useful for transforming data and extracting features.

- Text processing capabilities include the `featurizeText` and `getSentiment` functions. You can count n-grams, detect the language used, or perform text normalization. You can also perform common text cleaning operations such as stopword removal, or generate hashed or count-based features from text.

- Feature selection and feature transformation functions, such as  `selectFeatures` or `getSentiment`, analyze data and create features that are most useful for modeling.

- Work with categorical variables by using such as `categorical` or `categoricalHash`, which convert categorical values into indexed arrays for better performance.
​
- Functions specific to image processing and analytics, such as `extractPixels` or `featurizeImage`, let you get the most information about of images and process images faster.

For more information, see [MicrosoftML functions](https://msdn.microsoft.com/microsoft-r/microsoftml/microsoftml).

## How to use MicrosoftML

This section describes how to locate and load the package. The MicrosoftML package for R is installed by default with Microsoft R Server 9.1.0 and in SQL Server 2017.

It is also available for use with SQL Server 2016, if you upgrade the R components for the instance, by using the Microsoft R Server installer as described here: [Upgrade an instance of SQL Server using binding](use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md)

The package is not loaded by default; thus, you must explicitly load the package as part of your code to use its functions.

### Calling MicrosoftML functions from R in SQL Server

In your R code, load the MicrosoftML package and call its functions, like any other package.

```R
library(microsoftml);
library(RevoScaleR);
logisticRegression(args);
```

The MicrosoftML package is fully integrated with the data processing pipeline provided by the RevoScaleR package. Thus, you can use the MicrosoftML package in any Windows-based compute context, including an instance of SQL Server that has machine learning extensions enabled.

However, MicrosoftML requires a reference to RevoScaleR and its functions in order to use remote compute contexts.

## See Also

[R tutorials](../tutorials/sql-server-r-tutorials.md)

