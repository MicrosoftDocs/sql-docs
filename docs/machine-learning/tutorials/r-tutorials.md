---
title: R tutorials
titleSuffix: SQL machine learning 
description: This article describes R tutorials for SQL machine learning. Learn how to run scripts and build machine learning models.
ms.prod: sql
ms.technology: machine-learning
ms.topic: tutorial
author: cawrites
ms.author: chadam
ms.reviewer: garye, davidph
ms.date: 05/04/2020  
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---

# R tutorials for SQL machine learning

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
This article describes the R tutorials and quickstarts for [Machine Learning Services on SQL Server](../sql-server-machine-learning-services.md) and on [Big Data Clusters](../../big-data-cluster/machine-learning-services.md).
::: moniker-end
::: moniker range="=sql-server-2017||=sqlallproducts-allversions"
This article describes the R tutorials and quickstarts for [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md).
::: moniker-end
::: moniker range="=sql-server-2016||=sqlallproducts-allversions"
This article describes the R tutorials and quickstarts for [SQL Server 2016 R Services](../r/sql-server-r-services.md).
::: moniker-end

+ Learn how to run R scripts.
+ Build, train, and deploy R models to SQL Server.
+ Learn about remote and local compute contexts.
+ Explore the Microsoft R  packages for data science and machine learning.

<a name="bkmk_sqltutorials"></a>

## R tutorials

| Tutorial | Description |
|------|-------------|
| [Predict ski rental with decision tree](r-predictive-model-introduction.md) | Use R and a decision tree model to predict the number of future ski rentals. Use notebooks in Azure Data Studio for preparing data and training the model, and T-SQL for model deployment. |
| [Categorizing customers using k-means clustering](r-clustering-model-introduction.md) | Use R to develop and deploy a K-Means clustering model to categorize customers. Use notebooks in Azure Data Studio for preparing data and training the model, and T-SQL for model deployment. |
| [In-database R analytics for data scientists](../tutorials/walkthrough-data-science-end-to-end-walkthrough.md) | For R developers new to SQL Server, this tutorial explains how to perform common data science tasks in SQL Server. Load and visualize data, train and save a model to SQL Server, and use the model for predictive analytics. |
| [In-database R analytics for SQL developers](../tutorials/sqldev-in-database-r-for-sql-developers.md) | Build and deploy a complete R solution, using only [!INCLUDE[tsql](../../includes/tsql-md.md)] tools. Focuses on moving a solution into production. You'll learn how to wrap R code in a stored procedure, save an R model to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, and make parameterized calls to the R model for prediction. |

## R quickstarts

If you are new to SQL machine learning, you can also try the R quickstarts.

| Quickstart | Description |
|-|-|
| [Run simple R scripts](quickstart-r-create-script.md) | Learn the basics of how to call R in T-SQL using [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md). |
| [Data structures and objects using R](quickstart-r-data-types-and-objects.md) | Shows how SQL uses the R to handle data structures. |
| [Create and score a predictive model in R](quickstart-r-data-types-and-objects.md) | Explains how to create, train, and use a R model to make predictions from new data. |

## Next steps

For more technical detail about R in SQL Server, see [R language extension in SQL Server](../concepts/extension-r.md).
