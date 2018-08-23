---
title: SQL Server machine learning and extensibility | Microsoft Docs
ms.prod: sql
ms.technology: machine-learning

ms.date: 08/30/2018  
ms.topic: overview
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---

# SQL Server MLS and Extensibility Documentation

SQL Server machine learning extends the database engine to include embedded R, Python, and Java for running external code on resident, relational data.

R and Python libraries include base distributions, extended with Microsoft machine learning algorithms and a comprehensive collection of functions for predictive analytics at scale. Data manipulation and transformation, exploration and visualization, and analysis are available at the instance level without having to load or transfer data across the network.

Java extensions are currently in preview. Using SQL Server vNext, you can operationalize Java code in stored procedures, or access code through Transact-SQL operating on local relational data.

|   |   |   |   |   |   |
|---|---|---|---|---|---|
| [R logo](media/index/logo_r.svg) |  Open-source R, extended with Microsoft AI algorithms for machine learning workloads and statistical analysis, visualization, and data manipulation at scale. | [Python logo](media/index/logo_python.svg)  |  Python developers can use Microsoft libraries for predictive analytics and machine learning at scale, using functions from revoscalepy and microsoftml. Anaconda and Python 3.5-compatible libraries are supported.  | [Java logo](media/index/logo_java.svg) | (SQL Server vNext only) Java developers can wrap code in stored procedures or in a binary format accessible through Transact-SQL. |

> [!VIDEO https://www.youtube.com/embed/ACejZ9optCQ]


## 5-Minute Quickstarts

Links to quickstarts go here.

## Step-by-Step Tutorials

+ [How to execute R from T-SQL and stored procedures](/tutorials/sql-dev-r-tutorials.md)
+ [How to embed analytics in Python](tutorials/sqldev-in-database-python-for-sql-developers.md)
+ [Create a predictive model in R](tutorials/rtsql-create-a-predictive-model.md)

## Reference

R library
Python library