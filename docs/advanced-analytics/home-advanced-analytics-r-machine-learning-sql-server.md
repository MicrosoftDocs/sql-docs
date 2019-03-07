---
title: R and Python machine learning and programming extensions documentation - SQL Server Machine Learning
description: R and Python in SQL Server, with built-in data science modeling and machine learning algorithms for enterprise data analysis at scale.
ms.prod: sql
ms.technology: machine-learning

ms.date: 01/10/2019
ms.topic: overview
author: HeidiSteen
ms.author: heidist
manager: cgronlun
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
---
# SQL Server Machine Learning

::: moniker range="=sql-server-ver15||=sqlallproducts-allversions"

## SQL Server Machine Learning and Programming Extensions Documentation

Learn how to use R and Python external libraries and languages on resident, relational data with our quickstarts, tutorials, and how-to articles. R and Python libraries in [SQL Server machine learning](what-is-sql-server-machine-learning.md) include base distributions, data science models, machine learning algorithms, and functions for conducting high-performance analytics at scale, without having to transfer data across the network.

In SQL Server 2019, Java code execution uses the same extensibility framework as R and Python, but does not include data science and machine learning function libraries. For more information about new features, see [What's New in SQL Server Machine Services](what-s-new-in-sql-server-machine-learning-services.md).

|   |   |
|---|:--|
| ![R logo](media/index/logo_r.png) | Open-source R, extended with [RevoScaleR](/machine-learning-server/r-reference/revoscaler/revoscaler) and Microsoft AI algorithms in [MicrosoftML](/machine-learning-server/r-reference/microsoftml/microsoftml-package). These libraries give you forecasting and prediction models, statistical analysis, visualization, and data manipulation at scale.<br/>R integration starts in [SQL Server 2016](install/sql-r-services-windows-install.md) and is also in [SQL Server 2017](install/sql-machine-learning-services-windows-install.md). |
| ![Python logo](media/index/logo_python.png) | Python developers can use Microsoft [revoscalepy](/machine-learning-server/python-reference/revoscalepy/revoscalepy-package) and [microsoftml](/machine-learning-server/python-reference/microsoftml/microsoftml-package) libraries for predictive analytics and machine learning at scale. Anaconda and Python 3.5-compatible libraries are the baseline distribution.<br/>Python integration starts in [SQL Server 2017](install/sql-machine-learning-services-windows-install.md). |
| ![Java logo](media/index/logo_java.png) | Java developers can use the [Java language extension](java/extension-java.md) to wrap code in stored procedures or in a binary format accessible through Transact-SQL.<br/>Java integration starts in [SQL Server 2019 - preview](install/sql-machine-learning-services-ver15.md). |
| &nbsp; | &nbsp; |
::: moniker-end

::: moniker range="=sql-server-2016||=sql-server-2017"

## SQL Server Machine Learning R and Python Documentation

Learn how to use R and Python external libraries and languages on resident, relational data with our quickstarts, tutorials, and how-to articles. R and Python libraries in [SQL Server machine learning](what-is-sql-server-machine-learning.md) include base distributions, data science models, machine learning algorithms, and functions for conducting high-performance analytics at scale, without having to transfer data across the network.

|   |   |
|---|:--|
| ![R logo](media/index/logo_r.png) | Open-source R, extended with [RevoScaleR](/machine-learning-server/r-reference/revoscaler/revoscaler) and Microsoft AI algorithms in [MicrosoftML](/machine-learning-server/r-reference/microsoftml/microsoftml-package). These libraries give you forecasting and prediction models, statistical analysis, visualization, and data manipulation at scale.<br/>R integration starts in [SQL Server 2016](install/sql-r-services-windows-install.md) and is also in [SQL Server 2017](install/sql-machine-learning-services-windows-install.md). |
| ![Python logo](media/index/logo_python.png) | Python developers can use Microsoft [revoscalepy](/machine-learning-server/python-reference/revoscalepy/revoscalepy-package) and [microsoftml](/machine-learning-server/python-reference/microsoftml/microsoftml-package) libraries for predictive analytics and machine learning at scale. Anaconda and Python 3.5-compatible libraries are the baseline distribution.<br/>Python integration starts in [SQL Server 2017](install/sql-machine-learning-services-windows-install.md). |
| &nbsp; | &nbsp; |
::: moniker-end

## 5-Minute Quickstarts

- [Create a predictive model in R](tutorials/rtsql-create-a-predictive-model-r.md)

- [Predict and plot from model using R](tutorials/rtsql-predict-and-plot-from-model.md)

## Step-by-Step Tutorials

- [How to add machine learning and extensibility framework to SQL Server](install/sql-machine-learning-services-windows-install.md)

- [How to execute R from T-SQL and stored procedures](tutorials/sqldev-in-database-r-for-sql-developers.md)

- [How to use embedded analytics in Python](tutorials/sqldev-in-database-python-for-sql-developers.md)

## Video Introduction

> [!VIDEO https://www.youtube.com/embed/ACejZ9optCQ]

## Reference

| Package | Language | Description |
|:--------|:---------|:------------|
| [RevoScaleR](/machine-learning-server/r-reference/revoscaler/revoscaler) | R | Distributed and parallel processing for R tasks: data transformation, exploration, visualization, statistical and predictive analytics. |
| [MicrosoftML](/machine-learning-server/r-reference/microsoftml/microsoftml-package) | R | Functions based on Microsoft's AI algorithms, adapted for R. |
| [olapR](/machine-learning-server/r-reference/olapr/olapr) | R | Imports data from OLAP cube.s |
| [sqlRUtils](/machine-learning-server/r-reference/sqlrutils/sqlrutils) | R | Helper functions for encapsulating R and T-SQL. |
[revoscalepy](/machine-learning-server/python-reference/revoscalepy/revoscalepy-package) | Python | Distributed and parallel processing for Python tasks: data transformation, exploration, visualization, statistical and predictive analytics. |
| [microsoftml](/machine-learning-server/python-reference/microsoftml/microsoftml-package) | Python | Functions based on Microsoft's AI algorithms, adapted for Python. |
| &nbsp; | &nbsp; | &nbsp; |
