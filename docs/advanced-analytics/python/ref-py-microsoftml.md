---
title: microsoftml Python package - SQL Server Machine Learning Services
description: Introduces the Microsoft machine learning algorithms and models for Python, as related to SQL Server machine learning workloads.
ms.prod: sql
ms.technology: machine-learning

ms.date: 12/04/2018  
ms.topic: conceptual
ms.author: dphansen
ms.author: davidph
manager: cgronlun
---
# microsoftml (Python module in SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

**microsoftml** is a Python35-compatible module from Microsoft providing high-performance machine learning algorithms. It includes functions for training and transformations, scoring, text and image analysis, and feature extraction for deriving values from existing data.

The machine learning APIs were developed by Microsoft for internal machine learning applications and have been refined over the years to support high performance on big data, using multicore processing and fast data streaming. This package originated as a Python equivalent of an R version, [MicrosoftML](../r/ref-r-microsoftml.md), that has similar functions. 

## Full reference documentation

The **microsoftml** library is distributed in multiple Microsoft products, but usage is the same whether you get the library in SQL Server or another product. Because the functions are the same, [documentation for individual microsoftml functions](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/microsoftml-package) is published to just one location under the [Python reference](https://docs.microsoft.com/machine-learning-server/python-reference/introducing-python-package-reference) for Microsoft Machine Learning Server. Should any product-specific behaviors exist, discrepancies will be noted in the function help page.

## Versions and platforms

The **microsoftml** module is based on Python 3.5 and available only when you install one of the following Microsoft products or downloads:

+ [SQL Server 2017 Machine Learning Services](../install/sql-machine-learning-services-windows-install.md)
+ [Microsoft Machine Learning Server 9.2.0 or later](https://docs.microsoft.com/machine-learning-server/)
+ [Python client libraries for a data science client](setup-python-client-tools-sql.md)

> [!NOTE]
> Full product release versions are Windows-only, starting with SQL Server 2017. Linux support for **microsoftml** is new in [SQL Server 2019 Preview](../../linux/sql-server-linux-setup-machine-learning.md).

## Package dependencies

Algorithms in **microsoftml** depend on [revoscalepy](ref-py-revoscalepy.md) for:

+ Data source objects. Data consumed by **microsoftml** functions are created using **revoscalepy** functions.
+ Remote computing (shifting function execution to a remote SQL Server instance). The **revoscalepy** library provides functions for creating and activating a remote compute context for SQL server.

In most cases, you will load the packages together whenever you are using **microsoftml**.

## Functions by category

This section lists the functions by category to give you an idea of how each one is used. You can also use the [table of contents](https://docs.microsoft.com/machine-learning-server/python-reference/introducing-python-package-reference) to find functions in alphabetical order.

## 1-Training functions

| Function | Description |
|----------|-------------|
|[microsoftml.rx_ensemble](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/rx-ensemble) | Train an ensemble of models. |
|[microsoftml.rx_fast_forest](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/rx-fast-forest)  | Random Forest. |
|[microsoftml.rx_fast_linear](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/rx-fast-linear) | Linear Model. with Stochastic Dual Coordinate Ascent. |
|[microsoftml.rx_fast_trees](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/rx-fast-trees) | Boosted Trees. |
|[microsoftml.rx_logistic_regression](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/rx-logistic-regression) | Logistic Regression. |
|[microsoftml.rx_neural_network](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/rx-neural-network) | Neural Network. |
|[microsoftml.rx_oneclass_svm](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/rx-oneclass-svm) | Anomaly Detection. |

<a name="ml-transforms"></a>

## 2-Transform functions

### Categorical variable handling

| Function | Description |
|----------|-------------|
|[microsoftml.categorical](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/categorical) | Converts a text column into categories. |
|[microsoftml.categorical_hash](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/categorical-hash) | Hashes and converts a text column into categories. |

### Schema manipulation

| Function | Description |
|----------|-------------|
|[microsoftml.concat](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/concat) | Concatenates multiple columns into a single vector. |
|[microsoftml.drop_columns](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/drop-columns) | Drops columns from a dataset. |
|[microsoftml.select_columns](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/select-columns) | Retains columns of a dataset. |


### Variable selection

| Function | Description |
|----------|-------------|
|[microsoftml.count_select](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/count-select) |Feature selection based on counts. |
|[microsoftml.mutualinformation_select](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/mutualinformation-select) | Feature selection based on mutual information. |


### Text analytics

| Function | Description |
|----------|-------------|
|[microsoftml.featurize_text](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/featurize-text) | Converts text columns into numerical features. |
|[microsoftml.get_sentiment](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/get-sentiment) | Sentiment analysis. |


### Image analytics 

| Function | Description |
|----------|-------------|
|[microsoftml.load_image](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/load-image) | Loads an image. |
|[microsoftml.resize_image](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/resize-image) | Resizes an Image. |
|[microsoftml.extract_pixels](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/extract-pixels) | Extracts pixels from an image. |
|[microsoftml.featurize_image](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/featurize-image) | Converts an image into features. |

### Featurization functions

| Function | Description |
|----------|-------------|
|[microsoftml.rx_featurize](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/rx-featurize) | Data transformation for data sources |

<a name="ml-scoring"></a>

## 3-Scoring functions

| Function | Description |
|----------|-------------|
|[microsoftml.rx_predict](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/rx-predict) | Scores using a Microsoft machine learning model |

## How to call microsoftml

Functions in **microsoftml** are callable in Python code encapsulated in stored procedures. Most developers build **microsoftml** solutions locally, and then migrate finished Python code to stored procedures as a deployment exercise.

The **microsoftml** package for Python is installed by default, but unlike **revoscalepy**, it is not loaded by default when you start a Python session using the Python executables installed with SQL Server.

As a first step, import the **microsoftml** package, and import **revoscalepy** if you need to use remote compute contexts or related connectivity or data source objects. Then, reference the individual functions you need.

```python
from microsoftml.modules.logistic_regression.rx_logistic_regression import rx_logistic_regression
from revoscalepy.functions.RxSummary import rx_summary
from revoscalepy.etl.RxImport import rx_import_datasource
```

## See also

+ [Python tutorials](../tutorials/sql-server-python-tutorials.md)
+ [Tutorial: Embed Python code in T-SQL](../tutorials/run-python-using-t-sql.md)
+ [Python Reference (Microsoft Machine Learning Server)](https://docs.microsoft.com/machine-learning-server/python-reference/introducing-python-package-reference)

