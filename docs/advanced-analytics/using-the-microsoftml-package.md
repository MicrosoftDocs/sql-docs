---
title: "Using the MicrosoftML Package with SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "08/23/2017"
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
# Using the MicrosoftML package with SQL Server

The [**MicrosoftML**](https://msdn.microsoft.com/microsoft-r/microsoftml-introduction) package that is provided with Microsoft R Server and SQL Server 2017 includes multiple machine learning algorithms. These APIs were developed by Microsoft for internal machine learning applications, and have been refined over the years to support big data and high performance, with multicore processing and fast data streaming. MicrosoftML also includes numerous transformations for text processing and featurization.

In SQL Server 2017 CTP 2.0, support was added for the Python language. Python functions equivalent to those in the MicrosoftML package for R have been included in this release. Because Python conventions prefer lower case, the corresponding package name for Python is **microsoftml**.

+ **MicrosoftML for R**

    Introduction and package reference: [MicrosoftML: machine learning R algorithms](https://docs.microsoft.com/en-us/r-server/r-reference/microsoftml/microsoftml-package)

    Because R is case-sensitive, make sure that you reference the name correctly when loading the package.

+ **microsoftml for Python**

    Introduction and package reference: [microsoftml (Function library for Python)](https://docs.microsoft.com/r-server/python-reference/microsoftml/microsoftml-package). 

## What's in MicrosoftML

MicrosoftML contains a variety of machine learnign algorithms and transformations that ahve been highly optimized for performance.

### Machine learning algorithms

-  Linear models: `rxFastLinear` is a linear learner based on stochastic dual coordinate ascent that can be used for binary classification or regression. The model supports L1 and L2 regularization.

- Decision tree and decision forest models: `rxFastTree` is a boosted decision tree algorithm originally known as FastRank, which was developed for use in Bing. It is one of the fastest and most popular learners. Supports binary classification and regression.

  `rxFastForest` is a logistic regression model based on the random forest method. It is similar to the `rxLogit` function in RevoScaleR, but supports L1 and L2 regularization. Supports binary classification and regression.

- Logistic regression: `rxLogisticRegression` is a logistic regression model similar to the `rxLogit` function in RevoScaleR, with additional support for L1 and L2 regularization. Supports binary or multiclass classification.

- Neural networks: The `rxNeuralNet` function supports binary classification, multiclass classification, and regression using neural networks. Customizable and supports convoluted networks with GPU acceleration, using a single GPU.

- Anomaly detection.  The `rxOneClassSvm` function is based on the SVM method but is optimized for binary classification in unbalanced data sets.

### Transformation functions

MicrosoftML includes numerous specialized functions that are useful for transforming data and extracting features.

- Text processing capabilities include the `featurizeText` and `getSentiment` functions. You can count n-grams, detect the language used, or perform text normalization. You can also perform common text cleaning operations such as stopword removal, or generate hashed or count-based features from text.

- Feature selection and feature transformation functions, such as  `selectFeatures` or `getSentiment`, analyze data and create features that are most useful for modeling.

- Work with categorical variables by using such as `categorical` or `categoricalHash`, which convert categorical values into indexed arrays for better performance.
​
- Functions specific to image processing and analytics, such as `extractPixels` or `featurizeImage`, let you get the most information about of images and process images faster.

For more information, see [MicrosoftML functions](https://msdn.microsoft.com/microsoft-r/microsoftml/microsoftml).

## How to use MicrosoftML

This section describes how to locate and load the package in your R and Python code.

+ The MicrosoftML package for R is installed by default with Microsoft R Server 9.1.0 and in SQL Server 2017. 

    It is also available for use with SQL Server 2016 if you upgrade the R components using Microsoft R Server installer as described here: [Upgrade an instance of SQL Server using binding](r/use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md)

+ The microsoftml package for Python is installed by default in SQL Server 2017 (requires RC2 or later)

   An early version was released with RC1 but the library has undergone considerable revision. We strongly recommend that you get the latest release candidate.

However, for both R and Python, the package is not loaded by default; thus, you must explicitly load the package as part of your code to use its functions.

### Calling MicrosoftML functions from R in SQL Server

In your R code, load the MicrosoftML package and call its functions, like any other package.

```R
library(microsoftml);
library(RevoScaleR);
logisticRegression(args);
```

**Notes**

+ The MicrosoftML package is fully integrated with the data processing pipeline provided by the RevoScaleR package. Thus, you can use the MicrosoftML package in any Windows-based compute context, including an instance of SQL Server that has machine learning extensions enabled.

    However, MicrosoftML requires a reference to RevoScaleR and its functions in order to use remote compute contexts.

### Calling microsoftml functions from Python in SQL Server

To call functions from the package, in your Python code, import the **microsoftml** package, and import **revoscalepy** if you need to use remote compute contexts or related connectivity or data source objects. Then, reference the individual functions you need.

```Python
from microsoftml.modules.logistic_regression.rx_logistic_regression import rx_logistic_regression
from revoscalepy.functions.RxSummary import rx_summary
from revoscalepy.etl.RxImport import rx_import_datasource
```

**Notes**

+ In SQL Server 2017, **microsoftml** is a Python35-compatible module. 

+ The functions in **microsoftml** are integrated with the compute contexts and data sources that are supported by **revoscalepy**. Thus, you can use the **microsoftml** Python package to create and score from models in any Windows-based compute context, including an instance of SQL Server that has machine learning extensions. enabled.

    However, **microsoftml** for Python requires a reference to **revoscalepy** and its functions in order to use remote compute contexts.

For more information about revoscalepy, see:

+ [What is revoscalepy](python/what-is-revoscalepy.md)

+ [revoscalepy function library](https://docs.microsoft.com/en-us/r-server/python-reference/revoscalepy/revoscalepy-package) 