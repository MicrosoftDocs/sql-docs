---
title: Python tutorials
titleSuffix: SQL machine learning
description: This article describes Python tutorials for SQL machine learning. Learn how to run scripts and build machine learning models.
author: VanMSFT
ms.author: vanto
ms.date: 05/21/2020
ms.service: sql
ms.subservice: machine-learning
ms.topic: tutorial
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---
# Python tutorials for SQL machine learning
[!INCLUDE [SQL Server 2017 SQL MI](../../includes/applies-to-version/sqlserver2017-asdbmi.md)]

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
This article describes the Python tutorials and quickstarts for [Machine Learning Services on SQL Server](../sql-server-machine-learning-services.md) and on [Big Data Clusters](../../big-data-cluster/machine-learning-services.md).
::: moniker-end
::: moniker range="=sql-server-2017"
This article describes the Python tutorials and quickstarts for [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md).
::: moniker-end
::: moniker range="=azuresqldb-mi-current"
This article describes the Python tutorials and quickstarts for [Azure SQL Managed Instance Machine Learning Services](/azure/azure-sql/managed-instance/machine-learning-services-overview).
::: moniker-end

<a name="bkmk_pythontutorials"></a>

## Python tutorials

::: moniker range=">=sql-server-2017||>=sql-server-linux-ver15"
| Tutorial | Description |
|-|-|
| [Create a model using revoscalepy](use-python-revoscalepy-to-create-model.md) | Demonstrates how to run code from a remote Python client using SQL Server as compute context. The tutorial creates a model using **rxLinMod** from the **revoscalepy** library. |
| [Python data analytics for SQL developers](python-taxi-classification-introduction.md) | This end-to-end walkthrough demonstrates the process of building a complete Python solution using T-SQL. |
::: moniker-end

## Python quickstarts

If you are new to SQL machine learning, you can also try the Python quickstarts.

| Quickstart | Description |
|-|-|
| [Run simple Python scripts](quickstart-python-create-script.md) | Learn the basics of how to call Python in T-SQL using [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md). |
| [Data structures and objects using Python](quickstart-python-data-structures.md) | Shows how SQL uses the Python pandas package to handle data structures. |
| [Create and score a predictive model in Python](quickstart-python-train-score-model.md) | Explains how to create, train, and use a Python model to make predictions from new data. |

## Related content

- [Python language extension in SQL Server Machine Learning Services](../concepts/extension-python.md)
