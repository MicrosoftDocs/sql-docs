---
title: microsoftml Python package
description: microsoftml is a Python package from Microsoft that provides high-performance machine learning algorithms. It includes functions for training and transformations, scoring, text and image analysis, and feature extraction for deriving values from existing data. The package is included in SQL Server Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning-services
ms.date: 08/03/2021
ms.topic: reference
author: garyericson
ms.author: garye
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15"
---
# microsoftml (Python package in SQL Server Machine Learning Services)
[!INCLUDE [SQL Server 2017 and later](../../includes/applies-to-version/sqlserver2017.md)]

**microsoftml** is a Python package from Microsoft that provides high-performance machine learning algorithms. It includes functions for training and transformations, scoring, text and image analysis, and feature extraction for deriving values from existing data. The package is included in [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) and supports high performance on big data, using multicore processing, and fast data streaming.

| Package details       | Information |
|-----------------------|-------------|
| Current version:      |  9.4        |
| Built on:             | [Anaconda 4.2](https://www.continuum.io/why-anaconda) distribution of [Python 3.7.1](https://www.python.org/doc) |
| Package distribution: | [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) version 2017 or 2019. |

## How to use microsoftml

The **microsoftml** module is installed as part of SQL Server Machine Learning Services when you add Python to your installation. You get the full collection of proprietary packages plus a Python distribution with its modules and interpreters. You can use any Python IDE to write Python script calling functions in **microsoftml**, but the script must run on a computer having SQL Server Machine Learning Services with Python.

**Microsoftml** and **revoscalepy** are tightly coupled; data sources used in **microsoftml** are defined as [**revoscalepy**](/machine-learning-server/python-reference/revoscalepy/revoscalepy-package) objects. Compute context limitations in **revoscalepy** transfer to **microsoftml**. Namely, all functionality is available for local operations, but switching to a remote compute context requires [RxSpark](/machine-learning-server/python-reference/revoscalepy/rxspark) or [RxInSQLServer](/machine-learning-server/python-reference/revoscalepy/rxinsqlserver).

## Versions and platforms

The **microsoftml** module is available only when you install one of the following Microsoft products or downloads:

+ [SQL Server Machine Learning Services](../install/sql-machine-learning-services-windows-install.md)
+ [Python client libraries for a data science client](setup-python-client-tools-sql.md)

> [!NOTE]
> Full product release versions are Windows-only in SQL Server 2017. Both Windows and Linux are supported for **microsoftml** in [SQL Server 2019](../../linux/sql-server-linux-setup-machine-learning.md).

## Package dependencies

Algorithms in **microsoftml** depend on [revoscalepy](ref-py-revoscalepy.md) for:

+ Data source objects - Data consumed by **microsoftml** functions are created using **revoscalepy** functions.
+ Remote computing (shifting function execution to a remote SQL Server instance) - The **revoscalepy** package provides functions for creating and activating a remote compute context for SQL server.

In most cases, you will load the packages together whenever you are using **microsoftml**.

## Functions by category

This section lists the functions by category to give you an idea of how each one is used. You can also use the table of contents to find functions in alphabetical order.

## 1-Training functions

| Function | Description |
|----------|-------------|
|[microsoftml.rx_ensemble](/sql/machine-learning/python/reference/microsoftml/rx-ensemble) | Train an ensemble of models. |
|[microsoftml.rx_fast_forest](/sql/machine-learning/python/reference/microsoftml/rx-fast-forest)  | Random Forest. |
|[microsoftml.rx_fast_linear](/sql/machine-learning/python/reference/microsoftml/rx-fast-linear) | Linear Model. with Stochastic Dual Coordinate Ascent. |
|[microsoftml.rx_fast_trees](/sql/machine-learning/python/reference/microsoftml/rx-fast-trees) | Boosted Trees. |
|[microsoftml.rx_logistic_regression](/sql/machine-learning/python/reference/microsoftml/rx-logistic-regression) | Logistic Regression. |
|[microsoftml.rx_neural_network](/sql/machine-learning/python/reference/microsoftml/rx-neural-network) | Neural Network. |
|[microsoftml.rx_oneclass_svm](/sql/machine-learning/python/reference/microsoftml/rx-oneclass-svm) | Anomaly Detection. |

<a name="ml-transforms"></a>

## 2-Transform functions

### Categorical variable handling

| Function | Description |
|----------|-------------|
|[microsoftml.categorical](/sql/machine-learning/python/reference/microsoftml/categorical) | Converts a text column into categories. |
|[microsoftml.categorical_hash](/sql/machine-learning/python/reference/microsoftml/categorical-hash) | Hashes and converts a text column into categories. |

### Schema manipulation

| Function | Description |
|----------|-------------|
|[microsoftml.concat](/sql/machine-learning/python/reference/microsoftml/concat) | Concatenates multiple columns into a single vector. |
|[microsoftml.drop_columns](/sql/machine-learning/python/reference/microsoftml/drop-columns) | Drops columns from a dataset. |
|[microsoftml.select_columns](/sql/machine-learning/python/reference/microsoftml/select-columns) | Retains columns of a dataset. |

### Variable selection

| Function | Description |
|----------|-------------|
|[microsoftml.count_select](/sql/machine-learning/python/reference/microsoftml/count-select) |Feature selection based on counts. |
|[microsoftml.mutualinformation_select](/sql/machine-learning/python/reference/microsoftml/mutualinformation-select) | Feature selection based on mutual information. |

### Text analytics

| Function | Description |
|----------|-------------|
|[microsoftml.featurize_text](/sql/machine-learning/python/reference/microsoftml/featurize-text) | Converts text columns into numerical features. |
|[microsoftml.get_sentiment](/sql/machine-learning/python/reference/microsoftml/get-sentiment) | Sentiment analysis. |

### Image analytics

| Function | Description |
|----------|-------------|
|[microsoftml.load_image](/sql/machine-learning/python/reference/microsoftml/load-image) | Loads an image. |
|[microsoftml.resize_image](/sql/machine-learning/python/reference/microsoftml/resize-image) | Resizes an Image. |
|[microsoftml.extract_pixels](/sql/machine-learning/python/reference/microsoftml/extract-pixels) | Extracts pixels from an image. |
|[microsoftml.featurize_image](/sql/machine-learning/python/reference/microsoftml/featurize-image) | Converts an image into features. |

### Featurization functions

| Function | Description |
|----------|-------------|
|[microsoftml.rx_featurize](/sql/machine-learning/python/reference/microsoftml/rx-featurize) | Data transformation for data sources |

<a name="ml-scoring"></a>

## Scoring functions

| Function | Description |
|----------|-------------|
|[microsoftml.rx_predict](/sql/machine-learning/python/reference/microsoftml/rx-predict) | Scores using a Microsoft machine learning model |

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

+ [Python tutorials](../tutorials/python-tutorials.md)
+ [Manage Python packages](../package-management/python-package-information.md)
