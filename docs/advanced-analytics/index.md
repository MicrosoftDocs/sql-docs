---
title: SQL Server AI and extensibility product documentation | Microsoft Docs
ms.prod: sql
ms.technology: machine-learning

ms.date: 08/30/2018  
ms.topic: overview
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---

# SQL Server AI and Extensibility Documentation

SQL Server machine learning extends the database engine to include embedded R, Python, and Java for running external code on resident, relational data. Learn how to use external libraries and languages on relational data with our quickstarts, tutorials, and how-to articles.

R and Python libraries include base distributions, extended with Microsoft machine learning algorithms and a comprehensive collection of functions for predictive analytics at scale. Data manipulation and transformation, exploration and visualization, and analysis are available at the instance level without having to load or transfer data across the network.

|   |   | 
|---|---|-
| ![R logo](./media/index/logo_r.png) | Open-source R, extended with [RevoScaleR](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler) and Microsoft AI algorithms in [MicrosoftML](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/microsoftml-package). These libraries give you forecasting and prediction models, statistical analysis, visualization, and data manipulation at scale. <br/>R integration starts in [SQL Server 2016](./install/sql-r-services-windows-install.md).| 
| ![Python logo](./media/index/logo_python.png) | Python developers can use Microsoft [revoscalepy](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/revoscalepy-package) and [microsoftml](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/microsoftml-package) libraries for predictive analytics and machine learning at scale. Anaconda and Python 3.5-compatible libraries are the baseline distribution. <br/>Python integration starts in [SQL Server 2017](sql-machine-learning-services-windows-install.md).  | 
| ![Java logo](./media/index/logo_java.png) | Java developers can wrap code in stored procedures or in a binary format accessible through Transact-SQL. Java support uses the same extensibility framework as R and Python, but does not have the Microsoft machine learning libraries (such as RevoScaleR or revoscalepy) for those languages.<br/>Java integration is currently [SQL Server vNext only](./install/sql-machine-learning-services-vnext.md). |

## 5-Minute Quickstarts

Links to quickstarts go here...

## Step-by-Step Tutorials

+ [How to execute R from T-SQL and stored procedures](./tutorials/sqldev-in-database-r-for-sql-developers.md)
+ [How to embed analytics in Python](./tutorials/sqldev-in-database-python-for-sql-developers.md)
+ [Create a predictive model in R](./tutorials/rtsql-create-a-predictive-model-r.md)

## Video Introduction

> [!VIDEO https://www.youtube.com/embed/ACejZ9optCQ]

## Reference

| Package | Language | Description | 
|---------|----------|-------------|
| [RevoScaleR](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler) | R | Distributed and parallel processing for R tasks: data transformation, exploration, visualization, statistical and predictive analytics. |
| [MicrosoftML](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/microsoftml-package) | R | Functions based on Microsoft's AI algorithms, adapted for R. |
| [olapR](https://docs.microsoft.com/machine-learning-server/r-reference/olapr/olapr) | R | Imports data from OLAP cube.s |
| [sqlRUtils]() | R | Helper functions for encapsulating R and T-SQL. |
[revoscalepy](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/revoscalepy-package) | Python | Distributed and parallel processing for Python tasks: data transformation, exploration, visualization, statistical and predictive analytics.  | 
| [microsoftml](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/microsoftml-package) | Python | Functions based on Microsoft's AI algorithms, adapted for Python.  |