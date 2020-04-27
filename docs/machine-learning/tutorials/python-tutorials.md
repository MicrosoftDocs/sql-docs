---
title: Python tutorials
titleSuffix: SQL machine learning
description: This article describes Python tutorials for SQL machine learning. Learn how to run scripts and build machine learning models.
ms.prod: sql
ms.technology: machine-learning
ms.date: 04/15/2020
ms.topic: tutorial
author: dphansen
ms.author: davidph
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Python tutorials for SQL machine learning
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
This article describes the Python tutorials and quickstarts for [Machine Learning Services on SQL Server](../sql-server-machine-learning-services.md) and on [Big Data Clusters](../../big-data-cluster/machine-learning-services.md).
::: moniker-end
::: moniker range="=sql-server-2017||=sqlallproducts-allversions"
This article describes the Python tutorials and quickstarts for [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md).
::: moniker-end

<a name="bkmk_pythontutorials"></a>

## Python tutorials

| Tutorial | Description |
|-|-|
| [Predict ski rental with linear regression](python-ski-rental-linear-regression.md) | Use Python and linear regression to predict the number of ski rentals. Use notebooks in Azure Data Studio for preparing data and training the model, and T-SQL for model deployment. |
| [Categorizing customers using k-means clustering](python-clustering-model.md) | Use Python to develop and deploy a K-Means clustering model to categorize customers. Use notebooks in Azure Data Studio for preparing data and training the model, and T-SQL for model deployment. |
| [Create a model using revoscalepy](use-python-revoscalepy-to-create-model.md) | Demonstrates how to run code from a remote Python client using SQL Server as compute context. The tutorial creates a model using **rxLinMod** from the **revoscalepy** library. |
| [Python data analytics for SQL developers](sqldev-in-database-python-for-sql-developers.md) | This end-to-end walkthrough demonstrates the process of building a complete Python solution using T-SQL. |

## Python quickstarts

If you are new to SQL machine learning, you can also try the Python quickstarts.

| Quickstart | Description |
|-|-|
| [Run simple Python scripts](quickstart-python-create-script.md) | Learn the basics of how to call Python in T-SQL using [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md). |
| [Data structures and objects using Python](quickstart-python-data-structures.md) | Shows how SQL uses the Python pandas package to handle data structures. |
| [Create and score a predictive model in Python](quickstart-python-train-score-model.md) | Explains how to create, train, and use a Python model to make predictions from new data. |

## Next steps

+ [Python extension to SQL Server](../concepts/extension-python.md)
