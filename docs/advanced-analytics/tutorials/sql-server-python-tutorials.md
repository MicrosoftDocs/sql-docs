---
title: SQL Server 2017 Python tutorial overview - SQL Server Machine Learning
description: Introduction to the Python tutorials for SQL Server 2017 in-database analytics.
ms.prod: sql
ms.technology: machine-learning

ms.date: 12/18/2018  
ms.topic: tutorial
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# SQL Server 2017 Python tutorials
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes the Python tutorials for in-database analytics on [SQL Server 2017 Machine Learning Services](../install/sql-machine-learning-services-windows-install.md). 

+ Learn how to wrap and run Python code in stored procedures.
+ Serialize and save Python-based models to SQL Server databases.
+ Learn about remote and local compute contexts, and when to use them.
+ Explore the Microsoft Python modules for data science and machine learning tasks.

<a name="bkmk_pythontutorials"></a>

## Python quickstarts and tutorials

| Link | Description |
|------|-------------|
| [Quickstart: "Hello world" Python script in SQL Server](quickstart-python-run-using-t-sql.md) | Learn the basics of how to call Python in T-SQL. |
| [Quickstart: Create, train, and use a Python model with stored procedures in SQL Server](quickstart-python-train-score-in-tsql.md) | Explains the mechanics of embedding Python code in a stored procedure, providing inputs, and stored procedure execution. |
| [Tutorial: Create a model using revoscalepy](use-python-revoscalepy-to-create-model.md) | Demonstrates how to run code from a remote Python terminal, using SQL Server compute context. You should be somewhat familiar with Python tools and environments. Sample code is provided that creates a model using **rxLinMod**, from the new **revoscalepy** library. |
| [Tutorial: Learn in-Database Python analytics for SQL developers](sqldev-in-database-python-for-sql-developers.md) | This end-to-end walkthrough demonstrates the process of building a complete Python solution using T-SQL stored procedures. All Python code is included.|

<a name ="bkmk_samples"></a>

## Code samples

These samples and demos provided by the SQL Server development team highlight ways that you can use embedded analytics in real-world applications.

| Link | Description |
|------|-------------|
| [Build a predictive model using Python and SQL Server](https://microsoft.github.io/sql-ml-tutorials/python/rentalprediction/) | Learn how a ski rental business might use machine learning to predict future rentals, which helps the business plan and staff to meet future demand. |
| [Perform customer clustering using Python and SQL Server](https://microsoft.github.io/sql-ml-tutorials/python/customerclustering/) | Learn how to use the Kmeans algorithm to perform unsupervised clustering of customers. |

## See also

+ [Python extension to SQL Server](../concepts/extension-python.md)
+ [SQL Server Machine Learning Services tutorials](machine-learning-services-tutorials.md)
