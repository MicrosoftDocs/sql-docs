---
title: SQL Server machine learning and extensibility product documentation | Microsoft Docs
ms.prod: sql
ms.technology: machine-learning

ms.date: 08/30/2018  
ms.topic: overview
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---

# SQL Server Machine Learning and Extensibility Documentation

Learn how to use external libraries and languages on resident, relational data with our quickstarts, tutorials, and how-to articles. [SQL Server machine learning](what-is-sql-server-machine-learning.md) extends the database engine to include embedded R, Python, and Java for running external code on SQL Server. 

R and Python libraries include base distributions plus Microsoft's machine learning algorithms and function libraries, enabling high-performance predictive analytics without having to transfer data across the network.
Java code execution uses the same extensibility framework as R and Python, but does not include Java-equivalent packages for machine learning or embedded analytics at scale.

|   |   | 
|---|---|-
| ![R logo](./media/index/logo_r.png) | Open-source R, extended with [RevoScaleR](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler) and Microsoft AI algorithms in [MicrosoftML](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/microsoftml-package). These libraries give you forecasting and prediction models, statistical analysis, visualization, and data manipulation at scale. <br/>R integration starts in [SQL Server 2016](./install/sql-r-services-windows-install.md) and is also in [SQL Server 2017](./install/sql-machine-learning-services-windows-install.md). | 
| ![Python logo](./media/index/logo_python.png) | Python developers can use Microsoft [revoscalepy](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/revoscalepy-package) and [microsoftml](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/microsoftml-package) libraries for predictive analytics and machine learning at scale. Anaconda and Python 3.5-compatible libraries are the baseline distribution. <br/>Python integration starts in [SQL Server 2017](./install/sql-machine-learning-services-windows-install.md).  | 
| ![Java logo](./media/index/logo_java.png) | Java developers can wrap code in stored procedures or in a binary format accessible through Transact-SQL. <br/>Java integration is currently [SQL Server vNext only](./install/sql-machine-learning-services-vnext.md). |

## 5-Minute Quickstarts

+ [Create a predictive model in R](./tutorials/rtsql-create-a-predictive-model-r.md)

+ [Predict and plot from model using R](./tutorials/rtsql-predict-and-plot-from-model.md)


## Step-by-Step Tutorials

+ [How to add machine learning and extensibility framework to SQL Server](install/sql-machine-learning-services-windows-install.md)

+ [How to execute R from T-SQL and stored procedures](./tutorials/sqldev-in-database-r-for-sql-developers.md)

+ [How to use embedded analytics in Python](./tutorials/sqldev-in-database-python-for-sql-developers.md)


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