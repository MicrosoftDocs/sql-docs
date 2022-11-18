---
title: microsoftml Python package
description: microsoftml is a Python package from Microsoft that provides high-performance machine learning algorithms. It includes functions for training and transformations, scoring, text and image analysis, and feature extraction for deriving values from existing data. The package is included in SQL Server Machine Learning Services.
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 08/03/2021
ms.topic: reference
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15"
---
# microsoftml (Python package in SQL Server Machine Learning Services)
[!INCLUDE [SQL Server 2017 and later](../../includes/applies-to-version/sqlserver2017.md)]

**microsoftml** is a Python package from Microsoft that provides high-performance machine learning algorithms. It includes functions for training and transformations, scoring, text and image analysis, and feature extraction for deriving values from existing data. The package is included in [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) and supports high performance on big data, using multicore processing, and fast data streaming.

| Package details       | Information |
|-----------------------|-------------|
| Current version:      |  9.4        |
| Built on:             | [Anaconda 4.2](https://anaconda.org/conda-forge/opencv/files?version=4.2.0) distribution of [Python 3.7.1](https://www.python.org/doc) |
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
|[microsoftml.rx_ensemble](reference/microsoftml/rx-ensemble.md) | Train an ensemble of models. |
|[microsoftml.rx_fast_forest](reference/microsoftml/rx-fast-forest.md)  | Random Forest. |
|[microsoftml.rx_fast_linear](reference/microsoftml/rx-fast-linear.md) | Linear Model. with Stochastic Dual Coordinate Ascent. |
|[microsoftml.rx_fast_trees](reference/microsoftml/rx-fast-trees.md) | Boosted Trees. |
|[microsoftml.rx_logistic_regression](reference/microsoftml/rx-logistic-regression.md) | Logistic Regression. |
|[microsoftml.rx_neural_network](reference/microsoftml/rx-neural-network.md) | Neural Network. |
|[microsoftml.rx_oneclass_svm](reference/microsoftml/rx-oneclass-svm.md) | Anomaly Detection. |

<a name="ml-transforms"></a>

## 2-Transform functions

### Categorical variable handling

| Function | Description |
|----------|-------------|
|[microsoftml.categorical](reference/microsoftml/categorical.md) | Converts a text column into categories. |
|[microsoftml.categorical_hash](reference/microsoftml/categorical-hash.md) | Hashes and converts a text column into categories. |

### Schema manipulation

| Function | Description |
|----------|-------------|
|[microsoftml.concat](reference/microsoftml/concat.md) | Concatenates multiple columns into a single vector. |
|[microsoftml.drop_columns](reference/microsoftml/drop-columns.md) | Drops columns from a dataset. |
|[microsoftml.select_columns](reference/microsoftml/select-columns.md) | Retains columns of a dataset. |

### Variable selection

| Function | Description |
|----------|-------------|
|[microsoftml.count_select](reference/microsoftml/count-select.md) |Feature selection based on counts. |
|[microsoftml.mutualinformation_select](reference/microsoftml/mutualinformation-select.md) | Feature selection based on mutual information. |

### Text analytics

| Function | Description |
|----------|-------------|
|[microsoftml.featurize_text](reference/microsoftml/featurize-text.md) | Converts text columns into numerical features. |
|[microsoftml.get_sentiment](reference/microsoftml/get-sentiment.md) | Sentiment analysis. |

### Image analytics

| Function | Description |
|----------|-------------|
|[microsoftml.load_image](reference/microsoftml/load-image.md) | Loads an image. |
|[microsoftml.resize_image](reference/microsoftml/resize-image.md) | Resizes an Image. |
|[microsoftml.extract_pixels](reference/microsoftml/extract-pixels.md) | Extracts pixels from an image. |
|[microsoftml.featurize_image](reference/microsoftml/featurize-image.md) | Converts an image into features. |

### Featurization functions

| Function | Description |
|----------|-------------|
|[microsoftml.rx_featurize](reference/microsoftml/rx-featurize.md) | Data transformation for data sources |

<a name="ml-scoring"></a>

## Scoring functions

| Function | Description |
|----------|-------------|
|[microsoftml.rx_predict](reference/microsoftml/rx-predict.md) | Scores using a Microsoft machine learning model |

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
