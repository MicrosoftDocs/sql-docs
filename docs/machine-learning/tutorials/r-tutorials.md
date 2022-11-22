---
title: R tutorials
titleSuffix: SQL machine learning 
description: This article describes R tutorials for SQL machine learning. Learn how to run scripts and build machine learning models.
ms.service: sql
ms.subservice: machine-learning
ms.topic: tutorial
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: garye, jroth
ms.date: 05/07/2021
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---

# R tutorials for SQL machine learning
[!INCLUDE [SQL Server 2016 SQL MI](../../includes/applies-to-version/sqlserver2016-asdbmi.md)]

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
This article describes the R tutorials and quickstarts for [Machine Learning Services on SQL Server](../sql-server-machine-learning-services.md) and on [Big Data Clusters](../../big-data-cluster/machine-learning-services.md).
::: moniker-end
::: moniker range="=sql-server-2017"
This article describes the R tutorials and quickstarts for [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md).
::: moniker-end
::: moniker range="=sql-server-2016"
This article describes the R tutorials and quickstarts for [SQL Server 2016 R Services](../r/sql-server-r-services.md).
::: moniker-end
::: moniker range="=azuresqldb-mi-current"
This article describes the Python tutorials and quickstarts for [Azure SQL Managed Instance Machine Learning Services](/azure/azure-sql/managed-instance/machine-learning-services-overview).
::: moniker-end

<a name="bkmk_sqltutorials"></a>

## R tutorials

::: moniker range=">=sql-server-2016||>=sql-server-linux-ver15"
| Tutorial | Description |
|------|-------------|
| [Predict ski rental with decision tree](r-predictive-model-introduction.md) | Use R and a decision tree model to predict the number of future ski rentals. Use notebooks in Azure Data Studio for preparing data and training the model, and T-SQL for model deployment. |
| [Categorizing customers using k-means clustering](r-clustering-model-introduction.md) | Use R to develop and deploy a K-Means clustering model to categorize customers. Use notebooks in Azure Data Studio for preparing data and training the model, and T-SQL for model deployment. |
| [In-database R analytics for data scientists](../tutorials/walkthrough-data-science-end-to-end-walkthrough.md) | For R developers new to SQL machine learning, this tutorial explains how to perform common data science tasks in SQL. Load and visualize data, train and save a model in a database, and use the model for predictive analytics. |
| [In-database R analytics for SQL developers](../tutorials/r-taxi-classification-introduction.md) | Build and deploy a complete R solution, using only SQL tools. Focuses on moving a solution into production. You'll learn how to wrap R code in a stored procedure, save an R model in a database, and make parameterized calls to the R model for prediction. |
::: moniker-end
::: moniker range="=azuresqldb-mi-current"
| Tutorial | Description |
|------|-------------|
| [Predict ski rental with decision tree](r-predictive-model-introduction.md) | Use R and a decision tree model to predict the number of future ski rentals. Use notebooks in Azure Data Studio for preparing data and training the model, and T-SQL for model deployment. |
| [Categorizing customers using k-means clustering](r-clustering-model-introduction.md) | Use R to develop and deploy a K-Means clustering model to categorize customers. Use notebooks in Azure Data Studio for preparing data and training the model, and T-SQL for model deployment. |
::: moniker-end

## R quickstarts

If you are new to SQL machine learning, you can also try the R quickstarts.

| Quickstart | Description |
|-|-|
| [Run simple R scripts](quickstart-r-create-script.md) | Learn the basics of how to call R in T-SQL using [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md). |
| [Data structures and objects using R](quickstart-r-data-types-and-objects.md) | Shows how SQL uses the R to handle data structures. |
| [Create and score a predictive model in R](quickstart-r-data-types-and-objects.md) | Explains how to create, train, and use a R model to make predictions from new data. |

## Next steps

+ [R extension in SQL Server](../concepts/extension-r.md)
